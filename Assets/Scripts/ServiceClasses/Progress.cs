using UnityEngine;

[System.Serializable]
public class Progress : ISavable, ILoadable<Progress>
{
    public int Score = 0;

    public int[] BuyedItems = new int[100];

    public string GetSavedData()
    {
        return JsonUtility.ToJson(this, true);
    }

    public Progress GetLoadedData(string savedData)
    {
        return JsonUtility.FromJson<Progress>(savedData) ?? new Progress();
    }

}