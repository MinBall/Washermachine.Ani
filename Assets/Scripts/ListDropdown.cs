using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ListDropdown : MonoBehaviour
{
    /*string DROPDOWN_KEY = "DROPDOWN_KEY";

    int[] currentOption;
    int[] Count = new int[3]{ 0,0,0 };
    public TMP_Dropdown[] options;
    public TextMeshProUGUI[] ListName;
    public TextMeshProUGUI[] Price;
    public TextMeshProUGUI[] TotalCount;

    List<string> optionList = new List<string>() { "선택하세요", "Y셔츠", "남방", "마남방", "정장 상의", "정장 하의", "양복 예복 조끼", "콤비 상의", "연미복 상의", "턱시도 상의", "턱시도 바지" };

    void Awake()
    {
        currentOption = new int[options.Length];
        if (PlayerPrefs.HasKey(DROPDOWN_KEY) == false)
        {
            for (int i = 0; i < options.Length; i++)
            {
                currentOption[i] = 0;
            }
        }
        else
        {
            for (int i = 0; i < options.Length; i++)
            {
                currentOption[i] = PlayerPrefs.GetInt(DROPDOWN_KEY + i);
            }
        }
    }

    void Start()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].ClearOptions();
            options[i].AddOptions(optionList);
            options[i].value = currentOption[i];
            int index = i;
            options[i].onValueChanged.AddListener(delegate { setDropDown(index, options[index].value); });
        }
        //setDropDown(0, currentOption[0]); //최초 옵션 실행이 필요한 경우
        resetDropdowns();
    }

    void setDropDown(int index, int option)
    {
        if (option != 0)
        {
            string selectedListName = CalendarController.WasherMenu[option];
            int selectedCount = 1;

            for (int i = 0; i < options.Length; i++)
            {
                if (i != index && ListName[i].text == selectedListName)
                {
                    // ListName이 같으면 Count 증가시키고 다음 배열에 할당하지 않음
                    Count[i]++;
                    TotalCount[i].text = Count[i].ToString();                    
                    Price[index].text = (Count[index] * CalendarController.Washerprice[option]).ToString();
                    selectedCount = 0;
                }
            }

            if (selectedCount > 0)
            {
                ListName[index].text = selectedListName;                
                Count[index] = 1;
                Price[index].text = CalendarController.Washerprice[option].ToString();                                
                TotalCount[index].text = Count[index].ToString();
                PlayerPrefs.SetInt(DROPDOWN_KEY + index, option);                
            }
        }
    }

    void resetDropdowns()
    {
        for (int i = 0; i < options.Length; i++)
        {
            currentOption[i] = 0;
            options[i].value = 0;
            setDropDown(i, 0);
        }
    }*/
}
