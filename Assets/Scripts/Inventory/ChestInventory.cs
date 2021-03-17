using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInventory : WorldObjectsInventory
{
    int CountInRow = 2;
    int Rows = 2;
    public override event System.Action OnClosePanel;
    public void OnInventoryInteract()
    {
        if (InventoryPanel.gameObject.activeSelf == false)
        {
            InventoryPanel.gameObject.SetActive(true);
            InventoryUtill.CalculateGridParametersBySize(InventoryPanel, out CountInRow, out Rows);
            InventoryUtill.GenerateSlots(InventoryPanel, RootInventoryPanel, Rows, CountInRow, InventoryItemHolderPref, InventorySlots, WhatCanContein.All);
            GenerateItemsWithSort();
            SubscribeToItemEvents();
            SubscribeToSlotsEvents();

        }
        else
        {
            AddAllItems();

            foreach (var item in InventorySlots)
            {
                Destroy(item.gameObject);
            }
            InventorySlots.Clear();
            OnClosePanel?.Invoke();
            InventoryPanel.gameObject.SetActive(false);



        }
    }
}
