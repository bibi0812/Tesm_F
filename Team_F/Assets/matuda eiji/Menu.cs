//using UnityEngine;

//public class Menu : MonoBehaviour
//{
//    [Header("ポーズUI")]
//    public GameObject pauseUI;

//    [Header("ゴールUI")]
//    public GameObject goalUI;   // ← これを追加（ここにゴールUIを入れる）

//    [Header("スタートUI（カウントダウン）")]
//    public GameObject startUIButton;

//    private bool isPaused = false;

//    void Start()
//    {
//        pauseUI.SetActive(false);
//        goalUI.SetActive(false);
//    }

//    void Update()
//    {
//        // ゴールUIが出ている時はメニュー禁止
//        if (goalUI != null && goalUI.activeSelf)
//            return;

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
//            Time.timeScale = 0f; // ゲームを止める（任意）
//        }
//    }

//    public void PauseGame()
//    {
//        pauseUI.SetActive(true);
//        Time.timeScale = 0f;
//        isPaused = true;
//    }

//    public void ResumeGame()
//    {
//        startUIButton.SetActive(false);
//        Time.timeScale = 1f;
//        isPaused = false;
//    }
//}

using UnityEngine;

public class Menu : MonoBehaviour
{
    [Header("ポーズUI (MenuUI)")]
    public GameObject pauseUI;  // MenuUI

    [Header("ゴールUI")]
    public GameObject goalUI;

    [Header("MenuUI 内のスタートボタン")]
    public GameObject startUIButton;

    private bool isPaused = false;

    void Start()
    {
        pauseUI.SetActive(false);       // MenuUI 非表示
        startUIButton.SetActive(false); // ボタンも非表示
        if (goalUI != null) goalUI.SetActive(false);
    }

    void Update()
    {
        if (goalUI != null && goalUI.activeSelf)
            return; // ゴール中は無効

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ClosePauseMenu(); // Escでポーズ解除
            else
                PauseGame();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Goal"))
        {
            if (goalUI != null) goalUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void PauseGame()
    {
        pauseUI.SetActive(true);       // MenuUI 表示
        startUIButton.SetActive(false); // ボタン非表示
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ClosePauseMenu()
    {
        // MenuUIは表示したまま、ボタンだけ出す
        startUIButton.SetActive(true);
        // ゲームはまだ停止
    }

    public void ResumeGame()
    {
        // ボタン押したらMenuUIも消す
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
}

