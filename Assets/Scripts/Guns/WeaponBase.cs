using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum WeaponShootType {Automatic, semiAutomatic }
public class WeaponBase : MonoBehaviour
{
    public WeaponShootType Type;

    public float CoolDown = 0.05f;
    protected float CooldownTimer = 0.05f;
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
