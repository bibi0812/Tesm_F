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
        }
    }
}
