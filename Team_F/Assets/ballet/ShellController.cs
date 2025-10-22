using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Block") || collision.CompareTag("Enemy"))
        {
            Destroy(gameObject); // 「bleck」または「Enemy」タグを持つオブジェクトに当たったら弾を消す
        }
        if (collision.CompareTag("Enemy")||collision.CompareTag("Braek"))
        {
            // 敵オブジェクトを削除
            Destroy(collision.gameObject);

            // 弾を削除
            Destroy(gameObject);
        }
    }
}
