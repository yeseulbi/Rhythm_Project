using System.Collections.Generic;
using UnityEngine;

public class Note_Check : MonoBehaviour
{
    //public Transform transformPrb;
    static public float timeToReach;

    public float perfectRange = 0.2f;
    public float goodRange = 0.5f;
    public float missRange = 1.0f;
    [SerializeField]float NoteSpeed=5f;

    Vector3 judgeVec;

    float dist;
    int Note_Number;
    List<GameObject>[] NoteList = new List<GameObject>[8];

    int judgeNumber;
    public void Init(int line)
    {
        judgeNumber = line;
    }
    private void Start()
    {
        judgeVec = Note_Manager.inst.judgeLine[judgeNumber].position;
        NoteList[judgeNumber] = Note_Manager.inst.NoteList[judgeNumber];

        //노트까지 도달하는 데에 얼마나 걸리는 지
        float distance = Vector2.Distance(transform.position, judgeVec);
        timeToReach = distance / NoteSpeed;

        //transformPrb = GetComponent<Transform>(); 
        NoteList[judgeNumber].Add(gameObject);
        Note_Number = NoteList[judgeNumber].IndexOf(gameObject);
    }
    void Update()
    {
        dist = Vector2.Distance(transform.position, judgeVec);
        if (gameObject != null&& GameManager.MusicOn)
        {
            transform.Translate(Vector3.up * NoteSpeed * Time.deltaTime);
            //Debug.Log("GameTimer: "+timeToReach);
            //Debug.Log("이동");
        }
        // 방향은 각 노트에 따라 다르게
        if (Note_Number==0||(Note_Number> 0 &&NoteList[judgeNumber][Note_Number-1]==null))
        {
            if (judgeNumber == 0 && Input.GetKeyDown(KeyCode.UpArrow) ||             //위
                judgeNumber == 2 && Input.GetKeyDown(KeyCode.RightArrow) ||          //오른쪽
                judgeNumber == 4 && Input.GetKeyDown(KeyCode.DownArrow) ||           //아래
                judgeNumber == 6 && Input.GetKeyDown(KeyCode.LeftArrow))             //왼쪽
                NoteJudge();

            else if (Input.GetKey(KeyCode.UpArrow) &&
                    (judgeNumber == 1 && Input.GetKeyDown(KeyCode.RightArrow) ||     //동타 오른쪽 위
                    judgeNumber == 7 && Input.GetKeyDown(KeyCode.LeftArrow)))        //동타 왼쪽 위
                NoteJudge();

            else if (Input.GetKey(KeyCode.DownArrow) &&
                    (judgeNumber == 3 && Input.GetKeyDown(KeyCode.RightArrow) ||     //동타 오른쪽 아래
                    judgeNumber == 5 && Input.GetKeyDown(KeyCode.LeftArrow)))        //동타 왼쪽 아래
                NoteJudge();

            else if (Input.GetKey(KeyCode.RightArrow) &&
                    (judgeNumber == 1 && Input.GetKeyDown(KeyCode.UpArrow) ||        //동타 오른쪽 위
                    judgeNumber == 3 && Input.GetKeyDown(KeyCode.DownArrow)))        //동타 오른쪽 아래
                NoteJudge();

            else if (Input.GetKey(KeyCode.LeftArrow) &&
                    (judgeNumber == 7 && Input.GetKeyDown(KeyCode.UpArrow) ||        //동타 왼쪽 위
                    judgeNumber == 5 && Input.GetKeyDown(KeyCode.DownArrow)))        //동타 왼쪽 아래
                NoteJudge();
        }
    }

    private void NoteJudge()
    {
        Debug.Log(judgeNumber + "Note: "+ NoteList[judgeNumber].IndexOf(gameObject));

        if (dist <= perfectRange)
        {
            Debug.Log("Perfect!");
            Destroy(gameObject); // 노트 제거
        }
        else if (dist <= goodRange)
        {
            Debug.Log("Good!");
            Destroy(gameObject); // 노트 제거
        }
        else if (dist <= missRange)
        {
            Debug.Log("Miss!");
            Destroy(gameObject); // 노트 제거
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log(judgeNumber + "Note: " + NoteList[judgeNumber].IndexOf(gameObject));
            Debug.Log("Miss");
            Destroy(gameObject);
        }
    }
}
