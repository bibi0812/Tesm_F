using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float moveSpeed = 2f;        // 移動速度
    public float moveDistance = 3f;     // 移動する距離

    private Vector2 startPos;           // 初期位置
    private bool movingRight = false;    // 右へ移動中かどうか

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // 移動方向に応じて位置を更新
        if (movingRight)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            if (transform.position.x >= startPos.x + moveDistance)
            {
                movingRight = false;
                Flip();
            }
        }
        else
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            if (transform.position.x <= startPos.x - moveDistance)
            {
                movingRight = true;
                Flip();
            }
        }
    }

    // スプライトの向きを反転させる（必要な場合）
    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1; // 左右反転
        transform.localScale = scale;
    }
}
