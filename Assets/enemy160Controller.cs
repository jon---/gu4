using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy160Controller : MonoBehaviour {

	//public
	public GameObject enemy160ControllerPrefab;

	public Sprite enemy160;	//enemy160
	public Sprite enemy161;	//enemy161

	//private
	//local const
	//x,y min/max
	const float xmin = -3.1f;	//反射位置
	const float xmax = 3.1f;	//反射位置
	const float ymin = -7.0f;
	const float ymax = 7.0f;
	//x,y speed base
	const float xspdbase = 0.045f;
	const float yspdbase = 0.045f;
	//base hit point
	const int basehitpoint1 = 25;//21;	//type0
	const int basehitpoint2 = 16;//12;	//type1
	const int basehitpoint3 = 9;//5;	//type2,3,4
	//score
	readonly int hitscore = 10;
	readonly int score = 300;

	//system local
	int intervalCnt;	//interval counter

	//component cash
	Transform cashTransform;
	SpriteRenderer sr;
	GameObject mainCtr;
	mainController mc;
	GameObject playerCtr;
	playerController plc;

	//local
	//type
	int type;

	//pos x,y
	float posx;
	float posy;

	//move speed
	float xx;
	float yy;

	//current direction
	float cdir;

	//display direction
	float ddir;
	float dd;

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

		//maincontroller
		mainCtr = GameObject.Find ("mainController");
		mc = mainCtr.GetComponent<mainController> ();

		//playercontroller
		playerCtr = GameObject.Find ("playerController");
		plc = playerCtr.GetComponent<playerController> ();

		//pos x,y
		cashTransform.position = new Vector3( posx, posy, 0.0f );

		//current direction
		//(set from parent objects)
//		cdir = 0.0f;

		//mov x,y
		xx = Mathf.Cos( (cdir * Mathf.Deg2Rad) ) * 1.0f * xspdbase;
		yy = Mathf.Sin( (cdir * Mathf.Deg2Rad) ) * 1.0f * yspdbase;

		//sprite
		if ( (type == 0) || (type == 1) || (type == 2)) {
			sr.sprite = enemy160;
		} else if ( (type == 3) || (type == 4) ) {
			sr.sprite = enemy161;
		}

		//scale
		if ((type == 0)) {
			cashTransform.localScale = new Vector3 (3.7f, 3.7f, 1.0f);
		} else if ((type == 1)) {
			cashTransform.localScale = new Vector3 (2.9f, 2.9f, 1.0f);
		} else if ((type == 2) || (type == 3)) {
			cashTransform.localScale = new Vector3 (2.6f, 2.6f, 1.0f);
		} else if ((type == 4)) {
			cashTransform.localScale = new Vector3 (2.3f, 2.3f, 1.0f);
		}

		//display direction
		ddir = Random.Range( 0.0f, 360.0f );
		dd = -0.7f;
		int r = Random.Range (0, 2);
		if (r == 0) {
			dd = dd - (dd * 2);
		}
		dd = dd + Random.Range (-0.51f, +0.51f); 

		//item
		//(set from parent objects)
//		item = -1;

		//enemy inital hitpoint
		if ( (type == 0) ) {
			eHpIntial = basehitpoint1 + 0;
		} else if ( (type == 1) ) {
			eHpIntial = basehitpoint2 + 0;
		} else if ( (type == 2) || (type == 3) || (type == 4) ) {
			eHpIntial = basehitpoint3 + 0;
		}

		//level adjust
		if (mc.gameLevel == mc.gameLevelEasy) {
			eHpIntial = eHpIntial - 1;
		} else if (mc.gameLevel == mc.gameLevelNormal) {
			eHpIntial = eHpIntial + 2;
		} else if (mc.gameLevel == mc.gameLevelHard) {
			eHpIntial = eHpIntial + 4;
		}

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
			cashTransform.Translate (xx, yy, 0);
			//display rotate
			ddir = ddir + dd;
			if (ddir <= 0.0f) {
				ddir = ddir + 360.0f;
			}
			if (ddir >= 360.0f) {
				ddir = ddir - 360.0f;
			}
			cashTransform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, (ddir)));

			//move result process
			if (cashTransform.position.x > xmax){
				Vector3 pos = cashTransform.position;
				pos.x = xmax;
				cashTransform.position = pos;
				xx = xx - (xx * 2);
			}
			if (cashTransform.position.x < xmin){
				Vector3 pos = cashTransform.position;
				pos.x = xmin;
				cashTransform.position = pos;
				xx = xx - (xx * 2);
			}
			if ( (cashTransform.position.y > ymax) ||
				(cashTransform.position.y < ymin) ) {
				if (alreadydelete == true) {
					return;
				}
				//objnum dec
				if (incobj == true) {
					mc.decObj ();
					incobj = false;
				} else {
					#if UNITY_EDITOR
					Debug.Log ("no inc dec enemy160");
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
			if ((type == 0) || (type == 1)) {
				if (damage < mc.damageBig) {
					//generate small enemy160
					this.generateSmallEnemy ();
				}
			} else {
				//add game score
				mc.addGameScore (this.score);
				//generate power up item
				if (this.item != mc.puType_None) {
					mc.generatePowerup100 (item, cashTransform.position.x, cashTransform.position.y);
				}
			}
			//generate explosion middle effect 
			mc.generateExplosionMiddleEffect ((cashTransform.position.x), (cashTransform.position.y));
			//objnum dec
			if (incobj == true) {
				mc.decObj ();
				incobj = false;
			} else {
				#if UNITY_EDITOR
				Debug.Log ("no inc dec enemy160");
				#endif
			}
			//destroy this
			alreadydelete = true;
			Destroy (gameObject);
		}
		//add game score
		mc.addGameScore( this.hitscore );
	}

	//generate small enemy
	private void generateSmallEnemy(){
		GameObject go;
		if (type == 0) {
			go = Instantiate (enemy160ControllerPrefab) as GameObject;
			go.GetComponent<enemy160Controller>().setInitStatus (-1, cashTransform.position.x, cashTransform.position.y, Random.Range (210.0f, 330.0f), 1);
			go = Instantiate (enemy160ControllerPrefab) as GameObject;
			go.GetComponent<enemy160Controller>().setInitStatus (-1, cashTransform.position.x, cashTransform.position.y, Random.Range (210.0f, 330.0f), 1);
			go = Instantiate (enemy160ControllerPrefab) as GameObject;
			go.GetComponent<enemy160Controller>().setInitStatus (-1, cashTransform.position.x, cashTransform.position.y, Random.Range (210.0f, 330.0f), 1);
		} else if (type == 1) {
			go = Instantiate (enemy160ControllerPrefab) as GameObject;
			go.GetComponent<enemy160Controller>().setInitStatus (-1, cashTransform.position.x, cashTransform.position.y, Random.Range (210.0f, 330.0f), 2);
			go = Instantiate (enemy160ControllerPrefab) as GameObject;
			go.GetComponent<enemy160Controller>().setInitStatus (-1, cashTransform.position.x, cashTransform.position.y, Random.Range (210.0f, 330.0f), 3);
			go = Instantiate (enemy160ControllerPrefab) as GameObject;
			go.GetComponent<enemy160Controller>().setInitStatus (-1, cashTransform.position.x, cashTransform.position.y, Random.Range (210.0f, 330.0f), 4);
		}
	}

	//public
	public void setInitStatus( int itm, float px, float py, float dir, int type ){	//item,posx/y,dir,type set
		this.item = itm;
		this.posx = px;
		this.posy = py;
		this.cdir = dir;
		this.type = type;
	}

}
