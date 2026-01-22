using UnityEngine;

public class BossAreaTrigger : MonoBehaviour
{
    public BGMManager bgmManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            BGMManager.Instance.StartBossBattle();
        }
    }
}

