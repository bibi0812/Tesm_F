
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Fall : MonoBehaviour
{

    public float length = 0.0f; // 自動落下検知距離

    // ★修正: 落下後に「消えて復活」させるため、このフラグはコード内で常にtrueとして扱います。
    // public bool isDelete = false; 

    public GameObject deadObj; // 死亡当たり

    public float resetTime = 5.0f; // ★重要: 消えてから元に戻るまでの時間（秒）

    Vector3 initialPosition; // 初期位置を保存
    RigidbodyType2D initialBodyType; // 初期BodyTypeを保存
    Color initialColor; // 初期色（透明度）を保存

    Collider2D mainCollider; // 本体コライダーを保存

    bool isFell = false; // 落下フラグ
    float fadeTime = 0.5f; // フェードアウト時間

    // ★追加: ツララが「消えている」状態のフラグ
    bool isVanished = false;
    float currentResetTimer = 0.0f; // リセット用タイマー

    // スクリプトが最初にロードされたときに一度だけ呼ばれる
    void Start()
    {
        // 初期状態を保存
        initialPosition = transform.position;
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        initialBodyType = rbody.bodyType;
        initialColor = GetComponent<SpriteRenderer>().color;

        mainCollider = GetComponent<Collider2D>();

        // Rigidbody2Dの物理挙動を停止
        rbody.bodyType = RigidbodyType2D.Static;
        deadObj.SetActive(false);
    }

    // 毎フレーム（画面が更新されるたび）に呼ばれる
    void Update()
    {
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();

        // 1. プレイヤーによる落下検知
        if (!isVanished) // 消えている間は落下検知をしない
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                float d = Vector2.Distance(transform.position, player.transform.position);
                if (length >= d && rbody.bodyType == RigidbodyType2D.Static)
                {
                    // Dynamic（動的状態）に切り替えて落下開始
                    rbody.bodyType = RigidbodyType2D.Dynamic;
                    deadObj.SetActive(true); // 死亡当たりを表示
                }
            }
        }


        // 2. 落下後の処理（フェードアウト -> 消滅 -> リセットタイマー）
        if (isFell && !isVanished)
        {
            // フェードアウト処理
            fadeTime -= Time.deltaTime;
            Color col = GetComponent<SpriteRenderer>().color;
            // 透明度を 0.5秒かけて 1.0 から 0.0 へ変化させる
            col.a = fadeTime / 0.5f;
            GetComponent<SpriteRenderer>().color = col;

            if (fadeTime <= 0.0f)
            {
                // 消滅処理
                isVanished = true; // 消えたフラグON

                // ツララ本体を見えないようにする
                GetComponent<SpriteRenderer>().enabled = false;

                // 物理演算と当たり判定を停止する
                rbody.bodyType = RigidbodyType2D.Static;
                if (mainCollider != null) mainCollider.enabled = false;

                // タイマーを初期化
                currentResetTimer = 0.0f;
            }
        }

        // 3. 復活タイマー処理
        if (isVanished)
        {
            currentResetTimer += Time.deltaTime;

            // リセット時間を超えたらツララを復活させる
            if (currentResetTimer >= resetTime)
            {
                ResetToInitialState();
            }
        }
    }

    // ★修正・追加: ツララを初期状態に戻すメソッド（復活）
    void ResetToInitialState()
    {
        // 1. 位置を初期位置に戻す
        transform.position = initialPosition;
        // 回転もリセット
        transform.rotation = Quaternion.identity;

        // 2. スプライトを表示し、色・透明度を元に戻す
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<SpriteRenderer>().color = initialColor;

        // 3. Rigidbody2Dを初期BodyType（Static）に戻す
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        rbody.bodyType = initialBodyType; // Staticに戻す
        rbody.linearVelocity = Vector2.zero;
        rbody.angularVelocity = 0.0f;

        // 4. コライダーを再度有効にする
        if (mainCollider != null)
        {
            mainCollider.enabled = true;
        }

        // 5. 死亡当たりを非表示にする
        deadObj.SetActive(false);

        // 6. すべてのフラグとタイマーをリセット
        isFell = false;
        isVanished = false;
        fadeTime = 0.5f; // フェードアウト時間を元に戻す
        currentResetTimer = 0.0f;
    }


    //接触開始
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Dynamic（動的）な状態になっていれば、何かに触れた時点で落下済みと判断する
        if (GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Dynamic)
        {
            // まだフェードアウト中でなければ落下フラグをONにし、フェードアウトを開始する
            if (!isVanished)
            {
                isFell = true;
            }
        }
    }

    //範囲表示 void
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, length);
    }
}



//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;

//public class Fall : MonoBehaviour
//{

//    public float length = 0.0f;//自動落下検知距離

//    public bool isDelete = false;//落下後に削除するフラグ


//    bool isFell = false; // 落下フラグ
//    float fadeTime = 0.5f;//フェードアウト時間

//    // スクリプトが最初にロードされたときに一度だけ呼ばれる
//    void Start()
//    {
//        //Rigidbody2Dの物理挙動を停止
//        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
//        rbody.bodyType = RigidbodyType2D.Static;

//    }

//    // 毎フレーム（画面が更新されるたび）に呼ばれる
//    void Update()
//    {
//        GameObject player = GameObject.FindGameObjectWithTag("Player");//プレイヤーを探す
//        if (player != null)
//        {

//            float d = Vector2.Distance(transform.position, player.transform.position);
//            if (length >= d)
//            {
//                Rigidbody2D rbody = GetComponent<Rigidbody2D>();
//                if (rbody.bodyType == RigidbodyType2D.Static)
//                {
//                    rbody.bodyType = RigidbodyType2D.Dynamic;

//                }
//            }
//        }
//        if (isFell)
//        {
//            //落下した
//            fadeTime -= Time.deltaTime;//前のフレームの差分秒マイナス
//            Color col = GetComponent<SpriteRenderer>().color;  //カラーを再設定する
//            col.a = fadeTime; //透明値を変更
//            GetComponent<SpriteRenderer>().color = col; //カラーを再設定する
//            if (fadeTime <= 0.0f)
//            {
//                //0以下(透明)になったら消す
//                Destroy(gameObject);
//            }
//        }

//    }
//    //接触開始
//    void OnCollisionEnter2D(Collision2D collision)
//    {
//        if (isDelete)
//        {
//            isFell = true; //落下フラグオン
//        }
//    }
//    //範囲表示 void
//    void OnDrawGizmosSelected()
//    {
//        Gizmos.DrawWireSphere(transform.position, length);
//    }
//}

