using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPCGenerator : MonoBehaviour
{
    [SerializeField] GameObject NPCPointPrefab;
    [SerializeField] List<GameObject> NPCsPrefabs;//Add a List to see witch one is used to generate ne one
    private Pool<NPCController> npcsPool;



    private void OnEnable()
    {
        npcsPool = new Pool<NPCController>(50);//Change Later

        SubscribeToEvents();
    }

    private void OnDisable()
    {
        UnSubscribeToEvents();
    }


    #region Events

    private void SubscribeToEvents()
    {
        EventsManager.OnAddNPC_Event += AddNPC;
        EventsManager.OnRemoveNPC_Event += RemoveNPC;
    }

    private void UnSubscribeToEvents()
    {
        EventsManager.OnAddNPC_Event -= AddNPC;
        EventsManager.OnRemoveNPC_Event -= RemoveNPC;
    }

    private void AddNPC()
    {
        if (!ChairsUtilities.isThereEmptyChair())
        {
            Debug.Log("Chairs are fUll!!!");
            return;
        }

        if (npcsPool.IsThereDisabledGameObjectInPool())
        {
            GameObject npc = npcsPool.GetGameObjectFromPool();
            NPCController npcController = npc.GetComponent<NPCController>();
            npcController.ResetNPC();
        }
        else if (!npcsPool.IsPoolFull())
        {
            GameObject npc = Instantiate(NPCsPrefabs[RandomNPCPrefabIndex()],
                this.transform.position, Quaternion.identity);
            npc.transform.parent = this.transform;
            npc.transform.localPosition = Vector3.zero;
            npcsPool.AddToPool(npc);
            NPCController npcController = npc.GetComponent<NPCController>();
            npcController.ResetNPC();
        }
        else
        {
            Debug.Log("Pool is fUll!!!");
            return;
        }
    }

    private void RemoveNPC()
    {
        if (npcsPool.IsThereEnabledGameObjectInPool())
        {
            GameObject npc = npcsPool.GetGameObjectFromPool(RandomEnabledNPCIndex());
            NPCController npcController = npc.GetComponent<NPCController>();
            npcController.TimeToLeave();
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
