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
            //Debug.Log("�̵�");
        }
        if (dist <= missRange)
        {// ������ �� ��Ʈ�� ���� �ٸ���
            if (judgeNumber == 0 && Input.GetKey(KeyCode.UpArrow) ||             //��
                judgeNumber == 2 && Input.GetKey(KeyCode.RightArrow) ||          //������
                judgeNumber == 4 && Input.GetKey(KeyCode.DownArrow) ||           //�Ʒ�
                judgeNumber == 6 && Input.GetKey(KeyCode.LeftArrow))             //����
                NoteJudge();

            else if (Input.GetKey(KeyCode.UpArrow) &&
                    (judgeNumber == 1 && Input.GetKey(KeyCode.RightArrow) ||     //��Ÿ ������ ��
                    judgeNumber == 7 && Input.GetKey(KeyCode.LeftArrow)))        //��Ÿ ���� ��
                NoteJudge();

            else if (Input.GetKey(KeyCode.DownArrow) &&
                    (judgeNumber == 3 && Input.GetKey(KeyCode.RightArrow) ||     //��Ÿ ������ �Ʒ�
                    judgeNumber == 5 && Input.GetKey(KeyCode.LeftArrow)))        //��Ÿ ���� �Ʒ�
                NoteJudge();

            else if (Input.GetKey(KeyCode.RightArrow) &&
                    (judgeNumber == 1 && Input.GetKey(KeyCode.UpArrow) ||        //��Ÿ ������ ��
                    judgeNumber == 3 && Input.GetKey(KeyCode.DownArrow)))        //��Ÿ ������ �Ʒ�
                NoteJudge();

            else if (Input.GetKey(KeyCode.LeftArrow) &&
                    (judgeNumber == 7 && Input.GetKey(KeyCode.UpArrow) ||        //��Ÿ ���� ��
                    judgeNumber == 5 && Input.GetKey(KeyCode.DownArrow)))        //��Ÿ ���� �Ʒ�
                NoteJudge();
        }
    }

    private void NoteJudge()
    {
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
            Debug.Log("Miss!");
            Destroy(gameObject);
        }
    }
}
