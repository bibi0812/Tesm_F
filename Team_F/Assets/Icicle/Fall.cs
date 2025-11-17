using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Fall : MonoBehaviour
{
   
    public float length = 0.0f;//自動落下検知距離

    public bool isDelete = false;//落下後に削除するフラグ

    public GameObject deadObj;//死亡当たり

    bool isFell = false; // 落下フラグ
    float fadeTime = 0.5f;//フェードアウト時間
   
    // スクリプトが最初にロードされたときに一度だけ呼ばれる
    void Start()
    {
        //Rigidbody2Dの物理挙動を停止
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        rbody.bodyType = RigidbodyType2D.Static;
        deadObj.SetActive(false);
    }

    // 毎フレーム（画面が更新されるたび）に呼ばれる
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");//プレイヤーを探す
        if(player != null)
        {

            float d = Vector2.Distance(transform.position, player.transform.position );
            if ( length >= d)
            {
                Rigidbody2D rbody = GetComponent<Rigidbody2D>();
                if (rbody.bodyType == RigidbodyType2D.Static)
                {
                    rbody.bodyType = RigidbodyType2D.Dynamic;
                    deadObj.SetActive(true);//死亡当たりを表示
                }
            }
        }
        if(isFell)
        {
            //落下した
            fadeTime -= Time.deltaTime;//前のフレームの差分秒マイナス
            Color col = GetComponent<SpriteRenderer>().color;  //カラーを再設定する
            col.a = fadeTime; //透明値を変更
            GetComponent<SpriteRenderer>().color = col; //カラーを再設定する
            if(fadeTime <=0.0f)
            {
                //0以下(透明)になったら消す
                Destroy(gameObject);
            }
        }
       
    }
    //接触開始
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDelete)
        {
            isFell = true; //落下フラグオン
        }
    }
    //範囲表示 void
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, length); 
    }
}
