using System;


[Serializable]
public class GameSession
{
    public int numberOfCurrentGameSession;



    public int[] postTestPRPSAIndexes = new int[LanguageTextsData.postTestPRPSAQuestions.Count];


    public GameSession()
    {
        numberOfCurrentGameSession = 0;




        for (int i = 0; i < postTestPRPSAIndexes.Length; i++)
        {
            postTestPRPSAIndexes[i] = -1;
        }
    }


    public GameSession(int numberOfCurrentGameSession, int[] postTestPRPSAIndexes)
    {
        this.numberOfCurrentGameSession = numberOfCurrentGameSession;


        for (int i = 0; i < this.postTestPRPSAIndexes.Length; i++)
        {
            this.postTestPRPSAIndexes[i] = postTestPRPSAIndexes[i];
        }
    }


    public bool IsEveryThingSet()
    {
        bool IsEveryThingSet = true;

        if (numberOfCurrentGameSession == 0)
        {
            IsEveryThingSet = false;
        }


        for (int i = 0; i < postTestPRPSAIndexes.Length; i++)
        {
            if (postTestPRPSAIndexes[i] == -1)
            {
                IsEveryThingSet = false;
            }
        }

        return IsEveryThingSet;
    }


}
