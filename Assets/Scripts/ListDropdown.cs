using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ListDropdown : MonoBehaviour
{
    string DROPDOWN_KEY = "DROPDOWN_KEY";

    int currentOption;
    public TMP_Dropdown options;
    public TextMeshProUGUI Test;

    List<string> optionList = new List<string>();
    

    void Awake()
    {
        if (PlayerPrefs.HasKey(DROPDOWN_KEY) == false) currentOption = 0;
        else currentOption = PlayerPrefs.GetInt(DROPDOWN_KEY);
    }

    void Start()
    {
        options = this.GetComponent<TMP_Dropdown>();

        options.ClearOptions();
        optionList.Add("선택하세요");
        optionList.Add("Y셔츠");
        optionList.Add("남방");
        optionList.Add("마남방");
        optionList.Add("정장 상의");
        optionList.Add("정장 하의");
        optionList.Add("양복 예복 조끼");
        optionList.Add("콤비 상의");
        optionList.Add("연미복 상의");
        optionList.Add("턱시도 상의");
        optionList.Add("턱시도 바지");

        options.AddOptions(optionList);

        options.value = currentOption;

        options.onValueChanged.AddListener(delegate { setDropDown(options.value); });
        //setDropDown(currentOption); //최초 옵션 실행이 필요한 경우
    }
    void setDropDown(int option)
    {
        Test.text = CalendarController.WasherMenu[option];
        Debug.Log(option);
        PlayerPrefs.SetInt(DROPDOWN_KEY, option);
        // option 관련 동작
        //Debug.Log("current option : " + options.value);
    }
}
