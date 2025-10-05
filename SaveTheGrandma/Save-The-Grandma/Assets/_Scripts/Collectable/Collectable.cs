

using System;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public static event Action<InventoryInformation> OnItemCollected;
    public CollectableSO _collectableData;
    public ToolType ToolType { get; set; }

    private bool _canTurn;
    private Vector3 _targetPosition;
    private float sinX;
    private float up;
    public virtual void Start()
    {
        ToolType = _collectableData.InventoryInformation.ToolCanGather;
        float randomRotation = UnityEngine.Random.Range(0f, 180f);
        transform.Rotate(0, randomRotation, 0);
        Invoke(nameof(OnStartAnimation), 1f);
    }
    public virtual void Collect(ToolType toolType)
    {
        if (toolType == ToolType)
        {
            SendItemToInventory(_collectableData.InventoryInformation);
            Destroy(gameObject);
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

    public void SendItemToInventory(InventoryInformation a)
    {
        OnItemCollected?.Invoke(a);
    }

    private void OnStartAnimation()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        _canTurn = true;
        _targetPosition = transform.position + new Vector3(0, 2.5f, 0);
        transform.up = Vector3.up;
    }

}
