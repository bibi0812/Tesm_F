using UnityEngine;

public class Menu : MonoBehaviour
{
    [Header("ポーズUI (Panel)")]
    public GameObject pauseUI;

    private bool isPaused = false;

    void Start()
    {
        // 起動時はUIを消す
        pauseUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();   // 再開
            else
                PauseGame();    // ポーズ
        }
    }

    public void PauseGame()
    {
        pauseUI.SetActive(true);    // UIを表示
        Time.timeScale = 0f;        // ゲーム停止
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseUI.SetActive(false);   // UIを非表示
        Time.timeScale = 1f;        // ゲーム再開
        isPaused = false;
    }
}
