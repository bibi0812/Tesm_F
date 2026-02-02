

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class save : MonoBehaviour
{
    [Header("ONの時のイラスト")]
    public Sprite imageOn;

    [Header("OFFの時のイラスト")]
    public Sprite imageOff;

    [Header("現在のスイッチ状態")]
    public bool on = false;

    void Start()
    {
        UpdateVisuals();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // Playerが触れた、かつ、まだONになっていない時だけ実行
        if (col.gameObject.tag == "Player" && on == false)
        {
            // 強制的にONにする（一度ONになれば、ここを通らなくなります）
            on = true;

            UpdateVisuals();

            // デバッグログ：正しく動作したか確認用（不要なら消してOK）
            Debug.Log("セーブポイントがONになりました！");
        }
    }

    void UpdateVisuals()
    {
        if (on)
        {
            GetComponent<SpriteRenderer>().sprite = imageOn;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = imageOff;
        }
    }
}