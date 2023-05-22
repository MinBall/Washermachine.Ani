using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class StepController : MonoBehaviour
{    
    public Animator animator;
    public LocalizedTMPComponent localizedTMPComponent;
    public Camera mainCamera;
    public Camera uiCamera;
    public LayerMask guideLayer;
    public VideoPlayer Vp;
    public Image Progressbar;
    float timer = 0f;

    public void Awake ()
    {        
        Vp.url = System.IO.Path.Combine(Application.streamingAssetsPath, "SampleClip.mp4");      
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
        timer = 0;
        if (animator.GetInteger("PlayCount") >= 2)
        {
            Native.TestFinish();                     
        }
    }
    
    public static class Native
    {
        [DllImport("__Internal")]
        public static extern void TestFinish ();
    }    

    public void ObjReset()
    {
        CalendarController.action();
    }

    public void ProgressUpdate()
    {
        if (timer <= 1)
            timer += 0.1f;
    }

    private void Update()
    {
        Progressbar.fillAmount = Mathf.Lerp(Progressbar.fillAmount, timer, 2f * Time.deltaTime);
    }
}
