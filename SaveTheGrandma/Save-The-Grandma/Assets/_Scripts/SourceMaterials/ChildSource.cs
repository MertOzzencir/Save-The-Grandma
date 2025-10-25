using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class ChildSource : MonoBehaviour
{
    public Collectable _sourceMaterial { get; set; }
    public int ChildMaterialHealth { get; set; }
    public Vector3 ForceDirection { get; set; }
    public float ForcePower { get; set; }
    public int MaterialDropCount { get; set; }
    public int _childIndex { get; private set; }
    void Start()
    {
        _childIndex = transform.GetSiblingIndex();
            
    }

    public int GetHealth()
    {
        return ChildMaterialHealth;
    }
    public void OnDig(int damage)
    {
        ChildMaterialHealth -= damage;
        StartCoroutine(HandleAnimation(this, .2f, new Vector3(0.5f, 0.5f, 0.5f)));

    }

    public void HandleDeath()
    {
        Collider cod = GetComponent<Collider>();
        cod.enabled = true;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.AddForce(ForceDirection * ForcePower, ForceMode.Impulse);
        Invoke(nameof(CreateSourceMaterial), 1f);
    }

    private void CreateSourceMaterial()
    {
        for (int i = 0; i < MaterialDropCount; i++)
        {
            Instantiate(_sourceMaterial, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
        private IEnumerator HandleAnimation(ChildSource obj, float delayTime, Vector3 animationScale)
    {
        Vector3 originalScale = obj.transform.localScale;
        TweenManager.ScaleObject(this.transform, animationScale,delayTime/4 , Ease.OutBack);
        yield return new WaitForSeconds(delayTime/4);
        TweenManager.ScaleObject(this.transform, originalScale, delayTime, Ease.OutBack);
    }

  
}
