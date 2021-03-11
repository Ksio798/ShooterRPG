using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    GameObject interactingObject; // ссылка на объект, который находится радом, чтобы можно было с ним взаимодействовать

    Collider currentCollider; // тот коллайдер, с которым игрок взаимодействует, чтобы код не выполнился 2 раза подряд для одного и того же предмета
    public event System.Action<GameObject> OnInteracting;
    private void Update()
    {
        Interact();
    }
    void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E) && interactingObject != null)
        {
           Debug.Log("Interacting");
            interaction(interactingObject.GetComponentInParent<IInteractable>());
        }
    }
    void interaction(IInteractable interactable)
    {
        if(interactable.IsNeedInventory && interactingObject != null)
        {
        OnInteracting?.Invoke(interactingObject);
        }
        interactable.Interact(gameObject);

    }
    private void OnTriggerEnter(Collider collision)
    {
        if (currentCollider != collision || currentCollider == null) // проверка, не столкнулся ли игрок уже с этим предметом
        {
            currentCollider = collision;
            IInteractable interactable = collision.transform.GetComponentInParent<IInteractable>();
            if (interactable != null)
            {
            Debug.Log(collision.gameObject.name);
                if (!interactable.InteractingByKeyPressing)// если с объектом взаимодействуют без нажатия клавиши, то вызывать действие interaction ( см. Ниже)
                {
                    interaction(interactable);
                }
                else// иначе запомнить объект столкновения
                {
           // Debug.Log("Interacting" + "   "+collision.gameObject.name);
                    interactingObject = collision.gameObject;

                }
            }


        }
    }
    
    private void OnTriggerExit(Collider collision)
    {

        if (collision.gameObject == interactingObject)
        {
            Debug.Log("Exit");
            interactingObject = null;
        }
        if(collision == currentCollider)
        {
            currentCollider = null;
        }
    }

}
