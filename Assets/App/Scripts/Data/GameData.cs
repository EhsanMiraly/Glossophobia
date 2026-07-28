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
    public static Timer initialTimer = new Timer(0, 5, 0);
    public static Timer remainingTimer = new Timer(0, 0, 0);
    public static Timer extraTimer = new Timer(0, 0, 0);


    //GameState
    public static GameSession gameSession;
    public static bool isSimulating = false;
    public static bool isClockTicking = false;




    //public static List<GameObject> npcs = new List<GameObject>();
    //public static List<GameObject> npcsPool = new List<GameObject>();

}
