using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIController : MonoBehaviour
{
    public GameObject InventoryPanel;

   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            InventoryPanel.SetActive(!InventoryPanel.activeSelf);
        }
    }
}
