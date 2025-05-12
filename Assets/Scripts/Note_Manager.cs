using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Line { Up, UpRight, Right, DownRight, Down, DownLeft, Left, UpLeft }
public class NoteData
{
    public float hitTime; // 판정선 도달 시간 (오디오 시간 기준)
    public int linePosition;
    public bool spawned = false;
}

public class Note_Manager : MonoBehaviour
{
    [SerializeField] private AudioSource[] audioSource;
    int Audio=0;  // 오디오 인덱스
    public float MusicTime => audioSource[Audio].time;

    private List<NoteData> allNotes = new List<NoteData>();

    public static Note_Manager inst;
    
    public Transform[] judgeLine; // 중심 판정 라인 위치, 위에서부터 시계방향으로 01234567
    public List<GameObject>[] NoteList = new List<GameObject>[8];  //노트 리스트;

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
        NoteSpawn(0,Line.Up);
        NoteSpawn(1.6f,Line.Right);
        NoteSpawn(3.4f, Line.Right);
        NoteSpawn(4.9f, Line.Right);
        NoteSpawn(5.7f, Line.Right);
        NoteSpawn(6.6f, Line.Right);
        NoteSpawn(6.9f, Line.Right);
        NoteSpawn(7.38f, Line.Right);
        NoteSpawn(7.78f, Line.Right);
        NoteSpawn(8.2f, Line.Right);
        NoteSpawn(8.59f, Line.Right);
        NoteSpawn(8.9f, Line.Right);
        NoteSpawn(9.4f, Line.Right);
        NoteSpawn(9.79f, Line.Right);

        NoteSpawn(10.17f, Line.Right);
        NoteSpawn(10.55f, Line.Right);
        NoteSpawn(10.79f, Line.Right);
        NoteSpawn(11f, Line.Left);
        NoteSpawn(11.11f, Line.Right);
        NoteSpawn(11.19f, Line.Left);
        NoteSpawn(11.28f, Line.Right);

    }

    private void Update()    //update에서 if문 사용
    {
        //Debug.Log("Timer: "+GameManager.GameTimer);
        //Debug.Log("MusicTime: "+ audioSource[Audio].time);

        if (Input.GetKeyDown(KeyCode.RightArrow))
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
        }

        foreach (var note in allNotes)
        {
            timeToReach = Note_Check.timeToReach;
            if (!note.spawned && MusicTime >= (note.hitTime - timeToReach))
            {
                SpawnNote(note.linePosition);
                note.spawned = true;
            }
        }
    }
    private void SpawnNote(int line)
    {
        Vector3 spawnPos = GetSpawnPosition(line);
        GameObject obj = Instantiate(NotePrefab, spawnPos, judgeLine[line].rotation);
        obj.GetComponent<Note_Check>().Init(line);
    }
    private Vector3 GetSpawnPosition(int where)
    {
        Vector3 SpawnPos = Vector3.zero;

        if (where == 0 || where == 1 || where == 7)
        {
            SpawnPos.y = 5.1f;
            if (where == 1)
                SpawnPos.x = 5.1f;
            if (where == 7)
                SpawnPos.x = -5.1f;
        }
        else if (where == 3 || where == 4 || where == 5)
        {
            SpawnPos.y = -5.1f;
            if (where == 3)
                SpawnPos.x = 5.1f;
            if (where == 5)
                SpawnPos.x = -5.1f;
        }
        else
        {
            SpawnPos.y = 0;
            if (where == 2)
                SpawnPos.x = 5.1f;
            if (where == 6)
                SpawnPos.x = -5.1f;
        }

        return SpawnPos;
    }
    void NoteSpawn(float time, Line line)
    {
        allNotes.Add(new NoteData { hitTime = time, linePosition = (int)line });
    }
    void NoteSpawn(float time, int line)
    {
        allNotes.Add(new NoteData { hitTime = time, linePosition = line });
    }
}