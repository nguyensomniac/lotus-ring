﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackOption : Option
{

    override public void Expand()
    {
        KeyboardState.Instance.ActivatePreviousKeyboard();
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
