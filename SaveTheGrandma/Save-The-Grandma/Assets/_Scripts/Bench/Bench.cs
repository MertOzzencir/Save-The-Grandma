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
        SetCraftItem(AllCraftables[0]);
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

    public void SetCraftItem(Craftable nextCraftable)
    {
        ActiveCraftItem = nextCraftable;
        _mainCraftIcon.sprite = ActiveCraftItem.SOData.InventoryInformation.InventoryIcon;
        _craftMaterialName.text = ActiveCraftItem.SOData.InventoryInformation.InventoryName;
        SetCraftItemReq(nextCraftable);
    }
    private void SetCraftItemReq(Craftable nextCraftable)
    {
        ResetCraftItem();
        for(int i = 0; i < nextCraftable.SOData._recipes.Count; i++)
        {
            _refCollectables[i] =nextCraftable.SOData._recipes[i].Collectable;
            _materialReq[i].sprite = _refCollectables[i]._collectableData.InventoryInformation.InventoryIcon;
            _materialReq[i].color = new Color(255, 255, 255, 1);
            _refAmount[i] = nextCraftable.SOData._recipes[i].Amount;
            _materialAmountText[i].text = _refAmount[i].ToString();
            _materialNameText[i].text = nextCraftable.SOData._recipes[i].Collectable._collectableData.InventoryInformation.InventoryName;
        }
    }
    public void RecieveItem(InventoryType slotType, Collectable fixedCollectable, out bool materialDecrease)
    {
        materialDecrease = false;
        if (_duringCraft)
        {
            return;
        }
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

    public bool CanCreate()
    {
        if (_refAmount[0] <= 0 && _refAmount[1] <= 0)
        {
            return true;
        }
        return false;
    }
    public virtual void Create()
    {
        if (CanCreate())
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
        SetCraftItem(ActiveCraftItem);

    }
    public void ChangeCraftSelection(int Amount)
    {
        if (_duringCraft)
            return;
        int nextIndex = AllCraftables.IndexOf(ActiveCraftItem) + Amount;
        if (nextIndex >= AllCraftables.Count)
        {
            nextIndex = 0;
        } 
        if(nextIndex < 0)
        {
            nextIndex = AllCraftables.Count - 1;            
        }

        Craftable nextItem = AllCraftables[nextIndex];
        _outputIcon.sprite = null;
        _outputIcon.color = new Color(255, 255, 255, 0);
      
        
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

        SetCraftItem(nextItem);
    }
    private void ResetCraftItem()
    {
        for (int i = 0; i < 2; i++)
        {
            _materialReq[i].color = new Color(255, 255, 255, 0);
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

