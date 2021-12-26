using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeBirth : State
{
    private int howManyChildren = 3;
    private float makingBirthTimeRate = 1f;
    private float totalBirthTime;
    private bool isBirthFinished;

    public MakeBirth(Animal animal, StateMachine stateMachine) : base(animal, stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        animal.howManyChildren = howManyChildren;
        isBirthFinished = false;
        totalBirthTime = ((float)(howManyChildren + 1) * makingBirthTimeRate);
        for (int i = 0; i < howManyChildren; i++)
        {
            float tempTime = (i + 1) * makingBirthTimeRate;
            animal.StartCoroutine("MakeBirthWithRate", tempTime);
        }
    }

    public override void Exit()
    {
        base.Exit();
        isBirthFinished = true;
        animal.isPregnant = false;
        animal.curPergnantPersentance = 0f;
        animal.StopCoroutine("MakeBirthWithRate");
    }
    public override void HandleInput()
    {
        base.HandleInput();
        totalBirthTime -= Time.deltaTime;
        isBirthFinished = (totalBirthTime <= 0);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isBirthFinished)
        {
            stateMachine.ChangeState(animal.idle);
        }
    }

}
