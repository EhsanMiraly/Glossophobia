using UnityEngine;
using System.IO;

public class FolderSetup : MonoBehaviour
{
    void Awake()
    {
        //Making Folders
        string basePath = Application.persistentDataPath;

        GameData.pdfFilesFolderPath = Path.Combine(basePath, "PDFFiles");
        if (!Directory.Exists(GameData.pdfFilesFolderPath))
        {
            Directory.CreateDirectory(GameData.pdfFilesFolderPath);
        }

        GameData.DatabaseFolderPath = Path.Combine(basePath, "DataBase");
        if (!Directory.Exists(GameData.DatabaseFolderPath))
        {
            Directory.CreateDirectory(GameData.DatabaseFolderPath);
        }
        GameData.DatabaseFullPath = Path.Combine(GameData.DatabaseFolderPath, GameData.DatabaseName_GlossophobiaDB);
    }
}