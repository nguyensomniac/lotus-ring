using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardScript : MonoBehaviour {
    public KeyboardScript next;
    public Option left;
    public Option right;
    public Option top;
    public Option bottom;


    public void transitionOut()
    {
        gameObject.SetActive(false);
    }

    public void transitionIn()
    {
        gameObject.SetActive(true);
    }

    public void centerTap()
    {
        this.transitionOut();
        next.transitionIn();
        KeyboardState.Instance.ChangeActiveKeyboard(next);
    }

    public void leftActivate()
    {
        left.Expand();
    }

    public void rightActivate()
    {
        right.Expand();
    }

    public void topActivate()
    {
        top.Expand();
    }

    public void bottomActivate()
    {
        bottom.Expand();
    }

    // Use this for initialization
    void Start () {
        if (left) {
            left.SetParent(this);
        }
        if (right)
        {
            right.SetParent(this);
            Debug.Log(right.parent);
        }
        if (top)
        {
            top.SetParent(this);
        }
        if (bottom)
        {
            bottom.SetParent(this);
        }
        Debug.Log(gameObject.transform.localPosition);
        Vector3 pos = new Vector3(-24, -10.2f, 2);
        gameObject.transform.localPosition = pos;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
