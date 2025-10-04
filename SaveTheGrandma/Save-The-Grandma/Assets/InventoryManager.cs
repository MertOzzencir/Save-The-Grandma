using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private ItemSlot[] _slots;
    [SerializeField] private GameObject _inventory;

    private bool _canOpen;
    void Start()
    {
        Collectable.OnItemCollected += CollectItem;
        InputManager.OnTab += OpenInventory;
    }

    private void OpenInventory()
    {
        _canOpen = !_canOpen;
        _inventory.SetActive(_canOpen);
    }

    private void CollectItem(InventoryInformation inventoryInformation)
    {
        ItemSlot pickedSlot = null;
        foreach (var a in _slots)
        {
            if (a.SlotType == inventoryInformation.InventoryType)
            {
                pickedSlot = a;
            }
        }
        if (pickedSlot != null)
        {
            pickedSlot.ItemAmount++;
            pickedSlot.ItemAmountUI.text = pickedSlot.ItemAmount.ToString();
        }
        else
        {
            foreach (var a in _slots)
            {
                if (a.SlotType == InventoryType.Null)
                {
                    a.ItemIcon.sprite = inventoryInformation.InventoryIcon;
                    a.ItemAmount++;
                    a.ItemAmountUI.text = a.ItemAmount.ToString();
                    a.SlotType = inventoryInformation.InventoryType;
                    return;
                }
            }
        }
    }
}

[Serializable]
public class ItemSlot
{
    public Image ItemIcon;
    public int ItemAmount;
    public TextMeshProUGUI ItemAmountUI;
    public InventoryType SlotType; 
}
