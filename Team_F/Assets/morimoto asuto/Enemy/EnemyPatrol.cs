using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float moveSpeed = 2f;        // �ړ����x
    public float moveDistance = 3f;     // �ړ����鋗��

    private Vector2 startPos;           // �����ʒu
    private bool movingRight = false;    // �E�ֈړ������ǂ���

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // �ړ������ɉ����Ĉʒu���X�V
        if (movingRight)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            if (transform.position.x >= startPos.x + moveDistance)
            {
                movingRight = false;
                Flip();
            }
        }
        else
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            if (transform.position.x <= startPos.x - moveDistance)
            {
                movingRight = true;
                Flip();
            }
        }
    }

    // �X�v���C�g�̌����𔽓]������i�K�v�ȏꍇ�j
    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1; // ���E���]
        transform.localScale = scale;
    }
}
