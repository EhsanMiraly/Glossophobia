using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using SFB;

public class PDF_FileDialogManager
{
    public static async Task OpenFileDialog()
    {
        var extensions = new[] {
            new ExtensionFilter("PDF Files", "pdf"),
            new ExtensionFilter("All Files", "*")
        };

        var paths = StandaloneFileBrowser.OpenFilePanel("Select a PDF file", "", extensions, false);

        if (paths.Length > 0)
        {
            string selectedPath = paths[0];
            await SavePdfFileAsync(selectedPath);
        }
    }

    private static async Task SavePdfFileAsync(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Debug.LogError("⚠️ فایل انتخابی وجود ندارد!");
            return;
        }

        if (!Directory.Exists(GameData.pdfFilesFolderPath))
            Directory.CreateDirectory(GameData.pdfFilesFolderPath);

        // نام اصلی فایل
        string newFileName = Path.GetFileName(filePath);

        string targetPath = Path.Combine(GameData.pdfFilesFolderPath, newFileName);

        Debug.Log("⏳ در حال کپی فایل...");

        await Task.Run(() =>
        {
            File.Copy(filePath, targetPath, true);
        });

        Debug.Log("✅ کپی فایل انجام شد!");
    }

}
