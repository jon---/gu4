using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy516Controller : MonoBehaviour {
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
	//x,y offset
	const float xoffset_l = -1.305f;
	const float xoffset_r = 1.305f;
	const float yoffset_lr = -0.623f;
	//base hit point
	const int basehitpoint = 2410;//2410;
//	const int basehitpoint = 100;
	//score
	readonly int hitscore = 40;
	readonly int score = 20000;
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
	enemy515Controller e515Ctr;	//(set form parent objects)

	//local
	//lr type
	int lrtype;

	//move seq
	int movseq;

	//move seq cnt
	int movseqcnt;

	//mov x,y
	float xx;
	float yy;

	//first seq mov x,y
	float fxx;
	float fyy;

	//pos x,y
	float posx;
	float posy;

	//cdir
	float cdir;
	float cdd;

	//shot seq
	int stseq;

	//bullet cnt
	int bcnt;
	int bcnt2;
	float bdir;

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

		//lr type
		//(set from parent objects)
//		lrtype = 0;

		//move seq
		movseq = 0;

		//move seq cnt
		movseqcnt = 0;

		//pos x,y
		posx = 0.8f;
		if (lrtype == 0) {
			posx = posx * -1.0f;
		}
		posy = 1.0f;

		//position init
		cashTransform.position = new Vector3 (0.0f+posx, ymin + posy, 0);

		//mov x,y
		//(set from parent objects)
//		xx = 0;
//		yy = 0;

		//first mov x,y
		if (lrtype == 0) {
			fxx = (xoffset_l - posx) / 50.0f;
		} else {
			fxx = (xoffset_r - posx) / 50.0f;
		}
		fyy = (yoffset_lr - posy)/50.0f;

		//cdir
		cdir = 0.0f;
		cdd = 6.0f;

		//shot seq
		//(set from parent objects)
		stseq = 9;

		//bullet cnt
		bcnt = 0;	//(set from parent objects)
		bcnt2 = 0;
		bdir = 0;

		//enemy inital hitpoint
		eHpIntial = basehitpoint;
		eHpIntial = eHpIntial + (plc.pPower * 75);
		eHpIntial = eHpIntial + ((plc.oNum-2) * 50);
		eHpIntial = eHpIntial + (plc.pLaser * 50);
		eHpIntial = eHpIntial + (plc.pMissile * 75);

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

		//damage cnt
		damagecnt = 0;

		//on damage
		ondamage = false;

		//blink cnt
		blinkcnt = 0;

		//explosion count
		expcnt = 0;
		expdir = 0;

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
			//move back and first move
			if (movseqcnt <= 100) {
				cashTransform.Translate (xx, yy, 0);
				movseqcnt++;
			} else if ( movseqcnt <= 149 ){
				cashTransform.Translate (xx + fxx, yy + fyy, 0);
				movseqcnt++;
				if (movseqcnt % 8 == 0) {
					//play se 
					mc.playSound (mc.se_bossrotate);
				}
				if (movseqcnt == 150) {
					//play se
					mc.playSound( mc.se_basereleaseplayer );
				}
			} else {
				cashTransform.Translate (xx, yy, 0);
			}
			break;
		case 1:
			//move for atack
			cashTransform.Translate (xx, yy, 0);
			break;
		case 10:
			//enemy 516 forward and explosion
			//color
			sr.color = new Color (215.0f/255.0f, 215.0f/255.0f, 215.0f/255.0f, 210.0f/255.0f);
			//mov
			float xd = 0;
			if (lrtype == 0) {
				xd = -0.003f;
			} else {
				xd = 0.003f;
			}
			cashTransform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
			cashTransform.Translate (xd, -0.015f, 0);
			//direction
			cdir = cdir + cdd;//1.2f;
			if (cdir >= 360) {
				cdir = cdir - 360;
			}
			if (cdd > 1.2f) {
				cdd = cdd - 0.05f;
			}
			if (cdd <= 1.2f) {
				cdd = 1.2f;
			}
			cashTransform.rotation = Quaternion.Euler (new Vector3 (0, 0, cdir));
			//explosion
			const float x_offset3 = 0.2f;
			const float y_offset3 = 0.2f;
			expcnt++;
			if (expcnt % 4 == 0) {
				if (Random.Range (0.0f, 2.5f) <= 1.0f) {
					//generate explosion middle effect
					float xofset = Random.Range ((x_offset3 * -1), x_offset3);
					float yofset = Random.Range ((y_offset3 * -1), y_offset3);
					mc.generateExplosionMiddleEffect ((cashTransform.position.x + xofset), (cashTransform.position.y + yofset));
					//generate burner effect
					for (int i = 0; i < 3; i++) {
						mc.generateBurner100Effect (cashTransform.position.x, cashTransform.position.y, (Random.Range (-0.15f, 0.15f) * 1.8f), (Random.Range (-0.15f, 0.15f) * 1.8f));
					}
				}
			}
			//explosion2
			//last explosion
			expdir = expdir - 6;
			if (expdir % 12 == 0) {
				//generate explosion middle effect
				float ex = Mathf.Cos ((float)expdir * Mathf.Deg2Rad) * 1.0f * 0.4f;
				float ey = Mathf.Sin ((float)expdir * Mathf.Deg2Rad) * 1.0f * 0.4f;
				float exx = Mathf.Cos ((float)expdir * Mathf.Deg2Rad) * 1.0f * 0.11f;
				float eyy = Mathf.Sin ((float)expdir * Mathf.Deg2Rad) * 1.0f * 0.11f;
				mc.generateExplosionMiddleEffect ( (cashTransform.position.x + ex), (cashTransform.position.y + ey), exx, eyy );
			}
			//last?
			if (expdir <= -1080) {
				sr.sprite = null;
				expcnt = 0;
				expdir = 0;
				movseq = 11;
			}
			break;
		case 11:
			//enemy 516 explosion and term
			//play se
			mc.playSound (mc.se_middlebossexplosion);
//			//generate screen flash effect
//			mc.generateScreenFlashEffect ();
			//generate shake effect
			mc.generateScreenShakeEffect (23);
			//generate explosion middle effect
			for (int i = 0; i < 15; i++) {
				//explosion1
				float exx = Mathf.Cos ((float)(i*24) * Mathf.Deg2Rad) * 1.0f * 0.4f;
				float eyy = Mathf.Sin ((float)(i*24) * Mathf.Deg2Rad) * 1.0f * 0.4f;
				mc.generateExplosionMiddleEffect ( (cashTransform.position.x), (cashTransform.position.y), exx, eyy );
				//explosion2
				exx = Mathf.Cos ((float)((i*24)+12) * Mathf.Deg2Rad) * 1.0f * 0.7f;
				eyy = Mathf.Sin ((float)((i*24)+12) * Mathf.Deg2Rad) * 1.0f * 0.7f;
				mc.generateExplosionMiddleEffect ( (cashTransform.position.x), (cashTransform.position.y), exx, eyy );
			}
			//generate item
			if (mc.gameLevel == mc.gameLevelEasy) {
				mc.generatePowerup100 (mc.puType_shield, cashTransform.position.x, cashTransform.position.y);
				mc.generatePowerup100 (mc.puType_bomb, cashTransform.position.x, cashTransform.position.y);
			} else if (mc.gameLevel == mc.gameLevelNormal) {
				mc.generatePowerup100 (mc.puType_shield, cashTransform.position.x, cashTransform.position.y);
			} else if (mc.gameLevel == mc.gameLevelHard) {
				//none
			}
			//term
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
		if (eHp > 1) {
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
		const float xoffset_bl = 0.0f;
		const float yoffset_bl = -1.5f;
		const float b1spdbase = 0.31f;
		const float b2spdbase = 0.51f;
		const float b3spdbase = 0.43f;
		const float b4spdbase = 0.70f;
		float b1xx = 0;
		float b1yy = 0;
		int intvl = 0;
		int intvl2 = 0;
		switch (stseq) {	//(set from e515)
		case 0:
			//wait
			break;
		case 1:
			//atack1
			if (mc.gameLevel == mc.gameLevelEasy) {
				intvl = 5;
			} else if (mc.gameLevel == mc.gameLevelNormal) {
				intvl = 3;
			} else {
				intvl = 2;
			}
			if (lrtype == 0) {
				if (bcnt <= 20) {
					if (bcnt % intvl == 0) {
						b1xx = Mathf.Cos ((float)(180 + (bcnt * 9)) * Mathf.Deg2Rad) * 1.0f * b1spdbase;
						b1yy = Mathf.Sin ((float)(180 + (bcnt * 9)) * Mathf.Deg2Rad) * 1.0f * b1spdbase;
						mc.generateEnemyBullet100 (1, cashTransform.position.x, cashTransform.position.y, xoffset_bl, yoffset_bl, b1xx, b1yy);
					}
				}
				if (mc.gameLevel != mc.gameLevelEasy) {
					if (bcnt == 17) {
						mc.generateEnemyBullet110 (0, cashTransform.position.x, cashTransform.position.y, xoffset_bl, yoffset_bl, b2spdbase, b2spdbase);
					}
				}
			} else {
				if ( (bcnt >= 30) && (bcnt <= 50) ) {
					if (bcnt % intvl == 0) {
						b1xx = Mathf.Cos ((float)(180 + ((bcnt-30) * 9)) * Mathf.Deg2Rad) * 1.0f * b1spdbase;
						b1yy = Mathf.Sin ((float)(180 + ((bcnt-30) * 9)) * Mathf.Deg2Rad) * 1.0f * b1spdbase;
						mc.generateEnemyBullet100 (1, cashTransform.position.x, cashTransform.position.y, xoffset_bl, yoffset_bl, b1xx, b1yy);
					}
				}
				if (mc.gameLevel != mc.gameLevelEasy) {
					if (bcnt == 47) {
						mc.generateEnemyBullet110 (0, cashTransform.position.x, cashTransform.position.y, xoffset_bl, yoffset_bl, b2spdbase, b2spdbase);
					}
				}
			}
			break;
		case 2:
			//atack2
			if (mc.gameLevel == mc.gameLevelEasy) {
				intvl = 60;
				intvl2 = 100;	//none
			} else if (mc.gameLevel == mc.gameLevelNormal) {
				intvl = 50;
				intvl2 = 65;
			} else {
				intvl = 30;
				intvl2 = 35;
			}
			if (bcnt == intvl) {
				for (int i = 0; i <= 180; i = i + 45) {
					b1xx = Mathf.Cos ((float)(180 + (i - 6.0)) * Mathf.Deg2Rad) * 1.0f * b1spdbase;
					b1yy = Mathf.Sin ((float)(180 + (i - 6.0)) * Mathf.Deg2Rad) * 1.0f * b1spdbase;
					mc.generateEnemyBullet110 (1, cashTransform.position.x, cashTransform.position.y, xoffset_bl, yoffset_bl, b1xx, b1yy);
					b1xx = Mathf.Cos ((float)(180 + (i + 0.0f)) * Mathf.Deg2Rad) * 1.0f * b1spdbase;
					b1yy = Mathf.Sin ((float)(180 + (i + 0.0f)) * Mathf.Deg2Rad) * 1.0f * b1spdbase;
					mc.generateEnemyBullet110 (1, cashTransform.position.x, cashTransform.position.y, xoffset_bl, yoffset_bl, b1xx, b1yy);
					b1xx = Mathf.Cos ((float)(180 + (i + 6.0f)) * Mathf.Deg2Rad) * 1.0f * b1spdbase;
					b1yy = Mathf.Sin ((float)(180 + (i + 6.0f)) * Mathf.Deg2Rad) * 1.0f * b1spdbase;
					mc.generateEnemyBullet110 (1, cashTransform.position.x, cashTransform.position.y, xoffset_bl, yoffset_bl, b1xx, b1yy);
				}
			}
			if (mc.gameLevel != mc.gameLevelEasy) {
				if (bcnt == intvl2) {
					for (int i = 10; i <= 170; i = i + 30) {
						b1xx = Mathf.Cos ((float)(180 + (i - 3.0f)) * Mathf.Deg2Rad) * 1.0f * b2spdbase;
						b1yy = Mathf.Sin ((float)(180 + (i - 3.0f)) * Mathf.Deg2Rad) * 1.0f * b2spdbase;
						mc.generateEnemyBullet100 (1, cashTransform.position.x, cashTransform.position.y, xoffset_bl, yoffset_bl, b1xx, b1yy);
						b1xx = Mathf.Cos ((float)(180 + (i + 0.0f)) * Mathf.Deg2Rad) * 1.0f * b2spdbase;
						b1yy = Mathf.Sin ((float)(180 + (i + 0.0f)) * Mathf.Deg2Rad) * 1.0f * b2spdbase;
						mc.generateEnemyBullet100 (1, cashTransform.position.x, cashTransform.position.y, xoffset_bl, yoffset_bl, b1xx, b1yy);
						b1xx = Mathf.Cos ((float)(180 + (i + 3.0f)) * Mathf.Deg2Rad) * 1.0f * b2spdbase;
						b1yy = Mathf.Sin ((float)(180 + (i + 3.0f)) * Mathf.Deg2Rad) * 1.0f * b2spdbase;
						mc.generateEnemyBullet100 (1, cashTransform.position.x, cashTransform.position.y, xoffset_bl, yoffset_bl, b1xx, b1yy);
					}
				}
			}
			break;
		case 3:
			//atack3
			if (mc.gameLevel == mc.gameLevelEasy) {
				intvl = 25;
			} else if (mc.gameLevel == mc.gameLevelNormal) {
				intvl = 10;
			} else {
				intvl = 7;
			}
			if (lrtype == 0) {
				if (bcnt % intvl == 0) {
					mc.generateEnemyBullet120 ((float)(135 + (bcnt)), b3spdbase, cashTransform.position.x, cashTransform.position.y, xoffset_bl, yoffset_bl);
					mc.generateEnemyBullet120 ((float)(315 + (bcnt)), b3spdbase, cashTransform.position.x, cashTransform.position.y, xoffset_bl, yoffset_bl);
				}
			} else {
				if (bcnt % intvl == 0) {
					mc.generateEnemyBullet120 ((float)(45 + (bcnt)), b3spdbase, cashTransform.position.x, cashTransform.position.y, xoffset_bl, yoffset_bl);
					mc.generateEnemyBullet120 ((float)(225 + (bcnt)), b3spdbase, cashTransform.position.x, cashTransform.position.y, xoffset_bl, yoffset_bl);
				}
			}
			if (lrtype == 0) {
				if (bcnt % ((intvl * 6) + 0) == 0) {
					mc.generateEnemy150 (cashTransform.position.x + xoffset_bl, cashTransform.position.y + yoffset_bl-0.5f);
				}
			} else {
				if (bcnt % ((intvl * 6) + 5) == 0) {
					mc.generateEnemy150 (cashTransform.position.x + xoffset_bl, cashTransform.position.y + yoffset_bl-0.5f);
				}
			}
			break;
		case 4:
			//atack4
			bcnt2++;
			if (mc.gameLevel == mc.gameLevelEasy) {
				intvl = 50;
			} else if (mc.gameLevel == mc.gameLevelNormal) {
				intvl = 20;
			} else {
				intvl = 14;
			}
			if (lrtype == 0) {
				if (bcnt2 % intvl == 0) {
					b1xx = Mathf.Cos ((float)(257 + (bcnt)) * Mathf.Deg2Rad) * 1.0f * b2spdbase;
					b1yy = Mathf.Sin ((float)(257 + (bcnt)) * Mathf.Deg2Rad) * 1.0f * b2spdbase;
					mc.generateEnemyBullet130 (1, cashTransform.position.x, cashTransform.position.y, xoffset_bl, yoffset_bl, b1xx, b1yy);
					b1xx = Mathf.Cos ((float)(283 + (bcnt)) * Mathf.Deg2Rad) * 1.0f * b2spdbase;
					b1yy = Mathf.Sin ((float)(283 + (bcnt)) * Mathf.Deg2Rad) * 1.0f * b2spdbase;
					mc.generateEnemyBullet130 (1, cashTransform.position.x, cashTransform.position.y, xoffset_bl, yoffset_bl, b1xx, b1yy);
				}
			} else {
				if (bcnt2 % intvl == 0) {
					b1xx = Mathf.Cos ((float)(257 + ((bcnt))) * Mathf.Deg2Rad) * 1.0f * b2spdbase;
					b1yy = Mathf.Sin ((float)(257 + ((bcnt))) * Mathf.Deg2Rad) * 1.0f * b2spdbase;
					mc.generateEnemyBullet130 (1, cashTransform.position.x, cashTransform.position.y, xoffset_bl, yoffset_bl, b1xx, b1yy);
					b1xx = Mathf.Cos ((float)(283 + ((bcnt))) * Mathf.Deg2Rad) * 1.0f * b2spdbase;
					b1yy = Mathf.Sin ((float)(283 + ((bcnt))) * Mathf.Deg2Rad) * 1.0f * b2spdbase;
					mc.generateEnemyBullet130 (1, cashTransform.position.x, cashTransform.position.y, xoffset_bl, yoffset_bl, b1xx, b1yy);
				}
			}
			break;
		case 5:
			if (mc.gameLevel == mc.gameLevelEasy) {
				intvl = 10;
			} else if (mc.gameLevel == mc.gameLevelNormal) {
				intvl = 30;
			} else {
				intvl = 50;
			}
			if (bcnt == 0) {
				//to player x,y
				float xds, yds;
				Vector2 ppos = plc.getPlayerPos ();
				xds = (ppos.x) - (cashTransform.position.x + xoffset_bl);	//player,enemy x distance
				yds = (ppos.y) - (cashTransform.position.y + yoffset_bl);	//player,enemy y distance
				if ((xds == 0) && (yds == 0)) {	//for zero exception
					xds = 0.0001f;
				}
				bdir = Mathf.Atan2 (yds, xds)*Mathf.Rad2Deg;	//distance -> direction
			}
			if (bcnt % 7 == 0) {
				if (bcnt <= intvl) {
					mc.generateEnemyBullet120 (bdir-50.0f, b4spdbase, cashTransform.position.x, cashTransform.position.y, xoffset_bl, yoffset_bl);
					mc.generateEnemyBullet120 (bdir-15.0f, b3spdbase, cashTransform.position.x, cashTransform.position.y, xoffset_bl, yoffset_bl);
					mc.generateEnemyBullet120 (bdir, b2spdbase, cashTransform.position.x, cashTransform.position.y, xoffset_bl, yoffset_bl);
					mc.generateEnemyBullet120 (bdir+15.0f, b3spdbase, cashTransform.position.x, cashTransform.position.y, xoffset_bl, yoffset_bl);
					mc.generateEnemyBullet120 (bdir+50.0f, b4spdbase, cashTransform.position.x, cashTransform.position.y, xoffset_bl, yoffset_bl);
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
		if (eHp <= 0) {	//複数回呼ばれ対策
			return;
		}
		eHp = eHp - damage;
		if (eHp <= 0) {
			eHp = 0;
			//tag
			this.tag = "unavailableEnemy";
			//for e515
			e515Ctr.set_e516status( lrtype, false );
			//play se
			mc.playSound (mc.se_middlebossexplosion);
//			//generate screen flash effect
//			mc.generateScreenFlashEffect ();
			//generate explosion big effect
			mc.generateExplosionBigEffect ((cashTransform.position.x), (cashTransform.position.y));
			//generate shake effect
			mc.generateScreenShakeEffect (15);
			//generate wipe2
			mc.generateWipe2();
			//exp cnt
			expcnt = 0;
			//color
			sr.color = new Color (215.0f/255.0f, 215.0f/255.0f, 215.0f/255.0f, 210.0f/255.0f);
			//seq
			movseq = 10;
			stseq = -1;
			//add game score
			mc.addGameScore( this.score );
			//sub message display
			mc.dispSubMessage( (cashTransform.position.x+0.0f), (cashTransform.position.y-0.8f), 0.0f, -1.5f, this.score.ToString("D")+"pts!!", 2 );
			return;
		}
		//display hit point
//		mc.dispEnemyHitPoint( eHp, eHpIntial );
		//add game score
		mc.addGameScore( this.hitscore );
		//random explosion effect
		const float x_offset4 = 0.3f;
		const float y_offset4 = 1.2f;
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
				mc.generateBurner100Effect( cashTransform.position.x, cashTransform.position.y, (Random.Range(-0.15f,0.15f)*0.5f), (Random.Range(-0.15f,0.15f)*2.0f) );
			}
			//generate enemy damage effect
//			for (int i = 0; i < 20; i++) {
			for (int i = 0; i < 5; i++) {
				const float x_offset = 0.3f;
				const float y_offset = 1.5f;
				float xofset = Random.Range ((x_offset * -1), x_offset);
				float yofset = Random.Range ((y_offset * -1), y_offset);
				mc.generateEnemyDamageEffect( (cashTransform.position.x + xofset), (cashTransform.position.y + yofset) );
			}

		}
		//blink
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


	//public
	//this setting (enemy515 set)
	public void setStatus( int mseq, int sseq, int bcnt, float xx, float yy ){
		//movseq set
		if ( (movseq >= 0) && (movseq < 10) ) {
			//mov seq
			movseq = mseq;
			//mov info
			this.xx = xx;
			this.yy = yy;
		}
		//shot seq set
		if ((stseq >= 0) && (stseq < 10)) {
			//shotseq
			this.stseq = sseq;
			this.bcnt = bcnt;
		}
	}

	//set enemy516 term
	public void setEnemy516Term(){
		eHp = 0;
		//tag
		this.tag = "unavailableEnemy";
		//color
		sr.color = new Color (215.0f/255.0f, 215.0f/255.0f, 215.0f/255.0f, 210.0f/255.0f);
		//exp cnt
		expcnt = 0;
		//seq
		movseq = 10;
		stseq = -1;
	}

	//set enemy515 controller
	public void setEnemy515Ctr(enemy515Controller e515ctr){
		e515Ctr = e515ctr;
	}

	//public
	public void setInitStatus( int lrtype ){
		this.lrtype = lrtype;
	}

}
