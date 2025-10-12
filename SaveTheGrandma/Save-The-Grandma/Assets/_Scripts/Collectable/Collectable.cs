

using System;
using System.Collections;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public CollectableSO _collectableData;
    public ToolType ToolType { get; set; }
    public bool Collected;

    private bool _canTurn;
    private Vector3 _targetPosition;
    private float sinX;
    private float up;
    public virtual void Start()
    {
        ToolType = _collectableData.InventoryInformation.ToolCanGather;
        SetIdleAnimation();
    }

    public void SetIdleAnimation()
    {
        float randomRotation = UnityEngine.Random.Range(0f, 180f);
        transform.Rotate(0, randomRotation, 0);
        Invoke(nameof(IdleAnimation), 1f);
    }

    public virtual void Collect(ToolType toolType)
    {
        if (toolType == ToolType && !Collected)
        {
            Collected = true;
            InventoryManager.Instance.CollectItem(_collectableData.InventoryInformation);
            StartCoroutine(CollectAnimation());
            Destroy(gameObject,1f);
        }
    }
    public virtual void Update()
    {
        if (_canTurn != true)
            return;

        sinX += 1 * Time.deltaTime;
        up += 35 * Time.deltaTime;
        if (Vector3.Distance(transform.position, _targetPosition) > 0.01f)
            transform.position = Vector3.Lerp(transform.position, _targetPosition, 0.8f * Time.deltaTime);
        float sin = Mathf.Sin(sinX);
        transform.rotation = Quaternion.Euler(sin * 15f, up, 0);


    }
    private void IdleAnimation()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        _canTurn = true;
        _targetPosition = transform.position + new Vector3(0, 2.5f, 0);
        transform.up = Vector3.up;
    }

    public IEnumerator CollectAnimation()
    {
        while (true)
        {
            transform.position += Vector3.up * Time.deltaTime * 20f;
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, 1.5f * Time.deltaTime); 
            yield return null;
        }
    }

}
