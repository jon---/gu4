using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerup100Controller : MonoBehaviour {
	//public
	//public getstar100 Prefab
	public GameObject getStar100ControllerPrefab;
	//item sprite
	public Sprite spPower;
	public Sprite spLaser;
	public Sprite spMissile;
	public Sprite spOption;
	public Sprite spBomb;
	public Sprite spShield;
	public Sprite spScore;
	public Sprite sp1up;

	//item type
	public int pType_powerup = 0x00;
	public int pType_laser = 0x01;
	public int pType_missile = 0x02;
	public int pType_option = 0x03;
	public int pType_bomb = 0x04;
	public int pType_shield = 0x05;
	public int pType_score = 0x06;
	public int pType_1up = 0x07;


	//private
	//local const
	//x,y min/max
	const float xmin = -2.437f;//-3.0f;
	const float xmax = 2.437f;//3.0f;
	const float ymin = -4.628f;//-5.0f;
	const float ymax = 4.628f;//5.0f;
	//x,y speed base
	const float xspd_base = 0.11f;
	const float yspd_base = 0.11f;

	//sub weapon
	const int swpLaser = 0x00;
	const int swpMissile = 0x01;

	//color
	const float clmax = 255.0f;
	const float clmin = 160.0f;
	const float clstep = 18.0f;

	//system local
	int intervalCnt;	//interval count

	//local
	//cash
	//component cash
	Transform cashTransform;
	SpriteRenderer sr;
//	Animator animt;
	GameObject mainCtr;
	mainController mc;
	GameObject playerCtr;
	playerController plc;

	//move seq
	int mvseq;

	//x,y move speed (direction)
	private float xx;
	private float yy;
	float xs;
	float ys;

	//pos x,y
	float posx;
	float posy;

	//target direction
	float tdir;

	//current direction
	float cdir;

	//display direction
	float ddir;
	float dd;

	//delete time
	int deltime;

	//item type
	public int pType;	//(set from parent objects)

	//color val
	bool fdout;
	float clval;

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

		//sprite
		sr = GetComponent<SpriteRenderer>();

		//main controller
		mainCtr = GameObject.Find ("mainController");
		mc = mainCtr.GetComponent<mainController> ();

		//playercontroller
		playerCtr = GameObject.Find ("playerController");
		plc = playerCtr.GetComponent<playerController> ();

		//animator
//		animt = GetComponent<Animator>();
//		animt.speed = 1.5f;

		//move seq
		mvseq = 0;

		//display direction
		ddir = 0.0f;
		dd = 24.0f;

		//pos x,y (set from parent objects)
		cashTransform.position = new Vector3( posx, posy, 0.0f );

		//target direction init
		//direction to player
		float xdistance, ydistance;
		float direction;
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

		//score以外は初期角度を変更する
		if (pType != pType_score) {
			if (cashTransform.position.x >= 0) {
				tdir = tdir - (90.0f + Random.Range(-25.0f, +25.0f));
				if (tdir <= 0) {
					tdir = tdir + 360;
				}
			} else {
				tdir = tdir + (90.0f + Random.Range(-25.0f, +25.0f));
				if (tdir >= 360) {
					tdir = tdir - 360;
				}
			}
		}

		//current direction
		cdir = tdir;

		//x,y move speed (direction)
		if (pType != pType_score) {
			xx = Mathf.Cos (cdir * Mathf.Deg2Rad) * 1.0f;
			yy = Mathf.Sin (cdir * Mathf.Deg2Rad) * 1.0f;
		} else {
			xx = 0.0f;
			yy = 1.0f;
		}

		xs = 1.8f;
		ys = 1.8f;

		//score scale
		if (pType == pType_score) {
			cashTransform.localScale = new Vector3 (0.9f, 0.9f, 1.0f);
		}

		//delete time
		if (pType == pType_score) {
			deltime = 0;
		} else {
			deltime = 2500;
		}

		//type
		//(set from parent objects)
//		pType = pType_powerup;

		//sprite
		//(set from parent objects)
		Sprite[] spr = new Sprite[]{ spPower, spLaser, spMissile, spOption, spBomb, spShield, spScore, sp1up };
		sr.sprite = spr[pType];

		//color
		fdout = true;
		clval = 255.0f;

		//同フレームでincするため 生成時にincする
		//star obj num inc
//		if (pType == pType_score) {
//			mc.incStarObjNum ();
//		}

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

		//delete time
		if (deltime > 0) {
			deltime = deltime - 1;
		}

		//move
		switch(mvseq){
		case 0:
			//score type only
			if (this.pType == pType_score) {
				//target direction
				//direction to player
				float xdistance, ydistance;
				float direction;
				Vector2 ppos = plc.getPlayerPos ();
				const float doffset = +90.0f;
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
				const float dspd = 1.28f;
				if ((tdir > cdir) && ((tdir - cdir) > dspd)) {
					if ((tdir - cdir) < 180) {
						cdir = cdir + dspd;
					} else {
						cdir = cdir - dspd;
					}
				} else if ((tdir < cdir) && ((cdir - tdir) > dspd)) {
					if ((cdir - tdir) < 180) {
						cdir = cdir - dspd;
					} else {
						cdir = cdir + dspd;
					}
				} else {
					cdir = tdir;
				}
				if (cdir > 360) {
					cdir = cdir - 360;
				}
				if (cdir < 0) {
					cdir = cdir + 360;
				}
				//display rotation angle
				ddir = ddir - dd;
				if (ddir <= 0.0f) {
					ddir = ddir + 360.0f;
				}
				//display rotation brake
				if (dd > 2) {
					dd = dd - 0.8f;
					if (dd <= 2) {
						dd = 2;
					}
				}
				//move
				this.cashTransform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, cdir-doffset) );
//				cashTransform.Translate (xx*xspd_base*xs, yy*yspd_base*ys, 0);
				cashTransform.Translate (xx, yy*yspd_base*ys, 0);
				// move speed
//				xs = xs - 0.02f;
//				if (xs <= 1.0f) {
//					xs = 1.0f;
//				}
				ys = ys - 0.02f;
				if (ys <= 1.0f) {
					ys = 1.0f;
				}
				//move result process
				this.moveChange ();
				//display rotation
				this.cashTransform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, ddir) );
			} else {
				//move
				cashTransform.Translate (xx*xspd_base, yy*yspd_base, 0);
				//move result process
				this.moveChange ();
			}
			break;
		default:
			break;
		}


		////interval process
		//interval count
		intervalCnt++;
		if (intervalCnt >= 1) {
			intervalCnt = 0;

			//color change
			if (fdout == true) {
				clval = clval - clstep;
				if (clval <= clmin) {
					clval = clmin;
					fdout = false;
				}
			} else {
				clval = clval + clstep;
				if (clval >= clmax) {
					clval = clmax;
					fdout = true;
				}
			}
			Color cr = sr.color;
			cr.r = clval / 255.0f;
			cr.g = clval / 255.0f;
			cr.b = clval / 255.0f;
			sr.color = cr;

		}

	}

	//move change
	private void moveChange(){
		float xmx = xmax;
		float xmn = xmin;
		float ymx = ymax;
		float ymn = ymin;
		if (pType == pType_score) {
			xmx = 4.0f;
			xmn = -4.0f;
			ymx = 6.0f;
			ymn = -6.0f;
		}
		if ( cashTransform.position.x < xmn ){
			if (deltime <= 0) {
				if (alreadydelete == true) {
					return;
				}
				//objnum dec
				if (incobj == true) {
					//star obj num dec
					if (pType == pType_score) {
						mc.decStarObjNum ();
					}
					mc.decObj ();
					incobj = false;
				} else {
					#if UNITY_EDITOR
					Debug.Log ("no inc dec powerup100");
					#endif
				}
				//delete this
				alreadydelete = true;
				Destroy (gameObject);
			} else {
				Vector3 pos = cashTransform.position;
				pos.x = xmn;
				cashTransform.position = pos;
				xx = xx - (xx * 2.0f);
			}
		}
		if ( cashTransform.position.x > xmx ){
			if (deltime <= 0) {
				if (alreadydelete == true) {
					return;
				}
				//objnum dec
				if (incobj == true) {
					//star obj num dec
					if (pType == pType_score) {
						mc.decStarObjNum ();
					}
					mc.decObj ();
					incobj = false;
				} else {
					#if UNITY_EDITOR
					Debug.Log ("no inc dec powerup100");
					#endif
				}
				//delete this
				alreadydelete = true;
				Destroy (gameObject);
			} else {
				Vector3 pos = cashTransform.position;
				pos.x = xmx;
				cashTransform.position = pos;
				xx = xx - (xx * 2.0f);
			}
		}
		if ( cashTransform.position.y < ymn ){
			if (deltime <= 0) {
				if (alreadydelete == true) {
					return;
				}
				//objnum dec
				if (incobj == true) {
					//star obj num dec
					if (pType == pType_score) {
						mc.decStarObjNum ();
					}
					mc.decObj ();
					incobj = false;
				} else {
					#if UNITY_EDITOR
					Debug.Log ("no inc dec powerup100");
					#endif
				}
				//delete this
				alreadydelete = true;
				Destroy (gameObject);
			} else {
				Vector3 pos = cashTransform.position;
				pos.y = ymn;
				cashTransform.position = pos;
				yy = yy - (yy * 2.0f);
			}
		}
		if ( cashTransform.position.y > ymx ){
			if (deltime <= 0) {
				if (alreadydelete == true) {
					return;
				}
				//objnum dec
				if (incobj == true) {
					//star obj num dec
					if (pType == pType_score) {
						mc.decStarObjNum ();
					}
					mc.decObj ();
					incobj = false;
				} else {
					#if UNITY_EDITOR
					Debug.Log ("no inc dec powerup100");
					#endif
				}
				//delete this
				alreadydelete = true;
				Destroy (gameObject);
			} else {
				Vector3 pos = cashTransform.position;
				pos.y = ymx;
				cashTransform.position = pos;
				yy = yy - (yy * 2.0f);
			}
		}
	}

	//move change collision side map
