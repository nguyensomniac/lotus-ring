using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardScript : MonoBehaviour {



	// Use this for initialization
	void Start () {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(0, 0, 5);
        cube.transform.rotation = Quaternion.Euler(0, 45, 0);
        cube.GetComponent<Renderer>().material.color = new Color(1F, 0F, 0F, 0.5F);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
