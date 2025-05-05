using UnityEngine;

public class Button : MonoBehaviour
{
    private int pressers = 0;
    private Animator animator;

    [SerializeField] private Activateable activateableObject;
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // if there is no one on the button call open and
            if (pressers == 0)
            {
                animator.enabled = true;
                animator.Play("ButtonPress");
                activateableObject.Activate();
            }
            pressers++;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            pressers--;
            // when everyone leaves the button call close
            if (pressers == 0 && activateableObject != null)
            {
                animator.enabled = true;
                animator.Play("ButtonUnpress");

                if (activateableObject != null)
                {
                    activateableObject.Deactivate();
                }
            }
        }
    }

    void Update()
    {
        
    }
}
