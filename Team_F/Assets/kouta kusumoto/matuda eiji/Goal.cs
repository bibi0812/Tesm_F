using UnityEngine;
using UnityEngine.SceneManagement; // �V�[���؂�ւ��Ɏg���ꍇ

public class Goal : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // �{�[���Ƀ^�O "Ball" �����Ă�����N���A
        if (other.CompareTag("Boll"))
        {
            Debug.Log("�S�[���I�N���A�I");

            // �����ŃV�[����؂�ւ����胁�b�Z�[�W���o������ł���
            // ��F���̃V�[����
            // SceneManager.LoadScene("NextStage");

            // ��F�V���v���ɒ�~����ꍇ
            Time.timeScale = 5;
        }
    }
}
