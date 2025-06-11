using UnityEngine;
using System;
using System.Collections.Generic;

public class Player : MonoBehaviour, IPlayer
{
    private Rigidbody2D body;
    private Animator anim;
    private int facingDirection;
    private float lastVelocity;
    private Vector3 startPosition;
    private List<Clone> clones;
    public ReplayData replay;
    private AudioSource audioSource;
    private string jumpSoundFile = "Free UI Click Sound Effects Pack/AUDIO/Pop/SFX_UI_Click_Organic_Pop_Liquid_Thick_Generic_1";
    private string runSoundFile = "Free UI Click Sound Effects Pack/AUDIO/Sci-Fi/SFX_UI_Click_Designed_Scifi_Thin_Negative_Back_1";
    private AudioClip jumpSound;
    private AudioClip runSound;
    private int frameCounter = 0;

    [SerializeField] private float speed = 3;
    [SerializeField] private int maxClones = 1;
    [SerializeField] private float jumpStrength = 5;
    [SerializeField] private Clone cloneTemplate;

    void Start()
    {
        jumpSound = Resources.Load<AudioClip>(jumpSoundFile);
        runSound = Resources.Load<AudioClip>(runSoundFile);
        audioSource = GetComponent<AudioSource>();
        startPosition = transform.position;
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        replay = new ReplayData();
        clones = new List<Clone>();
        facingDirection = 1;
        lastVelocity = 0;
    }

    public void Respawn()
    {
        replay = new ReplayData();
        transform.position = startPosition;
        body.linearVelocity = new Vector2(0, 0);
    }

    public void Die()
    {
        if (clones.Count < maxClones)
        {
            Clone clone = Instantiate(cloneTemplate);
            clone.SetReplayData(replay);
            clones.Add(clone);
        }
        else
        {
            clones[0].SetReplayData(replay);
            clones[0].Respawn();
            clones.Add(clones[0]);
            clones.RemoveAt(0);
        }
        foreach (Clone clone1 in clones)
            clone1.Respawn();

        // reset
        replay = new ReplayData();
        transform.position = startPosition;
        body.linearVelocity = new Vector2(0, 0);
    }

    void Update()
    {
        frameCounter++;
        Vector2 newVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.linearVelocity.y);
        float deltaVelocity = (body.linearVelocity.y - lastVelocity);
        string animationName = "Stand";

        // direction check to flip animations horizontaly
        if (Math.Abs(body.linearVelocity.x) > 0.01)
        {
            if (body.linearVelocity.x < -0.01)
            {
                facingDirection = -1;
            }

            if (body.linearVelocity.x > 0.01)
            {
                facingDirection = 1;
            }

            if (Math.Abs(deltaVelocity) < 0.01)
            {
                if (frameCounter % 6 == 0)
                {
                    audioSource.clip = runSound;
                    audioSource.Play();
                }
                animationName = "Run";
            }
        }

        // jumping and jump animation
        if(Math.Abs(deltaVelocity) < 0.001)
        {
            if(Input.GetKeyDown(KeyCode.W))
            {
                audioSource.clip = jumpSound;
                audioSource.Play();
                newVelocity.y = jumpStrength;
            }
        } 
        else 
        {
            animationName = "Jump";
        }

        // update player
        transform.localScale = new Vector3(facingDirection, 1, 1);
        anim.Play(animationName);
        body.linearVelocity = newVelocity;

        // store data for clone replay 
        replay.positions.Add(transform.position);
        replay.animations.Add(animationName);
        replay.facingDirections.Add(facingDirection);
        
        // reset and spawn/respawn clone
        if(Input.GetKeyDown(KeyCode.R))
        {
            Die();
        }
    }
}
