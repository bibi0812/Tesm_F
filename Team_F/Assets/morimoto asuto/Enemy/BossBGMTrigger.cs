using UnityEngine;

public class BossBGMTrigger : MonoBehaviour
{
    public AudioSource normalBGM;
    public AudioSource bossBGM;

    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasTriggered && collision.CompareTag("Player"))
        {
            hasTriggered = true;
            normalBGM.Stop();
            bossBGM.Play();
        }
    }
}
