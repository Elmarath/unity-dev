using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchForFood : State
{
    private Vector3 destination;
    private float _speed;
    private bool isArrived;
    private bool isEating;

    public SearchForFood(Animal animal, StateMachine stateMachine) : base(animal, stateMachine)
    {

    }

    public override void Enter()
    {
        Debug.Log("sdjkfjkdfjkfdjkfd");
        base.Enter();
        destination = animal.CreateRandDestination(animal.wonderRadius);
        animal.GotoDestination(destination);
        _speed = animal.normalSpeed;
    }

    public override void Exit()
    {
        base.Exit();
        isArrived = false;
    }
    public override void HandleInput()
    {
        isArrived = animal.IsCloseEnough(destination, 1f);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isArrived)
        {
            stateMachine.ChangeState(animal.idle);
        }
    }
}
