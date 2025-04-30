using UnityEngine;

public class Clone : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator anim;
    private ReplayData replay;
    private int index;
    
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        index = 0;
    }

    void Update()
    {
        // null check
        if(replay == null || replay.positions == null || replay.animations == null || replay.facingDirections == null){
            return;
        }

        // update positions and animations
        if(index < replay.positions.Count){
            transform.position = replay.positions[index];
        }
        if(index < replay.animations.Count){
            anim.Play(replay.animations[index]);
        } else {
            anim.Play("Stand");
        }
        if(index < replay.facingDirections.Count){
            transform.localScale = new Vector3(replay.facingDirections[index], 1, 1);
        }

        // set body type to dynamic once replay is done so that the clone falls
        if(index >= replay.positions.Count){
            body.bodyType = RigidbodyType2D.Dynamic;
        } else {
            body.bodyType = RigidbodyType2D.Static;
        }

        // resume player collision after some time
        if(index == 60){
            gameObject.layer = LayerMask.NameToLayer("Player");
        }

        index++;
    }

    public void SetReplayData(ReplayData replay){
        this.replay = replay;
    }
}
