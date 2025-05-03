using UnityEngine;

public class Button : MonoBehaviour
{
    private int pressers = 0;
    private Animator animator;

    [SerializeField] private Door door;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (pressers == 0)
            {
                animator.enabled = true;
                animator.Play("ButtonPress");
                door.Open();
            }
            pressers++;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            pressers--;
            if (pressers == 0)
            {
                animator.enabled = true;
                animator.Play("ButtonUnpress");
                door.Close();
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
