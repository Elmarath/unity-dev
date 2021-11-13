using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoForFood : State
{
    private bool isFoodStillAvalible;
    private bool isArrived;
    private Vector3 _foodLocation;

    public GoForFood(Animal animal, StateMachine stateMachine) : base(animal, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        animal.GotoDestination(_foodLocation);
    }

    public override void Exit()
    {
        base.Exit();
    }
    public override void HandleInput()
    {
        isFoodStillAvalible = animal.foundFood;
        _foodLocation = animal.foodLocation;
        animal.foodToBeEaten = animal.targetFood;
        isArrived = animal.IsCloseEnough(_foodLocation, 1f);
    }

    public override void LogicUpdate()
    {
        if (!isFoodStillAvalible)
        {
            stateMachine.ChangeState(animal.idle);
        }
        else if (isFoodStillAvalible && isArrived)
        {
            stateMachine.ChangeState(animal.eatFood);
        }
    }

}
