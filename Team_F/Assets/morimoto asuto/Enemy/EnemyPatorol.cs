using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("移動設定")]
    public float moveSpeed = 2f;
    public float moveDistance = 3f;
    public float chaseSpeed = 4f;

    [Header("視界設定")]
    public float detectionRange = 5f;
    public LayerMask playerLayer;

    private Vector2 startPos;
    private bool movingRight = false;
    private Transform player;
    private bool isChasing = false;

    void Start()
    {
        startPos = transform.position;
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player == null)
        {
            Debug.LogWarning("Playerが見つかりません！'Player'タグを付けてください。");
        }
    }

    void Update()
    {
        if (player == null)
        {
            Patrol();
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            isChasing = true;
        }
        else if (distanceToPlayer > detectionRange * 1.2f)
        {
            isChasing = false;
        }

        if (isChasing)
            ChasePlayer();
        else
            Patrol();
    }

    void Patrol()
    {
        float moveDir = movingRight ? 1f : -1f;
        transform.Translate(Vector2.right * moveDir * moveSpeed * Time.deltaTime, Space.World);

        if (transform.position.x >= startPos.x + moveDistance && movingRight)
        {
            movingRight = false;
            Flip();
        }
        else if (transform.position.x <= startPos.x - moveDistance && !movingRight)
        {
            movingRight = true;
            Flip();
        }
    }

    void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;

        // 移動（ワールド座標系）
        transform.Translate(direction * chaseSpeed * Time.deltaTime, Space.World);

        // 見た目の向きをプレイヤー方向に合わせる
        if ((direction.x > 0 && transform.localScale.x > 0) ||
            (direction.x < 0 && transform.localScale.x < 0))
        {
            Flip();
        }
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
