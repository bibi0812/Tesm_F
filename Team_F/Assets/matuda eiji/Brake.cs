using UnityEngine;

public class BreakBlock : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ボール（または弾など）に当たったら非表示にする
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}
