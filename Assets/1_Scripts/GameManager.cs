using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] GameObject ESC_Panel;
    [SerializeField]Image Btn_Background;

    public static bool MusicOn = true;
    void Start()
    {

    }

    void Update()
    {
        //ESC 일시 정지 패널
        if(!Btn_Background.gameObject.activeSelf&&Input.GetKeyDown(KeyCode.Escape))
        {
            MusicOn = false; // 음악 정지
            Btn_Background.gameObject.SetActive(true); // 버튼 활성화
            ESC_Panel.SetActive(true); // ESC 패널 활성화
        }
        else if(Btn_Background.gameObject.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            MusicOn = true; // 음악 재생
            Btn_Background.gameObject.SetActive(false); // 버튼 비활성화
            ESC_Panel.SetActive(false); // ESC 패널 비활성화
        }
    }

    // ESC 일시 정지 패널 -> 버튼 UI 함수
    public void Continue_Btn()
    {
        Btn_Background.gameObject.SetActive(false); // 버튼 비활성화
        ESC_Panel.SetActive(false); // ESC 패널 비활성화
    }

    public void ReStart_Btn()
    {
        MusicOn = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  //해당 씬
    }

    public void RhythmExit_Btn()
    {
        SceneManager.LoadScene("Menu_Scene");
    }
    public void Setting_Btn()
    {
        
    }

    /////////////////////////////Setting Btn 0.Graphic 1.Sound 2.System 3.GameOff 4.Close
    
    // 0.Graphic
    public void Graphic_Btn()
    {
        Debug.Log("Graphic Button Clicked");
    }
    
    // 1.Sound
    public void Sound_Btn()
    {
        Debug.Log("Sound Button Clicked");
    }
    
    // 2.System
    public void System_Btn()
    {
        Debug.Log("System Button Clicked");
    }
    
    // 3.GameOff
    public void GameOff_Btn()
    {
        Application.Quit();
    }
}
