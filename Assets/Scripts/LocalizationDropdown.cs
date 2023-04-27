using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LocalizationDropdown : MonoBehaviour
{
    string DROPDOWN_KEY = "DROPDOWN_KEY";

    int currentOption;
    public static TMP_Dropdown options;

    List<string> optionList = new List<string>();

    LocalizationManager localizationManager;

    void Awake()
    {
        if (PlayerPrefs.HasKey(DROPDOWN_KEY) == false) currentOption = 0;
        else currentOption = PlayerPrefs.GetInt(DROPDOWN_KEY);
    }

    void Start()
    {
        options = this.GetComponent<TMP_Dropdown>();

        options.ClearOptions();

        optionList.Add("Korean");
        optionList.Add("English");
        optionList.Add("Hungarian");
        optionList.Add("Ukrainian");

        options.AddOptions(optionList);

        options.value = currentOption;

        options.onValueChanged.AddListener(delegate { setDropDown(options.value); });
        setDropDown(currentOption); //최초 옵션 실행이 필요한 경우
    }

    void setDropDown(int option)
    {
        PlayerPrefs.SetInt(DROPDOWN_KEY, option);
        LocalizationManager.Dropdown();

        // option 관련 동작
        //Debug.Log("current option : " + options.value);
    }
}

