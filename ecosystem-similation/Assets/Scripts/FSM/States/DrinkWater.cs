using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkWater : State
{

    private float drinkingRate;
    private bool exitState;
    private bool isFull;
    public DrinkWater(Animal animal, StateMachine stateMachine) : base(animal, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Drinking the Water");
        drinkingRate = animal.drinkingRate;
    }

    public override void Exit()
    {
        base.Exit();
    }
    public override void HandleInput()
    {
        if((animal.curThirst - 2f) >= animal.maxThirst){
            isFull = true;
        } 
        else
        {
            isFull = false;
        }
    }

    public override void LogicUpdate()
    {
        animal.curThirst += Time.deltaTime * animal.drinkingRate;

        if(isFull){
            stateMachine.ChangeState(animal.idle);
        }
    }

}
