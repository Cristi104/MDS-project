using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{

    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if(player != null) player.Death();

            Clone clone = other.GetComponent<Clone>();
            if (clone != null) clone.Death();
        }
    }

    void Update()
    {
        
    }
}
