using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    [SerializeField] string nextLevelName = null;
    [SerializeField] LevelTimer levelTimer; 

    void Start()
    {
        if (levelTimer == null)
        {
            Debug.LogError("LevelTimer not assigned!");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && nextLevelName != null)
        {
            levelTimer.StopTimer(); 
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextLevelName);
        }
    }
}
