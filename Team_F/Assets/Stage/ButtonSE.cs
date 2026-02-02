using UnityEngine;

public class ButtonSE : MonoBehaviour
{
    public void PlaySE()
    {
        if (SEManager.Instance != null)
        {
            SEManager.Instance.PlayClick();
        }
    }
}
