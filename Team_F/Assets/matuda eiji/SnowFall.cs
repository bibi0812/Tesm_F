using UnityEngine;

public class SnowFall : MonoBehaviour
{
    public float fallSpeed = 2f;     // 落下スピード
    public float destroyOffset = 1f; // 画面外に出たと認識する余白

    private Camera mainCamera;
    private float leftX;
    private float rightX;
    private float topY;
    private float bottomY;

    void Start()
    {
        mainCamera = Camera.main;

        // ★カメラ範囲を計算
        float height = mainCamera.orthographicSize * 2;
        float width = height * mainCamera.aspect;

        leftX = mainCamera.transform.position.x - width / 2;
        rightX = mainCamera.transform.position.x + width / 2;
        topY = mainCamera.transform.position.y + height / 2;
        bottomY = mainCamera.transform.position.y - height / 2;

        // ★カメラ左右の範囲、Yはカメラ上に配置
        float x = Random.Range(leftX, rightX);
        float y = topY + destroyOffset;

        transform.position = new Vector3(x, y, 0);
    }

    void Update()
    {
        // 落下
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime, Space.World);

        // 一番下まで落ちたら削除
        if (transform.position.y < bottomY - destroyOffset)
        {
            Destroy(gameObject);
        }
    }
}
