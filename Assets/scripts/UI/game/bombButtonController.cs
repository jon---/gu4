using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class bombButtonController : MonoBehaviour {
	//public

	//private
	//component cash
	GameObject playerCtr;
	playerController plc;

	// Use this for initialization
	void Start () {
		//cash
		//playercontroller
		playerCtr = GameObject.Find ("playerController");
		plc = playerCtr.GetComponent<playerController> ();

	}
	
	float cnt = 0.0f;	//time scale cnt
	// Update is called once per frame
	void Update () {
		//wait and pause
		cnt = cnt + Time.timeScale;
		if (cnt < 1.0f) {
			return;
		} else {
			cnt = cnt - 1.0f;
		}
		//nop		
	}

	//bomb button down
	public void OnBombButtonDown(){
		//bomb button process
		plc.tapBombButton ();
	}

}
