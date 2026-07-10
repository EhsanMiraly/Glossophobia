using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Demographics_SaveSystem : SaveSystem
{
    private static Demographics_SaveData demographics_SaveData = new Demographics_SaveData();


    public static string Demographics_SaveFileName()
    {
        CreateSaveDirectory();
        return saveDirectory + "/Demographics_SaveData" + ".txt";
    }

    public static void Save_Demographics()
    {
        demographics_SaveData.genderIndex = DemographicsData.currentGenderIndex;
        demographics_SaveData.age = DemographicsData.currentAge;
        demographics_SaveData.educationLevelIndex = DemographicsData.currentEducationLevelIndex;
        demographics_SaveData.fieldOfStudyIndex = DemographicsData.currentFieldOfStudyIndex;
        demographics_SaveData.jobIndex = DemographicsData.currentJobIndex;
        demographics_SaveData.levelOfExperienceIndex = DemographicsData.currentLevelOfExperienceIndex;
        demographics_SaveData.levelOfNeedIndex = DemographicsData.currentLevelOfAnxietyIndex;
        demographics_SaveData.levelOfAnxietyIndex = DemographicsData.currentLevelOfAnxietyIndex;
        demographics_SaveData.formalTrainingIndex = DemographicsData.currentFormalTrainingIndex;
        demographics_SaveData.takingMedicationIndex = DemographicsData.currentTakingMedicationIndex;
        demographics_SaveData.games3DIndex = DemographicsData.currentGames3DIndex;
        demographics_SaveData.simulationGamesIndex = DemographicsData.currentSimulationGamesIndex;

        File.WriteAllText(Demographics_SaveFileName(), JsonUtility.ToJson(demographics_SaveData, true));
    }

    public static void Load_Demographics()
    {
        if (!File.Exists(Demographics_SaveFileName()))
        {
            return;
        }

        string saveContent = File.ReadAllText(Demographics_SaveFileName());

        demographics_SaveData = JsonUtility.FromJson<Demographics_SaveData>(saveContent);

        DemographicsData.currentGenderIndex = demographics_SaveData.genderIndex;
        DemographicsData.currentAge = demographics_SaveData.age;
        DemographicsData.currentEducationLevelIndex = demographics_SaveData.educationLevelIndex;
        DemographicsData.currentFieldOfStudyIndex = demographics_SaveData.fieldOfStudyIndex;
        DemographicsData.currentJobIndex = demographics_SaveData.jobIndex;
        DemographicsData.currentLevelOfExperienceIndex = demographics_SaveData.levelOfExperienceIndex;
        DemographicsData.currentLevelOfAnxietyIndex = demographics_SaveData.levelOfNeedIndex;
        DemographicsData.currentLevelOfAnxietyIndex = demographics_SaveData.levelOfAnxietyIndex;
        DemographicsData.currentFormalTrainingIndex = demographics_SaveData.formalTrainingIndex;
        DemographicsData.currentTakingMedicationIndex = demographics_SaveData.takingMedicationIndex;
        DemographicsData.currentGames3DIndex = demographics_SaveData.games3DIndex;
        DemographicsData.currentSimulationGamesIndex = demographics_SaveData.simulationGamesIndex;
    }

}


[System.Serializable]
public struct Demographics_SaveData
{
    public int genderIndex;
    public int age;
    public int educationLevelIndex;
    public int fieldOfStudyIndex;
    public int jobIndex;
    public int levelOfExperienceIndex;
    public int levelOfNeedIndex;
    public int levelOfAnxietyIndex;
    public int formalTrainingIndex;
    public int takingMedicationIndex;
    public int games3DIndex;
    public int simulationGamesIndex;
}