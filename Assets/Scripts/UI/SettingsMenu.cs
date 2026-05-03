using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class SettingsMenu : MonoBehaviour
{
    [Header("References Locomotion")]
    public Behaviour continousMove;
    public Behaviour teleportMove;
    public UnityEngine.XR.Interaction.Toolkit.Locomotion.Movement.ContinuousMoveProvider continuousMoveProvider;

    [Header("References Visual (Post-it Images)")]
    public Image postItLeft; 
    public Image postItRight;
    public Image postItLocomotion;   
    public Image postItAcceleration; 
    
    [Header("Palette Colors")]
    public Color colorHandSelected = new Color(1f, 0.7f, 0.9f); // Rosa para mano
    public Color colorToggleOn = new Color(0.7f, 0.9f, 1f);  // Azul claro para toggles
    public Color colorOff = new Color(0.8f, 0.8f, 0.8f);        // Gris para apagado

    private void Start()
    {
        // 1. Cargar estado de la mano dominante (Por defecto diestro = 0)
        bool isLeftHanded = PlayerPrefs.GetInt("IsLeftHanded", 0) == 1;
        UpdateVisualsHands(isLeftHanded);

        // 2. Cargar estado de la locomotion (Por defecto encendido = 1)
        bool isSmooth = PlayerPrefs.GetInt("UseSmoothLocomotion", 1) == 1;
        if(postItLocomotion != null) postItLocomotion.color = isSmooth ? colorToggleOn : colorOff;

        // 3. Cargar estado de la aceleracion (Por defecto apagado = 0)
        bool useAcceleration = PlayerPrefs.GetInt("UseAcceleration", 0) == 1;
        if(postItAcceleration != null) postItAcceleration.color = useAcceleration ? colorToggleOn : colorOff;
    }

    // --- LOCOMOTION ---
    public void SetLocomotion(bool isSmooth)
    {
        // Si isSmooth es True, encendemos Continuous y apagamos Teleport
        if(continousMove != null) continousMove.enabled = isSmooth;
        if(teleportMove != null) teleportMove.enabled = !isSmooth;

        // Guardamos la preferencia
        PlayerPrefs.SetInt("UseSmoothLocomotion", isSmooth ? 1 : 0);
        PlayerPrefs.Save();
        
        // Cambio visual de color
        if(postItLocomotion != null)
        {
            postItLocomotion.color = isSmooth ? colorToggleOn : colorOff;
        }
            
        Debug.Log("Locomoción configurada en: " + (isSmooth ? "Smooth (Continuo)" : "Teletransporte"));
    }

    // --- ACELERACIÓN ---
    public void SetAcceleration(bool useAcceleration)
    {
        if (continuousMoveProvider != null)
        {
            // Velocidad 3.5 = Correr | Velocidad 2.5 = Caminar normal
            continuousMoveProvider.moveSpeed = useAcceleration ? 3.5f : 2.5f;
        }
        
        // Guardamos la preferencia
        PlayerPrefs.SetInt("UseAcceleration", useAcceleration ? 1 : 0);
        PlayerPrefs.Save();
        
        // Cambio visual de color
        if(postItAcceleration != null)
        {
            postItAcceleration.color = useAcceleration ? colorToggleOn : colorOff;
        }

        Debug.Log("Aceleración activada: " + useAcceleration);
    }

    // --- DOMINATION HAND ---
    public void SelectedHandLeft()
    {
        PlayerPrefs.SetInt("IsLeftHanded", 1);
        PlayerPrefs.Save();
        UpdateVisualsHands(true);
    }

    public void SelectedHandRight()
    {
        PlayerPrefs.SetInt("IsLeftHanded", 0);
        PlayerPrefs.Save();
        UpdateVisualsHands(false);
    }

    private void UpdateVisualsHands(bool isLeftHanded)
    {
        if (postItLeft != null && postItRight != null)
        {
            postItLeft.color = isLeftHanded ? colorHandSelected : colorOff;
            postItRight.color = isLeftHanded ? colorOff : colorHandSelected;
        }
    }
}