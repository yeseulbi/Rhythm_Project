using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu_Manager : MonoBehaviour
{
    [SerializeField]GameObject Particle_BG;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void particleOnOff()
    {
        Particle_BG.SetActive(!Particle_BG.activeSelf); // 파티클 비활성화
    }
}
