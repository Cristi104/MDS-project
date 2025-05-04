using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{

    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // when a player or clone colides with the spikes call Death
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
