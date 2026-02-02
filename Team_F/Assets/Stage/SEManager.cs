using UnityEngine;

public class SEManager : MonoBehaviour
{
    public static SEManager Instance;
    public AudioSource audioSource;
    public AudioClip clickSE;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ÉVÅ[ÉìêÿÇËë÷Ç¶Ç≈Ç‡écÇ∑
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayClick()
    {
        audioSource.PlayOneShot(clickSE);
    }
    public void StopAll()
    {
        audioSource.Stop();
    }
}
