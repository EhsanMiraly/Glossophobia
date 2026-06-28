using System.Reflection.Metadata;
using System.Threading.Tasks;
using UnityEngine;

public class NPCController : MonoBehaviour, IObjectInPool
{
    [SerializeField] GameObject NPCPointPrefab;

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




    #region Movement
    private Vector2 chairLocation;
    [HideInInspector] public Vector3[] Points { get; set; }
    int currentDestinationIndex;
    Vector3 currentDestination;
    public float duration;
    private float timer = 0f;
    #endregion

    #region NPC State
    private bool goingToChair;
    private bool sitting;
    private bool goingToInitialPoint;
    #endregion


    private void OnEnable()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (goingToChair)
        {
            GoToChair();
        }
        else if (sitting)
        {
            //Sitting();
        }
        else if (goingToInitialPoint)
        {
            //GoingToInitial();
        }

        //GoForward(Points[0], Points[1]);
    }


    public void ResetNPC()
    {
        IsEnable = true;

        chairLocation = ChairsUtilities.FindRandomEmptyChairBasedOnChairPossibility();
        ChairsUtilities.UpdateChairOccupied(chairLocation, true);
        Debug.Log(chairLocation);

        Points = new Vector3[]
        {
                new Vector3(0,0,0),
                new Vector3(9,0,0),
                new Vector3(9,0,0)+new Vector3(0, 0, (int)chairLocation.x * -2),
                new Vector3(9,0,0)+new Vector3(0, 0, (int)chairLocation.x * -2) +
                 new Vector3(((int)chairLocation.y + 1) + 0.35f, 0, 0)
        };

        GeneratePointsToSee();//Delete Later

        transform.localPosition = Points[0];
        transform.localRotation = Quaternion.identity;
        transform.localRotation *= Quaternion.Euler(0, 90, 0);



        goingToChair = true;
        sitting = false;
        goingToInitialPoint = false;

        /*
        currentDestinationIndex = 0;
        currentDestination = myPoints[currentDestinationIndex];



        transform.localRotation = NavigationData.initialDirection;

        for (int i = 0; i < myPoints.Length; i++)
        {
            GameObject test = Instantiate(testPrefab, transform.parent);
            test.transform.localPosition = myPoints[i];
            test.transform.localRotation = Quaternion.identity;
        }
        */
    }

    //Delete Later
    private void GeneratePointsToSee()
    {
        for (int i = 0; i < Points.Length; i++)
        {
            GameObject point = Instantiate(NPCPointPrefab);
            point.transform.parent = this.transform.parent;
            point.transform.localPosition = Points[i];
        }
    }

    public void GoToChair()
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


    public void GoToInitialPoint()
    {





        //When Reaches initial
        ChairsUtilities.UpdateChairOccupied(chairLocation, false);
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



    //Brain and Pool Changes this;
    int id;


    AnimationEventReceiver animationEventReceiver;





    float distanceToDestination;




    private void Start()
    {
        animator = GetComponent<Animator>();
        animationEventReceiver = GetComponent<AnimationEventReceiver>();

        ResetNPCState();
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


}
