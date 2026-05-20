using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParakeetController : MonoBehaviour
{
    [Header("Maquina de Estados")]
    public StateMachine<ParakeetController> stateMachine;

    [Header("References to World")]
    public Transform PlayerTransform;
    public Transform[] Perches;

    [Header("Settings")]
    public float FleeDistance = 5f;
    public float FlightSpeed = 10f;

    [Header("Animations")]
    public Animator Animator;
    private int currentStateHash;

    public readonly int Anim_Idle = Animator.StringToHash("Idle");
    public readonly int Anim_Fly = Animator.StringToHash("Fly");

    [Header("SoundSettings")]
    SFXPlayer playerSFX;
    float sfxTimer;

    // Instancia de los estados
    public ParakeetIdleState S_IdleState;
    public ParakeetFlightState S_FlightState;

    [HideInInspector] public Transform CurrentPerch; // Para almacenar la rama actual del perico

    void Start()
    {
        stateMachine = new StateMachine<ParakeetController>(this);

        S_IdleState = new ParakeetIdleState();
        S_FlightState = new ParakeetFlightState();

        if (Perches.Length > 0)
        {
            CurrentPerch = Perches[0];
            transform.position = CurrentPerch.position;
        }

        stateMachine.SetCurrentState(S_IdleState);

        //Buscamos el Script de los SFX
        playerSFX = GetComponent<SFXPlayer>();
    }

    void Update()
    {
        stateMachine.Updating();
        ReproduceRandomSound();
    }

    public void ChangeAnimationState(int newStateHash, float transitionDuration = 0.1f)
    {
        if (currentStateHash == newStateHash) return;

        Animator.CrossFade(newStateHash, transitionDuration);
        currentStateHash = newStateHash;
    }

    void ReproduceRandomSound()
    {
        //Reproduces un sonido cada tanto tiempo aleatotrio entre 5 a 15 segundos.
        if(sfxTimer <= 0)
        {
            playerSFX.PlayRandomSFX();
            sfxTimer = Random.Range(10, 20);
        }
        else
        {
            sfxTimer -= 1 * Time.deltaTime;
        }
    }
}
