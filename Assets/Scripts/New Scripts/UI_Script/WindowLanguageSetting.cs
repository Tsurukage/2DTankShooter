using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;

public class WindowLanguageSetting : MonoBehaviour
{
    private static WindowLanguageSetting Instance;
    [SerializeField] private Language currentLang = 0;
    [SerializeField] private Button btn_chn;
    [SerializeField] private Button btn_eng;
    [SerializeField] private Button btn_close;
    [SerializeField] private Text text_title;
    void Display(bool display)
    {
        gameObject.SetActive(display);
    }
    void Start()
    {
        Instance = this;
        var language = PlayerPrefs.GetInt("language");
        SetLanguage((Language)language);
        if (btn_close != null)
            btn_close.onClick.AddListener(CloseWindow);
        if (btn_chn != null)
        {
            btn_chn.onClick.AddListener(() => SetLanguage(Language.Chinese));
            btn_chn.onClick.AddListener(SoundEffect);
        }
        if (btn_eng != null)
        {
            btn_eng.onClick.AddListener(() => SetLanguage(Language.English));
            btn_eng.onClick.AddListener(SoundEffect);
        }
        Display(false);
    }

    private void CloseWindow()
    {
        SoundEffectManager.Instance.OnClickSound();
        Display(false);
    }

    private void SetLanguage(Language language)
    {
        currentLang = language;
        PlayerPrefs.SetInt("language", (int)currentLang);
        print(currentLang);
        var playerUi = PlayerUIManager.Instance;
        if(playerUi !=null)
            playerUi.SetLanguage(currentLang);
        var localeSelector = FindObjectOfType<LocalizationSelector>();
        if (localeSelector != null)
            localeSelector.ChangeLocale((int)currentLang);
    }
    private void SoundEffect()
    {
        SoundEffectManager.Instance.OnClickSound();
    }
    public static void OpenDisplay()
    {
        Instance.Display(true);
    }
}
public enum Language
{
    Chinese = 0,
    English = 1
}