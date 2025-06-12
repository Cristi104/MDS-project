using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles player interaction with an exit door that loads the next level.
/// </summary>
/// <remarks>
/// This component triggers a level transition when the player enters its collider,
/// stops the level timer, and loads the specified next level.
/// </remarks>
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
            Debug.LogError("LevelTimer not assigned to ExitDoor!", this);
        }
    }

    /// <summary>
    /// Handles trigger collisions with the player to initiate level transition.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !string.IsNullOrEmpty(nextLevelName))
        {
            levelTimer.StopTimer();
            SceneManager.LoadScene(nextLevelName);
        }
    }
}