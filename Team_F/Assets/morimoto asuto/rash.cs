using System.Collections;
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
