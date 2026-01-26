using UnityEngine;

// ボスエリアに入ったことを検知するためのクラス
public class BossAreaTrigger : MonoBehaviour
{
    // BGMを管理するクラスへの参照
    // （※この変数は現在は使われていません）
    public BGMManager bgmManager;

    // 2Dのトリガーコライダーに何かが入ったときに呼ばれる
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 侵入してきたオブジェクトのタグが「Player」かどうかをチェック
        if (other.CompareTag("Player"))
        {
            // BGMManagerのシングルトンインスタンスを使って
            // ボス戦用BGMを再生開始する
            BGMManager.Instance.StartBossBattle();
        }
    }
}
