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

    public bool CanOpenInventory;
    void Start()
    {
        Instance = this;
        InputManager.OnTab += OpenInventory;
        InputManager.OnClose += CloseEsc;
    }

    private void CloseEsc()
    {
        CanOpenInventory = true;
        OpenInventory();
    }

    public void OpenInventory()
    {
        Debug.Log("Trying to Inventory");
        if (!CanOpenInventory)
        {
            Debug.Log("Open?");
            CanOpenInventory = true;
            _inventory.SetActive(CanOpenInventory);
        }
        else {
            Debug.Log("Close");
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
                    a.MaterialName.fontSize = inventoryInformation.FontSize;
                    return;
                }
            }
        }
    }
}

