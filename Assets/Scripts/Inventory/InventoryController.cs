using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : InventoryBase
{

    public int CountInRow = 5; 
    public int Rows = 30;
    public GameObject InventoryObject;
    public RectTransform InteractionPanel;
    public RectTransform QSlotsPanel;
    void Start()
    {
        // InventoryUtill.CalculateGridParameters(InventoryPanel, Rows, CountInRow);
        InventoryUtill.CalculateGridParametersBySize(InventoryPanel,out CountInRow, out Rows);
        InventoryUtill.GenerateSlots(InventoryPanel, RootInventoryPanel, Rows, CountInRow, InventoryItemHolderPref, InventorySlots, WhatCanContein.All);
        for (int i = 0; i < items.Count; i++)
        {
              InventoryItems.Add(InventoryUtill.GenerateItem(items[i], InventorySlots[i], RootInventoryPanel, ItemPrefab, InfoPanel , ButtonPanel));
        }
        SubscribeToItemEvents();
        SubscribeToSlotsEvents();
        Owner.GetComponent<Interaction>().OnInteracting += InteractWithInvPanel;
    }
   void InteractWithInvPanel(GameObject interactable)
    {
        WorldObjectsInventory inventory = interactable.GetComponentInParent<WorldObjectsInventory>();
        if(inventory != null)
        {
            inventory.RootInventoryPanel = RootInventoryPanel;
            inventory.InventoryPanel = InteractionPanel;
            inventory.InfoPanel = InfoPanel;
            inventory.ButtonPanel = ButtonPanel;
            inventory.Owner = Owner;
            inventory.OnClosePanel += OnClosePanel;
        GetComponent<PlayerUIController>().InventoryPanel.SetActive(true);
       // InteractionPanel.gameObject.SetActive(true);
        QSlotsPanel.gameObject.SetActive(false);
        }



        
    }
    void OnClosePanel()
    {
        QSlotsPanel.gameObject.SetActive(true);
        GetComponent<PlayerUIController>().InventoryPanel.SetActive(false);
    }

}
