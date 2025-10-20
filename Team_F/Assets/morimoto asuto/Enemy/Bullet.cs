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
        // 敵（タグが "Enemy"）に接触したら、この弾を消す
        if (collision.CompareTag("Enemy")) 
        {
            Destroy(gameObject);
        }
    }
}
