using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationData
{
    SystemLanguage language;
    CSVObject csv;

    public LocalizationData(SystemLanguage language)
    {
        this.language = language;
        Init();
    }

    void Init()
    {
        //csv = CSVReader.Read(Resources.Load<TextAsset>("Localization/" + language.ToString() + "/translation").text);
        csv = CSVReader.Read(Resources.Load<TextAsset>("Localization/translation").text);
    }


    public string GetValueByCode(string code, string defaultString, string columnName = "CONTENT")
    {
        Dictionary<string, object> data = csv.GetDataByCode(code);
        if (data != null && data[columnName] != null)
        {
            return data[columnName].ToString();
        }
        else
        {
            Debug.LogWarning("There is localization error. CODE :" + code );
            return defaultString;
        }
    }
}

