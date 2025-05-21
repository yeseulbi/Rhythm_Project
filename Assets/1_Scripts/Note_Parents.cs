using System.Collections.Generic;
using UnityEngine;

public class Note_Parents : MonoBehaviour
{
    // 이펙트는 배열을 사용하여 judgeNumber에 따라 다르게 설정할 예정이다.(방향, 모양 등을)
    static public float timeToReach;

    [SerializeField] float NoteSpeed = 5f;

    GameObject Child_Note;
    ParticleSystem noteParticleSystem;
    AudioSource SoundEffect;
    Vector3 judgeVec;

    List<GameObject>[] NoteParentsList = new List<GameObject>[8];

    bool ChildBool = true; // 노트가 생성되었는지 확인하는 변수
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
        if(!Child_Note && GameManager.MusicOn && ChildBool) // 자식노트가 없는데 collider가 충돌했을 때도 체크X(부모노트와 자식노트의 collider 크기는 같다.)
        {
            ChildBool = false;
            //Debug.Log("자식 없음!");
            SoundEffect.Play();
            noteParticleSystem.Play();
        }
        if(!ChildBool&& !SoundEffect.isPlaying) // 효과음이 재생되지 않을 때가 기준이지만, 이펙트를 추가했을 때 이펙트가 기준이 된다. 또는 시간을 기준으로
        {
            Destroy(gameObject);
            //Debug.Log("부모 노트 파괴");
        }
    }
}