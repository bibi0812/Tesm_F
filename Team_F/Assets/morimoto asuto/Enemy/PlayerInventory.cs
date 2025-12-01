using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int keyCount = 0;

    public void AddKey(int amount)
    {
        keyCount += amount;
        Debug.Log("Œ®‚ğæ“¾I Œ»İ‚ÌŒ®‚Ì”: " + keyCount);
    }

    public bool UseKey(int amount)
    {
        if (keyCount >= amount)
        {
            keyCount -= amount;
            return true;
        }
        return false;
    }
}
