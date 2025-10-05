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

    private Sprite _standartSprite;
    void Start()
    {
        _standartSprite = ItemIcon.sprite;
    }
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
        ItemIcon.sprite = _standartSprite;
        ItemAmount = 0;
        ItemAmountUI.text = "";
        InventoryType = InventoryType.Null;
        MaterialName.text = "";
    }
}

