using System.Collections;

using System.Collections.Generic;

using UnityEngine;

public class CannonController : MonoBehaviour

{

    public GameObject objPrrefad;

    public float fireSpeed = 10.0f;  // ���ˑ��x

    Transform gateTransform;

    void Start()

    {

        gateTransform = transform.Find("gate");

        if (gateTransform == null)

        {

            Debug.LogError("gate�I�u�W�F�N�g��������܂���B");

        }

    }

    void Update()

    {

        // ���N���b�N�����o

        if (Input.GetMouseButtonDown(0))

        {

            Shoot();

        }

    }

    void Shoot()

    {

        // �}�E�X�̃��[���h���W���擾

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mouseWorldPos.z = 0f;  // 2D�Ȃ̂�Z���W��0��

        // Vector3 �� Vector2 �ɕϊ�

        Vector2 mousePos2D = new Vector2(mouseWorldPos.x, mouseWorldPos.y);

        // ���ˈʒu�igate�̈ʒu�j

        Vector2 firePosition = gateTransform.position;

        // ���˕����𐳋K�����Čv�Z

        Vector2 direction = (mousePos2D - firePosition).normalized;

        // �e�𐶐�
        GameObject obj = Instantiate(objPrrefad, firePosition, Quaternion.identity);

        // Rigidbody2D�ɗ͂�������
        Rigidbody2D rbody = obj.GetComponent<Rigidbody2D>();
        rbody.AddForce(direction * fireSpeed, ForceMode2D.Impulse);


        // �C��̌������}�E�X�����Ɍ��������ꍇ�͈ȉ����A���R�����g

        /*

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);

        */

    }

}
