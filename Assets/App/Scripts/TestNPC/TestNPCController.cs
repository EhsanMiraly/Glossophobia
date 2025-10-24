using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class TestNPCController : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    private Vector2 myChairLocation = Vector2.zero;



    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        myChairLocation = GameData.FindRandomEmptyChair();

        navMeshAgent.destination = GameData.TurnEmptyChairLocationIntoGamePosition(myChairLocation,
        transform.parent.position);

        GameData.chairsOccupied[(int)myChairLocation.x, (int)myChairLocation.y] = true;
    }


    void Update()
    {
        if (navMeshAgent.enabled)
        {
            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    navMeshAgent.isStopped = true;      // متوقف کن
                    navMeshAgent.velocity = Vector3.zero; // سرعت رو صفر کن

                    navMeshAgent.transform.rotation = navMeshAgent.transform.rotation * Quaternion.Euler(0, 90, 0);
                    navMeshAgent.enabled = false;
                    navMeshAgent.transform.position =
                    GameData.TurnEmptyChairLocationIntoGamePosition(myChairLocation,
                    transform.parent.position) + new Vector3(0f, 0.1f, +0.75f);
                }
            }
        }


    }

}
