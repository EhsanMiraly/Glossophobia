using System.Reflection.Metadata;
using System.Threading.Tasks;
using UnityEngine;

public class NPCController : MonoBehaviour, IObjectInPool
{
    [SerializeField] GameObject NPCPointPrefab;

    public bool IsEnable { get; set; }


    Animator animator;
    NPC_AnimationEventReceiver npc_AnimationEventReceiver;


    #region Animation Names

    private static readonly int isStandingIdle_Hash = Animator.StringToHash("isStandingIdle");
    private static readonly int isWalking_Hash = Animator.StringToHash("isWalking");
    private static readonly int isTurningRight_Hash = Animator.StringToHash("isTurningRight");
    private static readonly int isTurningLeft_Hash = Animator.StringToHash("isTurningLeft");
    private static readonly int isSittingToStanding_Hash = Animator.StringToHash("isSittingToStanding");
    private static readonly int isStandingToSitting_Hash = Animator.StringToHash("isStandingToSitting");
    private static readonly int isSittingIdle_Hash = Animator.StringToHash("isSittingIdle");

    #endregion


    #region NPC State
    private bool goingToChair;
    private bool sitting;
    private bool goingToInitialPoint;
    #endregion


    #region Movement

    private Vector2 chairLocation;
    [HideInInspector] public Vector3[] Points { get; set; }
    int currentDestinationIndex;
    float distanceToDestination;
    float distanceToTeleport = 0.01f;
    public float duration;
    private float timer = 0f;

    #endregion




    private void OnEnable()
    {
        animator = GetComponentInChildren<Animator>();
        npc_AnimationEventReceiver = GetComponentInChildren<NPC_AnimationEventReceiver>();
    }

    private void Update()
    {
        if (goingToChair)
        {
            GoToChair();
        }
        else if (sitting)
        {
            Sitting();
        }
        else if (goingToInitialPoint)
        {
            GoToInitialPoint();
        }
    }


    public void ResetNPC()
    {
        IsEnable = true;
        this.gameObject.SetActive(true);

        chairLocation = ChairsUtilities.FindRandomEmptyChairBasedOnChairPossibility();
        ChairsUtilities.UpdateChairOccupied(chairLocation, true);
        Debug.Log(chairLocation);

        Points = new Vector3[]
        {
                new Vector3(0,0,0),
                new Vector3(9,0,0),
                new Vector3(9,0,0)+new Vector3(0, 0, (int)chairLocation.x * -2),
                new Vector3(9,0,0)+new Vector3(0, 0, (int)chairLocation.x * -2) +
                                    new Vector3(((int)chairLocation.y + 1) + 0.35f, 0, 0),
                new Vector3(9,0,0)+new Vector3(0, 0, (int)chairLocation.x * -2) +
                                    new Vector3(((int)chairLocation.y + 1) + 0.35f, 0, 0)+new Vector3(0,0,1)
        };

        GeneratePointsToSee();//Delete Later

        transform.localPosition = Points[0];
        transform.localRotation = Quaternion.identity;
        transform.localRotation *= Quaternion.Euler(0, 90, 0);



        goingToChair = true;
        sitting = false;
        goingToInitialPoint = false;


        currentDestinationIndex = 1;
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
                distanceToDestination = Vector3.Distance(transform.localPosition, Points[currentDestinationIndex]);

                if (distanceToDestination > distanceToTeleport)
                {
                    GoForward(Points[currentDestinationIndex - 1], Points[currentDestinationIndex]);
                }
                else
                {
                    transform.localPosition = Points[currentDestinationIndex];
                    timer = 0f;

                    if (currentDestinationIndex == 1)
                    {
                        if ((int)chairLocation.x != 0)
                        {
                            TurnRight();
                        }
                        currentDestinationIndex++;
                    }
                    else if (currentDestinationIndex == 2)
                    {
                        if ((int)chairLocation.x != 0)
                        {
                            TurnLeft();
                        }
                        currentDestinationIndex++;
                    }
                    else if (currentDestinationIndex == 3)
                    {
                        TurnRight();
                        currentDestinationIndex++;
                    }
                }
            }
        }
    }

    public void Sitting()
    {
        if (!isAnimationBusy())
        {
            SetAnimation(isSittingIdle_Hash);
        }
    }

    public void GoToInitialPoint()
    {
        if (!isAnimationBusy())
        {
            if (currentDestinationIndex == 4)
            {
                Stand();
                --currentDestinationIndex;
            }
            else
            {
                distanceToDestination = Vector3.Distance(transform.localPosition, Points[currentDestinationIndex]);

                if (distanceToDestination > distanceToTeleport)
                {
                    GoForward(Points[currentDestinationIndex + 1], Points[currentDestinationIndex]);
                }
                else
                {
                    transform.localPosition = Points[currentDestinationIndex];

                    if (currentDestinationIndex == 3)
                    {
                        TurnRight();
                        currentDestinationIndex--;
                    }
                    else if (currentDestinationIndex == 2)
                    {
                        if ((int)chairLocation.x != 0)
                        {
                            TurnRight();
                        }
                        currentDestinationIndex--;
                    }
                    else if (currentDestinationIndex == 1)
                    {
                        if ((int)chairLocation.x != 0)
                        {
                            TurnLeft();
                        }
                        currentDestinationIndex--;
                    }
                    else if (currentDestinationIndex == 0)
                    {
                        IsEnable = false;
                        this.gameObject.SetActive(false);
                        ChairsUtilities.UpdateChairOccupied(chairLocation, false);
                    }
                }
            }
        }
    }


    public bool isAnimationBusy()
    {
        if (!npc_AnimationEventReceiver.isTurningRight && !npc_AnimationEventReceiver.isTurningLeft &&
            !npc_AnimationEventReceiver.isSitting && !npc_AnimationEventReceiver.isStanding)
        {
            return false;
        }
        else
        {
            return true;
        }
    }


    public void GoForward(Vector3 start, Vector3 end)
    {
        if (!animator.GetBool(isWalking_Hash))
        {
            duration = Vector3.Distance(start, end) / 2f;
            SetAnimation(isWalking_Hash);
        }

        timer += Time.deltaTime;

        float t = timer / duration;
        t = Mathf.Clamp01(t);

        transform.localPosition = Vector3.Slerp(start, end, t);
    }

    public void TurnRight()
    {
        npc_AnimationEventReceiver.isTurningRight = true;
        if (!animator.GetBool(isTurningRight_Hash))
        {
            SetAnimation(isTurningRight_Hash);
        }
    }

    public void TurnLeft()
    {
        npc_AnimationEventReceiver.isTurningLeft = true;
        if (!animator.GetBool(isTurningLeft_Hash))
        {
            SetAnimation(isTurningLeft_Hash);
        }
    }

    public void Sit()
    {
        if (!animator.GetBool(isSittingIdle_Hash))
        {
            SetAnimation(isSittingIdle_Hash);
        }
    }

    public void Stand()
    {
        if (!animator.GetBool(isStandingIdle_Hash))
        {
            SetAnimation(isStandingIdle_Hash);
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


    public void MakeSittingStateTrue()
    {
        sitting = true;
    }

}
