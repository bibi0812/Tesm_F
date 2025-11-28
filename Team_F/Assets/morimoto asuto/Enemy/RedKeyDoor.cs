using UnityEngine;

public class RedKeyDoor : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerInventory inventory = collision.collider.GetComponent<PlayerInventory>();

            if (inventory != null)
            {
                // 鍵が1つ以上あれば UseKey(1) が true になる
                if (inventory.UseKey(1))
                {
                    Debug.Log("赤鍵ドア解除！ 壁を破壊！");
                    Destroy(gameObject);   // ← 壁を消す
                }
                else
                {
                    Debug.Log("鍵が無いため壁は開かない");
                }
            }
        }
    }
}
