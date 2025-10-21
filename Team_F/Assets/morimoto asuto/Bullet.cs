using System.Collections;
using System.Collections.Generic;
using  UnityEngine;

public class NewEmptyCSharpScript : MonoBehaviour
{

    //Update is called once per frame
     void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(un);  // âΩÇ©Ç…ê⁄êGÇµÇΩÇÁè¡Ç∑ÅB
    }
}
