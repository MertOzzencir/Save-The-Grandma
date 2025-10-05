using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask _benchLayermask;

    void Start()
    {
        InputManager.OnLeftMouse += OpenBench;
        InputManager.OnLeftMouse += PickItem;
    }

    private void OpenBench()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _benchLayermask))
        {
            Bench bench = hit.transform.GetComponent<Bench>();
            if (bench == null)
                return;
            bench.OpenCraftMenu();
        }
    }

    private void PickItem()
    {
        TryToPickItemSlot(out ItemSlot slot);
        if (slot == null)
            return;

        if (BenchController.Instance.ActiveBench != null)
        {
            Bench activeBench = BenchController.Instance.ActiveBench;
            Craftable activeCraftItem = activeBench.ActiveCraftItem;
            foreach (var a in activeCraftItem.SOData._recipes)
            {
                if (a.Collectable._collectableData.InventoryInformation.InventoryType == slot.SlotType)
                {
                    Debug.Log("It matched with Reqs");
                    
                }
            }

        }
    }

    private void TryToPickItemSlot(out ItemSlot slot)
    {
        slot = null;
        var ped = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
        var hits = new List<RaycastResult>();
        EventSystem.current.RaycastAll(ped, hits);

        foreach (var h in hits)
        {
            var s = h.gameObject.GetComponentInParent<ItemSlot>();
            if (s != null)
            {
                slot = s;
                return;
            }
        }
    }
}
