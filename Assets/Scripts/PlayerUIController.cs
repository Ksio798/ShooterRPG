using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIController : MonoBehaviour
{
    public GameObject InventoryPanel;
   
   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && StaticVariables.InMeny)
        {
            InventoryPanel.SetActive(!InventoryPanel.activeSelf);
            
            Cursor.visible = !Cursor.visible;
        }
    }
     void Start()
    {
        Cursor.visible = false;
    }
}
