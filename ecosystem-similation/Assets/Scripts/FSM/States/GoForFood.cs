using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoForFood : State
{
    private bool isFoodStillAvalible;
    private bool isArrived;
    private Vector3 _foodLocation;
    private GameObject foundedFood;

    public GoForFood(Animal animal, StateMachine stateMachine) : base(animal, stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("Going For Food!!");

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
