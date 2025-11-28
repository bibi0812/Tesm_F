using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class ShellController : MonoBehaviour
//{
//    void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (collision.CompareTag("Enemy") || collision.CompareTag("Braek") || collision.CompareTag("Block") || collision.CompareTag("Hole"))
//        {
//            Destroy(gameObject); // 「bleck」または「Enemy」タグを持つオブジェクトに当たったら弾を消す
//        }
//        if (collision.CompareTag("Enemy") || collision.CompareTag("Braek"))
//        {
//            // 敵オブジェクトを削除
//            Destroy(collision.gameObject);

//            // 弾を削除
//            Destroy(gameObject);

//        }
//    }
//}


//using UnityEngine;

public class ShellController : MonoBehaviour
{
    // 衝突回数を記録するプライベート変数
    private int hitCount = 0;

    void OnTriggerEnter2D(Collider2D collision)
    {
        // 衝突対象が「Enemy」または「Braek」タグを持つかチェック
        // 「Block」や「Hole」は1回目で消える動作を残すため、分けて処理します
        if (collision.CompareTag("Enemy") || collision.CompareTag("Braek") || collision.CompareTag("Block") || collision.CompareTag("Hole"))
        {
            Destroy(gameObject); // 「bleck」または「Enemy」タグを持つオブジェクトに当たったら弾を消す
        }

        if (collision.CompareTag("Enemy") || collision.CompareTag("Braek"))
        {
            // 衝突回数をカウントアップ
            hitCount++;

            // 2回目以降の衝突の場合
            if (hitCount >= 2)
            {
                // 衝突した相手オブジェクト（EnemyまたはBraek）を削除
                Destroy(collision.gameObject);

                // 弾オブジェクト自身を削除
                Destroy(gameObject);
            }
            // 1回目の衝突では、弾は何もしないでそのまま飛び続けます。
            // 敵や破壊可能オブジェクトは消えません。
        }
    }
}