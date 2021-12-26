using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : State
{
    private bool isWaitTimeOver;
    private float _waitTime;
    private bool readyToDie;
    private float maxNeedValue = 0f;
    private int maxNeedIndex = 0;

    private enum ToState
    {
        searchForFood,
        searchForWater,
        searchForMate,
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
        readyToDie = false;
    }
    public override void HandleInput()
    {
        base.HandleInput();
        _waitTime -= Time.deltaTime;

        animal.readyToBirth = (animal.curPergnantPersentance >= 100f);

        float[] needsArray = { animal.curHunger, animal.curThirst, animal.curHorny };

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

        if (animal.curHunger <= 0 || animal.curThirst <= 0)
        {
            readyToDie = true;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (readyToDie)
        {
            animal.Die();
        }

        if (isWaitTimeOver)
        {
            if (animal.readyToBirth)
            {
                stateMachine.ChangeState(animal.searchForFood);
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
                    case ToState.searchForMate:
                        stateMachine.ChangeState(animal.searchForMate);
                        break;
                }
            }
            else if (!(maxNeedValue >= 30f))
            {
                stateMachine.ChangeState(animal.wanderAround);
            }
        }


        // else if (isWaitTimeOver && (animal.isHungry || animal.isThirsty || animal.isHorny || animal.readyToBirth))
        // {
        //     if (animal.readyToBirth)
        //     {
        //         stateMachine.ChangeState(animal.makeBirth);
        //     }
        //     else if (animal.isHungry && (animal.curHunger <= animal.curThirst) && (animal.curHunger <= animal.curHorny))
        //     {
        //         stateMachine.ChangeState(animal.searchForFood);
        //     }
        //     else if (animal.isThirsty && (animal.curThirst < animal.curHunger) && (animal.curThirst < animal.curHorny))
        //     {
        //         stateMachine.ChangeState(animal.searchForWater);
        //     }
        //     else if (animal.isHorny && (animal.curHorny < animal.curHunger) && (animal.curHorny < animal.curThirst) && (animal.gender == Animal.Gender.male))
        //     {
        //         stateMachine.ChangeState(animal.searchForMate);
        //     }
        //     else
        //     {
        //         Debug.LogError("Logic Error");
        //     }
        // }

    }
}
