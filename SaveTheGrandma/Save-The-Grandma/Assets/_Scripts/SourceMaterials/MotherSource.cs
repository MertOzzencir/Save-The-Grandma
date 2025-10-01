using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class MotherSource : MonoBehaviour
{

    [SerializeField] private SourceSO _sourceSO;
    private ChildSource[] _childMaterials;
    private List<ChildSource> _allChildInfo = new List<ChildSource>();
    private int _totalChild;
    private int _indexOfChildCount = 0;
    private Spawner _ownSpawnObject;
    private float _lastTimeDiged;
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
            _allChildInfo.Add(a);
        }
    }

    public void Dig(int attackDamage, ToolType _recievedToolType)
    {
        
        if (_indexOfChildCount > _totalChild - 1)
                return;

        if (_recievedToolType != _sourceSO.AToolCanDig)
            return;

        ChildSource refChild = _allChildInfo.FirstOrDefault(value => value._childIndex == _indexOfChildCount);
        if (refChild != null)
        {
            refChild.ChildMaterialHealth -= attackDamage;
            _lastTimeDiged = Time.time;
            if (refChild.GetHealth() <= 0)
            {
                _allChildInfo.Remove(refChild);
                HandleChildDeath(refChild);
            }
        }

    }

    private void HandleChildDeath(ChildSource a)
    {
        _indexOfChildCount++;
        a.HandleDeath();
        HandleChildCount();
    }

    private void HandleChildCount()
    {
        if (_allChildInfo.Count <= 0)
        {
            _ownSpawnObject._spawnAvaliableArray[transform.parent].Avaliable = true;
            _ownSpawnObject._spawnTimer = 0;
            Destroy(gameObject, 1);
        }
    }
    public void SetSpawnerObject(Spawner obj)
    {
        _ownSpawnObject = obj;
    }
}
