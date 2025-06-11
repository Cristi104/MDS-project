using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }
    private List<MonoBehaviour> listeners;

    void Awake()
    {
        listeners = new List<MonoBehaviour>();
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void Subscribe(MonoBehaviour listener)
    {
        listeners.Add(listener);
    }

    public void Unsubscribe(MonoBehaviour listener)
    {
        listeners.Remove(listener);
    }

    public void Notify(string e){
        foreach (IEventListener listener in listeners.OfType<IEventListener>())
        {
            listener.UpdateEvent(e);
        }
    }
}