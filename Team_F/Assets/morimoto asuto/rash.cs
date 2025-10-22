using UnityEngine;

public class DashOnRightClick : MonoBehaviour
{
    public float dashDistance = 3f;   // 突進距離（3ブロック分）
    public float maxClickInterval = 0.5f; // 連続クリックの最大間隔（秒）

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

        if (Input.GetMouseButtonDown(1)) // 右クリック押されたら
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

        // マウス方向の逆方向に突進距離分移動するターゲット位置を計算
        dashTarget = transform.position - directionToMouse * dashDistance;

        isDashing = true;
    }

    void DashMove()
    {
        // 現在位置からターゲットに向かって突進
        transform.position = Vector3.MoveTowards(transform.position, dashTarget, dashSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, dashTarget) < 0.01f)
        {
            isDashing = false;
        }
    }
}
