using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoForWater : State
{
    private bool isArrived;
    private Vector3 _waterLocation;
    private GameObject foundedWater;

    public GoForWater(Animal animal, StateMachine stateMachine) : base(animal, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("In GoForWaterState");
        foundedWater = animal.foundedWater;
        _waterLocation = animal.foundedWater.transform.position;
        //stoping at desired distance
        _waterLocation = _waterLocation + (animal.transform.position - _waterLocation).normalized * 2f;
        animal.GotoDestination(_waterLocation);
    }

    public override void Exit()
    {
        base.Exit();
    }
    public override void HandleInput()
    {
        isArrived = animal.IsCloseEnough(_waterLocation, 2f);
    }

    public override void LogicUpdate()
    {
        if(isArrived){
            stateMachine.ChangeState(animal.drinkWater);
        }
    }

}
