using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchForFamele : State
{
    private Vector3 destination;
    private FieldOfView fow;
    private bool goForMate;
    private bool isArrived;

    public SearchForFamele(Animal animal, StateMachine stateMachine) : base(animal, stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();

        fow = animal.fow;
        goForMate = false;
        fow.StopAllCoroutines();
        animal.fow.targetMask = animal.rabbitMask;
        animal.fow.StartCoroutine("FindTargetsWithDelay", .2f);
        destination = animal.CreateRandomDestination();
        animal.GotoDestination(destination);
    }

    public override void Exit()
    {
        base.Exit();
        isArrived = false;
        if (animal.foundedMate)
        {
            animal.foundedMate.readyToWaitForMale = true;
            animal.foundedMate.goIdle = true;
        }

    }
    public override void HandleInput()
    {
        base.HandleInput();

        isArrived = animal.IsCloseEnough(destination, 1f);
        GameObject foundedMateObj = null;
        foundedMateObj = fow.returnedGameObject;

        if (foundedMateObj)
        {
            animal.foundedMate = foundedMateObj.GetComponentInParent<Animal>();
            if ((animal.foundedMate.gender == Animal.Gender.female))
            {
                animal.foundedMate.candidateMate = animal;

                if (animal.foundedMate.ValidateMatingCandidate(animal) && (animal.foundedMate != animal.previousMate))
                {
                    goForMate = true;
                }
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

        if (goForMate)
        {
            stateMachine.ChangeState(animal.goForFamele);
        }

        if (isArrived)
        {
            stateMachine.ChangeState(animal.idle);
        }
    }
}
