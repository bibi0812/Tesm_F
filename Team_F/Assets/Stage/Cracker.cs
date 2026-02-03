using UnityEngine;

public class Cracker : MonoBehaviour
{
    public float speed = 5f;
    public float lifeTime = 2f;

    Vector2 direction;

    public void Init(Vector2 dir)
    {
        direction = dir.normalized;
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Åö timeScale ñ≥éãÇ≈ìÆÇ©Ç∑
        transform.Translate(direction * speed * Time.unscaledDeltaTime);
    }
}
