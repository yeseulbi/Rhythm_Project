using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Stt_Sound : MonoBehaviour
{
    [Header("Slider")] public Slider Main, Music, Menu, SoundEffect;
    public static float MainVolume, MusicVolume, MenuVolume, SoundEffectVolume;
    public AudioMixer mixer;

    const string MIXER_MUSIC = "Music";   // ������ �����س��� �Ķ���� �̸����̴�
    const string MIXER_SE = "SoundEffect";

    void Start()
    {
        Main.value = PlayerPrefs.GetFloat("MainVolume");
        Music.value = PlayerPrefs.GetFloat("MusicVolume");
        Menu.value = PlayerPrefs.GetFloat("MenuVolume");
        SoundEffect.value = PlayerPrefs.GetFloat("SoundEffectVolume");
    }

    void Update()
    {

    }
    // ���� ����
    void SetMainVolume()
    {
        MainVolume = Main.value;
        PlayerPrefs.SetFloat("MainVolume", Main.value);
        PlayerPrefs.Save();
        AudioListener.volume = Mathf.Clamp(Main.value/100, 0f, 1f); // ���� ���� ����
        Debug.Log("Main Volume: " + Main.value + " Volume: "+AudioListener.volume);
    }
    // ���� ����
    void SetMusicVolume()
    {
        MusicVolume = Music.value;
        PlayerPrefs.SetFloat("MusicVolume", Music.value);
        PlayerPrefs.Save();
        Debug.Log("Music Volume: " + Music.value);
        SetMusicAudioMixer(); // ���� ����� �ͼ� ����
    }
    // �޴� ����
    void SetMenuVolume()
    {
        MenuVolume = Menu.value;
        PlayerPrefs.SetFloat("MenuVolume", Menu.value);
        PlayerPrefs.Save();
        Debug.Log("Menu Volume: " + Menu.value);
    }
    // ȿ���� ����
    public void SetSoundEffectVolume()
    {
        SoundEffectVolume = SoundEffect.value;
        PlayerPrefs.SetFloat("SoundEffectVolume", SoundEffect.value);
        PlayerPrefs.Save();
        Debug.Log("Sound Effect Volume: " + SoundEffect.value);
        SetSEAudioMixer(); // ȿ���� ����� �ͼ� ����
    }
    // ���� ����� �ͼ�
    public void SetMusicAudioMixer()
    {
        mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(MusicVolume) * 20);
    }
    // ȿ���� ����� �ͼ�
    public void SetSEAudioMixer()
    {
        mixer.SetFloat(MIXER_SE, Mathf.Log10(SoundEffectVolume) * 20);
    }

}
