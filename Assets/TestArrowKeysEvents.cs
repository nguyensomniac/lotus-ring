using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestArrowKeysEvents : MonoBehaviour {
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            KeyboardEventManager.TriggerEvent("LeftSwipe");
        }

        if (Input.GetKeyDown("w"))
        {
            KeyboardEventManager.TriggerEvent("TopSwipe");
        }

        if (Input.GetKeyDown("s"))
        {
            KeyboardEventManager.TriggerEvent("BottomSwipe");
        }

        if (Input.GetKeyDown("d"))
        {
            KeyboardEventManager.TriggerEvent("RightSwipe");
        }
    }
}
