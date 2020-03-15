using System.Linq;
using UnityEngine;
using UnityEditor;

public class LocalizationManager : MonoBehaviour
{
    public static GenericEvent<Localization> onLocalize = new GenericEvent<Localization>();

    private static Localization currentLocalization;

    public static Localization GetCurrentLocalization() => currentLocalization;

    [MenuItem("Localization/Create default en localization file")]
    public static void CreateDefault()
    {
        new Localization().SaveJson();
    }

    public static void LoadCurrentLocalization(string langCode = "en")
    {
        var localizations = Localization.LoadAll();
        if (localizations == null) return;

        Localization current = (from Localization loc
                                in localizations
                                where loc.lang == langCode
                                select loc).FirstOrDefault();
        if (current == null)
        {
            Debug.Log("Localization not loaded");
            return;
        }

        currentLocalization = current;
        onLocalize?.Invoke(currentLocalization);
        // Debug.Log("Current localization: " + currentLocalization);
    }
}
