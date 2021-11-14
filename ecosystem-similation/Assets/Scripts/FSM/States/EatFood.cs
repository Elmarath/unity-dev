using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatFood : State
{
    private bool isFoodFinished;
    private GameObject foundFood;
    private Food _foodToBeEatenRef;
    private Vector3 foodLocation;

    private bool exitState;

    public EatFood(Animal animal, StateMachine stateMachine) : base(animal, stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("Eating food!!");

        base.Enter();
        _foodToBeEatenRef = animal.foundedFood.GetComponent<Food>();
        _foodToBeEatenRef.GetEaten();
        Debug.Log("Started eating Food");
    }

    public override void Exit()
    {
        base.Exit();
    }
    public override void HandleInput()
    {
        if (!animal.foundedFood)
        {
            exitState = true;
        }
        isFoodFinished = !_foodToBeEatenRef.isEatable;
    }

    public override void LogicUpdate()
    {
        if (isFoodFinished)
        {
            stateMachine.ChangeState(animal.idle);
        }

        if (exitState)
        {
            stateMachine.ChangeState(animal.idle);
        }
    }

}
