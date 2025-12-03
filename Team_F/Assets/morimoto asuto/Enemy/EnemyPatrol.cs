using UnityEngine;

// 必要なコンポーネントとしてRigidbody2DとCollider2Dを自動的にアタッチする
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class EnemyPatrol : MonoBehaviour
{
    // --- 公開変数（インスペクターで設定可能） ---

    [Header("耐久設定")]
    public int maxHP = 10;          // 最大体力
    private int currentHP;          // 現在の体力

    [Header("移動設定")]
    public float moveSpeed = 2f;    // パトロール時の移動速度
    public float moveDistance = 3f; // パトロールの片道移動距離
    public float chaseSpeed = 4f;   // プレイヤー追跡時の移動速度

    [Header("視界設定")]
    public float detectionRange = 5f;   // プレイヤーを検知する（追跡を開始する）範囲
    public float stopChaseRange = 7f;   // プレイヤーを見失う（追跡を終了する）範囲
    public LayerMask playerLayer;       // プレイヤーのレイヤーマスク（未使用だが設定として残す）

    [Header("攻撃設定")]
    public float attackRange = 1.2f;    // 攻撃が可能な範囲
    public float attackCooldown = 1.5f; // 攻撃のクールダウン時間
    private float lastAttackTime = 0f;  // 最後に攻撃した時間

    [Header("攻撃エフェクト")]
    public GameObject fireBreathPrefab; // 攻撃（火炎ブレスなど）のPrefab
    public float fireOffset = 1f;       // 攻撃発生位置のオフセット

    [Header("カギオーブ設定")]
    public GameObject redKeyOrbPrefab;  // 敵が倒されたときにドロップするアイテムのPrefab

    // --- プライベート変数 ---

    private Rigidbody2D rb;         // Rigidbody2Dコンポーネント
    private Vector2 startPos;       // パトロールの開始位置（初期位置）
    private bool movingRight = false; // 右に移動中かどうかのフラグ
    private bool isChasing = false; // プレイヤーを追跡中かどうかのフラグ
    private Transform player;       // プレイヤーのTransformコンポーネント

    // --- Unity イベント関数 ---

    // スクリプトが最初にロードされたときに一度だけ呼ばれる
    void Start()
    {
        // Rigidbody2Dコンポーネントを取得
        rb = GetComponent<Rigidbody2D>();
        // 重力を無効化（2Dのトップダウンや特定プラットフォーマーで使う設定）
        rb.gravityScale = 0f;
        // 回転を固定
        rb.freezeRotation = true;
        // 初期位置を保存
        startPos = transform.position;

        // "Player"タグを持つゲームオブジェクトを検索し、Transformを取得
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player == null)
            Debug.LogWarning("Playerが見つかりません！'Player'タグを設定してください。");

        // 体力を最大値で初期化
        currentHP = maxHP;
    }

    // トリガーコライダーに何かが侵入したときに呼ばれる
    void OnTriggerEnter2D(Collider2D other)
    {
        // 侵入したオブジェクトのタグが"Dead"（例: プレイヤーの発射物）の場合
        if (other.CompareTag("Dead"))
        {
            // ダメージを受ける
            TakeDamage(1);
            // 衝突したオブジェクト（発射物など）を破壊
            Destroy(other.gameObject);
        }
    }


    // プレイヤーの視界チェックと追跡・攻撃の判断を行う
    void Update()
    {
        // プレイヤーが見つからない場合は処理をスキップ
        if (player == null) return;

        // プレイヤーまでの距離を計算
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // 距離が検知範囲内なら追跡フラグをON
        if (distanceToPlayer <= detectionRange)
            isChasing = true;
        // 距離が追跡停止範囲を超えたら追跡フラグをOFF
        else if (distanceToPlayer > stopChaseRange)
            isChasing = false;

        // 追跡中の場合、攻撃可能かどうかをチェック
        if (isChasing)
            TryAttackPlayer();
    }

    // 物理演算の更新（一定時間ごと）
    void FixedUpdate()
    {
        // 追跡中ならプレイヤーを追いかける処理を実行
        if (isChasing)
            ChasePlayer();
        // 追跡中でなければパトロール処理を実行
        else
            Patrol();
    }

    // --- メソッド（関数） ---

    // ダメージを受ける処理
    void TakeDamage(int damage)
    {
        // 体力を減らす
        currentHP -= damage;
        // 体力が0以下になったら死亡処理
        if (currentHP <= 0)
        {
            Die();
        }
    }

    // 死亡処理
    void Die()
    {
        
            Debug.Log("敵を倒した！");

            // ▽ BGMを通常に戻す（BGMManagerのEndBossBattle呼び出し）▽
            BGMManager bgm = FindObjectOfType<BGMManager>();
            if (bgm != null)
            {
                bgm.EndBossBattle();
            }
            else
            {
                Debug.LogWarning("BGMManagerがシーンに見つかりませんでした。");
            }
            // ▲ 追加部分 ▲

            // 赤いカギオーブのPrefabが設定されていれば、ドロップ
            if (redKeyOrbPrefab != null)
            {
                // 敵の位置から少し上に生成
                Instantiate(redKeyOrbPrefab, transform.position + Vector3.up * 1f, Quaternion.identity);
            }

            // 敵自身を破壊
            Destroy(gameObject);
        
    }

    // パトロール移動処理
    void Patrol()
    {
        // 移動方向（右なら1、左なら-1）を決定
        float moveDir = movingRight ? 1f : -1f;
        // Rigidbody2Dで移動（物理演算による移動）
        rb.linearVelocity = new Vector2(moveDir * moveSpeed, rb.linearVelocity.y);

        // --- 壁検知と方向転換 ---
        // 移動方向に短い距離でRaycastを飛ばし、"Ground"レイヤーの壁があるかチェック
        RaycastHit2D wallCheck = Physics2D.Raycast(transform.position, Vector2.right * moveDir, 0.3f, LayerMask.GetMask("Ground"));
        if (wallCheck.collider != null)
        {
            // 壁に当たったら方向を反転
            movingRight = !movingRight;
            Flip(); // 向きを反転させる
        }

        // --- パトロール範囲の端に到達したかチェックと方向転換 ---
        // 初期位置から設定された距離を超えて右に移動した場合
        if (transform.position.x >= startPos.x + moveDistance && movingRight)
        {
            movingRight = false; // 左へ方向転換
            Flip(); // 向きを反転させる
        }
        // 初期位置から設定された距離を超えて左に移動した場合
        else if (transform.position.x <= startPos.x - moveDistance && !movingRight)
        {
            movingRight = true; // 右へ方向転換
            Flip(); // 向きを反転させる
        }
    }

    // プレイヤー追跡処理
    void ChasePlayer()
    {
        if (player == null) return;

        // プレイヤーへの方向を計算し、正規化（長さ1）する
        Vector2 direction = (player.position - transform.position).normalized;
        // 追跡速度で移動
        rb.linearVelocity = direction * chaseSpeed;

        // --- 壁検知と停止 ---
        // プレイヤー方向へ短い距離でRaycastを飛ばし、"Ground"レイヤーの壁があるかチェック
        RaycastHit2D wallCheck = Physics2D.Raycast(transform.position, direction, 0.3f, LayerMask.GetMask("Ground"));
        if (wallCheck.collider != null)
            // 壁があったら移動を停止
            rb.linearVelocity = Vector2.zero;

        // --- プレイヤーの方向に応じて向きを反転 ---
        // プレイヤーが右にいるのに敵が左を向いている、またはその逆の場合
        if ((direction.x > 0 && transform.localScale.x > 0) ||
            (direction.x < 0 && transform.localScale.x < 0))
            Flip(); // 向きを反転させる
    }

    // 攻撃を試みる（クールダウンチェックを含む）
    void TryAttackPlayer()
    {
        if (player == null) return;
        float dist = Vector2.Distance(transform.position, player.position);

        // 距離が攻撃範囲内で、かつクールダウンが終了していたら攻撃実行
        if (dist <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time; // 最終攻撃時間を更新
        }
    }

    // 攻撃処理（火炎ブレスの生成と発射）
    void Attack()
    {
        // プレイヤーへの方向を計算
        Vector2 direction = (player.position - transform.position).normalized;
        // 攻撃オブジェクトの生成位置を計算（敵の位置からオフセット分ずらす）
        Vector3 spawnPos = transform.position + new Vector3(direction.x * fireOffset, direction.y * fireOffset, 0);
        // 攻撃オブジェクト（FireBreathPrefab）を生成
        GameObject breath = Instantiate(fireBreathPrefab, spawnPos, Quaternion.identity);

        // 攻撃オブジェクトのRigidbody2Dを取得し、進行方向に速度を与える
        Rigidbody2D br = breath.GetComponent<Rigidbody2D>();
        if (br != null)
            br.linearVelocity = direction * 15f; // 15fはブレスの移動速度

        // 攻撃オブジェクトを進行方向に向けるために角度を計算し、回転を設定
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        breath.transform.rotation = Quaternion.Euler(0, 0, angle);

        // 1秒後に攻撃オブジェクトを破壊
        Destroy(breath, 1f);
    }

    // 敵のグラフィックの左右を反転させる
    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1; // X軸のスケールを反転
        transform.localScale = scale;
    }

    // --- エディター専用機能 ---

    // Unityエディターでゲームオブジェクトが選択されているときにギズモを描画する
    void OnDrawGizmosSelected()
    {
        // 赤色で検知範囲をワイヤー球で描画
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);


        // 緑色で追跡停止範囲をワイヤー球で描画
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, stopChaseRange);

        // 黄色で攻撃範囲をワイヤー球で描画
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