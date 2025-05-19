using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;
public class DialogueProfile 
{
    public string dialogue;
    public int characterIndex;
    public int index;
}

public class Dialogue_Manager : MonoBehaviour
{
    public static List<DialogueProfile> DialogueList = new List<DialogueProfile>();
    
    [SerializeField] Sprite[] ImageIndex;
    Image CharacterImage;
    [SerializeField] Text DialogueText, CharacterName;
    GameObject DialogueObject;

    int dialogueIndex = 0; //해당 대사 인덱스

    public static Dialogue_Manager inst;

    string[] NameString = new string[] { "작곡가", "안무가", "연구원" }; //0. Composer, 1. Choreographer, 2. Costume

    private void Awake()
    {
        inst = this;
        DialogueObject = GetComponent<GameObject>();
        CharacterImage = GetComponentInChildren<Image>(true);
        SetDialogue("0", 0);
        SetDialogue("1", 0);
        SetDialogue("2", 0);
        SetDialogue("3", 1);
        SetDialogue("4", 1);
        SetDialogue("5", 1);
        SetDialogue("6", 2);
        SetDialogue("7", 2);
        SetDialogue("8", 2);
        SetDialogue("9", 2);
    }
    void Start()
    {
    }

    void Update()
    {
        
    }

    public void Dialog(int openNum, int closeNum)
    {
        if (Input.GetKeyUp(KeyCode.RightArrow)&&openNum!=closeNum)
        {
            foreach (var dialogues in DialogueList)
            {
                if (dialogues.index == openNum)
                {
                    CharacterImage.sprite = ImageIndex[dialogues.characterIndex];
                    CharacterName.text = NameString[dialogues.characterIndex];
                    DialogueText.text = dialogues.dialogue;
                }
            }
             openNum++;
        }
    }

    public void SetDialogue(string dialogue, int Character_index)//대사, 캐릭터 인덱스
    {
        DialogueList.Add(new DialogueProfile { dialogue = dialogue, characterIndex = Character_index, index = dialogueIndex});
        dialogueIndex++;
    }
}