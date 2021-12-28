using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForMale : State
{

    public WaitForMale(Animal animal, StateMachine stateMachine) : base(animal, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        animal.goIdle = false;
        animal.GotoDestination(animal.transform.position);
        animal.matingGround = animal.DecideMatingGround();
    }

    public override void Exit()
    {
        base.Exit();
    }
    public override void HandleInput()
    {
        base.HandleInput();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (animal.foundedMate.isMating)
        {
            stateMachine.ChangeState(animal.mate);
        }
    }
}
