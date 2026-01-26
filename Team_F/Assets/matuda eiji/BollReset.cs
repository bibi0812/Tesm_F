using UnityEngine;

public class BollReset : MonoBehaviour
{
    public Vector3 respawnPoint;  // 復活地点
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private bool isFlashing = false;

    private float defaultGravity; // 元の重力を記憶

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        respawnPoint = transform.position;   // 最初の位置を登録

        defaultGravity = rb.gravityScale;    // 重力を保存
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ▼ チェックポイントに触れたら復活地点を更新
        if (other.CompareTag("Checkpoint"))
        {
            respawnPoint = other.transform.position + new Vector3(0, 0.5f, 0);
            Debug.Log("チェックポイント更新：" + respawnPoint);
        }

        // ▼ 死亡イベント（穴・敵・ボス）
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

    // ▼ 死亡〜点滅〜復活の処理
    private System.Collections.IEnumerator DeathProcess()
    {
        GameManager.isDead = true; // 操作禁止
        isFlashing = true;

        // ▼ まず重力と動きを止める
        rb.gravityScale = 0f;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        // ▼ その場で点滅（6回）
        for (int i = 0; i < 3; i++)
        {
            sr.enabled = false;
            yield return new WaitForSeconds(0.1f);
            sr.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }

        // ▼ 復活地点へ移動
        transform.position = respawnPoint;

        // ▼ 動きをリセット
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        // ▼ 重力を元に戻す
        rb.gravityScale = defaultGravity;

        // ▼ 操作復活
        GameManager.isDead = false;
        isFlashing = false;

        // ▼ シーン中の弾をすべて削除
        foreach (var bullet in GameObject.FindGameObjectsWithTag("Dead"))
        {
            Destroy(bullet);
        }

    }
}
