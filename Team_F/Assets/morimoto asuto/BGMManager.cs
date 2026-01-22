using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance;

    public AudioSource audioSource;
    public AudioClip normalBGM;
    public AudioClip bossBGM;

    bool isBossBattle = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartBossBattle()
    {
        if (isBossBattle) return;

        isBossBattle = true;
        ChangeBGM(bossBGM);
    }

    public void EndBossBattle()
    {
        if (!isBossBattle) return;

        isBossBattle = false;
        ChangeBGM(normalBGM);
    }

    void ChangeBGM(AudioClip clip)
    {
        if (audioSource.clip == clip) return;

        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }
}
