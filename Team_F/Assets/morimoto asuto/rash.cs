/*
using UnityEngine;

public class PlayerDash : MonoBehaviour
{

    [SerializeField] float dashingForce;
    [SerializeField] float dashingTime;
    [SerializeField] float dashCoolDown;
    [SerializeField] float clickResetTime = 0.5f; // クリック間隔制限

    bool isDashing = false;
    bool canDash = true;

    int clickCount = 0;
    float clickTimer = 0f;

    Vector2 dashDirection; // ダッシュ方向を保持

    Rigidbody2D rb;

    public float dashCooldown = 3f;       // 繧ｯ繝ｼ繝ｫ繝繧ｦ繝ｳ譎る俣
    public float dashDistance = 3f;       // 繝繝・す繝･縺ｧ騾ｲ繧霍晞屬・・繝悶Ο繝・け・・

    private bool canDash = true;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 右クリック検出
        if (Input.GetMouseButtonDown(1))
        {
            clickCount++;
            clickTimer = 0f;

            if (clickCount >= 3 && canDash)
            {
                // マウス位置をワールド座標に変換
                Vector3 mouseScreenPosition = Input.mousePosition;
                Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

                // プレイヤーからマウス位置への方向を正規化（Zは2Dなので無視）
                dashDirection = (mouseWorldPosition - transform.position).normalized;
                dashDirection.z = 0f;

                StartCoroutine(Dash());
                clickCount = 0;
            }
        }

        // 一定時間内にクリックされなければリセット
        if (clickCount > 0)
        {
            clickTimer += Time.deltaTime;
            if (clickTimer > clickResetTime)
            {
                clickCount = 0;
                clickTimer = 0f;
            }
        }

        if (isDashing)
            return;
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0;

        // 指定された方向に速度を設定
        rb.velocity = dashDirection * dashingForce;

        yield return new WaitForSeconds(dashingTime);

        rb.gravityScale = originalGravity;
        rb.velocity = Vector2.zero;
        isDashing = false;

        yield return new WaitForSeconds(dashCoolDown);
        canDash = true;
    }
}

        if (Input.GetMouseButtonDown(1) && canDash) // 蜿ｳ繧ｯ繝ｪ繝・け1蝗槭〒遯・ｲ
        {
            StartDash();
        }
    }

    void StartDash()
    {
        canDash = false;

        Vector2 dashDirection = GetFacingDirection();
        Vector2 newPosition = rb.position + dashDirection.normalized * dashDistance;

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

        
        return rb.linearVelocity.sqrMagnitude > 0.1f ? rb.linearVelocity.normalized : Vector2.right;
    }
}
*/