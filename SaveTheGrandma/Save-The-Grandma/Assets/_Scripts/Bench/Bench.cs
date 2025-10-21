using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;using UnityEngine.EventSystems;

public class Bench : MonoBehaviour
{
    public List<Craftable> AllCraftables;

    [SerializeField] private GameObject _craftMenu;
    [SerializeField] private Image _mainCraftIcon;
    [SerializeField] private TextMeshProUGUI _craftMaterialName;
    [SerializeField] private TextMeshProUGUI[] _materialAmountText;
    [SerializeField] private TextMeshProUGUI[] _materialNameText;
    [SerializeField] private Image[] _materialReq;
    [SerializeField] private Image _outputIcon;
    [SerializeField] private float _craftTimer;
    [SerializeField] private Slider _craftUISlider;

    public Craftable ActiveCraftItem { get; set; }
    public bool UIColliderCheck{ get; set;}

    private Animator _anim;
    private bool _duringCraft;
    private float _uiCraftTimer;
    public bool OpenMenu;
    private int _craftArrayIndex;
    private Collectable[] _refCollectables = new Collectable[2];
    private int[] _refAmount = new int[2];
    private InventoryManager _inventory;
    private ToolUseManager _toolManager;
    private EntityAudioManager _audioManager;
    private int[] _sendBackCollectables = new int[2];
    public virtual void Start()
    {
        _craftUISlider.maxValue = _craftTimer;
        _audioManager = GetComponent<EntityAudioManager>();
        _toolManager = FindAnyObjectByType<ToolUseManager>();
        SetCraftItem(_craftArrayIndex);
        _inventory = FindAnyObjectByType<InventoryManager>();
        _anim = GetComponentInChildren<Animator>();
        _anim.GetComponent<AnimationEventHandler>().OnAnimEventFire += CraftClip;
        InputManager.OnClose += CloseEsc;

    }
    void Update()
    {
        if (OpenMenu)
        {
            var ped = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
            var hits = new List<RaycastResult>();
            EventSystem.current.RaycastAll(ped, hits);

            foreach (var h in hits)
            {
                var s = h.gameObject.GetComponentInParent<Canvas>();
                if (s != null)
                {
                    UIColliderCheck = true;
                    return;
                }

            }
            UIColliderCheck = false;
        }
    }

    public void CloseEsc()
    {
        if (OpenMenu == true)
        {
            OpenMenu = true;
            OpenCraftMenu();
        }

    }

    private void CraftClip(int index)
    {
        switch (index)
        {
            case 1: 
             _audioManager.VolumeSet(0.25f);
             _audioManager.PlayClipByIndex(2);
            break;
        }
       
    }
    public virtual void OpenCraftMenu()
    {


        _audioManager.VolumeSet(1f);
        Debug.Log("as");
        OpenMenu = !OpenMenu;
        if (OpenMenu)
        {
            _audioManager.PlayClipByIndex(0);
            _toolManager.SetUnactiveTool();
            _anim.SetBool("isOpen", true);
            BenchController.Instance.SetActiveBench(this);
            _craftMenu.SetActive(OpenMenu);
            if(_inventory.CanOpenInventory != true)
                _inventory.OpenInventory();        
        }
        else
        {
            UIColliderCheck = false;
            _audioManager.PlayClipByIndex(1);
            _anim.SetBool("isOpen", false);
            BenchController.Instance.ResetActiveBench();
            _craftMenu.SetActive(OpenMenu);
        }

    }

