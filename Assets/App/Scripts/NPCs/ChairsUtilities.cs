using UnityEngine;

public class ChairsUtilities
{
    public static int chairsRowAmount = 5;
    public static int chairsColomnAmount = 10;

    public static bool[,] chairsOccupied = new bool[chairsRowAmount, chairsColomnAmount];

    public static Vector2 centerOfGravity = new Vector2(4, 5);
    public static float[] chairsRowPossibility = new float[chairsRowAmount];
    public static float[] chairsColomnPossibility = new float[chairsColomnAmount];


    public static float chairsXApart = 1f;
    public static float chairsZApart = 2f;
    public static float floorThicness = 0.15f;



    public static bool isThereEmptyChair()
    {
        bool answer = false;

        for (int i = 0; i < chairsRowAmount; i++)
        {
            for (int j = 0; j < chairsColomnAmount; j++)
            {
                if (chairsOccupied[i, j] == false)
                {
                    answer = true;
                }
            }
        }

        return answer;
    }


    public static void UpdateChairPossibility()
    {
        for (int i = 0; i < chairsRowAmount; i++)
        {
            chairsRowPossibility[i] = chairsRowAmount - Mathf.Abs(centerOfGravity.x - i);
        }

        for (int i = 0; i < chairsColomnAmount; i++)
        {
            chairsColomnPossibility[i] = chairsColomnAmount - Mathf.Abs(centerOfGravity.y - i);
        }
    }

    public static Vector2 FindRandomEmptyChairBasedOnChairPossibility()
    {
        Vector2 result = new Vector2(-1, -1);

        float possibilitySum = 0;

        for (int i = 0; i < chairsRowAmount; i++)
        {
            for (int j = 0; j < chairsColomnAmount; j++)
            {
                if (chairsOccupied[i, j] == false)
                {
                    possibilitySum += (chairsRowPossibility[i] + chairsColomnPossibility[j]);
                }
            }
        }

        float chairLocation = Random.Range(0, possibilitySum);

        for (int i = 0; i < chairsRowAmount; i++)
        {
            for (int j = 0; j < chairsColomnAmount; j++)
            {
                if (chairsOccupied[i, j] == false)
                {
                    chairLocation -= (chairsRowPossibility[i] + chairsColomnPossibility[j]);
                    if (chairLocation <= 0)
                    {
                        result = new Vector2(i, j);
                        return result;
                    }
                }
            }
        }

        return result;
    }

    public static void UpdateChairOccupied(Vector2 chair, bool state)
    {
        chairsOccupied[(int)chair.x, (int)chair.y] = state;
    }

}
