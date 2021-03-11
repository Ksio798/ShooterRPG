using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : WorldObjectsInventory, IInteractable
{
    public bool InteractingByKeyPressing { get { return true; } }
    public bool IsNeedInventory { get { return true; } }
    int CountInRow = 2;
    int Rows = 2;
    public override event Action OnClosePanel;
    public void Interact(GameObject Player)
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
