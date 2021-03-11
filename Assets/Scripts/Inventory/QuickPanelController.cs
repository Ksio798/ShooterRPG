using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickPanelController : InventoryBase
{
    public int CountInRow = 4;
    public int Rows = 4;
    public int CountInRowForWeapon = 2;
    public int RowsForWeapon = 2;
  
    public GameObject InventoryObject;
    public RectTransform WeaponPanel;

    void Start()
    {

        InventoryUtill.GenerateSlots(InventoryPanel, RootInventoryPanel, Rows, CountInRow, InventoryItemHolderPref, InventorySlots, WhatCanContein.Using);
         InventoryUtill.GenerateSlots(WeaponPanel, RootInventoryPanel, RowsForWeapon, CountInRowForWeapon, InventoryItemHolderPref, InventorySlots, WhatCanContein.Weapon, WhatCanContein.Equipment);
        for (int i = 0; i < items.Count; i++)
        {
            InventoryItems.Add(InventoryUtill.GenerateItem(items[i], InventorySlots[i], RootInventoryPanel, ItemPrefab, InfoPanel, ButtonPanel));
            // InventoryItems.Add(InventoryUtill.GenerateItem(items[i], InventorySlots[i], RootInventoryPanel, ItemPrefab, InfoPanel, ButtonPanel));
        }
        SubscribeToItemEvents();
        SubscribeToSlotsEvents();
        Owner.OnQuickItemUsed += UseQPanelItem;
    }

    void UseQPanelItem(int index)
    {

        if (InventorySlots[index].InventoryItem != null)
            InventorySlots[index].InventoryItem.Use();
    }
    protected override void OnItemDropped(InventorySlot s, InventoryItem item)
    {
        base.OnItemDropped(s, item);
    }

}
