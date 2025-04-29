using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    private Rigidbody2D body;
    private float lastVelocity;
    public Animator anim;

    [SerializeField] private float speed;
    [SerializeField] private float jumpStrength;

    void Start(){
        body = GetComponent<Rigidbody2D>();
        lastVelocity = 0;
        anim = GetComponent<Animator>();
    }

    void Update(){
        Vector2 newVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.linearVelocity.y);
        float deltaVelocity = (body.linearVelocity.y - lastVelocity);
        string animationName = "Stand";

        if(Math.Abs(body.linearVelocity.x) > 0.01){
            if(body.linearVelocity.x < -0.01){
                transform.localScale = new Vector3(-1, 1, 1);
            }
            if(body.linearVelocity.x > 0.01){
                transform.localScale = new Vector3(1, 1, 1);
            }
            if(Math.Abs(deltaVelocity) < 0.01){
                animationName = "Run";
            }
        }

        if(Math.Abs(deltaVelocity) < 0.01){
            if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)){
                newVelocity.y = jumpStrength;
            }
        } else {
            animationName = "Jump";
        }

        anim.Play(animationName);

        body.linearVelocity = newVelocity;

    }
}
