using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator anim;
    public Transform LookTarget;
    public CameraLook camera;
    public Transform RightHandTarget;
    public Transform LeftHandTarget;

    public Vector3 offset;
    Vector3 shoulderEuler;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void SetStep(Vector2 newInput,bool sprint,bool crouch,bool jump)
    {
        anim.SetFloat("X", newInput.x);
        anim.SetFloat("Y", newInput.y);
        anim.SetBool("Sprinting", sprint);
        anim.SetBool("Crouching", crouch);
        if (jump)
            anim.SetTrigger("Jumping");
    }
    public void Aim(bool IsAim,bool IsScopeAim)
    {
        anim.SetBool("Aim", IsAim);
        anim.SetBool("SuperAim", IsScopeAim);
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (layerIndex == 0)
        {
            Debug.Log("IK PASS");
            // animation.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            // animation.SetIKPosition(AvatarIKGoal.RightHand, RightHandTarget.position);
            Transform shoulder = anim.GetBoneTransform(HumanBodyBones.Chest);
            shoulderEuler = shoulder.localEulerAngles;
            shoulderEuler.z = 180 - LookTarget.eulerAngles.x;
            anim.SetBoneLocalRotation(HumanBodyBones.Chest, Quaternion.Euler(shoulderEuler));

        }
        else if (layerIndex == 1) 
        {
            RightHandTarget.position = camera.transform.position -
                (camera.transform.right * offset.x + camera.transform.up * offset.y + camera.transform.forward * offset.z);

            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            anim.SetIKPosition(AvatarIKGoal.RightHand, RightHandTarget.position);
        }
        else if (layerIndex == 2)
        {
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            anim.SetIKPosition(AvatarIKGoal.LeftHand, LeftHandTarget.position);

        }
    }
}
