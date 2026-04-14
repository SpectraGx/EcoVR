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

        Debug.DrawRay(lensPoint.position, lensPoint.forward * maxPhotoDistance, Color.green, 2f);
        RaycastHit hit;


        if (Physics.Raycast(lensPoint.position, lensPoint.forward, out hit, maxPhotoDistance))
        {
            // 1. VFX & SFX de la camara
            if (cameraFeedback != null) cameraFeedback.TriggerFeedback();

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
