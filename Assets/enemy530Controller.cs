using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy530Controller : MonoBehaviour {
	//public
	//public enemy535 controller Prefab
	public GameObject enemy535ControllerPrefab;

	//private
	//local const
	//x,y min/max
	const float xmin = -4.0f;
	const float xmax = 4.0f;
	const float ymin = -8.0f;
	const float ymax = 8.0f;
	//x,y speed base
	const float xspd = 0.004f;
	const float yspd = 0.004f;
	const float xspd2 = 0.008f;
	const float yspd2 = 0.008f;
	//base hit point
	const int basehitpoint = 8780;//8280;
	const int basehitpoint2 = 1260;//760;
//	const int basehitpoint = 300;
//	const int basehitpoint2 = 100;
	//score
	readonly int hitscore = 50;
	readonly int score = 100000;

	//system local
	int intervalCnt;	//interval count

	//component cash
	Transform cashTransform;
	SpriteRenderer sr;
	GameObject mainCtr;
	mainController mc;
	GameObject playerCtr;
	playerController plc;
	GameObject e535;
	enemy535Controller e535Ctr;

	//local
	//e516 exist
	bool exist_e535;

	//master seq
	int mstseq;

	//move seq
	int movseq;
	int movseq2;

	//mov seq cnt
	int movseqcnt;
	int movseqcnt2;

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
	int bcnt3;

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

		//enemy535
		e535 = Instantiate (enemy535ControllerPrefab) as GameObject;
		e535Ctr = e535.GetComponent<enemy535Controller> ();
		e535Ctr.setEnemy530Ctr( this );
		//e535 exist
		exist_e535 = true;

		//position init
		cashTransform.position = new Vector3 (0, ymin, 0);

		//master seq
		mstseq = 0;

		//move seq mode
		movseq = 0;
		movseq2 = 0;

		//move seq count
		movseqcnt = 0;
		movseqcnt2 = 0;

		//move
		mdir = 0;
		xx = 0;
		yy = 0.025f;
		tpos.x = 0.0f;
		tpos.y = 0.0f;

		//cdir
		cdir = 0;
		cdd = 4;

		//shot seq
		stseq = 99;
		stseq2 = 0;

		//bullet cnt
		bcnt = 0;
		bcnt2 = 0;
		bcnt3 = 0;

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
			//first move
			cashTransform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
			cashTransform.Translate (xx, yy, 0);
			if (cashTransform.position.y >= 2.5f) {
				xx = 0;
				yy = 0;
				movseqcnt++;
				if( movseqcnt >= 150 ){
					//tag
					this.tag = "enemy";
					e535.tag = "enemy";
					xx = xspd * 1.0f;
					yy = yspd * 1.0f;
					movseq = 1;
					movseq2 = 0;
					movseqcnt = 0;
					movseqcnt2 = 0;
					bcnt = 0;
					bcnt2 = 0;
					bcnt3 = 0;
					stseq = 10;//20;	//0	//10
				}
			}
			//to player x,y
			float xds, yds;
			Vector2 ppos = plc.getPlayerPos ();
			xds = (ppos.x) - (cashTransform.position.x);	//player,enemy x distance
			yds = (ppos.y) - (cashTransform.position.y);	//player,enemy y distance
			if ((xds == 0) && (yds == 0)) {	//for zero exception
				xds = 0.0001f;
			}
			mdir = Mathf.Atan2 (yds, xds) * Mathf.Rad2Deg;	//distance -> direction
			if (mdir < 0) {	//0-360
				mdir = mdir + 360.0f;
			}
			//offset
			mdir = mdir + 90;
			if (mdir >= 360) {
				mdir = mdir - 360;
			}
			//direction current -> target
			const float dspd = 4.0f;
			if ((mdir > cdir) && ((mdir - cdir) > dspd)) {
				if ((mdir - cdir) < 180) {
					cdir = cdir + dspd;
				} else {
					cdir = cdir - dspd;
				}
			} else if ((mdir < cdir) && ((cdir - mdir) > dspd)) {
				if ((cdir - mdir) < 180) {
					cdir = cdir - dspd;
				} else {
					cdir = cdir + dspd;
				}
			} else {
				cdir = mdir;
			}
			if (cdir >= 360) {
				cdir = cdir - 360;
			}
			if (cdir <= 0) {
				cdir = cdir + 360;
			}
			cashTransform.rotation = Quaternion.Euler (new Vector3 (0, 0, cdir));
			//setting enemy535
			if (exist_e535 == true) {
				e535Ctr.setStatus (movseq, xx, yy);
			}
			break;
		case 1:
			//move for atack
			const float xmax = 0.38f;
			const float xmin = -0.38f;
			const float ymax = 2.95f;
			const float ymin = 2.25f;
			//move
			cashTransform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
			cashTransform.Translate (xx, yy, 0);
			Vector3 pos = cashTransform.position;
			if ((pos.x >= xmax) || (pos.x <= xmin)) {
				xx = xx - (xx * 2.0f);
			}
			if ((pos.y >= ymax) || (pos.y <= ymin)) {
				yy = yy - (yy * 2.0f);
			}
			//rotate
			//wait
			const int movwait = 60;
			if ( movseqcnt < movwait ) {
				movseqcnt++;
			}
			if (movseqcnt >= movwait) {
				switch (movseq2) {
				case 0:
					//0 -> 360
					cdir = cdir + cdd;
					if (cdir >= 360) {
						movseqcnt = 0;
						movseq2 = 1;
					}
					break;
				case 1:
					//360 -> 270
					cdir = cdir - cdd;
					if (cdir <= 270) {
						movseqcnt = 0;
						movseq2 = 2;
					}
					break;
				case 2:
					//270 -> 360
					cdir = cdir + cdd;
					if (cdir >= 360) {
						movseqcnt = 0;
						movseq2 = 3;
					}
					break;
				case 3:
					//360 -> 450
					cdir = cdir + cdd;
					if (cdir >= 450) {
						movseqcnt = 0;
						movseq2 = 4;
					}
					break;
				case 4:
					//450 -> 360
					cdir = cdir - cdd;
					if (cdir <= 360) {
						movseqcnt = 0;
						movseq2 = 5;
					}
					break;
				case 5:
					//360 -> 0
					cdir = cdir - cdd;
					if (cdir <= 0) {
						movseqcnt = 0;
						movseq2 = 6;
					}
					break;
				case 6:
					//to player dir
					//to player x,y
					ppos = plc.getPlayerPos ();
					xds = (ppos.x) - (cashTransform.position.x);	//player,enemy x distance
					yds = (ppos.y) - (cashTransform.position.y);	//player,enemy y distance
					if ((xds == 0) && (yds == 0)) {	//for zero exception
						xds = 0.0001f;
					}
					mdir = Mathf.Atan2 (yds, xds) * Mathf.Rad2Deg;	//distance -> direction
					if (mdir < 0) {	//0-360
						mdir = mdir + 360.0f;
					}
					//offset
					mdir = mdir + 90;
					if (mdir >= 360) {
						mdir = mdir - 360;
					}
					//direction current -> target
					if ((mdir > cdir) && ((mdir - cdir) > dspd)) {
						if ((mdir - cdir) < 180) {
							cdir = cdir + dspd;
						} else {
							cdir = cdir - dspd;
						}
					} else if ((mdir < cdir) && ((cdir - mdir) > dspd)) {
						if ((cdir - mdir) < 180) {
							cdir = cdir - dspd;
						} else {
							cdir = cdir + dspd;
						}
					} else {
						cdir = mdir;
					}
					if (cdir >= 360) {
						cdir = cdir - 360;
					}
					if (cdir <= 0) {
						cdir = cdir + 360;
					}
					movseqcnt2++;
					if (movseqcnt2 >= 360) {
						movseqcnt2 = 0;
						movseqcnt = 0;
						if (mstseq == 0) {
							movseq2 = 0;
						} else {
							movseq2 = 6;
							movseq = 2;
						}
					}
					break;
				case 10:
					//wait -> 6
					if (cdir > 0) {
						cdir = cdir - cdd;
						if (cdir <= 0) {
							cdir = 0;
						}
					}
					movseqcnt++;
					if (movseqcnt >= 230) {
						this.tag = "enemy";
						mstseq = 1;
						xx = xspd2 * 1.0f;
						yy = yspd2 * 1.0f;
						movseqcnt = 60;
						movseqcnt2 = 0;
						movseq = 1;
						movseq2 = 6;
						stseq = 20;
					}
					break;
				default:
					break;
				}
			}
			cashTransform.rotation = Quaternion.Euler (new Vector3 (0, 0, cdir));
			//setting enemy535
			if (exist_e535 == true) {
				e535Ctr.setStatus (movseq, xx, yy);
			}
			break;
		case 2:
			//to player
			const float mspd = 0.16f;
			if (movseqcnt == 0) {
				//to player x,y
				ppos = plc.getPlayerPos ();
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
			cashTransform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
			cashTransform.Translate (xx, yy, 0);
			//rotate
			cashTransform.rotation = Quaternion.Euler (new Vector3 (0, 0, cdir));
			movseqcnt++;
			if( (cashTransform.position.x >= (tpos.x-0.2f)) && (cashTransform.position.x <= (tpos.x+0.2f)) && 
				(cashTransform.position.y >= (tpos.y-0.2f)) && (cashTransform.position.y <= (tpos.y+0.2f)) ){
				movseq = 3;
			}
			break;
		case 3:
			//to back
			cashTransform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
			cashTransform.Translate (xx*-1 , yy*-1, 0);
			cashTransform.rotation = Quaternion.Euler (new Vector3 (0, 0, cdir));
			movseqcnt--;
			if (movseqcnt <= 0) {
				xx = xspd2 * 1.0f;
				yy = yspd2 * 1.0f;
				movseqcnt = 0;
				movseq = 1;
			}
			break;
		case 10:
			//last explosion and move forward
			if (movseqcnt == 0) {
				//play se
				mc.playSound (mc.se_middlebossexplosion);
				//generate screen flash effect
				mc.generateScreenFlashEffect ();
				//generate explosion big effect
				mc.generateExplosionBigEffect ((cashTransform.position.x), (cashTransform.position.y));
				//generate shake effect
				mc.generateScreenShakeEffect (25);
			}
			movseqcnt++;
			//color
			sr.color = new Color (0.8f, 0.8f, 0.8f, 0.8f);
			//move
			cashTransform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
			cashTransform.Translate (0, -0.003f, 0);
			//explosion
			if (movseqcnt == 410) {
				//generate screen flash effect
				mc.generateScreenFlashEffect ();
			}
			if (movseqcnt == 415) {
				//slow motion start
				mc.setPauseMaskOn ();
				Time.timeScale = 0.23f;
			}
			if (movseqcnt == 418) {
				//generate explosion big effect
				mc.generateExplosionBigEffect ((cashTransform.position.x-1.0f), (cashTransform.position.y));
				mc.generateExplosionBigEffect ((cashTransform.position.x+1.0f), (cashTransform.position.y));
				mc.generateExplosionBigEffect ((cashTransform.position.x), (cashTransform.position.y-1.0f));
				mc.generateExplosionBigEffect ((cashTransform.position.x), (cashTransform.position.y+1.0f));
			}
			if (movseqcnt >= 420) {
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
				//generate screen flash effect
				mc.generateScreenFlashEffect ();
				//generate shake effect
				mc.generateScreenShakeEffect( 40 );
				//play se
				mc.playSound(mc.se_bossexplosion);
				//loop sound stop
				mc.stopLoopSe ();
			} else {
				//generate explosion
				//explosion 0
				if ((movseqcnt % 3 == 0) && (movseqcnt<=60)) {
					//generate burner effect
					float bx = 0.0f;
					float by = 0.0f;
					for (int i = 0; i < 7; i++) {
						bx = (Random.Range (-0.15f, 0.15f) * 2.4f);
						by = (Random.Range (-0.15f, 0.15f) * 2.4f);
						mc.generateBurner100Effect( cashTransform.position.x+bx, cashTransform.position.y+by, bx, by, 3.6f, 3.6f );
					}
				}
				//explosion 1
				const float x_offset2 = 2.6f;
				const float y_offset2 = 2.6f;
				expcnt++;
				if (expcnt >= 4) {
					expcnt = 0;
					if (Random.Range (0.0f, 2.5f) <= 1.9f) {
						//generate explosion middle effect
						float xofset = Random.Range ((x_offset2 * -1), x_offset2);
						float yofset = Random.Range ((y_offset2 * -1), y_offset2);
						mc.generateExplosionMiddleEffect ((cashTransform.position.x + xofset), (cashTransform.position.y + yofset));
						//generate burner effect
						for (int i = 0; i < 6; i++) {
							mc.generateBurner100Effect( cashTransform.position.x, cashTransform.position.y, (Random.Range(-0.15f,0.15f)*1.2f), (Random.Range(-0.15f,0.15f)*1.2f) );
						}
					}
				}
				//explosion 2
				if (movseqcnt % 2 == 0) {
					float exx = Mathf.Cos (expdir * Mathf.Deg2Rad) * 1.0f * 0.5f;
					float eyy = Mathf.Sin (expdir * Mathf.Deg2Rad) * 1.0f * 0.5f;
					float exx2 = Mathf.Cos ((expdir+60) * Mathf.Deg2Rad) * 1.0f * 0.5f;
					float eyy2 = Mathf.Sin ((expdir+60) * Mathf.Deg2Rad) * 1.0f * 0.5f;
					float exx3 = Mathf.Cos ((expdir+120) * Mathf.Deg2Rad) * 1.0f * 0.5f;
					float eyy3 = Mathf.Sin ((expdir+120) * Mathf.Deg2Rad) * 1.0f * 0.5f;
					float exx4 = Mathf.Cos ((expdir+180) * Mathf.Deg2Rad) * 1.0f * 0.5f;
					float eyy4 = Mathf.Sin ((expdir+180) * Mathf.Deg2Rad) * 1.0f * 0.5f;
					float exx5 = Mathf.Cos ((expdir+240) * Mathf.Deg2Rad) * 1.0f * 0.5f;
					float eyy5 = Mathf.Sin ((expdir+240) * Mathf.Deg2Rad) * 1.0f * 0.5f;
					float exx6 = Mathf.Cos ((expdir+300) * Mathf.Deg2Rad) * 1.0f * 0.5f;
					float eyy6 = Mathf.Sin ((expdir+300) * Mathf.Deg2Rad) * 1.0f * 0.5f;
					mc.generateExplosionMiddleEffect ((cashTransform.position.x+(exx*2)), (cashTransform.position.y+(eyy*2)), exx, eyy);
					mc.generateExplosionMiddleEffect ((cashTransform.position.x+(exx*2)), (cashTransform.position.y+(eyy*2)), exx*1.8f, eyy*1.8f);
					mc.generateExplosionMiddleEffect ((cashTransform.position.x+(exx2*2)), (cashTransform.position.y+(eyy2*2)), exx2, eyy2);
					mc.generateExplosionMiddleEffect ((cashTransform.position.x+(exx2*2)), (cashTransform.position.y+(eyy2*2)), exx2*1.8f, eyy2*1.8f);
					mc.generateExplosionMiddleEffect ((cashTransform.position.x+(exx3*2)), (cashTransform.position.y+(eyy3*2)), exx3, eyy3);
					mc.generateExplosionMiddleEffect ((cashTransform.position.x+(exx3*2)), (cashTransform.position.y+(eyy3*2)), exx3*1.8f, eyy3*1.8f);
					mc.generateExplosionMiddleEffect ((cashTransform.position.x+(exx4*2)), (cashTransform.position.y+(eyy4*2)), exx4, eyy4);
					mc.generateExplosionMiddleEffect ((cashTransform.position.x+(exx4*2)), (cashTransform.position.y+(eyy4*2)), exx4*1.8f, eyy4*1.8f);
					mc.generateExplosionMiddleEffect ((cashTransform.position.x+(exx5*2)), (cashTransform.position.y+(eyy5*2)), exx5, eyy5);
					mc.generateExplosionMiddleEffect ((cashTransform.position.x+(exx5*2)), (cashTransform.position.y+(eyy5*2)), exx5*1.8f, eyy5*1.8f);
					mc.generateExplosionMiddleEffect ((cashTransform.position.x+(exx6*2)), (cashTransform.position.y+(eyy6*2)), exx6, eyy6);
					mc.generateExplosionMiddleEffect ((cashTransform.position.x+(exx6*2)), (cashTransform.position.y+(eyy6*2)), exx6*1.8f, eyy6*1.8f);
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
			if (cdd < 40.0f) {
				cdd = cdd + 0.05f;
			}
			if (cdd >= 40.0f) {
				cdd = 40.0f;
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
				mc.generateExplosionBigEffect ((cashTransform.position.x-1.0f), (cashTransform.position.y));
				mc.generateExplosionBigEffect ((cashTransform.position.x+1.0f), (cashTransform.position.y));
				mc.generateExplosionBigEffect ((cashTransform.position.x), (cashTransform.position.y-1.0f));
				mc.generateExplosionBigEffect ((cashTransform.position.x), (cashTransform.position.y+1.0f));
				//generate shake effect
				mc.generateScreenShakeEffect( 60 );
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
					for (int i = 0; i < 15; i++) {
						xofset = Random.Range ((x_offset3 * -1), x_offset3);
						yofset = Random.Range ((y_offset3 * -1), y_offset3);
						mc.generateExplosionMiddleEffect ((cashTransform.position.x + xofset), (cashTransform.position.y + yofset), xofset, yofset);
					}
					//generate burner effect
					for (int i = 0; i < 15; i++) {
						mc.generateBurner100Effect( cashTransform.position.x, cashTransform.position.y, (Random.Range(-0.35f,0.35f)*3.5f), (Random.Range(-0.35f,0.35f)*3.5f), 5.8f, 5.8f );
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
		case 20:
			//first explosion and move forward
			//color
			sr.color = new Color (1.0f, 1.0f, 1.0f, 1.0f);
			if (movseqcnt == 0) {
				//play se
				mc.playSound (mc.se_middlebossexplosion);
				//generate screen flash effect
				mc.generateScreenFlashEffect ();
				//generate explosion big effect
				mc.generateExplosionBigEffect ((cashTransform.position.x), (cashTransform.position.y));
				//generate shake effect
				mc.generateScreenShakeEffect( 15 );
			}
			movseqcnt++;
			//explosion
			if (movseqcnt == 368) {
				//e535 term
				exist_e535 = false;
				e535Ctr.setEnemy535Term();
				//generate explosion big effect
				mc.generateExplosionBigEffect ((cashTransform.position.x), (cashTransform.position.y));
			}
			if (movseqcnt >= 370) {
				//seq
				expcnt = 0;
				movseqcnt = 0;
				movseq = 21;
				//generate screen flash effect
				mc.generateScreenFlashEffect ();
				//generate shake effect
				mc.generateScreenShakeEffect( 18 );
				//play se
				mc.playSound(mc.se_middlebossexplosion);
			} else {
				//generate explosion
				//explosion 0
				if ((movseqcnt % 3 == 0) && (movseqcnt<=40)) {
					//generate burner effect
					float bx = 0.0f;
					float by = 0.0f;
					for (int i = 0; i < 5; i++) {
						bx = (Random.Range (-0.15f, 0.15f) * 2.4f);
						by = (Random.Range (-0.15f, 0.15f) * 2.4f);
						mc.generateBurner100Effect( cashTransform.position.x+bx, cashTransform.position.y+by, bx, by, 4.0f, 4.0f );
					}
				}
				//explosion 1
				const float x_offset2 = 2.1f;
				const float y_offset2 = 2.1f;
				expcnt++;
				if (expcnt >= 4) {
					expcnt = 0;
					if (Random.Range (0.0f, 2.5f) <= 1.8f) {
						//generate explosion middle effect
						float xofset = Random.Range ((x_offset2 * -1), x_offset2);
						float yofset = Random.Range ((y_offset2 * -1), y_offset2);
						mc.generateExplosionMiddleEffect ((cashTransform.position.x + xofset), (cashTransform.position.y + yofset));
						//generate burner effect
						for (int i = 0; i < 3; i++) {
							mc.generateBurner100Effect( cashTransform.position.x, cashTransform.position.y, (Random.Range(-0.15f,0.15f)*0.6f), (Random.Range(-0.15f,0.15f)*0.6f) );
						}
					}
				}
				//explosion 2
				if (movseqcnt % 2 == 0) {
					float exx = Mathf.Cos (expdir * Mathf.Deg2Rad) * 1.0f * 0.3f;
					float eyy = Mathf.Sin (expdir * Mathf.Deg2Rad) * 1.0f * 0.3f;
					float exx2 = Mathf.Cos ((expdir+120) * Mathf.Deg2Rad) * 1.0f * 0.3f;
					float eyy2 = Mathf.Sin ((expdir+120) * Mathf.Deg2Rad) * 1.0f * 0.3f;
					float exx3 = Mathf.Cos ((expdir+240) * Mathf.Deg2Rad) * 1.0f * 0.3f;
					float eyy3 = Mathf.Sin ((expdir+240) * Mathf.Deg2Rad) * 1.0f * 0.3f;
					mc.generateExplosionMiddleEffect ((cashTransform.position.x+(exx*2)), (cashTransform.position.y+(eyy*2)), exx, eyy);
					mc.generateExplosionMiddleEffect ((cashTransform.position.x+(exx*2)), (cashTransform.position.y+(eyy*2)), exx*1.1f, eyy*1.1f);
					mc.generateExplosionMiddleEffect ((cashTransform.position.x+(exx2*2)), (cashTransform.position.y+(eyy2*2)), exx2, eyy2);
					mc.generateExplosionMiddleEffect ((cashTransform.position.x+(exx2*2)), (cashTransform.position.y+(eyy2*2)), exx2*1.1f, eyy2*1.1f);
					mc.generateExplosionMiddleEffect ((cashTransform.position.x+(exx3*2)), (cashTransform.position.y+(eyy3*2)), exx3, eyy3);
					mc.generateExplosionMiddleEffect ((cashTransform.position.x+(exx3*2)), (cashTransform.position.y+(eyy3*2)), exx3*1.1f, eyy3*1.1f);
					expdir = expdir - 12;
					if (expdir <= 0) {
						expdir = expdir + 360;
					}
				}
			}
//			//voice play
//			if (movseqcnt == 20) {
//				mc.playSound (mc.vo200);
//			}
			//setting enemy535
			if (exist_e535 == true) {
				e535Ctr.setStatus (movseq, 0, 0);
			}
			break;
		case 21:
			//rebirth
			//hp
			if (movseqcnt == 0) {
				//play se
				mc.playSound( mc.se_bosswakeup );
				//game event wait release (for message and scroll)
				mc.releaseWait ();
				//enemy inital hitpoint
				eHpIntial = basehitpoint2;
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
				//display hit point
				mc.dispEnemyHitPoint( eHp, eHpIntial );
			}
			//wait
			movseqcnt++;
			if( movseqcnt >= 5 ){
				mstseq = 1;
				xx = xspd2 * 1.0f;
				yy = yspd2 * 1.0f;
				movseqcnt = 0;
				movseqcnt2 = 0;
				movseq = 1;
				movseq2 = 10;
			}
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

		//shot process
		int intvl = 0;
		int intvl2 = 0;
		int intmin = 0;
		int intmax = 0;
		const float bspd1 = 0.58f;
		const float bspd2 = 0.62f;
		const float bspd3 = 0.82f;
		const float bspd4 = 0.88f;
		const float bspd5 = 0.96f;
		if (movseq == 1) {
			switch (stseq) {
			case 0:
				//wait 1-0
				bcnt++;
				if (bcnt >= 25) {	//25
					bcnt = 0;
					bcnt2 = 0;
					bcnt3 = 0;
					stseq = 1;
				}
				break;
			case 1:
				//atack 1-1
				if (mc.gameLevel == mc.gameLevelEasy) {
					intvl = 20;
				} else if (mc.gameLevel == mc.gameLevelNormal) {
					intvl = 40;
				} else if (mc.gameLevel == mc.gameLevelHard) {
					intvl = 55;
				}
				if ((bcnt % 10 == 0) && (bcnt % 60 <= intvl)) {
					for (int i = 0; i <= 360; i = i + 20) {
						float bx = Mathf.Cos ((i + (bcnt2 / 2)) * Mathf.Deg2Rad) * bspd1;
						float by = Mathf.Sin ((i + (bcnt2 / 2)) * Mathf.Deg2Rad) * bspd1;
						mc.generateEnemyBullet110 (1, cashTransform.position.x, cashTransform.position.y, 0, 0, bx, by);
					}
					bcnt2 = bcnt2 + 1;
				}
				bcnt++;
				if (bcnt >= 180) {
					bcnt = 0;
					bcnt2 = 0;
					bcnt3 = 0;
					stseq = 2;
				}
				break;
			case 2:
				//atack 1-2
				if (mc.gameLevel == mc.gameLevelEasy) {
					intvl = 20;
				} else if (mc.gameLevel == mc.gameLevelNormal) {
					intvl = 40;
				} else if (mc.gameLevel == mc.gameLevelHard) {
					intvl = 55;
				}
				if ((bcnt % 10 == 0) && (bcnt % 60 <= intvl)) {
					for (int i = 0; i <= 360; i = i + 20) {
						float bx = Mathf.Cos ((i + (bcnt2 / 2)) * Mathf.Deg2Rad) * bspd1;
						float by = Mathf.Sin ((i + (bcnt2 / 2)) * Mathf.Deg2Rad) * bspd1;
						mc.generateEnemyBullet110 (1, cashTransform.position.x, cashTransform.position.y, 0, 0, bx, by);
					}
					bcnt2 = bcnt2 - 1;
				}
				bcnt++;
				if (bcnt >= 180) {
					bcnt = 0;
					bcnt2 = 0;
					bcnt3 = 0;
					stseq = 3;
				}
				break;
			case 3:
				//atack 1-3
				if (mc.gameLevel == mc.gameLevelEasy) {
					intvl = 20;
				} else if (mc.gameLevel == mc.gameLevelNormal) {
					intvl = 40;
				} else if (mc.gameLevel == mc.gameLevelHard) {
					intvl = 55;
				}
				if ((bcnt % 10 == 0) && (bcnt % 60 <= intvl)) {
					for (int i = 0; i <= 360; i = i + 15) {
						mc.generateEnemyBullet120 (i + bcnt2, bspd2, cashTransform.position.x, cashTransform.position.y, 0, 0);
					}
					bcnt2 = bcnt2 + 2;
				}
				if (mc.gameLevel != mc.gameLevelEasy) {
					if (bcnt % 60 == 30) {
						for (int i = 0; i <= 360; i = i + 120) {
							float bx = Mathf.Cos ((i + (bcnt2 + 2)) * Mathf.Deg2Rad) * bspd3;
							float by = Mathf.Sin ((i + (bcnt2 + 2)) * Mathf.Deg2Rad) * bspd3;
							mc.generateEnemyBullet110 (1, cashTransform.position.x, cashTransform.position.y, 0, 0, bx, by);
						}
					}
				}
				bcnt++;
				if (bcnt >= 180) {
					bcnt = 0;
					bcnt2 = 0;
					bcnt3 = 0;
					stseq = 4;
				}
				break;
			case 4:
				//atack 1-4
				if (mc.gameLevel == mc.gameLevelEasy) {
					intvl = 20;
				} else if (mc.gameLevel == mc.gameLevelNormal) {
					intvl = 40;
				} else if (mc.gameLevel == mc.gameLevelHard) {
					intvl = 55;
				}
				if ((bcnt % 10 == 0) && (bcnt % 60 <= intvl)) {
					for (int i = 0; i <= 360; i = i + 15) {
						mc.generateEnemyBullet120 (i + bcnt2, bspd2, cashTransform.position.x, cashTransform.position.y, 0, 0);
					}
					bcnt2 = bcnt2 - 2;
				}
				if (mc.gameLevel != mc.gameLevelEasy) {
					if (bcnt % 60 == 30) {
						for (int i = 0; i <= 360; i = i + 120) {
							float bx = Mathf.Cos ((i + (bcnt2 + 2)) * Mathf.Deg2Rad) * bspd3;
							float by = Mathf.Sin ((i + (bcnt2 + 2)) * Mathf.Deg2Rad) * bspd3;
							mc.generateEnemyBullet110 (1, cashTransform.position.x, cashTransform.position.y, 0, 0, bx, by);
						}
					}
				}
				bcnt++;
				if (bcnt >= 180) {
					bcnt = 0;
					bcnt2 = 0;
					bcnt3 = 0;
					stseq = 5;
				}
				break;
			case 5:
				//atack 1-5
				if (mc.gameLevel == mc.gameLevelEasy) {
					intvl = 20;
				} else if (mc.gameLevel == mc.gameLevelNormal) {
					intvl = 40;
				} else if (mc.gameLevel == mc.gameLevelHard) {
					intvl = 55;
				}
				if ((bcnt % 10 == 0) && (bcnt % 60 <= intvl)) {
					for (int i = 0; i <= 360; i = i + 20) {
						float bx = Mathf.Cos ((i + (bcnt2)) * Mathf.Deg2Rad) * bspd2;
						float by = Mathf.Sin ((i + (bcnt2)) * Mathf.Deg2Rad) * bspd2;
						mc.generateEnemyBullet110 (1, cashTransform.position.x, cashTransform.position.y, 0, 0, bx, by);
					}
					bcnt2 = bcnt2 + 4;
				}
				if (mc.gameLevel != mc.gameLevelEasy) {
					if (bcnt % 60 == 20) {
						for (int i = 0; i <= 360; i = i + 60) {
							float bx = Mathf.Cos ((i + (bcnt2 + 2)) * Mathf.Deg2Rad) * bspd3;
							float by = Mathf.Sin ((i + (bcnt2 + 2)) * Mathf.Deg2Rad) * bspd3;
							mc.generateEnemyBullet110 (1, cashTransform.position.x, cashTransform.position.y, 0, 0, bx, by);
						}
					}
				}
				bcnt++;
				if (bcnt >= 180) {
					bcnt = 0;
					bcnt2 = 0;
					bcnt3 = 0;
					stseq = 6;
				}
				break;
			case 6:
				//atack 1-6
				if (mc.gameLevel == mc.gameLevelEasy) {
					intvl = 20;
				} else if (mc.gameLevel == mc.gameLevelNormal) {
					intvl = 40;
				} else if (mc.gameLevel == mc.gameLevelHard) {
					intvl = 55;
				}
				if ((bcnt % 10 == 0) && (bcnt % 60 <= intvl)) {
					for (int i = 0; i <= 360; i = i + 20) {
						float bx = Mathf.Cos ((i + (bcnt2)) * Mathf.Deg2Rad) * bspd2;
						float by = Mathf.Sin ((i + (bcnt2)) * Mathf.Deg2Rad) * bspd2;
						mc.generateEnemyBullet110 (1, cashTransform.position.x, cashTransform.position.y, 0, 0, bx, by);
					}
					bcnt2 = bcnt2 - 4;
				}
				if (mc.gameLevel != mc.gameLevelEasy) {
					if (bcnt % 60 == 20) {
						for (int i = 0; i <= 360; i = i + 60) {
							float bx = Mathf.Cos ((i + (bcnt2 + 2)) * Mathf.Deg2Rad) * bspd3;
							float by = Mathf.Sin ((i + (bcnt2 + 2)) * Mathf.Deg2Rad) * bspd3;
							mc.generateEnemyBullet110 (1, cashTransform.position.x, cashTransform.position.y, 0, 0, bx, by);
						}
					}
				}
				bcnt++;
				if (bcnt >= 180) {
					bcnt = 0;
					bcnt2 = 0;
					bcnt3 = 0;
					stseq = 7;
				}
				break;
			case 7:
				//atack 1-7
				if (mc.gameLevel == mc.gameLevelEasy) {
					intvl = 20;
				} else if (mc.gameLevel == mc.gameLevelNormal) {
					intvl = 40;
				} else if (mc.gameLevel == mc.gameLevelHard) {
					intvl = 55;
				}
				if ((bcnt % 10 == 0) && (bcnt % 60 <= intvl)) {
					for (int i = 0; i <= 60; i = i + 10) {
						float bx = Mathf.Cos ((i + (bcnt2)) * Mathf.Deg2Rad) * bspd2;
						float by = Mathf.Sin ((i + (bcnt2)) * Mathf.Deg2Rad) * bspd2;
						mc.generateEnemyBullet110 (1, cashTransform.position.x, cashTransform.position.y, 0, 0, bx, by);
					}
					for (int i = 180; i <= 240; i = i + 10) {
						float bx = Mathf.Cos ((i + (bcnt2)) * Mathf.Deg2Rad) * bspd2;
						float by = Mathf.Sin ((i + (bcnt2)) * Mathf.Deg2Rad) * bspd2;
						mc.generateEnemyBullet110 (1, cashTransform.position.x, cashTransform.position.y, 0, 0, bx, by);
					}
					bcnt2 = bcnt2 + 8;
				}
				if (mc.gameLevel != mc.gameLevelEasy) {
					if (bcnt % 20 == 10) {
						mc.generateEnemyBullet130 (0, cashTransform.position.x, cashTransform.position.y, 0, 0, bspd3, bspd3);
					}
				}
				bcnt++;
				if (bcnt >= 360) {
					bcnt = 0;
					bcnt2 = 0;
					bcnt3 = 0;
					stseq = 8;
				}
				break;
			case 8:
				//atack 1-8
				if (mc.gameLevel == mc.gameLevelEasy) {
					intvl = 20;
				} else if (mc.gameLevel == mc.gameLevelNormal) {
					intvl = 40;
				} else if (mc.gameLevel == mc.gameLevelHard) {
					intvl = 50;
				}
				if ((bcnt % 3 == 0) && (bcnt % 60 <= intvl)) {
					for (int i = 0; i <= 360; i = i + 90) {
						mc.generateEnemyBullet120 (i + bcnt2, bspd4, cashTransform.position.x, cashTransform.position.y, 0, 0);
					}
					bcnt2 = bcnt2 - 3;
				}
				if (mc.gameLevel != mc.gameLevelEasy) {
					if (bcnt % 30 == 5) {
						for (int i = 0; i <= 360; i = i + 60) {
							float bx = Mathf.Cos ((i + bcnt2 + 5) * Mathf.Deg2Rad) * bspd3;
							float by = Mathf.Sin ((i + bcnt2 + 5) * Mathf.Deg2Rad) * bspd3;
							mc.generateEnemyBullet110 (1, cashTransform.position.x, cashTransform.position.y, 0, 0, bx, by);
						}
					}
				}
				bcnt++;
				if (bcnt >= 360) {
					bcnt = 0;
					bcnt2 = 0;
					bcnt3 = 0;
					stseq = 9;
				}
				break;
			case 9:
				//atack 1-9
				if (mc.gameLevel == mc.gameLevelEasy) {
					intvl = 20;
				} else if (mc.gameLevel == mc.gameLevelNormal) {
					intvl = 40;
				} else if (mc.gameLevel == mc.gameLevelHard) {
					intvl = 50;
				}
				if ((bcnt % 3 == 0) && (bcnt % 60 <= intvl)) {
					for (int i = 0; i <= 360; i = i + 60) {
						mc.generateEnemyBullet120 (i + bcnt2, bspd4, cashTransform.position.x, cashTransform.position.y, 0, 0);
					}
					bcnt2 = bcnt2 - 2;
				}
				if (mc.gameLevel != mc.gameLevelEasy) {
					if (bcnt % 25 == 5) {
						for (int i = 0; i <= 360; i = i + 45) {
							float bx = Mathf.Cos ((i + bcnt2 + 5) * Mathf.Deg2Rad) * bspd3;
							float by = Mathf.Sin ((i + bcnt2 + 5) * Mathf.Deg2Rad) * bspd3;
							mc.generateEnemyBullet110 (1, cashTransform.position.x, cashTransform.position.y, 0, 0, bx, by);
						}
						mc.generateEnemyBullet110 (0, cashTransform.position.x, cashTransform.position.y, 0, 0, bspd4, bspd4);
					}
				}
				bcnt++;
				if (bcnt >= 360) {
					bcnt = 0;
					bcnt2 = 0;
					bcnt3 = 0;
					stseq = 0;
				}
				break;
			case 10:
				//first wait
				bcnt++;
				if (bcnt >= 60) {
					bcnt = 0;
					bcnt2 = 0;
					bcnt3 = 0;
					stseq = 0;
				}
				break;
			case 20:
				//wait 2-0
				bcnt++;
				if (bcnt >= 25) {	//25
					bcnt = 0;
					bcnt2 = 0;
					bcnt3 = 0;
					if ((float)((float)eHp / (float)eHpIntial) <= 0.5f) {
						stseq = 30;
					} else {
						stseq = 21;
					}
				}
				break;
			case 21:
				//atack 2-1
				if (mc.gameLevel == mc.gameLevelEasy) {
					intvl = 20;
				} else if (mc.gameLevel == mc.gameLevelNormal) {
					intvl = 40;
				} else if (mc.gameLevel == mc.gameLevelHard) {
					intvl = 55;
				}
				if ((bcnt % 8 == 0) && (bcnt % 60 <= intvl)) {
					for (int i = 0; i <= 360; i = i + 12) {
						float bx = Mathf.Cos ((i + (bcnt2 / 2)) * Mathf.Deg2Rad) * bspd4;
						float by = Mathf.Sin ((i + (bcnt2 / 2)) * Mathf.Deg2Rad) * bspd4;
						mc.generateEnemyBullet110 (1, cashTransform.position.x, cashTransform.position.y, 0, 0, bx, by);
					}
					bcnt2 = bcnt2 + 5;
				}
				if (mc.gameLevel != mc.gameLevelEasy) {
					if (bcnt % 30 == 5) {
						for (int i = 0; i <= 360; i = i + 45) {
							mc.generateEnemyBullet120 (i + bcnt3, bspd5, cashTransform.position.x, cashTransform.position.y, 0, 0);
						}
						bcnt3 = bcnt3 + 20;
						mc.generateEnemyBullet110 (0, cashTransform.position.x, cashTransform.position.y, 0, 0, bspd5, bspd5);
					}
				}
				bcnt++;
				if (bcnt >= 360) {
					bcnt = 0;
					bcnt2 = 0;
					bcnt3 = 0;
					stseq = 22;
				}
				break;
			case 22:
				//atack 2-2
				if (mc.gameLevel == mc.gameLevelEasy) {
					intvl = 20;
				} else if (mc.gameLevel == mc.gameLevelNormal) {
					intvl = 40;
				} else if (mc.gameLevel == mc.gameLevelHard) {
					intvl = 55;
				}
				if ((bcnt % 6 == 0) && (bcnt % 60 <= intvl)) {
					for (int i = 60; i <= 120; i = i + 6) {
						mc.generateEnemyBullet120 (i - bcnt2, bspd5, cashTransform.position.x, cashTransform.position.y, 0, 0);
					}
					for (int i = 240; i <= 300; i = i + 6) {
						mc.generateEnemyBullet120 (i - bcnt2, bspd5, cashTransform.position.x, cashTransform.position.y, 0, 0);
					}
				}
				if (bcnt % 60 == 20) {
					bcnt2 = bcnt2 + 60;
				}
				if (mc.gameLevel == mc.gameLevelHard) {
					if (bcnt % 60 == 10) {
						mc.generateEnemyBullet110 (0, cashTransform.position.x, cashTransform.position.y, 0, 0, bspd3, bspd3);
					}
				}
				bcnt++;
				if (bcnt >= 360) {
					bcnt = 0;
					bcnt2 = 0;
					bcnt3 = 0;
					stseq = 23;
				}
				break;
			case 23:
				//atack 2-3
				if (mc.gameLevel == mc.gameLevelEasy) {
					intvl = 20;
					intmax = 5;
				} else if (mc.gameLevel == mc.gameLevelNormal) {
					intvl = 40;
					intmax = 16;
				} else if (mc.gameLevel == mc.gameLevelHard) {
					intvl = 55;
					intmax = 20;
				}
				if ((bcnt % 22 == 0) && (bcnt % 60 <= intvl)) {
					for (int i = 0; i <= 360; i = i + 5) {
						if (i % 30 <= intmax) {
							float bx = Mathf.Cos ((i + (bcnt2)) * Mathf.Deg2Rad) * bspd4;
							float by = Mathf.Sin ((i + (bcnt2)) * Mathf.Deg2Rad) * bspd4;
							mc.generateEnemyBullet110 (1, cashTransform.position.x, cashTransform.position.y, 0, 0, bx, by);
						}
					}
					bcnt2 = bcnt2 + 4;
				}
				if (mc.gameLevel != mc.gameLevelEasy) {
					if (bcnt % 45 == 40) {
						for (int i = 0; i <= 360; i = i + 60) {
							float bx = Mathf.Cos ((i + bcnt3) * Mathf.Deg2Rad) * bspd2;
							float by = Mathf.Sin ((i + bcnt3) * Mathf.Deg2Rad) * bspd2;
							mc.generateEnemyBullet130 (1, cashTransform.position.x, cashTransform.position.y, 0, 0, bx, by);
						}
						bcnt3 = bcnt3 + 20;
					}
				}
				bcnt++;
				if (bcnt >= 360) {
					bcnt = 0;
					bcnt2 = 0;
					bcnt3 = 0;
					stseq = 24;
				}
				break;
			case 24:
				//atack 2-4
				if (mc.gameLevel == mc.gameLevelEasy) {
					intvl = 40;
				} else if (mc.gameLevel == mc.gameLevelNormal) {
					intvl = 80;
				} else if (mc.gameLevel == mc.gameLevelHard) {
					intvl = 110;
				}
				if ((bcnt % 2 == 0) && (bcnt % 120 <= intvl)) {
					for (int i = 0; i <= 360; i = i + 60) {
						float bx = Mathf.Cos ((i + (bcnt2)) * Mathf.Deg2Rad) * bspd4;
						float by = Mathf.Sin ((i + (bcnt2)) * Mathf.Deg2Rad) * bspd4;
						mc.generateEnemyBullet130 (1, cashTransform.position.x, cashTransform.position.y, 0, 0, bx, by);
					}
					bcnt2 = bcnt2 - 1;
				}
				if (mc.gameLevel != mc.gameLevelEasy) {
					if (bcnt % 30 == 5) {
						for (int i = 0; i <= 360; i = i + 45) {
							mc.generateEnemyBullet120 (i + bcnt3, bspd5, cashTransform.position.x, cashTransform.position.y, 0, 0);
						}
						bcnt3 = bcnt3 + 30;
						mc.generateEnemyBullet130 (0, cashTransform.position.x, cashTransform.position.y, 0, 0, bspd5, bspd5);
					}
				}
				bcnt++;
				if (bcnt >= 360) {
					bcnt = 0;
					bcnt2 = 0;
					bcnt3 = 0;
					stseq = 20;
				}
				break;
			case 30:
				//atack 2-2-1
				if (mc.gameLevel == mc.gameLevelEasy) {
					intvl = 20;
				} else if (mc.gameLevel == mc.gameLevelNormal) {
					intvl = 40;
				} else if (mc.gameLevel == mc.gameLevelHard) {
					intvl = 55;
				}
				if ((bcnt % 7 == 0) && (bcnt % 60 <= intvl)) {
					for (int i = 0; i <= 360; i = i + 9) {
						mc.generateEnemyBullet120 (i + bcnt2, bspd4, cashTransform.position.x, cashTransform.position.y, 0, 0);
					}
					if (bcnt % 120 <= 60) {
						bcnt2 = bcnt2 - 3;
					} else {
						bcnt2 = bcnt2 + 3;
					}
				}
				if (mc.gameLevel != mc.gameLevelEasy) {
					if (bcnt % 30 == 10) {
						for (int i = 0; i <= 360; i = i + 45) {
							float bx = Mathf.Cos ((i + bcnt2 + 5) * Mathf.Deg2Rad) * bspd3;
							float by = Mathf.Sin ((i + bcnt2 + 5) * Mathf.Deg2Rad) * bspd3;
							mc.generateEnemyBullet110 (1, cashTransform.position.x, cashTransform.position.y, 0, 0, bx, by);
						}
					}
				}
				bcnt++;
				if (bcnt >= 360) {
					bcnt = 0;
					bcnt2 = 0;
					bcnt3 = 0;
					stseq = 31;
				}
				break;
			case 31:
				//atack 2-2-2
				if (mc.gameLevel == mc.gameLevelEasy) {
					intvl = 20;
				} else if (mc.gameLevel == mc.gameLevelNormal) {
					intvl = 40;
				} else if (mc.gameLevel == mc.gameLevelHard) {
					intvl = 55;
				}
				if ((bcnt % 8 == 0) && (bcnt % 60 <= intvl)) {
					for (int i = 40; i <= 140; i = i + 10) {
						float bx = Mathf.Cos ((i + bcnt2) * Mathf.Deg2Rad) * bspd4;
						float by = Mathf.Sin ((i + bcnt2) * Mathf.Deg2Rad) * bspd4;
						mc.generateEnemyBullet130 (1, cashTransform.position.x, cashTransform.position.y, 0, 0, bx, by);
					}
					for (int i = 220; i <= 320; i = i + 10) {
						float bx = Mathf.Cos ((i + bcnt3) * Mathf.Deg2Rad) * bspd4;
						float by = Mathf.Sin ((i + bcnt3) * Mathf.Deg2Rad) * bspd4;
						mc.generateEnemyBullet130 (1, cashTransform.position.x, cashTransform.position.y, 0, 0, bx, by);
					}
					bcnt2 = bcnt2 + 12;
					bcnt3 = bcnt3 - 13;
				}
				if (mc.gameLevel != mc.gameLevelEasy) {
					if ( (bcnt%3 == 0) && (bcnt % 30 >= 15) ) {
						mc.generateEnemyBullet120 (bcnt3, bspd5, cashTransform.position.x, cashTransform.position.y, 0, 0);
					}
				}
				bcnt++;
				if (bcnt >= 360) {
					bcnt = 0;
					bcnt2 = 0;
					bcnt3 = 0;
					stseq = 32;
				}
				break;
			case 32:
				//atack 2-2-3
				if (mc.gameLevel == mc.gameLevelEasy) {
					intvl = 20;
					intmax = 15;
				} else if (mc.gameLevel == mc.gameLevelNormal) {
					intvl = 40;
					intmax = 26;
				} else if (mc.gameLevel == mc.gameLevelHard) {
					intvl = 55;
					intmax = 30;
				}
				if ((bcnt % 20 == 0) && (bcnt % 60 <= intvl)) {
					for (int i = 180; i <= 360; i = i + 5) {
						if (i % 40 <= intmax) {
							float bx = Mathf.Cos ((i + (bcnt2)) * Mathf.Deg2Rad) * bspd4;
							float by = Mathf.Sin ((i + (bcnt2)) * Mathf.Deg2Rad) * bspd4;
							mc.generateEnemyBullet100 (1, cashTransform.position.x, cashTransform.position.y, 0, 0, bx, by);
						}
					}
					if (bcnt2 >= 5) {
						bcnt2 = bcnt2 - 5;
					} else {
						bcnt2 = bcnt2 + 5;
					}
				}
				if (mc.gameLevel != mc.gameLevelEasy) {
					if (bcnt % 45 == 40) {
						for (int i = 0; i <= 360; i = i + 60) {
							mc.generateEnemyBullet120 (i, bspd5, cashTransform.position.x, cashTransform.position.y, 0, 0);
						}
						bcnt3 = bcnt3 + 20;
					}
				}
				bcnt++;
				if (bcnt >= 180) {
					bcnt = 0;
					bcnt2 = 0;
					bcnt3 = 0;
					stseq = 33;
				}
				break;
			case 33:
				//atack 2-2-4
				if (mc.gameLevel == mc.gameLevelEasy) {
					intvl = 20;
				} else if (mc.gameLevel == mc.gameLevelNormal) {
					intvl = 40;
				} else if (mc.gameLevel == mc.gameLevelHard) {
					intvl = 55;
				}
				if ((bcnt % 4 == 0) && (bcnt % 60 <= intvl)) {
					for (int i = 180; i <= 360; i = i + 30) {
							mc.generateEnemyBullet120 (i + bcnt2, bspd4, cashTransform.position.x, cashTransform.position.y, 0, 0);
					}
				}
				if (bcnt % 60 == 59) {
					if (bcnt2 >= 5) {
						bcnt2 = bcnt2 - 5;
					} else {
						bcnt2 = bcnt2 + 5;
					}
				}
				if (mc.gameLevel != mc.gameLevelEasy) {
					if (bcnt % 45 == 40) {
						for (int i = 0; i <= 360; i = i + 20) {
							float bx = Mathf.Cos ((i + bcnt3) * Mathf.Deg2Rad) * bspd3;
							float by = Mathf.Sin ((i + bcnt3) * Mathf.Deg2Rad) * bspd3;
							mc.generateEnemyBullet110 (1, cashTransform.position.x, cashTransform.position.y, 0, 0, bx, by);
						}
						bcnt3 = bcnt3 + 20;
					}
				}
				bcnt++;
				if (bcnt >= 180) {
					bcnt = 0;
					bcnt2 = 0;
					bcnt3 = 0;
					stseq = 34;
				}
				break;
			case 34:
				//atack 2-2-5
				if (mc.gameLevel == mc.gameLevelEasy) {
					intvl = 20;
				} else if (mc.gameLevel == mc.gameLevelNormal) {
					intvl = 40;
				} else if (mc.gameLevel == mc.gameLevelHard) {
					intvl = 55;
				}
				if ((bcnt % 7 == 0) && (bcnt % 60 <= intvl)) {
					for (int i = 0; i <= 360; i = i + 12) {
						float bx = Mathf.Cos ((i + bcnt2) * Mathf.Deg2Rad) * bspd4;
						float by = Mathf.Sin ((i + bcnt2) * Mathf.Deg2Rad) * bspd4;
						mc.generateEnemyBullet130 (1, cashTransform.position.x, cashTransform.position.y, 0, 0, bx, by);
					}
					if (bcnt % 120 <= 60) {
						bcnt2 = bcnt2 - 3;
					} else {
						bcnt2 = bcnt2 + 3;
					}
				}
				if (mc.gameLevel != mc.gameLevelEasy) {
					if (bcnt % 30 == 10) {
						for (int i = 0; i <= 360; i = i + 45) {
							float bx = Mathf.Cos ((i + bcnt2 + 5) * Mathf.Deg2Rad) * bspd3;
							float by = Mathf.Sin ((i + bcnt2 + 5) * Mathf.Deg2Rad) * bspd3;
							mc.generateEnemyBullet110 (1, cashTransform.position.x, cashTransform.position.y, 0, 0, bx, by);
						}
					}
				}
				bcnt++;
				if (bcnt >= 360) {
					bcnt = 0;
					bcnt2 = 0;
					bcnt3 = 0;
					stseq = 30;
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
			mc.generateWipe2 ();
			//tag
			this.tag = "unavailableEnemy";
			//color
			if (mstseq == 1) {
				sr.color = new Color (0.8f, 0.8f, 0.8f, 0.8f);
			}
			//bgm stop
			if (mstseq == 1) {
				mc.stopBgm ();
			}
			//exp cnt
			expcnt = 0;
			if (mstseq == 0) {
				//e535 tag
				e535Ctr.tag = "unavailableEnemy";
			}
			//term effect seq
			if (mstseq == 0) {
				movseq = 20;
				movseqcnt = 0;
			} else if (mstseq == 1) {
				movseq = 10;
				movseqcnt = 0;
			}
			//st seq
			stseq = -1;
			//score
			if (mstseq == 1) {
				//add game score
				mc.addGameScore (score);
				//sub message display
				mc.dispSubMessage ((cashTransform.position.x + 0.0f), (cashTransform.position.y - 0.3f), 0.0f, -1.5f, score.ToString ("D") + "pts!!", 2);
			}
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
			if (damagecnt >= 40) {
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
		for (int i = 0; i < 48; i++) {
			mc.generatePowerup100 (mc.puType_score, Random.Range (-3.0f, +3.0f), 4.4f + Random.Range (-0.5f, +0.5f));
		}
	}

	//public
	public void setInitStatus( int itm ){	//item set
		this.item = itm;	
	}

	//set enemy535 damage
	public void set_e535damage( int damage){
		if (damage >= 10) {
			damage = 10;
		}
		this.enemyHit (damage);
	}

}
