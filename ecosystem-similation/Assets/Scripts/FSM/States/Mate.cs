using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mate : State
{
    private float matingDuration;
    public Mate(Animal animal, StateMachine stateMachine) : base(animal, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        animal.isMating = true;
        matingDuration = 5f;
        animal.StartCoroutine("FinishMating", matingDuration);
    }

    public override void Exit()
    {
        base.Exit();
        if (animal.gender == Animal.Gender.female)
        {
            animal.isPregnant = true;
        }
        animal.previousMate = animal.foundedMate;
        animal.StartCoroutine("ResetPreviousMate");
        animal.isMating = false;
        animal.foundedMate.isMating = false;
        animal.curHorny = 0f;
    }
    public override void HandleInput()
    {
        base.HandleInput();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!animal.isMating)
        {
            stateMachine.ChangeState(animal.idle);
        }

        if (animal.goIdle)
        {
            stateMachine.ChangeState(animal.idle);
        }
    }
}
