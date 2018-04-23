using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy300Controller : MonoBehaviour {
	//public
	public Sprite enemy300l;
	public Sprite enemy300c;
	public Sprite enemy300r;

	//private
	//local const
	//x,y min/max
	const float xmin = -6.0f;
	const float xmax = 6.0f;
	const float ymin = -8.0f;
	const float ymax = 8.0f;
	//x,y speed base
	const float xspd = 0.03f;
	const float yspd = 0.12f;
	//x,y scale base
	const float xscl = 2.4f;
	const float yscl = 2.4f;
	//base hit point
	const int basehitpoint0 = 4620;
	const int basehitpoint1 = 6320;
	const int basehitpoint2 = 7820;
	//score
	readonly int hitscore = 40;
	readonly int score = 19600;

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

	//move seq
	int mvseq;

	//move speed
	float xx;
	float yy;

	//shot seq
	int stseq;
	int stseq2;

	//shot process
	float sdir = 0.0f;
	float ss = 0.0f;
	int blcnt = 0;
	int blintcnt = 0;
	//(type2 atack2)
	float stdir_l1 = 0;
	float stdir_l2 = 0;
	float stdir_l3 = 0;
	float stdir_r1 = 0;
	float stdir_r2 = 0;
	float stdir_r3 = 0;

	//item
	int item;

	//init hitpoint
	int eHpIntial;

	//hitpoint
	int eHp;

	//damage cnt
	int damagecnt;

	//on damage(for blink)
	bool ondamage;

	//blink cnt
	int blinkcnt;

	//explotion
	int expcnt;
	int expcnt2;
	float expdir;
	float expsize;

	//burner
	int bncnt;

	//waitcnt
	int waitcnt;


	// Use this for initialization
	void Start () {
		//system init
		intervalCnt = 0;

		//cash
		//transform cash
		cashTransform = transform;

		//sprite
		sr = GetComponent<SpriteRenderer>();

		//maincontroller
		mainCtr = GameObject.Find ("mainController");
		mc = mainCtr.GetComponent<mainController> ();

		//playercontroller
		playerCtr = GameObject.Find ("playerController");
		plc = playerCtr.GetComponent<playerController> ();

		//position init
		cashTransform.position = new Vector3 (-4.8f, -7.5f, 0.0f);

		//type
		//(set from parent objects)
//		type = 0;

		//move seq mode
		mvseq = 0;

		//move speed
		xx = 0.18f;
		yy = 0.41f;

		//shot seq mode
		stseq = 0;
		stseq2 = 0;

		//shot process
		if (type == 0) {
			sdir = 270.0f;
			ss = 12.0f;
			blcnt = 0;
			blintcnt = 0;
		} else if (type == 1) {
			sdir = 0.0f;
			ss = 0.0f;	//game level
			blcnt = 0;
			blintcnt = 0;
		} else if (type == 2) {
			sdir = 0.0f;
			ss = 0.0f;
			blcnt = 0;
			blintcnt = 0;
		}

		//item
		//(set from parent objects)
//		item = -1;

		//enemy inital hitpoint
		if (type == 0) {
			eHpIntial = basehitpoint0 + 0;
		} else if (type == 1) {
			eHpIntial = basehitpoint1 + 0;
		} else if (type == 2) {
			eHpIntial = basehitpoint2 + 0;
		}

		eHpIntial = eHpIntial + (plc.pPower * 60);
		eHpIntial = eHpIntial + ((plc.oNum-2) * 30);
		eHpIntial = eHpIntial + (plc.pLaser * 30);
		eHpIntial = eHpIntial + (plc.pMissile * 60);

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

		//explosion
		expcnt = 0;
		expcnt2 = 0;
		expdir = 0;
		expsize = 0;

		//burner
		bncnt = 0;

		//waitcnt
		waitcnt = 0;

		//middle flight se play
		mc.playSound(mc.se_middlebossflightin);
		//middle flight noise se play
		mc.playSound(mc.se_middlebossnoise);

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
		switch (mvseq) {
		case 0:
			//move forward
			cashTransform.Translate (xx, yy, 0);
			//move seq change position?
			if (xx <= 0.00005f) {
				xx = 0.0130f;
				yy = -0.0065f;
				//tag
				this.tag = "enemyLow";
				mvseq++;
			} else {
				//x,y acceleration
				xx = xx - 0.0031f;
				yy = yy - 0.0086f;
			}
			if (xx >= 0.1f) {
				sr.sprite = enemy300r;
			} else {
				sr.sprite = enemy300c;
			}
			//burner
			bncnt++;
			if (bncnt >= 3) {
				bncnt = 0;
				mc.generateBurner100Effect (cashTransform.position.x-1.1f, cashTransform.position.y-1.2f,
					0, -0.4f, 0.9f, 1.2f);
				mc.generateBurner100Effect (cashTransform.position.x+1.1f, cashTransform.position.y-1.2f,
					0, -0.4f, 0.9f, 1.2f);
				mc.generateBurner100Effect (cashTransform.position.x-1.25f, cashTransform.position.y-1.2f,
					0, -0.4f, 0.9f, 1.2f);
				mc.generateBurner100Effect (cashTransform.position.x+1.25f, cashTransform.position.y-1.2f,
					0, -0.4f, 0.9f, 1.2f);
			}
			break;
		case 1:
			//move left<->right forward<->back
			cashTransform.Translate (xx, yy, 0);
			if ((cashTransform.position.x <= -2.0f) || (cashTransform.position.x >= 2.0f)) {
				xx = xx - (xx * 2);
			}
			if ((cashTransform.position.y <= 1.35f) || (cashTransform.position.y >= 2.1f)) {
				yy = yy - (yy * 2);
			}
			if (xx >= 0.0f) {
				if (cashTransform.position.x <= -1.5f) {
					sr.sprite = enemy300c;
				} else {
					sr.sprite = enemy300r;
				}
			}
			if (xx <= 0.0f) {
				if (cashTransform.position.x >= 1.5f) {
					sr.sprite = enemy300c;
				} else {
					sr.sprite = enemy300l;
				}
			}
			//hp check
			if (eHp < (eHpIntial * 0.3)) {
				expcnt++;
				if (expcnt >= 7) {
					expcnt = 0;
					mc.generateExplosionMiddleEffect (
						cashTransform.position.x + Random.Range (-1.5f, +1.5f), cashTransform.position.y + Random.Range (-1.0f, +1.0f));
				}
			}
			//burner
			bncnt++;
			if (bncnt >= 3) {
				bncnt = 0;
				mc.generateBurner100Effect (cashTransform.position.x-1.1f, cashTransform.position.y-1.2f,
					0, -0.4f, 0.9f, 1.2f);
				mc.generateBurner100Effect (cashTransform.position.x+1.1f, cashTransform.position.y-1.2f,
					0, -0.4f, 0.9f, 1.2f);
				mc.generateBurner100Effect (cashTransform.position.x-1.25f, cashTransform.position.y-1.2f,
					0, -0.4f, 0.9f, 1.2f);
				mc.generateBurner100Effect (cashTransform.position.x+1.25f, cashTransform.position.y-1.2f,
					0, -0.4f, 0.9f, 1.2f);
			}
			break;

		case 2:
			//explosion and forward
			if (expcnt2 == 0) {
				//play se
				mc.playSound (mc.se_middlebossexplosion);
			}
			//color
			sr.color = new Color (215.0f / 255.0f, 215.0f / 255.0f, 215.0f / 255.0f, 160.0f / 255.0f);
			//explosion 1
			expcnt++;
			if (expcnt >= 6) {
				expcnt = 0;
				mc.generateExplosionMiddleEffect (
					cashTransform.position.x + Random.Range (-1.8f, +1.8f), cashTransform.position.y + Random.Range (-1.3f, +1.3f));
			}
			//explosion 2
			if ((expcnt2 % 3 == 0) && (expcnt2 <= 45)) {
				for (int i = 0; i <= 360; i = i + 60) {
					float bx = Mathf.Cos ((i+(expcnt2*5)) * Mathf.Deg2Rad) * 0.32f;
					float by = Mathf.Sin ((i+(expcnt2*5)) * Mathf.Deg2Rad) * 0.32f;
					mc.generateExplosionMiddleEffect (cashTransform.position.x, cashTransform.position.y, bx, by);
				}
			}
			expcnt2 = expcnt2 + 1;
			cashTransform.Translate (xx, yy, 0);
			if (cashTransform.position.y <= -1.15f) {
				//play se middile boss explosion
				mc.playSound(mc.se_middlebossexplosion);
				//stop loop se(flight noise)
				mc.stopLoopSe ();
				sr.color = new Color (215.0f/255.0f, 215.0f/255.0f, 215.0f/255.0f, 0.0f/255.0f);
				mc.generateScreenShakeEffect (50);
				for (int i = 0 ; i <= 6 ; i++) {	//8
					mc.generateExplosionMiddleEffect (
						cashTransform.position.x + Random.Range (-2.0f, +2.0f), cashTransform.position.y + Random.Range (-1.8f, +1.8f));
				}
				for (int i = 0 ; i <= 6 ; i++) {	//8
					mc.generateBurner100Effect (cashTransform.position.x, cashTransform.position.y,
						Random.Range (-1.5f, +1.5f), Random.Range (-1.5f, +1.5f), 2.5f, 2.5f );
				}
				expcnt = 0;
				mvseq = 3;
				//sprite none
				sr.sprite = null;
				//generate item
				this.generateItem ();
				//release wait
				mc.releaseWait ();
			}
			break;
		case 3:
			//explosion and term
			if ( (expcnt % 1) == 0) {
				float expx = cashTransform.position.x + Mathf.Cos ((expdir * Mathf.Deg2Rad)) * expsize;
				float expy = cashTransform.position.y + Mathf.Sin ((expdir * Mathf.Deg2Rad)) * expsize;
				mc.generateExplosionMiddleEffect (expx, expy);
				expdir = expdir - 20.0f;
				if (expdir <= 0.0f) {
					expdir = expdir + 360.0f;
				}
				expsize = expsize + 0.022f;
			}
			expcnt++;
			if (expcnt >= 80) {
				//term
				//objnum dec
				mc.decObj ();
				//destroy this
				Destroy (gameObject);
			}
			break;
		default:
			break;

		}

		//blink
		if (eHp > 0) {
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

		//move result process
		if ( (cashTransform.position.y > ymax) ||
			(cashTransform.position.y < ymin) ||
			(cashTransform.position.x < xmin) ||
			(cashTransform.position.x > xmax) ){
			//objnum dec
			mc.decObj();
			//delete this object
			Destroy (gameObject);
		}

		//shot process
		const float xloffset = -0.5f;
		const float yloffset = -0.3f;
		const float xroffset = +0.5f;
		const float yroffset = -0.3f;
		if (mvseq == 1) {
			if (type == 0) {
				//type 0
				int intmax = 0;
				int intmid = 0;
				switch (stseq) {
				case 0:
					//atack 1
					if (mc.gameLevel == mc.gameLevelEasy) {
						intmax = 15;
					} else if (mc.gameLevel == mc.gameLevelNormal) {
						intmax = 8;
					} else if (mc.gameLevel == mc.gameLevelHard) {
						intmax = 5;
					}
					blcnt++;
					if (blcnt > 8) {
						blcnt = 0;
						blintcnt++;
						if (blintcnt <= intmax) {
							const float bspd = 0.31f;//0.3f;
							mc.generateEnemyBullet120 (sdir, bspd, cashTransform.position.x, cashTransform.position.y, xloffset, yloffset);
							mc.generateEnemyBullet120 (sdir, bspd, cashTransform.position.x, cashTransform.position.y, xroffset, yloffset);
							mc.generateEnemyBullet120 (sdir - 90, bspd, cashTransform.position.x, cashTransform.position.y, xloffset, yloffset);
							mc.generateEnemyBullet120 (sdir - 90, bspd, cashTransform.position.x, cashTransform.position.y, xroffset, yloffset);
							mc.generateEnemyBullet120 (sdir - 180, bspd, cashTransform.position.x, cashTransform.position.y, xloffset, yloffset);
							mc.generateEnemyBullet120 (sdir - 180, bspd, cashTransform.position.x, cashTransform.position.y, xroffset, yloffset);
							mc.generateEnemyBullet120 (sdir - 270, bspd, cashTransform.position.x, cashTransform.position.y, xloffset, yloffset);
							mc.generateEnemyBullet120 (sdir - 270, bspd, cashTransform.position.x, cashTransform.position.y, xroffset, yloffset);
							sdir = sdir + ss;
							if ((sdir < 185.0f) || (sdir > 345.0f)) {
								ss = ss - (ss * 2);
							}
						}
						if (blintcnt >= 11) {
							blintcnt = 0;
							stseq2++;
							if (stseq2 >= 2) {
								blcnt = 0;
								stseq2 = 0;
								stseq = 1;
							}
						}
					}
					break;
				case 1:
					//atack 2
					blcnt++;
					if (blcnt > 19) {	//11
						float dd1 = 0.0f;
						blintcnt++;
						if (mc.gameLevel == mc.gameLevelEasy) {
							intmid = 10;
							intmax = 40;
						} else if (mc.gameLevel == mc.gameLevelNormal) {
							intmid = 30;
							intmax = 40;
						} else if (mc.gameLevel == mc.gameLevelHard) {
							intmid = 60;
							intmax = 80;
						}
						if (blintcnt <= intmid) {
							if (blintcnt % 4 == 0) {
								const float bspd = 0.31f;	//0.3f
								float bx;
								float by;
								for (int i = 0; i <= 360; i = i + 60) {
									bx = Mathf.Cos ((dd1 + i-blcnt) * Mathf.Deg2Rad) * bspd;
									by = Mathf.Sin ((dd1 + i-blcnt) * Mathf.Deg2Rad) * bspd;
									if (stseq2 % 2 == 0) {
										mc.generateEnemyBullet100 (1, cashTransform.position.x, cashTransform.position.y, xloffset, yloffset, bx, by);
									} else {
										mc.generateEnemyBullet100 (1, cashTransform.position.x, cashTransform.position.y, xroffset, yroffset, bx, by);
									}
								}
							}
						}
						if (blcnt >= 720) {
							blcnt = blcnt - 360;
						}
						if (blintcnt >= intmax) {	//13
							blcnt = 0;
							blintcnt = 0;
							stseq2++;
							if (stseq2 >= 4) {
								stseq2 = 0;
								stseq = 0;
								sdir = 270.0f;
							}
						}
					}
					break;
				case 10:
					//explosion + term
					break;
				default:
					break;
				}
			} else if (type == 1) {
				//type 1
				float bx;
				float by;
				float bspd = 0.34f;//0.3f;
				switch (stseq) {
				case 0:
					//atack 1
					if (mc.gameLevel == mc.gameLevelEasy) {
						ss = 15.0f;
					} else if (mc.gameLevel == mc.gameLevelNormal) {
						ss = 10.0f;
					} else if (mc.gameLevel == mc.gameLevelHard) {
						ss = 8.0f;
					}
					int intv = 0;
					int intv2 = 0;
					if (mc.gameLevel == mc.gameLevelEasy) {
						intv = 3;
					} else if (mc.gameLevel == mc.gameLevelNormal) {
						intv = 6;
					} else if (mc.gameLevel == mc.gameLevelHard) {
						intv = 7;
					}
						
					if (((blcnt % 20) >= 0) && ((blcnt % 20) <= intv)) {
						bx = Mathf.Cos (sdir * Mathf.Deg2Rad) * 1.0f * bspd;
						by = Mathf.Sin (sdir * Mathf.Deg2Rad) * 1.0f * bspd;
						mc.generateEnemyBullet100 (1, cashTransform.position.x, cashTransform.position.y, xloffset, yloffset, bx, by);
						if (mc.gameLevel == mc.gameLevelHard) {
							mc.generateEnemyBullet100 (1, cashTransform.position.x, cashTransform.position.y, xroffset, yroffset, bx, by);
						}
					}
					if (((blcnt % 20) >= 10) && ((blcnt % 20) <= (10 + intv))) {
						bx = Mathf.Cos (sdir * Mathf.Deg2Rad) * 1.0f * bspd;
						by = Mathf.Sin (sdir * Mathf.Deg2Rad) * 1.0f * bspd;
						if (mc.gameLevel == mc.gameLevelHard) {
							mc.generateEnemyBullet100 (1, cashTransform.position.x, cashTransform.position.y, xloffset, yloffset, bx, by);
						}
						mc.generateEnemyBullet100 (1, cashTransform.position.x, cashTransform.position.y, xroffset, yroffset, bx, by);
					}
					sdir = sdir + ss;
					if (sdir >= 360) {
						sdir = sdir - 360;
					}
					blcnt++;
					if (blcnt >= 360) {
						blcnt = 0;
						blintcnt++;
						if (blintcnt >= 1) {
							blcnt = 0;
							blintcnt = 0;
							sdir = 0;
							ss = 0;
							stseq2 = 0;
							stseq = 1;
						}
					}
					break;
				case 1:
					//atatck2
					bspd = 0.39f;//0.3f;
					intv = 0;
					intv2 = 0;
					if (mc.gameLevel == mc.gameLevelEasy) {
						intv = 180;
						intv2 = 8;
					} else if (mc.gameLevel == mc.gameLevelNormal) {
						intv = 120;
						intv2 = 4;
					} else if (mc.gameLevel == mc.gameLevelHard) {
						intv = 90;
						intv2 = 4;
					}
					if (blcnt % intv2 == 0) {
						for (int i = 0; i <= 360; i = i + intv) {
							if (blintcnt == 0) {
								mc.generateEnemyBullet120 (i + sdir, bspd, cashTransform.position.x, cashTransform.position.y, xloffset, yloffset);
							}else if ( blintcnt == 1 ){
								mc.generateEnemyBullet120 (i + sdir, bspd, cashTransform.position.x, cashTransform.position.y, xroffset, yroffset);
							}
						}
					}
					if (mc.gameLevel == mc.gameLevelEasy) {
						sdir = sdir + 6;
					} else if (mc.gameLevel == mc.gameLevelNormal) {
						sdir = sdir + 4;
					} else if (mc.gameLevel == mc.gameLevelHard) {
						sdir = sdir + 3;
					}
					if (sdir >= 720) {
						sdir = sdir - 720;
						blintcnt++;
						if (blintcnt >= 2) {
							blintcnt = 0;
							stseq2++;
							if (stseq2 >= 1) {
								blcnt = 0;
								blintcnt = 0;
								sdir = 0;
								ss = 0;
								stseq2 = 0;
								stseq = 0;
							}
						}
					}
					blcnt++;
					break;
				case 10:
					//explosion + term
					break;
				default:
					break;
				}
			} else if (type == 2) {
				//type 2
				//(atack1)
				float bx;
				float by;
				float bspd = 0.38f;//0.3f;
				int bstep = 4;
				int intmin = 0;
				int intmid = 0;
				int intmax = 0;
				//(atack2)
				switch (stseq) {
				case 0:
					//atack 1
					if (mc.gameLevel == mc.gameLevelEasy) {
						intmin = 10;
						intmid = 20;
						intmax = 50;
					} else if (mc.gameLevel == mc.gameLevelNormal) {
						intmin = 20;
						intmid = 40;
						intmax = 40;
					} else if (mc.gameLevel == mc.gameLevelHard) {
						intmin = 23;
						intmid = 45;
						intmax = 38;
					}
					if (blcnt == 10) {
						for (int i = 190; i <= 350; i = i + bstep) {
							if (i % 60 <= intmid) {
								bx = Mathf.Cos (i * Mathf.Deg2Rad) * bspd;
								by = Mathf.Sin (i * Mathf.Deg2Rad) * bspd;
								mc.generateEnemyBullet110 (1, cashTransform.position.x, cashTransform.position.y, xloffset, yloffset, bx, by);
							}
						}
					}
					if (blcnt == 40) {
						for (int i = 195; i <= 345; i = i + bstep) {
							if ( (i % 60 <= intmin) || (i % 60 >=intmax)) {
								bx = Mathf.Cos (i * Mathf.Deg2Rad) * bspd;
								by = Mathf.Sin (i * Mathf.Deg2Rad) * bspd;
								mc.generateEnemyBullet110 (1, cashTransform.position.x, cashTransform.position.y, xloffset, yloffset, bx, by);
							}
						}
					}
					if (blcnt == 80) {
						for (int i = 190; i <= 350; i = i + bstep) {
							if (i % 60 <= intmid) {
								bx = Mathf.Cos (i * Mathf.Deg2Rad) * bspd;
								by = Mathf.Sin (i * Mathf.Deg2Rad) * bspd;
								mc.generateEnemyBullet110 (1, cashTransform.position.x, cashTransform.position.y, xroffset, yroffset, bx, by);
							}
						}
					}
					if (blcnt == 110) {
						for (int i = 195; i <= 345; i = i + bstep) {
							if ( (i % 60 <= intmin) || (i % 60 >=intmax)) {
								bx = Mathf.Cos (i * Mathf.Deg2Rad) * bspd;
								by = Mathf.Sin (i * Mathf.Deg2Rad) * bspd;
								mc.generateEnemyBullet110 (1, cashTransform.position.x, cashTransform.position.y, xroffset, yroffset, bx, by);
							}
						}
					}
					blcnt++;
					if (blcnt >= 160) {
						blcnt = 0;
						stseq2++;
						if (stseq2 >= 1) {
							stseq2 = 0;
							stseq = 1;
							stdir_l1 = 0;
							stdir_l2 = 120;
							stdir_l3 = 240;
							stdir_r1 = 0;
							stdir_r2 = 120;
							stdir_r3 = 240;
						}
					}
					break;
				case 1:
					//atack 2
					float bstep3 = 8.0f;
					float bstep4 = 8.0f;
					if (mc.gameLevel == mc.gameLevelEasy) {
						intmax = 20;
					} else if (mc.gameLevel == mc.gameLevelNormal) {
						intmax = 55;
					} else if (mc.gameLevel == mc.gameLevelHard) {
						intmax = 65;
					}
					if (((blcnt % 4) == 0) && (blcnt % 80 <= intmax)) {
						mc.generateEnemyBullet120 (stdir_l1, bspd, cashTransform.position.x, cashTransform.position.y, xloffset, yloffset);
						mc.generateEnemyBullet120 (stdir_l2, bspd*1.1f, cashTransform.position.x, cashTransform.position.y, xloffset, yloffset);
						mc.generateEnemyBullet120 (stdir_l3, bspd*1.2f, cashTransform.position.x, cashTransform.position.y, xloffset, yloffset);
						mc.generateEnemyBullet120 (stdir_r1, bspd, cashTransform.position.x, cashTransform.position.y, xroffset, yroffset);
						mc.generateEnemyBullet120 (stdir_r2, bspd*1.1f, cashTransform.position.x, cashTransform.position.y, xroffset, yroffset);
						mc.generateEnemyBullet120 (stdir_r3, bspd*1.2f, cashTransform.position.x, cashTransform.position.y, xroffset, yroffset);
						stdir_l1 = stdir_l1 + bstep3;
						if (stdir_l1 >= 360) {
							stdir_l1 = stdir_l1 - 360;
						}
						stdir_l2 = stdir_l2 + bstep4;
						if (stdir_l2 >= 360) {
							stdir_l2 = stdir_l2 - 360;
						}
						stdir_l3 = stdir_l3 + bstep4;
						if (stdir_l3 >= 360) {
							stdir_l3 = stdir_l3 - 360;
						}
						stdir_r1 = stdir_r1 + bstep3;
						if (stdir_r1 >= 360) {
							stdir_r1 = stdir_r1 - 360;
						}
						stdir_r2 = stdir_r2 + bstep4;
						if (stdir_r2 >= 360) {
							stdir_r2 = stdir_r2 - 360;
						}
						stdir_r3 = stdir_r3 + bstep4;
						if (stdir_r3 >= 360) {
							stdir_r3 = stdir_r3 - 360;
						}
					}
					blcnt++;
					if (blcnt >= 360) {
						blcnt = 0;
						stseq = 2;
					}
					break;
				case 2:
					//atack 3
					if (mc.gameLevel == mc.gameLevelEasy) {
						intmax = 10;
					} else if (mc.gameLevel == mc.gameLevelNormal) {
						intmax = 6;
					} else if (mc.gameLevel == mc.gameLevelHard) {
						intmax = 5;
					}
					if (blcnt % intmax == 0) {
						bx = Mathf.Cos ((blcnt) * Mathf.Deg2Rad) * bspd;
						by = Mathf.Sin ((blcnt) * Mathf.Deg2Rad) * bspd;
						mc.generateEnemyBullet100 (1, cashTransform.position.x, cashTransform.position.y, xloffset, yloffset, bx, by);
						mc.generateEnemyBullet100 (1, cashTransform.position.x, cashTransform.position.y, xroffset, yroffset, bx, by);
						bx = Mathf.Cos ((blcnt+24) * Mathf.Deg2Rad) * bspd;
						by = Mathf.Sin ((blcnt+24) * Mathf.Deg2Rad) * bspd;
						mc.generateEnemyBullet100 (1, cashTransform.position.x, cashTransform.position.y, xloffset, yloffset, bx, by);
						mc.generateEnemyBullet100 (1, cashTransform.position.x, cashTransform.position.y, xroffset, yroffset, bx, by);
					}
					blcnt = blcnt + 8;
					if (blcnt >= 360) {
						blcnt = 0;
						stseq2++;
						if( stseq2 >= 6){
							stseq2 = 0;
							stseq = 0;
						}
					}
					break;
				case 10:
					//explosion + term
					break;
				default:
					break;
				}
			}
		}

		////interval process
		//interval count
		intervalCnt++;
		if (intervalCnt >= 2) {
			intervalCnt = 0;

//			//display hit point
//			mc.dispEnemyHitPoint( eHp, eHpIntial );
		}
	}


	//public
	//collision
	public void OnTriggerEnter2D(Collider2D coll){
		if (this.tag != "enemyLow") {
			return;
		}
		string cotag = coll.gameObject.tag;
		if (cotag == "player") {
			//collision player
		} else if (cotag == "playerBullet") {
			//collision palyer bullet
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
			//tag
			this.tag = "unavailableEnemy";
			//sprite
			sr.sprite = enemy300c;
			//color
			sr.color = new Color (215.0f/255.0f, 215.0f/255.0f, 215.0f/255.0f, 160.0f/255.0f);
			//generate wipe2
			mc.generateWipe2();
			//add game score
			mc.addGameScore( this.score );
			//explotion seq
			mvseq = 2;
			stseq = 10;
			xx = 0.0f;
			yy = -0.02f;
		}
		//display hit point
		mc.dispEnemyHitPoint( eHp, eHpIntial );
		//add game score
		mc.addGameScore( this.hitscore );
		//hit random explosion
		if (Random.Range (0, 30) >= 29) {
			mc.generateExplosionMiddleEffect (
				cashTransform.position.x + Random.Range (-0.5f, +0.5f), cashTransform.position.y + Random.Range (-0.5f, +0.5f));
		}
		//blink
		if (eHp > 0) {
			damagecnt++;
			if (damagecnt >= 30) {
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
//		for (int i = 0; i < 20; i++) {
		for (int i = 0; i < 15; i++) {
			mc.generatePowerup100 (mc.puType_score, Random.Range (-3.0f, +3.0f), 4.4f + Random.Range (-0.5f, +0.5f));
		}
		//adjust at game level and type
		if (mc.gameLevel == mc.gameLevelEasy) {
			mc.generatePowerup100 (mc.puType_shield, cashTransform.position.x, cashTransform.position.y);
			mc.generatePowerup100 (mc.puType_bomb, cashTransform.position.x, cashTransform.position.y);
			mc.generatePowerup100 (mc.puType_bomb, cashTransform.position.x, cashTransform.position.y);
			if (type == 0) {
				mc.generatePowerup100 (mc.puType_powerup, cashTransform.position.x, cashTransform.position.y);
				mc.generatePowerup100 (mc.puType_powerup, cashTransform.position.x, cashTransform.position.y);
				mc.generatePowerup100 (mc.puType_laser, cashTransform.position.x, cashTransform.position.y);
				mc.generatePowerup100 (mc.puType_option, cashTransform.position.x, cashTransform.position.y);
			} else if (type == 1) {
				mc.generatePowerup100 (mc.puType_powerup, cashTransform.position.x, cashTransform.position.y);
				mc.generatePowerup100 (mc.puType_powerup, cashTransform.position.x, cashTransform.position.y);
				mc.generatePowerup100 (mc.puType_laser, cashTransform.position.x, cashTransform.position.y);
				mc.generatePowerup100 (mc.puType_option, cashTransform.position.x, cashTransform.position.y);
			} else if (type == 2) {
				mc.generatePowerup100 (mc.puType_powerup, cashTransform.position.x, cashTransform.position.y);
				mc.generatePowerup100 (mc.puType_powerup, cashTransform.position.x, cashTransform.position.y);
				mc.generatePowerup100 (mc.puType_laser, cashTransform.position.x, cashTransform.position.y);
				mc.generatePowerup100 (mc.puType_option, cashTransform.position.x, cashTransform.position.y);
			}
		} else if (mc.gameLevel == mc.gameLevelNormal) {
			mc.generatePowerup100 (mc.puType_shield, cashTransform.position.x, cashTransform.position.y);
			mc.generatePowerup100 (mc.puType_bomb, cashTransform.position.x, cashTransform.position.y);
			if (type == 0) {
				mc.generatePowerup100 (mc.puType_powerup, cashTransform.position.x, cashTransform.position.y);
				mc.generatePowerup100 (mc.puType_laser, cashTransform.position.x, cashTransform.position.y);
				mc.generatePowerup100 (mc.puType_option, cashTransform.position.x, cashTransform.position.y);
			} else if (type == 1) {
				mc.generatePowerup100 (mc.puType_powerup, cashTransform.position.x, cashTransform.position.y);
				mc.generatePowerup100 (mc.puType_laser, cashTransform.position.x, cashTransform.position.y);
				mc.generatePowerup100 (mc.puType_option, cashTransform.position.x, cashTransform.position.y);
			} else if (type == 2) {
				mc.generatePowerup100 (mc.puType_powerup, cashTransform.position.x, cashTransform.position.y);
				mc.generatePowerup100 (mc.puType_laser, cashTransform.position.x, cashTransform.position.y);
				mc.generatePowerup100 (mc.puType_option, cashTransform.position.x, cashTransform.position.y);
			}
		} else if (mc.gameLevel == mc.gameLevelHard) {
			if (type == 0) {
				mc.generatePowerup100 (mc.puType_powerup, cashTransform.position.x, cashTransform.position.y);
				mc.generatePowerup100 (mc.puType_laser, cashTransform.position.x, cashTransform.position.y);
			} else if (type == 1) {
				mc.generatePowerup100 (mc.puType_powerup, cashTransform.position.x, cashTransform.position.y);
				mc.generatePowerup100 (mc.puType_option, cashTransform.position.x, cashTransform.position.y);
			} else if (type == 2) {
				mc.generatePowerup100 (mc.puType_powerup, cashTransform.position.x, cashTransform.position.y);
				mc.generatePowerup100 (mc.puType_option, cashTransform.position.x, cashTransform.position.y);
			}
		}
	}

	//public
	public void setInitStatus( int type, int itm ){	//item set
		this.type = type;
		this.item = itm;
	}

}
