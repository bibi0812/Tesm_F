using UnityEngine;

public class start : MonoBehaviour
{
    public Transform startPoint;

    void Start()
    {
        // ゲーム開始時にスタート地点へ移動
        if (startPoint != null)
        {
            transform.position = startPoint.position;
        }
    }
}
