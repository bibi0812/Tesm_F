using UnityEngine;

public class BossRoomTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            BossRoomManager.instance.EnterBossRoom();
        }
    }
}
