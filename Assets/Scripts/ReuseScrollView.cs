using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class ReuseScrollView : MonoBehaviour
{
    public string url = "https://jsonplaceholder.typicode.com/comments";
    public TMP_Text textComponent;
    public ScrollItem orgItemPrefab;
    public float itemHeight = 200.0f;
    public List<string> dataList;

    private ScrollRect _scroll;
    private List<ScrollItem> itemList;
    private float offset;

    private void Awake()
    {
        _scroll = GetComponent<ScrollRect>();
    }
    void Start()
    {
        StartCoroutine(LoadJson());
        dataList.Clear();      
    }

    IEnumerator LoadJson()
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("Error loading JSON: " + www.error);
        }
        else
        {
            string json = www.downloadHandler.text;

            // JSON 데이터를 Comment 배열로 변환합니다.
            Comment[] comments = JsonConvert.DeserializeObject<Comment[]>(json);

            string text = "";
            // 필요한 속성 값을 추출하여 TMP_Text에 할당합니다.
            foreach (Comment comment in comments)
            {
                text = "ID: " + comment.id + "\n"+"Email: " + comment.email + "\n"+ "Name: " + comment.name + "\n";
                dataList.Add(text);
            }

            CreateItem();
            SetContenHight();
        }
    }
    void CreateItem()
    {        
        RectTransform scrollRect = _scroll.GetComponent<RectTransform>();
        itemList = new List<ScrollItem>();

        int itemCount = (int)(scrollRect.rect.height / itemHeight) + 1 + 2;

        for(int i =0; i<itemCount; i++)
        {
            ScrollItem item = Instantiate<ScrollItem>(orgItemPrefab, _scroll.content);
            itemList.Add(item);

            item.transform.localPosition = new Vector3(0, -i * itemHeight);
            SetData(item,i);
        }
        offset = itemList.Count * itemHeight;
    }

    void SetContenHight()
    {
        _scroll.content.sizeDelta = new Vector2(_scroll.content.sizeDelta.x, dataList.Count * itemHeight);
    }

    bool RelocationItem(ScrollItem item, float contentY, float scrollHeight)
    {
        if(item.transform.localPosition.y + contentY > itemHeight *2f)
        {
            item.transform.localPosition -= new Vector3(0, offset);
            RelocationItem(item, contentY, scrollHeight);
            return true;
        }
        else if(item.transform.localPosition.y + contentY < -scrollHeight -itemHeight)
        {
            item.transform.localPosition += new Vector3(0, offset);
            RelocationItem(item, contentY, scrollHeight);
            return true;
        }
        return false;
    }

    void SetData(ScrollItem item, int idx)
    {
        if(idx < 0 || idx >= dataList.Count)
        {
            item.gameObject.SetActive(false);
            return;
        }
        item.gameObject.SetActive(true);
        item.SetText(dataList[idx].ToString());
    }
    void Update()
    {
        RectTransform scrollRect = _scroll.GetComponent<RectTransform>();
        float scrollHeight = scrollRect.rect.height;
        float contentY = _scroll.content.anchoredPosition.y;
        if (itemList != null)
        {
            foreach (ScrollItem item in itemList)
            {
                bool isChange = RelocationItem(item, contentY, scrollHeight);
                if (isChange)
                {
                    int idx = (int)(-item.transform.localPosition.y / itemHeight);
                    SetData(item, idx);
                }
            }
        }
    }
    [System.Serializable]
    public class Comment
    {
        public int id;
        public string name;
        public string email;
    }
}

