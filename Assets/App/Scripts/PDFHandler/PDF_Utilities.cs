using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PDF_Utilities
{
    public static List<string> GetPDFsFileNames()
    {
        List<string> pdfNames = new List<string>();

        if (!Directory.Exists(GameData.pdfFilesFolderPath))
        {
            Debug.LogError("⚠️ مسیر وجود ندارد!");
            return pdfNames;
        }

        // گرفتن همه فایل های pdf داخل فولدر
        string[] files = Directory.GetFiles(GameData.pdfFilesFolderPath, "*.pdf");

        foreach (string file in files)
        {
            pdfNames.Add(Path.GetFileName(file)); // فقط اسم فایل
        }

        return pdfNames;
    }

    public static void DeletePDF(string selectedPDF)
    {
        string selectedPDFPath = Path.Combine(GameData.pdfFilesFolderPath, selectedPDF);

        if (File.Exists(selectedPDFPath))
        {
            File.Delete(selectedPDFPath);
            Debug.Log("PDF deleted: " + selectedPDFPath);
        }
        else
        {
            Debug.LogWarning("File not found: " + selectedPDFPath);
        }
    }
}
