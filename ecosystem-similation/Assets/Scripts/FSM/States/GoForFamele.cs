using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoForFamele : State
{
    private bool isArrived;

    public GoForFamele(Animal animal, StateMachine stateMachine) : base(animal, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        animal.matingGround = animal.foundedMate.matingGround;
        animal.GotoDestination(animal.matingGround);
    }

    public override void Exit()
    {
        base.Exit();
        animal.matingGround = Vector3.negativeInfinity;
    }
    public override void HandleInput()
    {
        base.HandleInput();
        isArrived = Vector3.Distance(animal.transform.position, animal.matingGround) < 0.2f;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isArrived)
        {
            stateMachine.ChangeState(animal.mate);
        }

        if (animal.goIdle)
        {
            stateMachine.ChangeState(animal.idle);
        }
    }
}
