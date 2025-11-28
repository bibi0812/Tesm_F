using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using UnityEngine.UI; // UIを使うために追加

/// <summary>

/// マウスクリックに応じてオブジェクト（弾）を発射する砲台のコントローラー

/// </summary>

public class CannonController : MonoBehaviour

{

    public GameObject objPrefab; // 発射するオブジェクト（弾）のプレハブ

    public float fireSpeed = 20.0f;  // 弾の発射速度

    public float recoilForce = 1.0f; // 砲台に加わる反動の強さ

    // 🔥 新しく追加する変数

    private const int MAX_SHOTS = 5; // 最大発射回数を定数として定義

    private int remainingShots = MAX_SHOTS; // 残り発射回数

    // (オプション) 残弾表示用のUI Textコンポーネント (インスペクタで設定)

    public Text remainingShotsText;

    private Transform gateTransform; // 発射口（gate）のTransform

    private Rigidbody2D cannonRbody; // 砲台自身のRigidbody2D

    void Start()

    {

        // このオブジェクトの子から "gate" という名前のオブジェクトを探す

        gateTransform = transform.Find("gate");

        if (gateTransform == null)

        {

            Debug.LogError("gateオブジェクトが見つかりません。ヒエラルキー内に 'gate' という名前の子オブジェクトがありますか？");

        }

        // プレハブが設定されていない場合に警告を出す

        if (objPrefab == null)

        {

            Debug.LogError("objPrefab が設定されていません。インスペクタでプレハブを割り当ててください。");

        }

        // 砲台自身の Rigidbody2D を取得

        cannonRbody = GetComponent<Rigidbody2D>();

        if (cannonRbody == null)

        {

            // 反動を実現するためには、砲台（このスクリプトがアタッチされているオブジェクト）に

            // Rigidbody2D が必須です。

            Debug.LogError("このオブジェクトに Rigidbody2D がアタッチされていません。反動処理（リコイル）は動作しません。");

        }



    }

    void Update()

    {

        // マウスの左クリックが押されたときに弾を発射

        if (Input.GetMouseButtonDown(0))

        {

            Shoot();

        }

    }

    /// <summary>

    /// 弾を生成して発射する処理

    /// </summary>

    void Shoot()

