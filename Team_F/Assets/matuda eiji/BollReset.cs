using UnityEngine;

public class BollReset : MonoBehaviour
{
    public Vector3 respawnPoint;  // ← 復活地点（初期位置＋更新も可能）
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // ゲーム開始時の初期位置を復活地点にする
        respawnPoint = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //チェックポイントに触れたら復活ポイント更新
        if (other.CompareTag("Checkpoint"))
        {
            respawnPoint = other.transform.position + new Vector3(0, 0.5f, 0);
            Debug.Log("チェックポイント更新：" + respawnPoint);
        }

        //落ち穴や敵に触れたら復活
        if (other.CompareTag("Hole") || other.CompareTag("Enemy"))
        {
            // 位置を復活地点に戻す
            transform.position = respawnPoint;

            // 動きをリセット（勢いが残ると変な動きになる）
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }
}
