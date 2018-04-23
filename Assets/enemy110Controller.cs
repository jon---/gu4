using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy110Controller : MonoBehaviour {
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
	const float yspdbase = 0.12f;
	const float spdbase = 0.12f;
	//base hit point
	const int basehitpoint = 6;
	//score
	readonly int hitscore = 30;
	readonly int score = 450;

	//system local
	int intervalCnt;	//interval counter

	//component cash
	Transform cashTransform;
	GameObject mainCtr;
	mainController mc;
	GameObject playerCtr;
	playerController plc;
	Animator animt;

	//local
	//move seq
	int mvseq;

	//pos x,y
	float posx;
	float posy;

	//move speed
	float xx;
	float yy;
	float xs;
	float ys;
	float spd;

	//rotation start time
	int rst;

	//move time (no stop)
	int mvt;

	//bullet cnt
	int bcnt;

	//current direction
	float cdir;

	//target direction
	float tdir;

	//display direction
	float ddir;

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

		//animator
		animt = GetComponent<Animator>();
		animt.speed = 4.0f;

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
		bcnt= 1;	//初回発射させないため

		//move speed
		ys = 1.62f;
		xs = 0.0f;
		spd = 1.62f;

		//rotation start time
		rst = 10;

		//move time (no stop)
		mvt = 1;

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
			switch (mvseq) {
			case 0:
				//move forward and atatck
				//for scroll x move
				cashTransform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, 0.0f));
				cashTransform.Translate(mc.getMapxMov(), 0, 0);
				//direction to player (dispaly only rotation)
				float xdistance, ydistance;
				float direction;
				const float doffset = +90.0f;
				const float dspd = 5.5f;
				Vector2 ppos = plc.getPlayerPos ();
				if (rst > 0) {
					rst--;
				}
				if (rst <= 0) {
					rst = 0;
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
					if ((tdir > ddir) && ((tdir - ddir) > dspd)) {
						if ((tdir - ddir) < 180) {
							ddir = ddir + dspd;
						} else {
							ddir = ddir - dspd;
						}
					} else if ((tdir < ddir) && ((ddir - tdir) > dspd)) {
						if ((ddir - tdir) < 180) {
							ddir = ddir - dspd;
						} else {
							ddir = ddir + dspd;
						}
					} else {
						ddir = tdir;
					}
					if (ddir > 360) {
						ddir = ddir - 360;
					}
					if (ddir < 0) {
						ddir = ddir + 360;
					}
				}
				//display rotate
				this.cashTransform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, (ddir + doffset)));
				//move rotation
				float cd = 0;
				if (cdir >= (ddir + doffset)) {
					cd = (cdir - (ddir + doffset)) * 1.0f;
				} else if (cdir < (ddir + doffset)) {
					cd = ((ddir + doffset) - cdir) * -1.0f;
				}
				if (cd > 360.0f) {
					cd = cd - 360.0f;
				}
				if (cd < 0) {
					cd = cd + 360.0f;
				}
				xx = Mathf.Cos (cd * Mathf.Deg2Rad) * 1.0f;
				yy = Mathf.Sin (cd * Mathf.Deg2Rad) * 1.0f;
				//move speed
				if (spd > 0.25f) {
					spd = spd - 0.027f;
				}
				if (spd <= 0.25) {
					spd = 0.25f;
					cdir = ddir;
					mvt = 3;
					mvseq++;
					break;
				}
				xx = xx * spdbase * 1.0f * spd;
				yy = yy * spdbase * 1.0f * spd;
				//move
				cashTransform.Translate (xx, yy, 0);
				//atack
				//adjust at game level
				float intv = 28;
				float intvmax = 200;
				if (mc.gameLevel == mc.gameLevelEasy) {
					intv = 35;
					intvmax = 200;
				} else if (mc.gameLevel == mc.gameLevelNormal) {
					intv = 28;
					intvmax = 200;
				} else if (mc.gameLevel == mc.gameLevelHard) {
					intv = 20;
					intvmax = 61;
				}
				//shot bullet
				if (bcnt % intv == 0) {
					//shot bullet
					float bxl;
					float byl;
					float bxr;
					float byr;
					float bx;
					float by;
					const float bspd = 0.71f;
					bxl = Mathf.Cos ((ddir + 46.0f) * Mathf.Deg2Rad) * 1.0f;
					byl = Mathf.Sin ((ddir + 46.0f) * Mathf.Deg2Rad) * 1.0f;
					bxr = Mathf.Cos ((ddir - 46.0f) * Mathf.Deg2Rad) * 1.0f;
					byr = Mathf.Sin ((ddir - 46.0f) * Mathf.Deg2Rad) * 1.0f;
					mc.generateEnemyBullet120 (ddir, bspd, cashTransform.position.x, cashTransform.position.y, bxl * 0.40f, byl * 0.40f);
					mc.generateEnemyBullet120 (ddir, bspd, cashTransform.position.x, cashTransform.position.y, bxr * 0.40f, byr * 0.40f);
				}
				bcnt++;
				if (bcnt > intvmax) {
					bcnt = 0;
				}
				break;
			case 1:
				//move forward (to player)
				//for scroll x move
				cashTransform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, 0.0f));
				cashTransform.Translate (mc.getMapxMov (), 0, 0);
				//move speed
				if (ys < 1.0f) {
					ys = ys + 0.02f;
				}
				if (ys >= 1.00) {
					ys = 1.00f;
				}
				yy = yspdbase * -1 * ys;
				//direction to player
				Vector2 ppos2 = plc.getPlayerPos ();
				xdistance = (ppos2.x) - (cashTransform.position.x);	//player,enemy x distance
				ydistance = (ppos2.y) - (cashTransform.position.y);	//player,enemy y distance
				if ((xdistance == 0) && (ydistance == 0)) {	//for zero exception
					xdistance = 0.0001f;
				}
				direction = Mathf.Atan2 (ydistance, xdistance) * Mathf.Rad2Deg;	//distance -> direction
				tdir = direction;
				if (tdir < 0) {
					tdir = tdir + 360.0f;
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
				//rotate
				cashTransform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, (cdir + doffset)));
				//move
				const float dstns2 = 1.5f;
				if ((Mathf.Abs (xdistance) >= dstns2) || (Mathf.Abs (ydistance) >= dstns2)) {
					float spd = 0.0f;
					if (mc.gameLevel == mc.gameLevelEasy) {
						spd = 0.6f;
					} else if (mc.gameLevel == mc.gameLevelNormal) {
						spd = 0.7f;
					} else if (mc.gameLevel == mc.gameLevelHard) {
						spd = 1.0f;
					}
					cashTransform.Translate (0, yy*spd, 0);
				}
				//move seq change position?
				if (mvt > 0) {
					mvt--;
				}
				if (mvt <= 0) {
					mvt = 0;
				}
				const float dstns = 2.8f;
				if ((Mathf.Abs (xdistance) <= dstns) && (Mathf.Abs (ydistance) <= dstns) &&
					(Mathf.Abs (xdistance) >= dstns2) && (Mathf.Abs (ydistance) >= dstns2) &&
				    (Mathf.Abs (tdir - cdir) <= 10.0f) &&
				    ((cashTransform.position.y <= 3.8f) && (cashTransform.position.y >= -3.8f)) &&
				    ((cashTransform.position.x <= 2.1f) && (cashTransform.position.x >= -2.1f)) &&
				    (mvt <= 0)) {
					bcnt = 0;
					mvseq++;
				}
				break;
			case 2:
				//atack
				//for scroll move
				cashTransform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, 0.0f));
				cashTransform.Translate(mc.getMapxMov(), 0, 0);
				//display rotate
				cashTransform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, (cdir + doffset)));
				//adjust at game level
				intv = 0;
				intvmax = 0;
				if (mc.gameLevel == mc.gameLevelEasy) {
					intv = 30;
					intvmax = 48;
				} else if (mc.gameLevel == mc.gameLevelNormal) {
					intv = 21;
					intvmax = 40;
				} else if (mc.gameLevel == mc.gameLevelHard) {
					intv = 10;
					intvmax = 40;
				}
				//shot bullet
				if (bcnt % intv == 0) {
					//shot bullet
					float bxl;
					float byl;
					float bxr;
					float byr;
					float bx;
					float by;
					const float bspd = 0.71f;
					bxl = Mathf.Cos ((cdir + 46.0f) * Mathf.Deg2Rad) * 1.0f;
					byl = Mathf.Sin ((cdir + 46.0f) * Mathf.Deg2Rad) * 1.0f;
					bxr = Mathf.Cos ((cdir - 46.0f) * Mathf.Deg2Rad) * 1.0f;
					byr = Mathf.Sin ((cdir - 46.0f) * Mathf.Deg2Rad) * 1.0f;
					mc.generateEnemyBullet120 (cdir, bspd, cashTransform.position.x, cashTransform.position.y, bxl * 0.40f, byl * 0.40f);
					mc.generateEnemyBullet120 (cdir, bspd, cashTransform.position.x, cashTransform.position.y, bxr * 0.40f, byr * 0.40f);
				}
				bcnt++;
				if (bcnt > intvmax) {
					bcnt = 0;
					mvt = 25;
					mvseq--;
				}
				break;
			default:
				break;
			}
			//move result process
/* なし
			if ( (cashTransform.position.y > ymax) ||
				(cashTransform.position.y < ymin) ||
				(cashTransform.position.x < xmin) ||
				(cashTransform.position.x > xmax) ){
				//objnum dec
				mc.decObj();
				//delete this object
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
				mc.generatePowerup100( item, cashTransform.position.x, cashTransform.position.y );
			}
			//objnum dec
			if (incobj == true) {
				mc.decObj ();
				incobj = false;
			} else {
				#if UNITY_EDITOR
				Debug.Log ("no inc dec enemy110");
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
	public void setInitStatus( float trgdir, float dspdir, int itm, float px, float py ){	//direction,item,posx/y set
		this.cdir = trgdir;
		this.tdir = trgdir;
		this.ddir = dspdir;
		this.item = itm;
		this.posx = px;
		this.posy = py;
	}

}
