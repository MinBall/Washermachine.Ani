using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class ReuseScrollView : MonoBehaviour
{
    public ScrollItem orgItemPrefab;
    public float itemHeight = 200.0f;
    public List<int> dataList;

    private ScrollRect _scroll;
    private List<ScrollItem> itemList;
    private float offset;

    private void Awake()
    {
        _scroll = GetComponent<ScrollRect>();
    }
    void Start()
    {
        dataList.Clear();
        for(int i = 0; i <100; i++)
        {
            dataList.Add(i);
        }
        CreateItem();
        SetContenHight();
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
        _scroll.content.sizeDelta = new Vector2(_scroll.content.sizeDelta.x, dataList.Count * itemHeight); // * ºüÁü
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

        foreach(ScrollItem item in itemList)
        {
            bool isChange = RelocationItem(item, contentY, scrollHeight);
            if(isChange)
            {
                int idx = (int)(-item.transform.localPosition.y / itemHeight);
                SetData(item, idx);
            }
        }
    }
}
