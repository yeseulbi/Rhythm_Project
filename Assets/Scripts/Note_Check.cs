using DG.Tweening;
using UnityEngine;



public class Note_Check : MonoBehaviour
{
    public Transform transformPrb;

    public float perfectRange = 0.2f;
    public float goodRange = 0.5f;
    public float missRange = 1.0f;

    float Speed=5f;
    Vector3 judgeVec;

    float dist;
    int judgeNumber;
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
            transform.Translate(Vector3.up * Speed * Time.deltaTime);
            //Debug.Log("이동");
        }

        if (judgeNumber==0&&Input.GetKeyDown(KeyCode.UpArrow)) // 방향은 각 노트에 따라 다르게
            NoteJudge();
        else if(judgeNumber==2&& Input.GetKeyDown(KeyCode.RightArrow))
            NoteJudge();
        else if (judgeNumber == 4 && Input.GetKeyDown(KeyCode.DownArrow))
            NoteJudge();
        else if (judgeNumber == 6 && Input.GetKeyDown(KeyCode.LeftArrow))
            NoteJudge();
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            if (judgeNumber==1&& Input.GetKeyDown(KeyCode.RightArrow))
            {
                NoteJudge();
            }
            else if(judgeNumber==7&& Input.GetKeyDown(KeyCode.LeftArrow))
            {
                NoteJudge();
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            if (judgeNumber == 3 && Input.GetKeyDown(KeyCode.RightArrow))
            {
                NoteJudge();
            }
            else if (judgeNumber == 5 && Input.GetKeyDown(KeyCode.LeftArrow))
            {
                NoteJudge();
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (judgeNumber == 1 && Input.GetKeyDown(KeyCode.UpArrow))
            {
                NoteJudge();
            }
            else if (judgeNumber == 3 && Input.GetKeyDown(KeyCode.DownArrow))
            {
                NoteJudge();
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (judgeNumber == 7 && Input.GetKeyDown(KeyCode.UpArrow))
            {
                NoteJudge();
            }
            else if (judgeNumber == 5 && Input.GetKeyDown(KeyCode.DownArrow))
            {
                NoteJudge();
            }
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
