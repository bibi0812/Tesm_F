using UnityEngine;

public class GoalChecker : MonoBehaviour
{
    public GameObject clearText; // CLEAR! ��UI��Inspector�ɃZ�b�g

    private void Start()
    {
        // �O�̂��ߍŏ��ɔ�\���ɂ��Ă���
        clearText.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Gole"))
        {
            clearText.SetActive(true);
            Time.timeScale = 0f; // �Q�[�����~�߂�i�C�Ӂj
        }
    }
}
