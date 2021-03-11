using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{


    [HideInInspector]
    public Item RelatedItem; // связанная с этим изображением информация о предмете 
    [HideInInspector]
    public RectTransform rectTransform; //ссылка на объект RectTransform для указания размеров, масштаба и позиции   
    [HideInInspector]
    public InventorySlot ParentHolder; // слот, в котором сейчас находится предмет 

    public int Count = 1;  // количество предметов в стеке 
    // Статические поля для хранения ссылки на текущий разделяемый предмет (копируемый и новый предмет) 
    public static InventoryItem newItem = null;
    public static InventoryItem copyingItem = null;
    bool followMouse = false;
    public RectTransform InfoPanel;
    public RectTransform ButtonPanel;
    Coroutine coroutine;
    public event System.Action<InventoryItem> OnItemUsed;
    public event System.Action<InventoryItem> OnItemDestroyed;
    int pivotX = 0;
    int pivotY = 0;
    void Start()
    {
        if (RelatedItem.IsStackable)
            SetCount(Count, false);
        else
        {
            Count = 1;
            transform.GetChild(0).gameObject.SetActive(false);
        }
        rectTransform = GetComponent<RectTransform>();
        ParentHolder = transform.parent.GetComponent<InventorySlot>();
       // SetCount(Count, false);
    }
    private void Update()
    {
        if (followMouse)
        {
            transform.position = Input.mousePosition;
        }
    }


    void SetInfo()
    {

        InfoPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = RelatedItem.Name;
        InfoPanel.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = RelatedItem.Description;
    }
    void SetButtonInteraction()
    {
        InteractionPanel interactionPanel = ButtonPanel.GetComponent<InteractionPanel>();
        interactionPanel.UseButton.onClick.RemoveAllListeners();
        interactionPanel.DesstroyButton.onClick.RemoveAllListeners();
        if (RelatedItem.IsUseble)
        {
            interactionPanel.UseButton.gameObject.SetActive(true);
            interactionPanel.UseButton.onClick.AddListener(() => { Use(); });
        }
        else
        {
            interactionPanel.UseButton.gameObject.SetActive(false);
        }
        interactionPanel.DesstroyButton.onClick.AddListener(() => { DestroyItem(); });
    }


    public void FollowMouse()
    {
        followMouse = !followMouse;
        newItem.GetComponent<CanvasGroup>().blocksRaycasts = !followMouse;
    }

    public void SetNewHolder(RectTransform newHolder)
    {
        transform.SetParent(newHolder);
        InventoryUtill.SetInventoryItemSizeAndPos(this);
        ParentHolder = newHolder.GetComponent<InventorySlot>();
    }
    public void SetCount(int count, bool add = true)
    {
        TMPro.TextMeshProUGUI countTxt = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        if (!add)
            Count = count;
        else
            Count += count;
        countTxt.text = Count.ToString();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            return;
        }
        transform.SetParent(ParentHolder.RootInventoryPanel.transform);
        transform.SetAsLastSibling();
        GetComponent<CanvasGroup>().blocksRaycasts = false;


    }
    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            return;
        }


        transform.position = eventData.position;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            return;
        }
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        if (transform.parent.GetComponent<InventorySlot>() == null)
        {
            SetNewHolder(ParentHolder.GetComponent<RectTransform>());
        }

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (newItem != null)
        {
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
        SetInfo();
        InfoPanel.transform.position = eventData.position;

        if (eventData.position.y - InfoPanel.sizeDelta.y < 0)
        {
            pivotY = 0;

        }
        else
        {
            pivotY = 1;
        }
        if (eventData.position.x - InfoPanel.sizeDelta.x < 0)
        {
            pivotX = 0;
        }
        else
        {
            pivotX = 1;
        }
       
        InfoPanel.pivot = new Vector2(pivotX, pivotY);
        coroutine = StartCoroutine(WaitToPanel());
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (newItem != null)
        {
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        StopCoroutine(coroutine);
        InfoPanel.gameObject.SetActive(false);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (newItem == null)
            {
                if (RelatedItem.IsStackable && Count > 1)
                {
                    copyingItem = this;
                    newItem = Instantiate(this, ParentHolder.transform);
                    newItem.Count = 1;
                   
                    SetCount(-1);
                    InventoryUtill.SetInventoryItemSizeAndPos(this);
                    newItem.transform.SetParent(ParentHolder.RootInventoryPanel);
                    newItem.transform.position = eventData.position;
                    newItem.FollowMouse();
                }
            }
            else
            {
                if (this == copyingItem)
                {
                   
                    this.SetCount(-1);
              
                    newItem.SetCount(1);
                    if (Count == 0)
                    {
                        newItem.ParentHolder = this.ParentHolder; Destroy(gameObject);
                    }
                }
            }
        }
        else
        {

            SetButtonInteraction();
            ButtonPanel.transform.position = eventData.position;
            ButtonPanel.gameObject.SetActive(true);
            if (eventData.position.y - ButtonPanel.sizeDelta.y < 0)
            {
                pivotY = 0;

            }
            else
            {
                pivotY = 1;
            }
            if (eventData.position.x - ButtonPanel.sizeDelta.x < 0)
            {
                pivotX = 0;
            }
            else
            {
                pivotX = 1;
            }
            ButtonPanel.pivot = new Vector2(pivotX, pivotY);




        }
    }
    public void Use()
    {
        if (RelatedItem.IsUseble)
        {
            // Item , достать из него реальный предмет, 
            //сам Itm удалить из инвентаря
            OnItemUsed?.Invoke(this);
            if (RelatedItem.IsStackable && Count > 1)
            {
                SetCount(-1);
            }
            else
            {
                OnItemDestroyed?.Invoke(this);
                // Debug.Log("ItemDestr");
                Destroy(gameObject);
            }
            //OmItemChangedAmount?.Invoke(this);
            ButtonPanel.gameObject.SetActive(false);
        }
    }
    public void DestroyItem()
    {
        OnItemDestroyed?.Invoke(this);
        ButtonPanel.gameObject.SetActive(false);
        Destroy(gameObject);
    }



    IEnumerator WaitToPanel()
    {
        yield return new WaitForSeconds(1);
        InfoPanel.gameObject.SetActive(true);
    }
}

