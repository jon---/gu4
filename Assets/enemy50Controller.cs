using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy50Controller : MonoBehaviour {

	//private
	//local const
	//x,y min/max
	const float xmin = -5.5f;	//基本4.0だが横スクロール補正で消えてしまう対策で+1.5f
	const float xmax = 5.5f;
	const float ymin = -6.0f;
	const float ymax = 6.0f;
	//x,y speed base
	const float xspdbase = 0.10f;
	const float yspdbase = 0.10f;
	//base hit point
	const int basehitpoint = 5;
	//score
	readonly int hitscore = 10;
	readonly int score = 1300;

	//system local
	int intervalCnt;	//interval counter

	//component cash
	Transform cashTransform;
	GameObject mainCtr;
	mainController mc;

	//local
	//move seq
	int mvseq;

	//pos x,y
	float posx;
	float posy;

	//move speed
	float xx;
	float yy;

	//direction
	float dir;

	//move speed
	float mspd;

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

		//pos x,y
		cashTransform.position = new Vector3( posx, posy, 0.0f );

		//rotate
		cashTransform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, 0.0f));

		//move seq mode
		mvseq = 0;

		//move speed
		xx = 0.0f;
		yy = -1.0f;

		//direction
		//(set from parent objects)
		//		dir = 270.0f;

		//mspd
		//(set from parent objects)
		//		mspd = 1.0f;

		//rotate
		this.cashTransform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, this.dir));

		//start pos adjust
		cashTransform.Translate((xx*mspd*xspdbase*6.0f), (yy*mspd*yspdbase*6.0f), 0);

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

			//move process
			//move
			//for scroll x move
			cashTransform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, 0.0f));
			cashTransform.Translate (mc.getMapxMov (), 0, 0);
			//move
			if ((Mathf.Abs(xx)) <= 0.12f) {
				xx = xx * 1.02f;
				yy = yy * 1.02f;
			}
			cashTransform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, this.dir));
			cashTransform.Translate((xx*mspd*xspdbase), (yy*mspd*yspdbase), 0);

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
					Debug.Log ("no inc dec enemy50");
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
		} else if (cotag == "wipe2") {
			//collision wipe 2
			mc.generateEnemyDamageEffect( cashTransform.position.x, cashTransform.position.y );
			this.enemyHit( mc.damageWipe2 );
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
			if (damage >= mc.damageBig) {
				mc.generatePowerup100 (mc.puType_score, cashTransform.position.x, cashTransform.position.y);
			}
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
				Debug.Log ("no inc dec enemy50");
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
	public void setInitStatus( int itm, float direction, float spd, float px, float py ){	//item,posx/y set
		direction = direction - 270.0f;
		if (direction <= 0.0f) {
			direction = direction + 360.0f;
		}
		this.dir = direction;
		this.mspd = spd;
		this.item = itm;
		this.posx = px;
		this.posy = py;
	}

}
