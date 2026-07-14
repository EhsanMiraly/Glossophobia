using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PRPSA_BeforeData
{
    public static int[] currentAnswers;





    public static void InitializeAnswers()
    {
        currentAnswers = new int[20];

        for (int i = 0; i < currentAnswers.Length; i++)
        {
            currentAnswers[i] = -1;
        }
    }

    public static bool IsAllAnswersGiven()
    {
        if (currentAnswers == null)
        {
            return false;
        }

        return true;
    }

}


