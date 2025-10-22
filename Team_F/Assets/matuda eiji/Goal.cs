using UnityEngine;
using UnityEngine.SceneManagement; // シーン切り替えに使う場合

public class Goal : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // ボールにタグ "Ball" がついていたらクリア
        if (other.CompareTag("Boll"))
        {
            Debug.Log("ゴール！クリア！");
        }
    }
}
