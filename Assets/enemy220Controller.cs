using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy220Controller : MonoBehaviour {
	//public

	//private
	//local const
	//x,y min/max
	const float xmin = -6.0f;	//基本4.5だが横スクロール補正で消えてしまう対策で+1.5f
	const float xmax = 6.0f;
	const float ymin = -6.5f;
	const float ymax = 6.5f;
	//x,y speed base
	const float xspdbase = 0.00f;
	const float yspdbase = 0.10f;//0.12f
	//base hit point
	const int basehitpoint = 55;
	//score
	readonly int hitscore = 30;
	readonly int score = 2100;

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
	int mvseqcnt;

	//pos x,y
	float posx;
	float posy;

	//move speed
	float xx;
	float yy;

	//target pos
	float tposx;
	float tposy;

	//atack time
	int stcnt;

	//bullet cnt
	int bcnt;

	//bullet info
	float sx1;	//b1speed
	float sy1;	//b1speed
	float sx2;	//b2speed
	float sy2;	//b2speed
	float sx3;	//b3speed
	float sy3;	//b3speed
	float sx4;	//b4speed
	float sy4;	//b4speed
	float dir1;	//b direction1 
	float dir2;	//b direction2
	float sp1;	//b speed base1
	float sp2;	//b speed base2

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

		//target pos
		tposx = 0.0f;
		tposy = 0.0f;

		//move seq mode
		mvseq = 0;
		mvseqcnt = 0;

		//atack time
		stcnt = 0;

		//bullet cnt
		bcnt= 0 ;

		//move speed
		xx = xspdbase;// * Random.Range( -1.5f, 1.5f );
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
				//move forward (to player) and atack
				//for scroll x move
				cashTransform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, 0.0f));
				cashTransform.Translate (mc.getMapxMov (), 0, 0);
				//direction to player
				Vector2 ppos = plc.getPlayerPos ();
				if (mvseqcnt == 0) {
					tposx = ppos.x;
					tposy = ppos.y;
				}
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
				const float dspd = 5.3f;
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
				cashTransform.Translate (0, yy, 0);

				//move seq change position?
				if (((cashTransform.position.y <= (tposy + 1.0f)) && (cashTransform.position.y >= (tposy - 1.0f))) &&
				    ((cashTransform.position.x <= (tposx + 1.0f)) && (cashTransform.position.x >= (tposx - 1.0f)))) {
					bcnt = 0;
					mvseqcnt = 0;
					mvseq++;
				}
				//atack
				//shot
				//adjust at game level
				sp1 = 0.0f;
				sp2 = 0.0f;
				if (mc.gameLevel == mc.gameLevelEasy) {
					sp1 = 0.0f;
					sp2 = 0.0f;
				} else if (mc.gameLevel == mc.gameLevelNormal) {
					sp1 = 0.40f;
					sp2 = 0.55f;
				} else if (mc.gameLevel == mc.gameLevelHard) {
					sp1 = 0.55f;
					sp2 = 0.70f;
				}
				//shot bullet
				//atack
				//shot
				if (mc.gameLevel != mc.gameLevelEasy) {
					if ((bcnt == 5) || (bcnt == 15) || ((bcnt == 25)&&(mc.gameLevel == mc.gameLevelHard)) ) {
						if (bcnt == 5) {
							dir1 = 31.0f;
							dir2 = 12.5f;
							sp1 = 0.50f;
							sp2 = 0.65f;
							sx1 = Mathf.Cos ((cdir - dir1) * Mathf.Deg2Rad) * 1.0f;
							sy1 = Mathf.Sin ((cdir - dir1) * Mathf.Deg2Rad) * 1.0f;
							sx2 = Mathf.Cos ((cdir - dir2) * Mathf.Deg2Rad) * 1.0f;
							sy2 = Mathf.Sin ((cdir - dir2) * Mathf.Deg2Rad) * 1.0f;
							sx3 = Mathf.Cos ((cdir + dir2) * Mathf.Deg2Rad) * 1.0f;
							sy3 = Mathf.Sin ((cdir + dir2) * Mathf.Deg2Rad) * 1.0f;
							sx4 = Mathf.Cos ((cdir + dir1) * Mathf.Deg2Rad) * 1.0f;
							sy4 = Mathf.Sin ((cdir + dir1) * Mathf.Deg2Rad) * 1.0f;
						} else {
							dir1 = 44.0f;
							dir2 = 25.5f;
							sp1 = sp1*1.1f;
							sp2 = sp2*1.1f;
							sx1 = Mathf.Cos ((cdir - dir1) * Mathf.Deg2Rad) * 1.0f;
							sy1 = Mathf.Sin ((cdir - dir1) * Mathf.Deg2Rad) * 1.0f;
							sx2 = Mathf.Cos ((cdir - dir2) * Mathf.Deg2Rad) * 1.0f;
							sy2 = Mathf.Sin ((cdir - dir2) * Mathf.Deg2Rad) * 1.0f;
							sx3 = Mathf.Cos ((cdir + dir2) * Mathf.Deg2Rad) * 1.0f;
							sy3 = Mathf.Sin ((cdir + dir2) * Mathf.Deg2Rad) * 1.0f;
							sx4 = Mathf.Cos ((cdir + dir1) * Mathf.Deg2Rad) * 1.0f;
							sy4 = Mathf.Sin ((cdir + dir1) * Mathf.Deg2Rad) * 1.0f;
						}
						mc.generateEnemyBullet100 (1, cashTransform.position.x, cashTransform.position.y, 0.0f, 0.0f, sx1 * sp1, sy1 * sp1);
						mc.generateEnemyBullet100 (1, cashTransform.position.x, cashTransform.position.y, 0.0f, 0.0f, sx1 * sp2, sy1 * sp2);
						mc.generateEnemyBullet100 (1, cashTransform.position.x, cashTransform.position.y, 0.0f, 0.0f, sx2 * sp1, sy2 * sp1);
						mc.generateEnemyBullet100 (1, cashTransform.position.x, cashTransform.position.y, 0.0f, 0.0f, sx2 * sp2, sy2 * sp2);
						mc.generateEnemyBullet100 (1, cashTransform.position.x, cashTransform.position.y, 0.0f, 0.0f, sx3 * sp1, sy3 * sp1);
						mc.generateEnemyBullet100 (1, cashTransform.position.x, cashTransform.position.y, 0.0f, 0.0f, sx3 * sp2, sy3 * sp2);
						mc.generateEnemyBullet100 (1, cashTransform.position.x, cashTransform.position.y, 0.0f, 0.0f, sx4 * sp1, sy4 * sp1);
						mc.generateEnemyBullet100 (1, cashTransform.position.x, cashTransform.position.y, 0.0f, 0.0f, sx4 * sp2, sy4 * sp2);
	
					}
				}
				bcnt++;
				mvseqcnt++;
				stcnt++;
				break;
			case 1:
				//atack
				//for scroll x move
				cashTransform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, 0.0f));
				cashTransform.Translate (mc.getMapxMov (), 0, 0);
				//rotate
				cashTransform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, (cdir + doffset)));
				//adjust at game level
				int dd = 0;
				if (mc.gameLevel == mc.gameLevelEasy) {
					dd = 180;
					sp1 = 0.65f;
					sp2 = 0.70f;
				} else if (mc.gameLevel == mc.gameLevelNormal) {
					dd = 45;
					sp1 = 0.70f;
					sp2 = 0.90f;
				} else if (mc.gameLevel == mc.gameLevelHard) {
					dd = 12;
					sp1 = 0.80f;
					sp2 = 1.50f;
				}
				//shot bullet
				//atack
				//shot
				mvseqcnt++;
				if (mvseqcnt % 2 == 0) {
					dir1 = 44.0f;
					dir2 = 25.5f;
					sx1 = Mathf.Cos ((bcnt - dir1) * Mathf.Deg2Rad) * 1.0f;
					sy1 = Mathf.Sin ((bcnt - dir1) * Mathf.Deg2Rad) * 1.0f;
					sx2 = Mathf.Cos ((bcnt - dir2) * Mathf.Deg2Rad) * 1.0f;
					sy2 = Mathf.Sin ((bcnt - dir2) * Mathf.Deg2Rad) * 1.0f;
					mc.generateEnemyBullet100 (1, cashTransform.position.x, cashTransform.position.y, 0.0f, 0.0f, sx1 * sp1, sy1 * sp1);
					mc.generateEnemyBullet100 (1, cashTransform.position.x, cashTransform.position.y, 0.0f, 0.0f, sx2 * sp1, sy2 * sp1);
					mc.generateEnemyBullet100 (1, cashTransform.position.x, cashTransform.position.y, 0.0f, 0.0f, sx1 * sp2, sy1 * sp2);
					mc.generateEnemyBullet100 (1, cashTransform.position.x, cashTransform.position.y, 0.0f, 0.0f, sx2 * sp2, sy2 * sp2);
					bcnt = bcnt + dd;
					if (bcnt >= 360) {
						mvseqcnt = 0;
						bcnt = 0;
						mvseq = 2;
					}
				}
				break;
			case 2:
				//move back
				//for scroll x move
				cashTransform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, 0.0f));
				cashTransform.Translate(mc.getMapxMov(), 0, 0);
				//rotate
				cashTransform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, (cdir + doffset)));
				//move
				cashTransform.Translate (0, (yy*-1), 0);
				//y acceleration
				yy = yy - 0.0011f;//0.0015
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
					Debug.Log ("no inc dec enemy220");
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
					mc.generateCounterBullet( 0, cashTransform.position.x, cashTransform.position.y, -0.3f, -0.3f);
					mc.generateCounterBullet( 0, cashTransform.position.x, cashTransform.position.y, +0.3f, +0.3f);
				}
			}
			//add game score
			mc.addGameScore( this.score );
			//generate explosion middle effect 
			mc.generateExplosionMiddleEffect( (cashTransform.position.x-0.5f), (cashTransform.position.y+0.3f) );
			mc.generateExplosionMiddleEffect( (cashTransform.position.x), (cashTransform.position.y) );
			mc.generateExplosionMiddleEffect( (cashTransform.position.x+0.5f), (cashTransform.position.y+0.3f) );
			//generate power up item(score)
			mc.generatePowerup100( mc.puType_score, cashTransform.position.x, cashTransform.position.y );
			mc.generatePowerup100( mc.puType_score, cashTransform.position.x+0.3f, cashTransform.position.y+0.3f );
			mc.generatePowerup100( mc.puType_score, cashTransform.position.x+0.3f, cashTransform.position.y-0.3f );
//			mc.generatePowerup100( mc.puType_score, cashTransform.position.x-0.3f, cashTransform.position.y+0.3f );
//			mc.generatePowerup100( mc.puType_score, cashTransform.position.x-0.3f, cashTransform.position.y-0.3f );
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
				Debug.Log ("no inc dec enemy220");
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
