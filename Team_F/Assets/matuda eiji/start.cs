using UnityEngine;

public class start : MonoBehaviour
{
    public Transform startPoint;

    void Start()
    {
        // �Q�[���J�n���ɃX�^�[�g�n�_�ֈړ�
        if (startPoint != null)
        {
            transform.position = startPoint.position;
        }
    }
}
