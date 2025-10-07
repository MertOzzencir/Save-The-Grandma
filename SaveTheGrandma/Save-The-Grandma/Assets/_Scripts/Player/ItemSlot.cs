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
    public InventoryType InventoryType;
    public TextMeshProUGUI MaterialName;

   
    public InventoryType GetSlotType()
    {
        return InventoryType;
    }
    public void UpdateItemAmount()
    {
        ItemAmountUI.text = ItemAmount.ToString();
    }

    public void ResetSlot()
    {
        ItemIcon.sprite = null;
        ItemIcon.color = new Color(255, 255, 255, .1f);
        ItemAmount = 0;
        ItemAmountUI.text = "";
        InventoryType = InventoryType.Null;
        MaterialName.text = "";
    }
}

