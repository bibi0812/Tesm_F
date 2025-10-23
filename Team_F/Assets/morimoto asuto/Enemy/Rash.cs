using UnityEngine;

public class Rash : MonoBehaviour
{
    public float dashDistance = 10f;         // 突進距離
    public float maxClickInterval = 1f;      // 連続クリックの最大間隔（秒）
    public float dashCooldown = 3f;          // クールタイム（秒）

    private int rightClickCount = 0;
    private float lastClickTime = 0f;

    private bool isDashing = false;
    private Vector3 dashTarget;
    private float dashSpeed = 10f;

    private float lastDashTime = -Mathf.Infinity; // 最後にダッシュした時間（初期化：無限に前）

    void Update()
    {
        if (isDashing)
        {
            DashMove();
            return;
        }

        if (Input.GetMouseButtonDown(1)) // 右クリック
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

            // クールタイムが経過しているかチェック
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
        lastDashTime = Time.time; // ダッシュ開始時間を記録
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
