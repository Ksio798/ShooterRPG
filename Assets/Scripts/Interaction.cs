using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
   public GameObject Point;
    GameObject interactingObject; // ссылка на объект, который находится радом, чтобы можно было с ним взаимодействовать
    List<GameObject> interactingObjects = new List<GameObject>();
    GameObject inSight;
    Collider currentCollider; // тот коллайдер, с которым игрок взаимодействует, чтобы код не выполнился 2 раза подряд для одного и того же предмета
    public event System.Action<GameObject> OnInteracting;
    public LayerMask ObMask;
    private void Update()
    {
        //Physics.Raycast(transform.position, Vector3.forward, out hit);
        RaycastHit hit;
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward);

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward,out hit, 1000f, ObMask))
        {
            Transform objectInt = hit.transform;
            if(interactingObjects.Contains(objectInt.gameObject))
            {
                inSight = objectInt.gameObject;
                Debug.Log(inSight.name);
            }
            if (inSight!= null && objectInt.gameObject != inSight)
            {
                inSight = null;
            }
        }
       


        Interact();
    }
    void Interact()
    {
        //if (Input.GetKeyDown(KeyCode.E) && interactingObject != null)
        //{
        //   Debug.Log("Interacting");
        //    interaction(interactingObject.GetComponentInParent<IInteractable>());
        //}
        if (Input.GetKeyDown(KeyCode.E) && inSight != null)
        {
            Debug.Log("Interacting");
           int index = interactingObjects.IndexOf(inSight);
            IInteractable interactable = interactingObjects[index].GetComponent<IInteractable>();
            interaction(interactable);
        }
    }
    void interaction(IInteractable interactable)
    {
        //if(interactable.IsNeedInventory && interactingObject != null)
        //{
        //OnInteracting?.Invoke(interactingObject);
        //}
        //interactable.Interact(gameObject);
        if (interactable.IsNeedInventory && inSight != null)
        {
            Debug.Log("OnInter");
            OnInteracting?.Invoke(inSight);
        }
        interactable.Interact(gameObject);

    }
    
    private void OnTriggerEnter(Collider collision)
    {
        if (currentCollider != collision || currentCollider == null) // проверка, не столкнулся ли игрок уже с этим предметом
        {
            currentCollider = collision;
            IInteractable interactable = collision.transform.GetComponentInParent<IInteractable>();
            InteractingObject interactableGameObject = collision.transform.GetComponentInParent<InteractingObject>();
            if (interactable != null)
            {
            
                if (!interactable.InteractingByKeyPressing)// если с объектом взаимодействуют без нажатия клавиши, то вызывать действие interaction ( см. Ниже)
                {
                    interaction(interactable);
                }
                else// иначе запомнить объект столкновения
                {
                   
                    Debug.Log(collision.gameObject.name);
                    interactingObjects.Add(interactableGameObject.gameObject);
                    //interactingObject = collision.gameObject;

                }
            }


        }
    }
    
    private void OnTriggerExit(Collider collision)
    {

        //if (collision.gameObject == interactingObject)
        //{
        //    Debug.Log("Exit");
        //    if (interactingObjects.Contains(collision.gameObject))
        //    {
        //        interactingObjects.Remove(collision.gameObject);
        //    }
        //   // interactingObject = null;
        //}
        InteractingObject interactingObject = collision.transform.GetComponentInParent<InteractingObject>();
        if (interactingObjects.Contains(interactingObject.gameObject))
        {
            interactingObjects.Remove(interactingObject.gameObject);
        }
        if (interactingObject.gameObject == inSight)
        {
            inSight = null;
        }
        if (collision == currentCollider)
        {
            currentCollider = null;
        }
    }

}
