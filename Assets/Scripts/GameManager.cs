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

    public void Continue_Btn()
    {
        Btn_Background.gameObject.SetActive(false); // ��ư ��Ȱ��ȭ
        ESC_Panel.SetActive(false); // ESC �г� ��Ȱ��ȭ
    }

    public void Setting_Btn()
    {
        
    }

    public void ReStart_Btn()
    {
        MusicOn = true;
        SceneManager.LoadScene("SampleScene");  //�ش� ��
    }

    public void RhythmExit_Btn()
    {
        SceneManager.LoadScene("1_Menu_Scene");
    }

}
