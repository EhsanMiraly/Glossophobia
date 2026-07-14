using UnityEngine;
using System.IO;

public class PRPSA_Before_SaveSystem : SaveSystem
{
    private static PRPSA_Before_SaveData PRPSA_Before_SaveData = new PRPSA_Before_SaveData();

    public static string PRPSA_Before_SaveFileName()
    {
        CreateSaveDirectory();
        return saveDirectory + "/PRPSA_Before_SaveData" + ".txt";
    }

    public static void Save_PRPSA_Before()
    {
        PRPSA_Before_SaveData.answers = PRPSA_BeforeData.currentAnswers;

        File.WriteAllText(PRPSA_Before_SaveFileName(), JsonUtility.ToJson(PRPSA_Before_SaveData, true));
    }

    public static void Load_PRPSA_Before()
    {
        if (!File.Exists(PRPSA_Before_SaveFileName()))
        {
            return;
        }

        string saveContent = File.ReadAllText(PRPSA_Before_SaveFileName());

        PRPSA_Before_SaveData = JsonUtility.FromJson<PRPSA_Before_SaveData>(saveContent);

        PRPSA_BeforeData.currentAnswers = PRPSA_Before_SaveData.answers;
    }

}



[System.Serializable]
public struct PRPSA_Before_SaveData
{
    public int[] answers;
}
