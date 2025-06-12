using UnityEngine;

/// <summary>
/// Handles collision detection with spikes that cause player death.
/// </summary>
public class NewMonoBehaviourScript : MonoBehaviour
{

    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            IPlayer player = other.GetComponent<IPlayer>();
            if(player != null) player.Die();
        }
    }

    void Update()
    {
        
    }
}
