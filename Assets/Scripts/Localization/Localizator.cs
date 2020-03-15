using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// [RequireComponent(typeof(Text))]
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
        if (LocalizationManager.GetCurrentLocalization() != null)
        {
            LocalizeText(LocalizationManager.GetCurrentLocalization());
        }
    }

    public void LocalizeText(Localization loc)
    {
        // Debug.Log("localization of " + wordKey);
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
