using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public static class InventoryUtill 
{
    public static void CalculateGridParameters(RectTransform panel, int row, int colomns)
    {
        GridLayoutGroup grid = panel.GetComponent<GridLayoutGroup>();
        Vector2 spacing = grid.spacing;
        float paddingX = grid.padding.left + grid.padding.right; 
        float paddingY = grid.padding.top + grid.padding.bottom;
        float cellSizeX = (panel.rect.width - paddingX - spacing.x * (colomns - 1)) / colomns;
        float cellSizeY = (panel.rect.height - paddingY - spacing.y * (row - 1)) / row;
        float cellSize = (cellSizeX < cellSizeY) ? cellSizeX : cellSizeY;
        grid.cellSize = Vector2.one * cellSize;
        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = colomns;
        grid.childAlignment = TextAnchor.UpperCenter;
    }
    public static void CalculateGridParametersBySize(RectTransform panel, out int colomns, out int rows)
    {
       //Debug.Log(panel.name+"  " + panel.rect.height+"   "+ panel.sizeDelta+"   "+panel.rect.size +"  "+panel.rect.xMin+"   "+panel.rect.xMax);
        rows = 1; 
        colomns = 1;
        GridLayoutGroup grid = panel.GetComponent<GridLayoutGroup>();
        
        float paddingX = grid.padding.left + grid.padding.right;
        float paddingY = grid.padding.top + grid.padding.bottom;
        float height = panel.rect.height ;
        float width = panel.rect.width ;


        //rows = (int)(panel.rect.height / grid.cellSize.x);
        //colomns =(int)(panel.rect.width / grid.cellSize.x);

        rows = (int)(height / (grid.cellSize.x + grid.spacing.x) );
        colomns = (int)(width / (grid.cellSize.x + grid.spacing.x));


        //Vector2 spacing = grid.spacing;
        //float cellSizeX = (panel.rect.width - paddingX - spacing.x * (colomns - 1) - paddingY - spacing.y) / colomns;
        //grid.cellSize = Vector2.one * cellSizeX;
        //grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        //grid.constraintCount = colomns;
        //grid.childAlignment = TextAnchor.UpperCenter;
        //rows = (int)(panel.rect.height - paddingY - spacing.y) / (int)cellSizeX;

    }




    public static void GenerateSlots(RectTransform panel, RectTransform rootPanel, int row, int colomns, InventorySlot invSlotPrefab, List<InventorySlot> slots, WhatCanContein whatContein)
    {
        for (int i = 0; i < colomns * row; i++)
        {
            InventorySlot itemHolder = GameObject.Instantiate(invSlotPrefab); 
            itemHolder.name += i.ToString();
            itemHolder.RootInventoryPanel = rootPanel;
            itemHolder.transform.SetParent(panel);
            itemHolder.transform.localScale = Vector3.one;
            itemHolder.whatCoutein = whatContein;
            slots.Add(itemHolder);
        }

    }
    public static void GenerateSlots(RectTransform panel, RectTransform rootPanel, int row, int colomns,
        InventorySlot invSlotPrefab, List<InventorySlot> slots, WhatCanContein whatContein, WhatCanContein whatConteinSecond)
    {
        for (int i = 0; i < colomns * row; i++)
        {
            InventorySlot itemHolder = GameObject.Instantiate(invSlotPrefab);
            itemHolder.name += i.ToString();
            itemHolder.RootInventoryPanel = rootPanel;
            itemHolder.transform.SetParent(panel);
            itemHolder.transform.localScale = Vector3.one;
            if(i< (colomns * row)/2)
            itemHolder.whatCoutein = whatContein;
            else
                itemHolder.whatCoutein = whatConteinSecond;
            slots.Add(itemHolder);
        }

    }

    public static InventoryItem GenerateItem(Item itemrelated, InventorySlot slot, RectTransform rootPanel, InventoryItem prefab, RectTransform Panel, RectTransform ButtonPanel)
    {

        InventoryItem item = GameObject.Instantiate(prefab);
        item.RelatedItem = itemrelated;

        if (slot != null)
        {
            item.transform.SetParent(slot.transform);
            SetInventoryItemSizeAndPos(item);
        }
        else
        {
            item.transform.SetParent(rootPanel.transform);
        }
        item.Count = 1;
        item.transform.localPosition = Vector3.zero;
        item.GetComponent<Image>().sprite = itemrelated.Sprite;
        
        item.InfoPanel = Panel;
        item.ButtonPanel = ButtonPanel;
        return item;
    }
    public static InventoryItem GenerateItem(Item itemrelated, InventorySlot slot, RectTransform rootPanel, 
        InventoryItem prefab, RectTransform Panel, RectTransform ButtonPanel,int count)
    {

        InventoryItem item = GameObject.Instantiate(prefab);
        item.RelatedItem = itemrelated;

        if (slot != null)
        {
            item.transform.SetParent(slot.transform);
            SetInventoryItemSizeAndPos(item);
        }
        else
        {
            item.transform.SetParent(rootPanel.transform);
        }
        item.Count = 1;
        item.transform.localPosition = Vector3.zero;
        item.GetComponent<Image>().sprite = itemrelated.Sprite;
        item.Count = count;
        item.InfoPanel = Panel;
        item.ButtonPanel = ButtonPanel;
        return item;
    }
    public static void SetInventoryItemSizeAndPos(InventoryItem inventoryItem)
    {
        RectTransform rt = inventoryItem.GetComponent<RectTransform>();
        inventoryItem.transform.localScale = Vector3.one; 
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = Vector2.zero; 
        rt.offsetMax = Vector2.zero;
    }

}
