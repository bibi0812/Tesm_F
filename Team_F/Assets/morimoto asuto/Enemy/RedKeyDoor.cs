//using UnityEngine;

//public class RedKeyDoor : MonoBehaviour
//{
//    public AudioSource audio;
//    private void OnCollisionEnter2D(Collision2D collision)
//    {
//        if (collision.collider.CompareTag("Player"))
//        {
//            PlayerInventory inventory = collision.collider.GetComponent<PlayerInventory>();

//            if (inventory != null)
//            {
//                // 鍵が1つ以上あれば UseKey(1) が true になる
//                if (inventory.UseKey(1))
//                {
//                    Debug.Log("赤鍵ドア解除！ 壁を破壊！");
//                    Destroy(gameObject);   // ← 壁を消す
//                    audio.PlayOneShot(clickSE);

//                }
//                else
//                {
//                    Debug.Log("鍵が無いため壁は開かない");
//                }
//            }
//        }
//    }
//}
using UnityEngine;

public class RedKeyDoor : MonoBehaviour
{
    public AudioSource audioSource; // 変数名を audioSource に変更（推奨）
    public AudioClip clickSE;      // 再生するSEをインスペクターで指定

    private bool isOpened = false; // 二重に処理されないためのフラグ

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // すでに開いているなら何もしない
        if (isOpened) return;

        if (collision.collider.CompareTag("Player"))
        {
            PlayerInventory inventory = collision.collider.GetComponent<PlayerInventory>();

            if (inventory != null)
            {
                if (inventory.UseKey(1))
                {
                    OpenDoor();
                }
                else
                {
                    Debug.Log("鍵が無いため壁は開かない");
                }
            }
        }
    }

    private void OpenDoor()
    {
        isOpened = true;
        Debug.Log("赤鍵ドア解除！");

        // 1. 音を鳴らす
        if (audioSource != null && clickSE != null)
        {
            audioSource.PlayOneShot(clickSE);
        }
        // 1. 音を鳴らす
        if (audioSource != null && clickSE != null)
        {
            audioSource.pitch = 1.2f; // 少し高くして耳に残りやすくする
            audioSource.PlayOneShot(clickSE, 1.0f);
        }

        // 2. 見た目（Renderer）と当たり判定（Collider）を消す
        // これでプレイヤーからは「消えた」ように見える
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        // 3. 音が終わる頃にオブジェクトを完全に消去する
        // clickSE.length 秒待ってから Destroy される
        Destroy(gameObject, clickSE.length);
    }
}