using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBase100Controller : MonoBehaviour {
	//public

	//private
	//local const
	//x,y min/max
	const float xmin = -4.0f;
	const float xmax = 4.0f;
	const float ymin = -5.6f;
	const float ymax = 15f;
	//x,y speed base
	const float xspd = 0.0f;
	const float yspd = 0.225f;
	//scale base
	const float xsbase = 9.3f;
	const float ysbase = 5.98f;
	//player offset
	const float py_offset = -0.855f;

	//system local
	int intervalCnt;	//interval count

	//component cash
	Transform cashTransform;
	GameObject mainCtr;
	mainController mc;
	GameObject playerCtr;
	playerController plc;

	//local
	//type
	int type;

	//mov seq
	int movseq;
	int movseqcnt;	//for type 1

	//move speed
	float xx;
	float yy;
	float yyy;

	//scale
	float scl;
	float ss;
	float sss;

	//player
	float pyy;	//for type 0
	Vector2 ppos;	//for type 1

	//burner cnt
	int bncnt;

	// Use this for initialization
	void Start () {
		//system init
		intervalCnt = 0;

		//cash
		//transform cash
		cashTransform = transform;

		//maincontroller
		mainCtr = GameObject.Find ("mainController");
		mc = mainCtr.GetComponent<mainController> ();

		//playercontroller
		playerCtr = GameObject.Find ("playerController");
		plc = playerCtr.GetComponent<playerController> ();

		//position init
		cashTransform.position = new Vector3 (0.0f, ymin, 0.0f);

		//type
		//(set from parent objects)
//		type = 0;

		//mov seq
		if (type == 0) {
			movseq = 0;
		} else if ( type == 1 ){
			movseq = 20;
		}
		movseqcnt = 0;

		//move speed
		if (type == 0) {
			xx = 0;
			yy = 0.445f;
			yyy = 0;
		} else if ( type == 1 ){
			xx = 0;
			yy = 0.345f;
			yyy = 0;
		}

		//scale
		if (type == 0) {
			scl = 5.9f;
			ss = 0.04775f;
			sss = 0.0001f;
		} else if (type == 1) {
			scl = 1.0f;
			ss = 0.0f;
			sss = 0.0f;
		}

		//player
		pyy = -0.0155f;	//for type 0
		ppos = plc.getPlayerPos ();	//for type 1

		//player setting
		this.setPlayerStatus ();

		//burner cnt
		bncnt = 0;

		//play se base flight noise
		mc.playSound(mc.se_basenoise);
		//play se base flight in
		mc.playSound(mc.se_baseflightin);

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

			//move
			switch (movseq) {
			case 10:
				break;
			case 0:
				//type 0
				//forward
				cashTransform.localScale = new Vector3 (xsbase * scl, ysbase * scl, 0.0f);
				cashTransform.Translate (xx, yy, 0);
				this.setPlayerStatus ();
				//move forward end ?
				if (yy <= 0) {
					yy = -0.0002f;
					//next seq
					movseq++;
				}
				//brake
				yy = yy - 0.008f;
				//scale down
				scl = scl - ss;
				ss = ss - sss;
				if (scl <= 1.0f) {
					scl = 1.0f;
				}
				break;
			case 1:
				//back
				cashTransform.localScale = new Vector3 (xsbase * scl, ysbase * scl, 0.0f);
				cashTransform.Translate (xx, yy, 0);
				this.setPlayerStatus ();
				//move back end?
				if ( yy >= 0 ) {
					yy = 0.0f;
					//play se base release player
					mc.playSound(mc.se_basereleaseplayer);
					//next seq
					movseq++;
				}
				//move back
				if (cashTransform.position.y > 1.9f) {
					//back
					yy = yy - 0.00245f;
				} else {
					//brake
					yy = yy + 0.0045f;
				}
				//scale down
				scl = scl - ss;
				ss = ss - sss;
				if (scl <= 1.0f) {
					scl = 1.0f;
				}
				break;
			case 2:
				//release player
				this.setPlayerStatus ( pyy );
				//breake
				pyy = pyy + 0.00015f;
				//term player release?
				if (pyy >= 0) {
					pyy = 0.0f;
					//player release term and base move forward start
					yy = 0.003f;
					yyy = 0.98125f;
					//play se base acceleration
					mc.playSound(mc.se_bs130);
					//next seq
					movseq++;	
				}
				break;
			case 3:
				//base move forward
				cashTransform.Translate (xx, yy, 0);
				yyy = yyy - 0.00025f;
				yy = yy / yyy;
				//term base move forward?
				if (cashTransform.position.y > ymax) {
					//next seq
					movseq++;	
				}
				//generate burner
				if (bncnt % 2 == 0) {
					const float bnxoffset_l = -2.4f;
					const float bnyoffset_l = -1.15f;
					const float bnxoffset_r = -(bnxoffset_l);
					const float bnyoffset_r = bnyoffset_l;
					//left
					mc.generateBurner100Effect ((cashTransform.position.x + bnxoffset_l), (cashTransform.position.y + bnyoffset_l), 0, -0.6f, 2.7f, 2.7f);
					//right
					mc.generateBurner100Effect ((cashTransform.position.x + bnxoffset_r), (cashTransform.position.y + bnyoffset_r), 0, -0.6f, 2.7f, 2.7f);
				}
				bncnt++;
				//generate screen shake effect
				if ((Random.Range (0, 20)) == 0) {
					mc.generateScreenShakeEffect (5);
				}
				break;
			case 4:
				//term
				//base flight noise fade out
				mc.fadeoutLoopSe();
				//objnum dec
				mc.decObj ();
				//delete this
				Destroy (gameObject);
				break;
			case 20:
				//type 1
				//forward and player pos adjust
				const float tpposx = 0.0f;
				const float tpposy = -2.0f;
				//base move
				cashTransform.localScale = new Vector3 (xsbase * 1.5f, ysbase * 1.5f, 0.0f);
				if (cashTransform.position.y < 10.0f) {
					cashTransform.Translate (xx, yy, 0);
				}
				//player move
				if (ppos.x > tpposx) {
					ppos.x = ppos.x - 0.05f;
					if (ppos.x < tpposx) {
						ppos.x = tpposx;
					}
				}
				if (ppos.x < tpposx) {
					ppos.x = ppos.x + 0.05f;
					if (ppos.x > tpposx) {
						ppos.x = tpposx;
					}
				}
				if (ppos.y > tpposy) {
					ppos.y = ppos.y - 0.05f;
					if (ppos.y < tpposy) {
						ppos.y = tpposy;
					}
				}
				if (ppos.y < tpposy) {
					ppos.y = ppos.y + 0.05f;
					if (ppos.y > tpposy) {
						ppos.y = tpposy;
					}
				}
				this.setPlayerStatus ();
				//ppos ok?
				if ((ppos.x == tpposx) && (ppos.y == tpposy) && (cashTransform.position.y >= 10.0f) ) {
					mc.playSound (mc.vo260);
					cashTransform.localScale = new Vector3 (xsbase * 1.0f, ysbase * 1.0f, 0.0f);
					yy = -0.083f;
					yyy = -0.000321f;
					movseq = 21;
				}
				break;
			case 21:
				//back and breake
				if (yy < -0.000321f) {
					cashTransform.Translate (xx, yy, 0.0f);
					yy = yy - yyy;
					//generate burner
					if ( (bncnt % 2 == 0) && (bncnt % 50 >= 0) && (bncnt % 50 <= 8) ) {
						const float bnxoffset_l = -2.4f;
						const float bnyoffset_l = -1.15f;
						const float bnxoffset_r = -(bnxoffset_l);
						const float bnyoffset_r = bnyoffset_l;
						//left
						mc.generateBurner100Effect ((cashTransform.position.x + bnxoffset_l), (cashTransform.position.y + bnyoffset_l), 0, -0.4f, 1.4f, 1.4f);
						//right
						mc.generateBurner100Effect ((cashTransform.position.x + bnxoffset_r), (cashTransform.position.y + bnyoffset_r), 0, -0.4f, 1.4f, 1.4f);
					}
					bncnt++;
				} else {
					mc.playSound (mc.vo270);
					yy = -0.005f;
					movseq = 22;
				}
				break;
			case 22:
				//back and get player
				cashTransform.Translate (xx, yy, 0.0f);
				if (ppos.y >= (cashTransform.position.y + py_offset)) {
					//play se base release player
					mc.playSound(mc.se_basereleaseplayer);
					//generate shake effect
					mc.generateScreenShakeEffect( 5 );
					movseqcnt = 80;
					movseq = 23;
				}
				break;
			case 23:
				//wait
				movseqcnt--;
				if (movseqcnt <= 0) {
					type = 0;
					bncnt = 0;
					//move forward start
					yy = 0.003f;
					yyy = 0.98125f;
					//play se base acceleration
					mc.playSound (mc.se_bs130);
					//next seq
					movseq = 24;
				}
				break;
			case 24:
				//base and player move forward
				cashTransform.Translate (xx, yy, 0);
				this.setPlayerStatus ();
				yyy = yyy - 0.00025f;
				yy = yy / yyy;
				//term base move forward?
				if (cashTransform.position.y > ymax) {
					//next seq
					movseq = 25;	
				}
				//generate burner
				if (bncnt % 2 == 0) {
					const float bnxoffset_l = -2.4f;
					const float bnyoffset_l = -1.15f;
					const float bnxoffset_r = -(bnxoffset_l);
					const float bnyoffset_r = bnyoffset_l;
					//left
					mc.generateBurner100Effect ((cashTransform.position.x + bnxoffset_l), (cashTransform.position.y + bnyoffset_l), 0, -0.6f, 2.7f, 2.7f);
					//right
					mc.generateBurner100Effect ((cashTransform.position.x + bnxoffset_r), (cashTransform.position.y + bnyoffset_r), 0, -0.6f, 2.7f, 2.7f);
				}
				bncnt++;
				//generate screen shake effect
				if ((Random.Range (0, 20)) == 0) {
					mc.generateScreenShakeEffect (5);
				}
				break;
			case 25:
				//term
				mc.releaseWait();
				//player mode
				plc.setPlayerMode( plc.playerModeNoExist );
				//base flight noise fade out
				mc.fadeoutLoopSe();
				//objnum dec
				mc.decObj ();
				//delete this
				Destroy (gameObject);
				break;
			default:
				break;
			}
		}
	}

	//set player status
	private void setPlayerStatus( float pyy = 0 ){
		if (type == 0) {
			plc.setPlayerStatus (plc.playerModeOnBase, cashTransform.position.x, ((cashTransform.position.y) + (py_offset * scl)), scl, pyy); 
		} else if (type == 1) {
			plc.setPlayerStatus (plc.playerModeOnBase, ppos.x, ppos.y, scl, pyy); 
		}
	}

	//public

	//set init status
	public void setInitStatus( int type ){
		this.type = type;
	}

}
