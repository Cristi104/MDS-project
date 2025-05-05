using UnityEngine;
using TMPro;
using UnityEngine.UI; 

public class LevelTimer : MonoBehaviour
{
    public TMP_Text timerText;
    private float timeElapsed;
    private bool isRunning = true;

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
        float bestTime = PlayerPrefs.GetFloat("BestTime", float.MaxValue); 
        if (timeElapsed < bestTime)
        {
            PlayerPrefs.SetFloat("BestTime", timeElapsed); 
            Debug.Log("New Best Time: " + timeElapsed);
        }
    }
}
