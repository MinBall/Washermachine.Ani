using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizedTextComponent : LocalizedComponent
{
    Text textComponent;
    
    protected override void Start()
    {
        textComponent = GetComponent<Text>();
        onChangeAction = UpdateContent;
        LocalizationManager.Instance.AddFunctionToChangeEvent(onChangeAction);
        UpdateContent();
    }

    protected override void UpdateContent()
    {
        textComponent.text = LocalizationManager.Instance.GetStringFromCode(localizedCode, textComponent.text);
    }

}
