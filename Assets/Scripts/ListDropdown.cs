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

    List<string> optionList = new List<string>() { "�����ϼ���", "Y����", "����", "������", "���� ����", "���� ����", "�纹 ���� ����", "�޺� ����", "���̺� ����", "�νõ� ����", "�νõ� ����" };

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
        //setDropDown(0, currentOption[0]); //���� �ɼ� ������ �ʿ��� ���
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
                    // ListName�� ������ Count ������Ű�� ���� �迭�� �Ҵ����� ����
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
