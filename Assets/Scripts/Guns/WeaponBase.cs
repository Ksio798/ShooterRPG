using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum WeaponShootType {Automatic, NonAutomatic }
public class WeaponBase : MonoBehaviour
{
    public WeaponShootType Type;

    public float CoolDown = 0.05f;
    protected float CooldownTimer = 0.05f;
    public float RealodingTime = 0.5f;
    public int BulletInMagazine;
    public int MagazineCount;

    [HideInInspector]
    public int BulletCount;
    protected bool isrealoding = false;
    [HideInInspector]
    public int CurrentBulletCount;
    public Transform BulletSpawnPoint;
    public float ShootForce;

    public LayerMask TargetMask;
  
    protected void Start()
    {
        BulletCount = BulletInMagazine * MagazineCount;
        CurrentBulletCount = BulletInMagazine;
    }
    protected void Update()
    {
        if(CooldownTimer > 0)
        {
            CooldownTimer -= Time.deltaTime;
        }
    }
    public virtual void Shoot(bool isShooting)
    {
        if (Type == WeaponShootType.Automatic)
            ShootAutomatic(isShooting);
        else
            NonAutomatic(isShooting);
    }
    protected virtual void ShootAutomatic(bool isShooting)
    {
        if(isShooting && CooldownTimer<= 0 && CurrentBulletCount > 0 && !isrealoding)
        {
            perfomShootLight();
        }
    }
    protected virtual void NonAutomatic(bool isShooting)
    {
        if(CooldownTimer<= 0 && CurrentBulletCount> 0 && !isrealoding)
        {
            perfomShootLight();
        }

    }
    protected virtual void perfomShootLight()
    {
        CurrentBulletCount--;
        SpawnBullet();
        playEffects(true);
        CooldownTimer = CoolDown;

        if (CurrentBulletCount == 0)
        
        Realoding();
    }
    protected virtual void playEffects(bool effectPlay)
    {
    }
    protected virtual void playSound(bool isShooting)
    {

    }
    public void Realoding()
    {
        StartCoroutine(reloading());
    }
    protected virtual void SpawnBullet()
    {
        FindObjectOfType<BulletSpawner>().SpawnBullet(BulletSpawnPoint.forward, BulletSpawnPoint.position, TargetMask, ShootForce);
    }
    protected IEnumerator reloading()
    {
        isrealoding = true;
        yield return new WaitForSeconds(RealodingTime);

        if(MagazineCount>0)
        {
            BulletCount -= BulletInMagazine;
            CurrentBulletCount += BulletInMagazine;
            MagazineCount--;
        }
        isrealoding = false;
    }


}
