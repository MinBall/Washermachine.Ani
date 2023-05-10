using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CalendarController : MonoBehaviour
{
    public GameObject _calendarPanel;
    public GameObject _item;
    public GameObject[] Popup;

    public Text _yearNumText;
    public Text _monthNumText;
    public TextMeshProUGUI[] RandomList = new TextMeshProUGUI[3];
    public TextMeshProUGUI[] TotalList;
    public TextMeshProUGUI[] Price;
    public TextMeshProUGUI[] TotalCount;
    public TextMeshProUGUI TotalPrice;
    public TMP_Dropdown[] options;
    public TMP_InputField InputField;

    public Animator animator;

    public List<GameObject> _dateItems = new List<GameObject>();
    const int _totalDateNum = 42;

    public static CalendarController _calendarInstance;
    public static Action action;

    private DateTime _dateTime;
    DateTime nowDate = DateTime.Now;
    DateTime SelectDate;
    DateTime _2daylater;

    string Selectyear;
    string DROPDOWN_KEY = "DROPDOWN_KEY";

    int[] currentOption;
    int[] Count = new int[3] { 0, 0, 0 };
    int[] result = new int[3];
    int totalprice = 0;

    List<string> optionList = new List<string>() { "선택하세요", "Y셔츠", "남방", "마남방", "정장 상의", "정장 하의", "양복 예복 조끼", "콤비 상의", "연미복 상의", "턱시도 상의", "턱시도 바지" };

    Dictionary<int, int> Washerprice = new Dictionary<int, int>()
    {
        {1, 1800},
        {2, 4000},
        {3, 5200},
        {4, 5000},
        {5, 4000},
        {6, 2600},
        {7, 7200},
        {8, 9800},
        {9, 8800},
        {10, 6800},
    };
    Dictionary<int, string> WasherMenu = new Dictionary<int, string>()
    {
        {1, "Y셔츠"},
        {2, "남방"},
        {3, "마남방"},
        {4, "정장 상의"},
        {5, "정장 하의"},
        {6, "양복 예복 조끼"},
        {7, "콤비 상의"},
        {8, "연미복 상의"},
        {9, "턱시도 상의"},
        {10, "턱시도 바지"},
    };

    private void Awake()
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
        action += AllObjectReset;
    }
    void Start()
    {

        _calendarInstance = this;
        Vector3 startPos = _item.transform.localPosition;
        _dateItems.Clear();
        _dateItems.Add(_item);

        for (int i = 1; i < _totalDateNum; i++)
        {
            GameObject item = GameObject.Instantiate(_item) as GameObject;
            item.name = "Item" + (i + 1).ToString();
            item.transform.SetParent(_item.transform.parent);
            item.transform.localScale = Vector3.one;
            item.transform.localRotation = Quaternion.identity;
            item.transform.localPosition = new Vector3((i % 7) * 36  + startPos.x, startPos.y - (i / 7) * 30, startPos.z);

            _dateItems.Add(item);
        }

        _dateTime = DateTime.Now;

        CreateCalendar();

        _calendarPanel.SetActive(false);

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

    void CreateCalendar()
    {
        DateTime firstDay = _dateTime.AddDays(-(_dateTime.Day - 1));
        int index = GetDays(firstDay.DayOfWeek);

        int date = 0;
        for (int i = 0; i < _totalDateNum; i++)
        {
            Text label = _dateItems[i].GetComponentInChildren<Text>();
            _dateItems[i].SetActive(false);

            if (i >= index)
            {
                DateTime thatDay = firstDay.AddDays(date);
                if (thatDay.Month == firstDay.Month)
                {
                    _dateItems[i].SetActive(true);

                    label.text = (date + 1).ToString();
                    date++;
                }
            }
        }
        _yearNumText.text = _dateTime.Year.ToString();
        _monthNumText.text = _dateTime.Month.ToString("D2");
    }

    int GetDays(DayOfWeek day)
    {
        switch (day)
        {
            case DayOfWeek.Monday: return 1;
            case DayOfWeek.Tuesday: return 2;
            case DayOfWeek.Wednesday: return 3;
            case DayOfWeek.Thursday: return 4;
            case DayOfWeek.Friday: return 5;
            case DayOfWeek.Saturday: return 6;
            case DayOfWeek.Sunday: return 0;
        }

        return 0;
    }
    public void YearPrev()
    {
        _dateTime = _dateTime.AddYears(-1);
        CreateCalendar();
    }

    public void YearNext()
    {
        _dateTime = _dateTime.AddYears(1);
        CreateCalendar();
    }

    public void MonthPrev()
    {
        _dateTime = _dateTime.AddMonths(-1);
        CreateCalendar();
    }

    public void MonthNext()
    {
        _dateTime = _dateTime.AddMonths(1);
        CreateCalendar();
    }

    public void ShowCalendar(Text target)
    {
        _calendarPanel.SetActive(true);
        _target = target;
        //_calendarPanel.transform.position = new Vector3(965, 475, 0);//Input.mousePosition-new Vector3(0,120,0);
    }

    Text _target;

    //Item 클릭했을 경우 Text에 표시.
    // 날짜 선택 시 달력 창이 닫히면서 날짜가 Result Date에 나옴 여기서 조건문으로 금일로부터 2일 후가 아니면 오류 팝업 띄우기
    public void OnDateItemClick(string day)
    {
        string _monthNum;
        Selectyear = _yearNumText.text;
        int result;
        int nowday = nowDate.Day;
        int nowmonth = nowDate.Month;
        int nowyear = nowDate.Year;        

        _monthNum = _monthNumText.text;

        _2daylater = new DateTime(nowyear, nowmonth, nowday + 2);
        SelectDate = new DateTime(int.Parse(Selectyear), int.Parse(_monthNum),int.Parse(day));

        // result가 0이면 같음 1이면 전날 -1이면 이후
        result = DateTime.Compare(_2daylater, SelectDate);
        
        if(result == -1)
        {
            _calendarPanel.SetActive(false);
           // Debug.Log(SelectDate + "날짜로 접수되었습니다.");
            _target.text = _yearNumText.text + "-" + _monthNumText.text + "-" + int.Parse(day).ToString("D2");
            RandomNumber();
            AnimatorStepControl();
        }
        else
        {
            // 오류 팝업 띄우기
            //Debug.Log("2일 후 선택해 주세요.");
            Popup[0].SetActive(true);
            _calendarPanel.SetActive(false);
        }        
    }

    public void RandomNumber()
    {
        for (int i = 0; i < 3; i++)
        {
            result[i] =UnityEngine.Random.Range(1, 11);
            //Debug.Log(WasherMenu[result[i]] +" "+ Washerprice[result[i]] + "원");
            RandomList[i].text = WasherMenu[result[i]];
        }
    }
    void setDropDown(int index, int option)
    {
        if (option != 0)
        {            
            string selectedListName = WasherMenu[option];
            int selectedCount = 1;

            if (option == result[index])
            {
                for (int i = 0; i < options.Length; i++)
                {
                    if (i != index && TotalList[i].text == selectedListName)
                    {
                        // ListName이 같으면 Count 증가시키고 다음 배열에 할당하지 않음
                        Count[i]++;
                        TotalCount[i].text = Count[i].ToString();
                        Price[index].text = (Count[index] * Washerprice[option]).ToString();
                        Price[i].text = (Count[i] * Washerprice[option]).ToString();
                        selectedCount = 0;
                        totalprice += Washerprice[option];
                        TotalPrice.text = totalprice.ToString() + "원";
                        //Debug.Log(Washerprice[option]);
                        AnimatorStepControl();
                    }
                }

                if (selectedCount > 0)
                {
                    TotalList[index].text = selectedListName;
                    Count[index] = 1;
                    Price[index].text = Washerprice[option].ToString();
                    TotalCount[index].text = Count[index].ToString();
                    PlayerPrefs.SetInt(DROPDOWN_KEY + index, option);
                    totalprice += Washerprice[option];
                    TotalPrice.text = totalprice.ToString() + "원";
                    //Debug.Log(Washerprice[option]);
                    AnimatorStepControl();
                }
            }
            else
            {
                // 오류 팝업 띄우기 함수
                //Debug.Log("같은 옵션을 골라주세요,");
                Popup[1].SetActive(true);
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
    }
    /*public void ReceiptPopUpOn()
    {
        Popup[2].SetActive(true);
    }*/
    public void PopUpClose()
    {
        for(int i =0; i<3;i++)
            Popup[i].SetActive(false);
    }

    public void AnimatorStepControl()
    {
        animator.SetTrigger("Next");
    }

    public void AllObjectReset()
    {
        resetDropdowns();
        _target.text = "Result Data ...";
        InputField.text = "이름 입력";
        for (int i = 0; i < 3; i++)
        {
            RandomList[i].text = " ";
            TotalPrice.text = "0원";
            TotalCount[i].text = " ";
            Price[i].text = " ";
            TotalList[i].text = " ";
            totalprice = 0;
            Count[i] = 0;
        }
    }
}
