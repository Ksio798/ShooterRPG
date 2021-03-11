using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    public float speed;
    UnityEngine.CharacterController characterController;
    Vector3 velocity;
    public float VelocityY = -9.8f;
    public float JumpHeight;
    public float GravityY;

    public Transform GrounCheckObj;
    public LayerMask GroundMask;
    public float GroundDistance = 0.3f;
    bool isGrounded;
    private void Start()
    {
        characterController = GetComponent<UnityEngine.CharacterController>();
    }
    private void Update()
    {
        isGrounded = Physics.CheckSphere(GrounCheckObj.position, GroundDistance, GroundMask);
        if (isGrounded && velocity.y < 0)
            velocity.y = 0;
        velocity.y += GravityY*  Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

    }
    public void crouch(bool crouch)
    {

    }
    public void Step(Vector2 Input)
    {
        Vector3 step = transform.right * Input.x + transform.forward * Input.y;

        characterController.Move(step * speed * Time.deltaTime);
    }
    public void jump(bool jumping)
    {
       if(jumping&& isGrounded)
        {
            velocity.y = Mathf.Sqrt(JumpHeight * -2 * GravityY);
        }
    }
    public void Sprint(bool sprint)
    {
        
    }
}