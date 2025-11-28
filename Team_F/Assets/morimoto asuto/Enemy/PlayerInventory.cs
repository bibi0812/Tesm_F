using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int redKeys = 0;

    // カギを取得
    public void AddKey(int amount)
    {
        redKeys += amount;
        Debug.Log("赤いカギを取得！ 現在の数: " + redKeys);
    }

    // カギを消費して開ける
    public bool UseKey(int amount = 1)
    {
        if (redKeys >= amount)
        {
            redKeys -= amount;
            Debug.Log("赤いカギを使用！ 残り: " + redKeys);
            return true;
        }
        else
        {
            Debug.Log("カギが足りない！");
            return false;
        }
    }
}
