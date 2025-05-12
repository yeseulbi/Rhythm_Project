using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Line { Up, UpRight, Right, DownRight, Down, DownLeft, Left, UpLeft }
public class NoteData
{
    public float hitTime; // 판정선 도달 시간 (오디오 시간 기준)
    public Line linePosition;
    public bool spawned = false;
}

public class Note_Manager : MonoBehaviour
{
    [SerializeField] private AudioSource[] audioSource;
    int Audio=0;  // 오디오 인덱스
    public float MusicTime => audioSource[Audio].time;

    private List<NoteData> allNotes = new List<NoteData>();

    bool hasTriggered, hasTriggered1 = false;

    public static Note_Manager inst;
    
    public Transform[] judgeLine; // 중심 판정 라인 위치, 위에서부터 시계방향으로 01234567
    public List<GameObject>[] NoteList = new List<GameObject>[8];  //노트 리스트
    bool[] NoteArray = new bool[125];
    int NoteArrayNumber;

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
        NoteSpawn(6.5f, Line.Right);
        NoteSpawn(7.5f, Line.Right);
    }

    private void Update()    //update에서 if문 사용
    {
        //Debug.Log("Timer: "+GameManager.GameTimer);
        //Debug.Log("MusicTime: "+ audioSource[Audio].time);

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.Log("GameTimer: "+audioSource[Audio].time);
            Debug.Log("Key: Right");
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Debug.Log("GameTimer: " + audioSource[Audio].time);
            Debug.Log("Key: Left");
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow)|| Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("GameTimer: " + audioSource[Audio].time);
            Debug.Log("Key: Up and Down");
        }

        float currentTime = MusicTime;

        foreach (var note in allNotes)
        {
            timeToReach = Note_Check.timeToReach;
            if (!note.spawned && currentTime >= (note.hitTime - timeToReach))
            {
                SpawnNote(note.linePosition);
                note.spawned = true;
            }
        }
    }

/*    public void NoteSpawn(double Spawntime, Line Where) //Spawntime은 생성될 시간, Where은 생성되는 노트 위치
    {
        timeToReach = Note_Check.timeToReach;
        NoteCoroutine((float)Spawntime, (int)Where);
    }
    public void NoteSpawn(double Spawntime, int Where) //Spawntime은 생성될 시간, Where은 생성되는 노트 위치
    {
        timeToReach = Note_Check.timeToReach;
        NoteCoroutine((float)Spawntime, Where);
    }
    void NoteCoroutine(float time, int Where)//딜레이를 관리함.
    {
        Vector3 SpawnPos = Vector3.zero;
        //yield return new WaitForSeconds(time- timeToReach);
        if (Where == 0 || Where == 1 || Where == 7)
        {
            SpawnPos.y = 5.1f;
            if (Where == 1)
                SpawnPos.x = 5.1f;
            if (Where == 7)
                SpawnPos.x = -5.1f;
        }
        else if (Where == 3 || Where == 4 || Where == 5)
        {
            SpawnPos.y = -5.1f;
            if (Where == 3)
                SpawnPos.x = 5.1f;
            if (Where == 5)
                SpawnPos.x = -5.1f;
        }
        else
        {
            SpawnPos.y = 0;
            if (Where == 2)
                SpawnPos.x = 5.1f;
            if (Where == 6)
                SpawnPos.x = -5.1f;
        }
        judgeNumber = Where;
        Instantiate(NotePrefab, SpawnPos, judgeLine[judgeNumber].transform.rotation);
    }*/
    private void SpawnNote(Line line)
    {
        int where = (int)line;

        Vector3 spawnPos = GetSpawnPosition(where);
        GameObject note = Instantiate(NotePrefab, spawnPos, judgeLine[where].rotation);
        judgeNumber = where;

        NoteList[where].Add(note);
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
        allNotes.Add(new NoteData { hitTime = time, linePosition = line });
    }
}