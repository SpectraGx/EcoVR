using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoatiIdleState : State<CoatiController>
{
    private float timer;
    private float idleDuration;

    public override void Enter(CoatiController owner)
    {
        owner.NavMeshAgent.isStopped = true; // Detenemos al Coati

        idleDuration = Random.Range(2f, 5f); // Duración aleatoria entre 2 y 5 segundos
        timer = 0f; // Reiniciamos el timer

        Debug.Log("Coati se ha detenido a descansar.");
    }

    public override void Execute(CoatiController owner)
    {
        // E1: Si el jugador se acerca demasiado, dejamos el descanso y huimos
        float distanceToPlayer = Vector3.Distance(owner.transform.position, owner.PlayerTransform.position);
        if (distanceToPlayer <= owner.FleeDistance)
        {
            owner.stateMachine.SetCurrentState(owner.S_FleeState);
            return;
        }

        // E2: Sumar tiempo y checar si ya descansé lo suficiente
        timer += Time.deltaTime;
        if (timer >= idleDuration)
        {
            // Volvemos a patrullar
            owner.stateMachine.SetCurrentState(owner.S_WanderState);
        }
    }

    public override void Exit(CoatiController owner)
    {
        Debug.Log("Coati esta listo para seguir patrullando.");
        owner.NavMeshAgent.isStopped = false; // Reanudamos el movimiento del Coati
    }
}
