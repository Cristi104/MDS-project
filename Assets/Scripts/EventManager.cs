using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Manages event notification between game objects.
/// </summary>
/// <remarks>
/// Implements the singleton pattern to provide global access to event management.
/// Allows objects to subscribe/unsubscribe and receive event notifications.
/// </remarks>
public class EventManager : MonoBehaviour
{
    /// <summary>
    /// Singleton instance of the EventManager.
    /// </summary>
    public static EventManager Instance { get; private set; }

    private List<MonoBehaviour> _listeners = new List<MonoBehaviour>();

    private void Awake()
    {
        InitializeSingleton();
    }

    /// <summary>
    /// Initializes the singleton instance.
    /// </summary>
    private void InitializeSingleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    /// <summary>
    /// Subscribe, Unsubscribe, and Notify use observer pattern to manage event listeners. 
    /// </summary>>

    /// <summary>
    /// Subscribes a listener to receive events.
    /// </summary>>
    public void Subscribe(MonoBehaviour listener)
    {
        if (listener != null && !_listeners.Contains(listener))
        {
            _listeners.Add(listener);
        }
    }

    /// <summary>
    /// Unsubscribes a listener from receiving events.
    /// </summary>
    public void Unsubscribe(MonoBehaviour listener)
    {
        if (listener != null)
        {
            _listeners.Remove(listener);
        }
    }

    /// <summary>
    /// Notifies all subscribed IEventListener components of an event.
    /// </summary>
    public void Notify(string eventName)
    {
        foreach (IEventListener listener in _listeners.OfType<IEventListener>())
        {
            listener.UpdateEvent(eventName);
        }
    }
}