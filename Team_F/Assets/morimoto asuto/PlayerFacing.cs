using UnityEngine;

public class AimFlipX : MonoBehaviour
{
    public SpriteRenderer sprite;
    public BoxCollider2D hitCollider;

    public float colliderOffsetX = 0.2f;

    void Update()
    {
        Vector3 mouseWorldPos =
            Camera.main.ScreenToWorldPoint(Input.mousePosition);

        bool mouseRight = mouseWorldPos.x > transform.position.x;

        sprite.flipX = mouseRight;

        Vector2 offset = hitCollider.offset;
        offset.x = mouseRight ? colliderOffsetX : -colliderOffsetX;
        hitCollider.offset = offset;
    }
}





//using UnityEngine;

//public class AimFlipX : MonoBehaviour
//{
//    public SpriteRenderer sprite;
//    public Transform graphics; // Graphicsオブジェクト

//    void Update()
//    {
//        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

//        bool mouseRight = mouseWorldPos.x > transform.position.x;
//        sprite.flipX = mouseRight;
//    }
//}
