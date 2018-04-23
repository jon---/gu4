using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy170Controller : MonoBehaviour {

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
	const float yspdbase = 0.21f;
	//base hit point
	const int basehitpoint = 19;
	//score
	readonly int hitscore = 30;
	readonly int score = 1060;

	//system local
	int intervalCnt;	//interval counter

	//component cash
	Transform cashTransform;
	SpriteRenderer sr;
	GameObject mainCtr;
	mainController mc;
	GameObject playerCtr;
	playerController plc;
	Animator animt;

	//local
	//pos x,y
	float posx;
	float posy;

	//target pos
	float tposx;
	float tposy;
	float tpossize;
	float tposdir;
	float tposdd;

	//move speed
	float xx;
	float yy;

	//bullet cnt
	int bcnt;

	//atack stop cnt
	int attime;

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

		//sprite renderer
		sr = GetComponent<SpriteRenderer>();

		//animator
		animt = GetComponent<Animator>();
		animt.speed = 2.0f;

		//maincontroller
		mainCtr = GameObject.Find ("mainController");
		mc = mainCtr.GetComponent<mainController> ();

		//playercontroller
		playerCtr = GameObject.Find ("playerController");
		plc = playerCtr.GetComponent<playerController> ();

		//pos x,y
		//(set from parent objects)
		cashTransform.position = new Vector3( posx, posy, 0.0f );

		//target pos
		tposx = 0.0f;
		tposy = 0.0f;
		tpossize = 2.9f;
		tposdir = 90.0f;
		tposdd = 10.0f;

		//bullet cnt
		bcnt = 0;

		//atack stop cnt
		attime = 0;

		//move speed
		xx = xspdbase * Random.Range( -1.5f, 1.5f );
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

			//move forward (to player circle)
			//for scroll x move
			cashTransform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, 0.0f));
			cashTransform.Translate (mc.getMapxMov (), 0, 0);
			//target
			Vector2 ppos = plc.getPlayerPos ();
			if (bcnt == 0) {
				tposx = ppos.x + (Mathf.Cos (tposdir * Mathf.Deg2Rad) * 1.0f * tpossize);
				tposy = ppos.y + 5.1f + (Mathf.Sin (tposdir * Mathf.Deg2Rad) * 1.0f * tpossize);
				tposdir = tposdir + tposdd;
				if (tposdir >= 360) {
					tposdir = tposdir - 360;
				}
			} else {
				tposx = ppos.x;
				tposy = ppos.y;
			}
			//direction to target
			float xdistance, ydistance;
			float direction;
			const float doffset = +90.0f;
			xdistance = (tposx) - (cashTransform.position.x);	//player,enemy x distance
			ydistance = (tposy) - (cashTransform.position.y);	//player,enemy y distance
			if ((xdistance == 0) && (ydistance == 0)) {	//for zero exception
				xdistance = 0.0001f;
			}
			direction = Mathf.Atan2 (ydistance, xdistance) * Mathf.Rad2Deg;	//distance -> direction
			tdir = direction;
			if (tdir < 0) {
				tdir = tdir + 360.0f;
			}
			//direction current -> target
			const float dspd = 80.0f;	//3.0f
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
			//rotate
			cashTransform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, (cdir + doffset)));
			//move
			if (bcnt == 0) {
				cashTransform.Translate (0, yy, 0);
			}

			//atack start position?
			const float dstns = 3.6f;
			float xdis = ppos.x - cashTransform.position.x;
			float ydis = ppos.y - cashTransform.position.y;
			if ((Mathf.Abs (xdis) <= dstns) && (Mathf.Abs (ydis) <= dstns) &&
				((cashTransform.position.y <= 4.8f) && (cashTransform.position.y >= -4.8f)) &&
				((cashTransform.position.x <= 2.8f) && (cashTransform.position.x >= -2.8f))) {
				if (bcnt == 0) {
					if (attime <= 0) {
						bcnt = 1;
					}
				}
			}
			//atack
			const int seqbase = 2;
			if (bcnt >= 1) {
				bcnt++;
				if (bcnt == seqbase * 4) {
					float bspd = 0.8f;
					if (mc.gameLevel == mc.gameLevelEasy) {
						bspd = 0.7f;
					}else if (mc.gameLevel == mc.gameLevelHard) {
						bspd = 0.95f;
						mc.generateEnemyBullet100 (0, cashTransform.position.x, cashTransform.position.y, 0.0f, 0.0f, bspd*1.1f, bspd*1.1f);
					}
					mc.generateEnemyBullet100 (0, cashTransform.position.x, cashTransform.position.y, 0.0f, 0.0f, bspd, bspd);
				} else if (bcnt == seqbase * 9) {
					bcnt = 0;
					if (mc.gameLevel == mc.gameLevelEasy) {
						attime = 20;
					} else if (mc.gameLevel == mc.gameLevelNormal) {
						attime = 10;
					} else if (mc.gameLevel == mc.gameLevelHard) {
						attime = 3;
					}
				}
			}
			//atack stop time
			if (attime > 0) {
				attime--;
			}
			//move result process
/* //なし
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
					Debug.Log ("no inc dec enemy170");
					#endif
				}
				//delete this object
				alreadydelete = true;
				Destroy (gameObject);
			}
*/

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
				if (bcnt >= 1) {	//stop(atack) only
					mc.generatePowerup100 (item, cashTransform.position.x, cashTransform.position.y);
				}
			}
			//objnum dec
			if (incobj == true) {
				mc.decObj ();
				incobj = false;
			} else {
				#if UNITY_EDITOR
				Debug.Log ("no inc dec enemy170");
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
