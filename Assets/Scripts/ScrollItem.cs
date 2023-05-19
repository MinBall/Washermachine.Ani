using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollItem : MonoBehaviour
{
    public TMPro.TextMeshProUGUI Name;
    public void SetText(string text)
    {
        Name.text = text;
    }
}
