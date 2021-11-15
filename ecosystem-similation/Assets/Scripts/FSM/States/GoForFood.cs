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
        animal.GotoDestination(_foodLocation);
    }

    public override void Exit()
    {
        base.Exit();
    }
    public override void HandleInput()
    {
        isFoodStillAvalible = animal.foundedFood.GetComponent<Food>().isEatable;
        isFoodStartedToBeEaten = animal.foundedFood.GetComponent<Food>().isStartedToBeEaten;
        isArrived = animal.IsCloseEnough(_foodLocation, 2f);
    }

    public override void LogicUpdate()
    {
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
