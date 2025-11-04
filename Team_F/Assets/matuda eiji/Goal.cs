using UnityEngine;

public class GoalChecker : MonoBehaviour
{
    public GameObject clearText; // CLEAR! のUIをInspectorにセット

    private void Start()
    {
        // 念のため最初に非表示にしておく
        clearText.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Gole"))
        {
            clearText.SetActive(true);
            Time.timeScale = 0f; // ゲームを止める（任意）
        }
    }
}
