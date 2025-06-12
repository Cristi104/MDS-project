using UnityEngine;
using System.Collections;

/// <summary>
/// Represents a door that can be opened and closed through activation.
/// </summary>
/// <remarks>
/// This class handles door movement between open and closed positions,
/// responds to reset events, and manages coroutine-based smooth movement.
/// </remarks>
public class Door : Activateable, IEventListener
{
    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private Coroutine _currentMoveCoroutine;

    [SerializeField]
    private float secondsToOpen = 1f;

    [SerializeField]
    private Vector3 openOffset = Vector3.zero;

    private void Start()
    {
        _startPosition = transform.position;
        _targetPosition = transform.position + openOffset;
    }

    private void Awake()
    {
        EventManager.Instance.Subscribe(this);
    }

    /// <summary>
    /// Handles incoming events.
    /// </summary>
    public void UpdateEvent(string eventName)
    {
        if (eventName == "reset")
        {
            if (_currentMoveCoroutine != null)
            {
                StopCoroutine(_currentMoveCoroutine);
                _currentMoveCoroutine = null;
                transform.position = _startPosition;
            }
        }
    }

    /// <summary>
    /// Smoothly moves the door to the target position over time.
    /// </summary>
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

    /// <summary>
    /// Opens the door by moving it to the target position.
    /// </summary>
    public override void Activate()
    {
        if (_currentMoveCoroutine != null)
        {
            StopCoroutine(_currentMoveCoroutine);
            _currentMoveCoroutine = null;
        }
        _currentMoveCoroutine = StartCoroutine(MoveToPosition(_targetPosition));
    }

    /// <summary>
    /// Closes the door by moving it back to the start position.
    /// </summary>
    public override void Deactivate()
    {
        if (_currentMoveCoroutine != null)
        {
            StopCoroutine(_currentMoveCoroutine);
            _currentMoveCoroutine = null;
        }
        _currentMoveCoroutine = StartCoroutine(MoveToPosition(_startPosition));
    }

    private void Update()
    {
        
    }
}