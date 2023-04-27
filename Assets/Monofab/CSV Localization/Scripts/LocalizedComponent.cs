using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class LocalizedComponent : MonoBehaviour
{
    public string localizedCode;
    [Space]
    //[DrawIf("specificColumnName", true, ComparisonType.Equals, DisablingType.DontDraw)]
    public string localizedColumnName = "CONTENT";

    protected UnityAction onChangeAction;

    public string LocalizedCode { get => localizedCode; set { localizedCode = value; UpdateContent(); } }

    // Start is called before the first frame update
    protected abstract void Start();


    protected abstract void UpdateContent();

    private void OnDestroy()
    {
        if(LocalizationManager.Instance)
        {
            LocalizationManager.Instance.RemoveFunctionFromChangeEvent(onChangeAction);
        }
        
    }

}



