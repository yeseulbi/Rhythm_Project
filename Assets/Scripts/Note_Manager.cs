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

    public void NoteSpawn(float Time, int Where) // Time�� ��� �ð��� ��Ʈ�� ������ ���ϰ�, Where�� �Ȱ��� �� ��� ��ġ���� ������ ���Ѵ�. 
    {
        if (Where == 0) //�Ȱ����� ��������, ��Ʈ�� rotate�� �̵��ϰ� �������� ��ġ ���� ������
        {
            transform.rotation = new Quaternion(0,0,45,0);
        }
    }
}
