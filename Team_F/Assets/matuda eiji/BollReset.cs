using UnityEngine;

public class BollReset : MonoBehaviour
{
    public Vector3 respawnPoint;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private bool isFlashing = false;

    private float defaultGravity;

    // ★ 追加
    public BossRoomController bossRoomController;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        respawnPoint = transform.position;
        defaultGravity = rb.gravityScale;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            respawnPoint = other.transform.position + new Vector3(0, 0.5f, 0);
        }

        if (other.CompareTag("Hole") || other.CompareTag("Enemy") || other.CompareTag("Bose"))
        {
            if (!isFlashing)
                StartCoroutine(DeathProcess());

            BGMManager bgm = FindObjectOfType<BGMManager>();
            if (bgm != null)
            {
                bgm.EndBossBattle();
                bgm.PlayNormalBGM();
            }
        }
    }

    private System.Collections.IEnumerator DeathProcess()
    {
        GameManager.isDead = true;
        isFlashing = true;

        // ★ ここで扉を開ける（超重要）
        if (bossRoomController != null)
        {
            bossRoomController.OnPlayerDead();
        }
        else
        {
            Debug.LogWarning("BossRoomController が設定されていません");
        }

        // 動きを止める
        rb.gravityScale = 0f;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        // 点滅
        for (int i = 0; i < 3; i++)
        {
            sr.enabled = false;
            yield return new WaitForSeconds(0.1f);
            sr.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }

        // 復活
        transform.position = respawnPoint;

        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.gravityScale = defaultGravity;

        GameManager.isDead = false;
        isFlashing = false;

        foreach (var bullet in GameObject.FindGameObjectsWithTag("Dead"))
        {
            Destroy(bullet);
        }
    }
}
