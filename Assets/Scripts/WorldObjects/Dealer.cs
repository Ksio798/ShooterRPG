using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dealer : InteractingObject
{
    public GameObject TradeWindow;
    public GameObject DealListPanel;
    public List<Deal> Deals = new List<Deal>();

    public override void Interact(GameObject Player)
    {
        base.Interact(Player);
        if (!TradeWindow.activeSelf)
        {
            for (int i = 0; i < Deals.Count; i++)
            {

            }



            TradeWindow.SetActive(true);
            StaticVariables.InMeny = false;
            Cursor.visible = true;
        }



    }
}
[System.Serializable]
public class Deal
{
    public List<Item> ItemsToSell = new List<Item>();
    public List<Item> ItemsToBuy = new List<Item>();

}

