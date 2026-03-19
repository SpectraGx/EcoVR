using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IguanaController : MonoBehaviour
{
    [Header("Maquina de Estados")]
    public StateMachine<IguanaController> stateMachine;

    [Header("References to World")]
    public Transform PlayerTransform;
    public Transform hideoutPoint;

    [Header("Settings")]
    public float FleeDistance = 2f; // Para asustarse tiene que estar muy cerca
    public float RunSpeed = 5f;

    // Instancia de los estados
    public IguanaIdleState S_IdleState;
    public IguanaFleeState S_FleeState;
    public IguanaHiddenState S_HideState;

    void Start()
    {
        stateMachine = new StateMachine<IguanaController>(this);

        S_IdleState = new IguanaIdleState();
        S_FleeState = new IguanaFleeState();
        S_HideState = new IguanaHiddenState();

        stateMachine.SetCurrentState(S_IdleState); // Tomando el sol
    }

    void Update()
    {
        stateMachine.Updating();
    }
}
