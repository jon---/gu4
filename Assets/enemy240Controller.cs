using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy240Controller : MonoBehaviour {
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
	const float yspdbase = 0.05f;//0.12f
	//base hit point
	const int basehitpoint = 96;
	//score
	readonly int hitscore = 40;
	readonly int score = 2400;

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

	//target pos
	float tposx;
	float tposy;

	//shot seq
	int stseq;

	//bullet cnt
	int bcnt;

	//current direction
	float cdir;

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

		//shot seq
		stseq = 0;

		//bullet cnt
		bcnt= 0;

		//current direction
		cdir = 270;

		//move speed
		xx = xspdbase;
		yy = yspdbase * -1;

		//
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
				//move forward and atack
				//for scroll x move
				cashTransform.Translate (mc.getMapxMov (), 0, 0);
				//direction to player
				//move
				cashTransform.Translate (0, yy, 0);

				//atack
				//shot
				//shot bullet
				//atack
				//shot
				if ((cashTransform.position.y <= 5.8f) && (stseq == 0)) {
					stseq++;
				}
				if( stseq == 1){
					int bint = 0;
					//adjust at game level
					if (mc.gameLevel == mc.gameLevelEasy) {
						bint = 8;
					} else if (mc.gameLevel == mc.gameLevelNormal) {
						bint = 14;
					} else if (mc.gameLevel == mc.gameLevelHard) {
						bint = 24;
					}
					const float bspd1 = 0.60f;
					const float bspd2 = 0.64f;
					const float yoffset = -0.53f;
					if (mc.gameLevel != mc.gameLevelEasy) {
						if ((bcnt % 30 == 0) || (bcnt % 98 == 0)) {
							mc.generateEnemyBullet110 (0, cashTransform.position.x, cashTransform.position.y, 0.0f, yoffset, bspd1, bspd1);
						}
					}
					if ( (bcnt % 4 == 0) && (bcnt % 64 <= bint) ) {
						float bx1 = Mathf.Cos ((cdir + 0) * Mathf.Deg2Rad) * bspd1;
						float by1 = Mathf.Sin ((cdir + 0) * Mathf.Deg2Rad) * bspd1;
						float bx2 = Mathf.Cos ((cdir - 20) * Mathf.Deg2Rad) * bspd2;
						float by2 = Mathf.Sin ((cdir - 20) * Mathf.Deg2Rad) * bspd2;
						float bx3 = Mathf.Cos ((cdir + 20) * Mathf.Deg2Rad) * bspd2;
						float by3 = Mathf.Sin ((cdir + 20) * Mathf.Deg2Rad) * bspd2;
						mc.generateEnemyBullet110 (1, cashTransform.position.x, cashTransform.position.y, 0.0f, yoffset, bx1, by1);
						mc.generateEnemyBullet110 (1, cashTransform.position.x, cashTransform.position.y, 0.0f, yoffset, bx2, by2);
						mc.generateEnemyBullet110 (1, cashTransform.position.x, cashTransform.position.y, 0.0f, yoffset, bx3, by3);
						cdir = cdir + 2;
						if (cdir >= 285) {
							cdir = 255;
						}
					}
				}
				bcnt++;
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
					Debug.Log ("no inc dec enemy240");
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
				Debug.Log ("no inc dec enemy240");
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
	public void setInitStatus( int itm, float px, float py ){	//direction,item,posx/y set
		this.item = itm;	
		this.posx = px;
		this.posy = py;
	}

}
