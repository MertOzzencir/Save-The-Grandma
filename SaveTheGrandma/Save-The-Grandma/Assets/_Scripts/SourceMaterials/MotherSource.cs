using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MotherSource : Spawnable
{

    public SourceSO _sourceSO;
    public Transform DigPosition;
    private ChildSource[] _childMaterials;
    private List<ChildSource> _allChildInfo = new List<ChildSource>();
    private int _totalChild;
    private int _indexOfChildCount = 0;
     void Awake()
    {
        _childMaterials = GetComponentsInChildren<ChildSource>();
        _totalChild = transform.childCount;
        GetChildrenInfo(_childMaterials);
    }

    public void GetChildrenInfo(ChildSource[] childTransform)
    {
         foreach (var a in _childMaterials)
        {
            a.ChildMaterialHealth = _sourceSO.MaterialHeath;
            a._sourceMaterial = _sourceSO.SourceDropMaterial;
            a.ForceDirection = _sourceSO.GetRandomDirection();
            a.ForcePower = _sourceSO.OnChildDeathForcePower;
            a.MaterialDropCount = _sourceSO.MaterialDropCount;
            _allChildInfo.Add(a);
        }
    }

    public void Dig(int attackDamage, ToolType _recievedToolType,out bool isDead)
    {
        isDead = false;
        if (_indexOfChildCount > _totalChild - 1)
                return;

        if (_recievedToolType != _sourceSO.AToolCanDig)
            return;

        ChildSource refChild = _allChildInfo.FirstOrDefault(value => value._childIndex == _indexOfChildCount);
        if (refChild != null)
        {
            refChild.OnDig(attackDamage);

            if (refChild.GetHealth() <= 0)
            {
                _allChildInfo.Remove(refChild);
                HandleChildDeath(refChild);
                HandleChildCount(out isDead);
            }
        }

    }


    private void HandleChildDeath(ChildSource a)
    {
        _indexOfChildCount++;
        a.HandleDeath();
    }

    private void HandleChildCount(out bool isDead)
    {
        isDead = false;
        if (_indexOfChildCount >= _totalChild)
        {
            isDead = true;
            DigPosition.parent = null;
            Destroy(DigPosition.gameObject, 2f);
            ResetSpawner();
            Destroy(this);
            Destroy(gameObject, 1f);
        }
    }
    
}
