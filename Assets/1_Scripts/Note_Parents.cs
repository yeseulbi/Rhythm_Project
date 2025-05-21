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

    bool ChildBool = true; // 노트가 생성되었는지 확인하는 변수

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

        //노트까지 도달하는 데에 얼마나 걸리는 지
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
            //Debug.Log("이동");
        }
        if(!Child_Note && GameManager.MusicOn && ChildBool)
        {
            ChildBool = false;
            Debug.Log("자식 없음!");
            SoundEffect.Play();
            //noteParticleSystem.Play();
        }
        if(!ChildBool&& !SoundEffect.isPlaying)
        {
            Destroy(gameObject);
            Debug.Log("부모 노트 파괴");
        }
    }
}