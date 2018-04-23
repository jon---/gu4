using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class wakeupController : MonoBehaviour {
	//public

	//private
	//local const

	//system local
	int intervalCnt;	//interval count

	//component cash
	//system
	GameObject waitDisp;
	Text waitDispText;
	GameObject waitBarDisp;

	//local
	string waittxt = "PLEASE WAIT";

	void Awake() {
		QualitySettings.vSyncCount = 2; //vsync
		Application.targetFrameRate = 30;	//framerate
	}

	// Use this for initialization
	void Start () {
		//system init
		intervalCnt = 0;

		//cash
		//wait txt disp
		waitDisp = GameObject.Find ("waitDisp");
		waitDispText = waitDisp.GetComponent<Text> ();

		//wait bar disp
		waitBarDisp = GameObject.Find ("waitBarDisp");

		//disable multitouch
//		Input.multiTouchEnabled = false;

		//wait count disp
		waitDispText.text = waittxt;

		//load scene coroutine
		StartCoroutine ("loadGameScene"); 
	}
	
	// Update is called once per frame
	void Update () {
		////always process

		////interval process
		//interval count
		intervalCnt++;
		if (intervalCnt >= 2) {
			intervalCnt = 0;
			//nop
		}

	}

	//load game scene  
	IEnumerator loadGameScene(){
		int waitcnt = 0;
		int dotcnt = 0;
		string dottxt = "";

		//async load scene
		AsyncOperation loadWait = Application.LoadLevelAsync("gameScene");
		loadWait.allowSceneActivation = false;

		//load wait
		while (loadWait.progress < 0.9f) {	//load term(0.9f)?

			//wait text disp process
			if (waitcnt % 6 == 0) {
				dottxt = "";
				for (int i = 0; i < dotcnt; i++) {
					dottxt = dottxt + ".";
				}
				waitDispText.text = waittxt + dottxt;	//wait dot display
				dotcnt++;
				if (dotcnt >= 4) {
					dotcnt = 0;
				}
			}

			//wait bar disp process
			waitBarDisp.transform.localScale = new Vector3(loadWait.progress, 1.0f, 1.0f);

			//wait 10.0msec(実際はフレーム以上時間)
			yield return new WaitForSeconds (0.010f);

			//wait cnt
			waitcnt++;
		}

		//load term

		//wait bar 100% disp
		waitBarDisp.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

		//wait 1.0sec
		yield return new WaitForSeconds(1.0f);

		//change scene
		loadWait.allowSceneActivation = true;
	}

}
