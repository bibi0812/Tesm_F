using UnityEngine;

public class GoalEffect : MonoBehaviour
{
    public ParticleSystem crackerLeft;
    public ParticleSystem crackerRight;

    private bool played = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (played) return;

        if (collision.CompareTag("Player"))
        {
            played = true;

            // クラッカー発射
            if (crackerLeft != null) crackerLeft.Play();
            if (crackerRight != null) crackerRight.Play();
        }
    }
}
