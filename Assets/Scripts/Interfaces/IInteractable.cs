using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public interface IInteractable
{
    bool InteractingByKeyPressing { get; }
    void Interact(GameObject Player);
    bool IsNeedInventory { get; }
    //RectTransform InvPanel { set; get; }
   


}
