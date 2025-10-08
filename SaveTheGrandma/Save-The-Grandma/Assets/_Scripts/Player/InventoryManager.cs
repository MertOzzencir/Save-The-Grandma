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

    private bool _canOpen;
    void Start()
    {
        Instance = this;
        InputManager.OnTab += OpenInventory;
        InputManager.OnClose += OpenInventory;
    }

    public void OpenInventory()
    {
        if (_canOpen)
        {
            _canOpen = false;
            _inventory.SetActive(_canOpen);
        }
        else {
            _canOpen = true;
            _inventory.SetActive(_canOpen);
        }

    }
    public void OpenWithBench(bool checkFromBench)
    {
        _inventory.SetActive(checkFromBench);
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
            pickedSlot.ItemAmount++;
            pickedSlot.UpdateItemAmount();
        }
        else
        {
            foreach (var a in _slots)
            {
                if (a.InventoryType == InventoryType.Null)
                {
                    a.ItemIcon.sprite = inventoryInformation.InventoryIcon;
                    a.ItemIcon.color = new Color(255, 255, 255, 1);
                    a.ItemAmount++;
                    a.UpdateItemAmount();
                    a.InventoryType = inventoryInformation.InventoryType;
                    a.MaterialName.text = inventoryInformation.InventoryName;
                    return;
                }
            }
        }
    }
}

