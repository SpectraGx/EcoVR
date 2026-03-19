using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IguanaIdleState : State<IguanaController>
{
    public override void Enter(IguanaController iguana)
    {
        Debug.Log("IGUANA: COMO PUEDE HACER TANTO CALOOOORRRRR");
    }

    public override void Execute(IguanaController iguana)
    {
        float distanceToPlayer = Vector3.Distance(iguana.PlayerTransform.position, iguana.transform.position);

        if (distanceToPlayer < iguana.FleeDistance)
        {
            // Se esconde dentro de la roca
            iguana.stateMachine.SetCurrentState(iguana.S_FleeState);
        }
    }

    public override void Exit(IguanaController _owner)
    {
        Debug.Log("IGUANA: AY QUE MIEDO, MEJOR ME VOY");
    }
}
