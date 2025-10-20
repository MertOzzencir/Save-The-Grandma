using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ItemSlot : MonoBehaviour
{
    public event Action<bool,Image> OnAmountChange;
    public Image ItemIcon;
    public int ItemAmount;
    public TextMeshProUGUI ItemAmountUI;
    public InventoryType InventoryType;
    public TextMeshProUGUI MaterialName;
    public Image BackGround;

    void Awake()
    {
        BackGround = GetComponent<Image>();
    }
    public InventoryType GetSlotType()
    {
        return InventoryType;
    }
    public void UpdateItemAmount(int Amount)
    {
        ItemAmount += Amount;
        ItemAmountUI.text = ItemAmount.ToString();
        bool isPositive = Amount > 0 ? true : false;
        if (gameObject.activeInHierarchy)
        {
            Debug.Log(gameObject.activeSelf);
            OnAmountChange?.Invoke(isPositive,BackGround);
        }
    }

    public void ResetSlot()
    {
        ItemIcon.sprite = null;
        ItemIcon.color = new Color(255, 255, 255, 0f);
        ItemAmount = 0;
        ItemAmountUI.text = "";
        InventoryType = InventoryType.Null;
        MaterialName.text = "";
    }
}

