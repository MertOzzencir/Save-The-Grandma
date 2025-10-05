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
    [SerializeField] private TextMeshProUGUI[] _craftMaterialNameText;
    [SerializeField] private Image[] _materialReq;
    [SerializeField] private Image _outputIcon;

    public Craftable ActiveCraftItem;

    private bool _openMenu;
    private int _craftArrayIndex;
    void Start()
    {
        SetCraftItem(_craftArrayIndex);
    }
    public void OpenCraftMenu()
    {
        _openMenu = !_openMenu;
        if (_openMenu)
        {
            BenchController.Instance.SetActiveBench(this);
            _craftMenu.SetActive(_openMenu);
        }
        else
        {
            BenchController.Instance.ResetActiveBench();
            _craftMenu.SetActive(_openMenu);
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
        ResetAll();
        if (_allCraftable[index].SOData._recipes.Count == 1)
        {
            _materialReq[0].sprite = _allCraftable[index].SOData._recipes[0].Collectable._collectableData.InventoryInformation.InventoryIcon;
            _craftMaterialNameText[0].text = _allCraftable[index].SOData._recipes[0].Amount.ToString();
        }
        else
        {
            _materialReq[0].sprite = _allCraftable[index].SOData._recipes[0].Collectable._collectableData.InventoryInformation.InventoryIcon;
            _craftMaterialNameText[0].text = _allCraftable[index].SOData._recipes[0].Amount.ToString();
            _materialReq[1].sprite = _allCraftable[index].SOData._recipes[1].Collectable._collectableData.InventoryInformation.InventoryIcon;
            _craftMaterialNameText[1].text = _allCraftable[index].SOData._recipes[1].Amount.ToString();
        }

    }
    public void ChangeCraftSelection(int Amount)
    {
        _craftArrayIndex += Amount;
        Debug.Log(_craftArrayIndex);

        if (_craftArrayIndex >= _allCraftable.Length)
            _craftArrayIndex = 0;

        if (_craftArrayIndex < 0)
            _craftArrayIndex = _allCraftable.Length - 1;
        Debug.Log("sa");
        Debug.Log(_craftArrayIndex);


        SetCraftItem(_craftArrayIndex);
    }
    private void ResetAll()
    {

        _materialReq[0].sprite = null;
        _craftMaterialNameText[0].text = "";
        _materialReq[1].sprite = null;
        _craftMaterialNameText[1].text = "";



    }
}

