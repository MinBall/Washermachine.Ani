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

            // JSON �����͸� Comment �迭�� ��ȯ�մϴ�.
            Comment[] comments = JsonConvert.DeserializeObject<Comment[]>(json);

            // �ʿ��� �Ӽ� ���� �����Ͽ� TMP_Text�� �Ҵ��մϴ�.
            string text = "";
            foreach (Comment comment in comments)
            {
                text += "ID: " + comment.id + "\n";
                text += "Name: " + comment.name + "\n";
                text += "Email: " + comment.email + "\n\n";
            }

            // TMP_Text�� �Ҵ��Ͽ� ǥ���մϴ�.
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