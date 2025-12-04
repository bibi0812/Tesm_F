////using UnityEngine;

////public class Menu : MonoBehaviour
////{
////    [Header("ポーズUI")]
////    public GameObject pauseUI;

////    [Header("ゴールUI")]
////    public GameObject goalUI;   // ← これを追加（ここにゴールUIを入れる）

////    private bool isPaused = false;

////    void Start()
////    {
////        pauseUI.SetActive(false);
////        goalUI.SetActive(false);
////    }

////    void Update()
////    {
////        // ゴールUIが出ている時はメニュー禁止
////        if (goalUI != null && goalUI.activeSelf)
////            return;

////        if (Input.GetKeyDown(KeyCode.Escape))
////        {
////            if (isPaused)
////                ResumeGame();
////            else
////                PauseGame();
////        }
////    }
////    private void OnTriggerEnter2D(Collider2D collision)
////    {
////        if (collision.CompareTag("Goal"))
////        {
////            goalUI.SetActive(true);
////            Time.timeScale = 0f; // ゲームを止める（任意）
////        }
////    }

////    public void PauseGame()
////    {
////        pauseUI.SetActive(true);
////        Time.timeScale = 0f;
////        isPaused = true;
////    }

////    public void ResumeGame()
////    {
////        pauseUI.SetActive(true);
////        Time.timeScale = 1f;
////        isPaused = false;
////    }
////}


//using UnityEngine;

//public class Menu : MonoBehaviour
//{
//    [Header("ポーズUI")]
//    public GameObject pauseUI;

//    [Header("ゴールUI")]
//    public GameObject goalUI;

//    private bool isPaused = false;

//    void Start()
//    {
//        pauseUI.SetActive(false);
//        goalUI.SetActive(false);
//    }

//    void Update()
//    {
//        // ゴールUIが出ている時はポーズ禁止
//        if (goalUI != null && goalUI.activeSelf)
//            return;

//        // Esc キーでポーズ → 解除
//        if (Input.GetKeyDown(KeyCode.Escape))
//        {
//            if (isPaused)
//                ResumeGame();
//            else
//                PauseGame();
//        }
//    }

//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (collision.CompareTag("Goal"))
//        {
//            goalUI.SetActive(true);
//            Time.timeScale = 0f;
//        }
//    }

//    public void PauseGame()
//    {
//        pauseUI.SetActive(true);
//        Time.timeScale = 0f;  // ゲーム停止
//        isPaused = true;
//    }

//    public void ResumeGame()
//    {
//        pauseUI.SetActive(false);
//        Time.timeScale = 1f;  // ゲーム再開
//        isPaused = false;
//    }
//}
using UnityEngine;

public class Menu : MonoBehaviour
{
    [Header("ポーズUI")]
    public GameObject pauseUI;

    [Header("ゴールUI")]
    public GameObject goalUI;

    // ★どこからでも参照できる「ポーズ中フラグ」
    public static bool isPaused = false;

    void Start()
    {
        pauseUI.SetActive(false);
        goalUI.SetActive(false);
        isPaused = false;
    }

    void Update()
    {
        // ゴールUIが出ている時はポーズ禁止
        if (goalUI != null && goalUI.activeSelf)
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Goal"))
        {
            goalUI.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
        }
    }

    public void PauseGame()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;     // ★ 追加
    }

    public void ResumeGame()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;    // ★ 追加
    }
}

