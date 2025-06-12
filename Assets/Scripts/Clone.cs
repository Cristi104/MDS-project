using UnityEngine;
using System;

/// <summary>
/// Represents a clone character that replays recorded player movements and actions.
/// </summary>
/// <remarks>
/// This class handles clone behavior including movement replay, visual appearance,
/// death/respawn mechanics, and event handling.
/// </remarks>
public class Clone : MonoBehaviour, IPlayer, IEventListener
{
    private Rigidbody2D _body;
    private Animator _animator;
    private ReplayData _replay;
    private SpriteRenderer _spriteRenderer;
    private int _index;

    [SerializeField]
    private GameObject deathParticle;

    private void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _index = 0;

        Color[] presetColors = new Color[]
        {
            Color.red,
            Color.green,
            Color.blue,
            Color.yellow,
            Color.cyan,
            Color.magenta
        };

        // Set transparency for all preset colors
        for (int i = 0; i < presetColors.Length; i++)
        {
            presetColors[i].a = 0.7f;
        }

        int randomColorIndex = UnityEngine.Random.Range(0, presetColors.Length);
        _spriteRenderer.color = presetColors[randomColorIndex];

        ParticleSystem.MainModule mainModule = GetComponent<ParticleSystem>().main;
        mainModule.startColor = presetColors[randomColorIndex];
    }

    /// <summary>
    /// Handles the clone's death sequence.
    /// </summary>
    public void Die()
    {
        GameObject particle = Instantiate(deathParticle);
        particle.transform.position = transform.position;
        transform.position = new Vector3(100, 100, 100);
        _index = _replay.positions.Count + 1;
    }

    /// <summary>
    /// Resets the clone to its initial state.
    /// </summary>
    public void Respawn()
    {
        _index = 0;
        gameObject.layer = LayerMask.NameToLayer("Clone");
    }

    /// <summary>
    /// Handles incoming events.
    /// </summary>
    /// <param name="eventName">The name of the event to handle.</param>
    public void UpdateEvent(string eventName)
    {
        if (eventName == "reset")
        {
            Respawn();
        }
    }

    private void Update()
    {
        // Null check for replay data
        if (_replay == null || _replay.positions == null ||
            _replay.animations == null || _replay.facingDirections == null)
        {
            return;
        }

        // Update positions and animations
        if (_index < _replay.positions.Count)
        {
            transform.position = _replay.positions[_index];
        }

        if (_index < _replay.animations.Count)
        {
            _animator.Play(_replay.animations[_index]);
        }
        else
        {
            _animator.Play("Stand");
        }

        if (_index < _replay.facingDirections.Count)
        {
            transform.localScale = new Vector3(_replay.facingDirections[_index], 1, 1);
        }

        _index++;
    }

    /// <summary>
    /// Sets the replay data for this clone to follow.
    /// </summary>
    /// <param name="replayData">The replay data containing movement and animation information.</param>
    public void SetReplayData(ReplayData replayData)
    {
        _replay = replayData;
    }
}