using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoatiFleeState : State<CoatiController>
{
    public override void Enter(CoatiController entity)
    {
        Debug.Log("CORRE, COATI, CORRE");
        entity._navMeshAgent.speed = 8f; // Aumentamos la velocidad por pánico
    }

    public override void Execute(CoatiController entity)
    {
        float distanceToPlayer = Vector3.Distance(entity.transform.position, entity._playerTransform.position);

        // E1: ¿Ya me alejé lo suficiente?
        if (distanceToPlayer >= entity._safeRadius)
        {
            // Volvemos a patrullar
            entity.stateMachine.SetCurrentState(entity.wanderState);
            return;
        }

        // E2: Correr alejandose del jugador
        Vector3 directionAwayFromPlayer = entity.transform.position - entity._playerTransform.position;
        directionAwayFromPlayer.y = 0; // Evitamos que intente correr hacia arriba o abajo

        Vector3 fleePosition = entity.transform.position + directionAwayFromPlayer.normalized * 5f;

        // Le ordenamos al NavMeshAgent que corra hacia allá
        entity._navMeshAgent.SetDestination(fleePosition);
    }

    public override void Exit(CoatiController entity)
    {
        Debug.Log("Estoy a salvo");
        entity._navMeshAgent.ResetPath();
    }
}