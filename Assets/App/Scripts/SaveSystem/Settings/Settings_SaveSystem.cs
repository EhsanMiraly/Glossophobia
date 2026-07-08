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
        settings_SaveData.sawWelcome = SettingsData.currentSawWelcome;
        settings_SaveData.sawLaptopHint = SettingsData.currentSawLaptopHint;

        settings_SaveData.languageIndex = SettingsData.currentLanguageIndex;
        settings_SaveData.fontSizeIndex = SettingsData.currentFontSizeIndex;

        settings_SaveData.soundVolume = SettingsData.currentSoundVolume;

        settings_SaveData.targetFrameRateIndex = SettingsData.currentTargetFrameRateIndex;
        settings_SaveData.fieldOfViewIndex = SettingsData.currentFieldOfViewIndex;

        settings_SaveData.moveSpeed = SettingsData.currentMoveSpeed;
        settings_SaveData.horizontalSensitivity = SettingsData.currentHorizontalSensitivity;
        settings_SaveData.verticalSensitivity = SettingsData.currentVerticalSensitivity;

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

        SettingsData.currentSawWelcome = settings_SaveData.sawWelcome;
        SettingsData.currentSawLaptopHint = settings_SaveData.sawLaptopHint;

        SettingsData.currentLanguageIndex = settings_SaveData.languageIndex;
        SettingsData.currentFontSizeIndex = settings_SaveData.fontSizeIndex;

        SettingsData.currentSoundVolume = settings_SaveData.soundVolume;

        SettingsData.currentTargetFrameRateIndex = settings_SaveData.targetFrameRateIndex;
        SettingsData.currentFieldOfViewIndex = settings_SaveData.fieldOfViewIndex;

        SettingsData.currentMoveSpeed = settings_SaveData.moveSpeed;
        SettingsData.currentHorizontalSensitivity = settings_SaveData.horizontalSensitivity;
        SettingsData.currentVerticalSensitivity = settings_SaveData.verticalSensitivity;
    }

}



[System.Serializable]
public struct Settings_SaveData
{
    public bool sawWelcome;
    public bool sawLaptopHint;

    public int languageIndex;
    public int fontSizeIndex;

    public int soundVolume;

    public int targetFrameRateIndex;
    public int fieldOfViewIndex;

    public int moveSpeed;
    public int horizontalSensitivity;
    public int verticalSensitivity;
}
