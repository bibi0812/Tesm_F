using UnityEngine;

public class RedKey : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory inv = other.GetComponent<PlayerInventory>();
            if (inv != null)
            {
                inv.AddKey(1);  // © Œ®‚ğ‘‚â‚·
                Destroy(gameObject); // Œ®‚ğÁ‚·
            }
        }
    }
}
