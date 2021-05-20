using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet3 : MonoBehaviour
{
    public LayerMask TargetMask;
    public GameObject ImpactPrefab;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Bullet hit object 0");
        if ((TargetMask.value & 1 << collision.gameObject.layer) > 0)
        {
            Debug.Log("Bullet hit object 1");
            ICharecter e = collision.transform.GetComponent<ICharecter>();
            if (e != null)
            {
                e.TakeDamage();
            }
            else
            {

                Debug.Log("Bullet hit object");
              
                SpawnPref(collision.GetContact(0).point, -collision.GetContact(0).normal);
            }
        }

    }
    void SpawnPref(Vector3 pos, Vector3 forward)
    {
        GameObject impact = Instantiate(ImpactPrefab);
        impact.transform.position = pos-forward*0.1f;
        impact.transform.forward = forward;
    }
}
