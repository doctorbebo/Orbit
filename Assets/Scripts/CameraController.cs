using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{   
    public ShipController shipController;
    public float smoothSpeed = 0.1f;
    public Vector3 offSet;

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 desiredPosition = new Vector3(shipController.xPosCamera, 0, 0) + offSet;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    }
    
    
}
