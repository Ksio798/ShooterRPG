using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerinput : MonoBehaviour
{
    public PlayerAnimation PlayerAnimation;
    public PlayerMovment PlayerMovementController;
    public PlayerAttack PlayerAttack;
  
    int ButtonPushCount;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (StaticVariables.CanMove)
        {

        Vector2 input = getMovement();
        bool isAttack = getAttack();
        bool isCrouch = getCrouch();
        bool isSprint = getSprint();
        bool isJump = getJump();
        bool IsAim = SetAim();
        bool IsScopeAim = SetScopeAim();
        
        PlayerMovementController.crouch(isCrouch);
        PlayerMovementController.Step(input);
        PlayerMovementController.jump(isJump);
        PlayerMovementController.Sprint(isSprint);
        PlayerAnimation.SetStep(input, isSprint, isCrouch, isJump);
        PlayerAnimation.Aim(IsAim, IsScopeAim);
        }

    }
    Vector2 getMovement()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        return input;
    }
    bool getCrouch()
    {
        return Input.GetKey(KeyCode.C);
    }

    bool getSprint()
    {
        return Input.GetKey(KeyCode.LeftShift);
    }

    bool getAttack()
    {
        return Input.GetButtonUp("Fire1");
    }
    bool SetAim()
    {
        bool p = Input.GetButtonUp("Fire2");
        if(p)
        ButtonPushCount++;
        if (ButtonPushCount == 1)
        {
            return true;
        }
        else if(ButtonPushCount == 2)
        {
            ButtonPushCount = 0;
            return false;
        }
        return false;
    }
    bool SetScopeAim()
    {
        return Input.GetButton("Fire2");
    }



    bool getJump()
    {
        return Input.GetButtonDown("Jump");
    }

}
