using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class UsingItems : MonoBehaviour
{
  
    public PlayerConteiner playerController;
    public event System.Action<int> OnQuickItemUsed;
    //Доделвть использование предметов из инвенторяS
    public void Use(IUsedle Item)
    {
        Item.Use(playerController);

    }
    void Update()
    {

       
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {

            OnQuickItemUsed?.Invoke(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            OnQuickItemUsed?.Invoke(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            OnQuickItemUsed?.Invoke(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            OnQuickItemUsed?.Invoke(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            OnQuickItemUsed?.Invoke(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            OnQuickItemUsed?.Invoke(5);
        }
    }


}
