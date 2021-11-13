using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchForFood : State
{
    private Vector3 destination;
    private Vector3 _foodLocation;
    private float _speed;
    private bool isArrived;
    private bool _foundFood;
    private bool isEating;

    public SearchForFood(Animal animal, StateMachine stateMachine) : base(animal, stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        _speed = animal.normalSpeed;
        destination = animal.CreateRandDestination(animal.wonderRadius);
        animal.GotoDestination(destination);

    }

    public override void Exit()
    {
        base.Exit();
        isArrived = false;
    }
    public override void HandleInput()
    {
        isArrived = animal.IsCloseEnough(destination, 1f);
        _foundFood = animal.foundFood;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_foundFood)
        {
            stateMachine.ChangeState(animal.goForFood);
        }

        if (isArrived)
        {
            stateMachine.ChangeState(animal.idle);
        }
    }

}
