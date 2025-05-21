using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Line { Up, UpRight, Right, DownRight, Down, DownLeft, Left, UpLeft }
public class NoteData
{
    public float hitTime; // 판정선 도달 시간 (오디오 시간 기준)
    public int linePosition;
    public int spriteIndex; // 추가
    public int effectIndex; // 효과음 인덱스
    public bool spawned = false;
}

public class Note_Manager : MonoBehaviour
{
    [SerializeField] private Sprite[] noteSprites; // Inspector에서 스프라이트 할당
    //[SerializeField] private 
    [SerializeField] private AudioSource[] audioSource;
    [SerializeField] private AudioClip[] SoundEffectClip; // 효과음 배열
    AudioSource EffectaudioSource; // 효과음 오디오 소스

    int Audio=0;  // 오디오 인덱스
    public float MusicTime => audioSource[Audio].time;

    private List<NoteData> allNotes = new List<NoteData>();

    public static Note_Manager inst;
    
    public Transform[] judgeLine; // 중심 판정 라인 위치, 위에서부터 시계방향으로 01234567
    public List<GameObject>[] NoteList_Parents = new List<GameObject>[8]; // 부모 노트 리스트
    public List<GameObject>[] NoteList = new List<GameObject>[8];  //노트 리스트;

    float timeToReach;
    [SerializeField] GameObject NotePrefab_Parents, NotePrefab;
    
    [HideInInspector]public int judgeNumber;
    void Awake()
    {
        inst = this;
        EffectaudioSource = GetComponent<AudioSource>();
        for (int i = 0; i < NoteList_Parents.Length; i++)
        {
            NoteList_Parents[i] = new List<GameObject>();
        }
        for (int i = 0; i < NoteList.Length; i++)
        {
            NoteList[i] = new List<GameObject>();
        }
    }
    private void Start()
    {
        NoteSpawn(0, Line.Up,0,0);
        NoteSpawn(2.5f, Line.Left,0,0);
        NoteSpawn(3.0f, Line.Left, 0, 0);
        NoteSpawn(3.5f, Line.Left, 0, 0);
        NoteSpawn(4.0f, Line.Left, 0, 0);
        NoteSpawn(4.5f, Line.Left, 0, 0);
    }
    float AudioTimeRemember;
    private void Update()    //update에서 if문 사용
    {
        //Debug.Log("Timer: "+GameManager.GameTimer);
        //Debug.Log("MusicTime: "+ audioSource[Audio].time);
        if (audioSource[Audio].isPlaying&& !GameManager.MusicOn)
        {
            AudioTimeRemember = audioSource[Audio].time;
            audioSource[Audio].Stop();
        }
        else if (!audioSource[Audio].isPlaying && GameManager.MusicOn)
        {
            audioSource[Audio].Play();
            audioSource[Audio].time = AudioTimeRemember; // 이전에 멈춘 시간으로 재생
        }
        /*else
        {
            Debug.Log("GameTimer: " + audioSource[Audio].time);
            //Debug.Log("Key: None");
        }*/

        /*if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.Log("GameTimer: "+audioSource[Audio].time);
            //Debug.Log("Key: Right");
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Debug.Log("GameTimer: " + audioSource[Audio].time);
            //Debug.Log("Key: Left");
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow)|| Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("GameTimer: " + audioSource[Audio].time);
            //Debug.Log("Key: Up and Down");
        }*/

        foreach (var note in allNotes)
        {
            timeToReach = Note_Parents.timeToReach;
            if (!note.spawned && MusicTime >= (note.hitTime - timeToReach))
            {
                SpawnNote(note.linePosition, note.spriteIndex, note.effectIndex); // spriteIndex, effectIndex 전달
                note.spawned = true;
            }
        }
    }
    private void SpawnNote(int line, int spriteIndex, int effectIndex)
    {
        // 위치 지정, 생성, judgeNumber 가져오기
        Vector3 spawnPos = GetSpawnPosition(line);
        GameObject obj_Parents = Instantiate(NotePrefab_Parents, spawnPos, judgeLine[line].rotation);   // 부모 노트 생성(transform 조정, 이펙트/효과음 제어)
        GameObject obj = Instantiate(NotePrefab, obj_Parents.transform);                                // 자식 노트 생성(SpriteRenderer, 판정 제어)
        obj_Parents.GetComponent<Note_Parents>().Init(line);    // 부모 노트에 judgeNumber 입력
        obj.GetComponent<Note_Check>().Init(line);              // 자식 노트에 judgeNumber 입력

        //효과음 변경
        var AddEffect = obj_Parents.GetComponent<AudioSource>();
        if (AddEffect != null && SoundEffectClip != null && effectIndex >= 0 && effectIndex < SoundEffectClip.Length)
            AddEffect.clip= SoundEffectClip[effectIndex]; // 효과음 넣기
        
        // 스프라이트 렌더러 변경
        var sr = obj.GetComponent<SpriteRenderer>();
        if (sr != null && noteSprites != null && spriteIndex >= 0 && spriteIndex < noteSprites.Length)
            sr.sprite = noteSprites[spriteIndex];

    }

    private Vector3 GetSpawnPosition(int where)
    {
        Vector3 SpawnPos = Vector3.zero;

        switch (where)
        {
            case 0: // Up
                SpawnPos.y = 5.1f;
                break;

            case 1: // UpRight
                SpawnPos.y = 5.1f;
                SpawnPos.x = 5.1f;
                break;

            case 2: // Right
                SpawnPos.y = 0;
                SpawnPos.x = 5.1f;
                break;

            case 3: // DownRight
                SpawnPos.y = -5.1f;
                SpawnPos.x = 5.1f;
                break;

            case 4: // Down
                SpawnPos.y = -5.1f;
                break;

            case 5: // DownLeft
                SpawnPos.y = -5.1f;
                SpawnPos.x = -5.1f;
                break;

            case 6: // Left
                SpawnPos.y = 0;
                SpawnPos.x = -5.1f;
                break;

            case 7: // UpLeft
                SpawnPos.y = 5.1f;
                SpawnPos.x = -5.1f;
                break;
        }
        return SpawnPos;
    }
    void NoteSpawn(float time, Line line, int spriteIndex, int EftIndex)
    {
        allNotes.Add(new NoteData { hitTime = time, linePosition = (int)line, spriteIndex = spriteIndex, effectIndex = EftIndex });
    }
    void NoteSpawn(float time, int line, int spriteIndex, int EftIndex)
    {
        allNotes.Add(new NoteData { hitTime = time, linePosition = line, spriteIndex = spriteIndex, effectIndex = EftIndex });
    }
    void NoteSpawn(float time, Line line, int spriteIndex)
    {
        allNotes.Add(new NoteData { hitTime = time, linePosition = (int)line, spriteIndex = spriteIndex, effectIndex = 0 });
    }
    void NoteSpawn(float time, int line, int spriteIndex)
    {
        allNotes.Add(new NoteData { hitTime = time, linePosition = line, spriteIndex = spriteIndex, effectIndex = 0 });
    }
    void NoteSpawn(float time, Line line)
    {
        allNotes.Add(new NoteData { hitTime = time, linePosition = (int)line });
    }
}