using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy200Controller : MonoBehaviour {
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
	const float yspdbase = 0.09f;
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

	//atack time
	int stcnt;

	//bullet cnt
	int bcnt;

	//bullet info
	float ox1;	//b1offsetx
	float oy1;	//b1offsety
	float ox2;	//b2offsetx
	float oy2;	//b2offsety
	float ox3;	//b3offsetx
	float oy3;	//b3offsety
	float ox4;	//b4offsetx
	float oy4;	//b4offsety
	float sx1;	//b1spped
	float sy1;	//b1spped
	float sx2;	//b2spped
	float sy2;	//b2spped
	float sx3;	//b3spped
	float sy3;	//b3spped
	float sx4;	//b4spped
	float sy4;	//b4spped
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

		//animator
		animt = GetComponent<Animator>();
		animt.speed = 4.0f;

		//pos x,y
		//(set from parent objects)
		cashTransform.position = new Vector3( posx, posy, 0.0f );

		//move seq mode
		mvseq = 0;

		//atack time
		stcnt = 0;

		//bullet cnt
		bcnt = 0;

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

			//move and atack process
			switch (mvseq) {
			case 0:
				//move forward (to player) and atack
				//for scroll x move
				cashTransform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, 0.0f));
				cashTransform.Translate (mc.getMapxMov (), 0, 0);
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
				const float dspd = 1.3f;
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
				const float dstns = 3.8f;
				if ((Mathf.Abs (xdistance) <= dstns) && (Mathf.Abs (ydistance) <= dstns) &&
				    ((cashTransform.position.y <= 4.2f) && (cashTransform.position.y >= -4.2f)) &&
				    ((cashTransform.position.x <= 2.3f) && (cashTransform.position.x >= -2.3f))) {
					mvseq++;
				}
				//atack
				//shot
				if (mc.gameLevel != mc.gameLevelEasy) {
					if (stcnt == 12) {
						ox1 = Mathf.Cos ((cdir - 30.0f) * Mathf.Deg2Rad) * 0.67f;
						oy1 = Mathf.Sin ((cdir - 30.0f) * Mathf.Deg2Rad) * 0.67f;
						ox2 = Mathf.Cos ((cdir - 24.5f) * Mathf.Deg2Rad) * 0.57f;
						oy2 = Mathf.Sin ((cdir - 24.5f) * Mathf.Deg2Rad) * 0.57f;
						ox3 = Mathf.Cos ((cdir + 24.5f) * Mathf.Deg2Rad) * 0.57f;
						oy3 = Mathf.Sin ((cdir + 24.5f) * Mathf.Deg2Rad) * 0.57f;
						ox4 = Mathf.Cos ((cdir + 30.0f) * Mathf.Deg2Rad) * 0.67f;
						oy4 = Mathf.Sin ((cdir + 30.0f) * Mathf.Deg2Rad) * 0.67f;
						mc.generateEnemyBullet100 (0, cashTransform.position.x, cashTransform.position.y, ox1 * 1.2f, oy1 * 1.2f);
						mc.generateEnemyBullet100 (0, cashTransform.position.x, cashTransform.position.y, ox2 * 1.2f, oy2 * 1.2f);
						mc.generateEnemyBullet100 (0, cashTransform.position.x, cashTransform.position.y, ox3 * 1.2f, oy3 * 1.2f);
						mc.generateEnemyBullet100 (0, cashTransform.position.x, cashTransform.position.y, ox4 * 1.2f, oy4 * 1.2f);
					}
				}
				stcnt++;
				break;
			case 1:
				//atack
				//for scroll x move
				cashTransform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, 0.0f));
				cashTransform.Translate(mc.getMapxMov(), 0, 0);
				//rotate
				cashTransform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, (cdir + doffset)));
				//adjust at game level
				float intvmax = 0;
				if (mc.gameLevel == mc.gameLevelEasy) {
					intvmax = 18;
				} else if (mc.gameLevel == mc.gameLevelNormal) {
					intvmax = 16;
				} else if (mc.gameLevel == mc.gameLevelHard) {
					intvmax = 14;
				}
				//shot bullet
				//atack
				//shot
				if ((bcnt == 5) || (bcnt == 15)) {
					ox1 = Mathf.Cos ((cdir - 20.0f) * Mathf.Deg2Rad) * 0.67f;
					oy1 = Mathf.Sin ((cdir - 20.0f) * Mathf.Deg2Rad) * 0.67f;
					if (mc.gameLevel != mc.gameLevelEasy) {
						ox2 = Mathf.Cos ((cdir - 4.5f) * Mathf.Deg2Rad) * 0.57f;
						oy2 = Mathf.Sin ((cdir - 4.5f) * Mathf.Deg2Rad) * 0.57f;
						ox3 = Mathf.Cos ((cdir + 4.5f) * Mathf.Deg2Rad) * 0.57f;
						oy3 = Mathf.Sin ((cdir + 4.5f) * Mathf.Deg2Rad) * 0.57f;
						if (mc.gameLevel == mc.gameLevelHard) {
							ox4 = Mathf.Cos ((cdir + 20.0f) * Mathf.Deg2Rad) * 0.67f;
							oy4 = Mathf.Sin ((cdir + 20.0f) * Mathf.Deg2Rad) * 0.67f;
						}
					}
					if (bcnt == 5) {
						dir1 = 11.0f;
						dir2 = 2.5f;
						sp1 = 0.50f;
						sp2 = 0.65f;
						sx1 = Mathf.Cos ((cdir - dir1) * Mathf.Deg2Rad) * 1.0f;
						sy1 = Mathf.Sin ((cdir - dir1) * Mathf.Deg2Rad) * 1.0f;
						if (mc.gameLevel != mc.gameLevelEasy) {
							sx2 = Mathf.Cos ((cdir - dir2) * Mathf.Deg2Rad) * 1.0f;
							sy2 = Mathf.Sin ((cdir - dir2) * Mathf.Deg2Rad) * 1.0f;
							sx3 = Mathf.Cos ((cdir + dir2) * Mathf.Deg2Rad) * 1.0f;
							sy3 = Mathf.Sin ((cdir + dir2) * Mathf.Deg2Rad) * 1.0f;
						}
						sx4 = Mathf.Cos ((cdir + dir1) * Mathf.Deg2Rad) * 1.0f;
						sy4 = Mathf.Sin ((cdir + dir1) * Mathf.Deg2Rad) * 1.0f;
					} else {
						dir1 = 14.0f;
						dir2 = 5.5f;
						sp1 = 0.70f;
						sp2 = 0.90f;
						sx1 = Mathf.Cos ((cdir - dir1) * Mathf.Deg2Rad) * 1.0f;
						sy1 = Mathf.Sin ((cdir - dir1) * Mathf.Deg2Rad) * 1.0f;
						if (mc.gameLevel != mc.gameLevelEasy) {
							sx2 = Mathf.Cos ((cdir - dir2) * Mathf.Deg2Rad) * 1.0f;
							sy2 = Mathf.Sin ((cdir - dir2) * Mathf.Deg2Rad) * 1.0f;
							sx3 = Mathf.Cos ((cdir + dir2) * Mathf.Deg2Rad) * 1.0f;
							sy3 = Mathf.Sin ((cdir + dir2) * Mathf.Deg2Rad) * 1.0f;
						}
						sx4 = Mathf.Cos ((cdir + dir1) * Mathf.Deg2Rad) * 1.0f;
						sy4 = Mathf.Sin ((cdir + dir1) * Mathf.Deg2Rad) * 1.0f;
					}
					mc.generateEnemyBullet100 (1, cashTransform.position.x, cashTransform.position.y, ox1, oy1, sx1 * sp1, sy1 * sp1);
					mc.generateEnemyBullet100 (1, cashTransform.position.x, cashTransform.position.y, ox1, oy1, sx4 * sp2, sy4 * sp2);
					if (mc.gameLevel != mc.gameLevelEasy) {
						mc.generateEnemyBullet100 (1, cashTransform.position.x, cashTransform.position.y, ox2, oy2, sx2 * sp1, sy2 * sp1);
						mc.generateEnemyBullet100 (1, cashTransform.position.x, cashTransform.position.y, ox2, oy2, sx3 * sp2, sy3 * sp2);
						mc.generateEnemyBullet100 (1, cashTransform.position.x, cashTransform.position.y, ox3, oy3, sx4 * sp1, sy4 * sp1);
						mc.generateEnemyBullet100 (1, cashTransform.position.x, cashTransform.position.y, ox3, oy3, sx1 * sp2, sy1 * sp2);
						if (mc.gameLevel == mc.gameLevelHard) {
							mc.generateEnemyBullet100 (1, cashTransform.position.x, cashTransform.position.y, ox4, oy4, sx3 * sp1, sy3 * sp1);
							mc.generateEnemyBullet100 (1, cashTransform.position.x, cashTransform.position.y, ox4, oy4, sx2 * sp2, sy2 * sp2);
						}
					}

				}
				bcnt++;
				if (bcnt > intvmax) {
					mvseq++;
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
				yy = yy - 0.0015f;
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
					Debug.Log ("no inc dec enemy200");
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
				Debug.Log ("no inc dec enemy200");
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
