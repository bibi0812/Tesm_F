using UnityEngine;

public class Braek : MonoBehaviour // �N���X���𕪂���₷���ύX����
{
    // 2D�����Փ˂����������Ƃ��ɌĂ΂��i�N���X�̒��ڂ̃����o�[�Ƃ��Ē�`�j
    void OnCollisionEnter2D(Collision2D collision)
    {
        // �Փˑ��肪�uBall�v�^�O�������Ă��邩�`�F�b�N
        if (collision.gameObject.CompareTag("Dead"))
        {

            // 2. ���̃u���b�N�̃Q�[���I�u�W�F�N�g��j��i�폜�j����
            Destroy(gameObject);
        }
    }
}