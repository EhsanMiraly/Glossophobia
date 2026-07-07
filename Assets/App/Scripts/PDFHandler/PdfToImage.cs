using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using PDFtoImage;
using SkiaSharp;
using UnityEngine;

public class PdfToImage
{
    private static int renderDpi = 150;

    private static bool isRunning = false;


    public static async Task StartConversion()
    {
        if (isRunning)
        {
            Debug.LogWarning("PdfToImage: یک تبدیل در حال اجراست");
            return;
        }

        if (!File.Exists(GameData.selectedPdfFileFullPath))
        {
            Debug.LogError("PdfToImage: فایل PDF پیدا نشد: " + GameData.selectedPdfFileFullPath);
            return;
        }

        isRunning = true;

        try
        {
            List<byte[]> pngPages = await Task.Run(() => ConvertPdfToPngBytes());

            CreateTexturesFromPngBytes(pngPages);
        }
        catch (Exception e)
        {
            Debug.LogError("PdfToImage Error: " + e);
        }
        finally
        {
            isRunning = false;
        }
    }

    private static List<byte[]> ConvertPdfToPngBytes()
    {
        byte[] pdfBytes = File.ReadAllBytes(GameData.selectedPdfFileFullPath);

        var options = new RenderOptions
        {
            Dpi = renderDpi,
            WithAspectRatio = true
        };

        IEnumerable<SKBitmap> bitmaps = Conversion.ToImages(pdfBytes, password: null, options);

        var pngList = new List<byte[]>();

        foreach (var bmp in bitmaps)
        {
            try
            {
                using (var encoded = bmp.Encode(SKEncodedImageFormat.Png, 100))
                {
                    pngList.Add(encoded.ToArray());
                }
            }
            finally
            {
                bmp.Dispose();
            }
        }

        return pngList;
    }

    private static void CreateTexturesFromPngBytes(List<byte[]> pngList)
    {
        GameData.pageTextures.Clear();

        foreach (var png in pngList)
        {
            Texture2D tex = new Texture2D(2, 2, TextureFormat.RGBA32, false);
            bool ok = tex.LoadImage(png);

            if (ok)
            {
                tex.Apply();
                GameData.pageTextures.Add(tex);
            }
            else
            {
                UnityEngine.GameObject.Destroy(tex);
            }
        }
    }

}