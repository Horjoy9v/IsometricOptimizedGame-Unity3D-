using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

public static class JsonManager
{
    private static Dictionary<string, string> textNames;

    static JsonManager()
    {
        LoadText();
    }

    public static void LoadText()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("GameData");

        if (jsonFile != null)
        {
            string jsonText = jsonFile.text;
            textNames = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonText);
        }
        else
        {
            Debug.LogError("Failed load GameData.json.");
        }
    }
    public static void SaveText(string key, string value)
    {
        if (textNames != null)
        {
            textNames[key] = value;

            string jsonText = JsonConvert.SerializeObject(textNames, Formatting.Indented);
            string filePath = Path.Combine(Application.dataPath, "Resources/GameData.json");
            File.WriteAllText(filePath, jsonText);
        }
        else
        {
            Debug.LogError("textNames dictionary is not initialized.");
        }
    }
    public static void UpdateText(string key, Text textObject)
    {
        if (textNames != null && textNames.ContainsKey(key))
        {
            textObject.text = textNames[key];
        }
        else
        {
            Debug.LogError($"Key '{key}' not found.");
        }
    }
}
