using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class track100Controller : MonoBehaviour {
	//public
	public float speed_y;	//スクロール速度

	//private
	//local const
	//x,y min/max
	const float xmin = -4.0f;
	const float xmax = 4.0f;
	const float ymin = -6.0f;
	const float ymax = 6.0f;
	//x,y speed base
	const float xspd = 0.0f;
	const float yspd = 0.05f;

	//system local
	int intervalCnt;	//interval counter

	//system cash

	//component cash
	Transform cashTransform;
	GameObject mainCtr;
	mainController mc;

	//local
	//(set from parent objects)
	float xpos;
	float ypos;

	//objinc
	bool incobj = false;


	// Use this for initialization
	void Start () {
		//system init
		intervalCnt = 0;

		//cash
		//transform cash
		cashTransform = transform;

		//maincontroller
		mainCtr = GameObject.Find ("mainController");
		mc = mainCtr.GetComponent<mainController> ();

		//x,y pos
		cashTransform.position = new Vector3 ( xpos, ypos, 0);

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

		////interval process
		//interval count
		intervalCnt++;
		if (intervalCnt >= 1) {
			intervalCnt = 0;
			//move
			cashTransform.Translate (0, (mc.getScrollSpeed()*-1), 0);
			//move result process
			if ( (cashTransform.position.y > ymax) ||
				(cashTransform.position.y < ymin) ||
				(cashTransform.position.x < xmin) ||
				(cashTransform.position.x > xmax) ){
				//delete this object
				if (incobj == true) {
					mc.decObj ();
				} else {
					#if UNITY_EDITOR
					Debug.Log ("no inc dec track100");
					#endif
				}
				//delete this
				Destroy (gameObject);
			}
		}	
	}

	//public

	//set init
	public void setinitStatus( float x, float y ){
		this.xpos = x;
		this.ypos = y;
	}

}
