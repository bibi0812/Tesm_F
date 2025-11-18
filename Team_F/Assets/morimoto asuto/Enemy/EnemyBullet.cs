using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float deleteTime = 3.0f;  //削除する時間

     void Start()
    {
        Destroy(gameObject, deleteTime);    //削除設定  
    }

    private void Update()
    {
        
    }

     void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);   //何かに接触したら消す
    }
}