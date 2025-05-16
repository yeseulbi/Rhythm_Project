using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    void Start()
    {
        
    }

    private bool isPlayerInDoor = false;

    private void Update()
    {
        if (isPlayerInDoor && Input.GetKeyDown(KeyCode.Z))
        {
            SceneManager.LoadScene("2_Stage_Scene");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            isPlayerInDoor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            isPlayerInDoor = false;
        }
    }
}
