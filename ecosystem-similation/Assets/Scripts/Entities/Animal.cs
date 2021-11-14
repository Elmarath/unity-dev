using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Animal : MonoBehaviour
{
    #region AnimalVeraiableAttributes
    public float normalSpeed = 5f;
    public float waitTime = 1f; // wait for next casual destination
    [Range(1, 100)]
    public float viewRadius = 10f;
    [Range(0, 360)]
    public float viewAngle = 100f;
    [Range(1, 5)]
    public float minSearchDistance = 2f;
    public float maxHunger = 100f;
    [Range(0.25f, 10)]
    public float gettingHungryRate = 2f; // deletes x point per sec
    public float gettingFullMultiplier = 30f;
    #endregion

    #region 
    public LayerMask foodMask;
    public LayerMask obstacleMask;
    #endregion

    #region StateVeriables
    [HideInInspector]
    public StateMachine movementSM;
    [HideInInspector]
    public Idle idle;
    public SearchForFood searchForFood;
    [HideInInspector]
    public GoForFood goForFood;
    [HideInInspector]
    public WanderAround wanderAround;
    [HideInInspector]
    public EatFood eatFood;
    #endregion

    #region StateChangeVeriables
    [HideInInspector]
    public bool isHungry = false;
    [HideInInspector]
    public GameObject foundedFood;
    #endregion

    #region FieldOfView
    [HideInInspector]
    public FieldOfView fow;
    [HideInInspector]
    public GameObject foundTarget;
    #endregion

    #region Attachments
    private NavMeshAgent agent;
    private HungerBar hungerBar;
    public GameObject indicator;
    #endregion

    #region animalSurvivalVariables
    public float curHunger;
    #endregion 

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        fow = GetComponent<FieldOfView>();
        fow.viewRadius = viewRadius;
        fow.viewAngle = viewAngle;
        movementSM = new StateMachine();
        idle = new Idle(this, movementSM);
        wanderAround = new WanderAround(this, movementSM);
        searchForFood = new SearchForFood(this, movementSM);
        goForFood = new GoForFood(this, movementSM);
        eatFood = new EatFood(this, movementSM);

        curHunger = maxHunger;

        //UI
        hungerBar = GetComponentInChildren<HungerBar>();

        movementSM.Initialize(idle);
    }

    void Start()
    {
        agent.speed = normalSpeed;

        //UI elements
        hungerBar.SetMaxHunger(maxHunger);
    }

    void Update()
    {
        //update SurvivalVariables
        curHunger -= Time.deltaTime * gettingHungryRate;

        //Update UI elements
        hungerBar.SetHealth(curHunger);

        movementSM.CurrentState.HandleInput();
        movementSM.CurrentState.LogicUpdate();
    }

    public Vector3 CreateRandomDestination()
    {
        Vector3 origin = transform.position;
        Vector3 dirToTarget;
        float distance = viewRadius;
        int layermask = 1;
        Vector3 destination;
        NavMeshHit navHit;
        bool angleCheck;
        bool distanceCheck;

        int i = 0;
        do
        {
            i++;
            Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;
            randomDirection.y = 0;
            randomDirection += origin;

            NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);
            destination = navHit.position;

            dirToTarget = (destination - transform.position).normalized;
            angleCheck = Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2;
            distanceCheck = Vector3.Distance(destination, origin) > minSearchDistance; // min distance  
        } while (!angleCheck && !distanceCheck && i < 30);

        if (i >= 30)
        {
            destination = transform.position - transform.forward * 5;
        }
        return destination;
    }

    public void GotoDestination(Vector3 destination)
    {
        Vector3 dir = destination - transform.position;
        Debug.DrawRay(transform.position, dir, Color.red, 2f, false);
        agent.SetDestination(destination);
    }

    public bool IsCloseEnough(Vector3 destination, float tolerance)
    {
        Vector3 _position = new Vector3(transform.position.x, 0, transform.position.z);
        return Vector3.Distance(destination, _position) < tolerance;
    }

    public void Die()
    {
        Debug.Log("Died!!");
        Destroy(this.gameObject);
    }

}
