using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static float GameTimer;
    void Start()
    {
        GameTimer = 0;
    }

    void Update()
    {
        GameTimer = Time.time;
    }

}
