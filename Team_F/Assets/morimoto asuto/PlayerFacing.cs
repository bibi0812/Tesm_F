using UnityEngine;

public class AimFlipX : MonoBehaviour
{
    public SpriteRenderer sprite;
    public Transform graphics; // Graphicsオブジェクト

    void Update()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        bool mouseRight = mouseWorldPos.x > transform.position.x;
        sprite.flipX = mouseRight;
    }
}
