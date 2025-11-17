using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MovingBlock : MonoBehaviour
{
    // Y移動距離
    public float moveY = 0.0f;
    // X移動距離
    public float moveX = 0.0f;
    // 時間
    public float times = 0.0f;
    // 停止時間
    public float wait = 0.0f;
    // 乗った時に動くフラグ
    public bool isMoveWhenOn = false;
    // 動くフラグ
    public bool isCanMove = true;
    // 初期位置
    Vector3 startPos;
    // 移動位置
    Vector3 endPos;
    // 反転フラグ
    bool isReverse = false;
    // 移動補完値
    float movep = 0f;
    // Start is called before the first frame update
    void Start()
    {
        // 初期位置
        startPos = transform.position;
        // 移動位置
        endPos = new Vector2(startPos.x + moveX, startPos.y + moveY);
        if (isMoveWhenOn)
        {
            // 乗った時に動くので最初に動かない
            isCanMove = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (isCanMove)
        {
            // 移動距離
            float distance = Vector2.Distance(startPos, endPos);
            // 1秒の移動距離
            float ds = distance / times;
            // 1フレームの移動距離
            float df = ds * Time.deltaTime / distance;
            // 移動補完値
            movep += df;
            if (isReverse)
            {
                // 逆移動
                transform.position = Vector2.Lerp(endPos, startPos, movep);
            }
            else
            {
                // 正移動
                transform.position = Vector2.Lerp(startPos, endPos, movep);
            }
            if (movep >= 1.0f)
            {
                // 移動補完値リセット
                movep = 0.0f;
                // 移動を逆転
                isReverse = !isReverse;
                // 移動停止
                isCanMove = false;
                if (isMoveWhenOn == false)
                {
                    // 乗った時に動くフラグOFF
                    // 移動フラグを立てる遅延実行
                    Invoke("Move", wait);
                }
            }
        }
    }
    // 移動フラグを立てる
    public void Move()
    {
        isCanMove = true;
    }
    // 移動フラグを下ろす
    public void Stop()
    {
        isCanMove = false;
    }
    // 接触開始
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // 接触したのがプレイヤーなら移動床の子にする
            collision.transform.SetParent(transform);
            if (isMoveWhenOn)
            {
                // 乗った時に動くフラグON
                isCanMove = true;
                // 移動フラグを立てる
            }
        }
    }
    // 接触終了
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // 接触したのがプレイヤーなら移動床の子から外す
            collision.transform.SetParent(null);
        }
    }
    // 移動範囲表示
    void OnDrawGizmosSelected()
    {
        Vector2 fromPos;
        if (startPos == Vector3.zero)
        {
            fromPos = transform.position;
        }
        else
        {
            fromPos = startPos;
        }
        // 移動線
        Gizmos.color = Color.yellow;
        Vector2 toPos = new Vector2(fromPos.x + moveX, fromPos.y + moveY);
        Gizmos.DrawLine(fromPos, toPos);
        // スプライトのサイズ
        Vector2 size = GetComponent<SpriteRenderer>()?.size ?? Vector2.one;
        Gizmos.color = Color.green;
        // 初期位置
        Gizmos.DrawWireCube(fromPos, size);
        // 移動位置
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(toPos, size);
    }
}