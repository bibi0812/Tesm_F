using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    private static BulletSpawner instance;

    public GameObject bulletPrefab;   // 出す弾丸
    public float spawnInterval = 0.5f; // 弾を出す間隔
    private float timer = 0f;          // 経過時間

    void Awake()
    {
       
    }

    void Update()
    {
        // 時間を計測
        timer += Time.deltaTime;

        // 一定時間で弾を生成
        if (timer >= spawnInterval)
        {
            Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            timer = 0f; // タイマーリセット
        }
    }
}
