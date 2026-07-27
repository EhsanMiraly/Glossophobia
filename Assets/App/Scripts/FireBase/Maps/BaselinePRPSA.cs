using System;


[Serializable]
public class BaselinePRPSA
{
    public int[] baselinePRPSAIndexes = new int[LanguageTextsData.baselinePRPSAQuestions.Count];

    public BaselinePRPSA()
    {
        for (int i = 0; i < baselinePRPSAIndexes.Length; i++)
        {
            baselinePRPSAIndexes[i] = -1;
        }
    }

    public BaselinePRPSA(int[] baselinePRPSAIndexes)
    {
        for (int i = 0; i < this.baselinePRPSAIndexes.Length; i++)
        {
            this.baselinePRPSAIndexes[i] = baselinePRPSAIndexes[i];
        }
    }

    public bool IsEveryThingSet()
    {
        for (int i = 0; i < baselinePRPSAIndexes.Length; i++)
        {
            if (baselinePRPSAIndexes[i] == -1)
            {
                return false;
            }
        }

        return true;
    }
}
