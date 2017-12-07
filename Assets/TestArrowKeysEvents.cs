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
            KeyboardEventManager.TriggerEvent("LeftSwipe", 0);
        }

        if (Input.GetKeyDown("w"))
        {
            KeyboardEventManager.TriggerEvent("TopSwipe", 0);
        }

        if (Input.GetKeyDown("s"))
        {
            KeyboardEventManager.TriggerEvent("BottomSwipe", 0);
        }

        if (Input.GetKeyDown("d"))
        {
            KeyboardEventManager.TriggerEvent("RightSwipe", 0);
        }

        if (Input.GetKeyDown("e"))
        {
            KeyboardEventManager.TriggerEvent("CenterTap", 0);
        }
        if (Input.GetKeyDown("x"))
        {
            KeyboardEventManager.TriggerEvent("ScrollUp", 10);
        }
        if (Input.GetKeyDown("z"))
        {
            KeyboardEventManager.TriggerEvent("ScrollDown", -10);
        }
    }
}
