using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public AudioSource normalBGM;
    public AudioSource bossBGM;

    public void StartBossBattle()
    {
        if (normalBGM != null) normalBGM.Stop();
        if (bossBGM != null) bossBGM.Play();
    }

    public void EndBossBattle()
    {
        if (bossBGM != null) bossBGM.Stop();
        if (normalBGM != null) normalBGM.Play();
    }
}
