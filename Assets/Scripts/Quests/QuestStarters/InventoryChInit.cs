using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryChInit : MonoBehaviour, IQuestInitiator
{
    public event System.Action OnQuestStart;
    void Start()
    {
        InventoryBase inventory = GetComponent<InventoryBase>();
        inventory.OnInventoryChange += OnChange;
    }
    void OnChange(Item item, bool IsAdd)
    {
        OnQuestStart?.Invoke();
    }

}
