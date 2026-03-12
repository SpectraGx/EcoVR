using UnityEngine;
using UnityEngine.AI;

public class CoatiWanderState : State<CoatiController>
{
    private float timer = 0f;
    private float wanderInterval = 5f; // Caada 5s el coati cambia de destino

    public override void Enter(CoatiController entity)
    {
        Debug.Log("Coati patrullero");
        entity.NavMeshAgent.speed = 1.5f;  // Velocidad de patrullaje
        timer = wanderInterval; // Reiniciamos el timer al entrar al estado
    }

    public override void Execute(CoatiController entity)
    {
        // E1: Checa si el jugador invadio su espacio de seguridad
        float distanceToPlayer = Vector3.Distance(entity.transform.position, entity.PlayerTransform.position);//<---- En el caso que empiece a haber problemas de rendimiento poner un timer que cheque la distancia cada tanto tioempo y no cada cuadro
        
        if (distanceToPlayer < entity.FleeDistance)
        {
            // Si el jugador esta muy cerca, cambaia al estado de huida
            entity.stateMachine.SetCurrentState(entity.S_FleeState);
            return;
        }
        // E2: PATRULLAJE
        timer += Time.deltaTime;
        if (timer >= wanderInterval)
        {
            Vector3 randomDirection = Random.insideUnitSphere * entity.WanderRadius;
            randomDirection += entity.transform.position;
            NavMeshHit hit;

            // Checa si el punto es caminable
            if (NavMesh.SamplePosition(randomDirection, out hit, entity.WanderRadius, NavMesh.AllAreas))
            {
                entity.NavMeshAgent.SetDestination(hit.position);
            }

            timer = 0f;
        }
    }

    public override void Exit(CoatiController entity)
    {
        Debug.Log("Coati deja de patrullar");
        entity.NavMeshAgent.ResetPath(); // Detiene el movimiento al salir del estado
    }
}
