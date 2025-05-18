using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Stage_Manager : MonoBehaviour
{
    [SerializeField] Button[] Stage_Btn;
    [SerializeField] GameObject SpaceShip;
    int StageIndex = 0;
    void Start()
    {
        Debug.Log(Stage_Btn.Length);
    }

    void Update()
    {
        //스테이지 이동 코드
        Vector3 SpaceShipVec = new Vector3(Stage_Btn[StageIndex].gameObject.transform.position.x, Stage_Btn[StageIndex].gameObject.transform.position.y+2.1f);
        SpaceShip.transform.position = SpaceShipVec;
        if (Input.GetKeyDown(KeyCode.LeftArrow)&&StageIndex < Stage_Btn.Length-1)
        {
            ++StageIndex;
            Debug.Log(StageIndex);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && StageIndex > 0)
            --StageIndex;
        else if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("Menu_Scene");

        //스테이지 선택 코드
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (StageIndex == 0)
                SceneManager.LoadScene("SampleScene");
            else if (StageIndex == 1)
                SceneManager.LoadScene("3_Stage_Scene");
            else if (StageIndex == 2)
                SceneManager.LoadScene("4_Stage_Scene");
            else if (StageIndex == 3)
                SceneManager.LoadScene("5_Stage_Scene");
        }
    }


}
