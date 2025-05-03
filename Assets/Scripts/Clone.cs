using UnityEngine;
using System;

public class Clone : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator anim;
    private ReplayData replay;
    private SpriteRenderer spriteRenderer;
    private int index;
    
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        index = 0;
        Color[] presetColors = new Color[]
        {
            Color.red,
            Color.green,
            Color.blue,
            Color.yellow,
            Color.cyan,
            Color.magenta
        };
        int i = UnityEngine.Random.Range(0, presetColors.Length);
        spriteRenderer.color = presetColors[i];
    }

    public void Respawn()
    {
        index = 0;
        gameObject.layer = LayerMask.NameToLayer("Clone");
    }

    void Update()
    {
        // null check
        if(replay == null || replay.positions == null || replay.animations == null || replay.facingDirections == null)
        {
            return;
        }

        // update positions and animations
        if(index < replay.positions.Count)
        {
            transform.position = replay.positions[index];
        }
        if(index < replay.animations.Count)
        {
            anim.Play(replay.animations[index]);
        } 
        else 
        {
            anim.Play("Stand");
        }
        if(index < replay.facingDirections.Count){
            transform.localScale = new Vector3(replay.facingDirections[index], 1, 1);
        }

        // resume player collision after some time
        if(index == Math.Max(0, replay.positions.Count - 60))
        {
            gameObject.layer = LayerMask.NameToLayer("Player");
        }

        index++;
    }

    public void SetReplayData(ReplayData replay)
    {
        this.replay = replay;
    }
}
