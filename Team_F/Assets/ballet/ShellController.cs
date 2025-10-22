using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Block") || collision.CompareTag("Enemy"))
        {
            Destroy(gameObject); // �ubleck�v�܂��́uEnemy�v�^�O�����I�u�W�F�N�g�ɓ���������e������
        }
        if (collision.CompareTag("Enemy")||collision.CompareTag("Braek"))
        {
            // �G�I�u�W�F�N�g���폜
            Destroy(collision.gameObject);

            // �e���폜
            Destroy(gameObject);
        }
    }
}
