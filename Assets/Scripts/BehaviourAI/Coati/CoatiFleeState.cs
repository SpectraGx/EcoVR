using UnityEngine;
using UnityEngine.AI;

public class CoatiFleeState : State<CoatiController>
{
    public override void Enter(CoatiController entity)
    {
        //Debug.Log("CORRE, COATI, CORRE");
        entity.NavMeshAgent.speed = 8f; // Aumentamos la velocidad por pánico
        entity.NavMeshAgent.isStopped = false; // Nos aseguramos de que pueda moverse
        
        CalculateEscapeRoute(entity); // Calculamos la ruta de huida una sola vez al entrar
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
        // Si ya llegó al destino, le damos un nuevo punto para seguir corriendo.
        if (!entity.NavMeshAgent.pathPending && entity.NavMeshAgent.remainingDistance <= entity.NavMeshAgent.stoppingDistance)
        {
            CalculateEscapeRoute(entity);
        }
    }

    public override void Exit(CoatiController entity)
    {
        //Debug.Log("Estoy a salvo");
        entity.NavMeshAgent.ResetPath(); 
    }

    private void CalculateEscapeRoute(CoatiController entity)
    {
        Vector3 directionAwayFromPlayer = entity.transform.position - entity.PlayerTransform.position;
        directionAwayFromPlayer.y = 0; // Evitamos ejes verticales

        if (directionAwayFromPlayer.sqrMagnitude < 0.01f)
        {
            directionAwayFromPlayer = entity.transform.forward; 
        }

        // Buscamos un punto lejano usando el SafeRadius
        Vector3 fleePosition = entity.transform.position + directionAwayFromPlayer.normalized * entity.SafeRadius;

        NavMeshHit hit;
        // Validamos que el punto exista dentro del NavMesh
        if (NavMesh.SamplePosition(fleePosition, out hit, entity.SafeRadius, NavMesh.AllAreas))
        {
            entity.NavMeshAgent.SetDestination(hit.position);
        }
        else
        {
            // PLAN B: Si el jugador acorrala al coati contra un limite del mapa y no puede regresar que huya hacia un punto aleatorio 
            // cercano para intentar escapar por los lados
            Vector3 randomEscape = Random.insideUnitSphere * entity.SafeRadius;
            randomEscape += entity.transform.position;
            if (NavMesh.SamplePosition(randomEscape, out hit, entity.SafeRadius, NavMesh.AllAreas))
            {
                entity.NavMeshAgent.SetDestination(hit.position);
            }
        }
    }
}