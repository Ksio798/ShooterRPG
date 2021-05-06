using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : WeaponBase
{
    public Vector3 StandartPlacePosition;
    Vector3 CurrentGunPosition;
    public CameraLook camlook;
    public Transform Righteye;
    public Vector3 RecoilAmount = new Vector3(0.5f, 0.5f, 0.5f);
    public Vector3 RecoilTime = new Vector3(0.5f, 0.5f, 0.5f);
    Vector3 CurrentRecoil;
    public float GunPresicion;
    protected float Velocity_z_recoil, Velocity_x_Recoil, Velocity_y_Recoil;
    protected Vector3 velY;
    private void LateUpdate()
    {
        //base.Update();
            PositionGun();
        
       
      //  transform.localPosition = StandartPlacePosition;
    }
    protected override void playEffects(bool effectPlay)
    {
        base.playEffects(effectPlay);
        if (Type == WeaponShootType.Automatic)
            SetRecoil();
        else
            SetRecoilAutomatic();

    }

    void PositionGun()
    {
        Vector3 pos = new Vector3(camlook.transform.position.x, camlook.transform.position.y, camlook.transform.position.z);
        transform.position = pos - (camlook.transform.right * (StandartPlacePosition.x)) + (camlook.transform.up * (StandartPlacePosition.y)) + (camlook.transform.forward * (StandartPlacePosition.z));
        transform.rotation = Quaternion.Euler(camlook.CurrentXRotation, camlook.CurrentYRotation, 0);
        CurrentRecoil.z = Mathf.SmoothDamp(CurrentRecoil.z, 0, ref Velocity_z_recoil, RecoilTime.z);
        CurrentRecoil.x = Mathf.SmoothDamp(CurrentRecoil.x, 0, ref Velocity_x_Recoil, RecoilTime.z);
        CurrentRecoil.y = Mathf.SmoothDamp(CurrentRecoil.y, 0, ref Velocity_y_Recoil, RecoilTime.z);
        StandartPlacePosition.z = CurrentRecoil.z;
    }
    public void SetRecoilAutomatic()
    {
        CurrentRecoil.z -= RecoilAmount.z * Time.deltaTime;
        CurrentRecoil.x -= RecoilAmount.x * Time.deltaTime;
        CurrentRecoil.y -= RecoilAmount.y * Time.deltaTime;
        camlook.TargetXRotation -= Mathf.Abs(CurrentRecoil.y * Time.deltaTime * GunPresicion);
        camlook.TargetYRotation -= (CurrentRecoil.x * Time.deltaTime * GunPresicion);
        StandartPlacePosition.z = CurrentRecoil.z;
    }
    public void SetRecoil()
    {
        CurrentRecoil.z -= RecoilAmount.z;
        CurrentRecoil.x -= RecoilAmount.x;
        CurrentRecoil.y -= RecoilAmount.y;
        if(camlook != null)
        {
            camlook.TargetXRotation -= Mathf.Abs(CurrentRecoil.y * GunPresicion);
            camlook.TargetYRotation -= (CurrentRecoil.x * GunPresicion);
        }
    }
}
