using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WorldObject : InteractingObject
{
    public Item ConteinItem;
    public override void Interact(GameObject Player)
    {
        InventoryController inventory = Player.GetComponent<PlayerConteiner>().Inventory;
        inventory.AddItem(ConteinItem);
        Destroy(gameObject);
    }
    protected override void SetText(TextMeshProUGUI text)
    {
        text.text = $"{ConteinItem.Name}\nPress E to interact";
    }
}
