using UnityEngine;

public class FireBreathHit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {


        // Åö ï«Ç…ìñÇΩÇ¡ÇΩÇÁè¡Ç¶ÇÈ
        if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}