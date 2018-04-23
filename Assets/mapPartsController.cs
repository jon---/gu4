using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapPartsController : MonoBehaviour {

	////public

	////private
	//const
	//map draw
	const float xoffset = -3.5f;	//map draw offset x
	const float yoffset = -6.0f;	//map draw offset y
	const int map_xsize = 8;	//map draw size x
	const int map_ysize = 12;	//map draw size y

	//cash
	Transform cashTransform=null;
	SpriteRenderer sr=null;
	GameObject mainCtr;	//main controller
	mainController mc;
	GameObject mpCtr;	//map controller
	mapController mpc;

	//system
	private int intervalCnt;	//interval counter

	//local data
	//pos x,y
	float posx;
	float posy;

	//sprite
	Sprite spr=null;

	//x index
	int xindex;

	//scroll speed
	float speed_y;

	//scroll x move
	float mpxmov;


	// Use this for initialization
	void Start () {
		//cash
		//transform cash
		cashTransform = transform;
		//sprite renderer
		sr = GetComponent<SpriteRenderer>();
		//maincontroller
		mainCtr = GameObject.Find ("mainController");
		mc = mainCtr.GetComponent<mainController> ();
		//map controller
		mpCtr = GameObject.Find("mapController");
		mpc = mpCtr.GetComponent<mapController> ();

		//pos x,y (set from parent objects)
		cashTransform.position = new Vector3( posx, posy, 0.0f );

		//sprite init
		sr.sprite = spr;

		//system init
		intervalCnt = 0;

		//scroll speed
		speed_y = 0.0f;

		//scroll x move
		mpxmov=0;

		//objnum inc
		mc.incObj();
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

			//current scroll speed
			speed_y = mpc.getScrSpd();

			//scroll x move
			mpxmov = mpc.getMapxMov ();

			//move
			cashTransform.Translate (mpxmov, (speed_y * -1), 0);

			//new line generate (sprite change)
			if ( (cashTransform.position.y <= yoffset) && (speed_y>=0)) {
				cashTransform.Translate (0, map_ysize, 0);
				sr.sprite = mpc.getMapNextData (xindex);
			}else if ( (cashTransform.position.y >= (yoffset*-1)) && (speed_y<0)) {
				cashTransform.Translate (0, map_ysize*-1, 0);
				sr.sprite = mpc.getMapNextData (xindex);
			}
		}
	}

	////public

	//set init status
	public void setInitStatus( float posx, float posy, int xindex, Sprite spr ){
		this.posx = posx;
		this.posy = posy;
		this.xindex = xindex;
		this.spr = spr;
	}

	//x reset (for scroll x reset center )
	public void resetMapx( float x ){
		Vector2 pos = cashTransform.position;
		pos.x = x;
		cashTransform.position = pos;
	}

	//set pos
	public void initPos(){
		if (cashTransform != null) {
			cashTransform.position = new Vector3 (posx, posy, 0.0f);
		} else {
			transform.position = new Vector3 (posx, posy, 0.0f);
		}
	}

	//set sprite
	public void setSprite( Sprite spr ){
		this.spr = spr;
		if (sr != null) {
			sr.sprite = spr;
		} else {
			GetComponent<SpriteRenderer> ().sprite = spr;
		}
	}

}
