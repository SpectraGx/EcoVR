using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CoatiController : MonoBehaviour
{
    // Cerebro del Coati
    public StateMachine<CoatiController> stateMachine;

    [Header("References to World")]
    public NavMeshAgent _navMeshAgent;
    public Transform _playerTransform;

    [Header("Settings")]
    public float _fleeDistance = 5f;
    public float _wanderRadius = 10f;
    public float _safeRadius = 8f;

    // Instancia de los estados 
    public CoatiWanderState wanderState;
    public CoatiFleeState fleeState;

    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();

        // Inicializamos la maquina de estados como el dueño
        stateMachine = new StateMachine<CoatiController>(this);

        // Instancias de los estados
        wanderState = new CoatiWanderState();
        fleeState = new CoatiFleeState();

        // Establecemos el estado inicial de la maquina
        stateMachine.SetCurrentState(wanderState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Updating();
    }
}
