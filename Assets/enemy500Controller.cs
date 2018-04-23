using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy500Controller : MonoBehaviour {
	//public
	//public enemy510 controller Prefab
	public GameObject enemy510ControllerPrefab;
	//public track mark controller Prefab
	public GameObject track100ControllerPrefab;

	//private
	//local const
	//x,y min/max
	const float xmin = -4.0f;
	const float xmax = 4.0f;
	const float ymin = -7.0f;
	const float ymax = 7.5f;
	//x,y speed base
	const float xspd = 0.0f;
	const float yspd = 0.025f;
	//base hit point
//	const int basehitpoint = 5000;//org1000 ->  shot speed3/2 1500 shot+ *2 3000  option 1000->spped 1.0  ->*2 2000
	const int basehitpoint = 4950;//4650;
	//score
	readonly int hitscore = 40;
	readonly int score = 15000;

	//system local
	int intervalCnt;	//interval count

	//component cash
	Transform cashTransform;
	SpriteRenderer sr;
	GameObject mainCtr;
	mainController mc;
	GameObject playerCtr;
	playerController plc;
	Animator animt;
	enemy510Controller e510Ctr;

	//local
	//move seq
	int movseq;

	//mov seq cnt
	int seqcnt;

	//move speed
	float xx;
	float yy;

	//animation speed
	float aspd = 0;

	//item
	int item;

	//init hitpoint
	int eHpIntial;

	//enemy hitpoint
	int eHp;

	//damage cnt
	int damagecnt;

	//on damage(for blink)
	bool ondamage;

	//blink cnt
	int blinkcnt;

	//track mark count
	int trcnt;

	//explosion count
	int expcnt;

	//already voice play
	bool alreadyVoice;


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

		//item
		//(set from parent objects)
//		item = -1;

		//enemy inital hitpoint
		eHpIntial = basehitpoint;
		eHpIntial = eHpIntial + (plc.pPower * 150);
		eHpIntial = eHpIntial + ((plc.oNum-2) * 100);
		eHpIntial = eHpIntial + (plc.pLaser * 100);
		eHpIntial = eHpIntial + (plc.pMissile * 150);

		//adjust at game level
		if (mc.gameLevel == mc.gameLevelEasy) {
			eHpIntial = (int)((float)eHpIntial * 0.95f);
		} else if (mc.gameLevel == mc.gameLevelNormal) {
			eHpIntial = (int)((float)eHpIntial * 1.00f);
		} else if (mc.gameLevel == mc.gameLevelHard) {
			eHpIntial = (int)((float)eHpIntial * 1.05f);
		}

		//enemy hitpoint
		eHp = eHpIntial;

		//enemy510
		GameObject e510 = Instantiate (enemy510ControllerPrefab) as GameObject;
		e510Ctr = e510.GetComponent<enemy510Controller> ();
		e510Ctr.setEnemy500Ctr( this );

		//animator
		animt = GetComponent<Animator>();
		animt.speed = 0.0f;

		//position init
		cashTransform.position = new Vector3 (0, ymax, 0);

		//move seq mode
		movseq = 0;

		//move seq count
		seqcnt = 0;

		//move speed
		xx = 0;
		yy = 0;

		//track mark count
		trcnt = 0;

		//explosion count
		expcnt = 0;

		//damage cnt
		damagecnt = 0;

		//on damage
		ondamage = false;

		//blink cnt
		blinkcnt = 0;

		//already voice play
		alreadyVoice = false;

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
		//nop

		////interval process
		//interval count
		intervalCnt++;

		//interval process 1
		if (intervalCnt % 1 == 0 ) {
//			intervalCnt = 0;

			//move and atack process
			switch (movseq) {
			case 0:
				//move forward
				cashTransform.Translate (0, (mc.getScrollSpeed()*-1), 0);
				//move seq change position?
				if (cashTransform.position.y < -1) {
					//reverse
					yy = yspd * 0.0525f;
					movseq++;
					//animation start
					aspd = 0.6f;
					animt.speed = aspd;
					//play se
					//tank wake up
					mc.playSound(mc.se_bosswakeup);
					//tank move
					mc.playSound(mc.se_bossroadnoise);
					//shake effect
					mc.generateScreenShakeEffect( 15 );
				}
				//setting enemy510
				e510Ctr.setStatus (movseq, cashTransform.position.x, cashTransform.position.y);
				break;
			case 1:
				//move back
				cashTransform.Translate (xx, yy, 0);
				//y acceleration
				if (cashTransform.position.y < 1.0f) {
					yy = yy + 0.00125f;
				} else {
					yy = yy + 0.0005f;
				}
				if (cashTransform.position.y >= 2.5f) {
					this.tag = "groundEnemy";
					movseq++;
				}
				//animation acceleration
				if (aspd > 2.8f) {
					aspd = 2.8f;
				} else {
					aspd = aspd + 0.05f;//0.2f;
				}
				animt.speed = aspd;
				//setting enemy510
				e510Ctr.setStatus( movseq, cashTransform.position.x, cashTransform.position.y );
				//track mark count
				trcnt++;
				if (trcnt >= 4) {
					trcnt = 0;
					generateTrackMark ();
				}
				break;
			case 2:
				//wait (enemy510 atack)
			case 3:
				//wait (enemy510 last atack)
				//generate explotion
				if (eHp <= (eHpIntial*0.55)) {	//hit point check
					const float x_offset = 1.2f;
					const float y_offset = 1.4f;
					expcnt++;
					if (expcnt >= 8) {
						expcnt = 0;
						if (Random.Range (0.0f, 2.7f) <= 1.8f) {
							//generate explosion middle effect
							float xofset = Random.Range ((x_offset * -1), x_offset);
							float yofset = Random.Range ((y_offset * -1), y_offset);
							mc.generateExplosionMiddleEffect ((cashTransform.position.x + xofset), (cashTransform.position.y + yofset));
						}
					}
				}
				//warning voice play
				if (eHp <= (eHpIntial*0.22) ){
					if (alreadyVoice == false) {
						alreadyVoice = true;
						mc.playSound (mc.vo180);
					}
				}
				//track mark count
				trcnt++;
				if (trcnt >= 4) {
					trcnt = 0;
					generateTrackMark ();
				}
				break;
			case 4:
				//explosion
				sr.color = new Color (0.8f, 0.8f, 0.8f, 0.8f);
				seqcnt++;
				if (seqcnt == 196) {
					//slow motion start
					mc.setPauseMaskOn ();
					Time.timeScale = 0.25f;
				}
				if (seqcnt >= 200) {
					//slow motion end
					mc.setPauseMaskOff ();
					Time.timeScale = 1.0f;
					movseq++;
					//animation stop
					animt.speed = 0.0f;
					//move speed
					xx = 0;
					//setting enemy510
					e510Ctr.setStatus (movseq, cashTransform.position.x, cashTransform.position.y);
					//generate shake effect
					mc.generateScreenShakeEffect( 15 );
					//play se
					mc.playSound(mc.se_bossexplosion);
					//generate explosion big effect
					mc.generateExplosionBigEffect ((cashTransform.position.x), (cashTransform.position.y));
					//tank move sound stop
					mc.stopLoopSe ();
				} else {
					//generate explosion
					const float x_offset2 = 1.6f;
					const float y_offset2 = 1.8f;
					expcnt++;
					if (expcnt >= 4) {
						expcnt = 0;
						if (Random.Range (0.0f, 2.5f) <= 1.8f) {
							//generate explosion middle effect
							float xofset = Random.Range ((x_offset2 * -1), x_offset2);
							float yofset = Random.Range ((y_offset2 * -1), y_offset2);
							mc.generateExplosionMiddleEffect ((cashTransform.position.x + xofset), (cashTransform.position.y + yofset));
							//generate splash burner effect
							for (int i = 0; i < 10; i++) {
								mc.generateBurner100Effect( cashTransform.position.x, cashTransform.position.y, (Random.Range(-0.15f,0.15f)*4.5f), (Random.Range(-0.15f,0.15f)*4.5f) );
							}
						}
					}
					//track mark count
					trcnt++;
					if (trcnt >= 4) {
						trcnt = 0;
						generateTrackMark ();
					}
				}
				//voice play
				if (seqcnt == 20) {
					mc.playSound (mc.vo200);
				}
				break;
			case 5:
				//explosion and move forward
				cashTransform.Translate (0, (mc.getScrollSpeed()*-1), 0);
				//move seq change position?
				if ( cashTransform.position.y < -0.2f ) {
					//term	
					movseq++;
				}
				//generate explosion
				const float x_offset3 = 2.0f;
				const float y_offset3 = 2.2f;
				expcnt++;
				if (expcnt >= 2) {
					expcnt = 0;
					if (Random.Range (0.0f, 1.5f) <= 1.0f) {
						//generate explosion middle effect
						float xofset = Random.Range ((x_offset3 * -1), x_offset3);
						float yofset = Random.Range ((y_offset3 * -1), y_offset3);
						mc.generateExplosionMiddleEffect ((cashTransform.position.x + xofset), (cashTransform.position.y + yofset));
						//generate splash burner effect
						for (int i = 0; i < 15; i++) {
							mc.generateBurner100Effect( cashTransform.position.x, cashTransform.position.y, (Random.Range(-0.15f,0.15f)*5.8f), (Random.Range(-0.15f,0.15f)*5.8f) );
						}
					}
				}
				break;
			case 6:
				//term
				//generate screen flash effect
				mc.generateScreenFlashEffect ();
				//generate shake effect
				mc.generateScreenShakeEffect (25);
				//generate splash burner effect
				for (int i = 0; i < 30; i++) {
					mc.generateBurner100Effect (cashTransform.position.x, cashTransform.position.y, (Random.Range (-0.15f, 0.15f) * 6.3f), (Random.Range (-0.15f, 0.15f) * 6.3f));
				}
				//play se
				mc.playSound (mc.se_bossexplosion);
				//generate explosion big effect
				mc.generateExplosionBigEffect ((cashTransform.position.x), (cashTransform.position.y));
				//generate item
				this.generateItem ();
				//game event wait release
				mc.releaseWait ();
				//objnum dec
				mc.decObj ();
				//delet this
				Destroy (gameObject);
				//for safe
				movseq = -1;
				break;
			case -1:	//(for safe)
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
		}

		//blink
		if (eHp > 0) {
			if (blinkcnt >= 1) {
				blinkcnt--;
				if (blinkcnt == 2) {
					sr.color = new Color (0.0f, 0.0f, 0.0f, 1.0f);
				} else if (blinkcnt == 1) {
					sr.color = new Color (1.0f, 1.0f, 1.0f, 0.0f);
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

		//interval process 2
		if (intervalCnt >= 2) {
			intervalCnt = 0;

//			//display hit point
//			mc.dispEnemyHitPoint( eHp, eHpIntial );
		}
	}


	//public
	//get inittial hit point for enemy510
	public int getHpInitial(){
		return eHpIntial;
	}

	//get hit point for enemy510
	public int getHp(){
		return eHp;
	}

	//collision
	public void OnTriggerEnter2D(Collider2D coll){
		if (this.tag != "groundEnemy") {
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
		if (eHp <= 0) {	//複数回呼ばれ対策
			return;
		}
		eHp = eHp - damage;
		if (eHp <= 0) {
			eHp = 0;
			//tag
			this.tag = "unavailableEnemy";
			//generate wipe2
			mc.generateWipe2();
			//exp cnt
			expcnt = 0;
			//term effect seq
			movseq = 4;
			//color change
//			sr.color = new Color (215.0f/255.0f, 215.0f/255.0f, 215.0f/255.0f, 160.0f/255.0f);
			//setting enemy510
			e510Ctr.setStatus( movseq, cashTransform.position.x, cashTransform.position.y );
			//add game score
			mc.addGameScore( this.score );
//			//fadeout bgm
//			mc.fadeoutBgm();
		}
		//display hit point
		mc.dispEnemyHitPoint( eHp, eHpIntial );
		//add game score
		mc.addGameScore( this.hitscore );
		//random explosion effect
		const float x_offset4 = 1.2f;
		const float y_offset4 = 1.0f;
		if (Random.Range (0, 8) == 0) {
			float xofset = Random.Range ((x_offset4 * -1), x_offset4);
			float yofset = Random.Range ((y_offset4 * -1), y_offset4);
			mc.generateExplosionMiddleEffect ((cashTransform.position.x + xofset), (cashTransform.position.y + yofset));
		}
		//big damage? (bomb)
		if (damage >= mc.damageBig) {
			//generate burner effect
//			for (int i = 0; i < 30; i++) {
			for (int i = 0; i < 12; i++) {
				mc.generateBurner100Effect( cashTransform.position.x, cashTransform.position.y, (Random.Range(-0.15f,0.15f)*5.8f), (Random.Range(-0.15f,0.15f)*5.8f) );
			}
			//generate enemy damage effect
//			for (int i = 0; i < 20; i++) {
			for (int i = 0; i < 8; i++) {
				const float x_offset = 1.6f;
				const float y_offset = 1.8f;
				float xofset = Random.Range ((x_offset * -1), x_offset);
				float yofset = Random.Range ((y_offset * -1), y_offset);
				mc.generateEnemyDamageEffect( (cashTransform.position.x + xofset), (cashTransform.position.y + yofset) );
			}

		}
		//blink
		if (eHp > 0) {
			damagecnt++;
			if (damagecnt >= 30) {
				damagecnt = 0;
				if (blinkcnt <= 0) {
					blinkcnt = 3;
					e510Ctr.setBlink ();
				} else {
					ondamage = true;
				}
			}
		}
	}

	//generate track mark
	private void generateTrackMark(){
		const float xoffset1 = 1.16f;
		const float xoffset2 = 1.16f;
		const float yoffset = 1.62f;
		//genarate
		GameObject go;
		track100Controller t100;
		//mark l
		go = Instantiate (track100ControllerPrefab) as GameObject;
		t100 = go.GetComponent<track100Controller> ();
		t100.setinitStatus ((cashTransform.position.x - xoffset1), (cashTransform.position.y - yoffset));
		//mark r
		go = Instantiate (track100ControllerPrefab) as GameObject;
		t100 = go.GetComponent<track100Controller> ();
		t100.setinitStatus ((cashTransform.position.x + xoffset1), (cashTransform.position.y - yoffset));
	}

	//generate item
	private void generateItem(){
//		for (int i = 0; i < 50; i++) {
		for (int i = 0; i < 30; i++) {
			mc.generatePowerup100 (mc.puType_score, Random.Range (-3.0f, +3.0f), 4.4f + Random.Range (-0.5f, +0.5f));
		}
		mc.generatePowerup100 (mc.puType_1up, cashTransform.position.x, cashTransform.position.y);
		mc.generatePowerup100 (mc.puType_shield, cashTransform.position.x, cashTransform.position.y);
		mc.generatePowerup100 (mc.puType_bomb, cashTransform.position.x, cashTransform.position.y);
		if (mc.gameLevel != mc.gameLevelHard) {
			mc.generatePowerup100 (mc.puType_powerup, cashTransform.position.x, cashTransform.position.y);
			if (mc.gameLevel == mc.gameLevelEasy) {
				mc.generatePowerup100 (mc.puType_bomb, cashTransform.position.x, cashTransform.position.y);
				mc.generatePowerup100 (mc.puType_shield, cashTransform.position.x, cashTransform.position.y);
			}
		}
	}

	//public
	public void setInitStatus( int itm ){	//item set
		this.item = itm;	
	}

}
