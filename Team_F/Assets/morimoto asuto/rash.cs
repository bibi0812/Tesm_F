
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public float dashCooldown = 3f;       // クールダウン時間
    public float dashDistance = 3f;       // ダッシュで進む距離（3ブロック）

    private bool canDash = true;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && canDash) // 右クリック1回で突進
        {
            StartDash();
        }
    }

    void StartDash()
    {
        canDash = false;

        Vector2 dashDirection = GetFacingDirection();
        Vector2 newPosition = rb.position + dashDirection.normalized * dashDistance;

        // 瞬間移動で3ブロック分ダッシュ
        rb.MovePosition(newPosition);

        Invoke("ResetDash", dashCooldown);
    }

    void ResetDash()
    {
        canDash = true;
    }

    Vector2 GetFacingDirection()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        if (horizontalInput > 0)
            return Vector2.right;
        else if (horizontalInput < 0)
            return Vector2.left;

        // 入力がない場合、現在の速度方向（なければ右をデフォルトに）
        return rb.linearVelocity.sqrMagnitude > 0.1f ? rb.linearVelocity.normalized : Vector2.right;
    }
}
