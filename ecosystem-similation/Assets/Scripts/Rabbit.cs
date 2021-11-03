using UnityEngine;
using UnityEngine.AI;

public class Rabbit : MonoBehaviour
{
    [SerializeField]
    private float tolerance;
    [SerializeField]
    private GameObject indicator;

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
            // find a new destination after few seconds
            // TODO: wait for 2 seconds
            destination = CreateRandDestination(20);
            Vector3 dir = destination - transform.position;
            Debug.DrawRay(transform.position, dir, Color.red, 10f, false);
        }
        
    }

   

    private void GotoDestination(Vector3 destination)
    {
        agent.destination = destination;
    }

    private void FoolAround(float radius)
    {
        // 

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
        float x = Random.Range(0, radius);
        float z = Mathf.Pow(Mathf.Pow(radius, 2) - Mathf.Pow(x, 2), 0.5f);
        bool signMultiplier_1 = (Random.value < 0.5);
        bool signMultiplier_2 = (Random.value < 0.5);
        if (!signMultiplier_1)
            x *= -1f;
        if (!signMultiplier_2)
            z *= -1f;
        x /= Random.Range(1f, 3f);
        z /= Random.Range(1f, 3f);

        Vector3 newDestination = new Vector3(x, 0, z);
        NavMeshHit hit;
        if (NavMesh.SamplePosition(newDestination, out hit, 1f, NavMesh.AllAreas)) // if destination is on navmesh
        {
            return newDestination;
        }
        CreateRandDestination(radius);

        return Vector3.zero;
                  
    }

}