using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel = null;

    [SerializeField] private Dropdown langSelector = null;

    [SerializeField] private Slider spawnRateSlider = null;

    [SerializeField] private Text rateTextGUI = null;

    private bool changeGameStateRequires = true;

    private Localization[] localizations = null;

    void Awake()
    {
        GameStateManager.onGameStateChanged.AddListener((isPlaying) => changeGameStateRequires = isPlaying);

        localizations = Localization.LoadAll();
        InitDropdown();
        InitSlider();
    }

    void OnEnable()
    {
        Localization currentLoc = LocalizationManager.CurrentLocalization;
        langSelector.value = Array.IndexOf(localizations, currentLoc);
        rateTextGUI.text = DataManager.CurrentSettings.SummandSpawnRate.ToString();
        spawnRateSlider.value = DataManager.CurrentSettings.SummandSpawnRate;
        return;
    }

    private void InitDropdown()
    {
        langSelector.options.Clear();
        langSelector.AddOptions((from Localization loc in localizations select loc.langName).ToList());
        langSelector.onValueChanged.AddListener((value) =>
        {
            LocalizationManager.SetCurrentLocalization(localizations[value]);
            DataManager.CurrentSettings.SetLanguage(LocalizationManager.CurrentLocalization.langCode);
        });
    }

    private void InitSlider()
    {
        spawnRateSlider.onValueChanged.AddListener((value) =>
        {
            rateTextGUI.text = value.ToString();
            DataManager.CurrentSettings.SummandSpawnRate = (int)value;
        });
    }

    //вызывается по нажатию кнопки
    public void ResetProgress()
    {
        DataManager.ResetProgress();
    }

    //вызывается по нажатию кнопки
    public void SwitchVisibility()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
        bool isOpened = settingsPanel.activeSelf;

        if (!isOpened)
        {
            DataManager.SaveSettings();
        }

        if (changeGameStateRequires || !isOpened) GameStateManager.SwitchGameState(!isOpened);
    }
}
