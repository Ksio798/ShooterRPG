using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class InteractingObject : MonoBehaviour, IInteractable
{
   
    public GameObject Canvas;
    public bool NeedKey;
    public bool NeedInv;
    void Start()
    {
        ObjTrigger trigger = GetComponentInChildren<ObjTrigger>();
        trigger.OnEnterZone += OnEnter;
        trigger.OnExitZone += OnExit;
    }
    public bool InteractingByKeyPressing { get { return NeedKey; } }
    public virtual void Interact(GameObject Player)
    {
       
    }
    public bool IsNeedInventory { get { return NeedInv; } }
    public virtual void OnEnter()
    {
        TextMeshProUGUI text = Canvas.GetComponentInChildren<TextMeshProUGUI>();
        if (text != null)
        {

            SetText(text);
        }
        Canvas.SetActive(true);
    }
    public virtual void OnExit()
    {
        Canvas.SetActive(false);
    }
    protected virtual void SetText(TextMeshProUGUI text)
    {
        text.text = "Press E to interact";
    } 
    public void OnRay()
    {
        OnEnter();
    }
    public void OutRay()
    {
        Canvas.SetActive(false);
    }
}
