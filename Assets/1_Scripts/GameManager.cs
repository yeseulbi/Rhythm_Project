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
        //ESC �Ͻ� ���� �г�
        if(!Btn_Background.gameObject.activeSelf&&Input.GetKeyDown(KeyCode.Escape))
        {
            MusicOn = false; // ���� ����
            Btn_Background.gameObject.SetActive(true); // ��ư Ȱ��ȭ
            ESC_Panel.SetActive(true); // ESC �г� Ȱ��ȭ
        }
        else if(Btn_Background.gameObject.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            MusicOn = true; // ���� ���
            Btn_Background.gameObject.SetActive(false); // ��ư ��Ȱ��ȭ
            ESC_Panel.SetActive(false); // ESC �г� ��Ȱ��ȭ
        }
    }

    // ESC �Ͻ� ���� �г� -> ��ư UI �Լ�
    public void Continue_Btn()
    {
        Btn_Background.gameObject.SetActive(false); // ��ư ��Ȱ��ȭ
        ESC_Panel.SetActive(false); // ESC �г� ��Ȱ��ȭ
    }

    public void ReStart_Btn()
    {
        MusicOn = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  //�ش� ��
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
