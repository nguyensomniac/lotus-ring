using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackspaceOption : Option
{
    public Material normalMat;
    public Material hoverMat;

    override public void Expand()
    {
        KeyboardState.Instance.Backspace();
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}
