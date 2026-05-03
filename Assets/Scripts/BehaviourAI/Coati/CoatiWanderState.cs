using UnityEngine;
using UnityEngine.AI;

public class CoatiWanderState : State<CoatiController>
{
    public override void Enter(CoatiController coati)
    {
        //Debug.Log("Coati patrullero");
        coati.NavMeshAgent.speed = 1.5f; // Velocidad de patrullaje
        coati.NavMeshAgent.isStopped = false;

        // E2: PATRULLAJE
        Vector3 randomDirection = Random.insideUnitSphere * coati.WanderRadius;
        randomDirection += coati.transform.position;
        NavMeshHit hit;

        // Checa si el punto es caminable
        if (NavMesh.SamplePosition(randomDirection, out hit, coati.WanderRadius, 1))
        {
            coati.NavMeshAgent.SetDestination(hit.position);
        }
    }

    public override void Execute(CoatiController coati)
    {
        // E1: Checa si el jugador invadio su espacio de seguridad
        float distanceToPlayer = Vector3.Distance(coati.transform.position, coati.PlayerTransform.position); //<---- En el caso que empiece a haber problemas de rendimiento poner un timer que cheque la distancia cada tanto tioempo y no cada cuadro
        if (distanceToPlayer <= coati.FleeDistance)
        {
            coati.stateMachine.SetCurrentState(coati.S_FleeState); // Si el jugador esta muy cerca, cambaia al estado de huida
            return; 
        }

        // E2: ¿Ya llegué a mi destino?
        // Comprobamos si ya calculó la ruta (!pathPending)
        if (!coati.NavMeshAgent.pathPending && coati.NavMeshAgent.remainingDistance <= coati.NavMeshAgent.stoppingDistance)
        {
            // Si el NavMeshAgente ya no tiene a dónde ir, significa que llegó al destino.
            coati.stateMachine.SetCurrentState(coati.S_IdleState); // Transicionamos al estado Idle
        }
    }

    public override void Exit(CoatiController coati)
    {
        coati.NavMeshAgent.ResetPath(); // Detiene el movimiento al salir del estado
    }
}