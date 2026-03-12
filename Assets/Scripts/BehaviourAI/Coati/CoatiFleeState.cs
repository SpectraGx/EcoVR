using UnityEngine;

public class CoatiFleeState : State<CoatiController>
{
    public override void Enter(CoatiController entity)
    {
        Debug.Log("CORRE, COATI, CORRE");
        entity.NavMeshAgent.speed = 8f; // Aumentamos la velocidad por pánico
    }

    public override void Execute(CoatiController entity)
    {
        float distanceToPlayer = Vector3.Distance(entity.transform.position, entity.PlayerTransform.position);

        // E1: ¿Ya me alejé lo suficiente?
        if (distanceToPlayer >= entity.SafeRadius)
        {
            // Volvemos a patrullar
            entity.stateMachine.SetCurrentState(entity.S_WanderState);
            return;
        }

        // E2: Correr alejandose del jugador
        Vector3 directionAwayFromPlayer = entity.transform.position - entity.PlayerTransform.position;
        directionAwayFromPlayer.y = 0; // Evitamos que intente correr hacia arriba o abajo

        Vector3 fleePosition = entity.transform.position + directionAwayFromPlayer.normalized * 5f;

        // Le ordenamos al NavMeshAgent que corra hacia allá
        entity.NavMeshAgent.SetDestination(fleePosition);
    }

    public override void Exit(CoatiController entity)
    {
        Debug.Log("Estoy a salvo");
        entity.NavMeshAgent.ResetPath();
    }
}