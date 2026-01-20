using UnityEngine;

public class AimFlipX : MonoBehaviour
{
    public SpriteRenderer sprite;

    void Update()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // マウスが右にあったら反転
        sprite.flipX = mouseWorldPos.x > transform.position.x;
    }
}
