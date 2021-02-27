using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    // Update is called once per frame
    void Update()
    {
        yaw += speedH * Input.GetAxis("Mouse X");
        //Debug.Log(Input.GetAxisRaw("Mouse Y"));

        if(Input.GetMouseButtonDown(2)) {
            //pitch = Input.GetAxis("Mouse Y");
        }
        if(Input.GetMouseButton(2)) {
            
            pitch -= speedV * Input.GetAxisRaw("Mouse Y");
            transform.eulerAngles = new Vector3(0, yaw, 0.0f);
        }
    }
}
