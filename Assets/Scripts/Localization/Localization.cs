using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Localization : ISavable, ILoadable<Localization>
{
    public string lang = "en";

    public Dictionary<string, string> words = new Dictionary<string, string>();

    [SerializeField]
    private List<StringPair> wordsList
        = new List<StringPair>()
    {
        ("language","English"),
        ("score","Score"),
        ("shop","Shop"),
        ("buy","Buy")
    };

    public static Localization[] LoadAll()
    {
        string[] resourceFiles = Directory.GetFiles("Assets/Resources/");
        List<Localization> localizations = new List<Localization>();
        foreach (string filename in resourceFiles)
        {
            if (filename.Contains("-langFile.json") && !filename.Contains("meta"))
            {
                // Debug.Log($"filename: {filename}");
                localizations.Add(new Localization().GetLoadedData(File.ReadAllText(filename)));
            }
        }
        return localizations.ToArray();
    }

    public override string ToString()
    {
        string data = lang + "\n";
        foreach (string key in words.Keys)
        {
            data += $"{key}: {words[key]}\n";
        }
        return data;
    }

    public void SaveJson()
    {
        File.WriteAllText("Assets/Resources/" + lang + "-langFile.json", GetSavedData());
        // if (File.Exists("Assets/Resources/" + lang + "-langFile.json"))
        // {
        //     Debug.Log("Exists!");
        // }
    }

    public string GetSavedData()
    {
        return (JsonUtility.ToJson(this, true));
    }

    public Localization GetLoadedData(string savedData)
    {
        var loc = JsonUtility.FromJson<Localization>(savedData);
        loc.words = new Dictionary<string, string>();
        foreach (StringPair pair in loc.wordsList)
        {
            loc.words.Add(pair.Key, pair.Value);
        }
        // Debug.Log(loc);
        return loc;
    }
}