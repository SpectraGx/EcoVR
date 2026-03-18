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
    }

    void Update()
    {
        stateMachine.Updating();
    }
}
