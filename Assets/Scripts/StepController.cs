using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using UnityEngine;
using UnityEngine.Video;

public class StepController : MonoBehaviour
{
    public Animator animator;
    public LocalizedTMPComponent localizedTMPComponent;
    public Camera mainCamera;
    public Camera uiCamera;
    public LayerMask guideLayer;
    public VideoPlayer Vp;

    Dictionary<int, int> WasherMenu = new Dictionary<int, int>()
    {
        {1, 1800},
        {2, 4000},
        {3, 5200},
        {4, 5000},
        {5, 4000},
        {6, 2600},
        {7, 7200},
        {8, 9800},
        {9, 8800},
        {10, 6800},
    };

    public void Awake ()
    {        
        Vp.url = System.IO.Path.Combine(Application.streamingAssetsPath, "SampleClip.mp4");
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

    public void RandomNumber()
    {
        int result = 0;

        for (int i = 0; i < 3; i++) 
        {
            result = Random.Range(1, 11);
            Debug.Log(WasherMenu[result]);
        }
    }
}
