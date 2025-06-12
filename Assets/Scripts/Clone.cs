using UnityEngine;
using System;

public class Clone : MonoBehaviour, IPlayer, IEventListener
{
    private Rigidbody2D body;
    private Animator anim;
    private ReplayData replay;
    private SpriteRenderer spriteRenderer;
    private int index;
    
    [SerializeField] private GameObject deathParticle;
    
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

    public void Die()
    {
        GameObject particle = Instantiate(deathParticle);
        particle.transform.position = transform.position;
        transform.position = new Vector3(100, 100, 100);
        index = replay.positions.Count + 1;
    }

    public void Respawn()
    {
        index = 0;
        gameObject.layer = LayerMask.NameToLayer("Clone");
    }

    public void UpdateEvent(string e)
    {
        if (e == "reset")
            this.Respawn();
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

        index++;
    }

    public void SetReplayData(ReplayData replay)
    {
        this.replay = replay;
    }
}
