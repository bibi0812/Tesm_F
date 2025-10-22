using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] float dashDistance = 3f; // ダッシュ距離（3ブロック分）
    [SerializeField] float dashDuration = 0.1f; // ダッシュにかける時間（演出用）
    [SerializeField] float dashCoolDown = 1f;
    [SerializeField] float multiClickThreshold = 0.5f;

    bool isDashing = false;
    bool canDash = true;

    int clickCount = 0;
    float clickTimer = 0f;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isDashing)
            return;

        if (Input.GetMouseButtonDown(1))
        {
            clickCount++;
            clickTimer = multiClickThreshold;

            if (clickCount >= 3 && canDash)
            {
                StartCoroutine(Dash());
                clickCount = 0;
                clickTimer = 0f;
            }
        }

        if (clickCount > 0)
        {
            clickTimer -= Time.deltaTime;
            if (clickTimer <= 0f)
            {
                clickCount = 0;
                clickTimer = 0f;
            }
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;

        Vector3 mouseScreenPos = Input.mousePosition;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        mouseWorldPos.z = 0;

        // マウスの逆方向にダッシュ
        Vector2 dashDirection = (transform.position - mouseWorldPos).normalized;

        Vector2 dashTarget = (Vector2)transform.position + dashDirection * dashDistance;

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0;

        float elapsed = 0f;
        Vector2 start = rb.position;

        while (elapsed < dashDuration)
        {
            elapsed += Time.deltaTime;
            rb.MovePosition(Vector2.Lerp(start, dashTarget, elapsed / dashDuration));
            yield return null;
        }

        rb.MovePosition(dashTarget);
        rb.gravityScale = originalGravity;
        rb.velocity = Vector2.zero;

        isDashing = false;

        yield return new WaitForSeconds(dashCoolDown);
        canDash = true;
    }
}
