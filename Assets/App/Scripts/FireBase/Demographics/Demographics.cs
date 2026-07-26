using System;


[Serializable]
public class Demographics
{
    public int genderIndex;
    public int ageGroupIndex;
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


    public Demographics()
    {
        genderIndex = -1;
        ageGroupIndex = -1;
        educationLevelIndex = -1;
        fieldOfStudyIndex = -1;
        jobIndex = -1;
        levelOfExperienceIndex = -1;
        levelOfNeedIndex = -1;
        levelOfAnxietyIndex = -1;
        formalTrainingIndex = -1;
        takingMedicationIndex = -1;
        games3DIndex = -1;
        simulationGamesIndex = -1;
    }

    public Demographics(int genderIndex, int ageGroupIndex, int educationLevelIndex,
                        int fieldOfStudyIndex, int jobIndex, int levelOfExperienceIndex,
                        int levelOfNeedIndex, int levelOfAnxietyIndex, int formalTrainingIndex,
                        int takingMedicationIndex, int games3DIndex, int simulationGamesIndex)
    {
        this.genderIndex = genderIndex;
        this.ageGroupIndex = ageGroupIndex;
        this.educationLevelIndex = educationLevelIndex;
        this.fieldOfStudyIndex = fieldOfStudyIndex;
        this.jobIndex = jobIndex;
        this.levelOfExperienceIndex = levelOfExperienceIndex;
        this.levelOfNeedIndex = levelOfNeedIndex;
        this.levelOfAnxietyIndex = levelOfAnxietyIndex;
        this.formalTrainingIndex = formalTrainingIndex;
        this.takingMedicationIndex = takingMedicationIndex;
        this.games3DIndex = games3DIndex;
        this.simulationGamesIndex = simulationGamesIndex;
    }


    public bool IsEveryThingSet()
    {
        if (genderIndex != -1 && ageGroupIndex != -1 && educationLevelIndex != -1 &&
            fieldOfStudyIndex != -1 && jobIndex != -1 && levelOfExperienceIndex != -1 &&
            levelOfNeedIndex != -1 && levelOfAnxietyIndex != -1 &&
            formalTrainingIndex != -1 && takingMedicationIndex != -1 &&
            games3DIndex != -1 && simulationGamesIndex != -1)
        {
            return true;
        }

        return false;
    }
}
