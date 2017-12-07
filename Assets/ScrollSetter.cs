using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollSetter : MonoBehaviour {
    public ScrollRect scrollRect;
    public float scrollScale =100.0f;

    private float pos = 1.0f; // 1.0f (top) to 0.0f (bot)

	void Start () {
        pos = 1.0f;

        KeyboardEventManager.StartListening("ScrollUp", ScrollUp);
        KeyboardEventManager.StartListening("ScrollDown", ScrollDown);
	}
	
	void Update () {
        scrollRect.verticalNormalizedPosition = pos;
	}

    void ScrollUp(int val)
    {
        pos += (val / scrollScale);
        pos = Mathf.Clamp(pos, 0.0f, 1.0f);
    }

    void ScrollDown(int val)
    {
        pos += (val / scrollScale);
        pos = Mathf.Clamp(pos, 0.0f, 1.0f);
    }
}
