using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Animal : MonoBehaviour
{
    #region AnimalVeraiableAttributes
    public float normalSpeed = 5f;
    public float waitTime = 1f; // wait for next casual destination
    [Range(1, 5)]
    public float minSearchDistance = 2f;
    public float maxHunger = 100f;
    public float maxThirst = 100f;
    public float maxReproduceUrge = 100f;
    [Range(0.25f, 10)]
    public float gettingHungryRate = 2f; // deletes x point per sec
    [Range(0.25f, 10)]
    public float gettingThirstyRate = 3.5f;
    [Range(0.25f, 10)]
    public float gettingHornyRate = 0.75f;
    public float gettingFullMultiplier = 30f;
    public float drinkingRate = 30f;
    #endregion

    #region LayerMasks
    public LayerMask foodMask;
    public LayerMask waterMask;
    public LayerMask rabbitMask;
    public LayerMask obstacleMask;
    #endregion

    #region StateVeriables
    [HideInInspector]
    public StateMachine movementSM;
    [HideInInspector]
    public Idle idle;
    [HideInInspector]
    public SearchForFood searchForFood;
    [HideInInspector]
    public SearchForWater searchForWater;
    [HideInInspector]
    public GoForFood goForFood;
    [HideInInspector]
    public GoForWater goForWater;
    [HideInInspector]
    public GoForMate goForMate;
    [HideInInspector]
    public WanderAround wanderAround;
    [HideInInspector]
    public EatFood eatFood;
    [HideInInspector]
    public DrinkWater drinkWater;
    [HideInInspector]
    public SearchForMate searchForMate;
    #endregion

    #region StateChangeVeriables
    [HideInInspector]
    public bool isHungry = false;
    [HideInInspector]
    public bool isThirsty = false;
    [HideInInspector]
    public bool isHorny = false;
    [HideInInspector]
    public bool goIdle;
    [HideInInspector]
    public GameObject foundedFood;
    [HideInInspector]
    public GameObject foundedWater;
    [HideInInspector]
    public GameObject foundedMate;
    #endregion

    #region FieldOfView
    [HideInInspector]
    public float viewRadius;
    [HideInInspector]
    public float viewAngle;
    [HideInInspector]
    public FieldOfView fow;
    [HideInInspector]
    public GameObject foundTarget;
    #endregion

    #region Attachments
    private NavMeshAgent agent;
    private HungerBar hungerBar;
    private ThirstBar thirstBar;
    private ReproduceUrgeBar reproduceUrgeBar;
    public GameObject indicator;
    #endregion

    #region animalSurvivalVariables
    public float curHunger;
    public float curThirst;
    public float curHorny;
    #endregion 

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        fow = GetComponent<FieldOfView>();
        fow.selfRef = gameObject;
        viewRadius = fow.viewRadius;
        viewAngle = fow.viewAngle;
        movementSM = new StateMachine();
        idle = new Idle(this, movementSM);
        wanderAround = new WanderAround(this, movementSM);
        searchForFood = new SearchForFood(this, movementSM);
        searchForWater = new SearchForWater(this, movementSM);
        searchForMate = new SearchForMate(this, movementSM);
        goForFood = new GoForFood(this, movementSM);
        goForWater = new GoForWater(this, movementSM);
        goForMate = new GoForMate(this, movementSM);
        eatFood = new EatFood(this, movementSM);
        drinkWater = new DrinkWater(this, movementSM);

        curHunger = maxHunger;
        curThirst = maxThirst;
        curHorny = maxReproduceUrge;

        //UI
        hungerBar = GetComponentInChildren<HungerBar>();
        thirstBar = GetComponentInChildren<ThirstBar>();
        reproduceUrgeBar = GetComponentInChildren<ReproduceUrgeBar>();

        movementSM.Initialize(idle);
    }

    void Start()
    {
        agent.speed = normalSpeed;

        //UI elements
        hungerBar.SetMaxHunger(maxHunger);
        thirstBar.SetMaxThirst(maxThirst);
        reproduceUrgeBar.SetMaxReproduceUrge(maxReproduceUrge);
    }

    void Update()
    {
        //update SurvivalVariables
        curHunger -= Time.deltaTime * gettingHungryRate;
        curThirst -= Time.deltaTime * gettingThirstyRate;
        curHorny -= Time.deltaTime * gettingHornyRate;

        //Update UI elements
        hungerBar.SetThirst(curHunger);
        thirstBar.SetThirst(curThirst);
        reproduceUrgeBar.SetReproduceUrge(curHorny);

        // update states
        movementSM.CurrentState.HandleInput();
        movementSM.CurrentState.LogicUpdate();
    }

    public Vector3 CreateRandomDestination()
    {
        Vector3 origin = transform.position;
        Vector3 dirToTarget;
        float distance = viewRadius;
        Vector3 destination;
        NavMeshPath path = new NavMeshPath();
        bool angleCheck;
        bool distanceCheck;
        bool isValid;

        int i = 0;
        do
        {
            i++;
            destination = UnityEngine.Random.insideUnitSphere * distance;
            destination.y = 0;
            destination += origin;
            // check if destination valid
            isValid = agent.CalculatePath(destination, path);

            dirToTarget = (destination - transform.position).normalized;
            angleCheck = (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2);
            distanceCheck = (Vector3.Distance(destination, origin) > minSearchDistance); // min distance  
        } while (!angleCheck && !distanceCheck && i < 30 && !isValid);

        dirToTarget = (destination - transform.position).normalized;
        Debug.Log(Vector3.Angle(transform.forward, dirToTarget));

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

    public Vector3 CreateRandomValidPoint(Vector3 center, float radius)
    {
        Vector3 returnedRandomPoint;
        float x;
        float z;
        bool change;
        bool isValid = false;
        NavMeshPath path = new NavMeshPath();

        int i = 0;

        do
        {
            do
            {
                x = Random.Range(0.001f, radius);
                z = Mathf.Sqrt(radius - Mathf.Pow(x, 2f)); // may return NaN values
            } while (float.IsNaN(z));
            change = (Random.Range(0.001f, 1f) <= 0.5f);
            if (change)
            {
                float temp;
                temp = x;
                x = z;
                z = temp;
            }
            change = (Random.Range(0f, 1f) <= 0.5f);
            if (change)
            {
                x *= -1;
            }
            change = (Random.Range(0f, 1f) <= 0.5f);
            if (change)
            {
                z *= -1;
            }
            Debug.Log("x: " + x);
            Debug.Log("z: " + z);
            returnedRandomPoint = center + new Vector3(x, 0f, z);
            isValid = agent.CalculatePath(returnedRandomPoint, path);
            if (isValid)
            {
                break;
            }
            i++;
        } while (!isValid && i <= 30);

        if (i > 30)
        {
            Debug.Log("All founded points are not valid");
        }
        else
        {
            Debug.Log("Founded some points");
            return returnedRandomPoint;
        }
        return Vector3.negativeInfinity;
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }

    IEnumerator ReturnToIdleAfter15Sec()
    {
        yield return new WaitForSeconds(15f);
        goIdle = true;
    }
}
