using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogJumpState : State<FrogController>
{
    private float jumpTimer;

    public override void Enter(FrogController frog)
    {
        Debug.Log("RANA: MAMAWEBO");
        frog.StartCoroutine(frog.ParabolicJump());
    }

    public override void Execute(FrogController frog)
    {
       
    }

    public override void Exit(FrogController _owner)
    {
    }
}
