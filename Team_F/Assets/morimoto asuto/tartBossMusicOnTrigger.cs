using UnityEngine;

public class StartBossMusicOnTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Boss Area Triggered - StartBossMusic");

            // BGMコントローラを探して開始処理
            BGMBossController bgm = FindObjectOfType<BGMBossController>();
            if (bgm != null)
            {
                bgm.StartBossMusic();
            }
            else
            {
                Debug.LogError("BGMBossController がシーンに存在しません！");
            }
        }
    }
}
