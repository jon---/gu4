using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wipe1Controller : MonoBehaviour {
	//(wipe2共通)

	//public

	//private
	//local const

	//system local
	int intervalCnt;	//interval counter

	//system cash

	//component cash
	GameObject mainCtr;
	mainController mc;

	//local
	//exist cnt
	int existCnt;

	//objinc
	bool incobj = false;


	// Use this for initialization
	void Start () {
		//system init
		intervalCnt = 0;

		//cash
		//maincontroller
		mainCtr = GameObject.Find ("mainController");
		mc = mainCtr.GetComponent<mainController> ();

		//exist cnt
		existCnt = 5;

		//objnum inc
		mc.incObj();
		incobj = true;
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

		////always process

		//exist cnt
		existCnt--;
		if (existCnt <= 0) {
			existCnt = 0;

			//delete this object
			if (incobj == true) {
				mc.decObj ();
			} else {
				#if UNITY_EDITOR
				Debug.Log ("no inc dec wipe1");
				#endif
			}
			//delete this
			Destroy (gameObject);

		}


		////interval process
		//interval count
		intervalCnt++;
		if (intervalCnt >= 1) {
			intervalCnt = 0;
			//nop
		}	
	}

	//public

}
