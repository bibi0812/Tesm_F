using UnityEngine;

public class RedKeyOrb : MonoBehaviour
{
    public int keyValue = 1; // 取得したカギの数

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // プレイヤーにカギを渡す（Inventoryなどに追加する場合）
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();
            if (inventory != null)
            {
                inventory.AddKey(keyValue);
            }

            // カギオーブを消す
            Destroy(gameObject);
        }
    }
}
