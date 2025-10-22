using UnityEngine;

public class Rash : MonoBehaviour
{
    public float dashDistance = 10f;         // �ːi����
    public float maxClickInterval = 1f;      // �A���N���b�N�̍ő�Ԋu�i�b�j
    public float dashCooldown = 3f;          // �N�[���^�C���i�b�j

    private int rightClickCount = 0;
    private float lastClickTime = 0f;

    private bool isDashing = false;
    private Vector3 dashTarget;
    private float dashSpeed = 10f;

    private float lastDashTime = -Mathf.Infinity; // �Ō�Ƀ_�b�V���������ԁi�������F�����ɑO�j

    void Update()
    {
        if (isDashing)
        {
            DashMove();
            return;
        }

        if (Input.GetMouseButtonDown(1)) // �E�N���b�N
        {
            float timeSinceLastClick = Time.time - lastClickTime;

            if (timeSinceLastClick <= maxClickInterval)
            {
                rightClickCount++;
            }
            else
            {
                rightClickCount = 1;
            }

            lastClickTime = Time.time;

            // �N�[���^�C�����o�߂��Ă��邩�`�F�b�N
            if (rightClickCount >= 1 && Time.time - lastDashTime >= dashCooldown)
            {
                rightClickCount = 0;
                StartDash();
            }
        }
    }

    void StartDash()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = transform.position.z;

        Vector3 directionToMouse = (mouseWorldPos - transform.position).normalized;

        dashTarget = transform.position - directionToMouse * dashDistance;

        isDashing = true;
        lastDashTime = Time.time; // �_�b�V���J�n���Ԃ��L�^
    }

    void DashMove()
    {
        transform.position = Vector3.MoveTowards(transform.position, dashTarget, dashSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, dashTarget) < 0.01f)
        {
            isDashing = false;
        }
    }
}
