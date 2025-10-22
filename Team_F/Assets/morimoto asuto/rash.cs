/*
=======
using System.Collections;
>>>>>>> 9d4c18309d89640ff5633249bdc16e383bd89500
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
<<<<<<< HEAD

    [SerializeField] float dashingForce;
    [SerializeField] float dashingTime;
    [SerializeField] float dashCoolDown;
    [SerializeField] float clickResetTime = 0.5f; // �N���b�N�Ԋu����
=======
    [SerializeField] float dashDistance = 3f; // �_�b�V�������i3�u���b�N���j
    [SerializeField] float dashDuration = 0.1f; // �_�b�V���ɂ����鎞�ԁi���o�p�j
    [SerializeField] float dashCoolDown = 1f;
    [SerializeField] float multiClickThreshold = 0.5f;
>>>>>>> 9d4c18309d89640ff5633249bdc16e383bd89500

    bool isDashing = false;
    bool canDash = true;

    int clickCount = 0;
    float clickTimer = 0f;

    Rigidbody2D rb;
<<<<<<< HEAD

    public float dashCooldown = 3f;       // クールダウン時間
    public float dashDistance = 3f;       // ダチE��ュで進む距離�E�EブロチE���E�E

    private bool canDash = true;
    private Rigidbody2D rb;
=======
>>>>>>> 9d4c18309d89640ff5633249bdc16e383bd89500

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
<<<<<<< HEAD
        // �E�N���b�N���o
=======
        if (isDashing)
            return;

>>>>>>> 9d4c18309d89640ff5633249bdc16e383bd89500
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

        // �}�E�X�̋t�����Ƀ_�b�V��
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
<<<<<<< HEAD

        if (Input.GetMouseButtonDown(1) && canDash) // 右クリチE��1回で突E��
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

