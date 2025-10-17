using UnityEngine;

public class Braek : MonoBehaviour // �N���X���𕪂���₷���ύX����
{
    public GameObject breakEffectPrefab; // �j��G�t�F�N�g��Prefab���Z�b�g

    // 2D�����Փ˂����������Ƃ��ɌĂ΂��i�N���X�̒��ڂ̃����o�[�Ƃ��Ē�`�j
    void OnCollisionEnter2D(Collision2D collision)
    {
        // �Փˑ��肪�uBall�v�^�O�������Ă��邩�`�F�b�N�i�X�y���~�X���C���j
        if (collision.gameObject.CompareTag("Boll"))
        {
            // 1. �j��G�t�F�N�g���u���b�N�̈ʒu�Ő�������
            if (breakEffectPrefab != null)
            {
                // Instantiate(��������Prefab, �ʒu, ��])
                GameObject effect = Instantiate(breakEffectPrefab, transform.position, Quaternion.identity);
                // �G�t�F�N�g����莞�Ԍ�ɍ폜����i��: 2�b��j
                Destroy(effect, 2f);
            }

            // 2. ���̃u���b�N�̃Q�[���I�u�W�F�N�g��j��i�폜�j����
            Destroy(gameObject);
        }
    }
}