using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// базовый класс для компонентов UI, которые необходимо локализовать

public class Localizator : MonoBehaviour
{
    [SerializeField] protected Text textGUI = null;

    [SerializeField] protected string wordKey = "score";
    [SerializeField] protected string localizableText = "";
    [SerializeField] protected string additionSymbols = "";


    protected virtual void Awake()
    {
        LocalizationManager.onLocalize.AddListener(LocalizeText);
    }

    void OnEnable()
    {
        if (LocalizationManager.CurrentLocalization != null)
        {
            LocalizeText(LocalizationManager.CurrentLocalization);
        }
    }

    public void LocalizeText(Localization loc)
    {
        if (loc.words.ContainsKey(wordKey))
        {
            localizableText = loc.words[wordKey];
        }
        DisplayText();
    }

    public virtual void DisplayText()
    {
        textGUI.text = localizableText + additionSymbols;
    }
}