    {

        // 🔥 残弾チェック: 残り回数が0以下なら発射処理を中断

        if (remainingShots <= 0)

        {

            Debug.Log("残弾がありません。発射できませんでした。");

            return;

        }

        // プレハブや発射口が設定されていなければ処理を中断

        if (objPrefab == null || gateTransform == null) return;

        // メインカメラの存在チェック

        if (Camera.main == null)

        {

            Debug.LogError("Main Camera が見つかりません。");

            return;

        }

        // (中略：マウス座標のワールド変換)

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mouseWorldPos.z = 0f; // 2DなのでZ軸は0に固定

        // 発射位置とマウス位置を2Dベクトルで取得

        Vector2 mousePos2D = new Vector2(mouseWorldPos.x, mouseWorldPos.y);

        Vector2 firePosition = new Vector2(gateTransform.position.x, gateTransform.position.y);

        // 発射方向を計算し正規化（長さ1にする）

        Vector2 direction = (mousePos2D - firePosition).normalized;

        // (中略：角度と回転情報の計算)

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion rot = Quaternion.Euler(0, 0, angle); // 回転情報を作成

        // 弾を生成（位置と回転を指定）

        GameObject obj = Instantiate(objPrefab, firePosition, rot);

        // 弾の寿命を2秒に設定。2秒後に自動で破棄される

        Destroy(obj, 2f);

        // Rigidbody2Dコンポーネントを取得して力を加える（弾を発射）

        Rigidbody2D rbody = obj.GetComponent<Rigidbody2D>();

        if (rbody != null)

        {

            // インパルス（瞬間的な力）を加えて弾を発射

            rbody.AddForce(direction * fireSpeed, ForceMode2D.Impulse);

        }

        else

        {

            Debug.LogWarning("生成された弾に Rigidbody2D がアタッチされていません。プレハブを確認してください。");

        }



        // -------------------------------------------------------------

        // 🔥 砲台に反動を加える処理

        if (cannonRbody != null)

        {

            // 弾の発射方向とは逆の方向を計算

            Vector2 recoilDirection = -direction;

            // 砲台自身の Rigidbody2D にインパルス（瞬間的な力）を加える

            // これにより、発射方向とは逆へ砲台が吹き飛ばされる

            cannonRbody.AddForce(recoilDirection * recoilForce, ForceMode2D.Impulse);

        }

        // -------------------------------------------------------------

        // もし砲台を弾の発射方向に回転させたい場合は以下のコメントを外してください

        // transform.rotation = rot;

    }
}

    /// <summary>

    /// UI Textの表示を更新するヘルパーメソッド

    /// </summary>

  



    /*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UIを使うために追加（ただし、今回は未使用）

/// <summary>
/// マウスクリックに応じてオブジェクト（弾）を発射する砲台のコントローラー
/// </summary>
public class CannonController : MonoBehaviour
{
    public GameObject objPrefab; // 発射するオブジェクト（弾）のプレハブ
    public float fireSpeed = 2.0f;  // 弾の発射速度
    public float recoilForce = 10.0f; // 砲台に加わる反動の強さ

    // 🔥 クールタイム関連の変数
    public float fireInterval = 1.0f; // 🚀 発射クールタイムを 3.0秒 に変更
    private float nextFireTime = 0.0f; // 次に発射可能になる時刻 (Time.time)
    private Transform gateTransform; // 発射口（gate）のTransform
    private Rigidbody2D cannonRbody; // 砲台自身のRigidbody2D

    void Start()
    {
        // このオブジェクトの子から "gate" という名前のオブジェクトを探す
        gateTransform = transform.Find("gate");
        if (gateTransform == null)
        {
            Debug.LogError("gateオブジェクトが見つかりません。ヒエラルキー内に 'gate' という名前の子オブジェクトがありますか？");
        }

        // プレハブが設定されていない場合に警告を出す
        if (objPrefab == null)
        {
            Debug.LogError("objPrefab が設定されていません。インスペクタでプレハブを割り当ててください。");
        }

        // 砲台自身の Rigidbody2D を取得
        cannonRbody = GetComponent<Rigidbody2D>();
        if (cannonRbody == null)
        {
            Debug.LogError("このオブジェクトに Rigidbody2D がアタッチされていません。反動処理（リコイル）は動作しません。");
        }
    }

    void Update()
    {
        // マウスの左クリックが押されたときに弾を発射
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    /// <summary>
    /// 弾を生成して発射する処理
    /// </summary>
    void Shoot()
    {
        // 🚀 クールタイムチェック:
        // 現在時刻 (Time.time) が次に発射可能になる時刻 (nextFireTime) より小さければ、クールタイム中のため処理を中断
        if (Time.time < nextFireTime)
        {
            return;
        }

        // プレハブや発射口が設定されていなければ処理を中断
        if (objPrefab == null || gateTransform == null) return;

        // 🚀 クールタイムを更新:
        // 発射が成功したので、次に発射できる時刻を設定
        nextFireTime = Time.time + fireInterval;

        // メインカメラの存在チェック
        if (Camera.main == null)
        {
            Debug.LogError("Main Camera が見つかりません。");
            return;
        }

        // (中略：マウス座標のワールド変換)
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f; // 2DなのでZ軸は0に固定

        // 発射位置とマウス位置を2Dベクトルで取得
        Vector2 mousePos2D = new Vector2(mouseWorldPos.x, mouseWorldPos.y);
        Vector2 firePosition = new Vector2(gateTransform.position.x, gateTransform.position.y);

        // 発射方向を計算し正規化（長さ1にする）
        Vector2 direction = (mousePos2D - firePosition).normalized;

        // (中略：角度と回転情報の計算)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.Euler(0, 0, angle); // 回転情報を作成

        // 弾を生成（位置と回転を指定）
        GameObject obj = Instantiate(objPrefab, firePosition, rot);

        // 弾の寿命を2秒に設定。2秒後に自動で破棄される
        Destroy(obj, 2f);

        // Rigidbody2Dコンポーネントを取得して力を加える（弾を発射）
        Rigidbody2D rbody = obj.GetComponent<Rigidbody2D>();
        if (rbody != null)
        {
            // インパルス（瞬間的な力）を加えて弾を発射
            rbody.AddForce(direction * fireSpeed, ForceMode2D.Impulse);
        }
        else
        {
            Debug.LogWarning("生成された弾に Rigidbody2D がアタッチされていません。プレハブを確認してください。");
        }

        // -------------------------------------------------------------
        // 🔥 砲台に反動を加える処理
        if (cannonRbody != null)
        {
            // 弾の発射方向とは逆の方向を計算
            Vector2 recoilDirection = -direction;
            // 砲台自身の Rigidbody2D にインパルス（瞬間的な力）を加える
            cannonRbody.AddForce(recoilDirection * recoilForce, ForceMode2D.Impulse);
        }
        // -------------------------------------------------------------
    }
}*/