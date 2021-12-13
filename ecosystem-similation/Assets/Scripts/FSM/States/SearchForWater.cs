using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchForWater : State
{
    private Vector3 destination;
    private Vector3 _foodLocation;
    private FieldOfView fow;
    private GameObject foundedWater;
    private float _speed;
    private bool isArrived;
    private bool isFoodAvalible;
    private bool isFoodStatedToBeEaten;

    public SearchForWater(Animal animal, StateMachine stateMachine) : base(animal, stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entered Search For water state");
        fow = animal.fow;
        fow.StopAllCoroutines();
        animal.fow.targetMask = animal.waterMask;
        animal.fow.StartCoroutine("FindTargetsWithDelay", .2f);
        _speed = animal.normalSpeed;
        destination = animal.CreateRandomDestination();
        animal.GotoDestination(destination);
    }

    public override void Exit()
    {
        base.Exit();
        animal.fow.StopCoroutine("FindTargetsWithDelay");
        animal.foundedWater = foundedWater;
        isArrived = false;
    }
    public override void HandleInput()
    {
        isArrived = animal.IsCloseEnough(destination, 1f);
        foundedWater = fow.returnedGameObject;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (foundedWater)
        {
            stateMachine.ChangeState(animal.goForWater);
        }

        if (isArrived)
        {
            stateMachine.ChangeState(animal.idle);
        }
    }
}
