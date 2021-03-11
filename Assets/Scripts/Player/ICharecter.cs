using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharecter 
{
    event System.Action<GameObject> OnDied;
    float Health { get; set; }
    void TakeDamage();
    void Die();
}
