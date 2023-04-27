using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LocalizationManager : MonoBehaviour
{
    public SystemLanguage[] languages;

    SystemLanguage currentLanguage;
    Dictionary<SystemLanguage, LocalizationData> localizationDatas;
    UnityEvent onLanguageChanged;
    int languageIndex = 0;
    public static LocalizationManager Instance;
    public static Action Dropdown;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            localizationDatas = new Dictionary<SystemLanguage, LocalizationData>();
            for (int i = 0; i < languages.Length; i++)
            {
                localizationDatas.Add(languages[i], new LocalizationData(languages[i]));
            }
            onLanguageChanged = new UnityEvent();
            UpdateLanguageFromSystem();
        }
        else
        {
            Destroy(gameObject);
        }
        Dropdown = () => { SwitchCurrentLocalizationData(); };
    }

    private void UpdateLanguageFromSystem()
    {
        currentLanguage = Application.systemLanguage;
        if(!AreLanguagesContain(currentLanguage))
        {
            currentLanguage = SystemLanguage.English;
        }
        languageIndex = Indexof(currentLanguage);
    }

    int Indexof(SystemLanguage language)
    {
        for (int i = 0; i < languages.Length; i++)
        {
            if (languages[i] == language)
            {
                return i;
            }
        }
        return -1;
    }

    bool AreLanguagesContain(SystemLanguage language)
    {
        for (int i = 0; i < languages.Length; i++)
        {
            if(languages[i] == language)
            {
                return true;
            }
        }
        return false;
    }

    public void AddFunctionToChangeEvent(UnityAction action)
    {
        onLanguageChanged.AddListener(action);
    }

    public void RemoveFunctionFromChangeEvent(UnityAction action)
    {
        onLanguageChanged.RemoveListener(action);
    }

    public LocalizationData GetCurrentLocalizationData()
    {
        return localizationDatas[currentLanguage];
    }

    /// <summary>
    /// change current language to next language in list
    /// </summary>
    //s[Obsolete]
    public void SwitchCurrentLocalizationData()
    {
        //languageIndex = (languageIndex + 1) % languages.Length;
        languageIndex = LocalizationDropdown.options.value;
        currentLanguage = languages[languageIndex];
        onLanguageChanged.Invoke();
    }

    public void SwitchCurrentLocalizationData (SystemLanguage targetLanguage)
    {
        if(!languages.Contains(targetLanguage))
        {
            Debug.LogError("Target language doesn't exist in language list");
            return;
        }
        languageIndex = Indexof(targetLanguage);
        currentLanguage = languages[languageIndex];
        onLanguageChanged.Invoke();
    }

    public string GetStringFromCode(string code, string defaultString = "")
    {
        return GetTextFromCodeAndColumnName(code, defaultString, currentLanguage.ToString());
        //return GetTextFromCodeAndColumnName(code, defaultString, "CONTENT");
    }

    public string GetTextFromCodeAndColumnName(string code, string defaultString = "", string columnName = "CONTENT")
    {
        return GetCurrentLocalizationData().GetValueByCode(code, defaultString, columnName);  
    }


}


