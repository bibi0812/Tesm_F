using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// マウスクリックに応じてオブジェクト（弾）を発射する砲台のコントローラー
/// </summary>
public class CannonController : MonoBehaviour
{
    public GameObject objPrefab; // 発射するオブジェクト（弾）のプレハブ

    public float fireSpeed = 20.0f;  // 弾の発射速度

    private Transform gateTransform; // 発射口（gate）のTransform

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
        

        

            // 必要な情報が揃っていなければ処理しない

            if (objPrefab == null || gateTransform == null) return;

            // メインカメラが存在するかチェック

            if (Camera.main == null)

            {

                Debug.LogError("Main Camera が見つかりません。");

                return;

            }

            // マウスのスクリーン座標をワールド座標に変換

            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            mouseWorldPos.z = 0f; // 2DなのでZ座標は0に固定

            // 発射位置とマウス位置を2Dベクトルで取得

            Vector2 mousePos2D = new Vector2(mouseWorldPos.x, mouseWorldPos.y);

            Vector2 firePosition = new Vector2(gateTransform.position.x, gateTransform.position.y);

            // 発射方向を計算し正規化（方向ベクトルを長さ1にする）

            Vector2 direction = (mousePos2D - firePosition).normalized;

            // 発射方向に応じた角度を計算（ラジアン→度）

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            Quaternion rot = Quaternion.Euler(0, 0, angle); // 回転情報を作成

            // 弾を生成（位置と角度を指定）

            GameObject obj = Instantiate(objPrefab, firePosition, rot);

            // 弾にRigidbody2Dがアタッチされていれば力を加えて発射

            Rigidbody2D rbody = obj.GetComponent<Rigidbody2D>();

            if (rbody != null)

            {

                rbody.AddForce(direction * fireSpeed, ForceMode2D.Impulse);

            }

            else

            {

                Debug.LogWarning("生成された弾に Rigidbody2D がアタッチされていません。プレハブを確認してください。");

            }

            // ↓ 砲台の向きを弾の発射方向に変えたい場合はこの行を有効にする

            //transform.rotation = rot;

        

    }

}