using UnityEngine;

public class BossRoomController : MonoBehaviour
{
    public GameObject bossDoor;

    void Start()
    {
        Debug.Log("Start: 扉を開く");
        bossDoor.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        Debug.Log("Trigger: 扉を閉める");
        bossDoor.SetActive(true);
    }

    // ★ 必ず呼ばれているか確認する
    public void OnPlayerDead()
    {
        Debug.Log("PlayerDead: 扉を開く");
        bossDoor.SetActive(false);
    }
}
