using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System;

public static class JsonManager
{
    private readonly static byte[] encryptionKey =
    {
        0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16
    };

    private static Dictionary<string, string> textNames;

    static JsonManager()
    {
        LoadText();
    }
    public static void LoadText()
    {   
        try
        {
            string encryptedJsonText = LoadAndDecrypt(Path.Combine(Application.dataPath, "Resources/GameData.dat"));

            if (!string.IsNullOrEmpty(encryptedJsonText))
                textNames = JsonConvert.DeserializeObject<Dictionary<string, string>>(encryptedJsonText);

            else
                Debug.LogError("Failed to load encrypted data.");
            
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error in LoadText: {ex.Message}");
        }
    }
    public static void SaveText(string key, string value)
    {
        try
        {
            if (textNames != null)
            {
                textNames[key] = value;

                string jsonText = JsonConvert.SerializeObject(textNames, Formatting.Indented);
                EncryptAndSave(jsonText, Path.Combine(Application.dataPath, "Resources/GameData.dat"));
            }
            else
                Debug.LogError("textNames dictionary is not initialized.");
            
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error in SaveText: {ex.Message}");
        }
    }
    public static void UpdateText(string key, Text textObject)
    {
        if (textNames != null && textNames.ContainsKey(key))
            textObject.text = textNames[key];

        else
            Debug.LogError($"Key '{key}' not found.");
        
    }
    private static void EncryptAndSave(string data, string filePath)
    {
        using (AesManaged aes = new AesManaged())
        {
            aes.Key = encryptionKey;

            aes.GenerateIV();

            using (ICryptoTransform encryptor = aes.CreateEncryptor())
            using (FileStream fsOutput = new FileStream(filePath, FileMode.Create))
            using (CryptoStream cs = new CryptoStream(fsOutput, encryptor, CryptoStreamMode.Write))
            {
                fsOutput.Write(aes.IV, 0, aes.IV.Length);

                byte[] dataBytes = Encoding.UTF8.GetBytes(data);
                cs.Write(dataBytes, 0, dataBytes.Length);
            }
        }
    }
    private static string LoadAndDecrypt(string filePath)
    {
        using (AesManaged aes = new AesManaged())
        {
            aes.Key = encryptionKey;

            byte[] iv = new byte[16];
            using (FileStream fsInput = new FileStream(filePath, FileMode.Open))
            {
                fsInput.Read(iv, 0, iv.Length);
                aes.IV = iv;

                using (ICryptoTransform decryptor = aes.CreateDecryptor())
                using (MemoryStream msOutput = new MemoryStream())
                using (CryptoStream cs = new CryptoStream(msOutput, decryptor, CryptoStreamMode.Write))
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead;
                    while ((bytesRead = fsInput.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        cs.Write(buffer, 0, bytesRead);
                    }

                    cs.FlushFinalBlock();

                    return Encoding.UTF8.GetString(msOutput.ToArray());
                }
            }
        }
    }
}



