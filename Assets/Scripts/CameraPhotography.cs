using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPhotography : MonoBehaviour
{
    [SerializeField] private Transform lensPoint;
    [SerializeField] private float maxPhotoDistance = 12f;
    public CameraFeedback cameraFeedback;

    public void TakePhoto()
    {
        if (lensPoint == null)
        {
            Debug.LogWarning("Lens point no esta asignado.");
            return;
        }
        // 1. VFX & SFX de la camara
        if (cameraFeedback != null) cameraFeedback.TriggerFeedback();

        // CHECAR CON ETHAN QUE RAYCAST SE SIENTE MEJOR (SPHERECAST PARA HACERLO MAS FACIL O RAYCAST NORMAL PARA SER MAS PRECISO)

        //Debug.DrawRay(lensPoint.position, lensPoint.forward * maxPhotoDistance, Color.green, 2f);
        float laserThickness = 0.5f; // Grosor del rayo}
        Debug.DrawRay(lensPoint.position, lensPoint.forward * maxPhotoDistance, Color.red, 2f);
        RaycastHit hit;


        if (Physics.SphereCast(lensPoint.position, laserThickness, lensPoint.forward, out hit, maxPhotoDistance))
        {
            // 2. Buscamos si tiene un tag de "Animal"
            AnimalID animalDetected = hit.collider.GetComponentInParent<AnimalID>();

            if (animalDetected != null)
            {
                // 3. Si es un animal, registramos la foto
                PhotoManager.instance.RegisterPhoto(animalDetected.currentSpecies);
            }
        }
    }
}
