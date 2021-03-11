using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float MaxHealth;
    public float CurrentHealth;
    public void Healing(float HP)
    {
        if (CurrentHealth != MaxHealth)
        {
            if (CurrentHealth + HP <= MaxHealth)
            {
                CurrentHealth += HP;
            }
            else
            {
                CurrentHealth = MaxHealth;
            }
            Debug.Log(CurrentHealth);
        }
    }
}
