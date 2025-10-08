using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Bench : MonoBehaviour
{
    [SerializeField] private Craftable[] _allCraftable;
    [SerializeField] private GameObject _craftMenu;
    [SerializeField] private Image _mainCraftIcon;
    [SerializeField] private TextMeshProUGUI _craftMaterialName;
    [SerializeField] private TextMeshProUGUI[] _materialAmountText;
    [SerializeField] private TextMeshProUGUI[] _materialNameText;
    [SerializeField] private Image[] _materialReq;
    [SerializeField] private Image _outputIcon;
    [SerializeField] private Transform SpawnPosition;


    public Craftable ActiveCraftItem { get; set; }

    private bool _openMenu;
    private int _craftArrayIndex;
    private Collectable[] _refCollectables = new Collectable[2];
    private int[] _refAmount = new int[2];
    private InventoryManager _inventory;
    void Start()
    {
        SetCraftItem(_craftArrayIndex);
        _inventory = FindAnyObjectByType<InventoryManager>();
    }
    public void OpenCraftMenu()
    {
        _openMenu = !_openMenu;
        if (_openMenu)
        {
            BenchController.Instance.SetActiveBench(this);
            _craftMenu.SetActive(_openMenu);
            _inventory.OpenWithBench(true);
        }
        else
        {
            BenchController.Instance.ResetActiveBench();
            _craftMenu.SetActive(_openMenu);
            _inventory.OpenWithBench(false);
        }

    }

    private void SetCraftItem(int index)
    {
        ActiveCraftItem = _allCraftable[index];
        _mainCraftIcon.sprite = _allCraftable[index].SOData.InventoryInformation.InventoryIcon;
        _craftMaterialName.text = _allCraftable[index].SOData.InventoryInformation.InventoryName;
        SetCraftItemReq(index);
    }
    private void SetCraftItemReq(int index)
    {
        ResetCraftItem();
        if (_allCraftable[index].SOData._recipes.Count == 1)
        {
            _refCollectables[0] = _allCraftable[index].SOData._recipes[0].Collectable;
            _materialReq[0].sprite = _refCollectables[0]._collectableData.InventoryInformation.InventoryIcon;
            _refAmount[0] = _allCraftable[index].SOData._recipes[0].Amount;
            _materialAmountText[0].text = _refAmount[0].ToString();
            _materialNameText[0].text = _allCraftable[index].SOData._recipes[0].Collectable._collectableData.InventoryInformation.InventoryName;
        }
        else
        {
            _refCollectables[0] = _allCraftable[index].SOData._recipes[0].Collectable;
            _materialReq[0].sprite = _refCollectables[0]._collectableData.InventoryInformation.InventoryIcon;
            _refAmount[0] = _allCraftable[index].SOData._recipes[0].Amount;
            _materialAmountText[0].text = _refAmount[0].ToString();
            _materialNameText[0].text = _allCraftable[index].SOData._recipes[0].Collectable._collectableData.InventoryInformation.InventoryName;

            _refCollectables[1] = _allCraftable[index].SOData._recipes[1].Collectable;
            _materialReq[1].sprite = _refCollectables[1]._collectableData.InventoryInformation.InventoryIcon;
            _refAmount[1] = _allCraftable[index].SOData._recipes[1].Amount;
            _materialAmountText[1].text = _refAmount[1].ToString();
            _materialNameText[1].text = _allCraftable[index].SOData._recipes[1].Collectable._collectableData.InventoryInformation.InventoryName;
        }

    }
    public void RecieveItem(InventoryType slotType, Collectable fixedCollectable, out bool materialDecrease)
    {
        materialDecrease = false;
        for (int i = 0; i < _refCollectables.Length; i++)
        {
            if (_refCollectables[i] == fixedCollectable)
            {
                if (_refAmount[i] > 0)
                {
                    _refAmount[i]--;
                    _materialAmountText[i].text = _refAmount[i].ToString();
                    materialDecrease = true;
                    break;
                }
                else
                {
                    materialDecrease = false;
                    break;
                }
            }
        }

    }

    public bool CanCraft()
    {
        if (_refAmount[0] <= 0 && _refAmount[1] <= 0)
        {
            SetCraftItem(_craftArrayIndex);
            return true;
        }
        return false;
    }
    public void Create()
    {
        if (CanCraft())
        {
            InventoryManager.Instance.CollectItem(ActiveCraftItem.SOData.InventoryInformation);
            _outputIcon.sprite = ActiveCraftItem.SOData.InventoryInformation.InventoryIcon;
        }
    }
    public void ChangeCraftSelection(int Amount)
    {
        _craftArrayIndex += Amount;
        _outputIcon.sprite = null;
        if (_craftArrayIndex >= _allCraftable.Length)
            _craftArrayIndex = 0;

        if (_craftArrayIndex < 0)
            _craftArrayIndex = _allCraftable.Length - 1;

        SetCraftItem(_craftArrayIndex);
    }
    private void ResetCraftItem()
    {
        for (int i = 0; i < 2; i++)
        {
            _materialReq[i].sprite = null;
            _materialAmountText[i].text = "";
            _refCollectables[i] = null;
            _refAmount[i] = 0;
            _materialAmountText[i].text = "";
            _materialNameText[i].text = "";
        }
    }
}

