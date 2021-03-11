using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    GameObject interactingObject; // ������ �� ������, ������� ��������� �����, ����� ����� ���� � ��� �����������������

    Collider currentCollider; // ��� ���������, � ������� ����� ���������������, ����� ��� �� ���������� 2 ���� ������ ��� ������ � ���� �� ��������
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
        if (currentCollider != collision || currentCollider == null) // ��������, �� ���������� �� ����� ��� � ���� ���������
        {
            currentCollider = collision;
            IInteractable interactable = collision.transform.GetComponentInParent<IInteractable>();
            if (interactable != null)
            {
            Debug.Log(collision.gameObject.name);
                if (!interactable.InteractingByKeyPressing)// ���� � �������� ��������������� ��� ������� �������, �� �������� �������� interaction ( ��. ����)
                {
                    interaction(interactable);
                }
                else// ����� ��������� ������ ������������
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
