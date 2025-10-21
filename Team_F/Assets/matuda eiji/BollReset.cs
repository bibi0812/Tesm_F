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

        // スタート直後の意図しない動きを止める
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hole"))
        {
            // 物理挙動を完全に停止
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;
            }

            // プレイヤーの位置を初期位置に戻す
            transform.position = initialPosition;

            Debug.Log("落とし穴に落ちました");
        }
    }
}