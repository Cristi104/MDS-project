using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// Controls player movement, animations, and clone creation mechanics.
/// </summary>
/// <remarks>
/// Handles player physics, input processing, sound effects, and maintains
/// replay data for clone creation. Implements IPlayer interface.
/// </remarks>
public class Player : MonoBehaviour, IPlayer
{
    private Rigidbody2D _body;
    private Animator _animator;
    private int _facingDirection;
    private float _lastVelocity;
    private Vector3 _startPosition;
    private List<Clone> _clones;
    private AudioSource _audioSource;
    private int _frameCounter;
    private ReplayData _replay;

    private const string JUMP_SOUND_PATH = "Free UI Click Sound Effects Pack/AUDIO/Pop/SFX_UI_Click_Organic_Pop_Liquid_Thick_Generic_1";
    private const string RUN_SOUND_PATH = "Free UI Click Sound Effects Pack/AUDIO/Sci-Fi/SFX_UI_Click_Designed_Scifi_Thin_Negative_Back_1";
    private const float VELOCITY_THRESHOLD = 0.01f;
    private const float JUMP_VELOCITY_THRESHOLD = 0.001f;
    private const int RUN_SOUND_FRAME_INTERVAL = 6;

    [SerializeField]
    private float speed = 3f;

    [SerializeField]
    private int maxClones = 1;

    [SerializeField]
    private float jumpStrength = 5f;

    [SerializeField]
    private Clone cloneTemplate;

    [SerializeField]
    private GameObject deathParticle;

    private AudioClip _jumpSound;
    private AudioClip _runSound;

    private void Start()
    {
        _jumpSound = Resources.Load<AudioClip>(JUMP_SOUND_PATH);
        _runSound = Resources.Load<AudioClip>(RUN_SOUND_PATH);
        _audioSource = GetComponent<AudioSource>();
        _startPosition = transform.position;
        _animator = GetComponent<Animator>();
        _body = GetComponent<Rigidbody2D>();
        _replay = new ReplayData();
        _clones = new List<Clone>();
        _facingDirection = 1;
        _lastVelocity = 0f;
        _frameCounter = 0;
    }

    /// <summary>
    /// Resets player position and creates/manages clones.
    /// </summary>
    public void Respawn()
    {
        if (_clones.Count < maxClones)
        {
            Clone clone = Instantiate(cloneTemplate);
            clone.SetReplayData(_replay);
            _clones.Add(clone);
            EventManager.Instance.Subscribe(clone);
        }
        else
        {
            _clones[0].SetReplayData(_replay);
            _clones.Add(_clones[0]);
            _clones.RemoveAt(0);
        }

        EventManager.Instance.Notify("reset");
        _replay = new ReplayData();
        transform.position = _startPosition;
        _body.linearVelocity = Vector2.zero;
    }

    /// <summary>
    /// Handles player death sequence and respawn.
    /// </summary>
    public void Die()
    {
        GameObject particle = Instantiate(deathParticle);
        particle.transform.position = transform.position;
        Respawn();
    }

    /// <summary>
    /// Main game loop processing all player inputs and state updates.
    /// </summary>
    private void Update()
    {
        _frameCounter++;
        ProcessMovement();
        ProcessJump();
        UpdateAnimations();
        RecordReplayData();
        ProcessResetInput();
    }

    /// <summary>
    /// Calculates and applies horizontal movement based on player input.
    /// </summary>
    /// <remarks>
    /// Reads horizontal axis input and applies velocity while preserving vertical momentum.
    /// Does not handle jumping physics.
    /// </remarks>
    private void ProcessMovement()
    {
        Vector2 newVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, _body.linearVelocity.y);
        _body.linearVelocity = newVelocity;
    }

    /// <summary>
    /// Handles jump input and physics when grounded.
    /// </summary>
    private void ProcessJump()
    {
        float deltaVelocity = _body.linearVelocity.y - _lastVelocity;
        _lastVelocity = _body.linearVelocity.y;

        if (Math.Abs(deltaVelocity) < JUMP_VELOCITY_THRESHOLD && Input.GetKeyDown(KeyCode.W))
        {
            _audioSource.clip = _jumpSound;
            _audioSource.Play();
            _body.linearVelocity = new Vector2(_body.linearVelocity.x, jumpStrength);
        }
    }

    /// <summary>
    /// Manages animation states and character facing direction.
    /// </summary>
    private void UpdateAnimations()
    {
        string animationName = "Stand";
        float deltaVelocity = _body.linearVelocity.y - _lastVelocity;

        // Handle facing direction
        if (Math.Abs(_body.linearVelocity.x) > VELOCITY_THRESHOLD)
        {
            _facingDirection = _body.linearVelocity.x > 0 ? 1 : -1;

            // Play run sound and animation
            if (Math.Abs(deltaVelocity) < VELOCITY_THRESHOLD)
            {
                if (_frameCounter % RUN_SOUND_FRAME_INTERVAL == 0)
                {
                    _audioSource.clip = _runSound;
                    _audioSource.Play();
                }
                animationName = "Run";
            }
        }

        // Handle jump animation
        if (Math.Abs(deltaVelocity) >= JUMP_VELOCITY_THRESHOLD)
        {
            animationName = "Jump";
        }

        transform.localScale = new Vector3(_facingDirection, 1, 1);
        _animator.Play(animationName);
    }

    /// <summary>
    /// Records current player state for clone replay system.
    /// </summary>
    private void RecordReplayData()
    {
        _replay.positions.Add(transform.position);
        _replay.animations.Add(_animator.GetCurrentAnimatorClipInfo(0)[0].clip.name);
        _replay.facingDirections.Add(_facingDirection);
    }

    /// <summary>
    /// Checks for reset input and triggers respawn when detected.
    /// </summary>
    private void ProcessResetInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Respawn();
        }
    }
}