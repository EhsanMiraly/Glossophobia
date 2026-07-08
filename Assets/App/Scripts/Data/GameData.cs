using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.IO;

public class GameData
{
    public static string pdfFilesFolderPath = Path.Combine(Application.persistentDataPath, "PDFFiles");
    public static string selectedPdfFileFullPath = "";
    public static List<Texture2D> pageTextures = new List<Texture2D>();

    //Timer Data
    public static Timer initialTimer = new Timer(0, 1, 0);
    public static Timer remainingTimer = new Timer(0, 1, 0);
    public static Timer extraTimer = new Timer(0, 0, 0);





    //public static List<GameObject> npcs = new List<GameObject>();
    //public static List<GameObject> npcsPool = new List<GameObject>();





    public static bool isTyping = false;

    //PDF Data


    public static string selectedPdfName = "";





    //DataBase Data
    public static string DatabaseFolderPath = "";
    public static string DatabaseFullPath = "";
    public static string DatabaseName_GlossophobiaDB = "GlossophobiaDB.db";
    public static string Table_PlayerAccount = "PlayerAccount";
    public static string Table_PlayerDemographics = "PlayerDemographics";
    public static string Table_BeforeAnswers = "PlayerBeforeAnswers";
    public static string Table_AfterAnswers = "PlayerAfterAnswers";
    public static string Table_GameSettings = "GameSettings";





}
