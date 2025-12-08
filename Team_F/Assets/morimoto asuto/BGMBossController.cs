using UnityEngine;
using System.Collections; // フェードイン・アウトにコルーチンを使用する場合

public class BGMBossController : MonoBehaviour
{
    // 通常時のBGM用AudioSource
    public AudioSource normalBGM;
    // ボス戦時のBGM用AudioSource
    public AudioSource bossBGM;

    // トリガー侵入時にボスBGMへ変更するか
    public bool startBossMusicOnTrigger = true;

    private void Start()
    {
        // ゲーム開始時は通常BGMが流れている状態にする（必要に応じて）
        if (!normalBGM.isPlaying)
        {
            normalBGM.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (startBossMusicOnTrigger && other.CompareTag("Player"))
        {
            Debug.Log("Boss Area Triggered - StartBossMusic");
            StartBossMusic();
        }
    }

    // ボス戦開始時に呼ばれるメソッド
    public void StartBossMusic()
    {
        Debug.Log("StartBossMusic called");
        StartCoroutine(CrossFadeMusic(normalBGM, bossBGM, 1.0f)); // 1.0fはフェード時間
    }

    // ボス戦終了時に呼ばれるメソッド
    public void StopBossMusic()
    {
        Debug.Log("StopBossMusic called");
        StartCoroutine(CrossFadeMusic(bossBGM, normalBGM, 1.0f));
    }

    // BGMをクロスフェードさせるコルーチン
    IEnumerator CrossFadeMusic(AudioSource fadeOutSource, AudioSource fadeInSource, float duration)
    {
        Debug.Log("CrossFade Start: " + fadeOutSource.name + " -> " + fadeInSource.name);

        float timer = 0;
        float startVolumeOut = fadeOutSource.volume;

        // フェードイン側の再生を開始（既に再生中でなければ）
        if (!fadeInSource.isPlaying)
        {
            fadeInSource.volume = 0;
            fadeInSource.Play();
        }

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = timer / duration;

            fadeOutSource.volume = Mathf.Lerp(startVolumeOut, 0, t);
            fadeInSource.volume = Mathf.Lerp(0, 1, t);

            yield return null;
        }

        fadeOutSource.Stop();
        fadeOutSource.volume = startVolumeOut;
        fadeInSource.volume = 1;
    }
}
