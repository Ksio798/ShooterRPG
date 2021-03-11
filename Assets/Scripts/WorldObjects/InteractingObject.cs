using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class InteractingObject : MonoBehaviour, IInteractable
{
    public Item ConteinItem;
    public GameObject Canvas;
    void Start()
    {
        ObjTrigger trigger = GetComponentInChildren<ObjTrigger>();
        trigger.OnEnterZone += OnEnter;
        trigger.OnExitZone += OnExit;
    }
    public bool InteractingByKeyPressing { get { return true; } }
    public void Interact(GameObject Player)
    {
        InventoryController inventory = Player.GetComponent<PlayerConteiner>().Inventory;
        inventory.AddItem(ConteinItem);
        Destroy(gameObject);
    }
    public bool IsNeedInventory { get { return false; } }
    public virtual void OnEnter()
    {
        TextMeshProUGUI text = Canvas.GetComponentInChildren<TextMeshProUGUI>();
        if (text != null)
        {
            text.text = $"{ConteinItem.Name}\nPress E to interact";
        }
        Canvas.SetActive(true);
    }
   