/*using UnityEngine;

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
}*/
using UnityEngine;

// Rigidbody2DとCollider2Dが必須であることを保証
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class EnemyPatrol : MonoBehaviour
{
    // === 設定パラメータ ===

    [Header("移動設定")]
    // パトロール時の移動速度
    public float moveSpeed = 2f;
    // 初期位置から片側に移動する最大距離
    public float moveDistance = 3f;
    // プレイヤー追跡時の移動速度
    public float chaseSpeed = 4f;

    [Header("視界設定")]
    // プレイヤーの検出（追跡開始）範囲
    public float detectionRange = 5f;
    // プレイヤーを見失い（追跡停止）元のパトロールに戻る範囲
    public float stopChaseRange = 7f;
    // プレイヤーがいるレイヤー
    public LayerMask playerLayer;

    [Header("攻撃設定")]
    // プレイヤーに攻撃できる範囲
    public float attackRange = 1.2f;
    // 攻撃のクールダウン時間
    public float attackCooldown = 1.5f;
    // 最後に攻撃した時刻を記録
    private float lastAttackTime = 0f;

    [Header("攻撃エフェクト")]
    // 攻撃時に生成する火の玉などのPrefab
    public GameObject fireBreathPrefab;
    // 攻撃エフェクトが敵から離れて生成されるオフセット距離
    public float fireOffset = 1f;

    // === 内部コンポーネントと状態変数 ===

    private Rigidbody2D rb;
    // 敵の初期位置（パトロール範囲の基準点）
    private Vector2 startPos;
    // 敵が現在右に移動しているかどうかのフラグ
    private bool movingRight = false;
    // 敵がプレイヤーを追跡中かどうかのフラグ
    private bool isChasing = false;
    // プレイヤーのTransformコンポーネント
    private Transform player;

    // === Unityライフサイクル関数 ===

    void Start()
    {
        // 必要なコンポーネントを取得
        rb = GetComponent<Rigidbody2D>();
        // 重力の影響を無効化（空中を漂う敵など）
        rb.gravityScale = 0f;
        // 回転を固定
        rb.freezeRotation = true;

        // 初期位置を保存
        startPos = transform.position;

        // "Player"タグを持つオブジェクトを検索し、Transformを取得
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player == null)
        {
            // プレイヤーが見つからない場合の警告
            Debug.LogWarning("Playerが見つかりません！'Player'タグを設定してください。");
        }
    }

    void Update()
    {
        // プレイヤーがいなければ何もしない
        if (player == null) return;

        // プレイヤーとの距離を計算
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // 検出範囲に入ったら追跡開始
        if (distanceToPlayer <= detectionRange)
        {
            isChasing = true;
        }
        // 追跡停止範囲（detectionRangeより大きい）外に出たら追跡停止
        else if (distanceToPlayer > stopChaseRange)
        {
            isChasing = false;
        }

        // 追跡中なら攻撃判定を試みる
        if (isChasing)
        {
            TryAttackPlayer();
        }
    }

    // 物理演算（移動など）はFixedUpdateで行う
    void FixedUpdate()
    {
        if (isChasing)
            // 追跡中ならプレイヤーを追いかける
            ChasePlayer();
        else
            // 追跡中でなければパトロール
            Patrol();
    }

    // === 動作関数 ===

    // 決められた範囲で左右に移動する（パトロール）
    void Patrol()
    {
        // 移動方向を決定 (右: 1f, 左: -1f)
        float moveDir = movingRight ? 1f : -1f;
        // 速度を設定（Y軸速度は維持）
        rb.linearVelocity = new Vector2(moveDir * moveSpeed, rb.linearVelocity.y);

        // 前方に壁があるかレイキャストでチェック (0.3fはチェック距離)
        RaycastHit2D wallCheck = Physics2D.Raycast(transform.position, Vector2.right * moveDir, 0.3f, LayerMask.GetMask("Ground"));
        if (wallCheck.collider != null)
        {
            // 壁に当たったら方向転換
            movingRight = !movingRight;
            // 向きを反転させる
            Flip();
        }

        // パトロール範囲（右側）の限界に達したら方向転換
        if (transform.position.x >= startPos.x + moveDistance && movingRight)
        {
            movingRight = false;
            Flip();
        }
        // パトロール範囲（左側）の限界に達したら方向転換
        else if (transform.position.x <= startPos.x - moveDistance && !movingRight)
        {
            movingRight = true;
            Flip();
        }
    }

    // プレイヤーを追いかける
    void ChasePlayer()
    {
        if (player == null) return;

        // プレイヤーへの方向ベクトルを計算し、正規化
        Vector2 direction = (player.position - transform.position).normalized;
        // 追跡速度で移動
        rb.linearVelocity = direction * chaseSpeed;

        // 追跡方向の壁をチェックし、壁があれば停止 (壁をすり抜けないように)
        RaycastHit2D wallCheck = Physics2D.Raycast(transform.position, direction, 0.3f, LayerMask.GetMask("Ground"));
        if (wallCheck.collider != null)
        {
            rb.linearVelocity = Vector2.zero; // 壁に到達したら停止
        }

        // プレイヤーの方向に応じてキャラクターの向きを反転させる
        // (右向きに移動中かつスケールが右向き) or (左向きに移動中かつスケールが左向き) の場合に反転
        if ((direction.x > 0 && transform.localScale.x > 0) ||
            (direction.x < 0 && transform.localScale.x < 0))
        {
            Flip();
        }
    }

    // 攻撃可能であれば攻撃を試みる
    void TryAttackPlayer()
    {
        if (player == null) return;

        // プレイヤーとの距離を再計算
        float dist = Vector2.Distance(transform.position, player.position);

        // 攻撃範囲内かチェック
        if (dist <= attackRange)
        {
            // クールダウンが終了しているかチェック
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                // 攻撃を実行
                Attack();
                // 攻撃時刻を更新
                lastAttackTime = Time.time;
            }
        }
    }

    // 攻撃を実行する
    void Attack()
    {
        Debug.Log("敵が攻撃した!");

        // プレイヤー方向を計算
        Vector2 direction = (player.position - transform.position).normalized;

        // 攻撃エフェクト（火の玉など）の生成位置を計算
        Vector3 spawnPos = transform.position + new Vector3(direction.x * fireOffset, direction.y * fireOffset, 0);

        // 攻撃エフェクトを生成
        GameObject breath = Instantiate(fireBreathPrefab, spawnPos, Quaternion.identity);

        // 攻撃エフェクトにRigidbody2Dがあれば取得
        Rigidbody2D br = breath.GetComponent<Rigidbody2D>();
        if (br != null)
        {
            // 火の玉をプレイヤー方向へ飛ばす
            br.linearVelocity = direction * 6f; // 火の速さ
        }

        // ★ 火の玉の向きをプレイヤー方向に向ける（回転）
        // Atan2で角度を計算し、Quaternionに変換して回転を設定
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        breath.transform.rotation = Quaternion.Euler(0, 0, angle);

        // （もし敵が横移動しかしない場合、この回転処理は不要で、単純な左右反転だけで済む場合もあります）

        // 1秒後に攻撃エフェクトを破棄
        Destroy(breath, 1f);
    }

    // キャラクターの向きを左右反転させる
    void Flip()
    {
        Vector3 scale = transform.localScale;
        // Xスケールを反転（左右反転）
        scale.x *= -1;
        transform.localScale = scale;
    }

    // === エディタ専用関数 ===

    // シーンビューにギズモ（可視化用の線や球）を描画
    void OnDrawGizmosSelected()
    {
        // 検出範囲を赤で描画
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // 追跡停止範囲を緑で描画
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, stopChaseRange);

        // 攻撃範囲を黄色で描画
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
