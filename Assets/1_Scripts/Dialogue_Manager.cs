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

    int dialogueCount = 0; //현재 대사 카운트
    int dialogueIndex = 0; //해당 대사 인덱스

    string[] NameString = new string[] { "첫번째", "두번째", "세번째" }; //0. Composer, 1. Choreographer, 2. Costume

    void Start()
    {
        DialogueObject = GetComponent<GameObject>();
        CharacterImage = GetComponentInChildren<Image>(true);

        //DialogueObject.SetActive(true);
        
        SetDialogue("안녕하세요 테스트 1", 0);
        SetDialogue("반가워요 테스트 2", 1);
        SetDialogue("테스트 3", 2);
        SetDialogue("ㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇㅇ", 0);
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

    public void SetDialogue(string dialogue, int Character_index)//대사, 캐릭터 인덱스
    {
        DialogueList.Add(new DialogueProfile { dialogue = dialogue, characterIndex = Character_index, index = dialogueIndex});
        dialogueIndex++;
    }
}