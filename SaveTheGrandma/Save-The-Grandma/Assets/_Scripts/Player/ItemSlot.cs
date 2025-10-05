using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ItemSlot : MonoBehaviour
{
    public Image ItemIcon;
    public int ItemAmount;
    public TextMeshProUGUI ItemAmountUI;
    public InventoryType SlotType;

    public InventoryType GetSlotType()
    {
        return SlotType;
    }
}

