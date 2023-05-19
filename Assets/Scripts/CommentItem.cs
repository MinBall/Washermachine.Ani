using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;

public class CommentItem : MonoBehaviour
{
    [SerializeField] private TMP_Text idText;
    [SerializeField] private TMP_Text emailText;
    [SerializeField] private TMP_Text nameText;

    public void SetComment(Comment comment)
    {
        {
            idText.text = comment.id.ToString();
            emailText.text = comment.email;
            nameText.text = comment.name;
        }
    }
}

