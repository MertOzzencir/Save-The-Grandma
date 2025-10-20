using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class IncreaseIndicator : MonoBehaviour
{
    [SerializeField] private Color _positiveIncreaseColor;
    [SerializeField] private Color _negatifeIncreaseColor;
    private ItemSlot _owner;
    Coroutine indicator;
    void Awake()
    {
        _owner = GetComponentInParent<ItemSlot>();
        _owner.OnAmountChange += RespondToUI;
    }
    void OnEnable()
    {
        _owner.OnAmountChange += RespondToUI;
    }
    void OnDisable()
    {
        _owner.OnAmountChange -= RespondToUI;       
    }
    private void RespondToUI(bool obj, Image backGround)
    {   if (indicator != null)
            return;
        indicator = StartCoroutine(StartIndicator(obj, backGround));
    }
    
    IEnumerator StartIndicator(bool obj, Image backGround)
    {
        Image originalBG = backGround;
        Sprite originalSprite = backGround.sprite;
        backGround.sprite = null;
        if (obj)
        {
            backGround.color = _positiveIncreaseColor;
        }
        else
        {
            backGround.color = _negatifeIncreaseColor;
        }
        yield return new WaitForSeconds(0.17f);
        backGround = originalBG;
        backGround.sprite = originalSprite;
        backGround.color = new Color(255, 255, 255, 1);
        indicator = null;
    }
}
