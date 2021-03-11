using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public enum WhatCanContein
{
    All,
    Weapon,
    Equipment,
    Using

}
public class InventorySlot : MonoBehaviour, IDropHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    //GameObject Owner;
    // ������� ��������� �� ������. ���� ����� �������� ���� � ����������, � ������� �� ��������� � ���, ��� � ���� �������� ������� � ������� ������� 
    public event System.Action<InventorySlot, InventoryItem> OnItemDroped;
    public event System.Action<InventorySlot, InventoryItem> OnItemRemoved;
    //���� � ������������ ����������, � ������� ��������� ����    
    public InventoryBase ParentInventory;
    // ������ �� �������, ������� � ������ ������ ��������� � �����    
    public InventoryItem InventoryItem
    {
        get { return GetComponentInChildren<InventoryItem>(); }

    }
    //������ �� �������� ������    
    [HideInInspector]
    public RectTransform RootInventoryPanel;
    // ������ ��� ��������� ����������� � ���� ������ ���������: ������������� � ����� ����� � ���    void SetStackableItem(InventoryItem item){}    void SetUnStackableItem(InventoryItem item){} 
    public WhatCanContein whatCoutein;
    void SetStackableItem(InventoryItem item)
    {
       

            InventorySlot inventoryHolder = item.ParentHolder;
            if (inventoryHolder != null)
                inventoryHolder.OnItemRemoved?.Invoke(inventoryHolder, item);
            if (InventoryItem == null || item.RelatedItem.Name != InventoryItem.RelatedItem.Name|| item.Count + InventoryItem.Count > InventoryItem.RelatedItem.MaxCount)
            {
                SetUnStackableItem(item);
            }
            else
            {
                InventoryItem.SetCount(item.Count);
                Destroy(item.gameObject);
                OnItemDroped?.Invoke(this, InventoryItem);
            }
       

    }

    // ���������� ������ SetUnStackableItem - ������, ����� ������� ������ ���������. ������ ����� ����������, ����� ������� ��������(drop) � ����. ���� ���� �����, �� �������� �������� ������� 

    void SetUnStackableItem(InventoryItem item)
    {
        bool success = false;
        InventorySlot inventoryHolder = item.ParentHolder; if (InventoryItem != null)
        {
            if (inventoryHolder != null)
            {
                InventoryItem.SetNewHolder(inventoryHolder.GetComponent<RectTransform>()); success = true;
            }
            else
            {
                InventorySlot slot = ParentInventory.GetFirstEmptySlot(); if (slot != null)
                {
                    InventoryItem.SetNewHolder(slot.GetComponent<RectTransform>());

                    success = true;
                }

            }
        }
        else
        {
            success = true;
            if (inventoryHolder != null)
                inventoryHolder.OnItemRemoved?.Invoke(inventoryHolder, item);
        }

        if (success)
        {
            item.SetNewHolder(GetComponent<RectTransform>());
            OnItemDroped?.Invoke(this, item);
        }
    }


    // ������ �� ����������� IDropHandler, IPointerClickHandler   
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            return;
        }
        InventoryItem item = eventData.pointerDrag.GetComponent<InventoryItem>();

        if (item != null)
        {
            if (whatCoutein == WhatCanContein.Using && !item.RelatedItem.IsUseble)
                return;
            else if (whatCoutein == WhatCanContein.Weapon && !item.RelatedItem.IsWeapon)
                return;
            else if (whatCoutein == WhatCanContein.Equipment && !item.RelatedItem.IsEquipment)
                return;

            if (item.RelatedItem.IsStackable)
            {

                SetStackableItem(item);
            }
            else
            {
                SetUnStackableItem(item);
            }
        }

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (InventoryItem.newItem != null)
        {
            if (InventoryItem.newItem.RelatedItem.IsStackable)
            {

                SetStackableItem(InventoryItem.newItem);
            }
            else
            {
                SetUnStackableItem(InventoryItem.newItem);
            }
            InventoryItem.newItem.FollowMouse();
            InventoryItem.newItem = null;
            InventoryItem.copyingItem = null;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().color = new Color(1, 1, 1, 0);
    }
}
