using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {
	//public
	//public player bullet 100 Prefab
	public GameObject playerBullet100ControllerPrefab;
	//public player bullet 110 Prefab
	public GameObject playerBullet110ControllerPrefab;
	//public player laser 100 Prefab
	public GameObject playerLaser100ControllerPrefab;
	//public player missile 100 Prefab
	public GameObject playerMissile100ControllerPrefab;
	//public bomb Prefab
	public GameObject bombControllerPrefab;
	//public bomb laser Prefab
	public GameObject bombLaserControllerPrefab;
	//public player option100 Prefab
	public GameObject playerOption100ControllerPrefab;
	//player sprite
	public Sprite player120_0;	//player type-A center
	public Sprite player120_1;	//player type-A right 1
	public Sprite player120_2;	//player type-A right 2
	public Sprite player120_3;	//player type-A left 1
	public Sprite player120_4;	//player type-A left 2
	public Sprite player110_0;	//player type-B center
	public Sprite player110_1;	//player type-B right
	public Sprite player110_2;	//player type-B left
	public Sprite player100;	//player type-C center
	public Sprite player101;	//player type-C left
	public Sprite player102;	//player type-C right
	//player mode
	public int playerModeNormal = plModeNormal;	//game play
	public int playreModeInvalid = plModeInvalid;	//after damage
	public int playerModeRebirth = plModeRebirth;	//after death
	public int playerModeOnBase = plModeOnBase;	//at start on playerbase
	public int playerModeNoExist = plModeNoExist;	//no exist player
	//player level
	//player shot power
	public int pPower;
	//player laser power
	public int pLaser;
	//player missile power
	public int pMissile;
	//option num
	public int oNum;

	//private
	//local const
	//x,y min/max
	const float xmin = -2.49f;
	const float xmax = 2.49f;
	const float ymin = -4.68f;
	const float ymax = 4.68f;
	const float xsbase_typeA = 1.88f;	//scale typeA
	const float ysbase_typeA = 2.0f;	//scale typeA
	const float cradius_typeA = 0.0256f;	//collider radius typeA
	const float xsbase_typeB = 2.0f;	//scale typeB
	const float ysbase_typeB = 1.73f;	//scale typeB
	const float cradius_typeB = 0.0256f;	//collider radius typeB
	const float xsbase_typeC = 1.6f;	//scale typeC
	const float ysbase_typeC = 1.6f;	//scale typeC
	const float cradius_typeC = 0.032f;	//collider radius typeC
	//system local
	const float pmovk_slow = 1.0f;	//slow mode speed
	const float pmovk_normal = 1.2f;	//normal mode speed
	const float pmovk_fast = 1.8f;	//fast mode speed
	//item type
	const int itype_power = 0x00;
	const int itype_laser = 0x01;
	const int itype_missile = 0x02;
	const int itype_option = 0x03;
	const int itype_bomb = 0x04;
	const int itype_shield = 0x05;
	const int itype_score = 0x06;
	const int itype_1up = 0x07;
	//player valinit
	const int pPlayerInit = 3;
	const int pHpInitAB = pHpMaxAB;
	const int pHpInitC = pHpMaxC;
	const int pPowerInit = 0;
	const int pLaserInit = 0;
	const int pMissileInit = 0;
	const int pBombInit = 3;
	const int pOptionInit = 2;
	//player valmax
	const int pPlayerMax = 7;
	const int pHpMaxAB = 3;
	const int pHpMaxC = 4;
	const int pPowerMax = 5;
	const int pLaserMax = 5;
	const int pMissileMax = 5;
	const int pBombMaxAB = 5;
	const int pBombMaxC = 7;
	const int pOptionMax = 10;
	//shot level adjust
	//shot interval
	int[,] bltinterval = new int[,]{
		{4, 4, 4, 3, 3, 3},	//typeA
		{4, 4, 4, 4, 4, 4},	//typeB
		{4, 4, 4, 4, 4, 3},	//typeC
	};
	//shot speed
	float[,] bltspeed = new float[,] {
		{0.7f, 0.8f, 0.9f, 1.0f, 1.1f, 1.2f},	//typeA
		{0.7f, 0.7f, 0.7f, 0.75f, 0.8f, 0.85f},	//typeB
		{0.7f, 0.75f, 0.8f, 0.85f, 0.9f, 0.95f},	//typeC
	};
	//shot x scale
	//typeA
	const float s_scalex_lv0A = 1.2f;
	const float s_scalex_lv1A = 1.42f;
	const float s_scalex_lv2A= 1.64f;
	const float s_scalex_lv3A= 1.86f;
	const float s_scalex_lv4A= 2.08f;
	const float s_scalex_lv5A= 2.30f;
	//typeB
	const float s_scalex_lv0B= 0.76f;
	const float s_scalex_lv1B= 0.98f;
	const float s_scalex_lv2B= 1.2f;
	const float s_scalex_lv3B= 1.42f;
	const float s_scalex_lv4B= 1.64f;
	const float s_scalex_lv5B= 1.86f;
	//typeC
	const float s_scalex_lv0C= 1.0f;
	const float s_scalex_lv1C= 1.2f;
	const float s_scalex_lv2C= 1.4f;
	const float s_scalex_lv3C= 1.6f;
	const float s_scalex_lv4C= 1.8f;
	const float s_scalex_lv5C= 2.0f;
	float[,] s_scale_x = new float[,]{
		{ s_scalex_lv0A, s_scalex_lv1A, s_scalex_lv2A, s_scalex_lv3A, s_scalex_lv4A, s_scalex_lv5A },	//typeA
		{ s_scalex_lv0B, s_scalex_lv1B, s_scalex_lv2B, s_scalex_lv3B, s_scalex_lv4B, s_scalex_lv5B },	//typeB
		{ s_scalex_lv0C, s_scalex_lv1C, s_scalex_lv2C, s_scalex_lv3C, s_scalex_lv4C, s_scalex_lv5C },	//typeC
	};
	//shot y scale
	//typeA
	const float s_scaley_lv0A = 2.4f;
	const float s_scaley_lv1A = 2.84f;
	const float s_scaley_lv2A = 3.28f;
	const float s_scaley_lv3A = 3.72f;
	const float s_scaley_lv4A = 4.16f;
	const float s_scaley_lv5A = 4.6f;
	//typeB
	const float s_scaley_lv0B = 1.52f;
	const float s_scaley_lv1B = 1.96f;
	const float s_scaley_lv2B = 2.4f;
	const float s_scaley_lv3B = 2.84f;
	const float s_scaley_lv4B = 3.28f;
	const float s_scaley_lv5B = 3.72f;
	//typeC
	const float s_scaley_lv0C = 2.0f;
	const float s_scaley_lv1C = 2.4f;
	const float s_scaley_lv2C = 2.8f;
	const float s_scaley_lv3C = 3.2f;
	const float s_scaley_lv4C = 3.6f;
	const float s_scaley_lv5C = 4.0f;
	float[,] s_scale_y = new float[,] {
		{ s_scaley_lv0A, s_scaley_lv1A, s_scaley_lv2A, s_scaley_lv3A, s_scaley_lv4A, s_scaley_lv5A },	//typeA
		{ s_scaley_lv0B, s_scaley_lv1B, s_scaley_lv2B, s_scaley_lv3B, s_scaley_lv4B, s_scaley_lv5B },	//typeB
		{ s_scaley_lv0C, s_scaley_lv1C, s_scaley_lv2C, s_scaley_lv3C, s_scaley_lv4C, s_scaley_lv5C },	//typeC
	};
	//bullet offset 
	float[,] bloffsetp_y = new float[,]{	//power別offset
		{0.0f, 0.11f, 0.22f, 0.33f, 0.44f, 0.55f},	//typeA
		{-0.33f, -0.22f, -0.11f, 0.11f, 0.22f, 0.33f},	//typeB
		{-0.1f, 0.0f, 0.1f, 0.2f, 0.3f, 0.4f},	//typeC
	};
	//laser level adjust
	//laser x scale
	//typeA
	const float l_scalex_lv0A = 1.0f;	//laser none
	const float l_scalex_lv1A = 0.24f;
	const float l_scalex_lv2A = 0.32f;
	const float l_scalex_lv3A = 0.40f;
	const float l_scalex_lv4A = 0.48f;
	const float l_scalex_lv5A = 0.56f;
	//typeB
	const float l_scalex_lv0B = 1.0f;	//laser none
	const float l_scalex_lv1B = 0.48f;
	const float l_scalex_lv2B = 0.56f;
	const float l_scalex_lv3B = 0.64f;
	const float l_scalex_lv4B = 0.72f;
	const float l_scalex_lv5B = 0.80f;
	//typeC
	const float l_scalex_lv0C = 1.0f;	//laser none
	const float l_scalex_lv1C = 0.32f;
	const float l_scalex_lv2C = 0.40f;
	const float l_scalex_lv3C = 0.48f;
	const float l_scalex_lv4C = 0.56f;
	const float l_scalex_lv5C = 0.64f;
	float[,] l_scale_x = new float[,]{ 
		{l_scalex_lv0A, l_scalex_lv1A, l_scalex_lv2A, l_scalex_lv3A, l_scalex_lv4A, l_scalex_lv5A },	//typeA
		{l_scalex_lv0B, l_scalex_lv1B, l_scalex_lv2B, l_scalex_lv3B, l_scalex_lv4B, l_scalex_lv5B },	//typeB
		{l_scalex_lv0C, l_scalex_lv1C, l_scalex_lv2C, l_scalex_lv3C, l_scalex_lv4C, l_scalex_lv5C },	//typeC
	};
	//laser y scale
	//typeA
	const float l_scaley_lv0A = 1.0f;	//laser none
	const float l_scaley_lv1A = 1.2f;
	const float l_scaley_lv2A = 1.6f;
	const float l_scaley_lv3A = 2.0f;
	const float l_scaley_lv4A = 2.4f;
	const float l_scaley_lv5A = 2.8f;
	//typeB
	const float l_scaley_lv0B = 1.0f;	//laser none
	const float l_scaley_lv1B = 2.4f;
	const float l_scaley_lv2B = 2.8f;
	const float l_scaley_lv3B = 3.2f;
	const float l_scaley_lv4B = 3.6f;
	const float l_scaley_lv5B = 4.0f;
	//typeC
	const float l_scaley_lv0C = 1.0f;	//laser none
	const float l_scaley_lv1C = 1.6f;
	const float l_scaley_lv2C = 2.0f;
	const float l_scaley_lv3C = 2.4f;
	const float l_scaley_lv4C = 2.8f;
	const float l_scaley_lv5C = 3.2f;
	float[,] l_scale_y = new float[,]{ 
		{l_scaley_lv0A, l_scaley_lv1A, l_scaley_lv2A, l_scaley_lv3A, l_scaley_lv4A, l_scaley_lv5A },	//typeA
		{l_scaley_lv0B, l_scaley_lv1B, l_scaley_lv2B, l_scaley_lv3B, l_scaley_lv4B, l_scaley_lv5B },	//typeB
		{l_scaley_lv0C, l_scaley_lv1C, l_scaley_lv2C, l_scaley_lv3C, l_scaley_lv4C, l_scaley_lv5C },	//typeC
	};
	//laser offset
	float[,] laser1_offsety= new float[,] {
		{0, 0.8f, 1.0f, 1.2f, 1.4f, 1.6f},	//typeA
		{0, 1.4f, 1.6f, 1.8f, 2.0f, 2.2f},	//typeB
		{0, 1.0f, 1.2f, 1.4f, 1.6f, 1.8f},	//typeC
	};
	//missile level adjust
	//missile cnt
	int[,] mslintvl= new int[,] {
		{0, 31, 26, 21, 16, 11},	//typeA
		{0, 10, 8, 7, 5, 3},	//typeB
		{0, 25, 20, 16, 11, 7},	//typeC
	};

	//sub message info
	struct smsgInfo{
		public float xoffset;
		public float yoffset;
		public float xx;
		public float yy;
		public smsgInfo( float xo, float yo, float xs, float ys ){
			xoffset = xo;
			yoffset = yo;
			xx = xs;
			yy = ys;
		}
	}
	smsgInfo[] smsgInfoTable = new smsgInfo[]{
		new smsgInfo( 0.6f, -1.0f, 1.0f, -10.0f ),
		new smsgInfo( 0.0f, -0.3f, 0.0f, -10.0f ),
		new smsgInfo( -0.6f, -1.0f, -1.0f, -10.0f ),
		new smsgInfo( 0.6f, -0.3f, 1.0f, -10.0f ),
		new smsgInfo( 0.0f, -1.0f, 0.0f, -10.0f ),
		new smsgInfo( -0.6f, -0.3f, -1.0f, -10.0f ),
	};

	//cash
	//component cash
	Transform cashTransform;
	SpriteRenderer sr;
	GameObject mainCtr;
	mainController mc;
	//option
	GameObject[] optionCtr = new GameObject[pOptionMax];
	playerOption100Controller[] poc = new playerOption100Controller[pOptionMax];

	//system local
	int intervalCnt;	//interval count

	//local
	//tap
	Vector2	tapLastPos;	//last tap pos
	Vector2	tapNewPos;	//new tap pos
	Vector2	tapAbsPos;	//tap move pos

	//player mode
	const int plModeNormal = 0x00;	//game play
	const int plModeInvalid = 0x01;	//after damage
	const int plModeRebirth = 0x02;	//after death
	const int plModeOnBase = 0x03;	//at start on playerbase
	const int plModeNoExist = 0xff;	//no exist player
	int plMode;

	//x mov info for getter
	int pXmov;

	//player pos for getter
	Vector2 ppos;

	//player pos for getter( for laser only)
	Vector2 pposls;

	//player pos mov for getter
	Vector2 pposmov;

	//player speed
	float pmovk;

	//play scale
	float xsbase;
	float ysbase;

	//player type
	const int ptTypeA = 0x00;	//type-A
	const int ptTypeB = 0x01;	//type-B
	const int ptTypeC = 0x02;	//type-C
	int playerType;

	//player number
	int pNum;

	//player hitpoint
	int pHp;

	//bomb number
	int bNum;

	//player display x cnt
	int pDispx;

	//bomb interval
	int bInt;

	//explosion control count
	int exCnt;

	//submessage cnt
	int smsgCnt;
	int smsgCntMax;

	//player bullet
	//bullet cnt
	int bltcnt;
	//bullet direction offset
	float bldoffset;
	//bullet speed base
	float blbase_x;
	float blbase_y;
	//bullet1 x,y speed(direction)
	float bldl1;
	float bldr1;
	float bll1_x;
	float bll1_y;
	float blr1_x;
	float blr1_y;
	float bulletl1_offsetx;
	float bulletl1_offsety;
	float bulletr1_offsetx;
	float bulletr1_offsety;
	//bullet2 x,y speed(direction)
	float bldl2;
	float bldr2;
	float bll2_x;
	float bll2_y;
	float blr2_x;
	float blr2_y;
	float bulletl2_offsetx;
	float bulletl2_offsety;
	float bulletr2_offsetx;
	float bulletr2_offsety;
	//bullet3 x,y speed(direction)
	float bldl3;
	float bldr3;
	float bll3_x;
	float bll3_y;
	float blr3_x;
	float blr3_y;
	float bulletl3_offsetx;
	float bulletl3_offsety;
	float bulletr3_offsetx;
	float bulletr3_offsety;
	//bullet4 x,y speed(direction)
	float bldl4;
	float bldr4;
	float bll4_x;
	float bll4_y;
	float blr4_x;
	float blr4_y;
	float bulletl4_offsetx;
	float bulletl4_offsety;
	float bulletr4_offsetx;
	float bulletr4_offsety;

	//player laser
	//laser cnt
	int lsrcnt;
	//laser1 x,y spped
	float ls1_x;
	float ls1_y;

	//player missle
	//missile cnt
	int mslcnt;

	//invalid / rebirth
	const int blinkCntVal = 13;
	const int blinkTimerVal = 2;
	const float blinkAlpha = 170.0f / 255.0f;
	int blinkCnt;
	int blinkTimer;

	//rebirth voice
	bool rebirthVoice;

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

		//local init
//debug -->
//新キャラ/パターン/イベント実装時のテスト用
		this.setMode( plModeNoExist );
//		this.setMode( plModeNormal );	//debug
//debug <--

		//player speed
//debug -->
//新キャラ/パターン/イベント実装時のテスト用
//		pmovk = pmovk_slow;	//debug
		pmovk = pmovk_normal;
//		pmovk = pmovk_fast;	//debug
//debug <--

		//player type
//debug -->
		//新キャラ/パターン/イベント実装時のテスト用
		playerType = ptTypeA;
//		playerType = ptTypeB;	//debug
//		playerType = ptTypeC;	//debug
//debug <--
		this.setPlayerType( playerType );	//(set xsbase,ysbase)

		//player number
		pNum = pPlayerInit;
		//display player
		mc.dispPlayer( pNum );

		//hit point
		if (playerType == ptTypeC) {
			pHp = pHpInitC;
		} else {
			pHp = pHpInitAB;
		}
		//display shield
		mc.dispShield (pHp);

		//bomb number
		bNum = pBombInit;
		//display bomb
		mc.dispBomb( bNum );
		//bomb interval
		bInt = 0;

		//player display x cnt
		pDispx = 0;

		//player shot power
		pPower = pPowerInit;

		//player laser power
		pLaser = pLaserInit;

		//player missile power
		pMissile = pMissileInit;

		//option num
		oNum = 0;
		//option generate
		this.addOption ();

		//x mov info
		pXmov = 0;

		//player pos
		ppos.x = 0;
		ppos.y = 0;

		//player pos for laser
		pposls.x = 0;
		pposls.y = 0;

		//player pos mov
		pposmov.x = 0;
		pposmov.y = 0;

		//explosion count
		exCnt = 0;

		//submessage cnt
		smsgCnt = 0;
		smsgCntMax = smsgInfoTable.Length;

		//bullet count
		bltcnt = 0;

		//bullet
		blbase_x = 0.0f;	//speed
		blbase_y = 1.0f;	//speed
		bldoffset = 90.0f;	//direction offset
		//billet1 x,y speed(direction)
		bldl1 = 0.26f;
		bldr1 = bldl1 * -1;
		bll1_x = Mathf.Cos( (bldl1+bldoffset) * Mathf.Deg2Rad )*1.0f;
		bll1_y = Mathf.Sin( (bldl1+bldoffset) * Mathf.Deg2Rad )*1.0f;
		blr1_x = Mathf.Cos( (bldr1+bldoffset) * Mathf.Deg2Rad )*1.0f;
		blr1_y = Mathf.Sin( (bldr1+bldoffset) * Mathf.Deg2Rad )*1.0f;
		bulletl1_offsetx = -0.06f + bll1_x;
		bulletl1_offsety = (bll1_y*0.55f);
		bulletr1_offsetx = +0.06f + blr1_x;
		bulletr1_offsety = (blr1_y*0.55f);

		//bullet2 x,y speed(direction)
		bldl2 = 0.84f;
		bldr2 = bldl2 * -1;
		bll2_x = Mathf.Cos( (bldl2+bldoffset) * Mathf.Deg2Rad )*1.0f;
		bll2_y = Mathf.Sin( (bldl2+bldoffset) * Mathf.Deg2Rad )*1.0f;
		blr2_x = Mathf.Cos( (bldr2+bldoffset) * Mathf.Deg2Rad )*1.0f;
		blr2_y = Mathf.Sin( (bldr2+bldoffset) * Mathf.Deg2Rad )*1.0f;
		bulletl2_offsetx = -0.18f + bll2_x;
		bulletl2_offsety = bll2_y*0.53f;
		bulletr2_offsetx = +0.18f + blr2_x;
		bulletr2_offsety = blr2_y*0.53f;

		//bullet3 x,y speed(direction)
		bldl3 = 1.42f;
		bldr3 = bldl3 * -1;
		bll3_x = Mathf.Cos( (bldl3+bldoffset) * Mathf.Deg2Rad )*1.0f;
		bll3_y = Mathf.Sin( (bldl3+bldoffset) * Mathf.Deg2Rad )*1.0f;
		blr3_x = Mathf.Cos( (bldr3+bldoffset) * Mathf.Deg2Rad )*1.0f;
		blr3_y = Mathf.Sin( (bldr3+bldoffset) * Mathf.Deg2Rad )*1.0f;
		bulletl3_offsetx = -0.30f + bll3_x;
		bulletl3_offsety = bll3_y*0.51f;
		bulletr3_offsetx = +0.30f + blr3_x;
		bulletr3_offsety = blr3_y*0.51f;

		//bullet4 x,y speed(direction)
		bldl4 = 2.00f;
		bldr4 = bldl4 * -1;
		bll4_x = Mathf.Cos( (bldl4+bldoffset) * Mathf.Deg2Rad )*1.0f;
		bll4_y = Mathf.Sin( (bldl4+bldoffset) * Mathf.Deg2Rad )*1.0f;
		blr4_x = Mathf.Cos( (bldr4+bldoffset) * Mathf.Deg2Rad )*1.0f;
		blr4_y = Mathf.Sin( (bldr4+bldoffset) * Mathf.Deg2Rad )*1.0f;
		bulletl4_offsetx = -0.42f + bll4_x;
		bulletl4_offsety = bll4_y*0.49f;
		bulletr4_offsetx = +0.42f + blr4_x;
		bulletr4_offsety = blr4_y*0.49f;

		//laser
		//laser cnt
		lsrcnt = 0;
		//laser1 x,y speed
		ls1_x = 0.0f;
		ls1_y = 1.0f;

		//missile
		mslcnt = 0;

		//invalid / rebirth
		blinkCnt = 0;
		blinkTimer = 0;

		//rebirth voice
		rebirthVoice = false;

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

//debug -->
		#if UNITY_EDITOR || UNITY_WEBGL
		//key process (for unity editor or webgl only)
		//move (for unity editor only)
		bool keypush = false;
		float xmovkey = 0.0f;
		float ymovkey = 0.0f;
		//normal or invalid only move
		if ((plMode == plModeNormal) || (plMode == plModeInvalid)) {
			const float pmovk_key = 0.16f; 
			if (Input.GetKeyDown (KeyCode.LeftArrow) || Input.GetKey (KeyCode.LeftArrow)) {	//left
				keypush = true;
				xmovkey = pmovk_key * -1.0f;
			} else if (Input.GetKeyDown (KeyCode.RightArrow) || Input.GetKey (KeyCode.RightArrow)) {	//right
				keypush = true;
				xmovkey = pmovk_key * 1.0f;
			}
			if (Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKey (KeyCode.UpArrow)) {	//up
				keypush = true;
				ymovkey = pmovk_key * 1.0f;
			} else if (Input.GetKeyDown (KeyCode.DownArrow) || Input.GetKey (KeyCode.DownArrow)) {	//down
				keypush = true;
				ymovkey = pmovk_key * -1.0f;
			}
			cashTransform.Translate ( xmovkey, ymovkey, 0);
		}
		#endif
//debug <--
//debug -->
		#if UNITY_EDITOR || UNITY_WEBGL
		if (Input.anyKeyDown == true) {
			//spacekey bomb (for unity editor or webgl only)
			if (Input.GetKeyDown (KeyCode.Space)) {
				this.tapBombButton ();
			}
		}
		#endif
//debug <--
//debug -->
		#if UNITY_EDITOR
		//player level change (for unity editor only)
		//normal or invalid only move
		if (Input.anyKeyDown == true) {
			if ((plMode == plModeNormal) || (plMode == plModeInvalid)) {
				//shot power
				//'q'/'a' key shot power up/down ( for unity editor only )
				if (Input.GetKeyDown (KeyCode.Q)) {
					pPower = pPower + 1;
					if (pPower >= pPowerMax) {
						pPower = pPowerMax;
					}
				}
				if (Input.GetKeyDown (KeyCode.A)) {
					pPower = pPower - 1;
					if (pPower <= pPowerInit) {
						pPower = pPowerInit;
					}
				}
				//sw power
				//'w'/'s' key sub weapon power up/down ( for unity editor only )
				if (Input.GetKeyDown (KeyCode.W)) {
					if (mc.subWeapon == mc.subWeaponLaser) {
						pLaser = pLaser + 1;
						if (pLaser >= pLaserMax) {
							pLaser = pLaserMax;
						}
					} else {
						pMissile = pMissile + 1;
						if (pMissile >= pMissileMax) {
							pMissile = pMissileMax;
						}
					}
				}
				if (Input.GetKeyDown (KeyCode.S)) {
					if (mc.subWeapon == mc.subWeaponLaser) {
						pLaser = pLaser - 1;
						if (pLaser <= pLaserInit) {
							pLaser = pLaserInit;
						}
					} else {
						pMissile = pMissile - 1;
						if (pMissile <= pMissileInit) {
							pMissile = pMissileInit;
						}
					}
				}
				//option
				//'e'/'d' key opytion add/reset ( for unity editor only )
				if (Input.GetKeyDown (KeyCode.E)) {
					addOption ();
				}
				if (Input.GetKeyDown (KeyCode.D)) {
					optionReset ();
				}
			}
		}
		#endif
//debug <--

		//touch process
		if (Input.touchCount >= 1) {
			Touch tc = Input.GetTouch (0);
			//down
			if (tc.phase == TouchPhase.Began) {
				Vector2 tapPos = Camera.main.ScreenToWorldPoint (tc.position);
				tapLastPos = tapPos;
			}
			//during
			if ((tc.phase == TouchPhase.Moved) || (tc.phase == TouchPhase.Stationary)) {
				Vector2 tapPos = Camera.main.ScreenToWorldPoint (tc.position);
				tapNewPos = tapPos;
				tapAbsPos = tapNewPos - tapLastPos;
				tapLastPos = tapPos;
			}
			//up
			if ((tc.phase == TouchPhase.Ended) || (tc.phase == TouchPhase.Canceled)) {
				if (Input.touchCount >= 2) {
					tapLastPos = Camera.main.ScreenToWorldPoint (Input.GetTouch (1).position);	//multi touch ignore
				}
				tapAbsPos = new Vector2 (0, 0);
			}
		}

		//player move fix (touch only)
		float xmov = tapAbsPos.x * pmovk;
		float ymov = tapAbsPos.y * pmovk;

		//normal or invalid only move
		if ((plMode == plModeNormal) || (plMode == plModeInvalid)) {
			cashTransform.Translate (xmov, ymov, 0);

			//move common (key / mouse)
			Vector3 ppos = cashTransform.position;
			if (ppos.x > xmax) {	//right
				ppos.x = xmax;
				cashTransform.position = ppos;
			}
			if (ppos.x < xmin) {	//left
				ppos.x = xmin;
				cashTransform.position = ppos;
			}
			if (ppos.y > ymax) {	//up
				ppos.y = ymax;
				cashTransform.position = ppos;
			}
			if (ppos.y < ymin) {	//down
				ppos.y = ymin;
				cashTransform.position = ppos;
			}

		}

		//xmov info and sprite change
		const int dxmin3 = -9;	//for type-A
		const int dxmin2 = -6;
		const int dxmin1 = -3;
		const int dxmax1 = 3;
		const int dxmax2 = 6;
		const int dxmax3 = 9;	//for type-A
		if ((plMode == plModeNormal) || (plMode == plModeInvalid)) {
			#if UNITY_EDITOR || UNITY_WEBGL
			if ((xmov < 0.0f) || (xmovkey < 0.0f)) {
			#else
			if (xmov < 0.0f) {
			#endif
				//move left
				//x for display 
				pDispx = pDispx - 1;
				if (playerType == ptTypeA) {
					if (pDispx <= dxmin3) {
						pDispx = dxmin3;
					}
				} else {
					if (pDispx <= dxmin2) {
						pDispx = dxmin2;
					}
				}
				//x for x scroll
				this.pXmov = -1;
				#if UNITY_EDITOR || UNITY_WEBGL
			} else if ((xmov > 0.0f) || (xmovkey > 0.0f)) {
				#else
			} else if (xmov > 0.0f) {
				#endif
				//move right
				//x for display
				pDispx = pDispx + 1;
				if (playerType == ptTypeA) {
					if (pDispx >= dxmax3) {
						pDispx = dxmax3;
					}
				} else {
					if (pDispx >= dxmax2) {
						pDispx = dxmax2;
					}
				}
				//x for x scroll
				this.pXmov = +1;
			} else {
				//no move
				//x for display
				if (pDispx > 0) {
					pDispx = pDispx - 1;
				} else if (pDispx < 0) {
					pDispx = pDispx + 1;
				}
				//x for x scroll
				this.pXmov = 0;
			}
			//sprite change
			if (playerType == ptTypeA) {
				if (pDispx < dxmin2) {
					if (sr.sprite != player120_4) {
						sr.sprite = player120_4;
					}
				} else if (pDispx < dxmin1) {
					if (sr.sprite != player120_3) {
						sr.sprite = player120_3;
					}
				} else if (pDispx <= dxmax1) {
					if (sr.sprite != player120_0) {
						sr.sprite = player120_0;
					}
				} else if (pDispx <= dxmax2) {
					if (sr.sprite != player120_1) {
						sr.sprite = player120_1;
					}
				} else if (pDispx <= dxmax3) {
					if (sr.sprite != player120_2) {
						sr.sprite = player120_2;
					}
				}
			} else if (playerType == ptTypeB) {
				if (pDispx < dxmin1) {
					if (sr.sprite != player110_2) {
						sr.sprite = player110_2;
					}
				} else if (pDispx <= dxmax1) {
					if (sr.sprite != player110_0) {
						sr.sprite = player110_0;
					}
				} else if (pDispx <= dxmax2) {
					if (sr.sprite != player110_1) {
						sr.sprite = player110_1;
					}
				}
			} else if (playerType == ptTypeC) {
				if (pDispx < dxmin1) {
					if (sr.sprite != player101) {
						sr.sprite = player101;
					}
				} else if (pDispx <= dxmax1) {
					if (sr.sprite != player100) {
						sr.sprite = player100;
					}
				} else if (pDispx <= dxmax2) {
					if (sr.sprite != player102) {
						sr.sprite = player102;
					}
				}
			}
		} else {
			//center
			pDispx = 0;
		}

		//player pos fix
		ppos.x = cashTransform.position.x;
		ppos.y = cashTransform.position.y;

		//for option
		for (int i = 0; i < oNum; i++) {
			poc[i].setStatus ( ppos.x, ppos.y, plMode, playerType, pPower );
		}

		//for laser
		float xx = xmov;
		float yy = ymov;
//debug -->
		#if UNITY_EDITOR || UNITY_WEBGL
		//(for unity editor or webgl only)
		if ( keypush == true ) {
			xx = xmovkey;
			yy = ymovkey;
		}
		#endif
//debug <--
		//set player move info
		//player move
		pposmov.x = xx;
		pposmov.y = yy;
		//player pos (for laser)
		pposls.x = cashTransform.position.x;
		pposls.y = cashTransform.position.y + laser1_offsety [playerType, pLaser];

		//generate player bullet
		//normal or invalid only shot bullet
		if ((plMode == plModeNormal) || (plMode == plModeInvalid)) {
			//player bullet
			GameObject go;
//			const int bltinterval = 4;//2;
			int cPower = pPower;
			if (playerType == ptTypeA) {
				cPower++;
			}
			switch (cPower) {
			case 0:
				//power 0	bulletA1 l + bulletA1 r + bulletA2 l + bulletA2 r
				bltcnt++;
				if (bltcnt >= bltinterval[playerType, pPower]) {
					bltcnt = 0;
					mc.playSound (mc.se_playershot);
					//bullet l
					go = Instantiate (playerBullet100ControllerPrefab) as GameObject;
					go.GetComponent<playerBullet100Controller> ().setInitStatus ( 
						(cashTransform.position.x + bulletl1_offsetx), (cashTransform.position.y + bulletl1_offsety+bloffsetp_y[playerType, cPower]),
						s_scale_x[playerType, cPower], s_scale_y[playerType, cPower], bldl1, bltspeed[playerType, pPower] );
					//bullet r
					go = Instantiate (playerBullet100ControllerPrefab) as GameObject;
					go.GetComponent<playerBullet100Controller> ().setInitStatus ( 
						(cashTransform.position.x + bulletr1_offsetx), (cashTransform.position.y + bulletr1_offsety+bloffsetp_y[playerType, cPower]),
						s_scale_x[playerType, cPower], s_scale_y[playerType, cPower], bldr1, bltspeed[playerType, pPower] );
					//bullet l2
					go = Instantiate (playerBullet100ControllerPrefab) as GameObject;
					go.GetComponent<playerBullet100Controller> ().setInitStatus ( 
						(cashTransform.position.x + bulletl2_offsetx), (cashTransform.position.y + bulletl2_offsety+bloffsetp_y[playerType, cPower]),
						s_scale_x[playerType, cPower], s_scale_y[playerType, cPower], bldl2, bltspeed[playerType, pPower] );
					//bullet r2
					go = Instantiate (playerBullet100ControllerPrefab) as GameObject;
					go.GetComponent<playerBullet100Controller> ().setInitStatus ( 
						(cashTransform.position.x + bulletr2_offsetx), (cashTransform.position.y + bulletr2_offsety+bloffsetp_y[playerType, cPower]),
						s_scale_x[playerType, cPower], s_scale_y[playerType, cPower], bldr2, bltspeed[playerType, pPower] );
				}
				break;
			case 1:
				//power 1	bulletB1 l + bulletB1 r + bulletA2 l + bulletA2 r + bulletA3 l + bulletA3 r
				bltcnt++;
				if (bltcnt >= bltinterval[playerType, pPower]) {
					bltcnt = 0;
					mc.playSound (mc.se_playershot);
					//bullet l
					go = Instantiate (playerBullet100ControllerPrefab) as GameObject;
					go.GetComponent<playerBullet100Controller> ().setInitStatus ( 
						(cashTransform.position.x + bulletl1_offsetx), (cashTransform.position.y + bulletl1_offsety+bloffsetp_y[playerType, cPower]),
						s_scale_x[playerType, cPower], s_scale_y[playerType, cPower], bldl1, bltspeed[playerType, pPower] );
					//bullet r
					go = Instantiate (playerBullet100ControllerPrefab) as GameObject;
					go.GetComponent<playerBullet100Controller> ().setInitStatus ( 
						(cashTransform.position.x + bulletr1_offsetx), (cashTransform.position.y + bulletr1_offsety+bloffsetp_y[playerType, cPower]),
						s_scale_x[playerType, cPower], s_scale_y[playerType, cPower], bldr1, bltspeed[playerType, pPower] );
					//bullet l2
					go = Instantiate (playerBullet100ControllerPrefab) as GameObject;
					go.GetComponent<playerBullet100Controller> ().setInitStatus ( 
						(cashTransform.position.x + bulletl2_offsetx), (cashTransform.position.y + bulletl2_offsety+bloffsetp_y[playerType, cPower]),
						s_scale_x[playerType, cPower], s_scale_y[playerType, cPower], bldl2, bltspeed[playerType, pPower] );
					//bullet r2
					go = Instantiate (playerBullet100ControllerPrefab) as GameObject;
					go.GetComponent<playerBullet100Controller> ().setInitStatus ( 
						(cashTransform.position.x + bulletr2_offsetx), (cashTransform.position.y + bulletr2_offsety+bloffsetp_y[playerType, cPower]),
						s_scale_x[playerType, cPower], s_scale_y[playerType, cPower], bldr2, bltspeed[playerType, pPower] );
					//bullet l3
					go = Instantiate (playerBullet100ControllerPrefab) as GameObject;
					go.GetComponent<playerBullet100Controller> ().setInitStatus ( 
						(cashTransform.position.x + bulletl3_offsetx), (cashTransform.position.y + bulletl3_offsety+bloffsetp_y[playerType, cPower-1]),
						s_scale_x[playerType, cPower-1], s_scale_y[playerType, cPower-1], bldl3, bltspeed[playerType, pPower] );
					//bullet r3
					go = Instantiate (playerBullet100ControllerPrefab) as GameObject;
					go.GetComponent<playerBullet100Controller> ().setInitStatus ( 
						(cashTransform.position.x + bulletr3_offsetx), (cashTransform.position.y + bulletr3_offsety+bloffsetp_y[playerType, cPower-1]),
						s_scale_x[playerType, cPower-1], s_scale_y[playerType, cPower-1], bldr3, bltspeed[playerType, pPower] );
				}
				break;
			case 2:
				//power 2	bulletC1 l + bulletC1 r + bulletB2 l + bulletB2 r + bulletA3 l + bulletA3 r (typeA/C) + bulletA4 l + bulletA4 r
				bltcnt++;
				if (bltcnt >= bltinterval[playerType, pPower]) {
					bltcnt = 0;
					mc.playSound (mc.se_playershot);
					//bullet l
					go = Instantiate (playerBullet100ControllerPrefab) as GameObject;
					go.GetComponent<playerBullet100Controller> ().setInitStatus ( 
						(cashTransform.position.x + bulletl1_offsetx), (cashTransform.position.y + bulletl1_offsety+bloffsetp_y[playerType, cPower]),
						s_scale_x[playerType, cPower], s_scale_y[playerType, cPower], bldl1, bltspeed[playerType, pPower] );
					//bullet r
					go = Instantiate (playerBullet100ControllerPrefab) as GameObject;
					go.GetComponent<playerBullet100Controller> ().setInitStatus ( 
						(cashTransform.position.x + bulletr1_offsetx), (cashTransform.position.y + bulletr1_offsety+bloffsetp_y[playerType, cPower]),
						s_scale_x[playerType, cPower], s_scale_y[playerType, cPower], bldr1, bltspeed[playerType, pPower] );
					//bullet l2
					go = Instantiate (playerBullet100ControllerPrefab) as GameObject;
					go.GetComponent<playerBullet100Controller> ().setInitStatus ( 
						(cashTransform.position.x + bulletl2_offsetx), (cashTransform.position.y + bulletl2_offsety+bloffsetp_y[playerType, cPower-1]),
						s_scale_x[playerType, cPower-1], s_scale_y[playerType, cPower-1], bldl2, bltspeed[playerType, pPower] );
					//bullet r2
					go = Instantiate (playerBullet100ControllerPrefab) as GameObject;
					go.GetComponent<playerBullet100Controller> ().setInitStatus ( 
						(cashTransform.position.x + bulletr2_offsetx), (cashTransform.position.y + bulletr2_offsety+bloffsetp_y[playerType, cPower-1]),
						s_scale_x[playerType, cPower-1], s_scale_y[playerType, cPower-1], bldr2, bltspeed[playerType, pPower] );
					//bullet l3
					go = Instantiate (playerBullet100ControllerPrefab) as GameObject;
					go.GetComponent<playerBullet100Controller> ().setInitStatus ( 
						(cashTransform.position.x + bulletl3_offsetx), (cashTransform.position.y + bulletl3_offsety+bloffsetp_y[playerType, cPower-2]),
						s_scale_x[playerType, cPower-2], s_scale_y[playerType, cPower-2], bldl3, bltspeed[playerType, pPower] );
					//bullet r3
					go = Instantiate (playerBullet100ControllerPrefab) as GameObject;
					go.GetComponent<playerBullet100Controller> ().setInitStatus ( 
						(cashTransform.position.x + bulletr3_offsetx), (cashTransform.position.y + bulletr3_offsety+bloffsetp_y[playerType, cPower-2]),
						s_scale_x[playerType, cPower-2], s_scale_y[playerType, cPower-2], bldr3, bltspeed[playerType, pPower] );
					if (playerType != ptTypeB) {
						//bullet l4
						go = Instantiate (playerBullet100ControllerPrefab) as GameObject;
						go.GetComponent<playerBullet100Controller> ().setInitStatus (
							(cashTransform.position.x + bulletl4_offsetx), (cashTransform.position.y + bulletl4_offsety + bloffsetp_y [playerType, cPower - 2]),
							s_scale_x [playerType, cPower - 2], s_scale_y [playerType, cPower - 2], bldl4, bltspeed [playerType, pPower]);
						//bullet r4
						go = Instantiate (playerBullet100ControllerPrefab) as GameObject;
						go.GetComponent<playerBullet100Controller> ().setInitStatus (
							(cashTransform.position.x + bulletr4_offsetx), (cashTransform.position.y + bulletr4_offsety + bloffsetp_y [playerType, cPower - 2]),
							s_scale_x [playerType, cPower - 2], s_scale_y [playerType, cPower - 2], bldr4, bltspeed [playerType, pPower]);
					}
				}
				break;
			case 3:
			case 4:
			case 5:
				//power 3	bulletD1 l + bulletD1 r + bulletC2 l + bulletC2 r + bulletB3 l + bulletB3 r (typeA/C) + bulletA4 l + bulletA4 r
				//power 4	bulletE1 l + bulletE1 r + bulletD2 l + bulletD2 r + bulletC3 l + bulletC3 r (typeA/C) + bulletB4 l + bulletB4 r
				//power 5	bulletF1 l + bulletF1 r + bulletE2 l + bulletE2 r + bulletD3 l + bulletD3 r (typeA/C) + bulletC4 l + bulletC4 r
				bltcnt++;
				if (bltcnt >= bltinterval[playerType, pPower]) {
					bltcnt = 0;
					mc.playSound (mc.se_playershot);
					//bullet l
					go = Instantiate (playerBullet100ControllerPrefab) as GameObject;
					go.GetComponent<playerBullet100Controller> ().setInitStatus ( 
						(cashTransform.position.x + bulletl1_offsetx), (cashTransform.position.y + bulletl1_offsety+bloffsetp_y[playerType, cPower]),
						s_scale_x[playerType, cPower], s_scale_y[playerType, cPower], bldl1, bltspeed[playerType, pPower] );
					//bullet r
					go = Instantiate (playerBullet100ControllerPrefab) as GameObject;
					go.GetComponent<playerBullet100Controller> ().setInitStatus ( 
						(cashTransform.position.x + bulletr1_offsetx), (cashTransform.position.y + bulletr1_offsety+bloffsetp_y[playerType, cPower]),
						s_scale_x[playerType, cPower], s_scale_y[playerType, cPower], bldr1, bltspeed[playerType, pPower] );
					//bullet l2
					go = Instantiate (playerBullet100ControllerPrefab) as GameObject;
					go.GetComponent<playerBullet100Controller> ().setInitStatus ( 
						(cashTransform.position.x + bulletl2_offsetx), (cashTransform.position.y + bulletl2_offsety+bloffsetp_y[playerType, cPower-1]),
						s_scale_x[playerType, cPower-1], s_scale_y[playerType, cPower-1], bldl2, bltspeed[playerType, pPower] );
					//bullet r2
					go = Instantiate (playerBullet100ControllerPrefab) as GameObject;
					go.GetComponent<playerBullet100Controller> ().setInitStatus ( 
						(cashTransform.position.x + bulletr2_offsetx), (cashTransform.position.y + bulletr2_offsety+bloffsetp_y[playerType, cPower-1]),
						s_scale_x[playerType, cPower-1], s_scale_y[playerType, cPower-1], bldr2, bltspeed[playerType, pPower] );
					//bullet l3
					go = Instantiate (playerBullet100ControllerPrefab) as GameObject;
					go.GetComponent<playerBullet100Controller> ().setInitStatus ( 
						(cashTransform.position.x + bulletl3_offsetx), (cashTransform.position.y + bulletl3_offsety+bloffsetp_y[playerType, cPower-2]),
						s_scale_x[playerType, cPower-2], s_scale_y[playerType, cPower-2], bldl3, bltspeed[playerType, pPower] );
					//bullet r3
					go = Instantiate (playerBullet100ControllerPrefab) as GameObject;
					go.GetComponent<playerBullet100Controller> ().setInitStatus ( 
						(cashTransform.position.x + bulletr3_offsetx), (cashTransform.position.y + bulletr3_offsety+bloffsetp_y[playerType, cPower-2]),
						s_scale_x[playerType, cPower-2], s_scale_y[playerType, cPower-2], bldr3, bltspeed[playerType, pPower] );
					if (playerType != ptTypeB) {
						//bullet l4
						go = Instantiate (playerBullet100ControllerPrefab) as GameObject;
						go.GetComponent<playerBullet100Controller> ().setInitStatus (
							(cashTransform.position.x + bulletl4_offsetx), (cashTransform.position.y + bulletl4_offsety + bloffsetp_y [playerType, cPower - 3]),
							s_scale_x [playerType, cPower - 3], s_scale_y [playerType, cPower - 3], bldl4, bltspeed [playerType, pPower]);
						//bullet r4
						go = Instantiate (playerBullet100ControllerPrefab) as GameObject;
						go.GetComponent<playerBullet100Controller> ().setInitStatus (
							(cashTransform.position.x + bulletr4_offsetx), (cashTransform.position.y + bulletr4_offsety + bloffsetp_y [playerType, cPower - 3]),
							s_scale_x [playerType, cPower - 3], s_scale_y [playerType, cPower - 3], bldr4, bltspeed [playerType, pPower]);
					}
				}
				break;
			case 6://(typeA only)
				//power 6	bulletF1 l + bulletF1 r + bulletF2 l + bulletF2 r + bulletE3 l + bulletE3 r + bulletD4 l + bulletD4 r
				bltcnt++;
				if (bltcnt >= bltinterval[playerType, pPower]) {
					bltcnt = 0;
					mc.playSound (mc.se_playershot);
					//bullet l
					go = Instantiate (playerBullet100ControllerPrefab) as GameObject;
					go.GetComponent<playerBullet100Controller> ().setInitStatus ( 
						(cashTransform.position.x + bulletl1_offsetx), (cashTransform.position.y + bulletl1_offsety+bloffsetp_y[playerType, cPower-1]),
						s_scale_x[playerType, cPower-1], s_scale_y[playerType, cPower-1], bldl1, bltspeed[playerType, pPower] );
					//bullet r
					go = Instantiate (playerBullet100ControllerPrefab) as GameObject;
					go.GetComponent<playerBullet100Controller> ().setInitStatus ( 
						(cashTransform.position.x + bulletr1_offsetx), (cashTransform.position.y + bulletr1_offsety+bloffsetp_y[playerType, cPower-1]),
						s_scale_x[playerType, cPower-1], s_scale_y[playerType, cPower-1], bldr1, bltspeed[playerType, pPower] );
					//bullet l2
					go = Instantiate (playerBullet100ControllerPrefab) as GameObject;
					go.GetComponent<playerBullet100Controller> ().setInitStatus ( 
						(cashTransform.position.x + bulletl2_offsetx), (cashTransform.position.y + bulletl2_offsety+bloffsetp_y[playerType, cPower-1]),
						s_scale_x[playerType, cPower-1], s_scale_y[playerType, cPower-1], bldl2, bltspeed[playerType, pPower] );
					//bullet r2
					go = Instantiate (playerBullet100ControllerPrefab) as GameObject;
					go.GetComponent<playerBullet100Controller> ().setInitStatus ( 
						(cashTransform.position.x + bulletr2_offsetx), (cashTransform.position.y + bulletr2_offsety+bloffsetp_y[playerType, cPower-1]),
						s_scale_x[playerType, cPower-1], s_scale_y[playerType, cPower-1], bldr2, bltspeed[playerType, pPower] );
					//bullet l3
					go = Instantiate (playerBullet100ControllerPrefab) as GameObject;
					go.GetComponent<playerBullet100Controller> ().setInitStatus ( 
						(cashTransform.position.x + bulletl3_offsetx), (cashTransform.position.y + bulletl3_offsety+bloffsetp_y[playerType, cPower-2]),
						s_scale_x[playerType, cPower-2], s_scale_y[playerType, cPower-2], bldl3, bltspeed[playerType, pPower] );
					//bullet r3
					go = Instantiate (playerBullet100ControllerPrefab) as GameObject;
					go.GetComponent<playerBullet100Controller> ().setInitStatus ( 
						(cashTransform.position.x + bulletr3_offsetx), (cashTransform.position.y + bulletr3_offsety+bloffsetp_y[playerType, cPower-2]),
						s_scale_x[playerType, cPower-2], s_scale_y[playerType, cPower-2], bldr3, bltspeed[playerType, pPower] );
					//bullet l4
					go = Instantiate (playerBullet100ControllerPrefab) as GameObject;
					go.GetComponent<playerBullet100Controller> ().setInitStatus ( 
						(cashTransform.position.x + bulletl4_offsetx), (cashTransform.position.y + bulletl4_offsety+bloffsetp_y[playerType, cPower-3]),
						s_scale_x[playerType, cPower-3], s_scale_y[playerType, cPower-3], bldl4, bltspeed[playerType, pPower] );
					//bullet r4
					go = Instantiate (playerBullet100ControllerPrefab) as GameObject;
					go.GetComponent<playerBullet100Controller> ().setInitStatus ( 
						(cashTransform.position.x + bulletr4_offsetx), (cashTransform.position.y + bulletr4_offsety+bloffsetp_y[playerType, cPower-3]),
						s_scale_x[playerType, cPower-3], s_scale_y[playerType, cPower-3], bldr4, bltspeed[playerType, pPower] );
				}
				break;
			default:
				break;
			}
		}

		//generate player laser
		//normal or invalid only shot bullet
		if ( ((plMode == plModeNormal) || (plMode == plModeInvalid)) && (mc.subWeapon == mc.subWeaponLaser) ) {
			//player laser
			const int lsrinterval = 3;
			float laser1_offsetx = 0.0f;
			switch (pLaser) {
			case 0:
				//power 0
				//laser none
				break;
			case 1:
			case 2:
			case 3:
			case 4:
			case 5:
				//power 1
				//power 2
				//power 3
				//power 4
				//power 5
				lsrcnt++;
				if (lsrcnt >= lsrinterval) {
					lsrcnt = 0;
					mc.playSound (mc.se_playerlaser);
					//laser
					GameObject go = Instantiate (playerLaser100ControllerPrefab) as GameObject;
					if ((playerType == ptTypeA) || (playerType == ptTypeC)) {
						go.tag = "playerLaser1";
					} else if( playerType == ptTypeB ){
						go.tag = "playerLaser2";
					}
					go.GetComponent<playerLaser100Controller> ().setInitStatus (ls1_x, ls1_y, (
						cashTransform.position.x - laser1_offsetx), (cashTransform.position.y + laser1_offsety[playerType, pLaser]),
						l_scale_x[playerType, pLaser], l_scale_y[playerType, pLaser] );
				}
				break;
			default:
				break;
			}
		}

		//generate player missile
		//normal or invalid only shot bullet
		if ( ((plMode == plModeNormal) || (plMode == plModeInvalid)) && (mc.subWeapon == mc.subWeaponMissile) ) {
			//player missile
			switch (pMissile) {
			case 0:
				//power 0
				//missile none
				break;
			case 1:
			case 2:
			case 3:
			case 4:
			case 5:
				//power 1
				//power 2
				//power 3
				//power 4
				//power 5
				mslcnt++;
				if (mslcnt >= mslintvl[playerType, pMissile]) {
					mslcnt = 0;
					//missile l
					GameObject go = Instantiate (playerMissile100ControllerPrefab) as GameObject;
					if ((playerType == ptTypeA) || (playerType == ptTypeC)) {
						go.tag = "playerMissile1";
					} else if( playerType == ptTypeB ){
						go.tag = "playerMissile2";
					}
					go.GetComponent<playerMissile100Controller> ().setInitStatus ( 0.0f, +1.0f, -1,
						(cashTransform.position.x - 0.24f),(cashTransform.position.y + (-0.35f)) );
					//missile l
					go = Instantiate (playerMissile100ControllerPrefab) as GameObject;
					if ((playerType == ptTypeA) || (playerType == ptTypeC)) {
						go.tag = "playerMissile1";
					} else if( playerType == ptTypeB ){
						go.tag = "playerMissile2";
					}
					go.GetComponent<playerMissile100Controller> ().setInitStatus ( 0.0f, +1.0f, +1,
						(cashTransform.position.x + 0.24f), (cashTransform.position.y + (-0.35f)) );
				}
				break;
			default:
				break;
			}
		}

		//player rebirth mode process
		if (plMode == plModeRebirth) {
			cashTransform.Translate (0.0f, 0.12f, 0.0f);
			if (cashTransform.position.y >= -1.7f) {
				this.setMode (plModeInvalid);
				if (rebirthVoice == true) {
					rebirthVoice = false;
					mc.playSound (mc.vo210);
				}
			}
		}

		////interval process

		//interval count
		intervalCnt++;
		if (intervalCnt >= 2) {
			intervalCnt = 0;

			//bomb interval
			if (bInt > 0) {
				bInt--;
			}

			//player invalid/rebirth mode blink process
			if (blinkTimer > 0) {
				blinkTimer--;
				if ( blinkTimer <= 0 ) {
					blinkCnt--;
					if ((blinkCnt % 2) == 0) {
						//display on
						sr.color = new Color (255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f, blinkAlpha); 
					} else {
						//display off
						sr.color = new Color (255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f, 0.0f / 255.0f); 
					}
					if (blinkCnt <= 0) {
						if (plMode == plModeRebirth) {
							blinkCnt = blinkCntVal+1;
						} else if (plMode == plModeInvalid) {
							this.setMode (plModeNormal);
						} else {
							this.setMode (plModeNormal);	//for safe
						}
					}
					blinkTimer = blinkTimerVal;
				}
			}

			//player explosion process
			if (exCnt >= 1) {
				if ((exCnt % 3) == 0) {
					float ex = cashTransform.position.x + (Random.Range(-0.3f,0.3f));
					float ey = cashTransform.position.y + (Random.Range(-0.3f,0.3f));
					mc.generateExplosionLargeEffect(ex,ey);
				}
				exCnt--;
				//player rebirth mode set
				if (exCnt <= 0) {
					if (pNum <= 0) {
						//player num 0 game over
						this.setMode (plModeNoExist);
						if ((mc.getDebugMode() == true) && (mc.debug_jikimugen == true) && (mc.gameMode == mc.gamemodePlayDebug)) {	//debug mode player unlimit?
							//debug ---->
							//player status init
							this.setPlayerStatusInit();
							//rebirth start
							this.setMode (plModeRebirth);
							rebirthVoice = true;
							//debug <----
						} else {
							//game over process
							mc.setGameOver();
						}
					} else {
						//rebirth item
						this.generateRebirthItem ();
						//next hit point
						if (playerType == ptTypeC) {
							pHp = pHpInitC;
						} else {
							pHp = pHpInitAB;
						}
						//display shield
						mc.dispShield (pHp);
						//next bomb
						bNum = bNum + 3;
						if (playerType == ptTypeC) {
							if (bNum >= pBombMaxC) {
								bNum = pBombMaxC;
							}
						} else {
							if (bNum >= pBombMaxAB) {
								bNum = pBombMaxAB;
							}
						}
						//display bomb
						mc.dispBomb( bNum);
						//bomb interval
						bInt=0;
						//shot power
						pPower = pPowerInit;
						//sub weapon power
						pLaser = pLaserInit;
						pMissile = pMissileInit;
						//option reset
						this.optionReset();
						//rebirth start
						this.setMode (plModeRebirth);
						rebirthVoice = true;
						//player display x cnt
						pDispx = 0;
					}
				}
			}

		}

	}


	//public
	//collision
	public void OnTriggerEnter2D(Collider2D coll){
		string cotag = coll.gameObject.tag;
		if (cotag == "enemy") {
			//collision enemy
			if ((mc.getDebugMode() == false) || (mc.debug_jikimuteki == false) || (mc.gameMode != mc.gamemodePlayDebug)) {
				if (plMode == plModeNormal) {
					mc.generatePlayerDamageEffect (cashTransform.position.x, cashTransform.position.y);
					this.playerHit ();
				}
			}
		} else if (cotag == "enemyBullet") {
			//collision enemy bullet
			if ((mc.getDebugMode() == false) || (mc.debug_jikimuteki == false) || (mc.gameMode != mc.gamemodePlayDebug)) {
				if (plMode == plModeNormal) {
					mc.generatePlayerDamageEffect (cashTransform.position.x, cashTransform.position.y);
					this.playerHit ();
				}
			}
		} else if (cotag == "powerItem") {
			//collision power item
			if ( (plMode == plModeNormal) || (plMode == plModeInvalid) ) {
				GameObject go = coll.gameObject;
				int itype = go.GetComponent<powerup100Controller> ().getType();
				this.powerItemProcess( itype );
			}
		} else if (cotag == "scoreItem") {
			//collision score item(star)
			if ( (plMode == plModeNormal) || (plMode == plModeInvalid) ) {
				this.powerItemProcess( itype_score );
			}
		} else {
			//collision other
			//nop
		}
	}


	//private
	//player hit process
	private void playerHit(){
		if (pHp <= 0) {	//複数回呼ばれ対策
			return;
		}
		//hit point dec
		pHp = pHp - 1;
		//player damage in stage
		mc.haveDamageStg();
		if (pHp <= 0) {
			pNum = pNum - 1;
			if (pNum >= 0) {
				//display player
				mc.dispPlayer (pNum);
			}
			mc.setPlayerColor (1);	//red
			//play voice (player-1)
			mc.playSound (mc.vo160);
			//explotion effect start
			mc.generateExplosionLargeEffect ((cashTransform.position.x), (cashTransform.position.y));	//first
			//player no exist mode set
			this.setMode (plModeNoExist);
			//explosion cnt
			exCnt = 30;
			//sprite center
			this.setCenterSprite();
			//lost player in stage
			mc.lostPlayerStg();
		} else {
			//se
			mc.playSound(mc.se_playerdamage);
			//play voice (damage)
			mc.playSound ( mc.vo150 );
			//player invalid mode set
			this.setMode( plModeInvalid );
			//display shield
			mc.dispShield( pHp );
		}
		//display damage
		mc.dispSubMessage( 0.0f, 4.0f, 0.0f, -1.0f, "DAMAGE", 3 );	//red
		mc.setShieldColor (1);	//red
	}

	//set mode
	private void setMode( int md ){
		int lastmd = plMode;
		this.plMode = md;
		switch (md) {
		case plModeNormal:
			sr.color = new Color (1.0f, 1.0f, 1.0f, 1.0f);
			this.tag = "player";
			blinkCnt = 0;
			blinkTimer = 0;
			break;
		case plModeInvalid:
			sr.color = new Color (1.0f, 1.0f, 1.0f, blinkAlpha);
			this.tag = "unavailablePlayer";
			if (lastmd == plModeRebirth) {	//点滅継続を自然に見せるため
				blinkCnt = blinkCnt + blinkCntVal;
				//timer継続
			} else {
				blinkCnt = blinkCntVal;
				blinkTimer = blinkTimerVal;
			}
			break;
		case plModeRebirth:
			sr.color = new Color (1.0f, 1.0f, 1.0f, blinkAlpha);
			this.tag = "unavailablePlayer";
			blinkCnt = blinkCntVal;
			blinkTimer = blinkTimerVal;
			rebirthVoice = false;
			//potision
			cashTransform.position = new Vector3 (0.0f, -5.5f, 0.0f);
			break;
		case plModeOnBase:
			//status is set at public setPlayerStatus
			sr.color = new Color (1.0f, 1.0f, 1.0f, 1.0f);
			this.tag = "unavailablePlayer";
			blinkCnt = 0;
			blinkTimer = 0;
			//sprite change
			if (playerType == ptTypeA) {
				sr.sprite = player120_0;
			} else if (playerType == ptTypeB) {
				sr.sprite = player110_0;
			} else if (playerType == ptTypeC) {
				sr.sprite = player100;
			}
			break;
		case plModeNoExist:
			sr.color = new Color (1.0f, 1.0f, 1.0f, 0.0f);
			this.tag = "unavailablePlayer";
			blinkCnt = 0;
			blinkTimer = 0;
			break;
		default:
			break;
		}
	}

	//set center sprite
	private void setCenterSprite(){
		pDispx = 0;
		if (playerType == ptTypeA) {
			if (sr.sprite != player120_0) {
				sr.sprite = player120_0;
			}
		} else if (playerType == ptTypeB) {
			if (sr.sprite != player110_0) {
				sr.sprite = player110_0;
			}
		} else if (playerType == ptTypeC) {
			if (sr.sprite != player100) {
				sr.sprite = player100;
			}
		}
	}

	//power item process
	private void powerItemProcess( int type ){
		//sub message pos,mov info
		float smsg_offsetx = smsgInfoTable[smsgCnt].xoffset;
		float smsg_offsety = smsgInfoTable[smsgCnt].yoffset;
		float smsg_xx = smsgInfoTable [smsgCnt].xx;
		float smsg_yy = smsgInfoTable [smsgCnt].yy;
		smsgCnt++;
		if( smsgCnt >= smsgCntMax ){
			smsgCnt = 0;
		}
		//type process
		string st = "";
		int cl = 0;
		switch (type) {
		case itype_power:
			//shot power up
			//power max?
			if (pPower >= pPowerMax) {
				//score add
				mc.addGameScore( 2000 );
				//se
				mc.playSound(mc.se_getstar);
				//sub message display 
				mc.dispSubMessage( (cashTransform.position.x+smsg_offsetx), (cashTransform.position.y+smsg_offsety), smsg_xx, smsg_yy, "2000pts", 1 );
			} else {
				//shot power +1
				pPower++;
				//voice
				mc.playSound (mc.vo140);
				//se
				mc.playSound(mc.se_getitem);
				//sub message display 
				st = "SHOT lv."+(pPower+1).ToString("D");
				cl = 1;
				if (pPower >= pPowerMax) {
					st = st + " MAX!!";
				}
				mc.dispSubMessage( (cashTransform.position.x+smsg_offsetx), (cashTransform.position.y+smsg_offsety), smsg_xx, smsg_yy, st, cl );
			}
			break;
		case itype_laser:
			//laser
			//laser max?
			if (pLaser >= pLaserMax) {
				//score add
				mc.addGameScore( 2000 );
				//se
				mc.playSound(mc.se_getstar);
				//sub message display 
				mc.dispSubMessage( (cashTransform.position.x+smsg_offsetx), (cashTransform.position.y+smsg_offsety), smsg_xx, smsg_yy, "2000pts", 1 );
			} else {
				//laser power +1
				pLaser++;
				//voice
				mc.playSound (mc.vo140);
				//se
				mc.playSound(mc.se_getitem);
				//sub message display
				cl = 1;
				if (pLaser == 1) {
					st = "GET LASER lv.1";
				}else{
					st = "LASER lv." + (pLaser).ToString ("D");
					if (pLaser >= pLaserMax) {
						st = st + " MAX!!";
					}
				}
				mc.dispSubMessage( (cashTransform.position.x+smsg_offsetx), (cashTransform.position.y+smsg_offsety), smsg_xx, smsg_yy, st, cl );
			}
			break;
		case itype_missile:
			//missile
			//missile max?
			if (pMissile >= pMissileMax) {
				//score add
				mc.addGameScore( 2000 );
				//se
				mc.playSound(mc.se_getstar);
				//sub message display 
				mc.dispSubMessage( (cashTransform.position.x+smsg_offsetx), (cashTransform.position.y+smsg_offsety), smsg_xx, smsg_yy, "2000pts", 1 );
			} else {
				//missile power +1
				pMissile++;
				//voice
				mc.playSound (mc.vo140);
				//se
				mc.playSound(mc.se_getitem);
				//sub message display
				cl = 1;
				if (pMissile == 1) {
					st = "GET MISSILE lv.1";
				}else{
					st = "MISSILE lv." + (pMissile).ToString ("D");
					if (pMissile >= pMissileMax) {
						st = st + " MAX!!";
					}
				}
				mc.dispSubMessage( (cashTransform.position.x+smsg_offsetx), (cashTransform.position.y+smsg_offsety), smsg_xx, smsg_yy, st, cl );
			}
			break;
		case itype_option:
			//option
			//option max?
			if (oNum >= pOptionMax) {
				//score add
				mc.addGameScore( 2000 );
				//se
				mc.playSound(mc.se_getstar);
				//sub message display 
				mc.dispSubMessage( (cashTransform.position.x+smsg_offsetx), (cashTransform.position.y+smsg_offsety), smsg_xx, smsg_yy, "2000pts", 1 );
			} else {
				//option add
				this.addOption ();
				//voice
				mc.playSound (mc.vo140);
				//se
				mc.playSound(mc.se_getitem);
				//sub message display
				cl = 1;
				st = "OPTION lv." + (oNum/2).ToString("D");
				if (oNum >= pOptionMax) {
					st = st + " MAX!!";
				}
				mc.dispSubMessage( (cashTransform.position.x+smsg_offsetx), (cashTransform.position.y+smsg_offsety), smsg_xx, smsg_yy, st, cl );
			}
			break;
		case itype_bomb:
			//bomb
			//bomb max?
			int bmax = ((playerType == ptTypeC)?pBombMaxC:pBombMaxAB);
			if ( bNum >= bmax ) {
				//score add
				mc.addGameScore ( 3000 );
				//se
				mc.playSound(mc.se_getstar);
				//sub message display 
				mc.dispSubMessage( (cashTransform.position.x+smsg_offsetx), (cashTransform.position.y+smsg_offsety), smsg_xx, smsg_yy, "3000pts", 1 );
			} else {
				//bomb +1
				bNum++;
				//disp bomb
				mc.dispBomb ( bNum );
				mc.setBombNumColor ();	//white
				//se
				mc.playSound(mc.se_getitem);
				//sub message display 
				cl = 1;
				st = "BOMB +1";
				if (bNum >= bmax) {
					st = st + " MAX!!";
				}
				mc.dispSubMessage( (cashTransform.position.x+smsg_offsetx), (cashTransform.position.y+smsg_offsety), smsg_xx, smsg_yy, st, cl );
			}
			break;
		case itype_shield:
			//shield
			//hitpoint(shild+1) max?
			int hpmax = (playerType==ptTypeC?pHpMaxC:pHpMaxAB);
			if ( pHp >= hpmax ) {
				//score add
				mc.addGameScore ( 2500 );
				//se
				mc.playSound(mc.se_getstar);
				//sub message display 
				mc.dispSubMessage( (cashTransform.position.x+smsg_offsetx), (cashTransform.position.y+smsg_offsety), smsg_xx, smsg_yy, "2500pts", 1 );
			} else {
				//hitpoint +1
				pHp++;
				//disp shield
				mc.dispShield (pHp);
				mc.setShieldColor (0);	//white
				//se
				mc.playSound(mc.se_getitem);
				//sub message display
				cl = 1;
				st = "SHIELD +1";
				if (pHp >= hpmax) {
					st = st + " MAX!!";
				}
				mc.dispSubMessage( (cashTransform.position.x+smsg_offsetx), (cashTransform.position.y+smsg_offsety), smsg_xx, smsg_yy, st, cl );
			}
			break;
		case itype_score:
			//star
			//add star num
			mc.addStarNum ();
			//disp star num
			mc.dispStarNum ();
			//score add
			mc.addGameScore ( 200 );
			//se
			mc.playSound (mc.se_getstar);
			//sub message display 
			mc.dispSubMessage( (cashTransform.position.x+smsg_offsetx), (cashTransform.position.y+smsg_offsety), smsg_xx, smsg_yy, "200pts", 0, true );
			break;
		case itype_1up:
			//1up
			//player num max?
			if ( pNum >= pPlayerMax ) {
				//score add
				mc.addGameScore ( 10000 );
				//se
				mc.playSound(mc.se_getstar);
				//sub message display 
				mc.dispSubMessage( (cashTransform.position.x+smsg_offsetx), (cashTransform.position.y+smsg_offsety), smsg_xx, smsg_yy, "10000pts", 1 );
			} else {
				//player +1
				pNum++;
				//disp player num
				mc.dispPlayer ( pNum );
				mc.setPlayerColor (0);	//white
				//se
				mc.playSound(mc.se_getitem);
				//sub message display 
				st = "PLAYER 1UP";
				cl = 2;
				if (pNum >= pPlayerMax) {
					st = st + " MAX!!";
				}
				mc.dispSubMessage( (cashTransform.position.x+smsg_offsetx), (cashTransform.position.y+smsg_offsety), smsg_xx, smsg_yy, st, cl );
			}
			break;
		default:
			break;
		}
	}

	//add option
	private void addOption(){
		if (oNum >= pOptionMax) {
			return;
		}
		//option num
		int idx=oNum;
		//generate option
		//option0,2,4,6,8
		optionCtr [idx] = Instantiate (playerOption100ControllerPrefab) as GameObject;
		poc [idx] = optionCtr [idx].GetComponent<playerOption100Controller> ();
		poc [idx].setStatus (cashTransform.position.x, cashTransform.position.y, plMode, playerType, pPower );
		idx++;
		oNum++;
		//option1,3,5,7,9
		optionCtr [idx] = Instantiate (playerOption100ControllerPrefab) as GameObject;
		poc [idx] = optionCtr [idx].GetComponent<playerOption100Controller> ();
		poc [idx].setStatus (cashTransform.position.x, cashTransform.position.y, plMode, playerType, pPower );
		oNum++;;
		//all option init (shot回転の同期をとるため)
		for (int i = 0; i < oNum; i++) {
			poc [i].initStatus (i);
		}
	}


	//public
	//bomb process
	public void tapBombButton(){
		if (bInt <= 0) {
			bInt = 0;
			if ( (mc.getPause () == false) && (mc.getGameOver () == false) && (mc.getResultDisplay () == false) ) {	//no pause and no gameover and no result dispaly
				if ((bNum >= 1) || ((mc.getDebugMode () == true) && (mc.debug_bombmugen) && (mc.gameMode == mc.gamemodePlayDebug))) {	//bomb exist or bomb debug mode
					if ((plMode == plModeNormal) || (plMode == plModeInvalid)) {
						//generate bomb
						GameObject go;
						go = Instantiate (bombControllerPrefab) as GameObject;
						if ((playerType == ptTypeA) || (playerType == ptTypeB)) {
							go.tag = "bomb1";
						} else if (playerType == ptTypeC) {
							go.tag = "bomb2";
						}
//						go.transform.position = new Vector3 (0, 0, 0);
						//generate bomb laser
						go = Instantiate (bombLaserControllerPrefab) as GameObject;
						go.GetComponent<bombLaserController> ().setInitStatus (cashTransform.position.x, cashTransform.position.y);
						//player bomb se play
						mc.playSound(mc.se_bomb);
						//generate screen flash effect
						mc.generateScreenFlashEffect ();
						//generate screen shake effect
						mc.generateScreenShakeEffect (20);
						//bomb dec
						bNum--;
						if (bNum <= 0) {	//for safe
							bNum = 0;
						}
						//display bomb
						mc.dispBomb (bNum);
						//bomb use in stage 
						mc.useBombStg();
						//bomb interval
						bInt = 8;
					}
				} else {
					//play se bomb empty
					mc.playSound (mc.se_bombempty);
				}
			}
		}
	}

	//set player mode (for playerbase100)
	public void setPlayerMode( int mode ){
		this.setMode( mode );
	}

	//get player mode
	public int getPlayerMode(){
		return this.plMode;
	}

	//set player status (for playerbase100)
	public void setPlayerStatus( int mode, float x, float y, float s, float pyy = 0 ){

		//player mode
		this.setMode( mode );

		if (pyy != 0) {
			//move
			cashTransform.Translate (0, pyy, 0);
		} else {
			//potision
			cashTransform.position = new Vector3 (x, y, 0.0f);

			//scale
			cashTransform.localScale = new Vector3 (xsbase * s, ysbase * s);
		}
	}

	//set player speed
	public void setPlayerSpeed( int ps ){
		switch (ps) {
		case 0x00:
			//set player speed slow
			pmovk = pmovk_slow;
			break;
		case 0x01:
			//set player speed normal
			pmovk = pmovk_normal;
			break;
		case 0x02:
			//set player speed fast
			pmovk = pmovk_fast;
			break;
		default:
			break;
		}
	}

	//set player type
	public void setPlayerType( int pt ){
		CircleCollider2D cc = GetComponent<CircleCollider2D> ();
		switch (pt) {
		case ptTypeA:
			this.playerType = ptTypeA;
			sr.sprite = player120_0;
			cashTransform.localScale = new Vector3 (xsbase_typeA, ysbase_typeA, 1.0f);
			xsbase = xsbase_typeA;
			ysbase = ysbase_typeA;
			cc.radius = cradius_typeA;
			break;
		case ptTypeB:
			this.playerType = ptTypeB;
			sr.sprite = player110_0;
			cashTransform.localScale = new Vector3 (xsbase_typeB, ysbase_typeB, 1.0f);
			xsbase = xsbase_typeB;
			ysbase = ysbase_typeB;
			cc.radius = cradius_typeB;
			break;
		case ptTypeC:
			sr.sprite = player100;
			cashTransform.localScale = new Vector3 (xsbase_typeC, ysbase_typeC, 1.0f);
			xsbase = xsbase_typeC;
			ysbase = ysbase_typeC;
			cc.radius = cradius_typeC;
			this.playerType = ptTypeC;
			break;
		default:
			break;
		}
	}

	//option reset
	public void optionReset(){
		//option destroy (reserve default 2option)
		int targetOnum = oNum-1;
		for (int i = targetOnum; i >= 2 ; i--) {
			GameObject.Destroy (optionCtr [i]);
			mc.decObj ();
			oNum--;
		}
	}

	//set option (debug menu)
	public void addOptionDebug(){
		this.addOption ();
	}

	//set player status init
	public void setPlayerStatusInit(){
		//player number
		pNum = pPlayerInit;
		//display player
		mc.dispPlayer( pNum );

		//hit point
		if (playerType == ptTypeC) {
			pHp = pHpInitC;
		} else {
			pHp = pHpInitAB;
		}
		//display shield
		mc.dispShield (pHp);

		//bomb number
		bNum = pBombInit;
		//display bomb
		mc.dispBomb( bNum );
		//bomb interval
		bInt=0;

		//player shot power
		pPower = pPowerInit;

		//player laser power
		pLaser = pLaserInit;

		//player missile power
		pMissile = pMissileInit;

		//option
		this.optionReset();

		//scale
		cashTransform.localScale = new Vector3 (xsbase * 1.0f, ysbase * 1.0f, 1.0f);

		//player display x cnt
		pDispx = 0;

		//explosion cnt
		exCnt = 0;
	}

	//set player status init debug mode
	public void setPlayerStatusInitDebugMode(){
		//player number
		pNum = pPlayerInit;
		//display player
		mc.dispPlayer( pNum );

		//hit point
		if (playerType == ptTypeC) {
			pHp = pHpInitC;
		} else {
			pHp = pHpInitAB;
		}
		//display shield
		mc.dispShield (pHp);

		//bomb number
		bNum = pBombInit;
		//display bomb
		mc.dispBomb( bNum );
		//bomb interval
		bInt=0;

		//player shot power
//		pPower = pPowerInit;	//set debug menu 

		//player laser power
//		pLaser = pLaserInit;	//set debug menu

		//player missile power
//		pMissile = pMissileInit;	//set debug menu

		//option
//		this.optionReset();	//set debug menu

		//scale
		cashTransform.localScale = new Vector3 (xsbase * 1.0f, ysbase * 1.0f, 1.0f);

		//player display x cnt
		pDispx = 0;

		//explosion cnt
		exCnt = 0;
	}

	//generate rebirth item
	public void generateRebirthItem(){
		//shot power
		int pcnt = 0;
		if( pPower > 0 ){
			pcnt++;
		}
		if(pPower >= 4 ){
			pcnt++;
		}
		//s.w. power
		int swcnt = 0;
		if ((pLaser > 0) || (pMissile > 0)) {
			swcnt++;
		}
		if ((pLaser >= 4) || (pMissile > 4)) {
			swcnt++;
		}
		//option
		int opcnt = 0;
		if( oNum >= 3 ){
			opcnt++;
		}
		if( oNum >= 7 ){
			opcnt++;
		}

		//generate
		for (int i = 0; i < pcnt; i++) {
			mc.generatePowerup100 ( itype_power, cashTransform.position.x + Random.Range (-0.7f, +0.7f), cashTransform.position.y - 0.4f - Random.Range (0.0f, 0.3f) );
		}
		for (int i = 0; i < swcnt; i++) {
			mc.generatePowerup100 ( itype_laser, cashTransform.position.x + Random.Range (-0.7f, +0.7f), cashTransform.position.y - 0.4f - Random.Range (0.0f, 0.3f) );
		}
		for (int i = 0; i < opcnt; i++) {
			mc.generatePowerup100 ( itype_option, cashTransform.position.x + Random.Range (-0.7f, +0.7f), cashTransform.position.y - 0.4f - Random.Range (0.0f, 0.3f) );
		}
	}

	//get player x move info
	public int getPlayerxMoveInfo(){
		return this.pXmov;
	}

	//get player pos
	public Vector2 getPlayerPos(){
		return ppos;
	}

	//get player pos mov (for laser)
	public Vector2 getPlayerPosMov(){
		return pposmov;
	}

	//get player pos for laser
	public Vector2 getPlayerPosLaser(){
		return pposls;
	}

	//get player num
	public int getPlayerNum(){
		return pNum;
	}

	//get player hitpoint
	public int getPlayerHitPoint(){
		return pHp;
	}

	//get bomb num
	public int getBombNum(){
		return bNum;
	}

}
