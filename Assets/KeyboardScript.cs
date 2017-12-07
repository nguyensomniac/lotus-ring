using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardScript : MonoBehaviour {
    public KeyboardScript next;
    Option left;
    Option right;
    Option top;
    Option bottom;


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
        if (left)
        {
            left.Expand();
        }
    }

    public void rightActivate()
    {
        if (right)
        {
            right.Expand();
        }
    }

    public void topActivate()
    {
        if (top)
        {
            top.Expand();
        }
    }

    public void bottomActivate()
    {
        if (bottom)
        {
            bottom.Expand();
        }
    }

    // Use this for initialization
    void Start() {
        Option[] childOptions;
        childOptions = gameObject.GetComponentsInChildren<Option>();
        foreach (Option opt in childOptions)
        {
            if (opt.CompareTag("Left"))
            {
                left = opt;
                left.SetParent(this);
            }
            else if (opt.CompareTag("Right"))
            {
                right = opt;
                right.SetParent(this);
            }
            else if (opt.CompareTag("Top"))
            {
                top = opt;
                top.SetParent(this);
            }
            else if (opt.CompareTag("Bottom"))
            {
                bottom = opt;
                bottom.SetParent(this);
            }
        }
        Vector3 pos = new Vector3(-24, -10.2f, 2);
        gameObject.transform.localPosition = pos;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
