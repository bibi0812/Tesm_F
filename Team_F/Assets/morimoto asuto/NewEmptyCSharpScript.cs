using System.Collections;
using System.Collections.Generic;
using  UnityEngine;

public class NewEmptyCSharpScript : MonoBehaviour
{
    public float deletetime =3.0f ; //�폜���鎞�Ԏw��

    // Start is called before the first frame update
     void Start()
    {
        Destroy(gameObject, deletetime);     //�폜�ݒ�
    }

    //Update is called once per frame
     void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);  // �����ɐڐG����������B
    }
}
