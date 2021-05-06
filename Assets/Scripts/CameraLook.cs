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
    public float CurrentXRotation;
    [HideInInspector]
    public float CurrentYRotation;

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

        CurrentYRotation = Mathf.SmoothDamp(CurrentYRotation, TargetYRotation, ref rotationYvelocity, YRotationSpeed);
        CurrentXRotation = Mathf.SmoothDamp(CurrentXRotation, TargetXRotation, ref rotationXvelocity, XrotationSpeed);
        
        Player.transform.rotation = Quaternion.Euler(0, CurrentYRotation, 0);
        Head.transform.localRotation = Quaternion.Euler(CurrentXRotation, 0, 0);
            transform.localRotation = Quaternion.Euler(CurrentXRotation, 0, 0);
            //RightHandTarget.localRotation= Quaternion.Euler(CurentXPosition, 0, 0);
        }

    }

    private void LateUpdate()
    {
       // transform.rotation = Quaternion.Euler(transform.eulerAngles.x, CurentYPosition, 0);
    }
}
