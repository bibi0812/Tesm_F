using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public float speed = 5f;//��l��������

    // Update is called once per frame
    void Update()
    {
        //��l��������
        // ���L�[���͂̎擾
        float moveX = 0f;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveX = 1f;  // ���L�[�ŉE��
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveX = -1f; // ���L�[�ō���
        }

        // �v���C���[���ړ�������
        transform.Translate(moveX * speed * Time.deltaTime, 0, 0);
    }
}
