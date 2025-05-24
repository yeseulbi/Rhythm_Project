using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class Stt_Graphic : MonoBehaviour
{
    FullScreenMode fullScreenMode;
    [Header("Dropdown")] public Dropdown ScreenSize, FullScreen, Frame;
    private void Start()
    {
        // 초기화
        ScreenSize.value = PlayerPrefs.GetInt("ScreenSize");
        FullScreen.value = PlayerPrefs.GetInt("FullScreen");
        Frame.value = PlayerPrefs.GetInt("FrameRate");

        SetFullScreenSize();
        SetFullScreen();
        SetFrameRate();

        Screen.fullScreenMode = fullScreenMode; // 전체화면 모드 설정
    }

    // 창 크기
    public void SetFullScreenSize()
    {
        switch (ScreenSize.value)
        {
            case 0: Screen.SetResolution(960, 540, fullScreenMode); break;
            case 1: Screen.SetResolution(1280, 720, fullScreenMode); break;
            case 2: Screen.SetResolution(1600, 900, fullScreenMode); break;
            case 3: Screen.SetResolution(1920, 1080, fullScreenMode); break;
            case 4: Screen.SetResolution(2560, 1440, fullScreenMode); break;
            default: break;
        }
        PlayerPrefs.SetInt("ScreenSize", ScreenSize.value);
        PlayerPrefs.Save();
    }
    // 전체화면
    public void SetFullScreen()
    {
        switch (FullScreen.value)
        {
            case 0: fullScreenMode = FullScreenMode.ExclusiveFullScreen; break; // 완전한 전체화면 모드
            case 1: fullScreenMode = FullScreenMode.Windowed; break; // 창모드
            case 2: fullScreenMode = FullScreenMode.FullScreenWindow; break; // 테두리 없는 창 모드
            default: break;
        }
        Screen.fullScreenMode = fullScreenMode; // 전체화면 모드 설정
        PlayerPrefs.SetInt("FullScreen", FullScreen.value);
        PlayerPrefs.Save();
    }
    // 프레임
    public void SetFrameRate()
    {
        int frameRate = 0;
        switch (Frame.value)
        {
            case 0: frameRate = 30; Debug.Log("30"); break;
            case 1: frameRate = 45; Debug.Log("45"); break;
            case 2: frameRate = 60; Debug.Log("60"); break;
            case 3: frameRate = 90; Debug.Log("90"); break;
            case 4: frameRate = 120; Debug.Log("120"); break;
            case 5: frameRate = 144; Debug.Log("144"); break;
            default: break;
        }
        Application.targetFrameRate = frameRate;
        PlayerPrefs.SetInt("FrameRate", Frame.value);
        PlayerPrefs.Save();
    }
    //색각 서포트
    public void SetColorBlindSupport(bool isEnabled)
    {
        // 색각 서포트 기능 구현
        if (isEnabled)
        {
            // 색각 서포트 활성화
        }
        else
        {
            // 색각 서포트 비활성화
        }
    }
}
