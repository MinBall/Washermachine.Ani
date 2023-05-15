using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;

public class JsonExample : MonoBehaviour, IDragHandler
{
    public string url = "https://jsonplaceholder.typicode.com/comments";
    public GameObject commentPrefab;
    public RectTransform content;
    public Scrollbar scrollbar;

    private List<Comment> comments = new List<Comment>();
    private List<GameObject> commentObjects = new List<GameObject>();
    private float itemHeight = 150f;
    private float scrollOffset = 0f;
    private float contentHeight = 0f;

    private void Start()
    {
        StartCoroutine(LoadComments());
    }

    IEnumerator LoadComments()
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("Error loading comments");
        }
        else
        {
            string json = www.downloadHandler.text;
            comments = JsonUtility.FromJson<CommentListJson>(json).comments;
            GenerateCommentList();
        }
    }

    private void GenerateCommentList()
    {
        int numItems = comments.Count;
        contentHeight = numItems * itemHeight;
        content.sizeDelta = new Vector2(content.sizeDelta.x, contentHeight);

        for (int i = 0; i < numItems; i++)
        {
            GameObject commentObject;

            if (i < commentObjects.Count)
            {
                commentObject = commentObjects[i];
            }
            else
            {
                commentObject = Instantiate(commentPrefab, content);
                commentObjects.Add(commentObject);
            }

            commentObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, -i * itemHeight);
            commentObject.GetComponent<CommentItem>().SetComment(comments[i]);
        }
    }

    private void Update()
    {
        float scrollValue = scrollbar.value;
        float newScrollOffset = scrollValue * contentHeight;
        content.anchoredPosition = new Vector2(0f, newScrollOffset);
        float scrollDelta = newScrollOffset - scrollOffset;

        if (scrollDelta != 0f)
        {
            UpdateCommentPositions(scrollDelta);
        }

        scrollOffset = newScrollOffset;
    }

    private void UpdateCommentPositions(float scrollDelta)
    {
        int numItems = comments.Count;

        for (int i = 0; i < commentObjects.Count; i++)
        {
            GameObject commentObject = commentObjects[i];
            RectTransform commentTransform = commentObject.GetComponent<RectTransform>();
            Vector2 newPosition = commentTransform.anchoredPosition + new Vector2(0f, scrollDelta);

            if (newPosition.y > 0f)
            {
                newPosition.y -= contentHeight;
            }
            else if (newPosition.y < -contentHeight)
            {
                newPosition.y += contentHeight;
            }

            commentTransform.anchoredPosition = newPosition;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        float scrollValue = scrollbar.value;
        scrollValue += eventData.delta.y / contentHeight;
        scrollValue = Mathf.Clamp(scrollValue, 0f, 1f);
        scrollbar.value = scrollValue;
    }
}

[System.Serializable]
public class CommentListJson
{
    public List<Comment> comments;
}

[System.Serializable]
public class Comment
{
    public int postId;
    public int id;
    public string name;
    public string email;
    public string body;
}