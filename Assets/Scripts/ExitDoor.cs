using UnityEngine;

public class ExitDoor : MonoBehaviour
{

    [SerializeField] string nextLevelName = null;

    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // when a player or clone colides with the spikes call Death
        if (other.CompareTag("Player") && nextLevelName != null)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextLevelName);
        }
    }

    void Update()
    {
        
    }
}
