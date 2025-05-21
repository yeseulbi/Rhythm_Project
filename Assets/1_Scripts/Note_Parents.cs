using System.Collections.Generic;
using UnityEngine;

public class Note_Parents : MonoBehaviour
{
    // ����Ʈ�� �迭�� ����Ͽ� judgeNumber�� ���� �ٸ��� ������ �����̴�.(����, ��� ����)
    static public float timeToReach;

    [SerializeField] float NoteSpeed = 5f;

    GameObject Child_Note;
    ParticleSystem noteParticleSystem;
    AudioSource SoundEffect;
    Vector3 judgeVec;

    List<GameObject>[] NoteParentsList = new List<GameObject>[8];

    bool ChildBool = true; // ��Ʈ�� �����Ǿ����� Ȯ���ϴ� ����
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

        //��Ʈ���� �����ϴ� ���� �󸶳� �ɸ��� ��
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
            //Debug.Log("�̵�");
        }
        if(!Child_Note && GameManager.MusicOn && ChildBool) // �ڽĳ�Ʈ�� ���µ� collider�� �浹���� ���� üũX(�θ��Ʈ�� �ڽĳ�Ʈ�� collider ũ��� ����.)
        {
            ChildBool = false;
            //Debug.Log("�ڽ� ����!");
            SoundEffect.Play();
            noteParticleSystem.Play();
        }
        if(!ChildBool&& !SoundEffect.isPlaying) // ȿ������ ������� ���� ���� ����������, ����Ʈ�� �߰����� �� ����Ʈ�� ������ �ȴ�. �Ǵ� �ð��� ��������
        {
            Destroy(gameObject);
            //Debug.Log("�θ� ��Ʈ �ı�");
        }
    }
}