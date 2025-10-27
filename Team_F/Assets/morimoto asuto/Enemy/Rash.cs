using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Rash : MonoBehaviour
{
    [Header("Dash Settings")]
    public float dashPower = 10f;         // 横方向の固定スピード
    public float dashLift = 5f;           // 上方向の跳ね上がり
    public float dashCooldown = 3f;       // クールタイム
    public float gravityScale = 1f;       // 通常の重力
    public float dashSpeed = 1.5f;        // 全体倍率（0.5～2.0）

    private Rigidbody2D rb;
    private bool isDashing = false;
    private float lastDashTime = -Mathf.Infinity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        rb.gravityScale = gravityScale;
    }

    void Update()
    {
        if (isDashing || Time.time - lastDashTime < dashCooldown)
            return;

        if (Input.GetMouseButtonDown(1))
            StartDash();
    }

    void StartDash()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;

        Vector2 dirToMouse = ((Vector2)mouseWorldPos - (Vector2)transform.position).normalized;
        Vector2 reverseDir = -dirToMouse;

        rb.linearVelocity = Vector2.zero;

        // 横速度を固定にして打ち上げ
        Vector2 launchVelocity = reverseDir * dashPower * dashSpeed + Vector2.up * dashLift;

        rb.linearVelocity = launchVelocity;

        isDashing = true;
        lastDashTime = Time.time;

        // ダッシュ時間は固定
        Invoke(nameof(EndDash), 0.3f / dashSpeed);
    }

    void EndDash()
    {
        isDashing = false;
    }
}
