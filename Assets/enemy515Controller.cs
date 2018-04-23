using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy515Controller : MonoBehaviour {
	//public
	//public enemy516 controller Prefab
	public GameObject enemy516ControllerPrefab;

	//private
	//local const
	//x,y min/max
	const float xmin = -4.0f;
	const float xmax = 4.0f;
	const float ymin = -8.0f;
	const float ymax = 8.0f;
	//x,y speed base
	const float xspd = 0.002f;
	const float yspd = 0.002f;
	//base hit point
	const int basehitpoint = 4280;//4680;//3700;
//	const int basehitpoint = 300;
	//score
	readonly int hitscore = 40;
	readonly int score = 18000;

	//system local
	int intervalCnt;	//interval count

	//component cash
	Transform cashTransform;
	SpriteRenderer sr;
	GameObject mainCtr;
	mainController mc;
	GameObject playerCtr;
	playerController plc;
	GameObject e516_l;
	enemy516Controller e516Ctr_l;
	GameObject e516_r;
	enemy516Controller e516Ctr_r;

	//local
	//e516 exist
	bool exist_e516_l;
	bool exist_e516_r;

	//move seq
	int movseq;

	//mov seq cnt
	int movseqcnt;

	//move
	float mdir;
	float xx;
	float yy;
	Vector2 tpos;

	//cdir
	float cdir;
	float cdd;

	//shot seq
	int stseq;
	int stseq2;

	//bullet cnt
	int bcnt;
	int bcnt2;

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

	//explosion count
	int expcnt;
	int expdir;

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

		//enemy516
		//l
		e516_l = Instantiate (enemy516ControllerPrefab) as GameObject;
		e516Ctr_l = e516_l.GetComponent<enemy516Controller> ();
		e516Ctr_l.setEnemy515Ctr( this );
		e516Ctr_l.setInitStatus (0);
		//r
		e516_r = Instantiate (enemy516ControllerPrefab) as GameObject;
		e516Ctr_r = e516_r.GetComponent<enemy516Controller> ();
		e516Ctr_r.setEnemy515Ctr( this );
		e516Ctr_r.setInitStatus (1);
		//e516 exist
		exist_e516_l = true;
		exist_e516_r = true;

		//position init
		cashTransform.position = new Vector3 (0, ymin, 0);

		//move seq mode
		movseq = 0;

		//move seq count
		movseqcnt = 0;

		//move
		mdir = 0;
		xx = 0;
		yy = 0.05f;
		tpos.x = 0.0f;
		tpos.y = 0.0f;

		//cdir
		cdir = 0;
		cdd = 0.001f;

		//shot seq
		stseq = 9;
		stseq2 = 0;

		//bullet cnt
		bcnt = 0;
		bcnt2 = 0;

		//damage cnt
		damagecnt = 0;

		//on damage
		ondamage = false;

		//blink cnt
		blinkcnt = 0;

		//explosion count
		expcnt = 0;
		expdir = 180;

		//already voice play
		alreadyVoice = false;

		//play se
		mc.playSound( mc.se_bosswakeup );
		mc.playSound( mc.se_basenoise );

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
			//move back
			cashTransform.Translate (xx, yy, 0);
			//y acceleration
//			yy = yy - 0.003f;
//			if (cashTransform.position.y <= 0.007f) {
//				yy = 0.007f;
//			}
			if (cashTransform.position.y >= 3.0f) {
				//tag
				this.tag = "enemy";
				e516_l.tag = "enemy";
				e516_r.tag = "enemy";
				xx = xspd * 1.0f;
				yy = yspd * 1.0f;
				movseq = 1;
				stseq = 0;
			}
			//setting enemy516
			if (exist_e516_l == true) {
				e516Ctr_l.setStatus (movseq, stseq, bcnt, xx, yy);
			}
			if (exist_e516_r == true) {
				e516Ctr_r.setStatus (movseq, stseq, bcnt, xx, yy);
			}
			break;
		case 1:
			//move for atack
			const float xmax = 0.38f;
			const float xmin = -0.38f;
			const float ymax = 3.25f;
			const float ymin = 2.75f;
			//move
			cashTransform.Translate (xx, yy, 0);
			Vector3 pos = cashTransform.position;
			if ((pos.x >= xmax) || (pos.x <= xmin)) {
				xx = xx - (xx * 2.0f);
			}
			if ((pos.y >= ymax) || (pos.y <= ymin)) {
				yy = yy - (yy * 2.0f);
			}
			//setting enemy516
			if (exist_e516_l == true) {
				e516Ctr_l.setStatus (movseq, stseq, bcnt, xx, yy);
			}
			if (exist_e516_r == true) {
				e516Ctr_r.setStatus (movseq, stseq, bcnt, xx, yy);
			}
			break;
		case 2:
			//move solo atack
			const float xmax2 = 1.38f;
			const float xmin2 = -1.38f;
			const float ymax2 = 3.25f;
			const float ymin2 = 2.75f;
			//init
			if (movseqcnt == 0) {
				xx = xspd * 20.0f;
				yy = yspd * 20.0f;
				if (alreadyVoice == false) {
					alreadyVoice = true;
					mc.playSound (mc.vo180);
				}
			}
			//move
			cashTransform.Translate (xx, yy, 0);
			pos = cashTransform.position;
			if ((pos.x >= xmax2) || (pos.x <= xmin2)) {
				xx = xx - (xx * 2.0f);
			}
			if ((pos.y >= ymax2) || (pos.y <= ymin2)) {
				yy = yy - (yy * 2.0f);
			}
			movseqcnt++;
			if (movseqcnt >= 120) {
				movseqcnt = 0;
				movseq = 3;
			}
			break;
		case 3:
			//to player
			const float mspd = 0.16f;
			if (movseqcnt == 0) {
				//to player x,y
				float xds, yds;
				Vector2 ppos = plc.getPlayerPos ();
				xds = (ppos.x) - (cashTransform.position.x);	//player,enemy x distance
				yds = (ppos.y) - (cashTransform.position.y);	//player,enemy y distance
				if ((xds == 0) && (yds == 0)) {	//for zero exception
					xds = 0.0001f;
				}
				mdir = Mathf.Atan2 (yds, xds) * Mathf.Rad2Deg;	//distance -> direction
				xx = Mathf.Cos (mdir * Mathf.Deg2Rad) * 1.0f * mspd;
				yy = Mathf.Sin (mdir * Mathf.Deg2Rad) * 1.0f * mspd;
				tpos.x = ppos.x;
				tpos.y = ppos.y;
			}
			//move
			cashTransform.Translate (xx, yy, 0);
			movseqcnt++;
			if( (cashTransform.position.x >= (tpos.x-0.2f)) && (cashTransform.position.x <= (tpos.x+0.2f)) && 
				(cashTransform.position.y >= (tpos.y-0.2f)) && (cashTransform.position.y <= (tpos.y+0.2f)) ){
				movseq = 4;
			}
			break;
		case 4:
			//to back
			cashTransform.Translate (xx * -1.0f, yy * -1.0f, 0);
			movseqcnt--;
			if (movseqcnt <= 0) {
				movseqcnt = 0;
				movseq = 2;
			}
			break;
		case 10:
			//explosion and move forward
			if (movseqcnt == 0) {
				//play se
				mc.playSound (mc.se_middlebossexplosion);
				//generate screen flash effect
				mc.generateScreenFlashEffect ();
				//generate explosion big effect
				mc.generateExplosionBigEffect ((cashTransform.position.x), (cashTransform.position.y));
				//generate shake effect
				mc.generateScreenShakeEffect( 25 );
			}
			movseqcnt++;
			//color
			sr.color = new Color(0.8f, 0.8f, 0.8f, 0.8f);
			//move
			cashTransform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
			cashTransform.Translate (0, -0.006f, 0);
			//explosion
			if (movseqcnt == 315) {
				//slow motion start
				mc.setPauseMaskOn ();
				Time.timeScale = 0.23f;
			}
			if (movseqcnt == 318) {
				//generate explosion big effect
				mc.generateExplosionBigEffect ((cashTransform.position.x), (cashTransform.position.y));
			}
			if (movseqcnt >= 320) {
				//slow motion end
				mc.setPauseMaskOff ();
				Time.timeScale = 1.0f;
				//sprite
				sr.sprite = null;
				//seq
				expcnt = 0;
				movseqcnt = 0;
				movseq = 11;
				//move speed
				xx = 0;
				//generate shake effect
				mc.generateScreenShakeEffect( 28 );
				//play se
				mc.playSound(mc.se_bossexplosion);
				//loop sound stop
				mc.stopLoopSe ();
			} else {
				//generate explosion
				//explosion 0
				if ((movseqcnt % 3 == 0) && (movseqcnt<=20)) {
					//generate burner effect
					float bx = 0.0f;
					float by = 0.0f;
					for (int i = 0; i < 5; i++) {
						bx = (Random.Range (-0.15f, 0.15f) * 1.8f);
						by = (Random.Range (-0.15f, 0.15f) * 2.4f);
						mc.generateBurner100Effect( cashTransform.position.x+bx, cashTransform.position.y+by, bx, by, 3.0f, 3.0f );
					}
				}
				//explosion 1
				const float x_offset2 = 0.5f;
				const float y_offset2 = 1.1f;
				expcnt++;
				if (expcnt >= 4) {
					expcnt = 0;
					if (Random.Range (0.0f, 2.5f) <= 1.5f) {
						//generate explosion middle effect
						float xofset = Random.Range ((x_offset2 * -1), x_offset2);
						float yofset = Random.Range ((y_offset2 * -1), y_offset2);
						mc.generateExplosionMiddleEffect ((cashTransform.position.x + xofset), (cashTransform.position.y + yofset));
						//generate burner effect
						for (int i = 0; i < 2; i++) {
							mc.generateBurner100Effect( cashTransform.position.x, cashTransform.position.y, (Random.Range(-0.15f,0.15f)*0.5f), (Random.Range(-0.15f,0.15f)*0.5f) );
						}
					}
				}
				//explosion 2
				if (movseqcnt % 2 == 0) {
					float exx = Mathf.Cos (expdir * Mathf.Deg2Rad) * 1.0f * 0.2f;
					float eyy = Mathf.Sin (expdir * Mathf.Deg2Rad) * 1.0f * 0.2f;
					float exx2 = Mathf.Cos ((expdir+180) * Mathf.Deg2Rad) * 1.0f * 0.2f;
					float eyy2 = Mathf.Sin ((expdir+180) * Mathf.Deg2Rad) * 1.0f * 0.2f;
					mc.generateExplosionMiddleEffect ((cashTransform.position.x+(exx*2)), (cashTransform.position.y+(eyy*2)), exx, eyy);
					mc.generateExplosionMiddleEffect ((cashTransform.position.x+(exx*2)), (cashTransform.position.y+(eyy*2)), exx*1.1f, eyy*1.1f);
					mc.generateExplosionMiddleEffect ((cashTransform.position.x+(exx2*2)), (cashTransform.position.y+(eyy2*2)), exx2, eyy2);
					mc.generateExplosionMiddleEffect ((cashTransform.position.x+(exx2*2)), (cashTransform.position.y+(eyy2*2)), exx2*1.1f, eyy2*1.1f);
					expdir = expdir + 8;
					if (expdir >= 360) {
						expdir = expdir - 360;
					}
				}
			}
			//rotate
			cdir = cdir - cdd;
			if (cdir <= 0) {
				cdir = cdir + 360;
			}
			if (cdd < 60.0f) {
				cdd = cdd + 0.05f;
			}
			if (cdd >= 60.0f) {
				cdd = 60.0f;
			}
			cashTransform.rotation = Quaternion.Euler (new Vector3 (0, 0, cdir));
			//voice play
			if (movseqcnt == 20) {
				mc.playSound (mc.vo200);
			}
			break;
		case 11:
			//explosion and term
			if (movseqcnt == 0) {
				//generate item
				this.generateItem ();
			}
			movseqcnt++;
			//last?
			if ( movseqcnt >= 40 ) {
				//generate screen flash effect
				mc.generateScreenFlashEffect ();
				//generate explosion big effect
				mc.generateExplosionBigEffect ((cashTransform.position.x), (cashTransform.position.y));
				//generate shake effect
				mc.generateScreenShakeEffect( 30 );
				//term	
				movseq = 12;
			}
			//generate explosion
			const float x_offset3 = 4.7f;
			const float y_offset3 = 3.4f;
			expcnt++;
			if (expcnt >= 2) {
				expcnt = 0;
				if (Random.Range (0.0f, 1.5f) <= 1.2f) {
					//generate explosion middle effect
					float xofset = 0.0f;
					float yofset = 0.0f;
					for (int i = 0; i < 10; i++) {
						xofset = Random.Range ((x_offset3 * -1), x_offset3);
						yofset = Random.Range ((y_offset3 * -1), y_offset3);
						mc.generateExplosionMiddleEffect ((cashTransform.position.x + xofset), (cashTransform.position.y + yofset), xofset, yofset);
					}
					//generate burner effect
					for (int i = 0; i < 10; i++) {
						mc.generateBurner100Effect( cashTransform.position.x, cashTransform.position.y, (Random.Range(-0.35f,0.35f)*3.5f), (Random.Range(-0.35f,0.35f)*2.3f), 4.8f, 4.8f );
					}
				}
			}
			break;
		case 12:
			//term
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

		//shot process
		int intvl=0;
		switch (stseq) {
		case 0:
			//wait
			bcnt++;
			if (bcnt >= 35) {
				bcnt = 0;
				stseq = 1;
			}
			break;
		case 1:
			//atack1
			bcnt++;
			if (bcnt > 60) {
				bcnt = 0;
				stseq2++;
				if (stseq2 >= 1) {
					stseq2 = 0;
					stseq = 2;
				}
			}
			break;
		case 2:
			//atack2
			if (mc.gameLevel == mc.gameLevelEasy) {
				intvl = 140;
			} else if (mc.gameLevel == mc.gameLevelNormal) {
				intvl = 80;
			} else {
				intvl = 60;
			}
			bcnt++;
			if (bcnt >= intvl) {
				bcnt = 0;
				stseq2++;
				if (stseq2 >= 2) {
					stseq2 = 0;
					stseq = 3;
				}
			}
			break;
		case 3:
			//atack3
			bcnt++;
			if (bcnt >= 360) {
				bcnt = bcnt - 360;
				stseq2++;
				if (stseq2 >= 1) {
					bcnt = 0;
					bcnt2 = 0;
					stseq2 = 0;
					stseq = 4;
				}
			}
			break;
		case 4:
			//atack4
			if (bcnt2 == 0) {
				bcnt++;
				if (bcnt >= 80) {
					bcnt2 = 1;
				}
			} else if (bcnt2 == 1) {
				bcnt--;
				if (bcnt <= -80) {
					bcnt2 = 0;
					stseq2++;
					if (stseq2 >= 1) {
						bcnt = 0;
						bcnt2 = 0;
						stseq2 = 0;
						stseq = 5;
					}
				}
			}
			break;
		case 5:
			//atack5
			bcnt++;
			if (bcnt >= 80) {
				bcnt = 0;
				stseq2++;
				if (stseq2 >= 4) {
					bcnt = 0;
					bcnt2 = 0;
					stseq2 = 0;
					stseq = 0;
				}
			}
			break;
		case 6:
			//atack solo 1
			bcnt++;
			if (bcnt % 10 == 0) {
				if (movseq == 2) {
					mc.generateEnemy150 (cashTransform.position.x, cashTransform.position.y - 0.9f, -1, 1);
				}
			}
			break;
		case 9:
			//wait
			break;
		case -1:
			//nop
			break;
		default:
			break;
		}


		////interval process
		//interval count
		intervalCnt++;

		//interval process
		if (intervalCnt >= 2) {
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
		if (eHp <= 0) {	//複数回呼ばれ対策
			return;
		}
		eHp = eHp - damage;
		if (eHp <= 0) {
			eHp = 0;
			//generate wipe2
			mc.generateWipe2();
			//tag
			this.tag = "unavailableEnemy";
			//color
			sr.color = new Color(0.8f, 0.8f, 0.8f, 0.8f);
			//exp cnt
			expcnt = 0;
			//term effect seq
			movseq = 10;
			movseqcnt = 0;
			//st seq
			stseq = -1;
			//setting enemy516
			if (exist_e516_l == true ) {
				e516Ctr_l.setStatus (movseq, stseq, bcnt, xx, yy);
				e516Ctr_l.setEnemy516Term ();
			}
			if (exist_e516_r == true) {
				e516Ctr_r.setStatus (movseq, stseq, bcnt, xx, yy);
				e516Ctr_r.setEnemy516Term ();
			}
			//score
			int sc=this.score;
			if( (exist_e516_l == false) && (exist_e516_r == false) ){
				sc = sc + 30000;
			}else if ( (exist_e516_l == false) || (exist_e516_r == false) ) {
				sc = sc + 13000;
			}
			//add game score
			mc.addGameScore( sc );
			//sub message display
			mc.dispSubMessage( (cashTransform.position.x+0.0f), (cashTransform.position.y-0.3f), 0.0f, -1.5f, sc.ToString("D")+"pts!!", 2 );
		}
		//display hit point
		mc.dispEnemyHitPoint( eHp, eHpIntial );
		//add game score
		mc.addGameScore( this.hitscore );
		//random explosion effect
		const float x_offset4 = 0.3f;
		const float y_offset4 = 0.3f;
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
				mc.generateBurner100Effect( cashTransform.position.x, cashTransform.position.y, (Random.Range(-0.15f,0.15f)*0.8f), (Random.Range(-0.15f,0.15f)*0.8f) );
			}
			//generate enemy damage effect
//			for (int i = 0; i < 20; i++) {
			for (int i = 0; i < 5; i++) {
				const float x_offset = 0.4f;
				const float y_offset = 0.2f;
				float xofset = Random.Range ((x_offset * -1), x_offset);
				float yofset = Random.Range ((y_offset * -1), y_offset);
				mc.generateEnemyDamageEffect( (cashTransform.position.x + xofset), (cashTransform.position.y + yofset) );
			}

		}
		//blink
		if (eHp > 0) {
			damagecnt++;
			if (damagecnt >= 10) {
				damagecnt = 0;
				if (blinkcnt <= 0) {
					blinkcnt = 3;
				} else {
					ondamage = true;
				}
			}
		}
	}

	//generate item
	private void generateItem(){
//		for (int i = 0; i < 50; i++) {
		for (int i = 0; i < 35; i++) {
			mc.generatePowerup100 (mc.puType_score, Random.Range (-3.0f, +3.0f), 4.4f + Random.Range (-0.5f, +0.5f));
		}
		//adjust at game level
		if (mc.gameLevel == mc.gameLevelEasy) {
			mc.generatePowerup100 (mc.puType_shield, cashTransform.position.x, cashTransform.position.y);
			mc.generatePowerup100 (mc.puType_bomb, cashTransform.position.x, cashTransform.position.y);
		} else if (mc.gameLevel == mc.gameLevelNormal) {
			mc.generatePowerup100 (mc.puType_bomb, cashTransform.position.x, cashTransform.position.y);
		} else if (mc.gameLevel == mc.gameLevelHard) {
			mc.generatePowerup100 (mc.puType_shield, cashTransform.position.x, cashTransform.position.y);
		}
		mc.generatePowerup100 (mc.puType_1up, cashTransform.position.x, cashTransform.position.y);
		mc.generatePowerup100 (mc.puType_powerup, cashTransform.position.x, cashTransform.position.y);
		mc.generatePowerup100 (mc.puType_laser, cashTransform.position.x, cashTransform.position.y);
		mc.generatePowerup100 (mc.puType_option, cashTransform.position.x, cashTransform.position.y);
		mc.generatePowerup100 (mc.puType_shield, cashTransform.position.x, cashTransform.position.y);
		mc.generatePowerup100 (mc.puType_bomb, cashTransform.position.x, cashTransform.position.y);
	}

	//public
	public void setInitStatus( int itm ){	//item set
		this.item = itm;	
	}

	//set enemy516 status
	public void set_e516status( int lrtype, bool exist ){
		int e516damage = 1780;//1380;
//		int e516damage = 80;
		if ( (eHp - e516damage) <= 0 ) {
			int hpadj = eHp;
			if (eHp >= 400) {
				hpadj = 350;
			}
			if (eHp < 400) {
				hpadj = 350 - eHp;
			}
			e516damage = e516damage - ((eHp-e516damage)*-1) - hpadj;
		}
		if (lrtype == 0) {
			exist_e516_l = exist;
			if (exist == false) {
				this.enemyHit (e516damage);
			}
		} else {
			exist_e516_r = exist;
			if (exist == false) {
				this.enemyHit (e516damage);
			}
		}
		if ((exist_e516_l == false) && (exist_e516_r == false)) {
			if (eHp > 0) {
				movseqcnt = 0;
				movseq = 2;
				bcnt = 0;
				stseq = 6;
			}
		}
	}


}
