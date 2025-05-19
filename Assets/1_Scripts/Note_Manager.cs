using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Line { Up, UpRight, Right, DownRight, Down, DownLeft, Left, UpLeft }
public class NoteData
{
    public float hitTime; // ������ ���� �ð� (����� �ð� ����)
    public int linePosition;
    public int spriteIndex; // �߰�
    public bool spawned = false;
}

public class Note_Manager : MonoBehaviour
{
    [SerializeField] private Sprite[] noteSprites; // Inspector���� ��������Ʈ �Ҵ�
    //[SerializeField] private 
    [SerializeField] private AudioSource[] audioSource;
    int Audio=0;  // ����� �ε���
    public float MusicTime => audioSource[Audio].time;

    private List<NoteData> allNotes = new List<NoteData>();

    public static Note_Manager inst;
    
    public Transform[] judgeLine; // �߽� ���� ���� ��ġ, ���������� �ð�������� 01234567
    public List<GameObject>[] NoteList = new List<GameObject>[8];  //��Ʈ ����Ʈ;

    float timeToReach;
    [SerializeField] GameObject NotePrefab;
    
    [HideInInspector]public int judgeNumber;
    void Awake()
    {
        inst = this;
        for (int i = 0; i < NoteList.Length; i++)
        {
            NoteList[i] = new List<GameObject>();
        }
    }
    private void Start()
    {
        NoteSpawn(0, Line.Up);
        NoteSpawn(2.5f, Line.Left);
        NoteSpawn(3.0f, Line.Left);
        NoteSpawn(3.5f, Line.Left);
        NoteSpawn(4.0f, Line.Left);
        NoteSpawn(4.5f, Line.Left);
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
            timeToReach = Note_Check.timeToReach;
            if (!note.spawned && MusicTime >= (note.hitTime - timeToReach))
            {
                SpawnNote(note.linePosition, note.spriteIndex); // spriteIndex ����
                note.spawned = true;
            }
        }

    }
    private void SpawnNote(int line, int spriteIndex)
    {
        // ��ġ ����, ����, judgeNumber ��������
        Vector3 spawnPos = GetSpawnPosition(line);
        GameObject obj = Instantiate(NotePrefab, spawnPos, judgeLine[line].rotation);
        obj.GetComponent<Note_Check>().Init(line);

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
    void NoteSpawn(float time, Line line, int spriteIndex)
    {
        allNotes.Add(new NoteData { hitTime = time, linePosition = (int)line, spriteIndex = spriteIndex });
    }
    void NoteSpawn(float time, int line, int spriteIndex)
    {
        allNotes.Add(new NoteData { hitTime = time, linePosition = line, spriteIndex = spriteIndex });
    }
    void NoteSpawn(float time, Line line)
    {
        allNotes.Add(new NoteData { hitTime = time, linePosition = (int)line });
    }
}