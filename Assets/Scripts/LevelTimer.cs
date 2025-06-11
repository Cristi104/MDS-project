using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelTimer : MonoBehaviour
{
    public TMP_Text timerText;
    public TMP_Text bestTimeText;
    private float timeElapsed;
    private bool isRunning = true;  
    private string bestTimeKey;

    void Start()
    {
        bestTimeKey = "BestTime_" + UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        DisplayBestTime();
    }

    void Update()
    {
        if (isRunning)
        {
            timeElapsed += Time.deltaTime;
            timerText.text = "Time: " + timeElapsed.ToString("F2") + "s";
        }
    }

    public void StopTimer()
    {
        isRunning = false;

        float bestTime = PlayerPrefs.GetFloat(bestTimeKey, float.MaxValue);
        if (timeElapsed < bestTime)
        {
            PlayerPrefs.SetFloat(bestTimeKey, timeElapsed);
            Debug.Log("New Best Time for " + UnityEngine.SceneManagement.SceneManager.GetActiveScene().name + ": " + timeElapsed);
        }

        DisplayBestTime();
    }

    void DisplayBestTime()
    {
        float bestTime = PlayerPrefs.GetFloat(bestTimeKey, float.MaxValue);
        if (bestTime == float.MaxValue)
        {
            bestTimeText.text = "Best Time: --";
        }
        else
        {
            bestTimeText.text = "Best Time: " + bestTime.ToString("F2") + "s";
        }
    }
}
