using UnityEngine;

public class BossRoomManager : MonoBehaviour
{
    public static BossRoomManager instance;

    public GameObject bossWall;
    public bool isPlayerAlive = true;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void EnterBossRoom()
    {
        if (isPlayerAlive)
        {
            bossWall.SetActive(true);
        }
    }

    public void PlayerDied()
    {
        isPlayerAlive = false;
        bossWall.SetActive(false);
    }

    public void PlayerRespawn()
    {
        isPlayerAlive = true;
    }
}
