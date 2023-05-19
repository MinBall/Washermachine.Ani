using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using Newtonsoft.Json;
using System.Collections;

public class JsonExample : MonoBehaviour
{
    public string url = "https://jsonplaceholder.typicode.com/comments";
    public TMP_Text textComponent;

    private void Start()
    {
        StartCoroutine(LoadJson());
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

            // 필요한 속성 값을 추출하여 TMP_Text에 할당합니다.
            string text = "";
            foreach (Comment comment in comments)
            {
                text += "ID: " + comment.id + "\n";
                text += "Name: " + comment.name + "\n";
                text += "Email: " + comment.email + "\n\n";
            }

            // TMP_Text에 할당하여 표시합니다.
            textComponent.text = text;
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