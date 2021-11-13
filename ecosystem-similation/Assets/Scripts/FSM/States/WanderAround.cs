using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderAround : State
{
    private bool wander;
    private bool isArrived;
    private bool _isHungry;
    private float speed;


    private Vector3 destination;

    // Start is called before the first frame update
    public WanderAround(Animal animal, StateMachine stateMachine) : base(animal, stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        // Set a destination
        _isHungry = animal.isHungry;
        speed = animal.normalSpeed;
        destination = animal.CreateRandDestination(animal.wonderRadius);
        animal.GotoDestination(destination);

        wander = true;

    }

    public override void Exit()
    {
        base.Exit();
        isArrived = false;
        wander = false;
    }
    public override void HandleInput()
    {
        base.HandleInput();
        isArrived = animal.IsCloseEnough(destination, 0.1f);
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
