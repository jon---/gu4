using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerOption100Controller : MonoBehaviour {
	//public
	//public player bullet 110 Prefab
	public GameObject playerBullet110ControllerPrefab;

	//private
	//local const
	const float mvDirSpeed = -15.0f;	//op common
	const float mvSizeBase_x = 0.448f;	//op common
	const float mvSizeBase_y = 0.144f;	//op common
	const float mvDirInit_op0 = 90.0f;
	const float mvDirInit_op1 = 270.0f;
	const float stDirInit_op0 = 0.0f;
	const float stDirInit_op1 = 0.0f;
	const float stDirSpeedInit_op0 = -1.5f;
	const float stDirSpeedInit_op1 = 1.5f;
	const float stDirMax = 8.0f;		//op0,1 common

	//option position offset
	//op group 0
	const float xoffsetbase_opg0 = 0.0f;	//op0,1 common
	const float yoffsetbase_opg0 = 0.96f;	//op0,1 common
	//op group 1
	const float xoffsetbase_opg1 = -0.64f;	//op2,4 common
	const float yoffsetbase_opg1 = 0.0f;	//op2,4 common
	//op group 2
	const float xoffsetbase_opg2 = 0.64f;	//op3,5 common
	const float yoffsetbase_opg2 = 0.0f;	//op3,5 common
	//op group 3
	const float xoffsetbase_opg3 = -1.04f;	//op6,8 common
	const float yoffsetbase_opg3 = -0.96f;	//op6,8 common
	//op group 4
	const float xoffsetbase_opg4 = 1.04f;	//op7,9 common
	const float yoffsetbase_opg4 = -0.96f;	//op7,9 common
	float[] xoffsetbase = new float[]{ 
		xoffsetbase_opg0, xoffsetbase_opg0, xoffsetbase_opg1, xoffsetbase_opg2, xoffsetbase_opg1, xoffsetbase_opg2,
		xoffsetbase_opg3, xoffsetbase_opg4, xoffsetbase_opg3, xoffsetbase_opg4 };
	float[] yoffsetbase = new float[]{ 
		yoffsetbase_opg0, yoffsetbase_opg0, yoffsetbase_opg1, yoffsetbase_opg2, yoffsetbase_opg1, yoffsetbase_opg2,
		yoffsetbase_opg3, yoffsetbase_opg4, yoffsetbase_opg3, yoffsetbase_opg4 };

	//bullet
	//bullet interval
	int[,] bltinterval = new int[,]{
		{6, 6, 6, 5, 5, 4},	//typeA
		{8, 8, 8, 8, 7, 6},	//typeB
		{7, 7, 7, 7, 7, 7},	//typeC
	};
	//shot speed
	float[,] bltspeed = new float[,] {
		{0.6f, 0.6f, 0.6f, 0.7f, 0.7f, 0.8f},	//typeA
		{0.4f, 0.4f, 0.4f, 0.4f, 0.5f, 0.55f},	//typeB
		{0.50f, 0.52f, 0.54f, 0.56f, 0.58f, 0.60f},	//typeC
	};
	//bullet scale
	//x
	//typeA
	const float bScale_x_lv0A = 3.2f;
	const float bScale_x_lv1A = 3.34f;
	const float bScale_x_lv2A = 3.48f;
	const float bScale_x_lv3A = 3.62f;
	const float bScale_x_lv4A = 3.76f;
	const float bScale_x_lv5A = 3.90f;
	//typeB
	const float bScale_x_lv0B = 2.56f;
	const float bScale_x_lv1B = 2.58f;
	const float bScale_x_lv2B = 2.60f;
	const float bScale_x_lv3B = 2.62f;
	const float bScale_x_lv4B = 2.64f;
	const float bScale_x_lv5B = 2.66f;
	//typeC
	const float bScale_x_lv0C = 3.1f;
	const float bScale_x_lv1C = 3.15f;
	const float bScale_x_lv2C = 3.20f;
	const float bScale_x_lv3C = 3.25f;
	const float bScale_x_lv4C = 3.30f;
	const float bScale_x_lv5C = 3.35f;
	float[,] bScale_x = new float[,]{
		{bScale_x_lv0A, bScale_x_lv1A, bScale_x_lv2A, bScale_x_lv3A, bScale_x_lv4A, bScale_x_lv5A},	//typeA
		{bScale_x_lv0B, bScale_x_lv1B, bScale_x_lv2B, bScale_x_lv3B, bScale_x_lv4B, bScale_x_lv5B},	//typeB
		{bScale_x_lv0C, bScale_x_lv1C, bScale_x_lv2C, bScale_x_lv3C, bScale_x_lv4C, bScale_x_lv5C},	//typeC
	};
	//y
	//typeA
	const float bScale_y_lv0A = 3.2f;
	const float bScale_y_lv1A = 3.34f;
	const float bScale_y_lv2A = 3.48f;
	const float bScale_y_lv3A = 3.62f;
	const float bScale_y_lv4A = 3.76f;
	const float bScale_y_lv5A = 3.90f;
	//typeB
	const float bScale_y_lv0B = 2.56f;
	const float bScale_y_lv1B = 2.58f;
	const float bScale_y_lv2B = 2.60f;
	const float bScale_y_lv3B = 2.62f;
	const float bScale_y_lv4B = 2.64f;
	const float bScale_y_lv5B = 2.66f;
	//typeC
	const float bScale_y_lv0C = 3.1f;
	const float bScale_y_lv1C = 3.15f;
	const float bScale_y_lv2C = 3.20f;
	const float bScale_y_lv3C = 3.25f;
	const float bScale_y_lv4C = 3.30f;
	const float bScale_y_lv5C = 3.35f;
	float[,] bScale_y = new float[,]{
		{bScale_y_lv0A, bScale_y_lv1A, bScale_y_lv2A, bScale_y_lv3A, bScale_y_lv4A, bScale_y_lv5A},	//typeA
		{bScale_y_lv0B, bScale_y_lv1B, bScale_y_lv2B, bScale_y_lv3B, bScale_y_lv4B, bScale_y_lv5B},	//typeB
		{bScale_y_lv0C, bScale_y_lv1C, bScale_y_lv2C, bScale_y_lv3C, bScale_y_lv4C, bScale_y_lv5C},	//typeC
	};

	//cash
	//component cash
	Transform cashTransform;
	GameObject mainCtr;
	mainController mc;
	GameObject playerCtr;
	playerController plc;
	SpriteRenderer sr = null;	//(for instantiate)
	Animator animt;

	//system local
	int intervalCnt;	//interval count

	//option idx
	int optionIdx;

	//local
	//player position
	float px;
	float py;

	//move
	float mvDirection;	//move rotate direction
	float mvsizex;	//move rotate size x
	float mvsizey;	//move rotate size y
	float mvxoffset;	//move rotate position
	float mvyoffset;	//move rotate position

	//abs position
	float absposx;	//move abs player offset x
	float absposy;	//move abs player offset y

	//shot
	float stDirection;	//shot rotate direction
	float ssd;	//shot rotate speed

	//bullet cnt
	int bltcnt;

	//player mode
	//player mode
	const int plModeNormal = 0x00;	//game play
	const int plModeInvalid = 0x01;	//after damage
	const int plModeRebirth = 0x02;	//after death
	const int plModeOnBase = 0x03;	//at start on playerbase
	const int plModeNoExist = 0xff;	//no exist player
	int pMode;

	//player type
	int playerType;

	//bullet power
	int bPower;

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

		//sprite
		sr = GetComponent<SpriteRenderer>();

		//animator
		animt = GetComponent<Animator>();
		animt.speed = 2.5f;

		//option idx
		//(set from parent objects)
//		optionIdx = 0;

		//player position
		//(set from parent objects)
//		px = 0;
//		py = 0;

		//move directiion
		//set from parent objects
//op0
//		mvDirection = mvDirInit_op0;
//op1
//		mvDirection = mvDirInit_op1;
		mvsizex = 0.0f;
		mvsizey = 0.0f;
		absposx = 0.0f;
		absposy = 0.0f;

		//shot direction
		//set from parent objects
//op0
//		stDirection = 0.0f;
//		ssd = stDirSpeedInit_op0;
//op1
//		stDirection = 0.0f;
//		ssd = stDirSpeedInit_op1;

		//bullet cnt
		bltcnt = 0;

		//player mode
		//set from parent objects
//		pMode = plc.playerModeNoExist;

		//player type
		//set from parent objects
//		playerType = 0;

		//bullet power
		//set from parent objects
//		bPower = 0;

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

		//move rotate
		mvDirection = mvDirection + mvDirSpeed;
		if (mvDirection < 0.0f) {
			mvDirection = mvDirection + 360.0f;
		}
		if (mvDirection > 360.0f) {
			mvDirection = mvDirection - 360.0f;
		}
		//mov rotate size
		float mm;
		mm = (mvSizeBase_x - mvsizex)/60.0f;
		if (mm != 0.0f) {
			mvsizex = mvsizex + mm;
		}
		mm = (mvSizeBase_y - mvsizey)/60.0f;
		if (mm != 0.0f) {
			mvsizey = mvsizey + mm;
		}
		//fix mov offset
		mvxoffset = Mathf.Cos( mvDirection*Mathf.Deg2Rad ) * mvsizex;
		mvyoffset = Mathf.Sin( mvDirection*Mathf.Deg2Rad ) * mvsizey;

		//fix mov position
		float posx = px + mvxoffset;
		float posy = py + mvyoffset;

		//fix abs position
		float aa;
		aa = (xoffsetbase [this.optionIdx] - absposx)/50.0f;
		if (aa != 0.0f) {
			absposx = absposx + aa;
		}
		aa = (yoffsetbase [this.optionIdx] - absposy)/50.0f;
		if (aa != 0.0f) {
			absposy = absposy + aa;
		}

		//fix position set
		cashTransform.position = new Vector3 ( (posx+absposx), (posy+absposy), 0);

		//shot rotate
		stDirection = stDirection + ssd;
		if ((stDirection <= (stDirMax*-1)) || (stDirection >= stDirMax)) {
			ssd = ssd - (ssd * 2);
		}
		cashTransform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, stDirection));

		//shot
		if ((pMode == plModeNormal) || (pMode == plModeInvalid)) {	//normal or invalid only shot
			const float shot_offset = 90.0f;	//上向き発射のため
			const float shotposition_offsetbase = 0.4f;
			GameObject go;
			bltcnt++;
//			if (bltcnt >= 6) {	//5
			if (bltcnt >= bltinterval[playerType, bPower]) {
				bltcnt = 0;
				//shot se
				mc.playSound (mc.se_optionshot);
				//bullet
				float xx = Mathf.Cos ((stDirection + shot_offset) * Mathf.Deg2Rad) * 1.0f;
				float yy = Mathf.Sin ((stDirection + shot_offset) * Mathf.Deg2Rad) * 1.0f;
//				const float xs = 0.0f;
//				const float ys = 0.8f;
				go = Instantiate (playerBullet110ControllerPrefab) as GameObject;
				go.GetComponent<playerBullet110Controller> ().setInitStatus (
					(cashTransform.position.x + (xx * shotposition_offsetbase * bScale_x [playerType, bPower] / 2)),
					(cashTransform.position.y + (yy * shotposition_offsetbase * bScale_y [playerType, bPower] / 2)),
					bScale_x [playerType, bPower], bScale_y [playerType, bPower],
					stDirection, bltspeed[playerType, bPower] );
			}
		}


		////interval process

		//interval count
		intervalCnt++;
		if (intervalCnt >= 2) {
			intervalCnt = 0;
			//nop
		}
	}
		

	//public
	//set initial status
	public void initStatus( int op ){
		//save index
		this.optionIdx = op;
		//init
		switch (op) {
		case 0://gr0
		case 2://gr1
		case 3://gr2
		case 6://gr3
		case 7://gr4
			//option0
			mvDirection = mvDirInit_op0;
			stDirection = stDirInit_op0;
			ssd = stDirSpeedInit_op0;
			bltcnt = 0;
			break;
		case 1://gr0
		case 4://gr1
		case 5://gr2
		case 8://gr3
		case 9://gr4
			//option1
			mvDirection = mvDirInit_op1;
			stDirection = stDirInit_op1;
			ssd = stDirSpeedInit_op1;
			bltcnt = 0;
			break;
		default:
			break;
		}
	}

	//set status
	public void setStatus( float x, float y, int pmd, int pt, int pw ){	//x,y,player mode,player type,shot power
		this.px = x;
		this.py = y;
		this.setMode(pmd);
		this.playerType = pt;
		this.bPower = pw;
	}

	//set player mode
	public void setMode( int pmd ){
		SpriteRenderer spr;
		this.pMode = pmd;
		switch (pMode) {
		case plModeNormal:
			if (sr == null) {
				spr = GetComponent<SpriteRenderer> ();
			} else {
				spr = sr;
			}
			spr.color = new Color (1.0f, 1.0f, 1.0f, 1.0f);
			break;
		case plModeInvalid:
			if (sr == null) {
				spr = GetComponent<SpriteRenderer> ();
			} else {
				spr = sr;
			}
			spr.color = new Color (1.0f, 1.0f, 1.0f, 1.0f);
			break;
		case plModeRebirth:
			if (sr == null) {
				spr = GetComponent<SpriteRenderer> ();
			} else {
				spr = sr;
			}
			spr.color = new Color (1.0f, 1.0f, 1.0f, 0.0f);
			mvsizex = 0.0f;
			mvsizey = 0.0f;
			absposx = 0.0f;
			absposy = 0.0f;
			break;
		case plModeOnBase:
			if (sr == null) {
				spr = GetComponent<SpriteRenderer> ();
			} else {
				spr = sr;
			}
			spr.color = new Color (1.0f, 1.0f, 1.0f, 0.0f);
			mvsizex = 0.0f;
			mvsizey = 0.0f;
			absposx = 0.0f;
			absposy = 0.0f;
			break;
		case plModeNoExist:
			if (sr == null) {
				spr = GetComponent<SpriteRenderer> ();
			} else {
				spr = sr;
			}
			spr.color = new Color (1.0f, 1.0f, 1.0f, 0.0f);
			mvsizex = 0.0f;
			mvsizey = 0.0f;
			absposx = 0.0f;
			absposy = 0.0f;
			break;
		default:
			break;
		}
	}

}
