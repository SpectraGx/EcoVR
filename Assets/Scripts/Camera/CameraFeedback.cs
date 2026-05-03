using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using System.Collections;

public class CameraFeedback : MonoBehaviour
{
    [Header("VFX & SFX")]
    public AudioSource audioSource;
    public AudioClip shutterSound;
    
    public Light flashLight;
    public float flashDuration = 0.1f; // Duracion del destello

    [Header("Interaccion VR (Vibracion)")]
    public XRGrabInteractable grabInteractable;
    [Range(0f, 1f)] public float hapticIntensity = 0.6f;
    public float hapticDuration = 0.15f;

    void Start()
    {
        // Que el flash empiece apagado
        if (flashLight != null) flashLight.enabled = false;
        
        if (grabInteractable == null) grabInteractable = GetComponent<XRGrabInteractable>();
    }

    /// Metodo que debes llamar cuando tomas la foto / Se puede agregar otros efectos o eventos relacionados a la foto aqui
    public void TriggerFeedback()
    {
        // 1. Sonido
        if (audioSource != null && shutterSound != null)
        {
            audioSource.PlayOneShot(shutterSound);
        }

        // 2. Destello (Flash)
        if (flashLight != null)
        {
            StartCoroutine(FlashRoutine());
        }

        // 3. Vibración en el control real (Checar con Roy)
        TriggerHaptics();
    }

    private IEnumerator FlashRoutine()
    {
        flashLight.enabled = true;
        yield return new WaitForSeconds(flashDuration);
        flashLight.enabled = false;
    }

    private void TriggerHaptics()
    {
        // Verificamos si la cam está siendo agarrada por alguien
        if (grabInteractable.isSelected)
        {
            var interactor = grabInteractable.interactorsSelecting[0] as XRBaseInputInteractor;
            
            if (interactor != null)
            {
                interactor.SendHapticImpulse(hapticIntensity, hapticDuration);
            }
        }
    }
}