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

        //��Ʈ���� �����ϴ� ���� �󸶳� �ɸ��� ��
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
            //Debug.Log("�̵�");
        }
        // ������ �� ��Ʈ�� ���� �ٸ���
        if (Note_Number==0||(Note_Number> 0 &&NoteList[judgeNumber][Note_Number-1]==null))
        {
            if (judgeNumber == 0 && Input.GetKeyDown(KeyCode.UpArrow) ||             //��
                judgeNumber == 2 && Input.GetKeyDown(KeyCode.RightArrow) ||          //������
                judgeNumber == 4 && Input.GetKeyDown(KeyCode.DownArrow) ||           //�Ʒ�
                judgeNumber == 6 && Input.GetKeyDown(KeyCode.LeftArrow))             //����
                NoteJudge();

            else if (Input.GetKey(KeyCode.UpArrow) &&
                    (judgeNumber == 1 && Input.GetKeyDown(KeyCode.RightArrow) ||     //��Ÿ ������ ��
                    judgeNumber == 7 && Input.GetKeyDown(KeyCode.LeftArrow)))        //��Ÿ ���� ��
                NoteJudge();

            else if (Input.GetKey(KeyCode.DownArrow) &&
                    (judgeNumber == 3 && Input.GetKeyDown(KeyCode.RightArrow) ||     //��Ÿ ������ �Ʒ�
                    judgeNumber == 5 && Input.GetKeyDown(KeyCode.LeftArrow)))        //��Ÿ ���� �Ʒ�
                NoteJudge();

            else if (Input.GetKey(KeyCode.RightArrow) &&
                    (judgeNumber == 1 && Input.GetKeyDown(KeyCode.UpArrow) ||        //��Ÿ ������ ��
                    judgeNumber == 3 && Input.GetKeyDown(KeyCode.DownArrow)))        //��Ÿ ������ �Ʒ�
                NoteJudge();

            else if (Input.GetKey(KeyCode.LeftArrow) &&
                    (judgeNumber == 7 && Input.GetKeyDown(KeyCode.UpArrow) ||        //��Ÿ ���� ��
                    judgeNumber == 5 && Input.GetKeyDown(KeyCode.DownArrow)))        //��Ÿ ���� �Ʒ�
                NoteJudge();
        }
    }

    private void NoteJudge()
    {
        Debug.Log(judgeNumber + "Note: "+ NoteList[judgeNumber].IndexOf(gameObject));

        if (dist <= perfectRange)
        {
            Debug.Log("Perfect!");
            Destroy(gameObject); // ��Ʈ ����
        }
        else if (dist <= goodRange)
        {
            Debug.Log("Good!");
            Destroy(gameObject); // ��Ʈ ����
        }
        else if (dist <= missRange)
        {
            Debug.Log("Miss!");
            Destroy(gameObject); // ��Ʈ ����
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
