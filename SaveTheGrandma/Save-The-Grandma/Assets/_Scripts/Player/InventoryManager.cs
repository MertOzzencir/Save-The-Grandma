using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    [SerializeField] private ItemSlot[] _slots;
    [SerializeField] private GameObject _inventory;
    [SerializeField] private GameObject _inventoryVisual;

    public bool CanOpenInventory;
    private Vector3 _localScale;
    void Start()
    {
        Instance = this;
        InputManager.OnTab += OpenInventory;
        InputManager.OnClose += CloseEsc;
        _localScale = _inventoryVisual.transform.localScale;
        _inventoryVisual.transform.localScale = Vector3.zero;
    }

    private void CloseEsc()
    {
        CanOpenInventory = true;
        OpenInventory();
    }

    public void OpenInventory()
    {
        if (!CanOpenInventory)
        {
            TweenManager.ScaleObject(_inventoryVisual.transform, _localScale, .1f,DG.Tweening.Ease.OutElastic);
            CanOpenInventory = true;
            _inventory.SetActive(CanOpenInventory);
        }
        else {
            TweenManager.ScaleObject(_inventoryVisual.transform, Vector3.zero, 0.1f,DG.Tweening.Ease.OutElastic);
            CanOpenInventory = false;
            _inventory.SetActive(CanOpenInventory);
        }

    }
   
  

    public void CollectItem(InventoryInformation inventoryInformation)
    {
        ItemSlot pickedSlot = null;
        foreach (var a in _slots)
        {
            if (a.InventoryType == inventoryInformation.InventoryType)
            {
                pickedSlot = a;
            }
        }
        if (pickedSlot != null)
        {
            pickedSlot.UpdateItemAmount(+1);
        }
        else
        {
            foreach (var a in _slots)
            {
                if (a.InventoryType == InventoryType.Null)
                {
                    a.ItemIcon.sprite = inventoryInformation.InventoryIcon;
                    a.ItemIcon.color = new Color(255, 255, 255, 1);
                    a.UpdateItemAmount(+1);
                    a.InventoryType = inventoryInformation.InventoryType;
                    a.MaterialName.text = inventoryInformation.InventoryName;
                    a.MaterialName.fontSize = inventoryInformation.FontSize;
                    return;
                }
            }
        }
    }
}

