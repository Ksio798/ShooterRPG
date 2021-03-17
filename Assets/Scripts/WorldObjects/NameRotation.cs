using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameRotation : MonoBehaviour
{
     Camera MainCamera;
    void Start()
    {
        MainCamera = FindObjectOfType<Camera>();
    }


    void Update()
    {
        transform.LookAt(MainCamera.transform.position);
        //transform.eulerAngles = new Vector3(0, 180 + transform.eulerAngles.y, 0);
        transform.eulerAngles = new Vector3( -1*transform.eulerAngles.x, 180 + transform.eulerAngles.y, 0);
    }
}
