using UnityEngine;

public class BlockManager : MonoBehaviour
{
    private BreakBlock[] allBlocks;

    void Start()
    {
        // シーン内の全ブロックを最初に記録
        allBlocks = FindObjectsOfType<BreakBlock>(true); // 非アクティブも含めて取得
    }

    public void ResetAllBlocks()
    {
        foreach (var block in allBlocks)
        {
            block.gameObject.SetActive(true);
        }

        Debug.Log("ブロックをリセットしました。");
    }
}
