using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// Tracks and displays level completion time, including best time records.
/// </summary>
/// <remarks>
/// This component handles real-time timer display, stops timing when level completes,
/// and manages persistent storage of best times using PlayerPrefs.
/// </remarks>
public class LevelTimer : MonoBehaviour
{
    [SerializeField]
    private TMP_Text timerText;

    [SerializeField]
    private TMP_Text bestTimeText;

    private float _timeElapsed;
    private bool _isRunning = true;
    private string _bestTimeKey;
    private const float DEFAULT_BEST_TIME = float.MaxValue;

    private void Start()
    {
        _bestTimeKey = $"BestTime_{SceneManager.GetActiveScene().name}";
        DisplayBestTime();
    }

    private void Update()
    {
        if (!_isRunning) return;

        _timeElapsed += Time.deltaTime;
        timerText.text = $"Time: {_timeElapsed:F2}s";
    }

    /// <summary>
    /// Stops the timer and checks/saves if the current time is a new record.
    /// </summary>
    public void StopTimer()
    {
        _isRunning = false;

        float bestTime = PlayerPrefs.GetFloat(_bestTimeKey, DEFAULT_BEST_TIME);
        if (_timeElapsed < bestTime)
        {
            PlayerPrefs.SetFloat(_bestTimeKey, _timeElapsed);
            Debug.Log($"New Best Time for {SceneManager.GetActiveScene().name}: {_timeElapsed:F2}");
        }

        DisplayBestTime();
    }

    /// <summary>
    /// Updates the UI with the current best time for this level.
    /// </summary>
    private void DisplayBestTime()
    {
        float bestTime = PlayerPrefs.GetFloat(_bestTimeKey, DEFAULT_BEST_TIME);
        bestTimeText.text = bestTime == DEFAULT_BEST_TIME
            ? "Best Time: --"
            : $"Best Time: {bestTime:F2}s";
    }
}