using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameData
{
    public static int chairsXAmount = 10;
    public static int chairsZAmount = 5;
    public static int chairsXApart = 2;
    public static int chairsZApart = 4;
    public static int chairsXBaseDistance = 2;
    public static int chairsZzBaseDistance = 2;
    public static float floarThicness = 0.15f;

    public static bool[,] chairsOccupied = new bool[chairsXAmount, chairsZAmount];
    public static GameObject[] npcs = new GameObject[chairsXAmount * chairsZAmount];


    public static bool isThereEmptyChair()
    {
        bool answer = false;

        for (int i = 0; i < chairsXAmount; i++)
        {
            for (int j = 0; j < chairsZAmount; j++)
            {
                if (chairsOccupied[i, j] == false)
                {
                    answer = true;
                }
            }
        }

        return answer;
    }

    public static Vector2 FindRandomEmptyChair()
    {
        Vector2 empty = Vector2.zero;

        int emptyCount = 0;

        for (int i = 0; i < chairsXAmount; i++)
        {
            for (int j = 0; j < chairsZAmount; j++)
            {
                if (chairsOccupied[i, j] == false)
                {
                    emptyCount++;
                }
            }
        }

        int index = Random.Range(0, emptyCount);

        for (int i = 0; i < chairsXAmount; i++)
        {
            for (int j = 0; j < chairsZAmount; j++)
            {
                if (chairsOccupied[i, j] == false)
                {
                    if (index == 0)
                    {
                        empty = new Vector2(i, j);
                    }
                    index--;
                }
            }
        }

        return empty;
    }

    public static Vector3 TurnEmptyChairLocationIntoGamePosition(Vector2 chairLocation, Vector3 parentPosition)
    {
        //0,0 -> 3,0,2
        //1,1 -> 31.0.-2
        Vector3 position = new Vector3(-(chairLocation.x * 2f) + 33f, 0.15f, -(chairLocation.y * 4f) + 2);
        //Debug.Log(chairLocation + " and " + position);

        return parentPosition + position;
    }
}
