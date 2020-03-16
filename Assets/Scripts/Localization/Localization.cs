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
        return loc;
    }

    public override bool Equals(object obj)
    {
        return obj is Localization localization &&
               langCode == localization.langCode &&
               langName == localization.langName &&
               wordsList.Count == localization.wordsList.Count;
    }

    public override int GetHashCode()
    {
        int hashCode = 280244602;
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(langCode);
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(langName);
        hashCode = hashCode * -1521134295 + EqualityComparer<int>.Default.GetHashCode(wordsList.Count);
        return hashCode;
    }
}