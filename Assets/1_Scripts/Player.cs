using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]private GameObject[] NPC; //0. Stage_Door  1. Composer_NPC 2. Choreographer_NPC 3. Costume_NPC
    public GameObject Dialogue; //Dialogue_Manager
    int NPCIndex;

    private void Awake()
    {
    }
    void Start()
    {
        NPCIndex = -1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (NPCIndex == 0)
            {
                SceneManager.LoadScene("Stage_Scene");
            }
            else if (NPCIndex == 1)
            {
                Debug.Log("Composer_NPC");
            }
            else if (NPCIndex == 2)
            {
                Debug.Log("Choreographer_NPC");
            }
        }
        else if (NPCIndex == 3)
        {
            if(Input.GetKeyDown(KeyCode.Z))
            {
                Debug.Log("Costume_NPC");
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                Debug.Log("Costume");
            }
        }
    }
    GameObject Z_press;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < NPC.Length; i++)
        {
            if (collision.gameObject == NPC[i])
            {
                NPCIndex = i;
                Z_press = NPC[i].GetComponentInChildren<TextMeshPro>(true).gameObject;
                if(!Z_press.activeSelf)
                Z_press.SetActive(true);// Z press 시 입장
                break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == NPC[NPCIndex])
        {
            NPCIndex = -1;
            if (Z_press.activeSelf)
                Z_press.SetActive(false);// Z press 메시지 제거
        }
    }
}
