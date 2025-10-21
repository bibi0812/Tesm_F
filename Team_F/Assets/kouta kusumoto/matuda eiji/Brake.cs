using UnityEngine;

public class Braek : MonoBehaviour // クラス名を分かりやすく変更推奨
{   

    // 2D物理衝突が発生したときに呼ばれる（クラスの直接のメンバーとして定義）
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 衝突相手が「Ball」タグを持っているかチェック（スペルミスを修正）
        if (collision.gameObject.CompareTag("Boll"))
        {
            // 2. このブロックのゲームオブジェクトを破壊（削除）する
            Destroy(gameObject);
        }
    }
}