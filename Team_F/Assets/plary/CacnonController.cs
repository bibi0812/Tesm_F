using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �}�E�X�N���b�N�ɉ����ăI�u�W�F�N�g�i�e�j�𔭎˂���C��̃R���g���[���[
/// </summary>
public class CannonController : MonoBehaviour
{
    public GameObject objPrefab; // ���˂���I�u�W�F�N�g�i�e�j�̃v���n�u

    public float fireSpeed = 20.0f;  // �e�̔��ˑ��x

    private Transform gateTransform; // ���ˌ��igate�j��Transform

    void Start()
    {
        // ���̃I�u�W�F�N�g�̎q���� "gate" �Ƃ������O�̃I�u�W�F�N�g��T��
        gateTransform = transform.Find("gate");
        if (gateTransform == null)
        {
            Debug.LogError("gate�I�u�W�F�N�g��������܂���B�q�G�����L�[���� 'gate' �Ƃ������O�̎q�I�u�W�F�N�g������܂����H");
        }

        // �v���n�u���ݒ肳��Ă��Ȃ��ꍇ�Ɍx�����o��
        if (objPrefab == null)
        {
            Debug.LogError("objPrefab ���ݒ肳��Ă��܂���B�C���X�y�N�^�Ńv���n�u�����蓖�ĂĂ��������B");
        }
    }

    void Update()
    {
        // �}�E�X�̍��N���b�N�������ꂽ�Ƃ��ɒe�𔭎�
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    /// <summary>
    /// �e�𐶐����Ĕ��˂��鏈��
    /// </summary>
    void Shoot()
    {
        

        

            // �K�v�ȏ�񂪑����Ă��Ȃ���Ώ������Ȃ�

            if (objPrefab == null || gateTransform == null) return;

            // ���C���J���������݂��邩�`�F�b�N

            if (Camera.main == null)

            {

                Debug.LogError("Main Camera ��������܂���B");

                return;

            }

            // �}�E�X�̃X�N���[�����W�����[���h���W�ɕϊ�

            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            mouseWorldPos.z = 0f; // 2D�Ȃ̂�Z���W��0�ɌŒ�

            // ���ˈʒu�ƃ}�E�X�ʒu��2D�x�N�g���Ŏ擾

            Vector2 mousePos2D = new Vector2(mouseWorldPos.x, mouseWorldPos.y);

            Vector2 firePosition = new Vector2(gateTransform.position.x, gateTransform.position.y);

            // ���˕������v�Z�����K���i�����x�N�g���𒷂�1�ɂ���j

            Vector2 direction = (mousePos2D - firePosition).normalized;

            // ���˕����ɉ������p�x���v�Z�i���W�A�����x�j

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            Quaternion rot = Quaternion.Euler(0, 0, angle); // ��]�����쐬

            // �e�𐶐��i�ʒu�Ɗp�x���w��j

            GameObject obj = Instantiate(objPrefab, firePosition, rot);

            // �e��Rigidbody2D���A�^�b�`����Ă���Η͂������Ĕ���

            Rigidbody2D rbody = obj.GetComponent<Rigidbody2D>();

            if (rbody != null)

            {

                rbody.AddForce(direction * fireSpeed, ForceMode2D.Impulse);

            }

            else

            {

                Debug.LogWarning("�������ꂽ�e�� Rigidbody2D ���A�^�b�`����Ă��܂���B�v���n�u���m�F���Ă��������B");

            }

            // �� �C��̌�����e�̔��˕����ɕς������ꍇ�͂��̍s��L���ɂ���

            //transform.rotation = rot;

        

    }

}