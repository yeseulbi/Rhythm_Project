using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;
public class DialogueProfile : MonoBehaviour
{
    public string dialogue;
    public int characterIndex;
    public int index;
}

public class Dialogue_Manager : MonoBehaviour
{
    private List<DialogueProfile> DialogueList = new List<DialogueProfile>();
    
    [SerializeField] Sprite[] ImageIndex;
    Image CharacterImage;
    [SerializeField] Text DialogueText, CharacterName;
    GameObject DialogueObject;

    int dialogueCount = 0; //���� ��� ī��Ʈ
    int dialogueIndex = 0; //�ش� ��� �ε���

    string[] NameString = new string[] { "ù��°", "�ι�°", "����°" }; //0. Composer, 1. Choreographer, 2. Costume

    void Start()
    {
        DialogueObject = GetComponent<GameObject>();
        CharacterImage = GetComponentInChildren<Image>(true);

        //DialogueObject.SetActive(true);
        
        SetDialogue("�ȳ��ϼ��� �׽�Ʈ 1", 0);
        SetDialogue("�ݰ����� �׽�Ʈ 2", 1);
        SetDialogue("�׽�Ʈ 3", 2);
        SetDialogue("������������������������������������������������������������������������������������������������������������������������������������������������", 0);
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.RightArrow))
        {
            foreach(var dialogues in DialogueList)
            {
                if (dialogues.index == dialogueCount)
                {
                    CharacterImage.sprite = ImageIndex[dialogues.characterIndex];
                    CharacterName.text = NameString[dialogues.characterIndex];
                    DialogueText.text = dialogues.dialogue;
                }
            }
            dialogueCount++;
        }
    }

    public void SetDialogue(string dialogue, int Character_index)//���, ĳ���� �ε���
    {
        DialogueList.Add(new DialogueProfile { dialogue = dialogue, characterIndex = Character_index, index = dialogueIndex});
        dialogueIndex++;
    }
}