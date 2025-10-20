using System.Collections;
using System.Collections.Generic;
using  UnityEngine;

public class NewEmptyCSharpScript : MonoBehaviour
{
    public float deletetime =3.0f ; //íœ‚·‚éŠÔw’è

    // Start is called before the first frame update
     void Start()
    {
        Destroy(gameObject, deletetime);     //íœİ’è
    }

    //Update is called once per frame
     void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);  // ‰½‚©‚ÉÚG‚µ‚½‚çÁ‚·B
    }
}
