using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CoatiWanderState : State<CoatiController>
{
    private float timer = 0f;
    private float wanderInterval = 5f; // Caada 5s el coati cambia de destino

    public override void Enter(CoatiController entity)
    {
        Debug.Log("Coati patrullero");
        entity._navMeshAgent.speed = 1.5f;  // Velocidad de patrullaje
        timer = wanderInterval; // Reiniciamos el timer al entrar al estado
    }

    public override void Execute(CoatiController entity)
    {
        // E1: Checa si el jugador invadio su espacio de seguridad
        float distanceToPlayer = Vector3.Distance(entity.transform.position, entity._playerTransform.position);
        
        if (distanceToPlayer < entity._fleeDistance)
        {
            // Si el jugador esta muy cerca, cambaia al estado de huida
            entity.stateMachine.SetCurrentState(entity.fleeState);
            return;
        }

        // E2: PATRULLAJE
        timer += Time.deltaTime;
        if (timer >= wanderInterval)
        {
            Vector3 randomDirection = Random.insideUnitSphere * entity._wanderRadius;
            randomDirection += entity.transform.position;
            NavMeshHit hit;

            // Checa si el punto es caminable
            if (NavMesh.SamplePosition(randomDirection, out hit, entity._wanderRadius, NavMesh.AllAreas))
            {
                entity._navMeshAgent.SetDestination(hit.position);
            }

            timer = 0f;
        }
    }

    public override void Exit(CoatiController entity)
    {
        Debug.Log("Coati deja de patrullar");
        entity._navMeshAgent.ResetPath(); // Detiene el movimiento al salir del estado
    }
}
