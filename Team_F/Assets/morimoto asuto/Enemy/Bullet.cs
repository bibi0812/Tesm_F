using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �G�i�^�O�� "Enemy"�j�ɐڐG������A���̒e������
        if (collision.CompareTag("Enemy")) 
        {
            Destroy(gameObject);
        }
    }
}
