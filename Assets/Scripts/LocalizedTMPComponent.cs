using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LocalizedTMPComponent : LocalizedComponent
{
    TextMeshProUGUI textComponent;

    protected override void Start ()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        onChangeAction = UpdateContent;
        LocalizationManager.Instance.AddFunctionToChangeEvent(onChangeAction);
        UpdateContent();
    }

    protected override void UpdateContent ()
    {
        textComponent.text = LocalizationManager.Instance.GetStringFromCode(localizedCode, textComponent.text);
    }

}
