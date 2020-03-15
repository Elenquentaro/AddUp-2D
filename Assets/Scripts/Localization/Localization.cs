using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Localization : ISavable, ILoadable<Localization>
{
    public string langCode = "en";
    public string langName = "English";

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
        TextAsset[] resourceFiles = Resources.LoadAll<TextAsset>("Localizations");
        List<Localization> localizations = new List<Localization>();
        foreach (TextAsset asset in resourceFiles)
        {
            localizations.Add(new Localization().GetLoadedData(asset.text));
        }
        return localizations.ToArray();
    }

    public override string ToString()
    {
        string data = langCode + "\n";
        foreach (string key in words.Keys)
        {
            data += $"{key}: {words[key]}\n";
        }
        return data;
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