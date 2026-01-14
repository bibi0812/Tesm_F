using System.Collections;
using UnityEngine;

// Rigidbody2D と Collider2D を必ずアタッチさせる
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class EnemyPatrol : MonoBehaviour
{
    [Header("ボス専用 撃破音")]
    public AudioClip bossDeathSE;
    private AudioSource audioSource;

    


    [Header("ダメージ演出")]
    public AudioClip damageSE;          // ダメージ音
    public float flashTime = 0.1f;      // 色変更時間
    public Color damageColor = Color.red;


    [Header("耐久設定")]
    public int maxHP = 10;      // 最大HP
    private int currentHP;      // 現在のHP

    [Header("移動設定")]
    public float moveSpeed = 2f;      // パトロール移動速度
    public float moveDistance = 3f;   // 左右の移動範囲距離
    public float chaseSpeed = 4f;     // 追跡時の速度

    [Header("視界設定")]
    public float detectionRange = 5f;     // プレイヤー発見距離
    public float stopChaseRange = 7f;     // 追跡終了距離
    public LayerMask playerLayer;         // 未使用だが将来用設定

    [Header("攻撃設定")]
    public float attackRange = 1.2f;      // 攻撃可能距離
    public float attackCooldown = 1.5f;   // 攻撃クールダウン時間
    private float lastAttackTime = 0f;    // 最後の攻撃時刻

    [Header("攻撃エフェクト")]
    public GameObject fireBreathPrefab;   // ブレスPrefab
    public Transform breathPoint;         // 口位置（発射位置）

    [Header("カギオーブ設定")]
    public GameObject redKeyOrbPrefab;    // 敵撃破時のドロップ


    private bool isDead = false;
    private SpriteRenderer spriteRenderer;
    private Color defaultColor;
    private Coroutine flashCoroutine;
    private Rigidbody2D rb;
    private Vector2 startPos;             // 初期位置
    private bool movingRight = false;     // 移動方向判定
    private bool isChasing = false;       // 追跡中フラグ
    private Transform player;             // プレイヤー参照
    private bool bossMusicStarted = false;

    // ====================================================
    // 初期化処理
    // ====================================================
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.freezeRotation = true;

        startPos = transform.position;
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (!player)
            Debug.LogWarning("Playerが見つかりません!");

        currentHP = maxHP;

        // ★ 追加
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultColor = spriteRenderer.color;
    }

    // ダメージを受けるトリガー
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Dead"))
        {
            TakeDamage(1);
            Destroy(other.gameObject);
        }
    }

    // ====================================================
    // 追跡判定と攻撃判定
    // ====================================================
    void Update()
    {
        if (isDead) return;
        if (!player) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange) isChasing = true;
        else if (distanceToPlayer > stopChaseRange) isChasing = false;

        if (isChasing) TryAttackPlayer();
    }

    // ====================================================
    // 物理移動処理
    // ====================================================
    void FixedUpdate()
    {
        if (isDead) return;

        if (isChasing) ChasePlayer();
        else Patrol();
    }

    // ====================================================
    // ダメージ処理
    // ====================================================
    void TakeDamage(int dmg)
    {
        currentHP -= dmg;

        // ダメージ音
        if (damageSE && audioSource)
            audioSource.PlayOneShot(damageSE);

        // 色フラッシュ（重複防止）
        if (flashCoroutine != null)
            StopCoroutine(flashCoroutine);

        flashCoroutine = StartCoroutine(DamageFlash());

        if (currentHP <= 0) Die();
    }
    IEnumerator DamageFlash()
    {
        spriteRenderer.color = damageColor;
        yield return new WaitForSeconds(flashTime);
        spriteRenderer.color = defaultColor;
    }


    // 死亡処理
    void Die()
    {

        if (isDead) return;
        isDead = true;

        // ★ すべての行動を即停止
        rb.linearVelocity = Vector2.zero;
        rb.simulated = false;          // Rigidbody2D 完全停止
        isChasing = false;

        // ★ コルーチン停止（色フラッシュ対策）
        StopAllCoroutines();

        // ★ 攻撃を今後一切させない
        lastAttackTime = float.MaxValue;

        // ボス撃破音
        if (CompareTag("Bose") && bossDeathSE != null && audioSource != null)
        {
            audioSource.PlayOneShot(bossDeathSE);
        }

        // 鍵ドロップ（1回だけ）
        if (redKeyOrbPrefab != null)
            Instantiate(redKeyOrbPrefab, transform.position + Vector3.up, Quaternion.identity);

        // 見た目と当たり判定を即削除
        GetComponent<Collider2D>().enabled = false;
        spriteRenderer.enabled = false;

        // ★ EnemyPatrol 自体を無効化（超重要）
        enabled = false;

        // ★ SE 再生後に完全削除
        float delay = (CompareTag("Bose") && bossDeathSE != null)
            ? bossDeathSE.length
            : 0f;

        Destroy(gameObject, delay);

    }

    // ====================================================
    // パトロール移動
    // ====================================================
    void Patrol()
    {
        // 左右移動
        float moveDir = movingRight ? 1f : -1f;
        rb.linearVelocity = new Vector2(moveDir * moveSpeed, rb.linearVelocity.y);

        // 前方の壁チェック
        RaycastHit2D wallCheck = Physics2D.Raycast(transform.position, Vector2.right * moveDir, 0.3f, LayerMask.GetMask("Ground"));
        if (wallCheck.collider != null)
        {
            movingRight = !movingRight;
            Flip();
        }

        // 移動範囲を超えたら反転
        if (transform.position.x >= startPos.x + moveDistance && movingRight)
        {
            movingRight = false; Flip();
        }
        else if (transform.position.x <= startPos.x - moveDistance && !movingRight)
        {
            movingRight = true; Flip();
        }
    }

    // ====================================================
    // プレイヤー追跡
    // ====================================================
    void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * chaseSpeed;

        // プレイヤー方向に向きを合わせる
        if ((direction.x > 0 && transform.localScale.x > 0) ||
            (direction.x < 0 && transform.localScale.x < 0))
            Flip();
    }

    // 攻撃可能なら実行
    void TryAttackPlayer()
    {
        float dist = Vector2.Distance(transform.position, player.position);

        if (dist <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    // ====================================================
    // ブレス攻撃
    // ====================================================
    void Attack()
    {
        if (isDead) return;   // ★ 念押し
        if (!breathPoint || !fireBreathPrefab) return;

        Vector2 direction = (player.position - breathPoint.position).normalized;

        GameObject breath = Instantiate(fireBreathPrefab, breathPoint.position, Quaternion.identity);

        Rigidbody2D br = breath.GetComponent<Rigidbody2D>();
        if (br != null) br.linearVelocity = direction * 15f;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        breath.transform.rotation = Quaternion.Euler(0, 0, angle);

        Destroy(breath, 1f);
    }

    // ====================================================
    // 向き反転＋ブレス発射位置の調整
    // ====================================================
    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        // 口位置も左右反転
        if (breathPoint)
            breathPoint.localPosition = new Vector3(-breathPoint.localPosition.x, breathPoint.localPosition.y, 0);
    }

    // ギズモ描画（デバッグ用）
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



//using UnityEngine;

//[RequireComponent(typeof(Rigidbody2D))]
//[RequireComponent(typeof(Collider2D))]
//public class EnemyPatrol : MonoBehaviour
//{
//    [Header("耐久設定")]
//    public int maxHP = 10;
//    private int currentHP;

//    [Header("移動設定")]
//    public float moveSpeed = 2f;
//    public float moveDistance = 3f;
//    public float chaseSpeed = 4f;

//    [Header("視界設定")]
//    public float detectionRange = 5f;
//    public float stopChaseRange = 7f;
//    public LayerMask playerLayer;

//    [Header("攻撃設定")]
//    public float attackRange = 1.2f;
//    public float attackCooldown = 1.5f;
//    private float lastAttackTime = 0f;

//    [Header("攻撃エフェクト")]
//    public GameObject fireBreathPrefab;
//    public float fireOffset = 1f;

//    [Header("カギオーブ設定")]
//    public GameObject redKeyOrbPrefab;  // 常にこれを落とす

//    private Rigidbody2D rb;
//    private Vector2 startPos;
//    private bool movingRight = false;
//    private bool isChasing = false;
//    private Transform player;

//    void Start()
//    {
//        rb = GetComponent<Rigidbody2D>();
//        rb.gravityScale = 0f;
//        rb.freezeRotation = true;
//        startPos = transform.position;

//        player = GameObject.FindGameObjectWithTag("Player")?.transform;
//        if (player == null)
//            Debug.LogWarning("Playerが見つかりません！'Player'タグを設定してください。");

//        currentHP = maxHP;
//    }

//    void OnTriggerEnter2D(Collider2D other)
//    {
//        if (other.CompareTag("Dead"))
//        {
//            TakeDamage(1);
//            Destroy(other.gameObject);
//        }
//    }

//    void TakeDamage(int damage)
//    {
//        currentHP -= damage;
//        if (currentHP <= 0)
//        {
//            Die();
//        }
//    }

//    void Die()
//    {
//        Debug.Log("敵を倒した！");

//        // 常に赤いカギオーブを落とす
//        if (redKeyOrbPrefab != null)
//            Instantiate(redKeyOrbPrefab, transform.position + Vector3.up * 1f, Quaternion.identity);

//        Destroy(gameObject);
//    }

//    void Update()
//    {
//        if (player == null) return;

//        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

//        if (distanceToPlayer <= detectionRange)
//            isChasing = true;
//        else if (distanceToPlayer > stopChaseRange)
//            isChasing = false;

//        if (isChasing)
//            TryAttackPlayer();
//    }

//    void FixedUpdate()
//    {
//        if (isChasing)
//            ChasePlayer();
//        else
//            Patrol();
//    }

//    void Patrol()
//    {
//        float moveDir = movingRight ? 1f : -1f;
//        rb.linearVelocity = new Vector2(moveDir * moveSpeed, rb.linearVelocity.y);

//        RaycastHit2D wallCheck = Physics2D.Raycast(transform.position, Vector2.right * moveDir, 0.3f, LayerMask.GetMask("Ground"));
//        if (wallCheck.collider != null)
//        {
//            movingRight = !movingRight;
//            Flip();
//        }

//        if (transform.position.x >= startPos.x + moveDistance && movingRight)
//        {
//            movingRight = false;
//            Flip();
//        }
//        else if (transform.position.x <= startPos.x - moveDistance && !movingRight)
//        {
//            movingRight = true;
//            Flip();
//        }
//    }

//    void ChasePlayer()
//    {
//        if (player == null) return;

//        Vector2 direction = (player.position - transform.position).normalized;
//        rb.linearVelocity = direction * chaseSpeed;

//        RaycastHit2D wallCheck = Physics2D.Raycast(transform.position, direction, 0.3f, LayerMask.GetMask("Ground"));
//        if (wallCheck.collider != null)
//            rb.linearVelocity = Vector2.zero;

//        if ((direction.x > 0 && transform.localScale.x > 0) ||
//            (direction.x < 0 && transform.localScale.x < 0))
//            Flip();
//    }

//    void TryAttackPlayer()
//    {
//        if (player == null) return;
//        float dist = Vector2.Distance(transform.position, player.position);

//        if (dist <= attackRange && Time.time >= lastAttackTime + attackCooldown)
//        {
//            Attack();
//            lastAttackTime = Time.time;
//        }
//    }

//    void Attack()
//    {
//        Vector2 direction = (player.position - transform.position).normalized;
//        Vector3 spawnPos = transform.position + new Vector3(direction.x * fireOffset, direction.y * fireOffset, 0);
//        GameObject breath = Instantiate(fireBreathPrefab, spawnPos, Quaternion.identity);

//        Rigidbody2D br = breath.GetComponent<Rigidbody2D>();
//        if (br != null)
//            br.linearVelocity = direction * 15f;

//        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
//        breath.transform.rotation = Quaternion.Euler(0, 0, angle);

//        Destroy(breath, 1f);
//    }

//    void Flip()
//    {
//        Vector3 scale = transform.localScale;
//        scale.x *= -1;
//        transform.localScale = scale;
//    }

//    void OnDrawGizmosSelected()
//    {
//        Gizmos.color = Color.red;
//        Gizmos.DrawWireSphere(transform.position, detectionRange);

//        Gizmos.color = Color.green;
//        Gizmos.DrawWireSphere(transform.position, stopChaseRange);

//        Gizmos.color = Color.yellow;
//        Gizmos.DrawWireSphere(transform.position, attackRange);
//    }
//}