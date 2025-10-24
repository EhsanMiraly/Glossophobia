using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    private Vector2 myChairLocation = Vector2.zero;
    private Animator animator;



    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        myChairLocation = GameData.FindRandomEmptyChair();

        navMeshAgent.destination = GameData.TurnEmptyChairLocationIntoGamePosition(myChairLocation,
        transform.parent.position);
        //+ new Vector3(0f, 0f, 1f);

        GameData.chairsOccupied[(int)myChairLocation.x, (int)myChairLocation.y] = true;
    }


    void Update()
    {
        if (navMeshAgent.enabled)
        {
            animator.SetBool("isWalking", navMeshAgent.velocity.magnitude > 0.1f);

            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    navMeshAgent.isStopped = true;      // متوقف کن
                    navMeshAgent.velocity = Vector3.zero; // سرعت رو صفر کن
                    animator.SetBool("isWalking", false);

                    navMeshAgent.transform.rotation = Quaternion.identity;
                    animator.SetBool("isSitting", true);
                    navMeshAgent.enabled = false;
                    navMeshAgent.transform.position =
                    GameData.TurnEmptyChairLocationIntoGamePosition(myChairLocation,
                    transform.parent.position) + new Vector3(0f, 0.1f, -0.75f);
                }
            }
        }


    }


    void OnAnimatorMove()
    {
        if (animator.GetBool("isWalking"))
        {
            navMeshAgent.speed = (animator.deltaPosition / Time.deltaTime).magnitude;
        }
    }


}
