using UnityEngine;

/// <summary>
/// Handles button press interactions, animations, and sound effects.
/// </summary>
/// <remarks>
/// This component manages button state, plays animations and sound effects,
/// and controls an associated activatable object when pressed/unpressed.
/// </remarks>
public class Button : MonoBehaviour
{
    private int _pressers = 0;
    private Animator _animator;
    private AudioSource _audioSource;

    private const string SOUND_FILE = "Free UI Click Sound Effects Pack/AUDIO/Button/SFX_UI_Button_Organic_Plastic_Thin_Generic_3";

    [SerializeField]
    private Activateable activateableObject;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = Resources.Load<AudioClip>(SOUND_FILE);
        _animator = GetComponent<Animator>();
        _animator.enabled = false;
    }

    /// <summary>
    /// Handles collision when an object enters the button's trigger area.
    /// </summary>
    /// <param name="other">The collider of the object that entered the trigger.</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!gameObject.activeInHierarchy) return;

        if (other.CompareTag("Player"))
        {
            if (_pressers == 0)
            {
                _animator.enabled = true;
                _animator.Play("ButtonPress");
                activateableObject.Activate();
                _audioSource.Play();
            }
            _pressers++;
        }
    }

    /// <summary>
    /// Handles collision when an object exits the button's trigger area.
    /// </summary>
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!gameObject.activeInHierarchy) return;

        if (other.CompareTag("Player"))
        {
            _pressers--;
            if (_pressers == 0 && activateableObject != null)
            {
                _animator.enabled = true;
                _animator.Play("ButtonUnpress");
                _audioSource.Play();
                activateableObject.Deactivate();
            }
        }
    }

    private void Update()
    {

    }
}