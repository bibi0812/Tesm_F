/*
using UnityEngine;

public class PlayerDash : MonoBehaviour
{

    [SerializeField] float dashingForce;
    [SerializeField] float dashingTime;
    [SerializeField] float dashCoolDown;
    [SerializeField] float clickResetTime = 0.5f; // �N���b�N�Ԋu����

    bool isDashing = false;
    bool canDash = true;

    int clickCount = 0;
    float clickTimer = 0f;

    Vector2 dashDirection; // �_�b�V��������ێ�

    Rigidbody2D rb;

    public float dashCooldown = 3f;       // クールダウン時間
    public float dashDistance = 3f;       // ダチE��ュで進む距離�E�EブロチE���E�E

    private bool canDash = true;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // �E�N���b�N���o
        if (Input.GetMouseButtonDown(1))
        {
            clickCount++;
            clickTimer = 0f;

            if (clickCount >= 3 && canDash)
            {
                // �}�E�X�ʒu�����[���h���W�ɕϊ�
                Vector3 mouseScreenPosition = Input.mousePosition;
                Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

                // �v���C���[����}�E�X�ʒu�ւ̕����𐳋K���iZ��2D�Ȃ̂Ŗ����j
                dashDirection = (mouseWorldPosition - transform.position).normalized;
                dashDirection.z = 0f;

                StartCoroutine(Dash());
                clickCount = 0;
            }
        }

        // ��莞�ԓ��ɃN���b�N����Ȃ���΃��Z�b�g
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

        // �w�肳�ꂽ�����ɑ��x��ݒ�
        rb.velocity = dashDirection * dashingForce;

        yield return new WaitForSeconds(dashingTime);

        rb.gravityScale = originalGravity;
        rb.velocity = Vector2.zero;
        isDashing = false;

        yield return new WaitForSeconds(dashCoolDown);
        canDash = true;
    }
}

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