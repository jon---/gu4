using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy190Controller : MonoBehaviour {
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
	const float yspdbase = 0.16f;
	//base hit point
	const int basehitpoint = 33;
	//score
	readonly int hitscore = 20;
	readonly int score = 1360;

	//system local
	int intervalCnt;	//interval counter

	//component cash
	Transform cashTransform;
	GameObject mainCtr;
	mainController mc;
	GameObject playerCtr;
	playerController plc;

	//local
	//move seq
	int mvseq;

	//pos x,y
	float posx;
	float posy;

	//move speed
	float xx;
	float yy;

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
		//(set from parent objects)
		cashTransform.position = new Vector3( posx, posy, 0.0f );

		//move seq mode
		mvseq = 0;

		//bullet cnt
		bcnt= 0;

		//move speed
		xx = xspdbase;
		yy = yspdbase * -1;

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

			//move and atack process
			switch (mvseq) {
			case 0:
				//move forward (to player)
				//for scroll x move
				cashTransform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, 0.0f));
				cashTransform.Translate(mc.getMapxMov(), 0, 0);
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
				const float dspd = 2.5f;
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
				//rotate
				cashTransform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, (cdir+doffset)));
				//move
				cashTransform.Translate (0, yy, 0);

				//move seq change position?
				const float dstns = 4.8f;
				if( (Mathf.Abs(xdistance) <= dstns) && (Mathf.Abs(ydistance) <= dstns) &&
					((cashTransform.position.y <= 3.8f)&&(cashTransform.position.y >= -3.8f)) &&
					((cashTransform.position.x <= 2.5f)&&(cashTransform.position.x >= -2.5f)) ){
					mvseq++;
				}
				break;
			case 1:
				//atack
				//for scroll x move
				cashTransform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, 0.0f));
				cashTransform.Translate(mc.getMapxMov(), 0, 0);
				//display rotate
				cashTransform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, (cdir+doffset)));
				//adjust at game level
				float intv=12;
				float intvmax = 26;
				if (mc.gameLevel == mc.gameLevelEasy) {
					intv = 5;
					intvmax = 30;
				} else if (mc.gameLevel == mc.gameLevelNormal) {
					intv = 40;
					intvmax = 180;
				} else if (mc.gameLevel == mc.gameLevelHard) {
					intv = 50;
					intvmax = 240;
				}
				//shot bullet
				if( (bcnt%6 == 0) && (bcnt%60 <= intv) ){
					//shot bullet
					float bxoffset;
					float byoffset;
					const float bspd = 0.5f;
					bxoffset = Mathf.Cos (cdir * Mathf.Deg2Rad) * 0.24f;
					byoffset = Mathf.Sin (cdir * Mathf.Deg2Rad) * 0.24f;
					mc.generateEnemyBullet120 ((cdir-50.0f-(float)bcnt/3), bspd*1.2f, cashTransform.position.x, cashTransform.position.y, bxoffset, byoffset);
					mc.generateEnemyBullet120 ((cdir+0.0f), bspd, cashTransform.position.x, cashTransform.position.y, bxoffset, byoffset);
					mc.generateEnemyBullet120 ((cdir+50.0f+(float)bcnt/3), bspd*1.2f, cashTransform.position.x, cashTransform.position.y, bxoffset, byoffset);
				}
				bcnt++;
				if (bcnt >= intvmax) {
					mvseq++;
				}
				break;
			case 2:
				//move forward
				//for scroll move
				cashTransform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, 0.0f));
				cashTransform.Translate(mc.getMapxMov(), 0, 0);
				//rotate
				cashTransform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, (cdir+doffset)));
				//move
				cashTransform.Translate (0, yy, 0);
				//y acceleration
				yy = yy - 0.002f;
				break;
			default:
				break;

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
					Debug.Log ("no inc dec enemy190");
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
				Debug.Log ("no inc dec enemy190");
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
