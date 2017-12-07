using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Option : MonoBehaviour {
    KeyboardScript parent;
    abstract public void Expand();
    public KeyboardScript getParent()
    {
        return parent;
    }
    public void SetParent(KeyboardScript ks)
    {
        parent = ks;
    }
}