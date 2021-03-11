using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;
public class InteractionPanel : MonoBehaviour
{
    public Button UseButton;
    public Button DesstroyButton;

    void Update()
    {
      //  EventSystem.current.currentSelectedGameObject()
        if (Input.GetMouseButtonDown(0))
        {
            PointerEventData ped = new PointerEventData(EventSystem.current);
            ped.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(ped, results);
          //  Debug.Log(results.Count);
            if(!results.Any(x=>x.gameObject==gameObject))
                gameObject.SetActive(false);

            //Debug.Log(EventSystem.current.IsPointerOverGameObject()+"   "+ EventSystem.current.currentSelectedGameObject);
            //if (EventSystem.current.currentSelectedGameObject != this)
            //{
            //    gameObject.SetActive(false);
            //}
        }
    }
}
