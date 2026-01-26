using UnityEngine;

// BGMを一元管理するクラス（シングルトンなし）
public class BGMManager : MonoBehaviour
{
    // BGM再生用のAudioSource
    public AudioSource audioSource;

    // 各種BGM
    public AudioClip normalBGM; // 通常時BGM
    public AudioClip bossBGM;   // ボス戦BGM
    public AudioClip clearBGM;  // クリアBGM

    // 状態管理フラグ
    bool isBossBattle = false;   // ボス戦中かどうか
    bool isClearPlaying = false; // クリアBGM再生中かどうか

    private void Start()
    {
        // シーン開始時に通常BGMを再生
        PlayNormalBGM();
    }

    private void Update()
    {
        // クリアBGMが再生終了したら通常BGMに戻す
        if (isClearPlaying && !audioSource.isPlaying)
        {
            isClearPlaying = false;
            PlayNormalBGM();
        }
    }

    // ボス戦開始時に呼ぶ
    public void StartBossBattle()
    {
        if (isClearPlaying) return;

        isBossBattle = true;
        ChangeBGM(bossBGM, true);
    }

    // ボス戦終了時に呼ぶ
    public void EndBossBattle()
    {
        if (isClearPlaying) return;

        isBossBattle = false;
        PlayNormalBGM();
    }

    // クリア時に呼ぶ
    public void PlayClearBGM()
    {
        isBossBattle = false;
        isClearPlaying = true;

        audioSource.Stop();
        audioSource.loop = false;
        audioSource.clip = clearBGM;
        audioSource.Play();
    }

    // 通常BGMを再生
    public void PlayNormalBGM()
    {
        ChangeBGM(normalBGM, true);
    }

    // BGMを切り替える共通処理
    void ChangeBGM(AudioClip clip, bool loop)
    {
        if (audioSource.clip == clip && audioSource.loop == loop) return;

        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.loop = loop;
        audioSource.Play();
    }
}




//using UnityEngine;

//// BGMを一元管理するクラス
//public class BGMManager : MonoBehaviour
//{
//    // シングルトン用のインスタンス
//    public static BGMManager Instance;

//    // BGM再生用のAudioSource
//    public AudioSource audioSource;

//    // 各種BGM
//    public AudioClip normalBGM; // 通常時BGM
//    public AudioClip bossBGM;   // ボス戦BGM
//    public AudioClip clearBGM;  // クリアBGM

//    // 状態管理フラグ
//    bool isBossBattle = false;   // ボス戦中かどうか
//    bool isClearPlaying = false; // クリアBGM再生中かどうか

//    private void Awake()
//    {
//        // シングルトンの設定
//        if (Instance == null)
//        {
//            Instance = this;
//            // シーン切り替え後も破棄されないようにする
//            DontDestroyOnLoad(gameObject);
//        }
//        else
//        {
//            // 既に存在している場合は自分を削除
//            Destroy(gameObject);
//        }
//    }

//    private void Update()
//    {
//        // クリアBGMが再生終了したら通常BGMに戻す
//        if (isClearPlaying && !audioSource.isPlaying)
//        {
//            isClearPlaying = false;
//            PlayNormalBGM();
//        }
//    }

//    // ボス戦開始時に呼ぶ
//    public void StartBossBattle()
//    {
//        // クリアBGM中は切り替えさせない
//        if (isClearPlaying) return;

//        isBossBattle = true;
//        ChangeBGM(bossBGM, true); // ボスBGMをループ再生
//    }

//    // ボス戦終了時に呼ぶ
//    public void EndBossBattle()
//    {
//        // クリアBGM中は切り替えさせない
//        if (isClearPlaying) return;

//        isBossBattle = false;
//        PlayNormalBGM(); // 通常BGMに戻す
//    }

//    // クリア時に呼ぶ
//    public void PlayClearBGM()
//    {
//        isBossBattle = false;
//        isClearPlaying = true;

//        // 現在のBGMを停止
//        audioSource.Stop();

//        // ループなしでクリアBGMを再生
//        audioSource.loop = false;
//        audioSource.clip = clearBGM;
//        audioSource.Play();
//    }

//    // 通常BGMを再生
//    public void PlayNormalBGM()
//    {
//        ChangeBGM(normalBGM, true); // 通常BGMをループ再生
//    }

//    // BGMを切り替える共通処理
//    void ChangeBGM(AudioClip clip, bool loop)
//    {
//        // 同じBGM・同じループ設定なら何もしない
//        if (audioSource.clip == clip && audioSource.loop == loop) return;

//        // BGMを切り替えて再生
//        audioSource.Stop();
//        audioSource.clip = clip;
//        audioSource.loop = loop;
//        audioSource.Play();
//    }
//}
