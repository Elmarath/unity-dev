using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Animal : MonoBehaviour
{
    public enum Gender
    {
        male,
        female,
        baby
    };

    #region AnimalVeraiableAttributes
    public float normalSpeed = 5f;
    public float waitTime = 0.6f; // wait for next casual destination
    [Range(1, 5)]
    public float minSearchDistance = 2f;
    public float maxHunger = 100f;
    public float maxThirst = 100f;
    public float maxReproduceUrge = 60f;
    [Range(0.25f, 10)]
    public float gettingHungryRate = 2f; // deletes x point per sec
    [Range(0.25f, 10)]
    public float gettingThirstyRate = 3.5f;
    [Range(0f, 10)]
    public float gettingHornyRate = 0.75f;
    [Range(0.25f, 10)]
    public float pregnantTimeRate = 10f;
    public float gettingFullMultiplier = 30f;
    public float drinkingRate = 30f;
    #endregion

    #region LayerMasks
    public LayerMask foodMask;
    public LayerMask waterMask;
    public LayerMask rabbitMask;
    public LayerMask obstacleMask;
    public LayerMask nothingMask;
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
    public GoForFamele goForFamele;
    [HideInInspector]
    public WaitForMale waitForMale;
    [HideInInspector]
    public WanderAround wanderAround;
    [HideInInspector]
    public Mate mate;
    public EatFood eatFood;
    [HideInInspector]
    public DrinkWater drinkWater;
    [HideInInspector]
    public SearchForFamele searchForFamele;
    [HideInInspector]
    public MakeBirth makeBirth;
    #endregion

    #region StateChangeVeriables
    [HideInInspector]
    public bool isHorny = false;
    [HideInInspector]
    public bool goIdle = false;
    [HideInInspector]
    public bool readyToBirth = false;
    [HideInInspector]
    public bool readyToGoForMate = false;
    [HideInInspector]
    public bool readyToWaitForMale = false;
    [HideInInspector]
    public GameObject foundedFood;
    [HideInInspector]
    public GameObject foundedWater;
    [HideInInspector]
    public Animal candidateMate;
    [HideInInspector]
    public Animal foundedMate;
    [HideInInspector]
    public Vector3 matingGround;
    [HideInInspector]
    public Animal previousMate;
    [HideInInspector]
    public bool isMating;
    public bool isPregnant;
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
    private CurrentStateTextUI textUI;
    [HideInInspector]
    public Slider hungerBarSlider;
    [HideInInspector]
    public Slider thirstBarSlider;
    [HideInInspector]
    public Slider reproduceUrgeBarSlider;
    [HideInInspector]
    public Text currentStateTextUIText;
    [HideInInspector]
    public string causeOfDeath;
    public GameObject indicator;
    public BabyRabbitData babyRabbitData;
    #endregion

    #region animalSurvivalVariables
    public int howManyChildren = 3;
    public float curHunger;
    public float curThirst;
    public float curHorny;
    public float curPergnantPersentance;
    #endregion

    public Gender gender;

    private void Awake()
    {
        StopAllCoroutines();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = normalSpeed;
        fow = GetComponent<FieldOfView>();
        fow.selfRef = gameObject;
        viewRadius = fow.viewRadius;
        viewAngle = fow.viewAngle;
        movementSM = new StateMachine();
        idle = new Idle(this, movementSM);
        wanderAround = new WanderAround(this, movementSM);
        searchForFood = new SearchForFood(this, movementSM);
        searchForWater = new SearchForWater(this, movementSM);
        searchForFamele = new SearchForFamele(this, movementSM);
        goForFood = new GoForFood(this, movementSM);
        goForWater = new GoForWater(this, movementSM);
        goForFamele = new GoForFamele(this, movementSM);
        waitForMale = new WaitForMale(this, movementSM);
        mate = new Mate(this, movementSM);
        eatFood = new EatFood(this, movementSM);
        drinkWater = new DrinkWater(this, movementSM);
        makeBirth = new MakeBirth(this, movementSM);

        curHunger = 0f;
        curThirst = 0f;
        curHorny = 0f;

        gender = (Gender)Random.Range(0, 2);
        agent.speed = normalSpeed;
        previousMate = null;

        //UI elements
        hungerBarSlider = transform.GetChild(0).GetChild(0).GetComponent<Slider>();
        thirstBarSlider = transform.GetChild(0).GetChild(1).GetComponent<Slider>();
        reproduceUrgeBarSlider = transform.GetChild(0).GetChild(2).GetComponent<Slider>();
        currentStateTextUIText = transform.GetChild(0).GetChild(3).GetComponent<Text>();

        hungerBar = GetComponent<HungerBar>();
        thirstBar = GetComponent<ThirstBar>();
        reproduceUrgeBar = GetComponent<ReproduceUrgeBar>();
        textUI = GetComponent<CurrentStateTextUI>();

        hungerBar._init_(hungerBarSlider);
        thirstBar._init_(thirstBarSlider);
        reproduceUrgeBar._init_(reproduceUrgeBarSlider);
        textUI._init_(currentStateTextUIText);

        hungerBar.SetMaxHunger(maxHunger);
        thirstBar.SetMaxThirst(maxThirst);
        reproduceUrgeBar.SetMaxReproduceUrge(maxReproduceUrge);

        //initialize
        movementSM.Initialize(idle);
    }

    void Update()
    {
        //update SurvivalVariables
        if (curHunger <= maxHunger)
        {
            curHunger += Time.deltaTime * gettingHungryRate;
        }
        else
        {
            causeOfDeath = "Hunger";
            Die();
        }

        if (curThirst <= maxThirst)
        {
            curThirst += Time.deltaTime * gettingThirstyRate;
        }
        else
        {
            causeOfDeath = "Thirst";
            Die();
        }

        if (curHorny <= maxReproduceUrge)
        {
            curHorny += Time.deltaTime * gettingHornyRate;
        }
        if (isPregnant)
        {
            curPergnantPersentance += Time.deltaTime * pregnantTimeRate;
        }

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
            return returnedRandomPoint;
        }
        return Vector3.negativeInfinity;
    }

    public void Die()
    {
        Debug.Log("Couse of death: " + causeOfDeath);
        Destroy(this.gameObject);
    }

    public IEnumerator ReturnToIdleAfter15Sec()
    {
        yield return new WaitForSeconds(15f);
        goIdle = true;
    }

    private IEnumerator ResetPreviousMate()
    {
        yield return new WaitForSeconds(30f);
        previousMate = null;
    }

    public IEnumerator MakeBirthWithRate(float delay)
    {
        yield return new WaitForSeconds(delay);
        Instantiate(babyRabbitData.babyRabbit, transform.position, transform.rotation);
    }

    IEnumerator FinishMating(float matingDuration)
    {
        yield return new WaitForSeconds(matingDuration);
        isMating = false;
    }

    public void UpdateTextUI(string updatedText)
    {
        textUI.SetStateText(updatedText);
    }

    public Vector3 DecideMatingGround()
    {
        Vector3 matingGround = CreateRandomValidPoint(transform.position, 1f);
        return matingGround;
    }

    public bool ValidateMatingCandidate(Animal candidateMale)
    {
        // if valid
        // if not previously mated for 90sec
        if (candidateMale != previousMate)
        {
            foundedMate = candidateMale;
            previousMate = foundedMate;
            StartCoroutine("ResetPreviousMate");
            movementSM.ChangeState(waitForMale);
            return true;
        }
        return false;
    }


}
