using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance;

    public AudioSource audioSource;
    public AudioClip normalBGM;
    public AudioClip bossBGM;
    public AudioClip clearBGM;

    bool isBossBattle = false;
    bool isClearPlaying = false;

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

    private void Update()
    {
        // ÉNÉäÉABGMÇ™èIÇÌÇ¡ÇΩÇÁí èÌBGMÇ…ñﬂÇ∑
        if (isClearPlaying && !audioSource.isPlaying)
        {
            isClearPlaying = false;
            PlayNormalBGM();
        }
    }

    public void StartBossBattle()
    {
        if (isClearPlaying) return;

        isBossBattle = true;
        ChangeBGM(bossBGM, true);
    }

    public void EndBossBattle()
    {
        if (isClearPlaying) return;

        isBossBattle = false;
        PlayNormalBGM();
    }

    public void PlayClearBGM()
    {
        isBossBattle = false;
        isClearPlaying = true;

        audioSource.Stop();
        audioSource.loop = false;
        audioSource.clip = clearBGM;
        audioSource.Play();
    }

    public void PlayNormalBGM()
    {
        ChangeBGM(normalBGM, true);
    }

    void ChangeBGM(AudioClip clip, bool loop)
    {
        if (audioSource.clip == clip && audioSource.loop == loop) return;

        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.loop = loop;
        audioSource.Play();
    }
}
