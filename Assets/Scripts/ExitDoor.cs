using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    [SerializeField]
    private string nextLevelName = null;

    [SerializeField]
    private LevelTimer levelTimer;

    private void Start()
    {
        if (levelTimer == null)
        {
            Debug.LogError("LevelTimer not assigned!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && nextLevelName != null)
        {
            levelTimer.StopTimer();
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextLevelName);
        }
    }
}