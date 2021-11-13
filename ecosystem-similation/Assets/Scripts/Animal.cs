using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Animal : MonoBehaviour
{
    #region StateVeriables
    public StateMachine movementSM;
    public Idle idle;
    public SearchForFood searchForFood;
    public GoForFood goForFood;
    public WanderAround wanderAround;
    public EatFood eatFood;
    #endregion

    #region AnimalVeraiableAttributes
    public float normalSpeed = 5f;
    public float waitTime = 1f; // wait for next casual destination
    public float wonderRadius = 15f; // wondering in a circle radius
    public Vector3 foodLocation;
    public bool foundFood;
    public bool isHungry = false;
    public GameObject targetFood;
    public GameObject foodToBeEaten;
    #endregion

    #region Attachments
    private NavMeshAgent agent;
    public GameObject indicator;
    #endregion

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        movementSM = new StateMachine();
        idle = new Idle(this, movementSM);
        wanderAround = new WanderAround(this, movementSM);
        searchForFood = new SearchForFood(this, movementSM);
        goForFood = new GoForFood(this, movementSM);
        eatFood = new EatFood(this, movementSM);

        movementSM.Initialize(idle);
    }

    void Start()
    {
        agent.speed = normalSpeed;
    }

    void Update()
    {
        movementSM.CurrentState.HandleInput();
        movementSM.CurrentState.LogicUpdate();
    }

    public bool isDestinationInvalid(Vector3 destination)
    {
        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(destination, path);

        return (path.status == NavMeshPathStatus.PathPartial);
    }

    //Creates a random valid destination with given radius
    public Vector3 CreateRandDestination(float radius)
    {
        Vector3 destination;
        do
        {
            bool signMultiplier_0 = (Random.value < 0.5);
            bool signMultiplier_1 = (Random.value < 0.5);
            bool signMultiplier_2 = (Random.value < 0.5);

            float x = Random.Range(0, radius);
            float z = Mathf.Pow(Mathf.Pow(radius, 2) - Mathf.Pow(x, 2), 0.5f);

            if (signMultiplier_0)
            {
                float temp = x;
                x = z;
                z = temp;
            }


            if (!signMultiplier_1)
                x *= -1f;
            if (!signMultiplier_2)
                z *= -1f;



            x /= Random.Range(1f, 5f);
            z /= Random.Range(1f, 5f);


            Vector3 _position = new Vector3(transform.position.x, 0, transform.position.y);
            Vector3 unrelativeDestination = new Vector3(x, 0, z);

            destination = _position + unrelativeDestination;

        } while (isDestinationInvalid(destination));


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

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            targetFood = other.gameObject;
            foodLocation = other.transform.position;
            foundFood = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            foundFood = false;
            foodToBeEaten = null;
        }
    }

}
