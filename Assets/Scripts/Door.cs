using UnityEngine;
using System.Collections;

public class Door : Activateable
{
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private Coroutine currentMoveCoroutine;

    [SerializeField]
    private float secondsToOpen = 1f;

    [SerializeField]
    private Vector3 openOffset = Vector3.zero;

    private void Start()
    {
        startPosition = transform.position;
        targetPosition = transform.position + openOffset;
    }

    /// <summary>
    /// Smoothly moves door to target position over time
    /// </summary>
    private IEnumerator MoveToPosition(Vector3 target)
    {
        Vector3 start = transform.position;
        float elapsed = 0f;

        while (elapsed < secondsToOpen)
        {
            transform.position = Vector3.Lerp(start, target, elapsed / secondsToOpen);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = target;
    }

    public override void Activate()
    {
        if (currentMoveCoroutine != null)
        {
            StopCoroutine(currentMoveCoroutine);
        }
        currentMoveCoroutine = StartCoroutine(MoveToPosition(targetPosition));
    }

    public override void Deactivate()
    {
        if (currentMoveCoroutine != null)
        {
            StopCoroutine(currentMoveCoroutine);
        }
        currentMoveCoroutine = StartCoroutine(MoveToPosition(startPosition));
    }

    private void Update()
    {
        // Intentionally left empty
    }
}