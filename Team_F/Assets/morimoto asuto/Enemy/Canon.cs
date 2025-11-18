using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon   : MonoBehaviour
{
    public GameObject objPrefab;       //発生させるprefabデータ
    public float delayTime = 3.0f;     //遅延時間
    public float length = 8.0f;　　　　//範囲　　　　　
    public float fireSpeed = 4.0f;     //発射速度

    GameObject player;                 //プレイヤー
    Transform gateTransform;               //発射口のTransform
    float passedTimes = 0;             //経過時間

　　// 距離チェック
  bool CheckLength(Vector2 targetPos)
    {
        bool ret = false;
        float d = Vector2.Distance(transform.position, targetPos);
        if (length >= d)
        {
            ret = true;
        }
        return ret;
    }

     void Start()
    {
        //発射オブジェクトのTransformを取得    
        gateTransform = transform.Find("gate");
        //プレイヤーを取得
        player = GameObject.FindGameObjectWithTag("Player");
    }

     void Update()
    {
        //待機時間加算
        passedTimes += Time.deltaTime;
        // Playerとの距離チェック
        if (CheckLength(player.transform.position))
        {
            //待機経過時間
            if (passedTimes > delayTime)
            {
                passedTimes = 0;//時間を0にリセット
                //砲台をプレハブから作る
                Vector2 pos = new Vector2(gateTransform.position.x,
                                          gateTransform.position.y);
                GameObject obj = Instantiate(objPrefab, pos, Quaternion.identity);
                //砲台が向いている方向に発射する
                Rigidbody2D rbody = obj.GetComponent<Rigidbody2D>();
                float angleZ = transform.localEulerAngles.z;
                float x = Mathf.Cos(angleZ * Mathf.Deg2Rad);
                float y = Mathf.Sin(angleZ * Mathf.Deg2Rad);
                Vector2 v = new Vector2(x, y) * fireSpeed;
                rbody.AddForce(v, ForceMode2D.Impulse);
            }
        }
    }
    //範囲表示
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, length);
    }

}
