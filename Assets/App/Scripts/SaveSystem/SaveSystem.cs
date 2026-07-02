using UnityEngine;
using System.IO;

public class SaveSystem
{
    protected static string saveDirectory;

    protected static void CreateSaveDirectory()
    {
        saveDirectory = Path.Combine(Application.persistentDataPath, "SaveData");

        if (!Directory.Exists(saveDirectory))
        {
            Directory.CreateDirectory(saveDirectory);
        }
    }

}
