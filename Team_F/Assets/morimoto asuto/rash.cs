using UnityEngine;

public class DashOnRightClick : MonoBehaviour
{
    public float dashDistance = 3f;   // �ːi�����i3�u���b�N���j
    public float maxClickInterval = 0.5f; // �A���N���b�N�̍ő�Ԋu�i�b�j

    private int rightClickCount = 0;
    private float lastClickTime = 0f;

    private bool isDashing = false;
    private Vector3 dashTarget;
    private float dashSpeed = 10f;

    void Update()
    {
        if (isDashing)
        {
            DashMove();
            return;
        }

        if (Input.GetMouseButtonDown(1)) // �E�N���b�N�����ꂽ��
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

            if (rightClickCount >= 3)
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

        // �}�E�X�����̋t�����ɓːi�������ړ�����^�[�Q�b�g�ʒu���v�Z
        dashTarget = transform.position - directionToMouse * dashDistance;

        isDashing = true;
    }

    void DashMove()
    {
        // ���݈ʒu����^�[�Q�b�g�Ɍ������ēːi
        transform.position = Vector3.MoveTowards(transform.position, dashTarget, dashSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, dashTarget) < 0.01f)
        {
            isDashing = false;
        }
    }
}
