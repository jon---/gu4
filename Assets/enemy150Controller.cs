using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy150Controller : MonoBehaviour {

	//public
	public Sprite enemy150;	//enemy150 main
	public Sprite enemy151;	//enemy150 warp
	public Sprite enemy152;	//enemy150 type2

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
	//base hit point
	const int basehitpoint = 8;
	//score
	readonly int hitscore = 20;
	readonly int score = 600;

	//system local
	int intervalCnt;	//interval counter

	//component cash
	Transform cashTransform;
	SpriteRenderer sr;
	GameObject mainCtr;
	mainController mc;
//	GameObject playerCtr;
//	playerController plc;

	//local
	//type
	int type;
	//move seq
	int mvseq;

	//pos x,y
	float posx;
	float posy;

	//move speed
	float xx;
	float yy;

	//current direction
	float cdir;
	float dd;

	//warp
	int warpcnt;
	float warpcol;

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
//		playerCtr = GameObject.Find ("playerController");
//		plc = playerCtr.GetComponent<playerController> ();

		//pos x,y
		cashTransform.position = new Vector3( posx, posy, 0.0f );

		//rotate
		cashTransform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, 0.0f));

		//type
		//(set from parent objects)
//		type = 0;

		//move seq mode
		mvseq = 0;

		//move speed
		xx = xspdbase * 1.0f;
		yy = yspdbase * 1.0f;

		//current direction
		cdir = 0.0f;
		dd = 1.5f;

		//warp
		warpcnt = 0;
		warpcol = 0.3f;

		//sprite
		sr.sprite = enemy151;

		//color
		sr.color = new Color( 0.3f, 0.3f, 0.3f, 0.0f);

		//item
		//(set from parent objects)
//		item = -1;

		//enemy inital hitpoint
		eHpIntial = basehitpoint + 0;

		//enemy hitpoint
		eHp = eHpIntial;

		//tag
		this.tag = "unavailableEnemy";

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
			switch (mvseq) {
			case 0:
				//warp appearance
				//for scroll x move
				cashTransform.Translate (mc.getMapxMov (), 0, 0);
				//warp
				//color
				warpcol = warpcol + (5.5f / 255.0f);
				if (warpcol >= (255.0f / 255.0f)) {
					warpcol = 255.0f / 255.0f;
				}
				Color cr = sr.color;
				if ((warpcnt % 2) == 0) {
					cr.a = 1.0f;
					if (warpcnt >= 60) {
						sr.sprite = enemy151;
					}
				} else {
					cr.a = 0.35f + (warpcnt*0.003f);
					if (warpcnt >= 60) {
						if (type == 0) {
							sr.sprite = enemy150;
						} else {
							sr.sprite = enemy152;
						}
					}
				}
				cr.r = warpcol;
				cr.g = warpcol;
				cr.b = warpcol;
				sr.color = cr;
				//cnt
				warpcnt++;
				if (warpcnt >= 130) {
					sr.color = new Color( 1.0f, 1.0f, 1.0f, 1.0f);
					if (type == 0) {
						sr.sprite = enemy150;
					} else {
						sr.sprite = enemy152;
					}
					mvseq = 1;
					//tag
					this.tag = "enemy";
				}
				break;
			case 1:
				//move
				//for scroll x move
				cashTransform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, 0.0f));
				cashTransform.Translate (mc.getMapxMov (), 0, 0);
				//move
				if (yy > -0.06f) {
					yy = yy - 0.014f;
				}
				if (yy <= -0.06f) {
					yy = yy - 0.0015f;
				}
				cashTransform.Translate (xx, yy, 0);
				//display rotate
				cdir = cdir - dd;
				if (cdir <= 0.0f) {
					cdir = cdir + 360.0f;
				}
				if (dd < 50.0f) {
					dd = dd + 0.7f;
				}
				cashTransform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, (cdir)));
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
					Debug.Log ("no inc dec enemy150");
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
		if (cotag == "wipe2") {
			//collision wipe 2
			mc.generateEnemyDamageEffect (cashTransform.position.x, cashTransform.position.y);
			this.enemyHit( mc.damageWipe2 );
			return;
		}
		if (this.tag == "unavailableEnemy") {
			return;
		}
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
			if (damage < mc.damageBig) {
				//adjust at game level
				int blint = 0;
				if (mc.gameLevel == mc.gameLevelEasy) {
					blint = 180;
				} else if (mc.gameLevel == mc.gameLevelNormal) {
					blint = 40;
				} else if (mc.gameLevel == mc.gameLevelHard) {
					blint = 20;
					//shot counter bullet
					if (damage < mc.damageBig) {
						mc.generateCounterBullet (0, cashTransform.position.x, cashTransform.position.y, 0, 0);
					}
				}
				//type
				if (type == 1) {
					float bx = 0.0f;
					float by = 0.0f;
					for (int i = 0; i <= 360; i = i + blint) {
						bx = Mathf.Cos (i * Mathf.Deg2Rad) * 1.0f * 0.44f;
						by = Mathf.Sin (i * Mathf.Deg2Rad) * 1.0f * 0.44f;
						mc.generateEnemyBullet110 (1, cashTransform.position.x, cashTransform.position.y, 0.0f, 0.0f, bx, by);
					}
				}
			}
			//add game score
			mc.addGameScore( this.score );
			//generate explosion middle effect 
			mc.generateExplosionMiddleEffect( (cashTransform.position.x), (cashTransform.position.y) );
			//generate power up item(score)
			if (type != 1) {
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
				Debug.Log ("no inc dec enemy150");
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
	public void setInitStatus( int itm, float px, float py, int type = 0 ){	//item,posx/y,type set
		this.item = itm;
		this.posx = px;
		this.posy = py;
		this.type = type;
	}

}
