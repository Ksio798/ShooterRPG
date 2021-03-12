using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIController : MonoBehaviour
{
    public GameObject InventoryPanel;
    public GameObject QuickSlotsP;
    public GameObject ChestPanel;
   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            InventoryPanel.SetActive(!InventoryPanel.activeSelf);
            QuickSlotsP.SetActive(true);
            ChestPanel.SetActive(false);

        }
    }
}
