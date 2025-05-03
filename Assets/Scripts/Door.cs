using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private Coroutine currentMoveCoroutine;

    [SerializeField] private float secondsToOpen = 1f;
    [SerializeField] private Vector3 openOffset = Vector3.zero;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       startPosition = transform.position;
       targetPosition = transform.position + openOffset;
    }
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

        transform.position = target; // Snap to final position
    }

    public void Open()
    {
        if (currentMoveCoroutine != null)
        {
            StopCoroutine(currentMoveCoroutine);
            currentMoveCoroutine = null;
        }
        currentMoveCoroutine = StartCoroutine(MoveToPosition(targetPosition));
    }

    public void Close()
    {
        if (currentMoveCoroutine != null)
        {
            StopCoroutine(currentMoveCoroutine);
            currentMoveCoroutine = null;
        }
        currentMoveCoroutine = StartCoroutine(MoveToPosition(startPosition));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
