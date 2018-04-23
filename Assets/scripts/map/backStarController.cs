using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backStarController : MonoBehaviour {

	////public

	//private
	//local const
	//x,y min/max
	const float xmin = -2.81f;
	const float xmax = 2.81f;
	const float ymin = -5.5f;
	const float ymax = 5.5f;
	//x,y speed base
	const float xspd = 0.95f;
	const float yspd = 0.95f;

	//cash
	Transform cashTransform;
	SpriteRenderer sr;
//	GameObject mainCtr;	//main controller
//	mainController mc;
	GameObject mpCtr;	//map controller
	mapController mpc;

	//system
	private int intervalCnt;	//interval counter

	//local data
	//pos x,y
	float posx;
	float posy;

	//scale
	float scl;
	float spdscl;

	//back color
	Color backcr;

	//scroll speed
	float speed_y;

	//scroll x move
	float mpxmov;

	//blink
	bool blink;
	bool blalpha;
	float blback;
	bool blinc;


	// Use this for initialization
	void Start () {
		//cash
		//transform cash
		cashTransform = transform;
		//sprite renderer
		sr = GetComponent<SpriteRenderer>();
		//maincontroller
//		mainCtr = GameObject.Find ("mainController");
//		mc = mainCtr.GetComponent<mainController> ();
		//map controller
		mpCtr = GameObject.Find("mapController");
		mpc = mpCtr.GetComponent<mapController> ();

		//scale
		scl = 0.0f;
		spdscl = 0.0f;

		//back color
		backcr = sr.color;

		//system init
		intervalCnt = 0;

		//scroll speed
		speed_y = 0.0f;

		//scroll x move
		mpxmov=0;

		//blink
		blink = false;
		blalpha = false;
		blback = 0.0f;
		blinc = false;

		//set new status
		this.setNewStatus();

//		//objnum inc
//		mc.incObj();
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

		//////always process

		//nop

		//////interval process
		intervalCnt++;
		if (intervalCnt >= 1) {
			intervalCnt = 0;

			//blink
			this.blinkProcess();

			//current scroll speed
			speed_y = mpc.getScrSpd()*yspd * spdscl;

			//scroll x move
			mpxmov = mpc.getMapxMov ()*xspd * (spdscl/1.8f);

			//move
			cashTransform.Translate (mpxmov, (speed_y * -1), 0);

			//new line generate (sprite change)
			if ( (cashTransform.position.y <= ymin) && (speed_y >= 0) ) {
				this.setNextStatus ();
			} else if ( (cashTransform.position.y >= ymax) && (speed_y < 0) ) {
				this.setNextStatus ();
			} 
		}
	}

	//private

	//set new status
	private void setNewStatus(){
		//new pos
		posx = Random.Range (xmin, xmax);
		posy = Random.Range (ymin, ymax);
		//status common
		this.setStatus ();
	}

	//set next status
	private void setNextStatus(){
		//new pos
		posx = Random.Range (xmin, xmax);
		if (speed_y >= 0) {
			posy = ymax;
		} else {
			posy = ymin;
		}
		//status common
		this.setStatus ();
	}

	//set status
	private void setStatus(){
		//set pos
		cashTransform.position = new Vector3( posx, posy, 0.0f );
		//set scale
		scl = Random.Range (0.2f, 0.5f);	//for size,scroll speed,xmov
		cashTransform.localScale = new Vector3 ((scl*1.25f), scl, 1.0f);
		//alpha
		Color cr;
		cr.r = backcr.r - (5.0f/255.0f * (0.8f-scl));
		cr.g = backcr.g - (5.0f/255.0f * (0.8f-scl));
		cr.b = backcr.b - (5.0f/255.0f * (0.8f-scl));
		cr.a = backcr.a - (20.0f/255.0f * (0.8f-scl));
		sr.color = cr;
		//speed scale
		spdscl = 2.6f * scl;
		//blink
		int rr = Random.Range (0, 6);
		if (rr == 0) {
			blink = true;
			blalpha = true;
			blback = cr.a;
		} else if (rr == 1) {
			blink = true;
			blalpha = false;
			blback = cr.r;
		} else {
			blink = false;
		}
		blinc = false;
	}

	//blink process
	private void blinkProcess(){
		if (blink == true) {
			Color cr = sr.color;
			if (blalpha == true) {
				if (blinc == true) {
					cr.a = cr.a + 0.05f;
					if (cr.a >= (blback)) {
						cr.a = blback;
						blinc = false;
					}
				} else {
					cr.a = cr.a - 0.05f;
					if (cr.a <= (blback - 0.5f)) {
						cr.a = (blback - 0.5f);
						blinc = true;
					}
				}
			} else {
				if (blinc == true) {
					cr.r = cr.r + 0.05f;
					if (cr.r >= (blback)) {
						cr.r = blback;
						blinc = false;
					}
				} else {
					cr.r = cr.r - 0.05f;
					if (cr.r <= (blback - 0.5f)) {
						cr.r = (blback - 0.5f);
						blinc = true;
					}
				}
			}
			sr.color = cr;
		}
	}

	////public

	//set init status
	public void setInitStatus(){
	}

	//x reset (for scroll x reset center )
	public void resetMapx( float x ){
		Vector2 pos = cashTransform.position;
		pos.x = posx;
		cashTransform.position = pos;
	}

}
