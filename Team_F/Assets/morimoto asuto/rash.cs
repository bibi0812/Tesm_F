<<<<<<< HEAD
=======


/*<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 334bd7ee685a91c790283b6fe1839bde97ba394e
/*<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> dacd970a26d343fcb975aaf5b9e29f8f799c345b
=======
<<<<<<< HEAD
>>>>>>> ef3b806d3bf78756770d6697cee143eedf66aae2

=======
<<<<<<< HEAD
>>>>>>> c3a5909344fc736736c4d085e711e011cc979074
/*
=======
using System.Collections;
>>>>>>> 9d4c18309d89640ff5633249bdc16e383bd89500
>>>>>>> 352b614a2a055fd4f9dd4b41b2b2b8ab01e57436
>>>>>>> a47ded5e5de7baa181b5a5fc648ba0103be991b9
using UnityEngine;

public class DashOnRightClick : MonoBehaviour
{
    public float dashDistance = 3f;   // �ːi�����i3�u���b�N���j
    public float maxClickInterval = 0.5f; // �A���N���b�N�̍ő�Ԋu�i�b�j

    private int rightClickCount = 0;
    private float lastClickTime = 0f;

    private bool isDashing = false;
    private Vector3 dashTarget;
    private float dashSpeed = 10f;

    void Update()
    {
        if (isDashing)
        {
            DashMove();
            return;
        }

        if (Input.GetMouseButtonDown(1)) // �E�N���b�N�����ꂽ��
        {
            float timeSinceLastClick = Time.time - lastClickTime;

            if (timeSinceLastClick <= maxClickInterval)
            {
                rightClickCount++;
            }
            else
            {
                rightClickCount = 1;
            }

            lastClickTime = Time.time;

            if (rightClickCount >= 3)
            {
                rightClickCount = 0;
                StartDash();
            }
        }
    }

    void StartDash()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = transform.position.z;

        Vector3 directionToMouse = (mouseWorldPos - transform.position).normalized;

        // �}�E�X�����̋t�����ɓːi�������ړ�����^�[�Q�b�g�ʒu���v�Z
        dashTarget = transform.position - directionToMouse * dashDistance;

        isDashing = true;
    }

    void DashMove()
    {
        // ���݈ʒu����^�[�Q�b�g�Ɍ������ēːi
        transform.position = Vector3.MoveTowards(transform.position, dashTarget, dashSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, dashTarget) < 0.01f)
        {
            isDashing = false;
        }
    }
}
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
*/
<<<<<<< HEAD
=======

>>>>>>> 334bd7ee685a91c790283b6fe1839bde97ba394e
