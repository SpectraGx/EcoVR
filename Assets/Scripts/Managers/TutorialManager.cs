using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class TutorialManager : MonoBehaviour
{
    public TextMeshProUGUI textoInstrucciones;

    [Header("References")]
    public XRGrabInteractable interactableCamera;
    public XRGrabInteractable interactableAlbum;
    
    // Enumerador para controlar las fases del tutorial
    private enum TutorialState { Start, GrabCamera, TakePhoto, GrabAlbum, End }
    private TutorialState currentState = TutorialState.Start;

    void Start()
    {
        // Suscribimos a los eventos de las manos 
        interactableCamera.selectEntered.AddListener(WhenGrabCamera);
        interactableAlbum.selectEntered.AddListener(WhenGrabAlbum);
        
        AdvanceState(TutorialState.GrabCamera);
    }

    private void AdvanceState(TutorialState newState)
    {
        currentState = newState;

        switch (currentState)
        {
            case TutorialState.GrabCamera:
                textoInstrucciones.text = "Bienvenido a EcoVR\nLleva tu mano a la cadera DERECHA y presiona el boton de agarre para sacar la camara";
                break;
            case TutorialState.TakePhoto:
                textoInstrucciones.text = "Perfecto!\nAhora apunta al objetivo de prueba frente a ti y presiona el gatillo para tomar una foto";
                break;
            case TutorialState.GrabAlbum:
                textoInstrucciones.text = "Foto tomada\nLleva tu mano a tu cadera IZQUIERDA para sacar tu album y revisarla";
                break;
            case TutorialState.End:
                textoInstrucciones.text = "¡Listo!\nYa sabes como documentar el ecosistema. Dirigete a la pizzarra para comenzar tu aventura:D";
                break;
        }
    }

    private void WhenGrabCamera(SelectEnterEventArgs args)
    {
        if (currentState == TutorialState.GrabCamera)
        {
            AdvanceState(TutorialState.TakePhoto);
        }
    }

    public void PhotoTakenSuccess() 
    {
        if (currentState == TutorialState.TakePhoto)
        {
            AdvanceState(TutorialState.GrabAlbum);
        }
    }

    private void WhenGrabAlbum(SelectEnterEventArgs args)
    {
        if (currentState == TutorialState.GrabAlbum)
        {
            AdvanceState(TutorialState.End);
        }
    }

    void OnDestroy()
    {
        // Limpiamos los eventos al salir de la escena para evitar errores de memoria
        interactableCamera.selectEntered.RemoveListener(WhenGrabCamera);
        interactableAlbum.selectEntered.RemoveListener(WhenGrabAlbum);
    }
}