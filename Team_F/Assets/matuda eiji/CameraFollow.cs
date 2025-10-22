using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // �C���X�y�N�^�[����Ǐ]�Ώۂ̃{�[����ݒ�ł���悤�ɂ���
    public Transform target;

    // �J�����ƃ{�[���̏����ʒu�̍��i�I�t�Z�b�g�j��ێ�����ϐ�
    private Vector3 offset;

    void Start()
    {
        // �J�����ƃ{�[���̌��݂̈ʒu�̍����v�Z���A�ۑ�����
        // ����ŁA�J�����ƃ{�[���̑��ΓI�Ȉʒu�֌W���Œ�ł���
        offset = transform.position - target.position;
    }

    // �v���C���[�̈ړ�����������������Ŏ��s�����LateUpdate���g���̂���ʓI
    void LateUpdate()
    {
        if (target != null)
        {
            // �{�[���̌��݈ʒu�ɃI�t�Z�b�g�𑫂����ʒu�ɃJ�������ړ�������
            transform.position = target.position + offset;
        }
    }
}