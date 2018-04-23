using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class continueButtonController : MonoBehaviour {
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

	//continue button
	public void OnContinueButton(){
		//continue button process
		mc.tapContinueButton ();
	}

}
