using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

// Override the abstract UnityEvent<T>
public class CommandEvent : UnityEvent<int>
{

}

public class KeyboardEventManager : MonoBehaviour
{

    private Dictionary<string, UnityEvent<int>> eventDictionary;

    private static KeyboardEventManager eventManager;

    public static KeyboardEventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(KeyboardEventManager)) as KeyboardEventManager;

                if (!eventManager)
                {
                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                }
                else
                {
                    eventManager.Init();
                }
            }

            return eventManager;
        }
    }

    void Init()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, UnityEvent<int>>();
        }
    }

    // In any code: EventManager.StartListening("party", CALLBACK_NAME);
    public static void StartListening(string eventName, UnityAction<int> listener)
    {
        UnityEvent<int> thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new CommandEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    // In any code: EventManager.StartListening("party", EXISTING_CALLBACK_NAME);
    public static void StopListening(string eventName, UnityAction<int> listener)
    {
        if (eventManager == null) return;
        UnityEvent<int> thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName, int value)
    {
        UnityEvent<int> thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(value);
        }
    }

    //void Update()
    //{
    //    EventManager.StartListening("TopSwipe", Callme);
    //}
    //void Callme(int val)
    //{
    //    Debug.Log(val);
    //}
}