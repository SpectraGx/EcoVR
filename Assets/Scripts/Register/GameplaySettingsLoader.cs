using UnityEngine;

public class GameplaySettingsLoader : MonoBehaviour
{
    [Header("References Locomotion")]
    public Behaviour continousMove;
    public Behaviour teleportMove;
    public UnityEngine.XR.Interaction.Toolkit.Locomotion.Movement.ContinuousMoveProvider continuousMoveProvider;

    [Header("References Hands & Smartwatch")]
    public Transform smartWatch;
    public Transform leftHand;
    public Transform rightHand;

    void Start()
    {
        ApplySaveSettings();
    }

    private void ApplySaveSettings()
    {
        // 1. LEER LOCOMOTION (Por defecto 1 = smooth)
        bool useSmooth = PlayerPrefs.GetInt("UseSmoothLocomotion", 1) == 1;
        if (continousMove != null) continousMove.enabled = useSmooth;
        if (teleportMove != null) teleportMove.enabled = !useSmooth;

        // 2. LEER ACELERATION (Por defecto 0 = apagado)
        bool useAcceleration = PlayerPrefs.GetInt("UseAcceleration", 0) == 1;
        if (continuousMoveProvider != null)
        {
            continuousMoveProvider.moveSpeed = useAcceleration ? 3.5f : 1.5f;
        }

        // 3. LEER MANO DOMINANTE (Por defecto 0 = diestro)
        bool isLeftHanded = PlayerPrefs.GetInt("IsLeftHanded", 0) == 1;
        
        // El reloj va en la mano NO DOMINANTE
        if (smartWatch != null && leftHand != null && rightHand != null)
        {
            if (isLeftHanded)
            {
                // Jugador Zurdo: Sujeta la cámara con la izquierda, el reloj va en la derecha.
                smartWatch.SetParent(rightHand, false);
            }
            else
            {
                // Jugador Diestro: Sujeta la cámara con la derecha, el reloj va en la izquierda.
                smartWatch.SetParent(leftHand, false);
            }
        }

        Debug.Log("GameplaySettingsLoader: Ajustes cargados correctamente en la escena.");
    }
}