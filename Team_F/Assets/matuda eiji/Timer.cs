using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    float time;

    void Start()
    {
        timerText.text = "TEST";
    }


    void Update()
    {
        time += Time.deltaTime;
        timerText.text = time.ToString("F2");
    }
}
