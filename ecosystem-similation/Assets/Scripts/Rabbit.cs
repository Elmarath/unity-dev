using UnityEngine;
using UnityEngine.AI;

public class Rabbit : MonoBehaviour
{
    [SerializeField]
    private float tolerance;
    private Vector3 Destination;
    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Destination = new Vector3(Random.Range(-15, 15), 0, Random.Range(-15, 15)) + transform.position;
    }


    private void Update()
    {
        agent.Move(Destination);
    }

    private void FoolAround(float radius)
    {
        // create a destination with given radius
    }

    private bool IsCloseEnough(float tolerance)
    {
        if (Vector3.Distance(Destination, transform.position) < tolerance)
        {
            return false;
        }
        else
        {
            return true;
        }



    }
}