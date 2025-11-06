using UnityEngine;

public class BollReset : MonoBehaviour
{
    private Vector3 initialPosition;
    private Rigidbody2D rb; // Rigidbody2Dを保持する変数

    void Start()
    {
        // Rigidbody2Dを取得し、変数に保持
        rb = GetComponent<Rigidbody2D>();

        // 初期位置を保存
        initialPosition = transform.position;
        Debug.Log("初期位置を保存しました: " + initialPosition);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hole")|| other.CompareTag("Enemy"))
        {
            // 位置を初期位置に戻す
            transform.position = initialPosition;

            // Rigidbody2Dの速度と角速度をリセット（重要！）
            if (rb != null)
            {
                // ★ ここを 'velocity' に修正しました ★
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;
            }
        }
        // 🔹 ブロックをリセット
        BlockManager manager = FindObjectOfType<BlockManager>();
        if (manager != null)
        {
            manager.ResetAllBlocks();
        }

        Debug.Log("リスタートしました。ブロックを元に戻しました。");
    }
}