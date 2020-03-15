using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Progress : ISavable, ILoadable<Progress>, IExtensionable
{
    public int Score = 0;

    public int[] BuyedItems = new int[100];

    public List<SummandInfo> summandsOnGrid = new List<SummandInfo>();

    public string GetSavedData()
    {
        return JsonUtility.ToJson(this, true);
    }

    public Progress GetLoadedData(string savedData)
    {
        return JsonUtility.FromJson<Progress>(savedData) ?? new Progress();
    }

    public string GetExtention()
    {
        return ".json";
    }
}