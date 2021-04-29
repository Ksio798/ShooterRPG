using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerQuestInit : MonoBehaviour, IQuestInitiator
{
    public event System.Action OnQuestStart;
    bool started = false;
    public void OnTriggerEnter(Collider collision)
    {
        if (!started)
        {
        OnQuestStart?.Invoke();
            started = true;
        }

    }
}
