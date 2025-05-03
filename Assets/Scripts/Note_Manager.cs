using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public enum Line { Up, UpRight, Right, DownRight, Down, DownLeft, Left, UpLeft }

public class Note_Manager : MonoBehaviour
{
    public Transform[] judgeLine; // 중심 판정 라인 위치, 위에서부터 시계방향으로 01234567
    [SerializeField] GameObject NotePrefab;
    
    [HideInInspector]public int judgeNumber;

    public static Note_Manager inst;
    void Awake()
    {
        inst = this;
    }

    private void Start()
    {
        //함수 작동 Test
        for(int i = 0;i<50;i++)
        {
            NoteSpawn(i, Random.Range(0, 8));
        }
        /*NoteSpawn(0, Line.Up);
        NoteSpawn(2, Line.Down);
        NoteSpawn(3, Line.Right);
        NoteSpawn(4, Line.Left);
        NoteSpawn(5, Line.UpLeft);
        NoteSpawn(6, Line.DownLeft);
        NoteSpawn(7, Line.UpRight);
        NoteSpawn(8, Line.DownRight);     */   
    }
    void Update()
    {

    }

    public void NoteSpawn(float Spawntime, Line Where) //Spawntime은 생성될 시간, Where은 생성되는 노트 위치
    {
        Vector3 SpawnPos = Vector3.zero;
        DOVirtual.DelayedCall(Spawntime, () =>
        {
            if (Where == Line.Up || Where == Line.UpRight || Where == Line.UpLeft)
            {
                SpawnPos.y = 5.1f;
                if (Where == Line.UpRight)
                    SpawnPos.x = 5.1f;
                if (Where == Line.UpLeft)
                    SpawnPos.x = -5.1f;
            }
            else if (Where == Line.DownRight || Where == Line.Down || Where == Line.DownLeft)
            {
                SpawnPos.y = -5.1f;
                if (Where == Line.DownRight)
                    SpawnPos.x = 5.1f;
                if (Where == Line.DownLeft)
                    SpawnPos.x = -5.1f;
            }
            else
            {
                SpawnPos.y = 0;
                //transform.rotation = Quaternion.Euler(0, 0, 90);
                if (Where == Line.Right)
                    SpawnPos.x = 5.1f;
                if (Where == Line.Left)
                    SpawnPos.x = -5.1f;
            }
            judgeNumber = (int)Where;
            Instantiate(NotePrefab, SpawnPos, judgeLine[judgeNumber].transform.rotation);
        });
    }
    public void NoteSpawn(float Spawntime, int Where) //Spawntime은 생성될 시간, Where은 생성되는 노트 위치
    {
        Vector3 SpawnPos = Vector3.zero;
        DOVirtual.DelayedCall(Spawntime, () =>
        {
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
                //transform.rotation = Quaternion.Euler(0, 0, 90);
                if (Where == 2)
                    SpawnPos.x = 5.1f;
                if (Where == 6)
                    SpawnPos.x = -5.1f;
            }
            judgeNumber = Where;
            Instantiate(NotePrefab, SpawnPos, judgeLine[judgeNumber].transform.rotation);
        });
    }
}
