﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy120Controller : MonoBehaviour {
	//public

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
	//base hit point
	const int basehitpoint = 8;
	//score
	readonly int hitscore = 10;
	readonly int score = 360;

	//system local
	int intervalCnt;	//interval counter

	//component cash
	Transform cashTransform;
	GameObject mainCtr;
	mainController mc;
	GameObject playerCtr;
	playerController plc;

	//local
	//pos x,y
	float posx;
	float posy;

	//bullet cnt
	int bcnt;

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

		//pos x,y
		cashTransform.position = new Vector3( posx, posy, 0.0f );

		//bullet cnt
		bcnt = 1;	//初回発射させないため

		//current direction
		//(set from parent objects)
//		cdir = 270.0f;
//		cdir = -90.0f;

		//target direction
		//(set from parent objects)
//		tdir = 270.0f;
//		tdir = -90.0f;

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

			//move forward at scroll speed (direction to player)
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
			const float dspd = 2.1f;
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
			//move
			this.cashTransform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, 0.0f));
			cashTransform.Translate (mc.getMapxMov(), (mc.getScrollSpeed()*-1), 0);
			this.cashTransform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, (cdir + doffset)));

			//atack
			//adjust at game level
			float intv = 0;
			float intvmax = 0;
			if (mc.gameLevel == mc.gameLevelEasy) {
				intv = 60;
				intvmax = 200;
			} else if (mc.gameLevel == mc.gameLevelNormal) {
				intv = 30;
				intvmax = 100;
			} else if (mc.gameLevel == mc.gameLevelHard) {
				intv = 10;
				intvmax = 40;
			}
			//shot bullet
			if(bcnt % intv == 0){
				if (((xdistance <= 2.0f) && (ydistance <= 2.0f)) || (cashTransform.position.y <= -5.0f)) {
					//nop
				} else {
					if (Random.Range (0, 6) <= 4) {
						//shot bullet
						float bxc1;
						float byc1;
						float bxc2;
						float byc2;
						float bx;
						float by;
						const float bspd = 0.68f;
						bxc1 = Mathf.Cos ((cdir - 31.0f) * Mathf.Deg2Rad) * 0.52f;
						byc1 = Mathf.Sin ((cdir - 31.0f) * Mathf.Deg2Rad) * 0.52f;
						bxc2 = Mathf.Cos ((cdir + 31.0f) * Mathf.Deg2Rad) * 0.52f;
						byc2 = Mathf.Sin ((cdir + 31.0f) * Mathf.Deg2Rad) * 0.52f;
						bx = Mathf.Cos ((cdir + 0.0f) * Mathf.Deg2Rad) * 1.0f;
						by = Mathf.Sin ((cdir + 0.0f) * Mathf.Deg2Rad) * 1.0f;
						mc.generateEnemyBullet100 (1, cashTransform.position.x, cashTransform.position.y, bxc1, byc1, bx * bspd, by * bspd);
						mc.generateEnemyBullet100 (1, cashTransform.position.x, cashTransform.position.y, bxc2, byc2, bx * bspd, by * bspd);
					}
				}
			}
			bcnt++;
			if (bcnt > intvmax) {
				bcnt = 0;
			}

			//move result process
			if ( (cashTransform.position.y > ymax) ||
				(cashTransform.position.y < ymin) ||
				(cashTransform.position.x < xmin) ||
				(cashTransform.position.x > xmax) ){
				if (alreadydelete == true) {
					return;
				}
				//objnum dec
				if (incobj == true) {
					mc.decObj ();
					incobj = false;
				} else {
					#if UNITY_EDITOR
					Debug.Log ("no inc dec enemy120");
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
			//objnum dec
			if (incobj == true) {
				mc.decObj ();
				incobj = false;
			} else {
				#if UNITY_EDITOR
				Debug.Log ("no inc dec enemy120");
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
	public void setInitStatus( float dir, int itm, float px, float py ){	//direction,item,posx/y set
		this.cdir = dir;
		this.tdir = dir;
		this.item = itm;	
		this.posx = px;
		this.posy = py;
	}

}
