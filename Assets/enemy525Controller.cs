using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy525Controller : MonoBehaviour {
	//public
	public Sprite enemy526;	//enemy526
	public Sprite explosion_0;	//explosion 0
	public Sprite explosion_1;	//explosion 1

	//private
	//local const
	//x,y min/max
	const float xmin = -8.0f;
	const float xmax = 8.0f;
	const float ymin = -8.0f;
	const float ymax = 8.0f;
	//x,y speed base
	const float xspd = 0.0f;
	const float yspd = 0.0f;
	//x,y offset
	const float xoffset_0 = -2.35f;
	const float yoffset_0 = 0.8f;
	const float xoffset_1 = -1.3f;
	const float yoffset_1 = -1.3f;
	const float xoffset_2 = 1.3f;
	const float yoffset_2 = -1.3f;
	const float xoffset_3 = 2.35f;
	const float yoffset_3 = 0.8f;
	//base hit point
	const int basehitpoint = 1780;//1980;
//	const int basehitpoint = 40;
	//score
	readonly int hitscore = 50;
	readonly int score = 28000;
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
	enemy520Controller e520Ctr;	//(set form parent objects)

	//local
	//type
	int type;

	//explosion
	bool explosion;

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
	int bcnt3;
	int bcnt4;
	int bcnt5;
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
	int expcnt2;


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

		//type
		//(set from parent objects)
//		type = 0;

		//explosion
		explosion = false;

		//move seq
		movseq = 0;

		//move seq cnt
		movseqcnt = 0;

		//pos x,y
		posx = 0.0f;
		posy = ymax;
		if (type == 0) {
			posx = posx + xoffset_0;
			posy = posy + yoffset_0;
		} else if (type == 1) {
			posx = posx + xoffset_1;
			posy = posy + yoffset_1;
		} else if (type == 2) {
			posx = posx + xoffset_2;
			posy = posy + yoffset_2;
		} else if (type == 3) {
			posx = posx + xoffset_3;
			posy = posy + yoffset_3;
		}

		//position init
		cashTransform.position = new Vector3 (posx, posy, 0);

		//mov x,y
		//(set from parent objects)
//		xx = 0;
//		yy = 0;

		//first mov x,y

		//cdir
		cdir = 0.0f;
		cdd = 1.0f;

		//shot seq
		//(set from parent objects)
		stseq = 99;

		//bullet cnt
		bcnt = 0;	//(set from parent objects)
		bcnt2 = 0;
		bcnt3 = 0;
		bcnt4 = 0;
		bcnt5 = 0;
		bdir = 0;

		//damage cnt
		damagecnt = 0;

		//on damage
		ondamage = false;

		//blink cnt
		blinkcnt = 0;

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

		//explosion count
		expcnt = 0;
		expcnt2 = 0;

		//sprite
		sr.sprite = null;

		//scale
		cashTransform.localScale = new Vector3( 0.2f, 0.2f, 1.0f );

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
			cashTransform.Translate (xx, yy, 0);
			break;
		case 1:
			//move for atack
			//sprite
			if (sr.sprite == null) {
				sr.sprite = enemy526;
			}
			//move
			cashTransform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
			cashTransform.Translate (xx, yy, 0);
			//rotate
			cashTransform.rotation = Quaternion.Euler (new Vector3 (0, 0, cdir));
			cdir = cdir - cdd;
			if (cdir <= 0) {
				cdir = cdir + 360;
			}
			if (cdd <= 8) {
				cdd = cdd + 0.05f;
			}
			//scale
			Vector3 scl = cashTransform.localScale;
			if (scl.x <= 2.2) {
				scl.x = scl.x + 0.1f;
				scl.y = scl.y + 0.1f;
				cashTransform.localScale = scl;
			}
			break;
		case 10:
			//enemy 525 explosion
			//color
			sr.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
			//move
			cashTransform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
			cashTransform.Translate (xx, yy, 0);
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
			expcnt2 = expcnt2 + 6;
			if (expcnt2 % 12 == 0) {
				//generate explosion middle effect
				float ex = Mathf.Cos ((float)expcnt2 * Mathf.Deg2Rad) * 1.0f * 0.4f;
				float ey = Mathf.Sin ((float)expcnt2 * Mathf.Deg2Rad) * 1.0f * 0.4f;
				float exx = Mathf.Cos ((float)expcnt2 * Mathf.Deg2Rad) * 1.0f * 0.4f;
				float eyy = Mathf.Sin ((float)expcnt2 * Mathf.Deg2Rad) * 1.0f * 0.4f;
				mc.generateExplosionMiddleEffect ( (cashTransform.position.x + ex), (cashTransform.position.y + ey), exx, eyy );
			}
			//last?
			if (expcnt2 >= 1080) {
				expcnt = 0;
				expcnt2 = 0;
				movseq = 11;
			}
			break;
		case 11:
			//enemy 526 explosion 2
			//color
			sr.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
			//move
			cashTransform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
			cashTransform.Translate (xx, yy, 0);
			//play se
			mc.playSound (mc.se_middlebossexplosion);
//			//generate screen flash effect
//			mc.generateScreenFlashEffect ();
			//generate shake effect
			mc.generateScreenShakeEffect (15);
			//generate explosion middle effect
			for (int i = 0; i < 15; i++) {
				//explosion1
				float exx = Mathf.Cos ((float)(i * 24) * Mathf.Deg2Rad) * 1.0f * 0.4f;
				float eyy = Mathf.Sin ((float)(i * 24) * Mathf.Deg2Rad) * 1.0f * 0.4f;
				mc.generateExplosionMiddleEffect ((cashTransform.position.x), (cashTransform.position.y), exx, eyy);
				//explosion2
				exx = Mathf.Cos ((float)((i * 24) + 12) * Mathf.Deg2Rad) * 1.0f * 0.7f;
				eyy = Mathf.Sin ((float)((i * 24) + 12) * Mathf.Deg2Rad) * 1.0f * 0.7f;
				mc.generateExplosionMiddleEffect ((cashTransform.position.x), (cashTransform.position.y), exx, eyy);
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
			expcnt = 0;
			expcnt2 = 0;
			movseq = 12;
			break;
		case 12:
			//explosion anim
			//color
			sr.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
			//move
			cashTransform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
			cashTransform.Translate (xx, yy, 0);
			//explosion
			expcnt++;
			if (expcnt >= 10) {
				expcnt = 0;
				if (sr.sprite == explosion_0) {
					sr.sprite = explosion_1;
				} else {
					sr.sprite = explosion_0;
				}
			}
			break;
		case 13:
			//term
			//objnum dec
			mc.decObj ();
			//delet this
			Destroy (gameObject);
			//seq
			movseq = -1;
			break;
		case -1:
			//nop
			break;
		default:
			break;

		}
		//move result process
		if ( (cashTransform.position.y > (ymax+2.0f)) ||
			(cashTransform.position.y < (ymin-2.0f)) ||
			(cashTransform.position.x < (xmin-2.0f)) ||
			(cashTransform.position.x > (xmax+2.0f)) ){
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
		const float xoffset_bl = 0.0f;
		const float yoffset_bl = -0.15f;
		const float b1spdbase = 0.34f;
		const float b2spdbase = 0.51f;
		const float b3spdbase = 0.41f;
		const float b4spdbase = 0.31f;
		const float b5spdbase = 0.71f;
		float b1xx = 0;
		float b1yy = 0;
		int intvl = 0;
		int intvl2 = 0;
		int intmin = 0;
		int intmid = 0;
		int intmax = 0;
		switch (stseq) {	//(set from e520)
		case 0:
			//wait
			bcnt = 0;
			bcnt2 = 0;
			bcnt3 = 0;
			bcnt4 = 0;
			bcnt5 = 0;
			break;
		case 1:
			//atack1
			if (mc.gameLevel == mc.gameLevelEasy) {
				intmax = 10;
			} else if (mc.gameLevel == mc.gameLevelNormal) {
				intmax = 30;
			} else if (mc.gameLevel == mc.gameLevelHard) {
				intmax = 38;
			}
			if ((type == 0) || (type == 3)) {
				if ( (bcnt%7 == 0) && (bcnt%45 <= intmax)) {
					mc.generateEnemy50 (270, b1spdbase, cashTransform.position.x, cashTransform.position.y);
				}
			}
			if ( (type == 1) || (type == 2) ) {
				if ( (bcnt%7 == 0) && (bcnt%45 <= intmax)) {
					mc.generateEnemy50 (270, (b1spdbase+0.10f), cashTransform.position.x, cashTransform.position.y);
				}
			}
			break;
		case 2:
			//atack2
			if (mc.gameLevel == mc.gameLevelEasy) {
				intmax = 5;
			} else if (mc.gameLevel == mc.gameLevelNormal) {
				intmax = 15;
			} else if (mc.gameLevel == mc.gameLevelHard) {
				intmax = 20;
			}
			if ((type == 0) || (type == 3)) {
				if ( bcnt%70 == 23 ) {
					for (int i = 180; i <= 360; i = i + 4) {
						if (i % 30 <= intmax) {
							float bx = Mathf.Cos (i * Mathf.Deg2Rad) * b2spdbase;
							float by = Mathf.Sin (i * Mathf.Deg2Rad) * b2spdbase;
							mc.generateEnemyBullet110 (1, cashTransform.position.x, cashTransform.position.y, 0, 0, bx, by);  
						}
					}
				}
			}
			if ((type == 1) || (type == 2)) {
				if ( bcnt%70 == 10 ) {
					for (int i = 180; i <= 360; i = i + 5) {
						if (i % 30 <= intmax) {
							float bx = Mathf.Cos (i * Mathf.Deg2Rad) * b2spdbase;
							float by = Mathf.Sin (i * Mathf.Deg2Rad) * b2spdbase;
							mc.generateEnemyBullet110 (1, cashTransform.position.x, cashTransform.position.y, 0, 0, bx, by);  
						}
					}
				}
			}
			break;
		case 3:
			//atack3
			if (mc.gameLevel == mc.gameLevelEasy) {
				intvl = 16;
			} else if (mc.gameLevel == mc.gameLevelNormal) {
				intvl = 12;
			} else if (mc.gameLevel == mc.gameLevelHard) {
				intvl = 10;
			}
			if ((type == 0) || (type == 3)) {
				if (bcnt % 6 == 0) {
					bcnt2 = bcnt2 + (intvl*2);
					mc.generateEnemyBullet120 (bcnt2 - 9, b3spdbase, cashTransform.position.x, cashTransform.position.y);
					if (mc.gameLevel != mc.gameLevelEasy) {
						mc.generateEnemyBullet120 (bcnt2 + 9, b3spdbase, cashTransform.position.x, cashTransform.position.y);
					}
				}
			}
			if (mc.gameLevel != mc.gameLevelEasy) {
				if ((type == 1) || (type == 2)) {
					if (bcnt % 5 == 0) {
						bcnt3 = bcnt3 + intvl;
						mc.generateEnemyBullet120 (bcnt3 - 6, b3spdbase, cashTransform.position.x, cashTransform.position.y);
						mc.generateEnemyBullet120 (bcnt3 + 6, b3spdbase, cashTransform.position.x, cashTransform.position.y);
					}
				}
			}
			break;
		case 4:
			//atack4
			if (bcnt == 0) {
				bcnt2 = 225;
				bcnt3 = 315;
			}
			if (type == 0) {
				if (bcnt % 6 == 0) {
					float bx = Mathf.Cos (bcnt2 * Mathf.Deg2Rad) * b4spdbase;
					float by = Mathf.Sin (bcnt2 * Mathf.Deg2Rad) * b4spdbase;
					mc.generateEnemyBullet110 (1, cashTransform.position.x, cashTransform.position.y, 0, 0, bx, by);
					bcnt2 = bcnt2 + 10;
					if (bcnt2 >= 315) {
						bcnt2 = 225;
					}
				}
			}
			if (type == 3) {
				if (bcnt % 6 == 0) {
					float bx = Mathf.Cos (bcnt3 * Mathf.Deg2Rad) * b4spdbase;
					float by = Mathf.Sin (bcnt3 * Mathf.Deg2Rad) * b4spdbase;
					mc.generateEnemyBullet110 (1, cashTransform.position.x, cashTransform.position.y, 0, 0, bx, by);  
					bcnt3 = bcnt3 - 10;
					if (bcnt3 <= 225) {
						bcnt3 = 315;
					}
				}
			}
			if (type == 1) {
				if ( (bcnt%5 == 0) && (bcnt%60 <= 30) ) {
					mc.generateEnemyBullet120 (275+(bcnt/5), b5spdbase, cashTransform.position.x, cashTransform.position.y, 0, 0);
				}
			}
			if (type == 2) {
				if ( (bcnt%5 == 0) && (bcnt%60 >= 30) ) {
					mc.generateEnemyBullet120 (265-(bcnt/5), b5spdbase, cashTransform.position.x, cashTransform.position.y, 0, 0);
				}
			}
			break;
		case 5:
			//atack5
			if (bcnt == 0) {
				bcnt2 = 0;
				bcnt3 = 0;
			}
			if (mc.gameLevel == mc.gameLevelEasy) {
				intmax = 10;
			} else if (mc.gameLevel == mc.gameLevelNormal) {
				intmax = 30;
			} else if (mc.gameLevel == mc.gameLevelHard) {
				intmax = 38;
			}
			if ((type == 0) || (type == 1) || (type == 2) || (type == 3)) {
				if ( (bcnt%7 == 0) && (bcnt%45 <= intmax)) {
					mc.generateEnemy50 (270+bcnt3, b1spdbase, cashTransform.position.x, cashTransform.position.y);
					mc.generateEnemy50 (30+bcnt3, b1spdbase, cashTransform.position.x, cashTransform.position.y);
					mc.generateEnemy50 (150+bcnt3, b1spdbase, cashTransform.position.x, cashTransform.position.y);
					bcnt2 = bcnt2 + 1;
					if (bcnt2 >= 13) {
						bcnt2 = 0;
						bcnt3 = bcnt3 - 30;
					}
				}
			}
			break;
		case 6:
			//atack6
			if (mc.gameLevel == mc.gameLevelEasy) {
				intmax = 10;
				intvl = 90;
			} else if (mc.gameLevel == mc.gameLevelNormal) {
				intmax = 40;
				intvl = 40;
			} else if (mc.gameLevel == mc.gameLevelHard) {
				intmax = 48;
				intvl = 25;
			}
			if (bcnt == 0) {
				bcnt2 = 0;
				bcnt3 = 0;
				bcnt4 = 0;
				bcnt5 = 0;
			}
			if (type == 0) {
				if ((bcnt % 4 == 0) && (bcnt % 50 <= intmax)) {
					float bx = Mathf.Cos (bcnt2 * Mathf.Deg2Rad) * b4spdbase;
					float by = Mathf.Sin (bcnt2 * Mathf.Deg2Rad) * b4spdbase;
					mc.generateEnemyBullet100 (1, cashTransform.position.x, cashTransform.position.y, 0, 0, bx, by);
					bcnt2 = bcnt2 + 7;
				}
			}
			if (bcnt >= 30) {
				if (type == 1) {
					if ((bcnt % 4 == 0) && (bcnt % 50 <= intmax)) {
						float bx = Mathf.Cos (bcnt3 * Mathf.Deg2Rad) * b4spdbase;
						float by = Mathf.Sin (bcnt3 * Mathf.Deg2Rad) * b4spdbase;
						mc.generateEnemyBullet100 (1, cashTransform.position.x, cashTransform.position.y, 0, 0, bx, by);
						bcnt3 = bcnt3 + 7;
					}
				}
			}
			if (bcnt >= 60) {
				if (type == 2) {
					if ((bcnt % 4 == 0) && (bcnt % 50 <= intmax)) {
						float bx = Mathf.Cos (bcnt4 * Mathf.Deg2Rad) * b4spdbase;
						float by = Mathf.Sin (bcnt4 * Mathf.Deg2Rad) * b4spdbase;
						mc.generateEnemyBullet100 (1, cashTransform.position.x, cashTransform.position.y, 0, 0, bx, by);
						bcnt4 = bcnt4 + 7;
					}
				}
			}
			if (bcnt >= 90) {
				if (type == 3) {
					if ((bcnt % 4 == 0) && (bcnt % 50 <= intmax)) {
						float bx = Mathf.Cos (bcnt5 * Mathf.Deg2Rad) * b4spdbase;
						float by = Mathf.Sin (bcnt5 * Mathf.Deg2Rad) * b4spdbase;
						mc.generateEnemyBullet100 (1, cashTransform.position.x, cashTransform.position.y, 0, 0, bx, by);
						bcnt5 = bcnt5 + 7;
					}
				}
			}
			if (bcnt % intvl == 0) {
				mc.generateEnemyBullet130 (0, cashTransform.position.x, cashTransform.position.y, 0, 0, b2spdbase, b2spdbase);
			}
			break;
		case 7:
			//atack 7
			if (mc.gameLevel == mc.gameLevelEasy) {
				intvl = 45;
			} else if (mc.gameLevel == mc.gameLevelNormal) {
				intvl = 20;
			} else if (mc.gameLevel == mc.gameLevelHard) {
				intvl = 12;
			}
			if (bcnt == 0) {
				bcnt2 = 0;
			}
			if (bcnt % intvl == 0) {
				mc.generateEnemy50 (3-bcnt2, b1spdbase, cashTransform.position.x, cashTransform.position.y);
				mc.generateEnemy50 (0-bcnt2, b1spdbase, cashTransform.position.x, cashTransform.position.y);
				mc.generateEnemy50 (357-bcnt2, b1spdbase, cashTransform.position.x, cashTransform.position.y);
				mc.generateEnemy50 (87-bcnt2, b1spdbase, cashTransform.position.x, cashTransform.position.y);
				mc.generateEnemy50 (90-bcnt2, b1spdbase, cashTransform.position.x, cashTransform.position.y);
				mc.generateEnemy50 (93-bcnt2, b1spdbase, cashTransform.position.x, cashTransform.position.y);
				mc.generateEnemy50 (177-bcnt2, b1spdbase, cashTransform.position.x, cashTransform.position.y);
				mc.generateEnemy50 (180-bcnt2, b1spdbase, cashTransform.position.x, cashTransform.position.y);
				mc.generateEnemy50 (183-bcnt2, b1spdbase, cashTransform.position.x, cashTransform.position.y);
				mc.generateEnemy50 (267-bcnt2, b1spdbase, cashTransform.position.x, cashTransform.position.y);
				mc.generateEnemy50 (270-bcnt2, b1spdbase, cashTransform.position.x, cashTransform.position.y);
				mc.generateEnemy50 (273-bcnt2, b1spdbase, cashTransform.position.x, cashTransform.position.y);
				bcnt2 = bcnt2 + 8;
			}
			break;
		case 8:
			//atack 8
			if (mc.gameLevel == mc.gameLevelEasy) {
				intmax = 5;
				intvl = 90;
			} else if (mc.gameLevel == mc.gameLevelNormal) {
				intmax = 15;
				intvl = 50;
			} else if (mc.gameLevel == mc.gameLevelHard) {
				intmax = 20;
				intvl = 25;
			}
			if ((type == 0) || (type == 3)) {
				if ( bcnt%100 == 23 ) {
					for (int i = 180; i <= 360; i = i + 4) {
						if (i % 30 <= intmax) {
							float bx = Mathf.Cos (i * Mathf.Deg2Rad) * b2spdbase;
							float by = Mathf.Sin (i * Mathf.Deg2Rad) * b2spdbase;
							mc.generateEnemyBullet130 (1, cashTransform.position.x, cashTransform.position.y, 0, 0, bx, by);  
						}
					}
				}
			}
			if ((type == 1) || (type == 2)) {
				if ( bcnt%100 == 10 ) {
					for (int i = 180; i <= 360; i = i + 4) {
						if (i % 30 <= intmax) {
							float bx = Mathf.Cos (i * Mathf.Deg2Rad) * b2spdbase;
							float by = Mathf.Sin (i * Mathf.Deg2Rad) * b2spdbase;
							mc.generateEnemyBullet130 (1, cashTransform.position.x, cashTransform.position.y, 0, 0, bx, by);  
						}
					}
				}
			}
			if (bcnt % intvl == 0) {
				mc.generateEnemyBullet110 (0, cashTransform.position.x, cashTransform.position.y, 0, 0, b2spdbase, b2spdbase);
			}
			break;
		case 99:
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
			//explosion
			explosion = true;
			//sprite
			sr.sprite = explosion_0;
			//tag
			this.tag = "unavailableEnemy";
			//color
			sr.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
			//for e515
			e520Ctr.set_e525status (type, false);
			//play se
			mc.playSound (mc.se_middlebossexplosion);
//			//generate screen flash effect
//			mc.generateScreenFlashEffect ();
			//generate explosion big effect
			mc.generateExplosionBigEffect ((cashTransform.position.x), (cashTransform.position.y));
			//generate shake effect
			mc.generateScreenShakeEffect (15);
			//generate wipe2
			mc.generateWipe2 ();
			//exp cnt
			expcnt = 0;
			//color
			sr.color = new Color (215.0f / 255.0f, 215.0f / 255.0f, 215.0f / 255.0f, 210.0f / 255.0f);
			//seq
			movseq = 10;
			stseq = -1;
			//add game score
			mc.addGameScore (this.score);
			//sub message display
			mc.dispSubMessage ((cashTransform.position.x + 0.0f), (cashTransform.position.y - 0.8f), 0.0f, -1.5f, this.score.ToString ("D") + "pts!!", 2);
			return;
		}
		//display hit point
//		mc.dispEnemyHitPoint( eHp, eHpIntial );
		//add game score
		mc.addGameScore( this.hitscore );
		//random explosion effect
		const float x_offset4 = 0.35f;
		const float y_offset4 = 0.35f;
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
				mc.generateBurner100Effect( cashTransform.position.x, cashTransform.position.y, (Random.Range(-0.15f,0.15f)*0.5f), (Random.Range(-0.15f,0.15f)*0.5f) );
			}
			//generate enemy damage effect
//			for (int i = 0; i < 20; i++) {
			for (int i = 0; i < 5; i++) {
				const float x_offset = 0.4f;
				const float y_offset = 0.4f;
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


	//public
	//this setting (enemy515 set)
	public void setStatus( int mseq, int sseq, int bcnt, float xx, float yy ){
		//movseq set
		if ((movseq >= 0) && (movseq < 10)) {
			//mov seq
			movseq = mseq;
		}
		//mov info
		this.xx = xx;
		this.yy = yy;
		//shot seq set
		if ((stseq >= 0) && (stseq < 100)) {
			//shotseq
			this.stseq = sseq;
			this.bcnt = bcnt;
		}
	}

	//set enemy525 term
	public void setEnemy525Term(){
		if (explosion == true) {
			eHp = 0;	//for safe
			//sprite
			sr.sprite = null;
			//tag
			this.tag = "unavailableEnemy";	//for safe
			eHp = 0;
			//seq
			movseq = 13;	//term
			stseq = -1;
		} else {
			eHp = 0;
			//sprite
			sr.sprite = null;
			//tag
			this.tag = "unavailableEnemy";
			//seq
			movseq = 13;	//term
			stseq = -1;
		}
	}

	//set enemy515 controller
	public void setEnemy520Ctr(enemy520Controller e520ctr){
		this.e520Ctr = e520ctr;
	}

	//public
	public void setInitStatus( int type ){
		this.type = type;
	}

}
