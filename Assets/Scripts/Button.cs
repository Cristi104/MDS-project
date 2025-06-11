using UnityEngine;

/// <summary>
/// Handles button press interactions, animations, and sound effects.
/// </summary>
public class Button : MonoBehaviour
{
    private int pressers = 0;
    private Animator animator;
    private AudioSource audioSource;

    private const string SoundFile = "Free UI Click Sound Effects Pack/AUDIO/Button/SFX_UI_Button_Organic_Plastic_Thin_Generic_3";

    [SerializeField]
    private Activateable activateableObject;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Resources.Load<AudioClip>(SoundFile);
        animator = GetComponent<Animator>();
        animator.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!gameObject.activeInHierarchy) return;

        if (other.CompareTag("Player"))
        {
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

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!gameObject.activeInHierarchy) return;

        if (other.CompareTag("Player"))
        {
            pressers--;
            if (pressers == 0 && activateableObject != null)
            {
                animator.enabled = true;
                animator.Play("ButtonUnpress");
                audioSource.Play();
                activateableObject.Deactivate();
            }
        }
    }

    private void Update()
    {

    }
}