using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderAround : State
{
    private bool isArrived;
    private float speed;
    private FieldOfView fow;


    private Vector3 destination;

    // Start is called before the first frame update
    public WanderAround(Animal animal, StateMachine stateMachine) : base(animal, stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        fow = animal.fow;
        fow.StopAllCoroutines();
        fow.targetMask = animal.nothingMask;
        speed = animal.normalSpeed;
        destination = animal.CreateRandomDestination();
        animal.GotoDestination(destination);

    }

    public override void Exit()
    {
        base.Exit();
        isArrived = false;
    }
    public override void HandleInput()
    {
        base.HandleInput();
        isArrived = animal.IsCloseEnough(destination, 1f);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (animal.goIdle)
        {
            stateMachine.ChangeState(animal.idle);
        }

        if (isArrived)
        {
            stateMachine.ChangeState(animal.idle);
        }
    }
}
