using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRecover : MonoBehaviour
{
    [SerializeField] GameObject CameraObject;
    [SerializeField] Transform ReturnPos;

    public void ReturnCamera()
    {
        CameraObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        CameraObject.transform.position = ReturnPos.position;
    }
}
