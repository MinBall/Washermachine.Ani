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
        optionList.Add("�����ϼ���");
        optionList.Add("Y����");
        optionList.Add("����");
        optionList.Add("������");
        optionList.Add("���� ����");
        optionList.Add("���� ����");
        optionList.Add("�纹 ���� ����");
        optionList.Add("�޺� ����");
        optionList.Add("���̺� ����");
        optionList.Add("�νõ� ����");
        optionList.Add("�νõ� ����");

        options.AddOptions(optionList);

        options.value = currentOption;

        options.onValueChanged.AddListener(delegate { setDropDown(options.value); });
        //setDropDown(currentOption); //���� �ɼ� ������ �ʿ��� ���
    }
    void setDropDown(int option)
    {
        Test.text = CalendarController.WasherMenu[option];
        Debug.Log(option);
        PlayerPrefs.SetInt(DROPDOWN_KEY, option);
        // option ���� ����
        //Debug.Log("current option : " + options.value);
    }
}
