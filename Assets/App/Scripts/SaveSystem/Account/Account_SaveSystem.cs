using System.IO;
using UnityEngine;

public class Account_SaveSystem : SaveSystem
{
    private static Account_SaveData account_SaveData = new Account_SaveData();

    public static string Account_SaveFileName()
    {
        CreateSaveDirectory();
        return saveDirectory + "/Account_SaveData" + ".txt";
    }


    public static void Save_Account()
    {
        account_SaveData.username = AccountData.currentUsername;
        account_SaveData.password = AccountData.currentPassword;

        File.WriteAllText(Account_SaveFileName(), JsonUtility.ToJson(account_SaveData, true));
    }

    public static void Load_Account()
    {
        if (!File.Exists(Account_SaveFileName()))
        {
            return;
        }

        string saveContent = File.ReadAllText(Account_SaveFileName());

        account_SaveData = JsonUtility.FromJson<Account_SaveData>(saveContent);

        AccountData.currentUsername = account_SaveData.username;
        AccountData.currentPassword = account_SaveData.password;
    }


}

[System.Serializable]

public struct Account_SaveData
{
    public string username;
    public string password;
}
