using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoForFood : State
{
    private bool isFoodStillAvalible;
    private bool isArrived;
    private Vector3 _foodLocation;
    private GameObject foundedFood;
    private bool isFoodStartedToBeEaten;

    public GoForFood(Animal animal, StateMachine stateMachine) : base(animal, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        foundedFood = animal.foundedFood;
        _foodLocation = animal.foundedFood.transform.position;
        _foodLocation.y = 0;
        animal.GotoDestination(_foodLocation);
    }

    public override void Exit()
    {
        base.Exit();
    }
    public override void HandleInput()
    {
        base.HandleInput();
        isFoodStillAvalible = animal.foundedFood.GetComponent<Food>().isEatable;
        isFoodStartedToBeEaten = animal.foundedFood.GetComponent<Food>().isStartedToBeEaten;
        isArrived = animal.IsCloseEnough(_foodLocation, 2f);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (animal.goIdle)
        {
            stateMachine.ChangeState(animal.idle);
        }
        if (isFoodStartedToBeEaten)
        {
            stateMachine.ChangeState(animal.idle);
        }

        else if (!isFoodStillAvalible)
        {
            stateMachine.ChangeState(animal.idle);
        }
        else if (isFoodStillAvalible && isArrived)
        {
            stateMachine.ChangeState(animal.eatFood);
        }
    }

}
