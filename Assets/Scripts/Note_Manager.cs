using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public class Note_Manager : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void NoteSpawn(float Time, int Where) // Time은 어느 시간에 노트가 나올지 정하고, Where은 팔각형 중 어느 위치에서 나올지 정한다. 
    {
        if (Where == 0) //팔각형의 위쪽으로, 노트의 rotate와 이동하고 내려오는 위치 등을 결정함
        {
            transform.rotation = new Quaternion(0,0,45,0);
        }
    }
}
