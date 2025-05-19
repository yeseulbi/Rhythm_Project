using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue_Manager : MonoBehaviour
{
    [SerializeField] Sprite[] ImageIndex;
    Image CharacterImage;
    Text DialogueText, CharacterName;
    GameObject DialogueObject;

    string[] NameString = new string[] { "Composer", "Choreographer", "Costume" }; //0. Composer, 1. Choreographer, 2. Costume
    int DialogueNumber = 0; //���� ����Ǵ� ��� �ε���

    void Start()
    {
        DialogueObject = GetComponent<GameObject>();
        CharacterImage = GetComponentInChildren<Image>(true);
        CharacterName = GetComponentInChildren<Text>(true); //�ݵ�� Name ������Ʈ�� Dialogue ������Ʈ���� ���� �־�� �Ѵ�
        DialogueText = GetComponentInChildren<Text>(true);

        DialogueObject.SetActive(true);
        if(Input.GetKeyUp(KeyCode.RightArrow))
        {

        }
        
    }

    void Update()
    {
        if(DialogueObject)
        {
            //SetDialogue("Hello, this is a test dialogue.", 0); // Example usage
        }
    }

    public void SetDialogue(string dialogue, int Character_index, int index)//���, ĳ���� �ε���, ��� ���� �ε���
    {
        int dialogueNumber = DialogueNumber;
        if (dialogueNumber == index)
        {
            CharacterImage.sprite = ImageIndex[Character_index];
            CharacterName.text = NameString[Character_index];
            DialogueText.text = dialogue;

            ++DialogueNumber;
        }
    }
}