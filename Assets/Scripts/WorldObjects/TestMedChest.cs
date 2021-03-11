using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMedChest : MonoBehaviour, IUsedle
{
    public void Use(PlayerConteiner playerController)
    {
        playerController.Healing(10);
        Destroy(gameObject);
    }
}
