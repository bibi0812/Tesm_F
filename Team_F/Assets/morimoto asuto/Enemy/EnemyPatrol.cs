using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class EnemyPatrol : MonoBehaviour
{
    [Header("移動設定")]
    public float moveSpeed = 2f;
    public float moveDistance = 3f;
    public float chaseSpeed = 4f;

    [Header("視界設定")]
    public float detectionRange = 5f;
    public float stopChaseRange = 7f;
    public LayerMask playerLayer;

    [Header("攻撃設定")]
    public float attackRange = 1.2f;
    public float attackCooldown = 1.5f;
    private float lastAttackTime = 0f;

  
    [Header("攻撃エフェクト")]
    public GameObject fireBreathPrefab;
    public Transform firePoint;     // ← 追加（口の位置）
    public float fireSpeed = 6f;

    private Rigidbody2D rb;
    private Vector2 startPos;
    private bool movingRight = false;
    private bool isChasing = false;
    private Transform player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.freezeRotation = true;

        startPos = transform.position;

        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player == null)
        {
            Debug.LogWarning("Playerが見つかりません！'Player'タグを設定してください。");
        }
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            isChasing = true;
        }
        else if (distanceToPlayer > stopChaseRange)
        {
            isChasing = false;
        }

        // 追跡中なら攻撃判定
        if (isChasing)
        {
            TryAttackPlayer();
        }
    }

    void FixedUpdate()
    {
        if (isChasing)
            ChasePlayer();
        else
            Patrol();
    }

    void Patrol()
    {
        float moveDir = movingRight ? 1f : -1f;
        rb.linearVelocity = new Vector2(moveDir * moveSpeed, rb.linearVelocity.y);

        RaycastHit2D wallCheck = Physics2D.Raycast(transform.position, Vector2.right * moveDir, 0.3f, LayerMask.GetMask("Ground"));
        if (wallCheck.collider != null)
        {
            movingRight = !movingRight;
            Flip();
        }

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
        if (player == null) return;

        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * chaseSpeed;

        RaycastHit2D wallCheck = Physics2D.Raycast(transform.position, direction, 0.3f, LayerMask.GetMask("Ground"));
        if (wallCheck.collider != null)
        {
            rb.linearVelocity = Vector2.zero;
        }

        if ((direction.x > 0 && transform.localScale.x > 0) ||
            (direction.x < 0 && transform.localScale.x < 0))
        {
            Flip();
        }
    }

    void TryAttackPlayer()
    {
        if (player == null) return;

        float dist = Vector2.Distance(transform.position, player.position);

        if (dist <= attackRange)
        {
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                Attack();
                lastAttackTime = Time.time;
            }
        }
    }

    void Attack()
    {
        Debug.Log("敵が攻撃した!");

        if (player == null || firePoint == null) return;

        // プレイヤー方向
        Vector2 direction = (player.position - firePoint.position).normalized;

        // 火を生成（口の位置）
        GameObject breath = Instantiate(fireBreathPrefab, firePoint.position, Quaternion.identity);

        // 火をプレイヤー方向へ飛ばす
        Rigidbody2D br = breath.GetComponent<Rigidbody2D>();
        if (br != null)
        {
            br.linearVelocity = direction * fireSpeed;
        }

        // 火の向きを回転させる
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        breath.transform.rotation = Quaternion.Euler(0, 0, angle);

        Destroy(breath, 1f);
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

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, stopChaseRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
