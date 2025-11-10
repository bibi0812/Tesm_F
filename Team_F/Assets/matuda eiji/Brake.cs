using UnityEngine;

public class BreakBlock : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
<<<<<<< HEAD
        // 衝突相手が「Ball」タグを持っているかチェック（スペルミスを修正）
=======
        // ボール（または弾など）に当たったら非表示にする
>>>>>>> 49b93ae4b242368dd87aee5da42aa5d3afe573b9
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}
