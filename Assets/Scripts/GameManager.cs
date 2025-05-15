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

    public void Continue_Btn()
    {
        Btn_Background.gameObject.SetActive(false); // 버튼 비활성화
        ESC_Panel.SetActive(false); // ESC 패널 비활성화
    }

    public void Setting_Btn()
    {
        
    }

    public void ReStart_Btn()
    {
        MusicOn = true;
        SceneManager.LoadScene("SampleScene");  //해당 씬
    }

    public void RhythmExit_Btn()
    {
        SceneManager.LoadScene("1_Menu_Scene");
    }

}
