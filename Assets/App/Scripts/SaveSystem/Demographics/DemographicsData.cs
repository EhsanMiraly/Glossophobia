using System.Collections.Generic;

public class DemographicsData
{
    public static int currentGenderIndex = 0; //Change
    public static int currentAge = 20;//Change
    public static int currentEducationLevelIndex = -1;
    public static int currentFieldOfStudyIndex = -1;
    public static int currentJobIndex = -1;
    public static int currentLevelOfExperienceIndex = -1;
    public static int currentLevelOfNeedIndex = -1;
    public static int currentLevelOfAnxietyIndex = -1;
    public static int currentFormalTrainingIndex = -1;
    public static int currentTakingMedicationIndex = -1;
    public static int currentGames3DIndex = -1;
    public static int currentSimulationGamesIndex = -1;


    public static bool IsEveryThingSet()
    {
        if (currentGenderIndex != -1 && currentAge != -1 && currentEducationLevelIndex != -1 &&
            currentFieldOfStudyIndex != -1 && currentJobIndex != -1 && currentLevelOfExperienceIndex != -1 &&
            currentLevelOfNeedIndex != -1 && currentLevelOfAnxietyIndex != -1 &&
            currentFormalTrainingIndex != -1 && currentTakingMedicationIndex != -1 &&
            currentGames3DIndex != -1 && currentSimulationGamesIndex != -1)
        {
            return true;
        }

        return false;
    }
}
