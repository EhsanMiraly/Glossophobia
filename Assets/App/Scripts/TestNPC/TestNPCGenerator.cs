using System.Collections;
using UnityEngine;

public class TestNPCGenerator : MonoBehaviour
{
    [SerializeField] GameObject npcPrefab;

    bool timeToMakeNPC = true;
    int baseIndex = 0;


    void Update()
    {
        if (timeToMakeNPC)
        {
            StartCoroutine(MakeNPC());
        }
    }

    IEnumerator MakeNPC()
    {
        timeToMakeNPC = false;

        if (GameData.isThereEmptyChair())
        {
            GameObject npc = Instantiate(npcPrefab, transform);
            npc.transform.localPosition = Vector3.zero;
            npc.transform.localRotation = Quaternion.identity;
            //npc.SetActive(false);
            GameData.npcs[baseIndex] = npc;
            baseIndex++;
        }

        yield return new WaitForSeconds(1f);

        timeToMakeNPC = true;
    }
}
