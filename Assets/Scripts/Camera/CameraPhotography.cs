using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraPhotography : MonoBehaviour
{
    [SerializeField] private Transform lensPoint;
    [SerializeField] private float maxPhotoDistance = 12f;
    public CameraFeedback cameraFeedback;

    [Header("Memory Connection")]
    public PhotoCaptureSystem photoCaptureSystem;

    [Header("Tutorial Connection")]
    public TutorialManager tutorialManager;

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
                speciesDetectedName = animalDetected.currentSpecies.ToString(); 
            }
            else if (hit.collider.CompareTag("ObjectiveTutorial")) // 3. Si no es un animal, verificamos si es el objetivo del tutorial
            {
                speciesDetectedName = "Ninguno";
                if (tutorialManager != null)
                {
                    tutorialManager.PhotoTakenSuccess(); // Le decimos al tutorial que se tomo la foto correctamente
                }
            }
        }

        // 4. Le decimos al sistema que guarde la textura
        if (photoCaptureSystem != null)
        {
            photoCaptureSystem.TakePhotograph(speciesDetectedName);
        }
    }
}