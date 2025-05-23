using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Menu_Manager : MonoBehaviour
{
    [SerializeField]GameObject Particle_BG;
    [SerializeField]GameObject Setting;
    [SerializeField]Scrollbar SttScrollbar;

    void Start()
    {
        SttScrollbar.value = 1;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            Setting.SetActive(!Setting.activeSelf);
    }

    public void particleOnOff_Btn()
    {
        Particle_BG.SetActive(!Particle_BG.activeSelf); // 파티클 비활성화
    }

    public void Setting_Btn()
    {
        Setting.SetActive(!Setting.activeSelf);
    }

    /////////////////////////////Setting Btn 0.Graphic 1.Sound 2.System 3.GameOff 4.Close

    // 0.Graphic
    public void Graphic_Btn()
    {
        Debug.Log("Graphic Button Clicked");
        SttScrollbar.value = 1;
    }

    // 1.Sound
    public void Sound_Btn()
    {
        Debug.Log("Sound Button Clicked");
        SttScrollbar.value = 0.4579999f;
    }

    // 2.System
    public void System_Btn()
    {
        Debug.Log("System Button Clicked");
        SttScrollbar.value = 0;
    }

    // 3.GameOff
    public void GameOff_Btn()
    {
        Application.Quit();
    }

    // 4.Close
    public void Close_Btn()
    {
        Setting.SetActive(false);
    }
}
