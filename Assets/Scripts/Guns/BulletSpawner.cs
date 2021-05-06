using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public Rigidbody BulletPrefab;
    public void SpawnBullet(Vector3 forward,Vector3 position,LayerMask layerMask,float force)
    {
        Rigidbody bp = Instantiate(BulletPrefab);

        Bullet b = bp.GetComponent<Bullet>();
        b.TargetMask = layerMask;
        bp.transform.position = position;
       bp.transform.forward =forward;
        bp.AddForce(forward * force);
    }
}
