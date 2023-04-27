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
    // TurnOffGuide�� 2�� ������� PlayCount�� 2�� ���� ��Ų��. �׷� 2��° ���࿡�� Native.TestFinish�� ����ȴ�.
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