    public void SetCraftItem(int index)
    {
        ActiveCraftItem = AllCraftables[index];
        _mainCraftIcon.sprite = AllCraftables[index].SOData.InventoryInformation.InventoryIcon;
        _craftMaterialName.text = AllCraftables[index].SOData.InventoryInformation.InventoryName;
        SetCraftItemReq(index);
    }
    private void SetCraftItemReq(int index)
    {
        ResetCraftItem();
        if (AllCraftables[index].SOData._recipes.Count == 1)
        {
            _refCollectables[0] = AllCraftables[index].SOData._recipes[0].Collectable;
            _materialReq[0].sprite = _refCollectables[0]._collectableData.InventoryInformation.InventoryIcon;
            _materialReq[0].color = new Color(255, 255, 255, 1);
            _materialReq[1].color = new Color(255, 255, 255, 0);
            _refAmount[0] = AllCraftables[index].SOData._recipes[0].Amount;
            _materialAmountText[0].text = _refAmount[0].ToString();
            _materialNameText[0].text = AllCraftables[index].SOData._recipes[0].Collectable._collectableData.InventoryInformation.InventoryName;
        }
        else
        {
            _refCollectables[0] = AllCraftables[index].SOData._recipes[0].Collectable;
            _materialReq[0].sprite = _refCollectables[0]._collectableData.InventoryInformation.InventoryIcon;
            _materialReq[0].color = new Color(255, 255, 255, 1);
            _refAmount[0] = AllCraftables[index].SOData._recipes[0].Amount;
            _materialAmountText[0].text = _refAmount[0].ToString();
            _materialNameText[0].text = AllCraftables[index].SOData._recipes[0].Collectable._collectableData.InventoryInformation.InventoryName;

            _refCollectables[1] = AllCraftables[index].SOData._recipes[1].Collectable;
            _materialReq[1].sprite = _refCollectables[1]._collectableData.InventoryInformation.InventoryIcon;
            _materialReq[1].color = new Color(255, 255, 255, 1);
            _refAmount[1] = AllCraftables[index].SOData._recipes[1].Amount;
            _materialAmountText[1].text = _refAmount[1].ToString();
            _materialNameText[1].text = AllCraftables[index].SOData._recipes[1].Collectable._collectableData.InventoryInformation.InventoryName;
        }

    }
    public void RecieveItem(InventoryType slotType, Collectable fixedCollectable, out bool materialDecrease)
    {
        if (_duringCraft)
        {
            materialDecrease = false;
            return;
        }
        materialDecrease = false;
        for (int i = 0; i < _refCollectables.Length; i++)
        {
            if (_refCollectables[i] == fixedCollectable)
            {
                if (_refAmount[i] > 0)
                {
                    _refAmount[i]--;
                    _sendBackCollectables[i]++;
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
        Create();

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
    public virtual void Create()
    {
        if (CanCraft())
        {
            StartCoroutine(CreateCoolDown());
        }
    }
    IEnumerator CreateCoolDown()
    {
        _anim.SetBool("canCraft", true);
        _craftUISlider.gameObject.SetActive(true);
        _duringCraft = true;
        _uiCraftTimer = 0;
        while (_uiCraftTimer <= _craftTimer)
        {
            _uiCraftTimer += Time.deltaTime;
            _craftUISlider.value = _uiCraftTimer;
            yield return null;
        }
        InventoryManager.Instance.CollectItem(ActiveCraftItem.SOData.InventoryInformation);
        _outputIcon.sprite = ActiveCraftItem.SOData.InventoryInformation.InventoryIcon;
        _outputIcon.color = new Color(255, 255, 255, 1);
        _craftUISlider.value = 0;
        _duringCraft = false;
        _craftUISlider.gameObject.SetActive(false);
        _anim.SetBool("canCraft", false);

    }
    public void ChangeCraftSelection(int Amount)
    {
        if (_duringCraft)
            return;
        _craftArrayIndex += Amount;
        _outputIcon.sprite = null;
        _outputIcon.color = new Color(255, 255, 255, 0);
        if (_craftArrayIndex >= AllCraftables.Count)
            _craftArrayIndex = 0;

        if (_craftArrayIndex < 0)
            _craftArrayIndex = AllCraftables.Count - 1;
        
        for(int i = 0; i < 2; i++)
        {
            if (_sendBackCollectables[i] != 0)
            {
                for(int y =0; y < _sendBackCollectables[i]; y++)
                {
                    InventoryManager.Instance.CollectItem(_refCollectables[i]._collectableData.InventoryInformation);
                }
            }
        }

        SetCraftItem(_craftArrayIndex);
    }
    private void ResetCraftItem()
    {
        for (int i = 0; i < 2; i++)
        {
            _sendBackCollectables[i] = 0;
            _materialReq[i].sprite = null;
            _materialAmountText[i].text = "";
            _refCollectables[i] = null;
            _refAmount[i] = 0;
            _materialAmountText[i].text = "";
            _materialNameText[i].text = "";
        }
    }
}

