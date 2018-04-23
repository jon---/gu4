using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy535Controller : MonoBehaviour {
	//public

	//private
	//local const
	//x,y min/max
	const float xmin = -4.0f;
	const float xmax = 4.0f;
	const float ymin = -8.0f;
	const float ymax = 8.0f;
	//x,y speed base
	const float xspd = 0.015f;
	const float yspd = 0.015f;
	//score
	readonly int hitscore = 40;
	readonly int score = 46000;
	//rotate speed
	const float rspd = 1.5f;

	//system local
	int intervalCnt;	//interval count

	//component cash
	Transform cashTransform;
	SpriteRenderer sr;
	GameObject mainCtr;
	mainController mc;
	GameObject playerCtr;
	playerController plc;
	enemy530Controller e530Ctr;	//(set form parent objects)

	//local
	//move seq
	int movseq;

	//move seq cnt
	int movseqcnt;

	//mov x,y
	float xx;
	float yy;

	//cdir
	float cdir;
	float cdd;
	float cddd;

	//damage cnt
	int damagecnt;

	//on damage(for blink)
	bool ondamage;

	//blink cnt
	int blinkcnt;


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

		//move seq
		movseq = 0;

		//move seq cnt
		movseqcnt = 0;

		//position init
		cashTransform.position = new Vector3 (0.0f, ymin, 0);

		//mov x,y
		//(set from parent objects)
//		xx = 0;
//		yy = 0;

		//cdir
		cdir = 0.0f;
		cdd = 0.2f;
		cddd = 0.05f;

		//damage cnt
		damagecnt = 0;

		//on damage
		ondamage = false;

		//blink cnt
		blinkcnt = 0;

		//tag
		this.tag = "unavailableEnemy";

		//objnum inc
		mc.incObj();
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

		//move process
		switch (movseq) {
		case 0:
			//first move
		case 1:
			//move for atack
			cashTransform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
			cashTransform.Translate (xx, yy, 0);
			//rotate
			const float cdmin = 0.1f;
			const float cdmax = 5.5f;
			cdir = cdir + cdd;
			if (cdir >= 360) {
				cdir = cdir - 360;
			}
			cdd = cdd + cddd;
			if ((cdd >= cdmax) || (cdd <= cdmin)) {
				cddd = cddd - (cddd * 2);
			}
			cashTransform.rotation = Quaternion.Euler (new Vector3 (0, 0, cdir));
			break;
		case 10:
		case 11:
		case 12:
			//enemy 530 explosion and term (for safe)
			break;
		case 20:
			sr.color = new Color (1.0f, 1.0f, 1.0f, 1.0f);
			cashTransform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
			cashTransform.Translate (xx, yy, 0);
			cdir = cdir + cdd;
			if (cdir >= 360) {
				cdir = cdir - 360;
			}
			if (cdd <= 30) {
				cdd = cdd + 0.1f;
			}
			cashTransform.rotation = Quaternion.Euler (new Vector3 (0, 0, cdir));
			//enemy 530 explosion
			break;
		case -1:
			//nop
			break;
		default:
			break;

		}
		//move result process
		if ( (cashTransform.position.y > ymax) ||
			(cashTransform.position.y < ymin) ||
			(cashTransform.position.x < xmin) ||
			(cashTransform.position.x > xmax) ){
			movseq = -1;	//for safe
		}

		//blink
		if ( this.tag == "enemy" ) {
			if (blinkcnt >= 1) {
				blinkcnt--;
				if (blinkcnt == 2) {
					sr.color = new Color (0.2f, 0.2f, 0.2f, 1.0f);
				} else if (blinkcnt == 1) {
					sr.color = new Color (1.0f, 1.0f, 1.0f, 0.2f);
				} else if (blinkcnt <= 0) {
					blinkcnt = 0;
					sr.color = new Color (1.0f, 1.0f, 1.0f, 1.0f);
					if (ondamage == true) {
						ondamage = false;
						blinkcnt = 3;
					}
				}
			}
		} else {
			ondamage = false;
			blinkcnt = 0;
		}

		////interval process
		//interval count
		intervalCnt++;
		if (intervalCnt >= 1) {
			intervalCnt = 0;
			//nop
		}
	}


	//public
	//collision
	public void OnTriggerEnter2D(Collider2D coll){
		if (this.tag != "enemy") {
			return;
		}
		string cotag = coll.gameObject.tag;
		if (cotag == "player") {
			//collision player
			//nop
		} else if (cotag == "playerBullet") {
			//collision player bullet
			this.enemyHit ( mc.damagePlayerBullet );
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
		e530Ctr.set_e535damage (damage);
		//add game score
		mc.addGameScore( this.hitscore );
		//random explosion effect
		const float x_offset4 = 0.1f;
		const float y_offset4 = 0.1f;
		if (Random.Range (0, 10) == 0) {
			float xofset = Random.Range ((x_offset4 * -1), x_offset4);
			float yofset = Random.Range ((y_offset4 * -1), y_offset4);
			mc.generateExplosionMiddleEffect ((cashTransform.position.x + xofset), (cashTransform.position.y + yofset));
		}
		//big damage? (bomb)
		if (damage >= mc.damageBig) {
			//generate burner effect
//			for (int i = 0; i < 30; i++) {
			for (int i = 0; i < 5; i++) {
				mc.generateBurner100Effect( cashTransform.position.x, cashTransform.position.y, (Random.Range(-0.15f,0.15f)*0.3f), (Random.Range(-0.15f,0.15f)*0.3f) );
			}
			//generate enemy damage effect
//			for (int i = 0; i < 20; i++) {
			for (int i = 0; i < 5; i++) {
				const float x_offset = 0.2f;
				const float y_offset = 0.2f;
				float xofset = Random.Range ((x_offset * -1), x_offset);
				float yofset = Random.Range ((y_offset * -1), y_offset);
				mc.generateEnemyDamageEffect( (cashTransform.position.x + xofset), (cashTransform.position.y + yofset) );
			}

		}
		//blink
		damagecnt++;
		if (damagecnt >= 40) {
			damagecnt = 0;
			if (blinkcnt <= 0) {
				blinkcnt = 3;
			} else {
				ondamage = true;
			}
		}
	}


	//public
	//this setting (enemy530 set)
	public void setStatus( int mseq, float xx, float yy ){
		//mov seq
		movseq = mseq;
		//mov info
		this.xx = xx;
		this.yy = yy;
	}

	//set enemy535 term
	public void setEnemy535Term(){
		//term
		//tag
		this.tag = "unavailableEnemy";
		//add game score
		mc.addGameScore( this.score );
		//objnum dec
		mc.decObj ();
		//delet this
		Destroy (gameObject);
		//for safe
		movseq = -1;
	}

	//set enemy530 controller
	public void setEnemy530Ctr(enemy530Controller e530ctr){
		this.e530Ctr = e530ctr;
	}

	//public
	public void setInitStatus(){
		//nop
	}

}
