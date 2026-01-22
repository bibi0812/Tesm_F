using UnityEngine;

// マウス位置に応じてスプライトの向きと
// EdgeCollider2D の形状を左右反転＋位置調整するスクリプト
public class AimFlipX_Edge : MonoBehaviour
{
    // 反転させるスプライト
    public SpriteRenderer sprite;

    // 向きに合わせて形を変更する EdgeCollider2D
    public EdgeCollider2D edgeCollider;

    // Colliderを左右にずらす量（X方向）
    public float colliderOffsetX = 0.2f;

    // 元のコライダー形状を保存
    private Vector2[] originalPoints;

    void Start()
    {
        // EdgeCollider2D の元の頂点情報を保存
        originalPoints = edgeCollider.points;
    }

    void Update()
    {
        // マウスの画面座標をワールド座標に変換
        Vector3 mouseWorldPos =
            Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // マウスがキャラクターより右にあるか判定
        bool mouseRight = mouseWorldPos.x > transform.position.x;

        // スプライトを左右反転
        sprite.flipX = mouseRight;

        // EdgeCollider2D を反転＋ずらす
        UpdateEdgeCollider(mouseRight);
    }

    // EdgeCollider2D の形状を更新
    void UpdateEdgeCollider(bool faceRight)
    {
        Vector2[] newPoints = new Vector2[originalPoints.Length];

        // 向きに応じたX方向のオフセット
        float offsetX = faceRight ? colliderOffsetX : -colliderOffsetX;

        for (int i = 0; i < originalPoints.Length; i++)
        {
            // Xを反転 ＋ オフセットを加算
            float x = faceRight
                ? originalPoints[i].x
                : -originalPoints[i].x;

            newPoints[i] = new Vector2(
                x + offsetX,
                originalPoints[i].y
            );
        }

        // 計算後の頂点を反映
        edgeCollider.points = newPoints;
    }
}
