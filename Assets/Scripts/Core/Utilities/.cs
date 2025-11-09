using System;
using System.IO;
using UnityEngine.UI;

public class ImageConverter
{
    public Image Base64ToImage(string base64String)
    {
        // Convert base 64 string to byte[]
        byte[] imageBytes = Convert.FromBase64String(base64String);

        // Convert byte[] to Image
        using (MemoryStream ms = new MemoryStream(imageBytes))
        {
            Image image = Image.FromStream(ms);
            return image;
        }
    }
}