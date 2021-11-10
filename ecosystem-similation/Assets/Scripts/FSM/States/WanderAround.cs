using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderAround : State
{
    private bool wander;
    private bool isArrived;
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
        destination = animal.CreateRandDestination(animal.wonderRadius);

        animal.GotoDestination(destination);

        speed = animal.normalSpeed;
        wander = true;
    }

    public override void Exit()
    {
        base.Exit();
        wander = false;
    }
    public override void HandleInput()
    {
        base.HandleInput();
        wander = !Input.GetKeyDown(KeyCode.Space);          
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        isArrived = animal.IsCloseEnough(destination, 0.1f);

        if (!wander)
        {
            stateMachine.ChangeState(animal.idle);
        }

        if (isArrived)
        {
            Debug.Log("Has Arrived");
            stateMachine.ChangeState(animal.idle);
        }
    }
}
