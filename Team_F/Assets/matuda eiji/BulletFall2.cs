using UnityEngine;

public class BulletFall2 : MonoBehaviour
{
    public float fallSpeed = 3f;     // 落ちる速さ
    public float rotateSpeed = 180f; // 回転スピード
    public float destroyY = -6f;     // 消える高さ
    public float startY = 6f;        // 出現位置の高さ
    public float rangeX = 8f;        // X軸の出現範囲

    void Start()
    {
        // 一番右から出す
        float startX = rangeX;  // rangeXが右端の座標
        transform.position = new Vector3(startX, startY, 0);
        // ランダムなX位置にセットして上から出す
        transform.position = new Vector3(Random.Range(-rangeX + 20, rangeX), startY, 0);

        // 回転速度をランダムにする（例：90~360度/秒）
        rotateSpeed = Random.Range(180f, 360f);
    }

    void Update()
    {

        // 下方向へ移動
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime, Space.World);

        // 回転
        transform.Rotate(0, 0, -rotateSpeed * Time.deltaTime);

        // 画面外で消す
        if (transform.position.y < destroyY)
        {
            Destroy(gameObject);
        }
    }
}