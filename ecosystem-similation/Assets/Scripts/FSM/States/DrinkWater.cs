using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkWater : State
{
    private bool isFull;
    private GameObject waterFound;

    private bool exitState;

    public DrinkWater(Animal animal, StateMachine stateMachine) : base(animal, stateMachine)
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
        if (!animal.foundedWater)
        {
            exitState = true;
        }
        isFull = ((animal.maxThirst - 0.5f) <= animal.curThirst);

        if (!isFull)
        {
            animal.curThirst += Time.deltaTime * animal.drinkingRate;
        }
    }

    public override void LogicUpdate()
    {
        if (isFull)
        {
            stateMachine.ChangeState(animal.idle);
        }

        if (exitState)
        {
            stateMachine.ChangeState(animal.idle);
        }
    }

}
