using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy130Controller : MonoBehaviour {
	//public
	//public enemy140 prefab(for game event)
	public GameObject enemy140ControllerPrefab;

	//private
	//local const
	//x,y min/max
	const float xmin = -5.5f;	//基本4.0だが横スクロール補正で消えてしまう対策で+1.5f
	const float xmax = 5.5f;
	const float ymin = -6.0f;
	const float ymax = 6.0f;
	//x,y speed base
	const float xspdbase = 0.00f;
	const float yspdbase = 0.00f;
	const float spdbase = 0.070f;
	//base hit point
	const int basehitpoint = 8;
	//score
	readonly int hitscore = 10;
	readonly int score = 350;

	//system local
	int intervalCnt;	//interval counter

	//component cash
	Transform cashTransform;
	GameObject mainCtr;
	mainController mc;
	GameObject playerCtr;
	playerController plc;
	Animator animt;
	enemy140Controller e140;

	//local
	//move seq
	int mvseq;

	//pos x,y
	float posx;
	float posy;

	//move speed
	float xx;
	float yy;

	//movement pattern
	int mp;
	const int mp_stop = 0;	//stop only
	const int mp_forward = 1;	//forward only
	const int mp_approaches = 2;	//approaches to player
	const int mp_escape = 3;	//escape for player
	const int mp_totarget = 4;	//to target x,y

	//escape stop time
	int stoptime;

	//target x,y
	float tx;
	float ty;

	//current direction
	float cdir;

	//target direction
	float tdir;

	//item
	int item;

	//init hitpoint
	int eHpIntial;

	//hitpoint
	int eHp;

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

		//animator
		animt = GetComponent<Animator>();
		if ( (mp == mp_stop) || (mp == mp_escape) ) {
			animt.speed = 0.0f;
		} else {
			animt.speed = 0.0f;//2.0f;
		}

		//pos x,y
		cashTransform.position = new Vector3( posx, posy, 0.0f );

		//movement pattern
		//(set from parent objects)
//		mp = mp_stop;

		//target x,y
		//(set from parent objects)
//		tx = 0.0f;
//		ty = 6.0f;

		//generate enemy140 and init
		GameObject go = Instantiate (enemy140ControllerPrefab) as GameObject;
		e140 = go.GetComponent<enemy140Controller> ();
		e140.setInitStatus (cdir, cashTransform.position.x, cashTransform.position.y);	//init

		//stop time
		if ( (mp == mp_escape) || (mp == mp_totarget) || (mp == mp_approaches)){
			stoptime = 38;
		} else {
			stoptime = 0;
		}

		//move seq mode
		mvseq = 0;

		//bullet cnt
//		bcnt= 1;	//初回発射させないため

		//current direction
		//(set from parent objects)
//		cdir = 270.0f;
//		cdir = -90.0f;

		//target direction
		//(set from parent objects)
//		tdir = 270.0f;
//		tdir = -90.0f;

		//display direction
		//(set from parent objects)
//		ddir = 120.0f

		//item
		//(set from parent objects)
//		item = -1;

		//enemy inital hitpoint
		eHpIntial = basehitpoint + 0;

		//enemy hitpoint
		eHp = eHpIntial;

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

			//move and atack process
			const float doffset = +90.0f;

			//movement pattern process
			switch (mp) {
			case mp_stop:
				//stop
				xx = 0.0f;
				yy = 0.0f;
				break;
			case mp_forward:
				//move forward only
				xx = 0.0f * spdbase;
				yy = -1.0f * spdbase;
				break;
			case mp_approaches:
				//direction to player
			case mp_escape:
				//escape for player
			case mp_totarget:
				//to target
				float xdistance, ydistance;
				float direction;
				const float dspd = 3.4f;
				float tposx = 0.0f;
				float tposy = 0.0f;
				if ( ((mp == mp_escape)||(mp == mp_totarget)||(mp == mp_approaches)) && (stoptime > 0) ) {
					stoptime--;
					if (stoptime <= 0) {
						stoptime = 0;
						animt.speed = 2.0f;
					}
					xx = 0.0f;
					yy = 0.0f;
					break;
				}
				//target x,y
				if ( (mp == mp_approaches) || (mp == mp_escape) ) {	//to player
					Vector2 ppos = plc.getPlayerPos ();
					tposx = ppos.x;
					tposy = ppos.y;
				} else if (mp == mp_totarget) {	//to target
					tposx = tx;
					tposy = ty;
				}
				xdistance = (tposx) - (cashTransform.position.x);	//target,enemy x distance
				ydistance = (tposy) - (cashTransform.position.y);	//target,enemy y distance
				if ((xdistance == 0) && (ydistance == 0)) {	//for zero exception
					xdistance = 0.0001f;
				}
				direction = Mathf.Atan2 (ydistance, xdistance) * Mathf.Rad2Deg;	//distance -> direction
				tdir = direction;
				if (tdir < 0) {
					tdir = tdir + 360.0f;
				}
				if (mp == mp_escape) {	//move escape?
					tdir = tdir + 180.0f;
					if (tdir > 360) {
						tdir = tdir - 360;
					}
				}
				//direction current -> target
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
				xx = 0.0f * spdbase;
				yy = -1.0f * spdbase;
				break;
			default:
				break;
			}
			//move (scroll)
			this.cashTransform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, 0.0f));
			cashTransform.Translate (mc.getMapxMov(), (mc.getScrollSpeed()*-1), 0);
			//move
			this.cashTransform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, (cdir + doffset)));
			cashTransform.Translate (xx, yy, 0);
			//set enemy140 status
			e140.setStatus( cashTransform.position.x, cashTransform.position.y );

			//move result process
			if ( (cashTransform.position.y > ymax) ||
				(cashTransform.position.y < ymin) ||
				(cashTransform.position.x < xmin) ||
				(cashTransform.position.x > xmax) ){
				if (alreadydelete == true) {
					return;
				}
				//enemy140 destroy
				e140.setDestroy();
				//objnum dec
				if (incobj == true) {
					mc.decObj ();
					incobj = false;
				} else {
					#if UNITY_EDITOR
					Debug.Log ("no inc dec enemy130");
					#endif
				}
				//delete this object
				alreadydelete = true;
				Destroy (gameObject);
			}

		}
	}


	//public
	//collision
	public void OnTriggerEnter2D(Collider2D coll){
		if (alreadydelete == true) {
			return;
		}
		string cotag = coll.gameObject.tag;
		if (cotag == "player") {
			//collision player
			mc.generateEnemyDamageEffect( cashTransform.position.x, cashTransform.position.y );
			this.enemyHit( mc.damagePlayer );
		} else if (cotag == "playerBullet") {
			//collision player bullet
			this.enemyHit( mc.damagePlayerBullet );
		} else if (cotag == "playerLaser1") {
			//collision player laser 1
			this.enemyHit( mc.damagePlayerLaser1 );
		} else if (cotag == "playerLaser2") {
			//collision player laser 2
			this.enemyHit( mc.damagePlayerLaser2 );
		} else if (cotag == "playerMissile1") {
			//collision player missile 1
			this.enemyHit( mc.damagePlayerMissile1 );
		} else if (cotag == "playerMissile2") {
			//collision player missile 2
			this.enemyHit( mc.damagePlayerMissile2 );
		} else if (cotag == "missileBomb1") {
			//collision player missile bomb 1
			mc.generateEnemyDamageEffect( cashTransform.position.x, cashTransform.position.y );
			this.enemyHit( mc.damagePlayerMissileBomb1 );
		} else if (cotag == "missileBomb2") {
			//collision player missile bomb 2
			mc.generateEnemyDamageEffect( cashTransform.position.x, cashTransform.position.y );
			this.enemyHit( mc.damagePlayerMissileBomb1 );
		} else if (cotag == "bomb1") {
			//collision player bomb 1
			mc.generateEnemyDamageEffect( cashTransform.position.x, cashTransform.position.y );
			this.enemyHit( mc.damagePlayerBomb1 );
		} else if (cotag == "bomb2") {
			//collision player bomb 2
			mc.generateEnemyDamageEffect( cashTransform.position.x, cashTransform.position.y );
			this.enemyHit( mc.damagePlayerBomb2 );
		} else if (cotag == "bombLaser") {
			//collision player bomb laser
			mc.generateEnemyDamageEffect( cashTransform.position.x, cashTransform.position.y );
			this.enemyHit( mc.damagePlayerBombLaser );
		} else {
			//collision other
		}
	}

	//private
	//enemy hit process
	private void enemyHit( int damage ){
		if (eHp <= 0) {	//複数回呼ばれ対策
			return;
		}
		eHp = eHp - damage;
		if (eHp <= 0) {
			eHp = 0;
			//adjust at game level
			if (mc.gameLevel == mc.gameLevelEasy) {
				//nop
			} else if (mc.gameLevel == mc.gameLevelNormal) {
				//nop
			} else if (mc.gameLevel == mc.gameLevelHard) {
				//shot counter bullet
				if (damage < mc.damageBig) {
					mc.generateCounterBullet (0, cashTransform.position.x, cashTransform.position.y, 0, 0);
				}
			}
			//add game score
			mc.addGameScore( this.score );
			//generate explosion middle effect 
			mc.generateExplosionMiddleEffect( (cashTransform.position.x), (cashTransform.position.y) );
			//generate ground explosion effect
			mc.generateGroundExplosionEffect( (cashTransform.position.x), (cashTransform.position.y) );
			//generate power up item(score)
			mc.generatePowerup100( mc.puType_score, cashTransform.position.x, cashTransform.position.y );
			//generate power up item
			if (this.item != mc.puType_None) {
				mc.generatePowerup100( item, cashTransform.position.x, cashTransform.position.y );
			}
			//enemy140 destroy
			e140.setDestroy();
			//objnum dec
			if (incobj == true) {
				mc.decObj ();
				incobj = false;
			} else {
				#if UNITY_EDITOR
				Debug.Log ("no inc dec enemy130");
				#endif
			}
			//destroy this
			alreadydelete = true;
			Destroy (gameObject);
		}
		//add game score
		mc.addGameScore( this.hitscore );
	}


	//public
	public void setInitStatus( float cdir, int mp, float tx, float ty, int itm, float px, float py ){	//direction, movement pattern, target x/y, item, posx/y set
		this.cdir = cdir;
		this.tdir = cdir;
		this.mp = mp;
		this.tx = tx;
		this.ty = ty;
		this.item = itm;
		this.posx = px;
		this.posy = py;
	}

}
