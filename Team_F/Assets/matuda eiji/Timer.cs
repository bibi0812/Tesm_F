using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    float time;

    void Update()
    {
        time += Time.deltaTime;

        int minutes = (int)(time / 60f);   // •ª
        int seconds = (int)(time % 60f);   // •b

        timerText.text = $"{minutes:00}:{seconds:00}";
    }
}
