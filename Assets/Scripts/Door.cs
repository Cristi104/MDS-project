using UnityEngine;
using System.Collections;

public class Door : Activateable
{
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private Coroutine currentMoveCoroutine;

    [SerializeField] private float secondsToOpen = 1f;
    [SerializeField] private Vector3 openOffset = Vector3.zero;

    void Start()
    {
       startPosition = transform.position;
       targetPosition = transform.position + openOffset;
    }

    // interpolate to target position over secondsToOpen seconds
    private IEnumerator MoveToPosition(Vector3 target)
    {
        Vector3 start = transform.position;
        float elapsed = 0;

        while (elapsed < secondsToOpen)
        {
            transform.position = Vector3.Lerp(start, target, elapsed / secondsToOpen);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = target;
    }

    // move from current position to targetPosition
    public override void Activate()
    {
        if (currentMoveCoroutine != null)
        {
            StopCoroutine(currentMoveCoroutine);
            currentMoveCoroutine = null;
        }
        currentMoveCoroutine = StartCoroutine(MoveToPosition(targetPosition));
    }

    // move from current position to startPosition
    public override void Deactivate()
    {
        if (currentMoveCoroutine != null)
        {
            StopCoroutine(currentMoveCoroutine);
            currentMoveCoroutine = null;
        }
        currentMoveCoroutine = StartCoroutine(MoveToPosition(startPosition));
    }

    void Update()
    {
        
    }
}
