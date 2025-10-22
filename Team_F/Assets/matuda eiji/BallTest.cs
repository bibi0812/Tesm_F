using UnityEngine;

public class BallTest: MonoBehaviour
{
   
    public float speed = 5f; // ��������

    void Update()
    {
        float moveX = 0f;
        float moveY = 0f;

        // �� �� �ړ�
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveX = -1f;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            moveX = 1f;
        }

        // �� �ړ�
        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveY = 3f;
        }

        // ���ۂɈړ�������
        transform.Translate(new Vector3(moveX, moveY, 0) * speed * Time.deltaTime);
    }
}


