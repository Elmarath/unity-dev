using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatFood : State
{
    private bool isFoodFinished;
    private GameObject foundFood;
    private Food _foodToBeEatenRef;

    private bool exitState;

    public EatFood(Animal animal, StateMachine stateMachine) : base(animal, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _foodToBeEatenRef = animal.foundedFood.GetComponent<Food>();
        _foodToBeEatenRef.GetEaten();
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
            animal.curHunger += animal.gettingFullMultiplier;
        }

        if (exitState)
        {
            stateMachine.ChangeState(animal.idle);
        }
    }

}
