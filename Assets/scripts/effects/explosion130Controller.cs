using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion130Controller : MonoBehaviour {
	//public

	//private
	//local const
	//x,y min/max
	const float xmin = -4.0f;
	const float xmax = 4.0f;
	const float ymin = -6.0f;
	const float ymax = 6.0f;
	//x,y speed base
	const float xspd = 0.1f;
	const float yspd = 0.1f;

	//system local
	int intervalCnt;	//interval counter

	//system cash

	//component cash
	Transform cashTransform;
	GameObject mainCtr;
	mainController mc;
	Animator animt;

	//pos x,y
	float posx;
	float posy;

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

		//animator
		animt = GetComponent<Animator>();
		animt.speed = 0.8f;

		//pos x,y
		cashTransform.position = new Vector3( posx, posy, 0.0f );

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
			cashTransform.Translate ((mc.getMapxMov()), (mc.getScrollSpeed()*-1.0f), 0);

			//move result process
			if ( (cashTransform.position.y > ymax) ||
				(cashTransform.position.y < ymin) ||
				(cashTransform.position.x < xmin) ||
				(cashTransform.position.x > xmax) ){
				//objnum dec
				if (incobj == true) {
					mc.decObj ();
					incobj = false;
				} else {
					#if UNITY_EDITOR
					Debug.Log ("no inc dec explosion130");
					#endif
				}
				//delete this object
				Destroy (gameObject);
			}
		}	
	}

	//public

	//set init status
	public void setInitStatus( float px, float py ){
		this.posx = px;
		this.posy = py;
	}

}
