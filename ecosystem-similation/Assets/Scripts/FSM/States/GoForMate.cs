using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoForMate : State
{
    private bool isArrived;
    private Vector3 _waterLocation;

    public GoForMate(Animal animal, StateMachine stateMachine) : base(animal, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
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

        if (animal.goIdle)
        {
            stateMachine.ChangeState(animal.idle);
        }
    }

}
