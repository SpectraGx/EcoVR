using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogIdleState : State<FrogController>
{
    public override void Enter(FrogController frog)
    {
        //Debug.Log("RANA: CALMAO");
    }

    public override void Execute(FrogController frog)
    {
        float distanceToPlayer = Vector3.Distance(frog.PlayerTransform.position, frog.transform.position);

        if (distanceToPlayer < frog.FleeDistance)
        {
            // Salta para alejarse del jugador
            frog.stateMachine.SetCurrentState(frog.S_JumpState);
        }
    }

    public override void Exit(FrogController _owner)
    {
        //Debug.Log("RANA: AY QUE MIEDO, MEJOR ME_VOY SALTANDO");
    }
}
