using System.Collections.Generic;
using UnityEngine;

public class Note_Parents : MonoBehaviour
{
    //public Transform transformPrb;
    static public float timeToReach;

    [SerializeField] float NoteSpeed = 5f;

    GameObject Child_Note;
    ParticleSystem noteParticleSystem;
    AudioSource SoundEffect;
    Vector3 judgeVec;

    bool ChildBool = true; // ��Ʈ�� �����Ǿ����� Ȯ���ϴ� ����

    int Note_Number;
    List<GameObject>[] NoteParentsList = new List<GameObject>[8];

    int judgeNumber;
    public void Init(int line)
    {
        judgeNumber = line;
    }
    private void Start()
    {
        Transform child = transform.GetChild(0);
        Child_Note = child.gameObject;
        noteParticleSystem = GetComponent<ParticleSystem>();
        SoundEffect = GetComponent<AudioSource>();

        judgeVec = Note_Manager.inst.judgeLine[judgeNumber].position;
        NoteParentsList[judgeNumber] = Note_Manager.inst.NoteList_Parents[judgeNumber];

        //��Ʈ���� �����ϴ� ���� �󸶳� �ɸ��� ��
        float distance = Vector2.Distance(transform.position, judgeVec);
        timeToReach = distance / NoteSpeed;

        //transformPrb = GetComponent<Transform>(); 
        NoteParentsList[judgeNumber].Add(gameObject);
    }
    void Update()
    {
        if (gameObject != null && GameManager.MusicOn && ChildBool)
        {
            transform.Translate(Vector3.up * NoteSpeed * Time.deltaTime);
            //Debug.Log("GameTimer: "+timeToReach);
            //Debug.Log("�̵�");
        }
        if(!Child_Note && GameManager.MusicOn && ChildBool)
        {
            ChildBool = false;
            Debug.Log("�ڽ� ����!");
            SoundEffect.Play();
            //noteParticleSystem.Play();
        }
        if(!ChildBool&& !SoundEffect.isPlaying)
        {
            Destroy(gameObject);
            Debug.Log("�θ� ��Ʈ �ı�");
        }
    }
}