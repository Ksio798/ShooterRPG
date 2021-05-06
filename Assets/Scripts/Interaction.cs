using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{

    // GameObject interactingObject; // ������ �� ������, ������� ��������� �����, ����� ����� ���� � ��� �����������������
    List<GameObject> interactingObjects = new List<GameObject>();
    GameObject inSight;
    Collider currentCollider; // ��� ���������, � ������� ����� ���������������, ����� ��� �� ���������� 2 ���� ������ ��� ������ � ���� �� ��������
    public event System.Action<GameObject> OnInteracting;
    public LayerMask ObMask;
    private void Update()
    {

        RaycastHit hit;
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward);

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 1000f, ObMask))
        {
            Transform objectInt = hit.transform;

            if (interactingObjects.Contains(objectInt.gameObject))
            {


                inSight = objectInt.gameObject;
                inSight.GetComponent<InteractingObject>().OnRay();
                // Debug.Log(inSight.name);
            }
        }
        else if (inSight != null)
        {
            inSight.GetComponent<InteractingObject>().OutRay();
            inSight = null;
        }
        Interact();

    }
    void Interact()
    {

        if (Input.GetKeyDown(KeyCode.E) && inSight != null)
        {
            //  Debug.Log("Interacting");
            int index = interactingObjects.IndexOf(inSight);
            IInteractable interactable = interactingObjects[index].GetComponent<IInteractable>();
            interaction(interactable);
        }
    }
    void interaction(IInteractable interactable)
    {

        if (interactable.IsNeedInventory && inSight != null)
        {
            StaticVariables.CanMove = !StaticVariables.CanMove;
            Cursor.visible = !Cursor.visible;

            OnInteracting?.Invoke(inSight);
        }
        interactable.Interact(gameObject);

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (currentCollider != collision || currentCollider == null) // ��������, �� ���������� �� ����� ��� � ���� ���������
        {
            currentCollider = collision;
            IInteractable interactable = collision.transform.GetComponentInParent<IInteractable>();
            InteractingObject interactableGameObject = collision.transform.GetComponentInParent<InteractingObject>();
            if (interactable != null)
            {

                if (!interactable.InteractingByKeyPressing)// ���� � �������� ��������������� ��� ������� �������, �� �������� �������� interaction ( ��. ����)
                {
                    interaction(interactable);
                }
                else// ����� ��������� ������ ������������
                {

                   
                    interactingObjects.Add(interactableGameObject.gameObject);


                }
            }


        }
    }

    private void OnTriggerExit(Collider collision)
    {


        InteractingObject interactingObject = collision.transform.GetComponentInParent<InteractingObject>();
        if (interactingObject != null)
        {

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

}
