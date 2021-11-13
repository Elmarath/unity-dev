using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatFood : State
{
    private GameObject _foodToBeEaten;

    private Vector3 foodLocaction;
    private bool isFoodFinished;
    private Food _foodToBeEatenRef;

    public EatFood(Animal animal, StateMachine stateMachine) : base(animal, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _foodToBeEaten = animal.foodToBeEaten;
        _foodToBeEatenRef = animal.foodToBeEaten.GetComponent<Food>();
        foodLocaction = _foodToBeEaten.transform.position;
        _foodToBeEatenRef.GetEaten();
        Debug.Log("Started eating Food");
    }

    public override void Exit()
    {
        base.Exit();
    }
    public override void HandleInput()
    {
        isFoodFinished = !_foodToBeEatenRef.isEatable;
    }

    public override void LogicUpdate()
    {
        if (isFoodFinished)
        {
            stateMachine.ChangeState(animal.idle);
        }
    }

}
