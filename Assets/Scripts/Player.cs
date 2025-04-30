using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator anim;
    private int facingDirection;
    private float lastVelocity;
    public ReplayData replay;
    private GameObject clone;

    [SerializeField] private float speed;
    [SerializeField] private float jumpStrength;
    [SerializeField] private GameObject cloneTemplate;

    void Start(){
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        replay = new ReplayData();
        facingDirection = 1;
        lastVelocity = 0;
    }

    void Update(){
        Vector2 newVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.linearVelocity.y);
        float deltaVelocity = (body.linearVelocity.y - lastVelocity);
        string animationName = "Stand";

        // direction check to flip animations horizontaly
        if(Math.Abs(body.linearVelocity.x) > 0.01){
            if(body.linearVelocity.x < -0.01){
                facingDirection = -1;
            }

            if(body.linearVelocity.x > 0.01){
                facingDirection = 1;
            }

            if(Math.Abs(deltaVelocity) < 0.01){
                animationName = "Run";
            }
        }

        // jumping and jump animation
        if(Math.Abs(deltaVelocity) < 0.01){
            if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)){
                newVelocity.y = jumpStrength;
            }
        } else {
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
        if(Input.GetKeyDown(KeyCode.E)){
            if(clone != null){
                Destroy(clone);
            }
            clone = Instantiate(cloneTemplate);
            Clone cloneScript = clone.GetComponent<Clone>();
            cloneScript.SetReplayData(replay);
            replay = new ReplayData();
            
            // reset
            transform.position = new Vector3(0, 0, 0);
            body.linearVelocity = new Vector2(0, 0);
        }
    }
}
