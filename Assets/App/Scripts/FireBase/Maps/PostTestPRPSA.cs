using System;


[Serializable]
public class PostTestPRPSA
{
    public int[] postTestPRPSAIndexes = new int[LanguageTextsData.postTestPRPSAQuestions.Count];

    public PostTestPRPSA()
    {
        for (int i = 0; i < postTestPRPSAIndexes.Length; i++)
        {
            postTestPRPSAIndexes[i] = -1;
        }
    }

    public PostTestPRPSA(int[] postTestPRPSAIndexes)
    {
        for (int i = 0; i < this.postTestPRPSAIndexes.Length; i++)
        {
            this.postTestPRPSAIndexes[i] = postTestPRPSAIndexes[i];
        }
    }

    public bool IsEveryThingSet()
    {
        for (int i = 0; i < postTestPRPSAIndexes.Length; i++)
        {
            if (postTestPRPSAIndexes[i] == -1)
            {
                return false;
            }
        }

        return true;
    }
}
