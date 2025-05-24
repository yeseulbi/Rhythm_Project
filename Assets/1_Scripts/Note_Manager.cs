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
        NoteSpawn(4.5f, Line.UpRight, 0, 0);
        NoteSpawn(5.0f, Line.UpRight, 0, 0);
        NoteSpawn(5.5f, Line.UpRight, 0, 0);
        NoteSpawn(6.0f, Line.UpRight, 0, 0);
        NoteSpawn(6.5f, Line.UpLeft, 0, 0);
        NoteSpawn(7.0f, Line.UpLeft, 0, 0);
        NoteSpawn(7.5f, Line.DownRight, 0, 0);
        NoteSpawn(8.0f, Line.DownRight, 0, 0);
        NoteSpawn(8.5f, Line.DownLeft, 0, 0);
        NoteSpawn(9.0f, Line.DownLeft, 0, 0);
        NoteSpawn(9.5f, Line.Down, 0, 0);
        NoteSpawn(10.0f, Line.Down, 0, 0);
        NoteSpawn(10.5f, Line.Down, 0, 0);
    }
    float AudioTimeRemember;
    private void Update()    //update���� if�� ���
    {
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
        Vector3 SpawnPos = -judgeLine[line].up * 5.1f;
        GameObject obj_Parents = Instantiate(NotePrefab_Parents, SpawnPos, judgeLine[line].rotation);   // �θ� ��Ʈ ����(transform ����, ����Ʈ/ȿ���� ����)
        GameObject obj = Instantiate(NotePrefab, obj_Parents.transform);                                // �ڽ� ��Ʈ ����(SpriteRenderer, ���� ����)
        obj_Parents.GetComponent<Note_Parents>().Init(line);    // �θ� ��Ʈ�� judgeNumber �Է�
        obj.GetComponent<Note_Check>().Init(line);              // �ڽ� ��Ʈ�� judgeNumber �Է�

        //ȿ���� ����
        var AddEffect = obj_Parents.GetComponent <AudioSource>();
        AddEffect.resource = SoundEffectClip[effectIndex]; // ȿ���� �ֱ�

        // ��������Ʈ ������ ����
        var sr = obj.GetComponent<SpriteRenderer>();
        if (sr != null && noteSprites != null && spriteIndex >= 0 && spriteIndex < noteSprites.Length)
            sr.sprite = noteSprites[spriteIndex];

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