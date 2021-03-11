using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    public virtual void Shoot(bool isShooting)
    {

    }
    protected virtual void ShootAutomatic(bool isShooting)
    {

    }
    protected virtual void NonAutomatic(bool isShooting)
    {

    }
    protected virtual void perfomShootLight()
    {

    }
    protected virtual void playEffects(bool effectPlay)
    {
    }
    protected virtual void playSound(bool isShooting)
    {

    }
    public void Realoding()
    {

    }
    protected virtual void SpawnBullet()
    {

    }
    //protected IEnumerator reloading()
    //{
    //}


}
