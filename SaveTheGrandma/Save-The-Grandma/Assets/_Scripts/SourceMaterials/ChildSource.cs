using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class ChildSource : MonoBehaviour
{
    
    public Collectable _sourceMaterial{ get; set; }
    public int ChildMaterialHealth{ get; set; }
    public int _childIndex { get; private set; }
    void Start()
    {
        _childIndex = transform.GetSiblingIndex();
    }

    public int GetHealth()
    {
        return ChildMaterialHealth;
    }

    public void HandleDeath()
    {
        Collider cod = GetComponent<Collider>();
        cod.enabled = true;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.AddExplosionForce(225f, transform.position,15f,225f, ForceMode.Impulse);
        Invoke(nameof(CreateSourceMaterial),1f);
        
    }

    private void CreateSourceMaterial() {
        Instantiate(_sourceMaterial, transform.position, Quaternion.identity);
        Destroy(gameObject, 1);
    }

  
}
