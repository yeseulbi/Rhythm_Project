using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    [SerializeField]Button[] Rhythm_Btn_Arr = new Button[5];
    [SerializeField]Image Btn_Background;
    public void Continue_Btn()
    {
        
    }

    public void Setting_Btn()
    {
        
    }

    public void ReStart_Btn()
    {
        SceneManager.LoadScene("SampleScene");  //ÇØ´ç ¾À
    }

    public void RhythmExit_Btn()
    {
        SceneManager.LoadScene("1_Menu_Scene");
    }

}
