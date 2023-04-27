using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using UnityEngine;

public class StepController : MonoBehaviour
{
    public Animator animator;
    public LocalizedTMPComponent localizedTMPComponent;
    public Camera mainCamera;
    public Camera uiCamera;
    public LayerMask guideLayer;

    public void Awake ()
    {
        
    }
    public void ChangeMainText (string code)
    {
        localizedTMPComponent.LocalizedCode = code;
    }
    // TurnOffGuide를 2번 실행시켜 PlayCount를 2번 증가 시킨다. 그럼 2번째 실행에서 Native.TestFinish가 실행된다.
    public void TurnOffGuide()
    {
        mainCamera.cullingMask = mainCamera.cullingMask ^ guideLayer;
        uiCamera.cullingMask = uiCamera.cullingMask ^ guideLayer;
        animator.SetInteger("PlayCount", animator.GetInteger("PlayCount") + 1);
        if (animator.GetInteger("PlayCount") >= 2)
            Native.TestFinish();
    }
    
    public static class Native
    {

        [DllImport("__Internal")]
        public static extern void TestFinish ();
    }
}
