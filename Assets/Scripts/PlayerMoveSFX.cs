using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveSFX : MonoBehaviour
{
    public InputActionProperty LJoystickVal;

    [Header("SoundSettings")]
    SFXPlayer playerSFX;
    bool isPlaying = false;

    
    void Start()
    {
        //Buscamos el Script de los SFX
        playerSFX = GetComponent<SFXPlayer>();
        isPlaying = false;
    }


    void Update()
    {
        if(PlayerPrefs.GetInt("UseSmoothLocomotion", 1) == 1)
        {
            Vector2 move = LJoystickVal.action.ReadValue<Vector2>();
            if(move != Vector2.zero && isPlaying == false)
            {
                playerSFX.PlaySingleSFX();
                isPlaying = true;
                Debug.Log("PlayingSteps");
            }
            else if(move == Vector2.zero)
            {
                playerSFX.PlayStopSFX();
                isPlaying = false;
            }
        }
        else
        {
            return;
        }
    }
}
