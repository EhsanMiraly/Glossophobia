using System.Threading.Tasks;
using UnityEngine;

public class NPCController : MonoBehaviour, IObjectInPool
{
    public bool IsEnable { get; set; }


    Animator animator;

    #region Animation Names

    private static readonly int isStandingIdle_Hash = Animator.StringToHash("isStandingIdle");
    private static readonly int isWalking_Hash = Animator.StringToHash("isWalking");
    private static readonly int isTurningRight_Hash = Animator.StringToHash("isTurningRight");
    private static readonly int isTurningLeft_Hash = Animator.StringToHash("isTurningLeft");
    private static readonly int isSittingToStanding_Hash = Animator.StringToHash("isSittingToStanding");
    private static readonly int isStandingToSitting_Hash = Animator.StringToHash("isStandingToSitting");
    private static readonly int isSittingIdle_Hash = Animator.StringToHash("isSittingIdle");

    #endregion

    [HideInInspector] public Vector3[] Points { get; set; }

    public float duration;

    private float timer = 0f;


    private void OnEnable()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        GoForward(Points[0], Points[1]);
    }


    private void SetAnimation(int animation_Hash)
    {
        animator.SetBool(isStandingIdle_Hash, false);
        animator.SetBool(isWalking_Hash, false);
        animator.SetBool(isTurningRight_Hash, false);
        animator.SetBool(isTurningLeft_Hash, false);
        animator.SetBool(isSittingToStanding_Hash, false);
        animator.SetBool(isStandingToSitting_Hash, false);
        animator.SetBool(isSittingIdle_Hash, false);

        animator.SetBool(animation_Hash, true);
    }


    public void ResetNPC()
    {
        transform.localPosition = Points[0];
        transform.localRotation = Quaternion.identity;
        transform.localRotation *= Quaternion.Euler(0, 90, 0);

        IsEnable = true;

        /*
        chairLocation = Utilities.FindRandomEmptyChairBasedOnChairPossibility();
        GameData.chairsOccupied[(int)chairLocation.x, (int)chairLocation.y] = true;
        finalDestination = Utilities.TurnEmptyChairLocationIntoGamePosition(chairLocation);

        myPoints[0] = NavigationData.initialPoint;
        myPoints[1] = NavigationData.outDoorPoint;
        myPoints[2] = NavigationData.rowsEntryPoints[(int)chairLocation.y];
        myPoints[3] = finalDestination + new Vector3(0f, 0f, 0.5f);
        myPoints[4] = finalDestination + new Vector3(0f, 0f, 2f);

        currentDestinationIndex = 0;
        currentDestination = myPoints[currentDestinationIndex];

        goingToChair = true;
        sitting = false;
        goingToInitialPoint = false;

        transform.localRotation = NavigationData.initialDirection;

        for (int i = 0; i < myPoints.Length; i++)
        {
            GameObject test = Instantiate(testPrefab, transform.parent);
            test.transform.localPosition = myPoints[i];
            test.transform.localRotation = Quaternion.identity;
        }
        */
    }

    public void GoToChair()
    {

    }

    public void GoToInitialPoint()
    {

    }


    public void GoForward(Vector3 start, Vector3 end)
    {
        duration = Vector3.Distance(Points[0], Points[1]) / 2f;

        timer += Time.deltaTime;

        float t = timer / duration;
        t = Mathf.Clamp01(t);

        transform.localPosition = Vector3.Slerp(start, end, t);

        //transform.Translate(0f, 0f, 5 * Time.deltaTime, Space.Self);//walkSpeed
        if (!animator.GetBool(isWalking_Hash))
        {
            SetAnimation(isWalking_Hash);
        }
    }


    /*
//Brain and Pool Changes this;
    bool isActive;
    int id;


    AnimationEventReceiver animationEventReceiver;



    [SerializeField] GameObject testPrefab;
    [HideInInspector] public Vector2 chairLocation = Vector2.zero;
    [HideInInspector] public Vector3 finalDestination = Vector3.zero;
    public Vector3[] myPoints = new Vector3[5];

    //General States
    public bool goingToChair;
    public bool sitting;
    public bool goingToInitialPoint;




    float walkSpeed = 6f;
    float distanceToTeleport = 0.3f;


    int currentDestinationIndex;
    Vector3 currentDestination;

    float distanceToDestination;




    private void Start()
    {
        animator = GetComponent<Animator>();
        animationEventReceiver = GetComponent<AnimationEventReceiver>();

        ResetNPCState();
    }


    void Update()
    {
        if (goingToChair)
        {
            GoingToChair();
        }
        else if (sitting)
        {
            Sitting();
        }
        else if (goingToInitialPoint)
        {
            GoingToInitial();
        }
    }



    public void GoingToChair()
    {
        if (!isAnimationBusy())
        {
            if (currentDestinationIndex == 4)
            {
                Sit();
                goingToChair = false;
                Invoke("MakeSittingStateTrue", 1f);
            }
            else
            {
                distanceToDestination = Vector3.Distance(transform.localPosition, currentDestination);

                if (distanceToDestination > distanceToTeleport)
                {
                    GoForward();
                }
                else if (distanceToDestination <= distanceToTeleport)
                {
                    transform.localPosition = myPoints[currentDestinationIndex];

                    if (currentDestinationIndex == 0)
                    {
                        currentDestination = myPoints[++currentDestinationIndex];
                    }
                    else if (currentDestinationIndex == 1)
                    {
                        if ((int)chairLocation.y == 0)
                        {
                            TurnLeft();
                        }
                        else
                        {
                            TurnRight();
                        }
                        currentDestination = myPoints[++currentDestinationIndex];
                    }
                    else if (currentDestinationIndex == 2)
                    {
                        if ((int)chairLocation.y == 0)
                        {
                            TurnRight();
                        }
                        else
                        {
                            TurnLeft();
                        }
                        currentDestination = myPoints[++currentDestinationIndex];
                    }
                    else if (currentDestinationIndex == 3)
                    {
                        TurnRight();
                        ++currentDestinationIndex;
                    }
                }
            }
        }
    }

    public void Sitting()
    {
        if (!isAnimationBusy())
        {
            ActivateAnimation(isSitedHash);
        }
    }

    public void GoingToInitial()
    {
        if (!isAnimationBusy())
        {
            if (currentDestinationIndex == 4)
            {
                Stand();
                currentDestination = myPoints[--currentDestinationIndex];
            }
            else
            {
                distanceToDestination = Vector3.Distance(transform.localPosition, currentDestination);

                if (distanceToDestination > distanceToTeleport)
                {
                    GoForward();
                }
                else if (distanceToDestination <= distanceToTeleport)
                {
                    transform.localPosition = myPoints[currentDestinationIndex];

                    if (currentDestinationIndex == 3)
                    {
                        TurnRight();
                        currentDestination = myPoints[--currentDestinationIndex];
                    }
                    else if (currentDestinationIndex == 2)
                    {
                        if ((int)chairLocation.y == 0)
                        {
                            TurnLeft();
                        }
                        else
                        {
                            TurnRight();
                        }
                        currentDestination = myPoints[--currentDestinationIndex];
                    }
                    else if (currentDestinationIndex == 1)
                    {
                        if ((int)chairLocation.y == 0)
                        {
                            TurnRight();
                        }
                        else
                        {
                            TurnLeft();
                        }
                        currentDestination = myPoints[--currentDestinationIndex];
                    }
                    else if (currentDestinationIndex == 0)
                    {
                        isActive = false;
                        this.gameObject.SetActive(false);
                        GameData.chairsOccupied[(int)chairLocation.x, (int)chairLocation.y] = false;
                        for (int i = 0; i < GameData.npcs.Count; i++)
                        {
                            if (GameData.npcs[i].GetComponent<NPCController>().id == this.id)
                            {
                                GameData.npcsPool.Add(this.gameObject);
                                GameData.npcs.RemoveAt(i);
                            }
                        }
                    }
                }
            }
        }
    }




    

    public void TurnRight()
    {
        animationEventReceiver.isTurningRight = true;
        if (!animator.GetBool(isTurningRightHash))
        {
            ActivateAnimation(isTurningRightHash);
        }
    }
    public void TurnLeft()
    {
        animationEventReceiver.isTurningLeft = true;
        if (!animator.GetBool(isTurningLeftHash))
        {
            ActivateAnimation(isTurningLeftHash);
        }
    }
    public void Sit()
    {
        if (!animator.GetBool(isSittingHash))
        {
            ActivateAnimation(isSittingHash);
        }
    }
    public void Stand()
    {
        if (!animator.GetBool(isStandingHash))
        {
            ActivateAnimation(isStandingHash);
        }
    }


    public bool isAnimationBusy()
    {
        if (!animationEventReceiver.isTurningRight && !animationEventReceiver.isTurningLeft
            && !animationEventReceiver.isSitting && !animationEventReceiver.isStanding)
        {
            return false;
        }
        else
        {
            return true;
        }
    }


    public void MakeSittingStateTrue()
    {
        sitting = true;
    }


    public bool IsActive()
    {
        return isActive;
    }
    public void ActivateNPC()
    {
        ResetNPCState();
    }
    public void DeActiveNPC()
    {
        goingToInitialPoint = true;
        goingToChair = false;
        sitting = false;
    }

    public int GetID()
    {
        return id;
    }
    public void SetID(int id)
    {
        this.id = id;
    }
    */

}
