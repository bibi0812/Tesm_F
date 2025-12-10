using UnityEngine;

public class SnowSpawner : MonoBehaviour
{
    public GameObject snowPrefab;  // SnowFall が付いている雪プレハブ
    public float spawnInterval = 0.1f; // 雪の発生間隔
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            Instantiate(snowPrefab);
            timer = 0f;
        }
    }
}
