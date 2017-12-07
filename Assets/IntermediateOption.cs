using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntermediateOption : Option
{
    public string label;
    public Material normalMat;
    public Material hoverMat;
    public KeyboardScript child;

    public void OnActive()
    {
        GameObject bg = transform.Find("Cylinder").gameObject;
        var lerp = Mathf.PingPong(Time.time, 2.0f) / 2.0f;
        Renderer r = bg.GetComponent<Renderer>();
        r.material.Lerp(normalMat, hoverMat, lerp);
    }

    override public void Expand()
    {
        KeyboardState.Instance.AddToHistory(getParent());
        KeyboardState.Instance.ChangeActiveKeyboard(child);
    }

    void SetText(string txt)
    {
        Transform t = transform.Find("UITextPrefab").Find("Text");
        UnityEngine.UI.Text text = t.gameObject.GetComponent<UnityEngine.UI.Text>();
        if (text)
        {
            text.text = txt;
        }
    }

    // Use this for initialization
    void Start()
    {
        SetText(label);
    }

    // Update is called once per frame
    void Update()
    {
        if (KeyboardState.Instance.getIsCaps())
        {
            SetText(label.ToUpper());
        }
        else
        {
            SetText(label);
        }
    }
}
