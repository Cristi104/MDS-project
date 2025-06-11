using UnityEngine;

/// <summary>
/// Represents a player clone that replays recorded movements and animations.
/// </summary>
public class Clone : MonoBehaviour, IPlayer
{
    private Rigidbody2D body;
    private Animator anim;
    private ReplayData replay;
    private SpriteRenderer spriteRenderer;
    private int index;

    private static readonly Color[] PresetColors = new Color[]
    {
        Color.red,
        Color.green,
        Color.blue,
        Color.yellow,
        Color.cyan,
        Color.magenta
    };

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        index = 0;

        int randomIndex = UnityEngine.Random.Range(0, PresetColors.Length);
        spriteRenderer.color = PresetColors[randomIndex];
    }

    /// <summary>
    /// Handles the clone's death by moving it off-screen and stopping replay.
    /// </summary>
    public void Die()
    {
        transform.position = new Vector3(100, 100, 100);
        index = replay.Positions.Count + 1;
    }

    /// <summary>
    /// Resets the clone to begin replay from the start.
    /// </summary>
    public void Respawn()
    {
        index = 0;
        gameObject.layer = LayerMask.NameToLayer("Clone");
    }

    private void Update()
    {
        if (replay == null || replay.Positions == null ||
            replay.Animations == null || replay.FacingDirections == null)
        {
            return;
        }

        if (index < replay.Positions.Count)
        {
            transform.position = replay.Positions[index];
        }

        if (index < replay.Animations.Count)
        {
            anim.Play(replay.Animations[index]);
        }
        else
        {
            anim.Play("Stand");
        }

        if (index < replay.FacingDirections.Count)
        {
            transform.localScale = new Vector3(replay.FacingDirections[index], 1, 1);
        }

        index++;
    }

    /// <summary>
    /// Sets the replay data for this clone to follow.
    /// </summary>
    /// <param name="replay">The replay data containing positions, animations, and directions.</param>
    public void SetReplayData(ReplayData replay)
    {
        this.replay = replay;
    }
}