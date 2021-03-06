using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoForWater : State
{
    private bool isArrived;
    private Vector3 _waterLocation;

    public GoForWater(Animal animal, StateMachine stateMachine) : base(animal, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _waterLocation = animal.foundedWater.transform.position;
        // making a distance between animal and the water
        _waterLocation -= ((_waterLocation - animal.transform.position).normalized) * 2f;
        animal.GotoDestination(_waterLocation);
    }

    public override void Exit()
    {
        base.Exit();
    }
    public override void HandleInput()
    {
        base.HandleInput();
        isArrived = animal.IsCloseEnough(_waterLocation, 1f);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (animal.goIdle)
        {
            stateMachine.ChangeState(animal.idle);
        }
        if (isArrived)
        {
            stateMachine.ChangeState(animal.drinkWater);
        }
    }

}
