using UnityEngine;

public class EnemyBreathAI : MonoBehaviour
{
    public Transform player;         // プレイヤーのTransformをInspectorで指定
    public float attackRange = 5f;   // ブレスを吐く距離（例：5メートル以内）
    public float breathCooldown = 3f; // クールタイム3秒
    private bool canBreath = true;

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        // プレイヤーが一定距離以内に入ったら攻撃
        if (distance <= attackRange && canBreath)
        {
            StartCoroutine(BreathAttack());
        }
    }

    private System.Collections.IEnumerator BreathAttack()
    {
        canBreath = false;

        // 向きをプレイヤーの方向に向ける
        Vector3 direction = (player.position - transform.position).normalized;
        transform.forward = new Vector3(direction.x, 0, direction.z);

        Debug.Log("?? ブレス攻撃発動！");

        // ブレス攻撃の実処理（エフェクト・ダメージなど）
        // ここにParticleSystemや当たり判定処理を入れる
        yield return new WaitForSeconds(breathCooldown);

        canBreath = true;
        Debug.Log("?? ブレス攻撃が再使用可能になりました！");
    }
}
