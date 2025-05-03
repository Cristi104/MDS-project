using UnityEngine;
using System;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator anim;
    private int facingDirection;
    private float lastVelocity;
    private Vector3 startPosition;
    private List<Clone> clones;
    public ReplayData replay;

    [SerializeField] private float speed = 3;
    [SerializeField] private int maxClones = 1;
    [SerializeField] private float jumpStrength = 5;
    [SerializeField] private Clone cloneTemplate;

    void Start()
    {
        startPosition = transform.position;
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        replay = new ReplayData();
        clones = new List<Clone>();
        facingDirection = 1;
        lastVelocity = 0;
    }

    void Update()
    {
        Vector2 newVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.linearVelocity.y);
        float deltaVelocity = (body.linearVelocity.y - lastVelocity);
        string animationName = "Stand";

        // direction check to flip animations horizontaly
        if(Math.Abs(body.linearVelocity.x) > 0.01)
        {
            if(body.linearVelocity.x < -0.01)
            {
                facingDirection = -1;
            }

            if(body.linearVelocity.x > 0.01)
            {
                facingDirection = 1;
            }

            if(Math.Abs(deltaVelocity) < 0.01)
            {
                animationName = "Run";
            }
        }

        // jumping and jump animation
        if(Math.Abs(deltaVelocity) < 0.001)
        {
            if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
            {
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
        if(Input.GetKeyDown(KeyCode.E))
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
    }
}
