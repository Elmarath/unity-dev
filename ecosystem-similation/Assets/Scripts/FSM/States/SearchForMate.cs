using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchForMate : State
{
    private Vector3 destination;
    private FieldOfView fow;
    private Animal foundedMate;
    private bool isArrived;
    private bool readyToMate;

    public SearchForMate(Animal animal, StateMachine stateMachine) : base(animal, stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();

        fow = animal.fow;
        fow.StopAllCoroutines();
        animal.fow.targetMask = animal.rabbitMask;
        animal.fow.StartCoroutine("FindTargetsWithDelay", .2f);
        destination = animal.CreateRandomDestination();
        animal.GotoDestination(destination);
    }

    public override void Exit()
    {
        base.Exit();
        animal.foundedMate = foundedMate;
        isArrived = false;
        readyToMate = false;
    }
    public override void HandleInput()
    {
        base.HandleInput();

        isArrived = animal.IsCloseEnough(destination, 1f);
        GameObject foundedMateObj = fow.returnedGameObject;

        if (foundedMateObj)
        {
            foundedMate = foundedMateObj.GetComponent<Animal>();
            Debug.Log(foundedMate.name);
            if ((foundedMate.gender != Animal.Gender.baby) && (foundedMate.gender != animal.gender))
            {
                // if founded mate agrees : 
                Vector3 matingGround = animal.DecideMatingGround();
                readyToMate = true;
            }
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (animal.goIdle)
        {
            stateMachine.ChangeState(animal.idle);
        }

        if (readyToMate)
        {
            foundedMate.readyToGoForMate = true;
            Debug.Log("Waiting for mate!");
            //stateMachine.ChangeState(animal.waitForMate);
        }

        if (isArrived)
        {
            stateMachine.ChangeState(animal.idle);
        }
    }
}
