using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    private static Progress currentProgress = null;
    public static Progress CurrentProgress => currentProgress;

    private static Settings currentSettings = null;
    public static Settings CurrentSettings => currentSettings;

    void Awake()
    {
        ScoreCounter.onScoreChange.AddListener((score) =>
        {
            currentProgress.Score = score;
            SaveLoader.Save<Progress>(currentProgress);
        });

        ItemBuy.onPurchase.AddListener((item) =>
        {
            currentProgress.BuyedItems[item.number - 1]++;
            SaveLoader.Save<Progress>(currentProgress);
        });

        currentProgress = SaveLoader.Load<Progress>();
        currentSettings = SaveLoader.Load<Settings>();
    }

    void Start()
    {
        // currentSettings.SetLanguage("ru");
        LocalizationManager.LoadCurrentLocalization(currentSettings.Language);
    }

    public static void ResetProgress()
    {
        SaveLoader.Save<Progress>(new Progress());
        SceneManager.LoadScene(0);
    }

    public static void SaveGridState(List<SummandInfo> summandsOnGrid)
    {
        currentProgress.summandsOnGrid = summandsOnGrid;
        SaveLoader.Save<Progress>(currentProgress);
    }

    public static void SaveSettings()
    {
        SaveLoader.Save<Settings>(currentSettings);
    }
}