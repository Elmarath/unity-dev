using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchForFood : State
{
    private Vector3 destination;
    private Vector3 _foodLocation;
    private FieldOfView fow;
    private GameObject foundedFood;
    private Food _foundFoodInst;
    private float _speed;
    private bool isArrived;
    private bool isFoodAvalible;

    public SearchForFood(Animal animal, StateMachine stateMachine) : base(animal, stateMachine)
    {

    }

    public override void Enter()
    {
        Debug.Log("Searching for food!!");

        base.Enter();
        fow = animal.fow;
        animal.fow.targetMask = animal.foodMask;
        animal.fow.StartCoroutine("FindTargetsWithDelay", .2f);
        _speed = animal.normalSpeed;
        destination = animal.CreateRandDestination(animal.wonderRadius);
        animal.GotoDestination(destination);
    }

    public override void Exit()
    {
        base.Exit();
        animal.fow.StopCoroutine("FindTargetsWithDelay");
        animal.foundedFood = foundedFood;
        isArrived = false;
    }
    public override void HandleInput()
    {
        isArrived = animal.IsCloseEnough(destination, 1f);
        foundedFood = fow.returnedGameObject;
        if (foundedFood)
        {
            isFoodAvalible = foundedFood.GetComponent<Food>().isEatable;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (foundedFood && isFoodAvalible)
        {
            stateMachine.ChangeState(animal.goForFood);
        }

        if (isArrived)
        {
            stateMachine.ChangeState(animal.idle);
        }
    }

}
