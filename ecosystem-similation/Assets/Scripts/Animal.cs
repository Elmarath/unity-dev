using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Animal : MonoBehaviour 
{
    #region StateVeriables
    public StateMachine movementSM;
    public Idle idle;
    public WanderAround wanderAround;
    #endregion

    #region AnimalVeraiableAttributes
    public float normalSpeed = 5f;
    public float waitTime = 1f; // wait for next casual destination
    public float wonderRadius = 15f; // wondering in a circle radius
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


    //Creates a random valid destination with given radius
    public Vector3 CreateRandDestination(float radius)
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

        Vector3 destination = _position + unrelativeDestination;

        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(destination, path);

        if (path.status == NavMeshPathStatus.PathPartial) // if not reachable
        {
            CreateRandDestination(radius);
        }

        Instantiate(indicator, destination, this.transform.rotation);

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
}
