using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterOption : Option
{

    override public void Expand()
    {
        KeyboardState.Instance.ResetSearch();
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
