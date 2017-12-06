using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public Vector3 offset;
    public Transform aimPoint;
    public GameObject objectToFocus;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = objectToFocus.transform.position + objectToFocus.transform.forward * offset.z + objectToFocus.transform.up * offset.y + objectToFocus.transform.right * offset.x;
        transform.LookAt(aimPoint);
    }

    public void SetFov(float fov)
    {
        GetComponent<Camera>().fieldOfView = fov;
    }
}

