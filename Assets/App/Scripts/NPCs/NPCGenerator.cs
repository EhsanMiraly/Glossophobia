using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPCGenerator : MonoBehaviour
{
    [SerializeField] GameObject NPCPointPrefab;
    [SerializeField] List<GameObject> NPCsPrefabs;
    //IsUsed????
    private Pool<NPCController> npcsPool;


    #region Points

    Vector3 initialPoint;
    Vector3 inClassPoint;
    Vector3[] rowEntryPoints;
    Vector3[,] rowColomnPoints;

    #endregion



    private void OnEnable()
    {
        npcsPool = new Pool<NPCController>(5);//Change Later


        GeneratePoints();
        GeneratePointsToSee();

        SubscribeToEvents();
    }

    private void OnDisable()
    {
        UnSubscribeToEvents();
    }


    #region Points

    private void GeneratePoints()
    {
        initialPoint = new Vector3(0, 0, 0);
        inClassPoint = new Vector3(9, 0, 0);

        rowEntryPoints = new Vector3[5];

        rowColomnPoints = new Vector3[5, 10];

        for (int i = 0; i < 5; i++)
        {
            rowEntryPoints[i] = inClassPoint + new Vector3(0, 0, i * -2);
        }

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                rowColomnPoints[i, j] = rowEntryPoints[i] + new Vector3((j + 1) + 0.35f, 0, 0);
            }
        }

    }

    private void GeneratePointsToSee()
    {
        GameObject initialPoint_GO = Instantiate(NPCPointPrefab);
        initialPoint_GO.transform.parent = this.transform;
        initialPoint_GO.transform.localPosition = initialPoint;

        for (int i = 0; i < 5; i++)
        {
            GameObject point = Instantiate(NPCPointPrefab);
            point.transform.parent = this.transform;
            point.transform.localPosition = rowEntryPoints[i];
        }

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                GameObject point = Instantiate(NPCPointPrefab);
                point.transform.parent = this.transform;
                point.transform.localPosition = rowColomnPoints[i, j];
            }
        }
    }

    #endregion

    #region Events

    private void SubscribeToEvents()
    {
        EventsManager.OnAddNPC_Event += AddNPC;
        EventsManager.OnAddNPC_Event += RemoveNPC;
    }

    private void UnSubscribeToEvents()
    {
        EventsManager.OnAddNPC_Event -= AddNPC;
        EventsManager.OnAddNPC_Event -= RemoveNPC;
    }

    private void AddNPC()
    {
        if (npcsPool.IsThereDisabledGameObjectInPool())
        {
            GameObject npc = npcsPool.GetGameObjectFromPool();
            NPCController npcController = npc.GetComponent<NPCController>();
            npcController.ResetNPC();
            npcController.GoToChair();
        }
        else if (!npcsPool.IsPoolFull())
        {
            GameObject npc = Instantiate(NPCsPrefabs[RandomNPCPrefabIndex()]);
            npcsPool.AddToPool(npc);
            npc.transform.parent = this.transform;
            NPCController npcController = npc.GetComponent<NPCController>();
            npcController.Points = new Vector3[]//Select Random
            {
                initialPoint,
                inClassPoint,
                rowEntryPoints[3],
                rowColomnPoints[3,5]
            };
            npcController.ResetNPC();
            npcController.GoToChair();
        }
        else
        {
            Debug.Log("Pool is fUll!!!");
        }
    }

    private void RemoveNPC()
    {
        if (npcsPool.IsThereEnabledGameObjectInPool())
        {
            GameObject npc = npcsPool.GetGameObjectFromPool(RandomEnabledNPCIndex());
            NPCController npcController = npc.GetComponent<NPCController>();
            npcController.GoToInitialPoint();
        }
    }


    #endregion


    private int RandomNPCPrefabIndex()
    {
        int index = Random.Range(0, NPCsPrefabs.Count);
        return index;
    }

    private int RandomEnabledNPCIndex()
    {
        int numberOfActiveNPCs = 0;

        for (int i = 0; i < npcsPool.CurrentObjectsInPool; i++)
        {
            if (npcsPool.PoolList[i].GetComponent<NPCController>().IsEnable)
            {
                numberOfActiveNPCs++;
            }
        }

        int index = Random.Range(0, numberOfActiveNPCs);
        index++;

        for (int i = 0; i < npcsPool.CurrentObjectsInPool; i++)
        {
            if (npcsPool.PoolList[i].GetComponent<NPCController>().IsEnable)
            {
                index--;
                if (index == 0)
                {
                    return i;
                }
            }
        }

        return -1;
    }

}
