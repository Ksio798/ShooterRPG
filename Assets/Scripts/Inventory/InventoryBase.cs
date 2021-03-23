using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ItemWithCount
{
    public Item itemInfo;
    public int Count;
   public ItemWithCount(Item item, int count)
    {
        itemInfo = item;
        Count = count;
    }

}
public class InventoryBase : MonoBehaviour
{
    public RectTransform RootInventoryPanel; // ������ �� �������� ������     
    public InventoryItem ItemPrefab;  // ������ ��������     
    public InventorySlot InventoryItemHolderPref; //������ ����� 
    public RectTransform InventoryPanel;   // ������ �� ����������� ������������� ��������� � ����    
    public List<Item> items = new List<Item>();   // ������ ��������� � ���������  

    protected List<InventoryItem> InventoryItems = new List<InventoryItem>(); // ������ ���������     
    protected List<InventorySlot> InventorySlots = new List<InventorySlot>(); // ������ ������ 
    public RectTransform InfoPanel;
    public RectTransform ButtonPanel;
    public UsingItems Owner;


    // �������� �� ������� �� ��������� 
    public virtual void SubscribeToItemEvents()
    {
        foreach (var Item in InventoryItems)
        {
            Item.OnItemUsed += ItemUse;
            Item.OnItemDestroyed += ItemDestroy;

        }
    }

    // �������� �� ������� �� ����� 
    public virtual void SubscribeToSlotsEvents()
    {
        foreach (var slot in InventorySlots)
        {
            slot.OnItemDroped += OnItemDropped;
            slot.OnItemRemoved += OnItemRemoved;
            slot.ParentInventory = this;
        }
    }

    //��������� �������, ����� ������� �������� � ���� � ������� �� ����� 
    protected virtual void OnItemDropped(InventorySlot s, InventoryItem item)
    {
        if (!InventoryItems.Contains(item))
        {
            InventoryItems.Add(item);
            items.Add(item.RelatedItem);

        }
    }
    protected virtual void OnItemRemoved(InventorySlot s, InventoryItem item)
    {

        InventoryItems.Remove(item); items.Remove(item.RelatedItem);
    }

    //�������� ������ �� ������� ���� 
    public InventorySlot GetFirstEmptySlot()
    {
        InventorySlot slot = null;
        for (int i = 0; i < InventorySlots.Count; i++)
        {
            if (InventorySlots[i].InventoryItem == null)
            {
                slot = InventorySlots[i]; break;

            }
        }
        return slot;
    }
    // ������ �� ���������� Item/InventoryItem � ���������
    public virtual void AddItem(InventoryItem item)
    {
        for (int i = 0; i < InventorySlots.Count; i++)
        {
            if (InventorySlots[i].InventoryItem == null)
            {
                item.SetNewHolder(InventorySlots[i].GetComponent<RectTransform>());

                break;
            }
        }
    }

    public virtual void AddItems(List<InventoryItem> items)
    {
        for (int i = 0, j = 0; i < InventorySlots.Count; i++)
        {
            if (InventorySlots[i].InventoryItem == null)
            {
                items[j].SetNewHolder(InventorySlots[i].GetComponent<RectTransform>());

                j++;
                if (j == items.Count) break;
            }
        }
    }
    public virtual void AddItem(Item item)
    {
        for (int i = 0; i < InventorySlots.Count; i++)
        {
            if (InventorySlots[i].InventoryItem == null)
            {
                InventoryItem itemInv = InventoryUtill.GenerateItem(item, InventorySlots[i], RootInventoryPanel, ItemPrefab, InfoPanel, ButtonPanel);
              
                break;
            }
        }
    }
    public virtual void AddItems(List<Item> items)
    {
    }
    public virtual bool ContainsItem(Item item)
    {
        return false;
    }
    public void ItemUse(InventoryItem item)
    {
        Owner.Use(LoadPrefab(item.RelatedItem).GetComponent<IUsedle>());
        //Debug.Log("ItenUsed");
    }
    public void ItemDestroy(InventoryItem item)
    {
       // int indexItem = items.IndexOf(item.RelatedItem);
        items.Remove(item.RelatedItem);
//int indexInvI = InventoryItems.IndexOf(item);
        InventoryItems.Remove(item);
       
    }

    public GameObject LoadPrefab(Item LoadedItem)
    {


       // GameObject prefab = Resources.Load(LoadedItem.FileName + ".prefab") as GameObject;
        GameObject prefab = Resources.Load(LoadedItem.FileName) as GameObject;
        GameObject copy = Object.Instantiate(prefab);
        return copy;
    }
    public void AddAllItems()
    {
        List<Item> ItemsToSave = new List<Item>();
        for (int i = 0; i < InventorySlots.Count; i++)
        {
            if (InventorySlots[i].InventoryItem != null && InventorySlots[i].InventoryItem.RelatedItem.IsStackable)
            {
                for (int j = 0; j < InventorySlots[i].InventoryItem.Count; j++)
                {
                    ItemsToSave.Add(InventorySlots[i].InventoryItem.RelatedItem);

                }
            }
            else if(InventorySlots[i].InventoryItem != null)
            {
                ItemsToSave.Add(InventorySlots[i].InventoryItem.RelatedItem);
            }
        }
        items = ItemsToSave;
    }
    public void GenerateItemsWithSort()
    {
        List<ItemWithCount> ControlledItems = new List<ItemWithCount>();
        int index=0;
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].IsStackable)
            {
                bool IsFind = false;
                for (int j = 0; j < ControlledItems.Count; j++)
                {

                    if (ControlledItems[j].itemInfo == items[i] && ControlledItems[j].Count < items[i].MaxCount)
                    {

                        ControlledItems[j].Count += 1;
                        IsFind = true;
                        break;
                    }
                }
                if (!IsFind)
                {

                    ControlledItems.Add(new ItemWithCount(items[i], 1));
                }
            }
            else
            {
                InventoryItems.Add(InventoryUtill.GenerateItem(items[i], InventorySlots[index], RootInventoryPanel, ItemPrefab, InfoPanel, ButtonPanel));
                index++;
            }
        }
        for (int i = index; i < ControlledItems.Count; i++)
        {
           //�������� ������
            InventoryItems.Add(InventoryUtill.GenerateItem(ControlledItems[i].itemInfo, InventorySlots[i], RootInventoryPanel,
                ItemPrefab, InfoPanel, ButtonPanel, ControlledItems[i].Count));
        }
    }


}
