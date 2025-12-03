using UnityEngine;

public class BollReset : MonoBehaviour
{
    public Vector3 respawnPoint;  // 復活地点（初期位置＋更新も可能）
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        respawnPoint = transform.position;   // 最初の位置を登録
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ★ チェックポイントに触れたら復活地点を更新
        if (other.CompareTag("Checkpoint"))
        {
            // チェックポイントの少し上にずらす（例：0.5f）
            respawnPoint = other.transform.position + new Vector3(0, 0.5f, 0);
            Debug.Log("チェックポイント更新：" + respawnPoint);
        }

        // ★ 敵や穴に触れたら復活
        if (other.CompareTag("Hole") || other.CompareTag("Enemy") || other.CompareTag("Bose"))
        {
            transform.position = respawnPoint;

            // 動きをリセット（勢いが残らないように）
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("ぶつかった: " + collision.gameObject.name);
    }

}
