using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPhotography : MonoBehaviour
{
    [SerializeField] private Transform lensPoint;
    [SerializeField] private float maxPhotoDistance = 12f;
    public CameraFeedback cameraFeedback;

    [Header("Memory Connection")]
    public PhotoCaptureSystem photoCaptureSystem;

    public void TakePhoto()
    {
        if (lensPoint == null)
        {
            Debug.LogWarning("Lens point no esta asignado.");
            return;
        }

        // 1. VFX & SFX de la camara
        if (cameraFeedback != null) cameraFeedback.TriggerFeedback();

        float laserThickness = 0.5f; 
        Debug.DrawRay(lensPoint.position, lensPoint.forward * maxPhotoDistance, Color.red, 2f);
        RaycastHit hit;

        string speciesDetectedName = "Ninguno"; // Por defecto, asume que es una foto al paisaje

        if (Physics.SphereCast(lensPoint.position, laserThickness, lensPoint.forward, out hit, maxPhotoDistance))
        {
            // 2. Buscamos si el objeto golpeado tiene el script AnimalID
            AnimalID animalDetected = hit.collider.GetComponentInParent<AnimalID>();

            if (animalDetected != null)
            {
                // 3. Si es un animal, registramos la foto en el UI
                PhotoManager.instance.RegisterPhoto(animalDetected.currentSpecies);
                
                // Extraemos el nombre de la especie para la memoria
                speciesDetectedName = animalDetected.currentSpecies.ToString(); 
            }
        }

        // 4. Le decimos al sistema que guarde la textura
        if (photoCaptureSystem != null)
        {
            photoCaptureSystem.TakePhotograph(speciesDetectedName);
        }
    }
}