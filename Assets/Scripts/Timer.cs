using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float time = 0;
    private bool pauseTime = true;

    // Update is called once per frame
    void Update()
    {
        if (!pauseTime)
        {
            time = time + Time.deltaTime;
        }

        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        int milliseconds = Mathf.FloorToInt((time * 1000) % 1000);

        timerText.text = $"{minutes:00} : {seconds:00} : {milliseconds:000}";
    
    }
    public void StartTime()
    {
        pauseTime = false;
    }
    public void PauseTime()
    {
        pauseTime = true;
    }
    public void ResetTime()
    {
        time = 0;
    }

}
