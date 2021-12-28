using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : State
{
    private bool isWaitTimeOver;
    private float _waitTime;
    private float maxNeedValue = 0f;
    private int maxNeedIndex = 0;

    private enum ToState
    {
        searchForFood,
        searchForWater,
        nullState,
    };

    private ToState goToState;

    public Idle(Animal animal, StateMachine stateMachine) : base(animal, stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        goToState = ToState.nullState;
        _waitTime = animal.waitTime;
        isWaitTimeOver = false;
    }

    public override void Exit()
    {
        base.Exit();
    }
    public override void HandleInput()
    {
        base.HandleInput();
        _waitTime -= Time.deltaTime;

        animal.readyToBirth = (animal.curPergnantPersentance >= 100f);

        float[] needsArray = { animal.curHunger, animal.curThirst };

        maxNeedIndex = 0;
        maxNeedValue = 0f;
        for (int i = 0; i < needsArray.Length; i++)
        {
            if (maxNeedValue < needsArray[i])
            {
                maxNeedValue = needsArray[i];
                maxNeedIndex = i;
            }
        }
        goToState = (ToState)maxNeedIndex;

        if (_waitTime <= 0f)
        {
            isWaitTimeOver = true;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isWaitTimeOver)
        {
            if (animal.readyToBirth)
            {
                stateMachine.ChangeState(animal.makeBirth);
            }
            else if (maxNeedValue >= 30f)
            {
                switch (goToState)
                {
                    case ToState.searchForFood:
                        stateMachine.ChangeState(animal.searchForFood);
                        break;
                    case ToState.searchForWater:
                        stateMachine.ChangeState(animal.searchForWater);
                        break;
                }
            }
            else if ((animal.gender == Animal.Gender.male) && (animal.curHorny >= 30f))
            {
                stateMachine.ChangeState(animal.searchForFamele);
            }

            else if (!(maxNeedValue >= 30f))
            {
                stateMachine.ChangeState(animal.wanderAround);
            }
        }
    }
}
