using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : InteractingObject
{
    
   
    public override void Interact(GameObject Player)
    {
        GetComponent<ChestInventory>().OnInventoryInteract();

    }


}
