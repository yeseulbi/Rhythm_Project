using UnityEngine;
using UnityEngine.UI;

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
        Vector3 SpaceShipVec = new Vector3(Stage_Btn[StageIndex].gameObject.transform.position.x, Stage_Btn[StageIndex].gameObject.transform.position.y+216f);
        SpaceShip.transform.position = SpaceShipVec;
        if (Input.GetKeyDown(KeyCode.LeftArrow)&&StageIndex < Stage_Btn.Length-1)
        {
            ++StageIndex;
            Debug.Log(StageIndex);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && StageIndex > 0)
            --StageIndex;
    }
}
