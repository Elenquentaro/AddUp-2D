using UnityEngine;

[System.Serializable]
public class Settings : ISavable, ILoadable<Settings>, IExtensionable
{
    [SerializeField] private string language = "en";
    public string Language => language;
    public void SetLanguage(string value) => language = value;

    [SerializeField] private int summandSpawnRate = 10;
    public int SummandSpawnRate { get => summandSpawnRate; set => summandSpawnRate = value; }

    public Settings GetLoadedData(string savedData)
    {
        return JsonUtility.FromJson<Settings>(savedData) ?? new Settings();
    }

    public string GetSavedData()
    {
        return JsonUtility.ToJson(this, true);
    }

    public string GetExtention()
    {
        return ".json";
    }
}