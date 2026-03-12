using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Si es necesario añadir un namespace
public class CoatiController : MonoBehaviour
{
    // Cerebro del Coati
    [Header("Maquina de Estados")]
    public StateMachine<CoatiController> stateMachine;

    [Header("References to World")]
    public NavMeshAgent NavMeshAgent;
    public Transform PlayerTransform;

    [Header("Settings")]
    public float FleeDistance = 5f;
    public float WanderRadius = 10f;
    public float SafeRadius = 8f;

    // Instancia de los estados 
    public CoatiWanderState S_WanderState;
    public CoatiFleeState S_FleeState;

    void Start()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();

        // Inicializamos la maquina de estados como el dueño
        stateMachine = new StateMachine<CoatiController>(this);

        // Instancias de los estados
        S_WanderState = new CoatiWanderState();
        S_FleeState = new CoatiFleeState();

        // Establecemos el estado inicial de la maquina
        stateMachine.SetCurrentState(S_WanderState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Updating();
    }
}
