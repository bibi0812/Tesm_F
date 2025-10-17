using UnityEngine;

public class Braek : MonoBehaviour // クラス名を分かりやすく変更推奨
{
    public GameObject breakEffectPrefab; // 破壊エフェクトのPrefabをセット

    // 2D物理衝突が発生したときに呼ばれる（クラスの直接のメンバーとして定義）
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 衝突相手が「Ball」タグを持っているかチェック（スペルミスを修正）
        if (collision.gameObject.CompareTag("Boll"))
        {
            // 1. 破壊エフェクトをブロックの位置で生成する
            if (breakEffectPrefab != null)
            {
                // Instantiate(生成するPrefab, 位置, 回転)
                GameObject effect = Instantiate(breakEffectPrefab, transform.position, Quaternion.identity);
                // エフェクトを一定時間後に削除する（例: 2秒後）
                Destroy(effect, 2f);
            }

            // 2. このブロックのゲームオブジェクトを破壊（削除）する
            Destroy(gameObject);
        }
    }
}