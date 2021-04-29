using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionQuestInit : MonoBehaviour, IQuestInitiator
{
    public event System.Action OnQuestStart;
    void Start()
    {
        InteractingObject interacting = GetComponent<InteractingObject>();
        interacting.OnInteract += OnObjInteract;

    }
    void OnObjInteract()
    {
        OnQuestStart?.Invoke();
        InteractingObject interacting = GetComponent<InteractingObject>();
        interacting.OnInteract -= OnObjInteract;
        Destroy(this);
    }
   
}
