using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IguanaFleeState : State<IguanaController>
{
    public override void Enter(IguanaController iguana)
    {
        iguana.transform.LookAt(iguana.hideoutPoint.position);
    }

    public override void Execute(IguanaController iguana)
    {
        // E1: Movemos a la iguana hacia la grieta
        iguana.transform.position = Vector3.MoveTowards(
            iguana.transform.position, 
            iguana.hideoutPoint.position, 
            iguana.RunSpeed * Time.deltaTime
        );

        // E2: ¿Ya llegó a la grieta?
        float distanceToHideout = Vector3.Distance(iguana.transform.position, iguana.hideoutPoint.position);
        
        if (distanceToHideout <= 0.1f)
        {
            iguana.stateMachine.SetCurrentState(iguana.S_HideState);
        }
    }

    public override void Exit(IguanaController _owner)
    {
        Debug.Log("IGUANA: ESTOY A SALVO");
    }
}
