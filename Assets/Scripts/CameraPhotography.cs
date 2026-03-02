using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPhotography : MonoBehaviour
{
    [SerializeField] private Transform lensPoint;
    [SerializeField] private float maxPhotoDistance = 12f;

    public void TakePhoto()
    {
        if (lensPoint == null)
        {
            Debug.LogWarning("Lens point is not assigned.");
            return;
        }

        Debug.DrawRay(lensPoint.position, lensPoint.forward * maxPhotoDistance, Color.green, 2f);
        RaycastHit hit;

        if (Physics.Raycast(lensPoint.position, lensPoint.forward, out hit, maxPhotoDistance))
        {
            if (hit.collider.CompareTag("Fauna"))
            {
                Debug.Log("Foto tomada: " + hit.collider.gameObject.name);
            }
            else
            {
                Debug.Log("Foto del entorno tomada: " + hit.collider.gameObject.name);
            }
        }
        else
        {
            Debug.Log("No enfocada");
        }
    }
}
