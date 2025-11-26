using UnityEngine;

public class RedKeyDoor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();
            if (inventory != null)
            {
                // ƒJƒM‚ª‚ ‚ê‚Î•Ç‚ğÁ‚·
                if (inventory.UseKey())
                {
                    Destroy(gameObject); // •Ç‚ğíœ‚µ‚Ä’Ê‚ê‚é‚æ‚¤‚É
                }
            }
        }
    }
}
