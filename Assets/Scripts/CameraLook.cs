using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    public int sensetivity = 100;
    public Transform Player;
    public Transform Head;
    [HideInInspector]
    public float TargetXRotation = 0;
    [HideInInspector]
    public float TargetYRotation = 0;
    [HideInInspector]
    public float CurentXPosition;
    [HideInInspector]
    public float CurentYPosition;

    public float XrotationSpeed;

    public float YRotationSpeed;
    float Xrotation = 0 ;

    float rotationYvelocity, rotationXvelocity;
    public Transform RightHandTarget;
   
    private void Update()
    {
        if (StaticVariables.CanMove)
        {

        float mouseX = Input.GetAxis("Mouse X") * sensetivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensetivity * Time.deltaTime;
        TargetXRotation -= mouseY;
        TargetXRotation = Mathf.Clamp(TargetXRotation, -70, 90);
        TargetYRotation += mouseX;

        CurentYPosition = Mathf.SmoothDamp(CurentYPosition, TargetYRotation, ref rotationYvelocity, YRotationSpeed);
        CurentXPosition = Mathf.SmoothDamp(CurentXPosition, TargetXRotation, ref rotationXvelocity, XrotationSpeed);
        
        Player.transform.rotation = Quaternion.Euler(0, CurentYPosition, 0);
        Head.transform.localRotation = Quaternion.Euler(CurentXPosition, 0, 0);
        //RightHandTarget.localRotation= Quaternion.Euler(CurentXPosition, 0, 0);
        }

    }

    private void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, CurentYPosition, 0);
    }
}
