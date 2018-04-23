using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseButtonController : MonoBehaviour {
	//public

	//private
	//component cash
	GameObject mainCtr;
	mainController mc;

	// Use this for initialization
	void Start () {
		//cash
		//maincontroller
		mainCtr = GameObject.Find ("mainController");
		mc = mainCtr.GetComponent<mainController> ();

	}

	// Update is called once per frame
	void Update () {
		//nop		
	}

	//pause button down
	public void OnPauseButtonDown(){
		//pause button process
		mc.tapPauseButton ();
	}

}
