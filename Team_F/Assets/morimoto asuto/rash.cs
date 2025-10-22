<<<<<<< HEAD
using System.Collections;
=======
<<<<<<< HEAD
>>>>>>> b700d349892d5f970fd5d83b6270159e5a1a28b8
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
<<<<<<< HEAD
    [SerializeField] float dashingForce;
    [SerializeField] float dashingTime;
    [SerializeField] float dashCoolDown;
    [SerializeField] float clickResetTime = 0.5f; // ƒNƒŠƒbƒNŠÔŠu§ŒÀ

    bool isDashing = false;
    bool canDash = true;

    int clickCount = 0;
    float clickTimer = 0f;

    Vector2 dashDirection; // ƒ_ƒbƒVƒ…•ûŒü‚ð•ÛŽ

    Rigidbody2D rb;
=======
    public float dashCooldown = 3f;       // ã‚¯ãƒ¼ãƒ«ãƒ€ã‚¦ãƒ³æ™‚é–“
    public float dashDistance = 3f;       // ãƒ€ãƒƒã‚·ãƒ¥ã§é€²ã‚€è·é›¢ï¼ˆ3ãƒ–ãƒ­ãƒƒã‚¯ï¼‰

    private bool canDash = true;
    private Rigidbody2D rb;
>>>>>>> b700d349892d5f970fd5d83b6270159e5a1a28b8

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
<<<<<<< HEAD
        // ‰EƒNƒŠƒbƒNŒŸo
        if (Input.GetMouseButtonDown(1))
        {
            clickCount++;
            clickTimer = 0f;

            if (clickCount >= 3 && canDash)
            {
                // ƒ}ƒEƒXˆÊ’u‚ðƒ[ƒ‹ƒhÀ•W‚É•ÏŠ·
                Vector3 mouseScreenPosition = Input.mousePosition;
                Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

                // ƒvƒŒƒCƒ„[‚©‚çƒ}ƒEƒXˆÊ’u‚Ö‚Ì•ûŒü‚ð³‹K‰»iZ‚Í2D‚È‚Ì‚Å–³Ž‹j
                dashDirection = (mouseWorldPosition - transform.position).normalized;
                dashDirection.z = 0f;

                StartCoroutine(Dash());
                clickCount = 0;
            }
        }

        // ˆê’èŽžŠÔ“à‚ÉƒNƒŠƒbƒN‚³‚ê‚È‚¯‚ê‚ÎƒŠƒZƒbƒg
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

        // Žw’è‚³‚ê‚½•ûŒü‚É‘¬“x‚ðÝ’è
        rb.velocity = dashDirection * dashingForce;

        yield return new WaitForSeconds(dashingTime);

        rb.gravityScale = originalGravity;
        rb.velocity = Vector2.zero;
        isDashing = false;

        yield return new WaitForSeconds(dashCoolDown);
        canDash = true;
    }
}
=======
        if (Input.GetMouseButtonDown(1) && canDash) // å³ã‚¯ãƒªãƒƒã‚¯1å›žã§çªé€²
        {
            StartDash();
        }
    }

    void StartDash()
    {
        canDash = false;

        Vector2 dashDirection = GetFacingDirection();
        Vector2 newPosition = rb.position + dashDirection.normalized * dashDistance;

        // çž¬é–“ç§»å‹•ã§3ãƒ–ãƒ­ãƒƒã‚¯åˆ†ãƒ€ãƒƒã‚·ãƒ¥
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

        // å…¥åŠ›ãŒãªã„å ´åˆã€ç¾åœ¨ã®é€Ÿåº¦æ–¹å‘ï¼ˆãªã‘ã‚Œã°å³ã‚’ãƒ‡ãƒ•ã‚©ãƒ«ãƒˆã«ï¼‰
        return rb.linearVelocity.sqrMagnitude > 0.1f ? rb.linearVelocity.normalized : Vector2.right;
    }
}
=======
>>>>>>> 16dd36174709f86e8367c850245a3e4aacc82b47
>>>>>>> b700d349892d5f970fd5d83b6270159e5a1a28b8
