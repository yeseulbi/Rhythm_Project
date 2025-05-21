using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Line { Up, UpRight, Right, DownRight, Down, DownLeft, Left, UpLeft }
public class NoteData
{
    public float hitTime; // ������ ���� �ð� (����� �ð� ����)
    public int linePosition;
    public int spriteIndex; // �߰�
    public int effectIndex; // ȿ���� �ε���
    public bool spawned = false;
}

public class Note_Manager : MonoBehaviour
{
    [SerializeField] private Sprite[] noteSprites; // Inspector���� ��������Ʈ �Ҵ�
    //[SerializeField] private 
    [SerializeField] private AudioSource[] audioSource;
    [SerializeField] private AudioClip[] SoundEffectClip; // ȿ���� �迭
    AudioSource EffectaudioSource; // ȿ���� ����� �ҽ�

    int Audio=0;  // ����� �ε���
    public float MusicTime => audioSource[Audio].time;

    private List<NoteData> allNotes = new List<NoteData>();

    public static Note_Manager inst;
    
    public Transform[] judgeLine; // �߽� ���� ���� ��ġ, ���������� �ð�������� 01234567
    public List<GameObject>[] NoteList_Parents = new List<GameObject>[8]; // �θ� ��Ʈ ����Ʈ
    public List<GameObject>[] NoteList = new List<GameObject>[8];  //��Ʈ ����Ʈ;

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
    private void Update()    //update���� if�� ���
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
            audioSource[Audio].time = AudioTimeRemember; // ������ ���� �ð����� ���
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
                SpawnNote(note.linePosition, note.spriteIndex, note.effectIndex); // spriteIndex, effectIndex ����
                note.spawned = true;
            }
        }
    }
    private void SpawnNote(int line, int spriteIndex, int effectIndex)
    {
        // ��ġ ����, ����, judgeNumber ��������
        Vector3 spawnPos = GetSpawnPosition(line);
        GameObject obj_Parents = Instantiate(NotePrefab_Parents, spawnPos, judgeLine[line].rotation);   // �θ� ��Ʈ ����(transform ����, ����Ʈ/ȿ���� ����)
        GameObject obj = Instantiate(NotePrefab, obj_Parents.transform);                                // �ڽ� ��Ʈ ����(SpriteRenderer, ���� ����)
        obj_Parents.GetComponent<Note_Parents>().Init(line);    // �θ� ��Ʈ�� judgeNumber �Է�
        obj.GetComponent<Note_Check>().Init(line);              // �ڽ� ��Ʈ�� judgeNumber �Է�

        //ȿ���� ����
        var AddEffect = obj_Parents.GetComponent<AudioSource>();
        if (AddEffect != null && SoundEffectClip != null && effectIndex >= 0 && effectIndex < SoundEffectClip.Length)
            AddEffect.clip= SoundEffectClip[effectIndex]; // ȿ���� �ֱ�
        
        // ��������Ʈ ������ ����
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