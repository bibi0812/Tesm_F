using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public float speed = 5f;//主人公動き仮

    // Update is called once per frame
    void Update()
    {
        //主人公動き仮
        // 矢印キー入力の取得
        float moveX = 0f;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveX = 1f;  // →キーで右へ
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveX = -1f; // ←キーで左へ
        }

        // プレイヤーを移動させる
        transform.Translate(moveX * speed * Time.deltaTime, 0, 0);
    }
}
