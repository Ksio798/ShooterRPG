using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public LayerMask TargetMask;
    public GameObject ImpactPrefab;
    private void OnCollisionEnter(Collision collision)
    {
        if((TargetMask.value & 1<<collision.gameObject.layer)>0)
        {
            ICharecter e = collision.transform.GetComponent<ICharecter>();
            if(e!=null)
            {
                e.TakeDamage();
            }
            else
            {
                SpawnPref(collision.GetContact(0).point, collision.GetContact(0).normal);
            }
        }
        void SpawnPref(Vector3 pos,Vector3 fow)
        {
            GameObject impact = Instantiate(ImpactPrefab);
            impact.transform.position = pos;
            impact.transform.forward = fow;
        }
    }
}
