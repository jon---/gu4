using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy140Controller : MonoBehaviour {
	//public

	//private
	//local const
	//x,y min/max
	const float xmin = -4.0f;
	const float xmax = 4.0f;
	const float ymin = -6.0f;
	const float ymax = 6.0f;

	//system local
	int intervalCnt;	//interval counter

	//component cash
	Transform cashTransform;
	GameObject mainCtr;
	mainController mc;
	GameObject playerCtr;
	playerController plc;

	//local
	//bullet cnt
	int bcnt;

	//current direction
	float cdir;

	//target direction
	float tdir;

	//pos x,y
	float posx;
	float posy;

	//parent enemy130 x,y
	float e130x;
	float e130y;

	//already delete
	bool alreadydelete = false;

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

		//playercontroller
		playerCtr = GameObject.Find ("playerController");
		plc = playerCtr.GetComponent<playerController> ();

		//pos x,y
		cashTransform.position = new Vector3( posx, posy, 0.0f );

		//bullet cnt
		bcnt= 1;	//初回は発射させないため

		//current direction
		//(set from parent objects)
//		cdir = 270.0f;

		//target direction
		//(set from parent objects)
//		tdir = 270.0f;

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
		//nop

		////interval process
		//interval count
		intervalCnt++;
		if (intervalCnt >= 1) {
			intervalCnt = 0;

			//rotation and atack process
			//direction to player
			float xdistance, ydistance;
			float direction;
			const float doffset = +90.0f;
			Vector2 ppos = plc.getPlayerPos ();
			xdistance = (ppos.x) - (cashTransform.position.x);	//player,enemy x distance
			ydistance = (ppos.y) - (cashTransform.position.y);	//player,enemy y distance
			if ((xdistance == 0) && (ydistance == 0)) {	//for zero exception
				xdistance = 0.0001f;
			}
			direction = Mathf.Atan2 (ydistance, xdistance) * Mathf.Rad2Deg;	//distance -> direction
			tdir = direction;
			if (tdir < 0) {
				tdir = tdir + 360.0f;
			}
			//direction current -> target
			const float dspd = 1.8f;
			if ( (tdir > cdir) && ((tdir-cdir) > dspd) ) {
				if ((tdir - cdir) < 180) {
					cdir = cdir + dspd;
				} else {
					cdir = cdir - dspd;
				}
			} else if( (tdir < cdir) && ((cdir-tdir) > dspd) ){
				if ((cdir - tdir) < 180) {
					cdir = cdir - dspd;
				} else {
					cdir = cdir + dspd;
				}
			}else{
				cdir = tdir;
			}
			if (cdir > 360) {
				cdir = cdir - 360;
			}
			if (cdir < 0) {
				cdir = cdir + 360;
			}
			this.cashTransform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, (cdir+doffset)));

			//potision (from parent enemy130)
			cashTransform.position = new Vector3 (e130x, e130y, 0.0f );

			//atack
			//adjust at game level
			float intv=12;
			float intvmax = 26;
			if (mc.gameLevel == mc.gameLevelEasy) {
				intv = 70;
				intvmax = 210;
			} else if (mc.gameLevel == mc.gameLevelNormal) {
				intv = 40;
				intvmax = 150;
			} else if (mc.gameLevel == mc.gameLevelHard) {
				intv = 20;
				intvmax = 90;
			}
			//shot bullet
			if(bcnt == intv){
				if (((xdistance <= 2.0f) && (ydistance <= 2.0f)) || (cashTransform.position.y <= -4.8f)) {
					//nop
				} else {
					//shot bullet
					float bxc;
					float byc;
					float bx;
					float by;
					const float bspd = 0.5f;
					bxc = Mathf.Cos (cdir * Mathf.Deg2Rad) * 1.0f * 0.55f;
					byc = Mathf.Sin (cdir * Mathf.Deg2Rad) * 1.0f * 0.55f;
					mc.generateEnemyBullet120 (cdir, bspd, cashTransform.position.x, cashTransform.position.y, bxc, byc);
				}
			}
			bcnt++;
			if (bcnt >= intvmax) {
				bcnt = 1;	//0で発射せないため
			}

		}
	}


	//public
	//set destoroy
	public void setDestroy(){
		if (alreadydelete == true) {
			return;
		}
		//objnum dec
		if (incobj == true) {
			mc.decObj ();
			incobj = false;
		} else {
			#if UNITY_EDITOR
			Debug.Log ("no inc dec enemy140");
			#endif
		}
		//delete this object
		alreadydelete = true;
		Destroy (gameObject);
	}

	//set status(for parent enemy130)
	public void setStatus( float e130x, float e130y ){	//parent enemy130 potision set
		this.e130x = e130x;
		this.e130y = e130y;
	}

	//set init status
	public void setInitStatus( float dir, float e130x, float e130y ){	//direction,parent enemy130 potision set
		this.cdir = dir;
		this.tdir = dir;
		this.e130x = e130x;
		this.e130y = e130y;
		this.posx = e130x;
		this.posy = e130y;
	}

}
