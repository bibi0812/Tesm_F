using System.Collections;

using System.Collections.Generic;

using UnityEngine;

public class CannonController : MonoBehaviour

{

    public GameObject objPrrefad;

    public float fireSpeed = 10.0f;  // 発射速度

    Transform gateTransform;

    void Start()

    {

        gateTransform = transform.Find("gate");

        if (gateTransform == null)

        {

            Debug.LogError("gateオブジェクトが見つかりません。");

        }

    }

    void Update()

    {

        // 左クリックを検出

        if (Input.GetMouseButtonDown(0))

        {

            Shoot();

        }

    }

    void Shoot()

    {

        // マウスのワールド座標を取得

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mouseWorldPos.z = 0f;  // 2DなのでZ座標は0に

        // Vector3 → Vector2 に変換

        Vector2 mousePos2D = new Vector2(mouseWorldPos.x, mouseWorldPos.y);

        // 発射位置（gateの位置）

        Vector2 firePosition = gateTransform.position;

        // 発射方向を正規化して計算

        Vector2 direction = (mousePos2D - firePosition).normalized;

        // 弾を生成
        GameObject obj = Instantiate(objPrrefad, firePosition, Quaternion.identity);

        // Rigidbody2Dに力を加える
        Rigidbody2D rbody = obj.GetComponent<Rigidbody2D>();
        rbody.AddForce(direction * fireSpeed, ForceMode2D.Impulse);


        // 砲台の向きをマウス方向に向けたい場合は以下をアンコメント

        /*

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);

        */

    }

}
