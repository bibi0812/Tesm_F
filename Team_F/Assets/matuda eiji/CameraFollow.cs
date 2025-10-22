using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // インスペクターから追従対象のボールを設定できるようにする
    public Transform target;

    // カメラとボールの初期位置の差（オフセット）を保持する変数
    private Vector3 offset;

    void Start()
    {
        // カメラとボールの現在の位置の差を計算し、保存する
        // これで、カメラとボールの相対的な位置関係を固定できる
        offset = transform.position - target.position;
    }

    // プレイヤーの移動処理が完了した後で実行されるLateUpdateを使うのが一般的
    void LateUpdate()
    {
        if (target != null)
        {
            // ボールの現在位置にオフセットを足した位置にカメラを移動させる
            transform.position = target.position + offset;
        }
    }
}