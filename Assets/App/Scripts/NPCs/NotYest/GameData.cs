using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameData
{






    //public static List<GameObject> npcs = new List<GameObject>();
    //public static List<GameObject> npcsPool = new List<GameObject>();





    public static bool isTyping = false;

    //PDF Data
    public static string pdfFilesFolderPath = "";
    public static string selectedPdfFileFullPath = "";
    public static string selectedPdfName = "";
    public static List<Texture2D> pageTextures = new List<Texture2D>();

    //Timer Data
    public static int hours = 0;
    public static int minutes = 20;


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
