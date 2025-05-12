using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Note_Check : MonoBehaviour
{
    public Transform transformPrb;

    [SerializeField]
    public float perfectRange = 0.2f;
    public float goodRange = 0.5f;
    public float missRange = 1.0f;
    float NoteSpeed=5f;

    Vector3 judgeVec;

    float dist;
    int judgeNumber;
    private void Awake()
    {
        Note_Manager.NoteList[judgeNumber].Add(gameObject);
    }
    private void Start()
    {
        judgeNumber = Note_Manager.inst.judgeNumber;
        judgeVec = Note_Manager.inst.judgeLine[judgeNumber].position;
        transformPrb = GetComponent<Transform>(); 
    }
    void Update()
    {
        dist = Vector2.Distance(transform.position, judgeVec);
        if (gameObject != null)
        {
            transform.Translate(Vector3.up * NoteSpeed * Time.deltaTime);
            //Debug.Log("이동");
        }
        if (dist <= missRange)
        {// 방향은 각 노트에 따라 다르게
            if (judgeNumber == 0 && Input.GetKey(KeyCode.UpArrow) ||             //위
                judgeNumber == 2 && Input.GetKey(KeyCode.RightArrow) ||          //오른쪽
                judgeNumber == 4 && Input.GetKey(KeyCode.DownArrow) ||           //아래
                judgeNumber == 6 && Input.GetKey(KeyCode.LeftArrow))             //왼쪽
                NoteJudge();

            else if (Input.GetKey(KeyCode.UpArrow) &&
                    (judgeNumber == 1 && Input.GetKey(KeyCode.RightArrow) ||     //동타 오른쪽 위
                    judgeNumber == 7 && Input.GetKey(KeyCode.LeftArrow)))        //동타 왼쪽 위
                NoteJudge();

            else if (Input.GetKey(KeyCode.DownArrow) &&
                    (judgeNumber == 3 && Input.GetKey(KeyCode.RightArrow) ||     //동타 오른쪽 아래
                    judgeNumber == 5 && Input.GetKey(KeyCode.LeftArrow)))        //동타 왼쪽 아래
                NoteJudge();

            else if (Input.GetKey(KeyCode.RightArrow) &&
                    (judgeNumber == 1 && Input.GetKey(KeyCode.UpArrow) ||        //동타 오른쪽 위
                    judgeNumber == 3 && Input.GetKey(KeyCode.DownArrow)))        //동타 오른쪽 아래
                NoteJudge();

            else if (Input.GetKey(KeyCode.LeftArrow) &&
                    (judgeNumber == 7 && Input.GetKey(KeyCode.UpArrow) ||        //동타 왼쪽 위
                    judgeNumber == 5 && Input.GetKey(KeyCode.DownArrow)))        //동타 왼쪽 아래
                NoteJudge();
        }
    }

    private void NoteJudge()
    {
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
            Debug.Log("Miss!");
            Destroy(gameObject);
        }
    }
}
