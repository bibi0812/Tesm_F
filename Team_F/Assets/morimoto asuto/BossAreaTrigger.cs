using UnityEngine;

// ボスエリアにプレイヤーが入ったことを検知するクラス
public class BossAreaTrigger : MonoBehaviour
{
    // 他のCollider2Dがこのトリガーに入った瞬間に呼ばれる
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 侵入してきたオブジェクトがプレイヤーかどうか確認
        if (other.CompareTag("Player"))
        {
            // シーン内に存在するBGMManagerを探す
            BGMManager bgm = FindObjectOfType<BGMManager>();

            // BGMManagerが見つかった場合のみ処理する（null対策）
            if (bgm != null)
            {
                // ボス戦用BGMの再生を開始する
                bgm.StartBossBattle();
            }
        }
    }
}