//	private void moveChangeSideMap(){
//		//todo:
//	}


	//public
	//collision
	public void OnTriggerEnter2D(Collider2D coll){
		if (alreadydelete == true) {
			return;
		}
		string cotag = coll.gameObject.tag;
		if ((cotag == "player") || (cotag == "unavailablePlayer")) {
			//collision player
			int plMode = plc.getPlayerMode();
			if ((plMode == plc.playerModeNormal) || (plMode == plc.playreModeInvalid)) {
				//generate getstar effect
				if (this.pType == this.pType_score) {
					this.generateGetStar100Effects ();
				}
				//objnum dec
				if (incobj == true) {
					//star obj num dec
					if (pType == pType_score) {
						mc.decStarObjNum ();
					}
					mc.decObj ();
					incobj = false;
				} else {
					#if UNITY_EDITOR
					Debug.Log ("no inc dec power100");
					#endif
				}
				//delete this
				alreadydelete = true;
				Destroy (gameObject);
			}
//		} else if (cotag == "sideMap") {
//			//collision side map
//			this.moveChangeSideMap();
		} else {
			//collision other
		}
	}

	//generate getstar effect
	private void generateGetStar100Effects(){
		//generate getstar effect
		Vector2 ppos = plc.getPlayerPos ();
		GameObject go;
		getStar100Controller s100;
//		for (int i = 0; i < 6; i++) {
		for (int i = 0; i < 3; i++) {
			//genarate
			go = Instantiate (getStar100ControllerPrefab) as GameObject;
			s100 = go.GetComponent<getStar100Controller> ();
			s100.setInitStatus ((ppos.x + Random.Range (-0.1f, +0.1f)), (ppos.y + Random.Range (0.01f, +0.1f)));
		}
	}

	//public

	//set init status
	public void setInitStatus(  int type, int sw, float px, float py ){
		if ( (sw == swpLaser) && (type == pType_missile) ) {
			type = pType_laser;
		}
		if ( (sw ==swpMissile) && (type == pType_laser) ) {
			type = pType_missile;
		}
		if (type == pType_score) {
			this.tag = "scoreItem";
		}
		this.pType = type;

		this.posx = px;
		this.posy = py;
	}

	//get item type
	public int getType(){
		return this.pType;
	}

}
