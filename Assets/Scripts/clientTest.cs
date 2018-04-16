using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clientTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.C))
        {
            gameObject.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
        }
	}
}
