using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Option : MonoBehaviour {
    public KeyboardScript parent;
    abstract public void Expand();
    public void SetParent(KeyboardScript ks)
    {
        parent = ks;
    }
}