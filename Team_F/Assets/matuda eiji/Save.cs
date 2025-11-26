using UnityEngine;

public class Save : MonoBehaviour
{
    public static Save instance;

    public Vector3 respawnPoint; // 復活地点

    private void Awake()
    {
        // シングルトン（1つだけ存在）
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
