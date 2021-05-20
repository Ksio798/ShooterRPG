using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2 : MonoBehaviour
{
    public LayerMask TargetMask;
    public GameObject ImpactPrefab;
    public float Damage;//there
    private void OnCollisionEnter(Collision collision)
    {
        if((TargetMask.value & 1<<collision.gameObject.layer)>0)
        {
            //Члима, нам не нужен интерфейс ICharecter. У нас есть класс Character для таких целей
            //Я изменил скрипт с ипользованием класса, это удобнее (there)
            Character e = collision.transform.GetComponent<Character>();//there
            if(e!=null)
            {
                e.TakeDamage(Damage);//there
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
