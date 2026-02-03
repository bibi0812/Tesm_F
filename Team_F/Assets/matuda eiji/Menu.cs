using UnityEngine;

public class Menu : MonoBehaviour
{
    [Header("ポーズUI")]
    public GameObject pauseUI;

    [Header("ゴールUI")]
    public GameObject goalUI;

    [Header("クラッカー")]
    public GameObject crackerPrefab;
    public Transform leftSpawn;
    public Transform rightSpawn;

    public static bool isPaused = false;

    void Start()
    {
        pauseUI.SetActive(false);
        goalUI.SetActive(false);
        isPaused = false;
    }

    void Update()
    {
        if (GameManager.isDead)
            return;

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

            // ★ クラッカー発射
            SpawnCracker();

            // 少し待ってから止める
            Invoke(nameof(StopGame), 1.0f);
        }
    }

    void StopGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
    }

    void SpawnCracker()
    {
        Debug.Log("SpawnCracker 呼ばれた");

        if (crackerPrefab == null || leftSpawn == null || rightSpawn == null)
        {
            Debug.LogError("クラッカーの設定が足りない！");
            return;
        }

        GameObject left = Instantiate(crackerPrefab, leftSpawn.position, Quaternion.identity);
        left.GetComponent<Cracker>().Init(Vector2.right);

        GameObject right = Instantiate(crackerPrefab, rightSpawn.position, Quaternion.identity);
        right.GetComponent<Cracker>().Init(Vector2.left);

        //// 左
        //GameObject left = Instantiate(crackerPrefab, leftSpawn.position, Quaternion.identity);
        //left.GetComponent<Cracker>().Init(Vector2.right);

        //// 右
        //GameObject right = Instantiate(crackerPrefab, rightSpawn.position, Quaternion.identity);
        //right.GetComponent<Cracker>().Init(Vector2.left);
    }

    public void PauseGame()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
}





//using UnityEngine;

//public class Menu : MonoBehaviour
//{
//    [Header("ポーズUI")]
//    public GameObject pauseUI;

//    [Header("ゴールUI")]
//    public GameObject goalUI;

//    // ★どこからでも参照できる「ポーズ中フラグ」
//    public static bool isPaused = false;

//    void Start()
//    {
//        pauseUI.SetActive(false);
//        goalUI.SetActive(false);
//        isPaused = false;
//    }

//    void Update()
//    {
//        // ★ 死亡中は Esc を押しても何も起きない
//        if (GameManager.isDead)
//            return;

//        // ゴールUIが出ている時はポーズ禁止
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

//            //SpawnCracker();   // ← 先に出す
//                              // Time.timeScale = 0f; ← 消す or 後で止める
//        }

//        //if (collision.CompareTag("Goal"))
//        //{
//        //    goalUI.SetActive(true);
//        //    Time.timeScale = 0f;
//        //    isPaused = true;
//        //}
//    }

//    public void PauseGame()
//    {
//        pauseUI.SetActive(true);
//        Time.timeScale = 0f;
//        isPaused = true;     // ★ 追加
//    }

//    public void ResumeGame()
//    {
//        pauseUI.SetActive(false);
//        Time.timeScale = 1f;
//        isPaused = false;    // ★ 追加
//    }
//}

