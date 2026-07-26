using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Firebase.Auth;
using Firebase.Firestore;
using System.Reflection.Metadata.Ecma335;
using System;
using Firebase;

public class FireStoreManager
{




    /*
    #region Utilities

    private async void CheckAndFixDependencies()
    {
        DependencyStatus status =
            await FirebaseApp.CheckAndFixDependenciesAsync();

        if (status == DependencyStatus.Available)
        {
            Debug.Log("Firebase initialized successfully.");
        }
        else
        {
            Debug.LogError($"Could not resolve Firebase dependencies: {status}");
        }
    }


    private static DocumentReference GetPlayerDocument()
    {
        if (FirebaseAuth.DefaultInstance.CurrentUser == null)
        {
            return null;
        }

        return FirebaseFirestore.DefaultInstance.Collection(players_Collection).
                Document(FirebaseAuth.DefaultInstance.CurrentUser.UserId);
    }

    #endregion


    

    public static async Task SaveDemographics(Demographics demographics)
    {
        DocumentReference playerDocument = GetPlayerDocument();

        if (playerDocument == null)
        {
            return;
        }

        Dictionary<string, object> demographics_Dictionary = new Dictionary<string, object>()
            {
                { genderIndex, demographics.genderIndex },
                { ageGroupIndex, demographics.ageGroupIndex },
                { educationLevelIndex, demographics.educationLevelIndex },
                { fieldOfStudyIndex, demographics.fieldOfStudyIndex },
                { jobIndex, demographics.jobIndex },
                { levelOfExperienceIndex, demographics.levelOfExperienceIndex },
                { levelOfNeedIndex, demographics.levelOfNeedIndex },
                { levelOfAnxietyIndex, demographics.levelOfAnxietyIndex },
                { formalTrainingIndex, demographics.formalTrainingIndex },
                { takingMedicationIndex, demographics.takingMedicationIndex },
                { games3DIndex, demographics.games3DIndex },
                { simulationGamesIndex, demographics.simulationGamesIndex }
            };

        Dictionary<string, object> update = new Dictionary<string, object>()
            {
                { demographics_Map, demographics_Dictionary }
            };

        await playerDocument.SetAsync(update, SetOptions.MergeAll);
    }


    public static async Task<Demographics> LoadDemographics()
    {
        DocumentReference playerDocument = GetPlayerDocument();

        if (playerDocument == null)
        {
            return new Demographics();
        }

        DocumentSnapshot snapshot = await playerDocument.GetSnapshotAsync();

        if (!snapshot.Exists)
        {
            return new Demographics();
        }

        Dictionary<string, object> data = snapshot.ToDictionary();

        if (!data.ContainsKey(demographics_Map))
        {
            return new Demographics();
        }

        Dictionary<string, object> demographics = data[demographics_Map] as Dictionary<string, object>;

        Demographics result = new Demographics
                (
                Convert.ToInt32(demographics[genderIndex]),
                Convert.ToInt32(demographics[ageGroupIndex]),
                Convert.ToInt32(demographics[educationLevelIndex]),
                Convert.ToInt32(demographics[fieldOfStudyIndex]),
                Convert.ToInt32(demographics[jobIndex]),
                Convert.ToInt32(demographics[levelOfExperienceIndex]),
                Convert.ToInt32(demographics[levelOfNeedIndex]),
                Convert.ToInt32(demographics[levelOfAnxietyIndex]),
                Convert.ToInt32(demographics[formalTrainingIndex]),
                Convert.ToInt32(demographics[takingMedicationIndex]),
                Convert.ToInt32(demographics[games3DIndex]),
                Convert.ToInt32(demographics[simulationGamesIndex])
                );

        return result;
    }

    #endregion
    */
}
