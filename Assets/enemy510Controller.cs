using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy510Controller : MonoBehaviour {
	//public

	//private
	//local const
	//x,y min/max
	const float xmin = -4.0f;
	const float xmax = 4.0f;
	const float ymin = -7.0f;
	const float ymax = 7.5f;
	//x,y speed base
	const float xspd = 0.015f;
	const float yspd = 0.005f;
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
	enemy500Controller e500Ctr;	//(set form parent objects)

	//local
	//move seq
	int movseq;

	//atack seq
	int atseq;
	//atack cnt
	int atcnt;
	float atcnt2;
	int atcnt3;
	int atcnt4;
	int atcnt5;
	float doffset1;
	float doffset2;
	float direction2;

	//rotate
	float tR;	//target rotate
	float cR;	//current rotate

	//bullet cnt
	int bcnt;

	//first bullet flag
	bool fstb;

	//sub bullet position offset
	float xoffset;
	float yoffset;

	//initial hitpoint
	int eHpInitial;

	//damage cnt
	int damagecnt;

	//on damage(for blink)
	bool ondamage;

	//blink cnt
	int blinkcnt;

	//sub bullet timer
	int sbtime;

	//sub bullet cnt
	int sbcnt;

	//atack2 interval
	int at2time;


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

		//position init
		cashTransform.position = new Vector3 (0, ymax, 0);

		//move seq mode
		movseq = 0;

		//damage cnt
		damagecnt = 0;

		//on damage
		ondamage = false;

		//blink cnt
		blinkcnt = 0;

		//atack seq
		atseq = 0;
		//atack cnt
		atcnt = 0;
		atcnt2 = 0;
		atcnt3 = 0;
		atcnt4 = 0;
		atcnt5 = 0;
		doffset1 = 0;
		doffset2 = 0;
		direction2=0;

		//initial hitpoint
		eHpInitial = e500Ctr.getHpInitial();

		//rotate
		tR = 0;
		cR = 0;

		//bullet cnt
		bcnt = 0;

		//first bullet flag
		fstb = false;

		//sub bullet position offset
		xoffset = 0;
		yoffset = 0;

		//sub bullet timer
		sbtime = 0;

		//sub bullet cnt
		sbcnt = 0;

		//atack2 interval
		at2time = 0;

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
		if (intervalCnt >= 1) {
			intervalCnt = 0;

			//move and atack process
			switch (movseq) {
			case 0:
				//wait (enemy500 set move forward)
				break;
			case 1:
				//wait (enemy500 set move back)
				break;
			case 2:
				//atack1
				//rotate direction process
				//direction
				float xdistance, ydistance;
				float direction;
				const float r_offset = -90.0f;	//90 rotate offset (砲身の向きが+90度のため)
				Vector2 ppos = plc.getPlayerPos ();
				xdistance = (ppos.x) - (cashTransform.position.x);	//player,enemy x distance
				ydistance = (ppos.y) - (cashTransform.position.y);	//player,enemy y distance
				if ((xdistance == 0) && (ydistance == 0)) {	//for zero exception
					xdistance = 0.0001f;
				}
				direction = Mathf.Atan2 (ydistance, xdistance) * Mathf.Rad2Deg;	//distance -> direction
				tR = direction + r_offset;
				//play se 
				if (cR != tR) {
					//rotate sound
					mc.playSound(mc.se_bossrotate);
				}
				//rotate (current -> target )
				if ((tR > cR) && ((tR - cR) > rspd)) {
					if ((tR - cR) < 180) {
						cR = cR + rspd;
					} else {
						cR = cR - rspd;
					}
				} else if ((cR > tR) && ((cR - tR) > rspd)) {
					if ((cR - tR) < 180) {
						cR = cR - rspd;
					} else {
						cR = cR + rspd;
					}
				} else {	//不一致の砲身ブレ防止
					cR = tR;
				}
				//set -179<-0->179 angle
				if (cR >= (180 + r_offset)) {
					cR = cR - 360;
				}
				if (cR <= (-180 + r_offset)) {
					cR = cR + 360;
				}
				cashTransform.rotation = Quaternion.Euler (new Vector3 (0, 0, cR));
				//atack seq
				const float mbl_spd = 0.80f;
				const float mbl_spd2 = 0.64f;
				const float sbl_spd = 0.75f;
				float xdistance2,ydistance2;
				switch (atseq) {
				case 0:
					//atack1-1
					//main bullet process
					bcnt++;
					if (bcnt >= 16) {	//main bullet interval
						bcnt = 0;
						//main bullet shot
						this.generateMainBullet();
					}
					//sub bullet process
					if (sbtime < 2000) {
						sbtime++;
					}
					sbtime++;
					float rrr;	//難易度調整はここで sub bullet 頻度
					if (sbtime >= 740) {	//wait timer for sub bullet level3
						rrr = 4.6f;
					} else if (sbtime >= 330) {	//wait timer for sub bullet level2
						rrr = 5.0f;
					} else if (sbtime >= 100) {	//wait timer for sub bullet level1
						rrr = 5.4f;
					} else {
						rrr = 10.0f;	//no shot
					}
					//adjust at game level
					if (mc.gameLevel == mc.gameLevelEasy) {
						rrr = rrr - 0.0f;
					} else if (mc.gameLevel == mc.gameLevelNormal) {
						rrr = rrr - 1.3f;
					} else if (mc.gameLevel == mc.gameLevelHard) {
						rrr = rrr - 1.8f;
					}
					sbcnt++;
					if (sbcnt >= 16) {	//sub bullet interval
						sbcnt = 0;
						//sub bullet shot
						if (Random.Range (0.0f, 8.0f) >= rrr) {
							//generate sub bullet(6shot)
							//generate position bullet 1,2,3, 4,5,6
							//bullet1 r1
							xoffset = (Mathf.Cos ((cR - r_offset + 130.0f) * Mathf.Deg2Rad) * 1.65f);
							yoffset = (Mathf.Sin ((cR - r_offset + 130.0f) * Mathf.Deg2Rad) * 1.65f);
							mc.generateEnemyBullet100 (0, cashTransform.position.x, cashTransform.position.y, xoffset, yoffset, sbl_spd, sbl_spd);
							//bullet2 r2
							xoffset = (Mathf.Cos ((cR - r_offset + 137.5f) * Mathf.Deg2Rad) * 1.51f);
							yoffset = (Mathf.Sin ((cR - r_offset + 137.5f) * Mathf.Deg2Rad) * 1.51f);
							mc.generateEnemyBullet100 (0, cashTransform.position.x, cashTransform.position.y, xoffset, yoffset, sbl_spd, sbl_spd);
							//bullet3 r3
							xoffset = (Mathf.Cos ((cR - r_offset + 144.0f) * Mathf.Deg2Rad) * 1.37f);
							yoffset = (Mathf.Sin ((cR - r_offset + 144.0f) * Mathf.Deg2Rad) * 1.37f);
							mc.generateEnemyBullet100 (0, cashTransform.position.x, cashTransform.position.y, xoffset, yoffset, sbl_spd, sbl_spd);
							//bullet4 l3
							xoffset = (Mathf.Cos ((cR - r_offset - 144.0f) * Mathf.Deg2Rad) * 1.37f);
							yoffset = (Mathf.Sin ((cR - r_offset - 144.0f) * Mathf.Deg2Rad) * 1.37f);
							mc.generateEnemyBullet100 (0, cashTransform.position.x, cashTransform.position.y, xoffset, yoffset, sbl_spd, sbl_spd);
							//bullet5 l2
							xoffset = (Mathf.Cos ((cR - r_offset - 137.5f) * Mathf.Deg2Rad) * 1.51f);
							yoffset = (Mathf.Sin ((cR - r_offset - 137.5f) * Mathf.Deg2Rad) * 1.51f);
							mc.generateEnemyBullet100 (0, cashTransform.position.x, cashTransform.position.y, xoffset, yoffset, sbl_spd, sbl_spd);
							//bullet6 l1
							xoffset = (Mathf.Cos ((cR - r_offset - 130.0f) * Mathf.Deg2Rad) * 1.65f);
							yoffset = (Mathf.Sin ((cR - r_offset - 130.0f) * Mathf.Deg2Rad) * 1.65f);
							mc.generateEnemyBullet100 (0, cashTransform.position.x, cashTransform.position.y, xoffset, yoffset, sbl_spd, sbl_spd);
						}
					}
					atcnt++;
					if (atcnt >= 180) {
						atcnt = 0;
						bcnt = 0;
						atseq = 1;
					}
					break;
				case 1:
					//atack 1-2
					//main bullet process
					bcnt++;
					if (bcnt >= 32) {	//main bullet interval
						bcnt = 0;
						//main bullet shot
						this.generateMainBullet ();
					}
					//sub bullet process
					const float sbl_spd6 = 0.47f;
					//bullet1 r1
					float xoffset1;
					float yoffset1;
					//bullet6 l1
					float xoffset2;
					float yoffset2;
					//bullet1 r1
					xoffset1 = (Mathf.Cos ((cR - r_offset + 130.0f) * Mathf.Deg2Rad) * 1.65f);
					yoffset1 = (Mathf.Sin ((cR - r_offset + 130.0f) * Mathf.Deg2Rad) * 1.65f);
					//bullet6 l1
					xoffset2 = (Mathf.Cos ((cR - r_offset - 130.0f) * Mathf.Deg2Rad) * 1.65f);
					yoffset2 = (Mathf.Sin ((cR - r_offset - 130.0f) * Mathf.Deg2Rad) * 1.65f);
					//adjust at game level
					int intmin = 0;
					int intmid = 0;
					int intmax = 0;
					if (mc.gameLevel == mc.gameLevelEasy) {
						intmin = 7;
						intmid = 32;
						intmax = 15;
					} else if (mc.gameLevel == mc.gameLevelNormal) {
						intmin = 5;
						intmid = 20;
						intmax = 25;
					} else if (mc.gameLevel == mc.gameLevelHard) {
						intmin = 4;
						intmid = 36;
						intmax = 32;
					}
					if (atcnt == 30) {
						for (int i = 180; i <= 360; i = i + 6) {
							if (i % 40 <= intmax) {
								float bx = Mathf.Cos ((i) * Mathf.Deg2Rad) * 1.0f * sbl_spd6;
								float by = Mathf.Sin ((i) * Mathf.Deg2Rad) * 1.0f * sbl_spd6;
								mc.generateEnemyBullet110 (1, cashTransform.position.x, cashTransform.position.y, xoffset2, yoffset2, bx, by);
							}
						}
					}
					if (atcnt == 50) {
						for (int i = 180; i <= 360; i = i + 6) {
							if ((i % 40 <= intmin) || (i % 40 >= intmid)) {
								float bx = Mathf.Cos ((i) * Mathf.Deg2Rad) * 1.0f * sbl_spd6;
								float by = Mathf.Sin ((i) * Mathf.Deg2Rad) * 1.0f * sbl_spd6;
								mc.generateEnemyBullet110 (1, cashTransform.position.x, cashTransform.position.y, xoffset1, yoffset1, bx, by);
							}
						}
					}
					atcnt++;
					if (atcnt >= 80) {
						atcnt = 0;
						atcnt5++;
						if (atcnt5 >= 3) {
							atcnt5 = 0;
							bcnt = 0;
							atseq = 2;
						}
					}
					break;
				case 2:
					//atack 1-3
					//main bullet process
					bcnt++;
					if (bcnt >= 32) {	//main bullet interval
						bcnt = 0;
						//main bullet shot
						this.generateMainBullet ();
					}
					//sub bullet process
					float xss;
					float yss;
					float atcnt10;
					const float sbl_spd2 = 0.47f;
					const float sbl_spd3 = 0.47f;
					//bullet1 r1
					xoffset1 = (Mathf.Cos ((cR - r_offset + 130.0f) * Mathf.Deg2Rad) * 1.65f);
					yoffset1 = (Mathf.Sin ((cR - r_offset + 130.0f) * Mathf.Deg2Rad) * 1.65f);
					//bullet6 l1
					xoffset2 = (Mathf.Cos ((cR - r_offset - 130.0f) * Mathf.Deg2Rad) * 1.65f);
					yoffset2 = (Mathf.Sin ((cR - r_offset - 130.0f) * Mathf.Deg2Rad) * 1.65f);
					atcnt10 = atcnt / 2;
					if (atcnt < 300) {
						if (atcnt % 12 == 0) {
							intmax = 0;
							if (mc.gameLevel == mc.gameLevelEasy) {
								intmax = 120;
							} else if (mc.gameLevel == mc.gameLevelNormal) {
								intmax = 60;
							} else if (mc.gameLevel == mc.gameLevelHard) {
								intmax = 45;
							}
							for (int i = 0; i <= 360; i = i + intmax) {
								xss = Mathf.Cos ((i + atcnt10) * Mathf.Deg2Rad) * 1.0f * sbl_spd2;
								yss = Mathf.Sin ((i + atcnt10) * Mathf.Deg2Rad) * 1.0f * sbl_spd2;
								mc.generateEnemyBullet130 (1, cashTransform.position.x, cashTransform.position.y, xoffset1, yoffset1, xss, yss);
								mc.generateEnemyBullet130 (1, cashTransform.position.x, cashTransform.position.y, xoffset2, yoffset2, xss, yss);
								xss = Mathf.Cos ((i - atcnt10) * Mathf.Deg2Rad) * 1.0f * sbl_spd3;
								yss = Mathf.Sin ((i - atcnt10) * Mathf.Deg2Rad) * 1.0f * sbl_spd3;
								mc.generateEnemyBullet130 (1, cashTransform.position.x, cashTransform.position.y, xoffset1, yoffset1, xss, yss);
								mc.generateEnemyBullet130 (1, cashTransform.position.x, cashTransform.position.y, xoffset2, yoffset2, xss, yss);
							}
						}
					}
					atcnt++;
					if (atcnt >= 360) {
						atcnt = 0;
						bcnt = 0;
						atseq = 3;
					}
					break;
				case 3:
					//atack 1-4
					//main bullet process
					bcnt++;
					if (bcnt >= 20) {	//main bullet interval
						bcnt = 0;
						//main bullet shot
						this.generateMainBullet ();
					}
					//sub bullet process
					//bullet1 r1
					xoffset1 = (Mathf.Cos ((cR - r_offset + 130.0f) * Mathf.Deg2Rad) * 1.65f);
					yoffset1 = (Mathf.Sin ((cR - r_offset + 130.0f) * Mathf.Deg2Rad) * 1.65f);
					//bullet6 l1
					xoffset2 = (Mathf.Cos ((cR - r_offset - 130.0f) * Mathf.Deg2Rad) * 1.65f);
					yoffset2 = (Mathf.Sin ((cR - r_offset - 130.0f) * Mathf.Deg2Rad) * 1.65f);
					const float sbl_spd4 = 0.43f;
					const float sbl_spd44 = 0.53f;
					atcnt10 = atcnt / 2;
					if (atcnt < 360) {
						intmax = 0;
						if (mc.gameLevel == mc.gameLevelEasy) {
							intmax = 36;
						} else if (mc.gameLevel == mc.gameLevelNormal) {
							intmax = 52;
						} else if (mc.gameLevel == mc.gameLevelHard) {
							intmax = 56;
						}
						if( (atcnt % 10 == 0) && (((atcnt % 60) >= 8) && ((atcnt % 60) <= intmax)) ){
							mc.generateEnemyBullet120 (-4-(atcnt10), sbl_spd44, cashTransform.position.x, cashTransform.position.y, xoffset1, yoffset1);
							mc.generateEnemyBullet120 (-4+(atcnt10), sbl_spd4, cashTransform.position.x, cashTransform.position.y, xoffset2, yoffset2);
							mc.generateEnemyBullet120 (4-(atcnt10), sbl_spd4, cashTransform.position.x, cashTransform.position.y, xoffset1, yoffset1);
							mc.generateEnemyBullet120 (4+(atcnt10), sbl_spd44, cashTransform.position.x, cashTransform.position.y, xoffset2, yoffset2);
							mc.generateEnemyBullet120 (86-(atcnt10), sbl_spd44, cashTransform.position.x, cashTransform.position.y, xoffset1, yoffset1);
							mc.generateEnemyBullet120 (86+(atcnt10), sbl_spd4, cashTransform.position.x, cashTransform.position.y, xoffset2, yoffset2);
							mc.generateEnemyBullet120 (94-(atcnt10), sbl_spd4, cashTransform.position.x, cashTransform.position.y, xoffset1, yoffset1);
							mc.generateEnemyBullet120 (94+(atcnt10), sbl_spd44, cashTransform.position.x, cashTransform.position.y, xoffset2, yoffset2);
							mc.generateEnemyBullet120 (176-(atcnt10), sbl_spd44, cashTransform.position.x, cashTransform.position.y, xoffset1, yoffset1);
							mc.generateEnemyBullet120 (176+(atcnt10), sbl_spd4, cashTransform.position.x, cashTransform.position.y, xoffset2, yoffset2);
							mc.generateEnemyBullet120 (184-(atcnt10), sbl_spd4, cashTransform.position.x, cashTransform.position.y, xoffset1, yoffset1);
							mc.generateEnemyBullet120 (184+(atcnt10), sbl_spd44, cashTransform.position.x, cashTransform.position.y, xoffset2, yoffset2);
							mc.generateEnemyBullet120 (266-(atcnt10), sbl_spd44, cashTransform.position.x, cashTransform.position.y, xoffset1, yoffset1);
							mc.generateEnemyBullet120 (266+(atcnt10), sbl_spd4, cashTransform.position.x, cashTransform.position.y, xoffset2, yoffset2);
							mc.generateEnemyBullet120 (274-(atcnt10), sbl_spd4, cashTransform.position.x, cashTransform.position.y, xoffset1, yoffset1);
							mc.generateEnemyBullet120 (274+(atcnt10), sbl_spd44, cashTransform.position.x, cashTransform.position.y, xoffset2, yoffset2);
						}
					}
					atcnt++;
					if (atcnt >= 420) {
						atcnt = 0;
						atcnt3 = 0;
						atcnt4 = 0;
						bcnt = 0;
						atseq = 4;
					}
					break;
				case 4:
					//atack 1-5
					//main bullet process
					bcnt++;
					if (bcnt >= 24) {	//main bullet interval
						bcnt = 0;
						//main bullet shot
						this.generateMainBullet ();
					}
					//sub bullet process
					float sbl_spd5 = 0;
					if (mc.gameLevel == mc.gameLevelEasy) {
						sbl_spd5 = 0.83f;
					} else if (mc.gameLevel == mc.gameLevelNormal) {
						sbl_spd5 = 0.86f;
					} else if (mc.gameLevel == mc.gameLevelHard) {
						sbl_spd5 = 0.89f;
					}
					float xx1 = 0;
					float yy1 = 0;
					float xx2 = 0;
					float yy2 = 0;
					//bullet1 r1
					xoffset1 = (Mathf.Cos ((cR - r_offset + 130.0f) * Mathf.Deg2Rad) * 1.65f);
					yoffset1 = (Mathf.Sin ((cR - r_offset + 130.0f) * Mathf.Deg2Rad) * 1.65f);
					//bullet6 l1
					xoffset2 = (Mathf.Cos ((cR - r_offset - 130.0f) * Mathf.Deg2Rad) * 1.65f);
					yoffset2 = (Mathf.Sin ((cR - r_offset - 130.0f) * Mathf.Deg2Rad) * 1.65f);
					if (atcnt <= 265) {
						if (atcnt4 == 0) {
							if (atcnt3 == 0) {
								xdistance2 = (playerCtr.transform.position.x) - (cashTransform.position.x + xoffset1);	//player,enemy x distance
								ydistance2 = (playerCtr.transform.position.y) - (cashTransform.position.y + yoffset1);	//player,enemy y distance
							} else {
								xdistance2 = (playerCtr.transform.position.x) - (cashTransform.position.x + xoffset2);	//player,enemy x distance
								ydistance2 = (playerCtr.transform.position.y) - (cashTransform.position.y + yoffset2);	//player,enemy y distance
							}
							if ((xdistance2 == 0) && (ydistance2 == 0)) {	//for zero exception
								xdistance2 = 0.0001f;
							}
							direction2 = Mathf.Atan2 (ydistance2, xdistance2);	//distance -> direction
							direction2 = direction2 * Mathf.Rad2Deg;
							doffset1 = 0;
							doffset2 = 0;
							atcnt2 = 3.0f;
						}
						if ((atcnt4 >= 0) && (atcnt4 < 10)) {
							doffset1 = doffset1 + atcnt2;
							doffset2 = doffset2 - atcnt2;
							atcnt2 = atcnt2 - 0.3f;
						}
						if ((atcnt4 >= 10) && (atcnt4 < 20)) {
							atcnt2 = atcnt2 + 0.3f;
							doffset1 = doffset1 - atcnt2;
							doffset2 = doffset2 + atcnt2;
						}
						if ((atcnt4 % 2 == 0) && (atcnt4 < 20)) {
							xx1 = Mathf.Cos ((direction2 + doffset1) * Mathf.Deg2Rad) * 1.0f * sbl_spd5;
							yy1 = Mathf.Sin ((direction2 + doffset1) * Mathf.Deg2Rad) * 1.0f * sbl_spd5;
							xx2 = Mathf.Cos ((direction2 + doffset2) * Mathf.Deg2Rad) * 1.0f * sbl_spd5;
							yy2 = Mathf.Sin ((direction2 + doffset2) * Mathf.Deg2Rad) * 1.0f * sbl_spd5;
							if (atcnt3 == 0) {
								mc.generateEnemyBullet100 (1, cashTransform.position.x, cashTransform.position.y, xoffset1, yoffset1, xx1, yy1);
								mc.generateEnemyBullet100 (1, cashTransform.position.x, cashTransform.position.y, xoffset1, yoffset1, xx2, yy2);
							}else{
								mc.generateEnemyBullet100 (1, cashTransform.position.x, cashTransform.position.y, xoffset2, yoffset2, xx1, yy1);
								mc.generateEnemyBullet100 (1, cashTransform.position.x, cashTransform.position.y, xoffset2, yoffset2, xx2, yy2);
							}
						}
					}
					atcnt4++;
					if (atcnt4 >= 55) {
						atcnt4 = 0;
						atcnt3++;
						if (atcnt3 >= 2) {
							atcnt3 = 0;
						}
					}
					atcnt++;
					if (atcnt >= 295) {
						bcnt = 0;
						sbcnt = 0;
						atcnt = 0;
						atseq = 0;
					}
					break;
				default:
					break;
				}
				//enemy500 hit point check -> next atack 
				if (e500Ctr.getHp() <= (eHpInitial*0.20f)) {
					//next seq
					bcnt = 0;
					sbcnt = 0;
					at2time = 100;
					movseq ++;
				}
				break;
			case 3:
				//atack2
				float rspdk = 4.6f;	//遅いほうが弾間隔狭く難易度高い
				//adjust at game level
				if (mc.gameLevel == mc.gameLevelEasy) {
					rspdk = rspdk - 0.0f;
				} else if (mc.gameLevel == mc.gameLevelNormal) {
					rspdk = rspdk - 0.7f;
				} else if (mc.gameLevel == mc.gameLevelHard) {
					rspdk = rspdk - 1.2f;
				}
				//rotate
				cR = cR + (rspd * rspdk);	//難易度調整はここで 回転スピード
				if (cR >= 360) {
					cR = cR - 360;
				}
				if (cR <= -360) {
					cR = cR + 360;
				}
				cashTransform.rotation = Quaternion.Euler (new Vector3 (0, 0, cR));
				//play se
				//rotate
				mc.playSound(mc.se_bossrotate);
				if (at2time >= 1) {	//main bullet interval at start rotate
					at2time--;
				}
				if (at2time <= 0) {
					at2time = 0;
					//main bullet process
					bcnt++;
					if (bcnt >= 2) {	//main bullet interval	//難易度調整はここで
						bcnt = 0;
						//generate bullet
						//generate position bullet 1,2
						float xpos1 = Mathf.Cos ((cR - r_offset - 18.0f) * Mathf.Deg2Rad) * 1.85f;
						float ypos1 = Mathf.Sin ((cR - r_offset - 18.0f) * Mathf.Deg2Rad) * 1.85f;
						float xpos2 = Mathf.Cos ((cR - r_offset + 18.0f) * Mathf.Deg2Rad) * 1.85f;
						float ypos2 = Mathf.Sin ((cR - r_offset + 18.0f) * Mathf.Deg2Rad) * 1.85f;
						//x,y speed
						float xs = Mathf.Cos ((cR - r_offset) * Mathf.Deg2Rad) * 1.0f;	//x speed * base speed
						float ys = Mathf.Sin ((cR - r_offset) * Mathf.Deg2Rad) * 1.0f;	//y speed * base speed
						//genarate
						//bullet1
						mc.generateEnemyBullet110(1, cashTransform.position.x, cashTransform.position.y, xpos1, ypos1, xs*mbl_spd2, ys*mbl_spd2 );
						//bullet2
						mc.generateEnemyBullet110(1, cashTransform.position.x, cashTransform.position.y, xpos2, ypos2, xs*mbl_spd2, ys*mbl_spd2 );
					}
					//sub bullet process
					float xoffset1;
					float yoffset1;
					float xoffset2;
					float yoffset2;
					float xoffset3;
					float yoffset3;
					float xoffset4;
					float yoffset4;
					float xoffset5;
					float yoffset5;
					float xoffset6;
					float yoffset6;
					//bullet1 r1
					xoffset1 = (Mathf.Cos ((cR - r_offset + 130.0f) * Mathf.Deg2Rad) * 1.65f);
					yoffset1 = (Mathf.Sin ((cR - r_offset + 130.0f) * Mathf.Deg2Rad) * 1.65f);
					//bullet2 r2
					xoffset2 = (Mathf.Cos ((cR - r_offset + 137.5f) * Mathf.Deg2Rad) * 1.51f);
					yoffset2 = (Mathf.Sin ((cR - r_offset + 137.5f) * Mathf.Deg2Rad) * 1.51f);
					//bullet3 r3
					xoffset3 = (Mathf.Cos ((cR - r_offset + 144.0f) * Mathf.Deg2Rad) * 1.37f);
					yoffset3 = (Mathf.Sin ((cR - r_offset + 144.0f) * Mathf.Deg2Rad) * 1.37f);
					//bullet4 l3
					xoffset4 = (Mathf.Cos ((cR - r_offset - 144.0f) * Mathf.Deg2Rad) * 1.37f);
					yoffset4 = (Mathf.Sin ((cR - r_offset - 144.0f) * Mathf.Deg2Rad) * 1.37f);
					//bullet5 l2
					xoffset5 = (Mathf.Cos ((cR - r_offset - 137.5f) * Mathf.Deg2Rad) * 1.51f);
					yoffset5 = (Mathf.Sin ((cR - r_offset - 137.5f) * Mathf.Deg2Rad) * 1.51f);
					//bullet6 l1
					xoffset6 = (Mathf.Cos ((cR - r_offset - 130.0f) * Mathf.Deg2Rad) * 1.65f);
					yoffset6 = (Mathf.Sin ((cR - r_offset - 130.0f) * Mathf.Deg2Rad) * 1.65f);
					sbcnt++;
					int sbcntmax = 60;
					//adjust at game level
					if (mc.gameLevel == mc.gameLevelEasy) {
						sbcntmax = sbcntmax - 0;
					} else if (mc.gameLevel == mc.gameLevelNormal) {
						sbcntmax = sbcntmax - 42;
					} else if (mc.gameLevel == mc.gameLevelHard) {
						sbcntmax = sbcntmax - 54;
					}
					if (sbcnt >= sbcntmax) {	//sub bullet interval	//難易度調整はここで sub bullet頻度
						sbcnt = 0;
						if ( Random.Range(0,2)==0) {
							float ss;
							float smin=0.41f;
							float smax=0.57f;
							ss = Random.Range (smin, smax);
							mc.generateEnemyBullet100 (0, cashTransform.position.x, cashTransform.position.y, xoffset1, yoffset1, ss, ss);
							ss = Random.Range (smin, smax);
							mc.generateEnemyBullet100 (0, cashTransform.position.x, cashTransform.position.y, xoffset2, yoffset2, ss, ss);
							ss = Random.Range (smin, smax);
							mc.generateEnemyBullet100 (0, cashTransform.position.x, cashTransform.position.y, xoffset3, yoffset3, ss, ss);
							ss = Random.Range (smin, smax);
							mc.generateEnemyBullet100 (0, cashTransform.position.x, cashTransform.position.y, xoffset4, yoffset4, ss, ss);
							ss = Random.Range (smin, smax);
							mc.generateEnemyBullet100 (0, cashTransform.position.x, cashTransform.position.y, xoffset5, yoffset5, ss, ss);
							ss = Random.Range (smin, smax);
							mc.generateEnemyBullet100 (0, cashTransform.position.x, cashTransform.position.y, xoffset6, yoffset6, ss, ss);
						}
					}

				}
				break;
			case 4:
				//explosion effect1(stop)
				sr.color = new Color (0.8f, 0.8f, 0.8f, 0.8f);
				break;
			case 5:
				//enemy 500 term effect2(forward) and enemy510 destroy
				//objnum dec
				mc.decObj();
				//delet this
				Destroy( this.gameObject );
				//for safe
				movseq = -1;
				break;
			case 6:
				//enemy 500,510 term (for safe)
				break;
			case -1:	//(for safe) 
				//nop
				break;
			default:
				break;

			}

			//blink
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

			//move result process
			if ( (cashTransform.position.y > ymax) ||
				(cashTransform.position.y < ymin) ||
				(cashTransform.position.x < xmin) ||
				(cashTransform.position.x > xmax) ){
				movseq = -1;	//for safe
			}

		}
	}


	//private
	private void generateMainBullet(){
		//bullet interval(random)
		const float mbl_spd = 0.77f;
		const float r_offset = -90.0f;	//90 rotate offset (砲身の向きが+90度のため)
		float rr;
		int hp = e500Ctr.getHp ();	//難易度調整はここで main bullet 頻度
		if (hp > (eHpInitial * 0.76)) {
			rr = 6.4f;	//bullet interval level 1
		} else if (hp > (eHpInitial * 0.62)) {
			rr = 6.9f;	//bullet interval level 2
		} else {
			rr = 7.4f;	//bullet interval level 3
		}
		//adjust at game level
		if (mc.gameLevel == mc.gameLevelEasy) {
			rr = rr + 0.0f;
		} else if (mc.gameLevel == mc.gameLevelNormal) {
			rr = rr + 1.4f;
		} else if (mc.gameLevel == mc.gameLevelHard) {
			rr = rr + 1.8f;
		}
		float r = Random.Range (0, rr);
		//first bullet to player and main bullet disable
		if (fstb == false) {
			if (cR == tR) {
				fstb = true;
				r = 4.0f;	//first shot
			} else {
				r = 0.0f;	//shot disable
			}
		}
		//main bullet shot
		if ((r >= 4.0f) && (Mathf.Abs (tR - cR) < 40.0f)) {	//random and rotate(砲身何度以内で発射するか)
			//generate bullet
			//generate position bullet 1,2
			float xpos1 = Mathf.Cos ((cR - r_offset - 18.0f) * Mathf.Deg2Rad) * 1.85f;
			float ypos1 = Mathf.Sin ((cR - r_offset - 18.0f) * Mathf.Deg2Rad) * 1.85f;
			float xpos2 = Mathf.Cos ((cR - r_offset + 18.0f) * Mathf.Deg2Rad) * 1.85f;
			float ypos2 = Mathf.Sin ((cR - r_offset + 18.0f) * Mathf.Deg2Rad) * 1.85f;
			//x,y speed
			float xs = Mathf.Cos ((cR - r_offset) * Mathf.Deg2Rad) * 1.0f;	//x speed * base speed
			float ys = Mathf.Sin ((cR - r_offset) * Mathf.Deg2Rad) * 1.0f;	//y speed * base speed
			//genarate
			//bullet1
			mc.generateEnemyBullet110 (1, cashTransform.position.x, cashTransform.position.y, xpos1, ypos1, xs * mbl_spd, ys * mbl_spd);
			//bullet2
			mc.generateEnemyBullet110 (1, cashTransform.position.x, cashTransform.position.y, xpos2, ypos2, xs * mbl_spd, ys * mbl_spd);
		}
	}
		

	//public
	//set blink
	public void setBlink(){
		if (blinkcnt <= 0) {
			blinkcnt = 3;
		} else {
			ondamage = true;
		}
	}

	//this setting (enemy500 set)
	public void setStatus( int seq, float cx, float cy ){
		//movseq set
		movseq = seq;
		//color change
		if (movseq == 4) {
//			sr.color = new Color (215.0f/255.0f, 215.0f/255.0f, 215.0f/255.0f, 160.0f/255.0f);
		}
		//position set
		cashTransform.position = new Vector3 (cx, cy, 0.0f);
	}

	//set enemy500 controller
	public void setEnemy500Ctr(enemy500Controller e500ctr){
		e500Ctr = e500ctr;
	}

	//public
	public void setInitStatus(){
		//nop
	}

}
