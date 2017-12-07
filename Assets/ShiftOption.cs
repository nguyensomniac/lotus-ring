using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftOption : Option
{

    override public void Expand()
    {
        KeyboardState.Instance.toggleShift();
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
