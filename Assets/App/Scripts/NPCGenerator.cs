using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;
using Unity.AI;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using System.Collections;

public class NPCGenerator : MonoBehaviour
{
    [SerializeField] GameObject npcPrefab;


    void Start()
    {
        for (int i = 0; i < GameData.chairsXAmount * GameData.chairsZAmount; i++)
        {
            GameObject npc = Instantiate(npcPrefab, transform);
            npc.transform.localPosition = Vector3.zero;
            npc.transform.localRotation = Quaternion.identity;

            //npc.SetActive(false);

            GameData.npcs[i] = npc;
        }
    }

}
