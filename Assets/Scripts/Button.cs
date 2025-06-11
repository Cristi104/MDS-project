using UnityEngine;

public class Button : MonoBehaviour
{
    private int pressers = 0;
    private Animator animator;
    private AudioSource audioSource;

    private string soundFile = "Free UI Click Sound Effects Pack/AUDIO/Button/SFX_UI_Button_Organic_Plastic_Thin_Generic_3";
    [SerializeField] private Activateable activateableObject;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Resources.Load<AudioClip>(soundFile);
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
                audioSource.Play();
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
                audioSource.Play();
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
