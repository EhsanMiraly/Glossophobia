using UnityEngine;
using System.IO;

public class Settings_SaveSystem : SaveSystem
{
    private static Settings_SaveData settings_SaveData = new Settings_SaveData();

    public static string Settings_SaveFileName()
    {
        CreateSaveDirectory();
        return saveDirectory + "/Settings_SaveData" + ".txt";
    }

    public static void Save_Settings()
    {
        settings_SaveData.currentLanguageIndex = SettingsData.currentLanguageIndex;
        settings_SaveData.currentFontSizeIndex = SettingsData.currentFontSizeIndex;

        settings_SaveData.soundVolume = SettingsData.soundVolume;

        File.WriteAllText(Settings_SaveFileName(), JsonUtility.ToJson(settings_SaveData, true));
    }

    public static void Load_Settings()
    {
        if (!File.Exists(Settings_SaveFileName()))
        {
            return;
        }

        string saveContent = File.ReadAllText(Settings_SaveFileName());

        settings_SaveData = JsonUtility.FromJson<Settings_SaveData>(saveContent);

        SettingsData.currentLanguageIndex = settings_SaveData.currentLanguageIndex;
        SettingsData.currentFontSizeIndex = settings_SaveData.currentFontSizeIndex;

        SettingsData.soundVolume = settings_SaveData.soundVolume;
    }

}



[System.Serializable]
public struct Settings_SaveData
{
    public int currentLanguageIndex;
    public int currentFontSizeIndex;

    public float soundVolume;
}
