using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextOption : MonoBehaviour {

    public string label;

	// Use this for initialization
	void Start () {
        Debug.Log(label);
        Transform t = transform.Find("UITextPrefab").Find("Text");
        UnityEngine.UI.Text text = t.gameObject.GetComponent<UnityEngine.UI.Text>();
        if (text)
        {
            text.text = label;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
