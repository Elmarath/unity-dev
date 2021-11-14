using UnityEngine;
using UnityEngine.AI;

public class Rabbit : MonoBehaviour
{
    [SerializeField]
    private float tolerance;

    public GameObject Indicator;

    private bool readyToMoveNextLoc;
    private Vector3 destination;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        destination = CreateRandDestination(15);
    }

    private void Update()
    {

        if (!IsCloseEnough(tolerance))
        {
            // TODO: if not reached in 15 seconds find a new destination
            GotoDestination(destination);
        } else
        {
            FoolAround(20);
        }
        
    }

   

    private void GotoDestination(Vector3 n_destination)
    {
        destination = n_destination;
        Vector3 dir = destination - transform.position;
        Debug.DrawRay(transform.position, dir, Color.red, 0.1f, false);
        agent.SetDestination(destination);
    }

    private void FoolAround(float radius)
    {
        destination = CreateRandDestination(radius);
        
    }

    private bool IsReadyToMoveNextLocation()
    {

        return true;
    }

    private bool IsCloseEnough(float tolerance)
    {
        if (Vector3.Distance(destination, transform.position) < tolerance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Creates a random destination with given radius (y = 0)
    private Vector3 CreateRandDestination(float radius)
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

        x /= Random.Range(1f, 3f);
        z /= Random.Range(1f, 3f);

        destination = new Vector3(x, 0, z) +
            new Vector3(transform.position.x, 0, transform.position.y) +
            new Vector3(transform.forward.x * radius / 2f, 0
            , transform.forward.z * radius / 2f);

        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(destination, path);

        if (path.status == NavMeshPathStatus.PathPartial) // if not reachable
        {
            CreateRandDestination(radius);
        }

        return destination;

    }


    private void GoForFood(Transform food)
    {
        Instantiate(Indicator, food.transform);
        destination = new Vector3(food.transform.position.x, 0, food.transform.position.z);
    }

    private void EatFood()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        //Check to see if the tag on the collider is equal to Enemy 
        if (other.tag == "Food")
        {
            GoForFood(other.GetComponentInParent<Transform>());
            Debug.Log("Triggered by Food");
        }
        if (other.tag == "Water")
        {
            Debug.Log("Triggered by Water");
        }
    }
}