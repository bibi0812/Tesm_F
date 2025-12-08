using UnityEngine;

public class PlayerBlink : MonoBehaviour
{
    public float blinkTime = 1.5f;  // 点滅する秒数
    public float blinkInterval = 0.1f; // 点滅間隔
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    public void StartBlink()
    {
        StartCoroutine(Blink());
    }

    private System.Collections.IEnumerator Blink()
    {
        float timer = 0f;

        while (timer < blinkTime)
        {
            // 表示/非表示を反転
            rend.enabled = !rend.enabled;

            timer += blinkInterval;
            yield return new WaitForSeconds(blinkInterval);
        }

        // 最後に必ず表示状態に戻す
        rend.enabled = true;
    }
}
