using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class displayController : MonoBehaviour {
	
	//public
	//public title screen prefab
	public GameObject titleParentPrefab;
	//public title screen debug menu prefab
	public GameObject debugMenuDispPrefab;
	//public credit screen prefab
	public GameObject creditListDispPrefab;
	//public debug menu screem prefab
	public GameObject debugMenuListDispPrefab;
	//public sub menu continue button prefab
	public GameObject continueButtonPrefab;
	//public sub menu goto title button prefab
	public GameObject gotoTitleButtonPrefab;
	//public sub message controller prefab
	public GameObject subMessageControllerPrefab;
	//public result display base frame prefab
	public GameObject resultBasefPrefab;
	//public result display goto next stage button prefab
	public GameObject nextStageButtonPrefab;
	//public ending text display prefab
	public GameObject endingTextDispPrefab;
	//font
	public Font font1;	//SNslitBK
	public Font font2;	//patrickX

	//public main message disp mode
	public int mainDispModeTimeDisp = mdModeTimeDisp;
	public int mainDispModeDispStart = mdModeDispStart;
	public int mainDispModeDispDelete = mdModeDispDelete;


	//private
	//const
	//position
	//move base speed
	const float move_base = 1.0f;
	//up info
	const float scoretxt_x = 94.0f;
	const float scoredisp_x = 94.0f;
	const float shieldtxt_x = 0.0f;
	const float shielddisp_x = 0.0f;
	const float playertxt_x = -106.0f;
	const float playerdisp_x = -106.0f;
	const float disp_u1ybase = 268.0f;
	const float disp_u2ybase = 248.0f;
	const float disp_uoffset = 50.0f;
	//pause info
	const float pauseb_xbase = 140.0f;
	const float pauseb_y = 182.0f;
	const float pauseb_offset = 80.0f;
	//bomb info
	const float bombb_y = -202.0f;
	const float bombtxt_y = -248.0f;
	const float bombdisp_y = -268.0f;
	const float bombb_xbase = -124.0f;
	const float bombtxt_xbase = -106.0f;
	const float bombdisp_xbase = -26.0f;
	const float disp_bomboffset = -104.0f;
	const float disp_bombtxtoffset = -104.0f;
	const float disp_bombdispoffset = -104.0f;
	//star info
	const float startxt_y = 230.0f;
	const float stardisp_y = 210.0f;
	const float startxt_xbase = 107.0f;
	const float stardisp_xbase = 27.5f;
	const float disp_startxtoffset = 101.0f;
	const float disp_stardispoffset = 101.5f;
	//enemy hp
	const float ehbf_y = -35.0f;
	const float ehb_y = -31.0f;
	const float eh_y = -31.0f;
	const float eh_xbase = 140.0f;
	const float eh_offset = 79.0f;
	const float eh_yoffset = 93.0f;
	//display mode
	const int dpModeNoDisp = 0x00;
	const int dpModeDispOn = 0x01;
	const int dpModeDispIn = 0x02;
	const int dpModeDispOut = 0x03;
	//enemy hp mode
	const int ehModeNoDisp = 0x00;
	const int ehModeDispOn = 0x01;
	const int ehModeDispIn = 0x02;
	const int ehModeDispOut = 0x03;
	//main message display mode
	const int mdModeTimeDisp = 0x00;
	const int mdModeDispStart = 0x01;
	const int mdModeDispDelete = 0x02;
	//description message table
	string[] descriptionStringTable = new string[]{
		//																									//<--ここまで
		"基本説明:自機を操作して敵弾を避け、敵を倒してゲームを進めていきます。全3面です。",
		"操作説明1:画面の任意の場所をタッチして自機を操作します。自機の弾は自動で発射します。",
		"操作説明2:左下のBボタンを押すとボムを一つ消費してボムが発動し敵と敵の弾を一掃し一定時間ボムレーザーを発射します。",
		"操作説明3:右上のPAUSEボタンを押すとゲームが一時停止状態になります。そこからタイトル画面へ戻る事もできます。",
		"操作説明4:自機が敵の攻撃を受けるとSHIELDを一つ消費します。SHIELDがない状態で攻撃を受けるとPLAYERを一つ消費します。",
		"操作説明5:PLAYERがない状態で攻撃を受けるとゲームオーバーです。CONTINUEかタイトル画面へ戻るを選択してください。",
		"操作説明6:敵を倒すとアイテムが出現する事もあります。アイテム取得により自機がパワーアップします。",
		"装備説明1:ノーマルショット:メインの武器で標準装備です。機体により性能が異なります。アイテム取得でパワーアップします。",
		"装備説明2:レーザー:通常の攻撃の他に敵弾を小さくする効果があります。Type-Bの機体ではさらに敵弾を消す事が可能です。",
		"装備説明3:ミサイル:発射速度が遅いですが攻撃力が高く、さらに爆風で敵を誘爆させる事ができます。",
		"装備説明4:オプション:自機を追従しながら広範囲の攻撃を行います。",
		"装備説明5:ボム:敵と敵弾を一掃し、一定時間ボムレーザーを発射します。敵弾を星アイテムにする事が可能です。",
		"アイテム説明1:「P」:メインショットがパワーアップします。",
		"アイテム説明2:「L」:レーザーを装備します。装備済みの場合はレーザーがパワーアップします。",
		"アイテム説明3:「M」:ミサイルを装備します。装備済みの場合はミサイルがパワーアップします。",
		"アイテム説明4:「O」:自機のオプション数が増えます。",
		"アイテム説明5:「B」:ボム保持数が一つ増えます。",
		"アイテム説明6:「S」:シールドを一つ回復します。",
		"アイテム説明7:「1UP」:自機の数が一つ増えます。",
		"アイテム説明8:「星」:得点を得られます。",
		"攻略法1:自機と敵弾の当たり判定は見た目より小さく、敵弾の合間をすり抜けて攻撃を避ける事が可能です。",
		"攻略法2:自分のプレイスタイルにあわせて自機のタイプとサブウェポンを選択します。(攻略法3〜7を参照)",
		"攻略法3:「圧倒的な火力が欲しい場合」:ミサイル選択:パワーアップしたミサイルは強力です。特にTYPE-Bは強力です。",
		"攻略法4:「敵弾の弾幕を容易に避けたい場合」:レーザー選択:敵弾を小さく、またはTYPE-Bで消す事が可能です。",
		"攻略法5:「ボスを楽に倒したい」:TYPE-C選択:強力なボムの連続攻撃が有効です。",
		"攻略法6:「バランスのとれた機体で進めたい」:TYPE-A選択:メインショットとサブウェポンのバランスが良いです。",
		"攻略法7:「高得点を得たい」:敵弾の多い時にボム使用:星を多く取得可能。TYPE-Cはボムレーザーの時間が長いです。",
		"GALAXYUNKOとは?:全てが謎につつまれています。意味、由来など、誰にもわかりません。(製作者にも)",
	};
	//																																//<--ここまで
	readonly string dscStrGlEasy = "ゲームモードEASY:敵の攻撃が少なく敵弾の速度が遅い、低難易度のゲームモードです。";
	readonly string dscStrGlNormal = "ゲームモードNORMAL:このゲームの標準的な難易度のゲームモードです。";
	readonly string dscStrGlHard = "ゲームモードHARD:敵の攻撃が多く敵弾の速度が速い、高難易度のゲームモードです。";
	readonly string dscStrPsSlow = "スピードSLOW:スワイプ量の1.0倍の速度で自機が動きます。";
	readonly string dscStrPsNormal = "スピードNORMAL:スワイプ量の1.2倍の速度で自機が動きます。";
	readonly string dscStrPsFast = "スピードFAST:スワイプ量の1.8倍の速度で自機が動きます。";
	readonly string dscStrSwLaser = "レーザー:アイテム取得で装備します。敵弾を小さくする効果があります。Type-Bの機体ではさらに敵弾を消す事が可能です。";
	readonly string dscStrSwMissile = "ミサイル:アイテム取得で装備します。発射速度が遅いですが攻撃力が高く、さらに爆風で敵を誘爆させる事ができます。";
	readonly string dscStrPtTypeA = "TYPE-A:メインショット重視のスタンダードな機体です。";
	readonly string dscStrPtTypeB = "TYPE-B:サブウェポン重視のテクニカルな機体です。";
	readonly string dscStrPtTypeC = "TYPE-C:防御力が高くボムが強力な機体です。";


	//local
	//component cash
	Transform cashTransform;
	GameObject mainCtr;
	mainController mc;
	//main message
	GameObject mainMessageDisp;
	Text mainMessageDispText;
	//sub menu
	GameObject continueButton;
	GameObject toTitleButton;
	//title ui
	GameObject verDisp;
	Text verDispText;

	GameObject titleParent;

	GameObject titleDispParent;
	Image titleDispParentImage;
	GameObject titleDisp;
	Text titleDispText;
	GameObject titleDispB;
	Text titleDispBText;

	GameObject gamelevelDisp;
	Text gamelevelDispText;
	GameObject glEasy;
	Text glEasyText;
	GameObject glNormal;
	Text glNormalText;
	GameObject glHard;
	Text glHardText;

	GameObject playerspeedDisp;
	Text playerspeedDispText;
	GameObject psSlow;
	Text psSlowText;
	GameObject psNormal;
	Text psNormalText;
	GameObject psFast;
	Text psFastText;

	GameObject subWeaponDisp;
	Text subWeaponDispText;
	GameObject swLaser;
	Text swLaserText;
	GameObject swMissile;
	Text swMissileText;

	GameObject playerTypeDisp;
	Text playerTypeDispText;

	GameObject ptADispf;
	Image ptADispfImage;
	GameObject ptADisp;
	Image ptADispImage;
	GameObject ptANameDisp;
	Text ptANameDispText;

	GameObject ptBDispf;
	Image ptBDispfImage;
	GameObject ptBDisp;
	Image ptBDispImage;
	GameObject ptBNameDisp;
	Text ptBNameDispText;

	GameObject ptCDispf;
	Image ptCDispfImage;
	GameObject ptCDisp;
	Image ptCDispImage;
	GameObject ptCNameDisp;
	Text ptCNameDispText;

	GameObject gamestartDispf;
	Image gamestartDispfImage;
	GameObject gamestartDisp;
	Text gamestartDispText;

	GameObject creditDispf;
	Image creditDispfImage;
	GameObject creditDisp;
	Text creditDispText;

	GameObject descriptionDispf;
	Image descriptionDispfImage;
	GameObject descriptionDisp;
	Text descriptionDispText;
	GameObject descriptionArrowLeftDisp;
	Text descriptionArrowLeftDispText;
	GameObject descriptionArrowRightDisp;
	Text descriptionArrowRightDispText;

	GameObject debugMenuDisp;
	Text debugMenuDispText;
	//credit ui
	GameObject creditListDisp;
	Text creditListDispText;
	//debug menu
	GameObject debugMenuListDisp;
	Text debugMenuListDispText;
	GameObject jikimuteki;
	Toggle jikimutekiToggle;
	GameObject jikimugen;
	Toggle jikimugenToggle;
	GameObject bombmugen;
	Toggle bombmugenToggle;
	GameObject debugstatus;
	Text debugStatusText;
	GameObject spower;
	Slider spowerSlider;
	GameObject option;
	Slider optionSlider;
	GameObject sweapon;
	Slider sweaponSlider;
	GameObject startpos;
	Dropdown startposDropdown;
	GameObject debugstart;
	Button debugstartButton;
	GameObject debugTitle;
	Button debugTitleButton;
	//game info ui
	GameObject scoreTxt;
	GameObject scoreDisp;
	Text scoreDispText;
	GameObject shieldTxt;
	GameObject shieldDisp;
	Text shieldDispText;
	GameObject playerTxt;
	GameObject playerDisp;
	Text playerDispText;
	GameObject bombButton;
	GameObject bombTxt;
	GameObject bombDisp;
	Text bombDispText;
	GameObject starTxt;
	GameObject starDisp;
	Text starDispText;
	GameObject pauseButton;
	Text pauseDispText;
	//eh info ui
	GameObject ehBasef;
	GameObject ehBase;
	GameObject eh;
	CanvasRenderer cashCanvasRenderer_eh;
	RectTransform cashRectTransform_eh;
	//result display
	GameObject resultBasef;
	GameObject resultBase;
	GameObject resultDisp;
	Text resultDispText;
	//result display goto next stage button
	GameObject nextStageButton;


	//system local
	int intervalCnt;	//interval count

	//local
	//game info
	int scoreColorCnt;
	int shieldColorCnt;
	int playerColorCnt;
	int bombColorCnt;
	int starNumColorCnt;

	//game info display in/out move
	float u1_ymov;
	float u3_xmov;
	float bb_xmov;
	float bt_xmov;
	float bd_xmov;
	float st_xmov;
	float sd_xmov;

	//enemy hp in/out move
	float eh_xmov;

	//game info display mode
	int dpMode;

	//enemy hp display mode
	int ehMode;

	//enemy hp display level
	int ehLevel;

	//enemy hp taget
	float ehTarget;

	//enemy hp current
	float ehCurrent;

	//enemy hp battle start
	bool ehBattle;

	//main message display mode
	int mainMsgDispMode;
	int mainMsgDispMode_pauseBackup;

	//main message display string
	string mainMsgStr;
	int mainMsgStrDispCnt;
	int mainMsgStrDispIntCnt;
	string mainMsgStr_pauseBackup;
	int mainMsgStrDispCnt_pauseBackup;
	int mainMsgStrDispIntCnt_pauseBackup;
	int mainMsgStrFontSize_pauseBackup;

	//main message display cnt
	int mainMsgCnt;
	int mainMsgBlinkCnt;
	int mainMsgCnt_pauseBackup;
	int mainMsgBlinkCnt_pauseBackup;

	//result display
	bool resultDisplay;
	int resultDispSeq;
	int resultDispNextSeq;
	int resultDispSeqCnt;
	int resultDispFixCnt;
	int resultTotal;
	int crntTotal;
	int colorCnt;
	string[] lineCal;
	string[] lineRslt;
	string resultStr;
	bool resultSkip;

	//title description
	int dscIndex;
	bool dscDisplay;
	string dscStr;
	int dscStrDispCnt;
	int dscStrDispIntCnt;
	int dscArrowLeftColorCnt;
	int dscArrowRightColorCnt;
	bool titlelogoRotate;

	//ending display
	bool endingDisplay;
	int endingSeq;
	int endingTextCnt;
	int endingTextWait;
	//ending text info struct
	struct endingTextInfo{
		public int color;
		public string txt;
		public endingTextInfo(int c, string t){
			color = c;
			txt = t;
		}
	}
	//color,text
	endingTextInfo[] endingTextInfoTbl;

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
		//main info disp
		mainMessageDisp = GameObject.Find ("mainMessageDisp");
		mainMessageDispText = mainMessageDisp.GetComponent<Text> ();
		//sub menu disp
		continueButton = null;
		toTitleButton = null;
		//title disp
		verDisp = GameObject.Find ("verDisp");
		verDispText = verDisp.GetComponent<Text> ();
		//score disp
		scoreTxt = GameObject.Find ("scoreTxt");
		scoreDisp = GameObject.Find ("scoreDisp");
		scoreDispText = scoreDisp.GetComponent<Text> ();
		//shield disp
		shieldTxt = GameObject.Find ("shieldTxt");
		shieldDisp = GameObject.Find ("shieldDisp");
		shieldDispText = shieldDisp.GetComponent<Text> ();
		//player disp
		playerTxt = GameObject.Find ("playerTxt");
		playerDisp = GameObject.Find ("playerDisp");
		playerDispText = playerDisp.GetComponent<Text> ();
		//bomb button
		bombButton = GameObject.Find ("bombButton");
		//bomb disp
		bombTxt = GameObject.Find ("bombTxt");
		bombDisp = GameObject.Find ("bombDisp");
		bombDispText = bombDisp.GetComponent<Text> ();
		//star disp
		starTxt = GameObject.Find ("starTxt");
		starDisp = GameObject.Find ("starDisp");
		starDispText = starDisp.GetComponent<Text> ();
		//pause button
		pauseButton = GameObject.Find ("pauseButton");
		//enemy hp img
		ehBasef = GameObject.Find ("enemyHitPointBaseFrame");
		ehBase = GameObject.Find ("enemyHitPointBase");
		eh = GameObject.Find ("enemyHitPoint");
		cashCanvasRenderer_eh = eh.GetComponent<CanvasRenderer> ();
		cashRectTransform_eh = eh.GetComponent<RectTransform> ();

		//game info
		scoreColorCnt = 0;
		shieldColorCnt = 0;
		playerColorCnt = 0;
		bombColorCnt = 0;
		starNumColorCnt = 0;

		//display in/out move
		u1_ymov = 0.0f;	//up info
		u3_xmov = 0.0f;	//pause button
		bb_xmov = 0.0f; //bomb button
		bt_xmov = 0.0f;	//bomb text
		bd_xmov = 0.0f;	//bomb info
		st_xmov = 0.0f;	//star text
		sd_xmov = 0.0f;	//star info

		//game info display mode
		dpMode = dpModeNoDisp;

		//enemy hp display mode
		ehMode = ehModeNoDisp;

		//enemy hp display level
		ehLevel = 1;

		//enemy hp taget
		ehTarget = 0.0f;

		//enemy hp current
		ehCurrent = 0.0f;

		//enemy hp battle start
		ehBattle = false;

		//main message display mode
		 mainMsgDispMode = 0;

		//main message display string
		mainMsgStr = "";
		mainMsgStrDispCnt = 0;
		mainMsgStrDispIntCnt = 0;
		mainMsgStr_pauseBackup = "";
		mainMsgStrDispCnt_pauseBackup = 0;
		mainMsgStrDispIntCnt_pauseBackup = 0;
		mainMsgStrFontSize_pauseBackup = 0;

		//main message display cnt
		mainMsgCnt = 0;
		mainMsgBlinkCnt = 0;
		mainMsgCnt_pauseBackup = 0;
		mainMsgBlinkCnt_pauseBackup = 0;

		//result display
		resultDisplay = false;
		resultDispSeq = 0;
		resultDispNextSeq = 0;
		resultDispSeqCnt = 0;
		resultDispFixCnt = 0;
		resultTotal = 0;
		crntTotal = 0;
		colorCnt = 0;
		resultStr = "";
		resultSkip = false;

		//title description
		dscIndex = 0;
		dscDisplay = false;
		dscStr = "";
		dscStrDispCnt = 0;
		dscStrDispIntCnt = 0;
		dscArrowLeftColorCnt = 0;
		dscArrowRightColorCnt = 0;
		titlelogoRotate = false;

		//ending display
		endingDisplay = false;
		endingSeq = 0;
		endingTextCnt = 0;
		endingTextWait = 0;
		endingTextInfoTbl = new endingTextInfo[] {	//color,text
			new endingTextInfo (0, "GAME DESIGNER"),
			new endingTextInfo (0, "PROGRAMMER"),
			new endingTextInfo (0, "ALL SOUNDS COMPOSER"),
			new endingTextInfo (1, "KOICHI 'JON' NISHIYAMA"),
			new endingTextInfo (0, ""),
			new endingTextInfo (0, ""),
			new endingTextInfo (0, "SPECIAL ADVISER"),
			new endingTextInfo (1, "Inochin"),
			new endingTextInfo (1, "IPC-BOSS"),
			new endingTextInfo (1, "IPC-UME"),
			new endingTextInfo (1, "JUNKO"),
			new endingTextInfo (1, "Q-CHAN"),
			new endingTextInfo (1, "shirokuma"),
			new endingTextInfo (1, "YUKO.S"),
			new endingTextInfo (1, "Yutaka Otake"),
			new endingTextInfo (0, ""),
			new endingTextInfo (0, ""),
			new endingTextInfo (0, "GRAPHICS"),
			new endingTextInfo (1, "M.F.S.RECLUSE"),
			new endingTextInfo (0, ""),
			new endingTextInfo (0, ""),
			new endingTextInfo (0, "FONTS"),
			new endingTextInfo (1, "Heart To Me"),
			new endingTextInfo (1, "patrick h. lauke"),
			new endingTextInfo (0, ""),
			new endingTextInfo (0, ""),
			new endingTextInfo (0, "SPECIAL THANKS"),
			new endingTextInfo (1, "HARUMI"),
			new endingTextInfo (1, "HIDEKI ASAI"),
			new endingTextInfo (1, "KOKOCHAN"),
			new endingTextInfo (1, "(LOP EAR RABBIT)"),
			new endingTextInfo (1, "RIKA"),
			new endingTextInfo (1, "TSUGUMI"),
			new endingTextInfo (0, ""),
			new endingTextInfo (0, ""),
			new endingTextInfo (0, ""),
			new endingTextInfo (0, ""),
			new endingTextInfo (0, ""),
			new endingTextInfo (0, ""),
			new endingTextInfo (0, ""),
			new endingTextInfo (0, ""),
			new endingTextInfo (0, ""),
			new endingTextInfo (0, ""),
			new endingTextInfo (0, ""),
			new endingTextInfo (0, ""),
			new endingTextInfo (0, ""),
			new endingTextInfo (3, "WHAT WAS"),
			new endingTextInfo (3, "GALAXY UNKO?"),
			new endingTextInfo (3, "NOBODY KNOWS."),
			new endingTextInfo (3, "BUT,"),
			new endingTextInfo (3, "THANKS TO YOU"),
			new endingTextInfo (3, "THE EARTH"),
			new endingTextInfo (3, "HAS BEEN SAVED•••"),
			new endingTextInfo (0, ""),
			new endingTextInfo (0, ""),
			new endingTextInfo (0, ""),
			new endingTextInfo (0, ""),
			new endingTextInfo (0, ""),
			new endingTextInfo (0, ""),
			new endingTextInfo (0, ""),
			new endingTextInfo (0, ""),
			new endingTextInfo (0, ""),
			new endingTextInfo (0, ""),
			new endingTextInfo (0, ""),
			new endingTextInfo (0, ""),
			new endingTextInfo (0, ""),
			new endingTextInfo (2, "THANK YOU FOR PLAYING!!"),
		};
	}


	float cnt = 0.0f;	//time scale cnt
	// Update is called once per frame
	void Update () {
		////always process

		//main message process
		this.mainMessageDisplayProcess();

		//result dispaly
		this.resultDisplayProcess();

		//wait and pause
		cnt = cnt + Time.timeScale;
		if (cnt < 1.0f) {
			return;
		} else {
			cnt = cnt - 1.0f;
		}

		//game info display process
		this.infoDispProcess();

		//enemy hp display process
		this.ehpDispProcess();

		//title description display process
		this.dscDispProcess();

		//ending display process
		this.endingDispProcess();

		////interval process

		//interval count
		intervalCnt++;
		if (intervalCnt >= 2) {
			intervalCnt = 0;
			//nop
		}
	}


	//private

	//game info disp process
	private void infoDispProcess(){
		//game info color process
		if (scoreColorCnt > 0) {
			scoreColorCnt--;
			if (scoreColorCnt <= 0) {
				scoreColorCnt = 0;
				scoreDispText.color = new Color (90.0f / 255.0f, 200.0f / 255.0f, 90.0f / 255.0f, 220.0f / 255.0f);
			}
		}
		if (shieldColorCnt > 0) {
			shieldColorCnt--;
			if (shieldColorCnt <= 0) {
				shieldColorCnt = 0;
				shieldDispText.color = new Color (90.0f / 255.0f, 200.0f / 255.0f, 90.0f / 255.0f, 220.0f / 255.0f);
			}
		}
		if (playerColorCnt > 0) {
			playerColorCnt--;
			if (playerColorCnt <= 0) {
				playerColorCnt = 0;
				playerDispText.color = new Color (90.0f / 255.0f, 200.0f / 255.0f, 90.0f / 255.0f, 220.0f / 255.0f);
			}
		}
		if (bombColorCnt > 0) {
			bombColorCnt--;
			if (bombColorCnt <= 0) {
				bombColorCnt = 0;
				bombDispText.color = new Color (90.0f / 255.0f, 200.0f / 255.0f, 90.0f / 255.0f, 220.0f / 255.0f);
			}
		}
		if (starNumColorCnt > 0) {
			starNumColorCnt--;
			if (starNumColorCnt <= 0) {
				starNumColorCnt = 0;
				starDispText.color = new Color (90.0f / 255.0f, 200.0f / 255.0f, 90.0f / 255.0f, 220.0f / 255.0f);
			}
		}
		//game info disp in/out process
		if( dpMode == dpModeDispIn ){
			//up info
			if (u1_ymov <= 0) {
				u1_ymov = 0;
			}else{
				u1_ymov = u1_ymov - (move_base);	//(offset50.0/mov1.0)-> 50times mov
			}
			//pause button info
			if (u3_xmov <= 0) {
				u3_xmov = 0;
			} else {
				u3_xmov = u3_xmov - (move_base) * (pauseb_offset / disp_uoffset);
			}
			//bomb info
			if (bb_xmov >= 0) {
				bb_xmov = 0;
			}else{
				bb_xmov = bb_xmov + (move_base * -1.0f) * (disp_bomboffset / disp_uoffset);
			}
			if (bt_xmov >= 0) {
				bt_xmov = 0;
			}else{
				bt_xmov = bt_xmov + (move_base * -1.0f) * (disp_bombtxtoffset / disp_uoffset);
			}
			if (bd_xmov >= 0) {
				bd_xmov = 0;
			}else{
				bd_xmov = bd_xmov + (move_base * -1.0f) * (disp_bombdispoffset / disp_uoffset);
			}
			//star info
			if (st_xmov <= 0) {
				st_xmov = 0;
			}else{
				st_xmov = st_xmov + (move_base * -1.0f) * (disp_startxtoffset / disp_uoffset);
			}
			if (sd_xmov <= 0) {
				sd_xmov = 0;
			}else{
				sd_xmov = sd_xmov + (move_base * -1.0f) * (disp_stardispoffset / disp_uoffset);
			}
			this.infoDisp ();
			//game info display in term?
			if ((u1_ymov == 0) && (bb_xmov == 0) && (bt_xmov == 0) && (bd_xmov == 0) && (st_xmov == 0) && (sd_xmov == 0) && (u3_xmov == 0)) {
				dpMode = dpModeDispOn;
			}
		}
	}

	//game info disp
	private void infoDisp(){
		//up1 info
		scoreTxt.GetComponent<RectTransform> ().localPosition = new Vector3(scoretxt_x, disp_u1ybase+u1_ymov, 0);
		scoreDisp.GetComponent<RectTransform> ().localPosition = new Vector3(scoredisp_x, disp_u2ybase+u1_ymov, 0);
		shieldTxt.GetComponent<RectTransform> ().localPosition = new Vector3(shieldtxt_x, disp_u1ybase+u1_ymov, 0);
		shieldDisp.GetComponent<RectTransform> ().localPosition = new Vector3(shielddisp_x, disp_u2ybase+u1_ymov, 0);
		playerTxt.GetComponent<RectTransform> ().localPosition = new Vector3(playertxt_x, disp_u1ybase+u1_ymov, 0);
		playerDisp.GetComponent<RectTransform> ().localPosition = new Vector3(playerdisp_x, disp_u2ybase+u1_ymov, 0);
		//up3 info
		pauseButton.GetComponent<RectTransform>().localPosition = new Vector3(pauseb_xbase+u3_xmov, pauseb_y, 0);
		//bomb info
		bombButton.GetComponent<RectTransform>().localPosition = new Vector3(bombb_xbase+bb_xmov, bombb_y, 0);
		bombTxt.GetComponent<RectTransform>().localPosition = new Vector3(bombtxt_xbase+bt_xmov, bombtxt_y, 0);
		bombDisp.GetComponent<RectTransform>().localPosition = new Vector3(bombdisp_xbase+bd_xmov, bombdisp_y, 0);
		//star info
		starTxt.GetComponent<RectTransform>().localPosition = new Vector3(startxt_xbase+st_xmov, startxt_y, 0);
		starDisp.GetComponent<RectTransform>().localPosition = new Vector3(stardisp_xbase+sd_xmov, stardisp_y, 0);
	}

	//enemy hp disp process
	private void ehpDispProcess(){
		const float ehvalinc = 0.015f;
		const float ehvaldec = 0.006f;
		if (ehMode == ehModeDispIn) {
			//pause button info
			if (eh_xmov <= 0) {
				eh_xmov = 0;
			} else {
				eh_xmov = eh_xmov - (move_base) * (eh_offset / disp_uoffset);
			}
			this.ehpDisp ();
			//enemy hp in term?
			if (eh_xmov == 0) {
				ehMode = ehModeDispOn;
				ehBattle = false;
				ehTarget = 1.0f;
			}
		} else if(ehMode == ehModeDispOn){
			if (ehTarget != ehCurrent) {
				if( ehTarget > ehCurrent){
					ehCurrent = ehCurrent + ehvalinc;
					if (ehCurrent > ehTarget) {
						ehCurrent = ehTarget;
					}
				}
				if( ehTarget < ehCurrent){
					ehCurrent = ehCurrent - ehvaldec;
					if (ehCurrent < ehTarget) {
						ehCurrent = ehTarget;
					}
				}
				//color
				if ( (ehCurrent <= 0.25) && (ehBattle == true) ) {
					cashCanvasRenderer_eh.SetColor( new Color (220.0f / 255.0f, 90.0f / 255.0f, 90.0f / 255.0f) );
				} else {
					cashCanvasRenderer_eh.SetColor( new Color (220.0f / 255.0f, 220.0f / 255.0f, 90.0f / 255.0f) );
				}
				//scale
				if (ehLevel == 0) {
					cashRectTransform_eh.localScale = new Vector3 (1.0f, (ehCurrent/2.0f), 1.0f);
				} else if (ehLevel == 1) {
					cashRectTransform_eh.localScale = new Vector3 (1.0f, (ehCurrent), 1.0f);
				}
			}
		}
	}

	//enemy hp disp
	private void ehpDisp(){
		float yoffsetb = 0;
		float yoffset = 0;
		if (ehLevel == 0) {
			yoffsetb = eh_yoffset;
			yoffset = eh_yoffset-2;
		} else if (ehLevel == 1) {
			yoffsetb = 0;
			yoffset = 0;
		}
		//enemy hp
		ehBasef.GetComponent<RectTransform>().localPosition = new Vector3(eh_xbase+eh_xmov, (ehbf_y+yoffsetb), 0);
		ehBase.GetComponent<RectTransform>().localPosition = new Vector3(eh_xbase+eh_xmov, (ehb_y+yoffset), 0);
		cashRectTransform_eh.localPosition = new Vector3(eh_xbase+eh_xmov, (eh_y+yoffset), 0);
	}

	//main message display process
	private void mainMessageDisplayProcess(){
		if (mainMsgCnt > 0) {	//main message display?
			if (mainMsgStrDispCnt <= mainMsgStr.Length) {
				//string slide
				string st = "";
				mainMsgStrDispIntCnt++;
				if (mainMsgStrDispIntCnt >= 2) {
					mainMsgStrDispIntCnt = 0;
					st = mainMsgStr.Substring (0, mainMsgStrDispCnt);
					if (mainMsgStrDispCnt <= (mainMsgStr.Length-1)) {
						st = st + "_";
					}
					for (int i = 0; i <= ((mainMsgStr.Length)-mainMsgStrDispCnt-2); i++) {
						st = st + " ";
					}
					mainMessageDispText.text = st;
					mainMsgStrDispCnt++;
					//play se
					mc.playSound (mc.se_resultcnt);
				}
			} else {
				//string
				mainMessageDispText.text = mainMsgStr;
				//color(blink)
				Color cr = mainMessageDispText.color;
				if (((mainMsgBlinkCnt / 10) % 2) == 0) {
					cr.a = 210.0f / 255.0f;
				} else {
					cr.a = 0.0f / 255.0f;
				}
				mainMessageDispText.color = cr;
				mainMsgBlinkCnt++;
				//disp mode
				if (mainMsgDispMode == mdModeTimeDisp) {
					//message time
					mainMsgCnt--;
					if (mainMsgCnt <= 0) {
						mainMessageDispText.text = "";
						mainMsgCnt = 0;
						mainMsgCnt_pauseBackup = 0;
					}
				}
			}
		}
	}

	//result display process
	private void resultDisplayProcess(){
		if (resultDisplay == true) {
			const int resultWait = 20;
			const int fixWait = 6;
			int spNum = 0;
			int rslt = 0;
			string tmpCal;
			string tmpRslt;
			//pause?
			if ((mc.getPause () == true) && (resultDispSeq != 11)) {
				return;
			}
			//generate line seq
			switch (resultDispSeq) {
			case 0:
				//base scale up
				Vector3 scl = resultBasef.transform.localScale;
				scl.x = scl.x + 0.05f;
				scl.y = scl.y + 0.05f;
				if ( (scl.x >= 1.0f) || (scl.y >= 1.0f) ) {
					scl.x = 1.0f;
					scl.y = 1.0f;
					resultDispSeqCnt = 0;
					resultDispNextSeq = 2;
					resultDispSeq = 1;
				}
				resultBasef.transform.localScale = scl;
				break;
			case 1:
				//line wait (line common)
				resultDispSeqCnt++;
				if( (resultDispSeqCnt > resultWait) || (resultSkip == true) ){
					resultSkip = false;
					//next seq
					resultDispSeqCnt = 0;
					resultDispSeq = resultDispNextSeq;
				}
				break;
			case 2:
				//disp get star in stage
				//line cal
				int star = mc.getStarNumStage ();
				tmpCal = "\n\nSTAR*" + star.ToString ("D");
				spNum = 4 - (star.ToString ("D").Length);
				for (int i = 0; i < spNum; i++) {
					tmpCal = tmpCal + " ";
				}
				tmpCal = tmpCal + "*1000 =";
				//line rslt
				rslt = star * 1000;
				spNum = 7 - (rslt.ToString ("D").Length);
				tmpRslt = "";
				for (int i = 0; i < spNum; i++) {
					tmpRslt = tmpRslt + " ";
				}
				tmpRslt = tmpRslt + "<color=\"#f8f88a\">" + rslt.ToString ("D") + "</color>";
				//set line cal+rslt
				lineCal [1] = tmpCal;
				lineRslt [1] = tmpRslt;
				//add toal
				resultTotal = resultTotal + rslt;
				//line wait and next line
				resultDispSeqCnt = 0;
				resultDispNextSeq = 3;
				resultDispSeq = 1;
				//play se(menu fix)
				mc.playSound( mc.se_ts100 );
				//base color change cnt
				resultDispFixCnt = fixWait;
				break;
			case 3:
				//disp player
				//line cal
				int pl = mc.getPlayerNum () - 1;
				if (pl < 0) {
					pl = 0;
				}
				tmpCal = "\n\nPLAYER*" + pl.ToString ("D") + " *10000 =";
				//line rslt
				rslt = pl * 10000;
				spNum = 6 - (rslt.ToString ("D").Length);
				tmpRslt = "";
				for (int i = 0; i < spNum; i++) {
					tmpRslt = tmpRslt + " ";
				}
				tmpRslt = tmpRslt + "<color=\"#f8f88a\">" + rslt.ToString ("D") + "</color>";
				//set line cal+rslt
				lineCal [2] = tmpCal;
				lineRslt [2] = tmpRslt;
				//add toal
				resultTotal = resultTotal + rslt;
				//line wait and next line
				resultDispSeqCnt = 0;
				resultDispNextSeq = 4;
				resultDispSeq = 1;
				//play se(menu fix)
				mc.playSound( mc.se_ts100 );
				resultDispFixCnt = fixWait;
				break;
			case 4:
				//disp shield
				//line cal
				int sd = mc.getPlayerHp () - 1;
				if (sd < 0) {
					sd = 0;
				}
				tmpCal = "\n\nSHIELD*" + sd.ToString ("D") + " *5000 =";
				//line rslt
				rslt = sd * 5000;
				spNum = 7 - (rslt.ToString ("D").Length);
				tmpRslt = "";
				for (int i = 0; i < spNum; i++) {
					tmpRslt = tmpRslt + " ";
				}
				tmpRslt = tmpRslt + "<color=\"#f8f88a\">" + rslt.ToString ("D") + "</color>";
				//set line cal+rslt
				lineCal [3] = tmpCal;
				lineRslt [3] = tmpRslt;
				//add toal
				resultTotal = resultTotal + rslt;
				//line wait and next line
				resultDispSeqCnt = 0;
				resultDispNextSeq = 5;
				resultDispSeq = 1;
				//play se(menu fix)
				mc.playSound( mc.se_ts100 );
				resultDispFixCnt = fixWait;
				break;
			case 5:
				//disp bomb
				//line cal
				int bm = mc.getPlayerBombNum ();
				tmpCal = "\n\nBOMB*" + bm.ToString ("D") + " ";
				if (bm < 10) {
					tmpCal = tmpCal + " ";
				}
				tmpCal = tmpCal + "*3000 =";
				//line rslt
				rslt = bm * 3000;
				spNum = 8 - (rslt.ToString ("D").Length);
				tmpRslt = "";
				for (int i = 0; i < spNum; i++) {
					tmpRslt = tmpRslt + " ";
				}
				tmpRslt = tmpRslt + "<color=\"#f8f88a\">" + rslt.ToString ("D") + "</color>";
				//set line cal+rslt
				lineCal [4] = tmpCal;
				lineRslt [4] = tmpRslt;
				//add toal
				resultTotal = resultTotal + rslt;
				//line wait and next line
				resultDispSeqCnt = 0;
				resultDispNextSeq = 6;
				resultDispSeq = 1;
				//play se(menu fix)
				mc.playSound( mc.se_ts100 );
				resultDispFixCnt = fixWait;
				break;
			case 6:
				//no continue
				//line rslt
				if (mc.getUseContinue() == false) {
					rslt = 30000;
				} else {
					rslt = 0;
				}
				spNum = 10 - (rslt.ToString ("D").Length);
				tmpRslt = "";
				for (int i = 0; i < spNum; i++) {
					tmpRslt = tmpRslt + " ";
				}
				tmpRslt = tmpRslt + "<color=\"#f8f88a\">" + rslt.ToString ("D") + "</color>";
				//set line rslt
				lineRslt [5] = tmpRslt;
				//add toal
				resultTotal = resultTotal + rslt;
				//line wait and next line
				resultDispSeqCnt = 0;
				resultDispNextSeq = 7;
				resultDispSeq = 1;
				//play se(menu fix)
				mc.playSound( mc.se_ts100 );
				resultDispFixCnt = fixWait;
				break;
			case 7:
				//no miss
				//line rslt
				if (mc.getLostPlayer() == false) {
					rslt = 80000;
				} else {
					rslt = 0;
				}
				spNum = 14 - (rslt.ToString ("D").Length);
				tmpRslt = "";
				for (int i = 0; i < spNum; i++) {
					tmpRslt = tmpRslt + " ";
				}
				tmpRslt = tmpRslt + "<color=\"#f8f88a\">" + rslt.ToString ("D") + "</color>";
				//set line rslt
				lineRslt [6] = tmpRslt;
				//add toal
				resultTotal = resultTotal + rslt;
				//line wait and next line
				resultDispSeqCnt = 0;
				resultDispNextSeq = 8;
				resultDispSeq = 1;
				//play se(menu fix)
				mc.playSound( mc.se_ts100 );
				resultDispFixCnt = fixWait;
				break;
			case 8:
				//no damage
				//line rslt
				if (mc.getHaveDamage() == false) {
					rslt = 300000;
				} else {
					rslt = 0;
				}
				spNum = 12 - (rslt.ToString ("D").Length);
				tmpRslt = "";
				for (int i = 0; i < spNum; i++) {
					tmpRslt = tmpRslt + " ";
				}
				tmpRslt = tmpRslt + "<color=\"#f8f88a\">" + rslt.ToString ("D") + "</color>";
				//set line rslt
				lineRslt [7] = tmpRslt;
				//add toal
				resultTotal = resultTotal + rslt;
				//line wait and next line
				resultDispSeqCnt = 0;
				resultDispNextSeq = 9;
				resultDispSeq = 1;
				//play se(menu fix)
				mc.playSound( mc.se_ts100 );
				resultDispFixCnt = fixWait;
				break;
			case 9:
				//bomb no use
				//line rslt
				if (mc.getUseBomb() == false) {
					rslt = 150000;
				} else {
					rslt = 0;
				}
				spNum = 10 - (rslt.ToString ("D").Length);
				tmpRslt = "";
				for (int i = 0; i < spNum; i++) {
					tmpRslt = tmpRslt + " ";
				}
				tmpRslt = tmpRslt + "<color=\"#f8f88a\">" + rslt.ToString ("D") + "</color>";
				//set line rslt
				lineRslt [8] = tmpRslt;
				//add toal
				resultTotal = resultTotal + rslt;
				//line wait and next line
				resultDispSeqCnt = 0;
				resultDispNextSeq = 10;
				resultDispSeq = 1;
				//play se(menu fix)
				mc.playSound( mc.se_ts100 );
				resultDispFixCnt = fixWait;
				break;
			case 10:
				//total
				//current total add
				if ( ((resultTotal - crntTotal) < 5000) || (resultSkip == true) ) {
					resultSkip = false;
					crntTotal = resultTotal;
					//add score
					mc.addGameScore( resultTotal );
					mc.setScoreColor ();	//white
					mc.dispSubMessage( 1.8f, 4.0f, -1.2f, -1.0f, ("+"+resultTotal.ToString("D")), 1 );	//white
					//next seq
					resultDispSeqCnt = 0;
					resultDispNextSeq = 11;
					resultDispSeq = 11;
					//play se(menu fix)
					mc.playSound (mc.se_ts110);
					resultDispFixCnt = fixWait;
					//generate next stage button
					this.generateNextStageButton();
				} else {
					crntTotal = crntTotal + 5000;
					//play se(total count)
					mc.playSound (mc.se_resultcnt);
				}
				//line rslt
				spNum = 8 - (crntTotal.ToString ("D").Length);
				tmpRslt = "<color=\"#ffffff\">";
				for (int i = 0; i < spNum; i++) {
					tmpRslt = tmpRslt + " ";
				}
				tmpRslt = tmpRslt + crntTotal.ToString("D");
				tmpRslt = tmpRslt + "</color>";
				//set line rslt
				lineRslt [9] = tmpRslt;
				break;
			case 11:
				//total color change + wait
				//line rslt
				spNum = 8 - (resultTotal.ToString ("D").Length);
				tmpRslt = "";
				if (colorCnt == 0) {
					tmpRslt = "<color=\"#ffffff\">";
					colorCnt++;
				} else if (colorCnt == 1) {
					tmpRslt = "<color=\"#ffff28\">";
					colorCnt++;
				} else if (colorCnt == 2) {
					tmpRslt = "<color=\"#ff2828\">";
					colorCnt = 0;
				}
				for (int i = 0; i < spNum; i++) {
					tmpRslt = tmpRslt + " ";
				}
				tmpRslt = tmpRslt + resultTotal.ToString("D");
				tmpRslt = tmpRslt + "</color>";
				//set line rslt
				lineRslt [9] = tmpRslt;
				break;
			default:
				break;
			}
			//fix base color change
			if (mc.getPause () == false) {
				if (resultDispFixCnt > 0) {
					resultDispFixCnt--;
					if (resultDispFixCnt <= 0) {
						resultBase.GetComponent<Image> ().color = new Color (90.0f / 255.0f, 200.0f / 255.0f, 90.0f / 255.0f, 60.0f / 255.0f);
					} else {
						resultBase.GetComponent<Image> ().color = new Color (200.0f / 255.0f, 200.0f / 255.0f, 200.0f / 255.0f, 160.0f / 255.0f);
					}
				}
			}
			//generate all text
			resultStr = "";
			for (int i = 0; i < lineCal.Length; i++) {
				resultStr = resultStr + lineCal [i] + lineRslt [i];
			}
			resultDispText.text = resultStr;
		}
	}

	//title description display process
	private void dscDispProcess(){
		if (dscDisplay == true) {	//description display?
			if (dscStrDispCnt <= dscStr.Length) {
				//string slide
				string st = "";
				dscStrDispIntCnt++;
				if (dscStrDispIntCnt >= 1) {
					dscStrDispIntCnt = 0;
					st = dscStr.Substring (0, dscStrDispCnt);
					if (dscStrDispCnt <= (dscStr.Length-1)) {
						st = st + "_";
					}
					for (int i = 0; i <= ((dscStr.Length)-dscStrDispCnt-2); i++) {
						st = st + " ";
					}
					descriptionDispText.text = st;
					dscStrDispCnt = dscStrDispCnt + 4;
					if (dscStrDispCnt >= dscStr.Length) {
						dscStrDispCnt = dscStr.Length+1;
					}
					//play se
					mc.playSound (mc.se_resultcnt);
				}
			} else {
				//string
				descriptionDispText.text = dscStr;
				dscDisplay = false;
			}
		}
		//arrow left color change
		if (dscArrowLeftColorCnt > 0) {
			dscArrowLeftColorCnt--;
			if (dscArrowLeftColorCnt <= 0) {
				dscArrowLeftColorCnt = 0;
				descriptionArrowLeftDispText.color = new Color (200.0f / 255.0f, 200.0f / 255.0f, 200.0f / 255.0f, 255.0f / 255.0f);
			} else {
				descriptionArrowLeftDispText.color = new Color (248.0f / 255.0f, 138.0f / 255.0f, 138.0f / 255.0f, 255.0f / 255.0f);
			}
		}
		//arrow right color change
		if (dscArrowRightColorCnt > 0) {
			dscArrowRightColorCnt--;
			if (dscArrowRightColorCnt <= 0) {
				dscArrowRightColorCnt = 0;
				descriptionArrowRightDispText.color = new Color (200.0f / 255.0f, 200.0f / 255.0f, 200.0f / 255.0f, 255.0f / 255.0f);
			} else {
				descriptionArrowRightDispText.color = new Color (248.0f / 255.0f, 138.0f / 255.0f, 138.0f / 255.0f, 255.0f / 255.0f);
			}
		}
	}

	//ending display process
	private void endingDispProcess(){
		if (endingDisplay == true) {
			switch (endingSeq){
			case 0:
				if (endingTextWait <= 0) {
					int col = endingTextInfoTbl [endingTextCnt].color;
					string txt = endingTextInfoTbl [endingTextCnt].txt;

					GameObject parent = GameObject.Find ("Canvas");
					GameObject endingTextDisp = Instantiate (endingTextDispPrefab) as GameObject;
					endingTextDisp.transform.SetParent (parent.transform, false);
					bool last = ((endingTextCnt == (endingTextInfoTbl.Length - 1)) ? true : false);
					endingTextDisp.GetComponent<endingTextDispController> ().setInitStatus (0, -200, +100, 0, 1.1f, txt, col, last);

					endingTextCnt++;
					if (endingTextCnt >= endingTextInfoTbl.Length) {
						endingTextWait = 350;
						endingSeq = 1;
					} else {
						endingTextWait = 20;
					}
				} else {
					endingTextWait = endingTextWait - 1;
				}
				break;
			case 1:
				endingTextWait--;
				if (endingTextWait <= 0) {
					mc.fadeoutBgm ();
					mc.playSound (mc.vo280);
					endingTextWait = 150;
					endingSeq = 2;
				}
				break;
			case 2:
				endingTextWait--;
				if (endingTextWait <= 0) {
					endingDisplay = false;
					endingSeq = 0;
					endingTextCnt = 0;
					endingTextWait = 0;
					mc.setEnding ();
					this.subMenuDisplayOn (true);
				}
				break;
			default:
				break;
			}
		}
	}

	//public
	//generate title screen
	public void generateTitleScreen(){
		const int objTxt = 0x00;
		const int objImg = 0x01;
		this.clearVerDisplay ();
		GameObject parent = GameObject.Find ("Canvas");
		EventTrigger trigger;
		EventTrigger.Entry entry;

		//title parent
		titleParent = Instantiate (titleParentPrefab) as GameObject;
		titleParent.transform.SetParent (parent.transform, false);
		titleParent.GetComponent<titleDispController> ().setInitState ( 255.0f, objTxt );

		//title logo parent
		titleDispParent = GameObject.Find("titleDispParentPrefab");
		titleDispParent.GetComponent<titleDispController> ().setInitState ( 0.0f, objImg, ((titlelogoRotate==true)?false:true) );	//first only rotate
		titleDispParentImage = titleDispParent.GetComponent<Image> ();
		titleDispParentImage.color = new Color (0.0f/255.0f, 0.0f/255.0f, 0.0f/255.0f, 0.0f/255.0f);
		//title italic
		titleDisp = GameObject.Find("titleDispPrefab");
		titleDispText = titleDisp.GetComponent<Text> ();
		titleDispText.text = "GALAXY \n UNKO 4 ";
		titleDispText.fontStyle = FontStyle.Italic;
		titleDispText.color = new Color (90.0f/255.0f, 200.0f/255.0f, 90.0f/255.0f, 0.0f/255.0f);
		//title bold italic
		titleDispB = GameObject.Find("titleDispBPrefab");
		titleDispB.GetComponent<titleDispController> ().setInitState ( 110.0f, objTxt );
		titleDispBText = titleDispB.GetComponent<Text> ();
		titleDispBText.text = "GALAXY \n UNKO 4 ";
		titleDispBText.fontStyle = FontStyle.BoldAndItalic;
		titleDispBText.color = new Color (90.0f/255.0f, 200.0f/255.0f, 90.0f/255.0f, 0.0f/255.0f);
		titleDispB.tag = "titleB";

		//game level
		gamelevelDisp = GameObject.Find("gamelevelDispPrefab");
		gamelevelDispText = gamelevelDisp.GetComponent<Text> ();
		gamelevelDispText.text = "GAME LEVEL";
		gamelevelDispText.color = new Color (90.0f/255.0f, 200.0f/255.0f, 90.0f/255.0f, 0.0f/255.0f);
		//easy
		glEasy = GameObject.Find("glEasyPrefab");
		glEasyText = glEasy.GetComponent<Text> ();
		glEasyText.text = "EASY";
		glEasyText.color = new Color (90.0f/255.0f, 200.0f/255.0f, 90.0f/255.0f, 0.0f/255.0f);
		trigger = glEasy.GetComponent<EventTrigger> ();
		entry = new EventTrigger.Entry ();
		entry.eventID = EventTriggerType.PointerDown;
		entry.callback.AddListener ( this.selectGlEasy );
		trigger.triggers.Add(entry);
		//nomal
		glNormal = GameObject.Find("glNormalPrefab");
		glNormalText = glNormal.GetComponent<Text> ();
		glNormalText.text = "NORMAL";
		glNormalText.color = new Color (90.0f/255.0f, 200.0f/255.0f, 90.0f/255.0f, 0.0f/255.0f);
		trigger = glNormal.GetComponent<EventTrigger> ();
		entry = new EventTrigger.Entry ();
		entry.eventID = EventTriggerType.PointerDown;
		entry.callback.AddListener ( this.selectGlNormal );
		trigger.triggers.Add(entry);
		//hard
		glHard = GameObject.Find("glHardPrefab");
		glHardText = glHard.GetComponent<Text> ();
		glHardText.text = "HARD";
		glHardText.color = new Color (90.0f/255.0f, 200.0f/255.0f, 90.0f/255.0f, 0.0f/255.0f);
		trigger = glHard.GetComponent<EventTrigger> ();
		entry = new EventTrigger.Entry ();
		entry.eventID = EventTriggerType.PointerDown;
		entry.callback.AddListener ( this.selectGlHard );
		trigger.triggers.Add(entry);

		//player speed
		playerspeedDisp = GameObject.Find("playerspeedDispPrefab");
		playerspeedDispText = playerspeedDisp.GetComponent<Text> ();
		playerspeedDispText.text = "PLAYER SPEED";
		playerspeedDispText.color = new Color (90.0f/255.0f, 200.0f/255.0f, 90.0f/255.0f, 0.0f/255.0f);
		//slow
		psSlow = GameObject.Find("psSlowPrefab");
		psSlowText = psSlow.GetComponent<Text> ();
		psSlowText.text = "SLOW";
		psSlowText.color = new Color (90.0f/255.0f, 200.0f/255.0f, 90.0f/255.0f, 0.0f/255.0f);
		trigger = psSlow.GetComponent<EventTrigger> ();
		entry = new EventTrigger.Entry ();
		entry.eventID = EventTriggerType.PointerDown;
		entry.callback.AddListener ( this.selectPsSlow );
		trigger.triggers.Add(entry);
		//normal
		psNormal = GameObject.Find("psNormalPrefab");
		psNormalText = psNormal.GetComponent<Text> ();
		psNormalText.text = "NORMAL";
		psNormalText.color = new Color (90.0f/255.0f, 200.0f/255.0f, 90.0f/255.0f, 0.0f/255.0f);
		trigger = psNormal.GetComponent<EventTrigger> ();
		entry = new EventTrigger.Entry ();
		entry.eventID = EventTriggerType.PointerDown;
		entry.callback.AddListener ( this.selectPsNormal );
		trigger.triggers.Add(entry);
		//fast
		psFast = GameObject.Find("psFastPrefab");
		psFastText = psFast.GetComponent<Text> ();
		psFastText.text = "FAST";
		psFastText.color = new Color (90.0f/255.0f, 200.0f/255.0f, 90.0f/255.0f, 0.0f/255.0f);
		trigger = psFast.GetComponent<EventTrigger> ();
		entry = new EventTrigger.Entry ();
		entry.eventID = EventTriggerType.PointerDown;
		entry.callback.AddListener ( this.selectPsFast );
		trigger.triggers.Add(entry);

		//sub weapon
		subWeaponDisp = GameObject.Find("subWeaponDispPrefab");
		subWeaponDispText = subWeaponDisp.GetComponent<Text> ();
		subWeaponDispText.text = "SUBWEAPON";
		subWeaponDispText.color = new Color (90.0f/255.0f, 200.0f/255.0f, 90.0f/255.0f, 0.0f/255.0f);
		//laser
		swLaser = GameObject.Find("swLaserPrefab");
		swLaserText = swLaser.GetComponent<Text> ();
		swLaserText.text = "LASER";
		swLaserText.color = new Color (90.0f/255.0f, 200.0f/255.0f, 90.0f/255.0f, 0.0f/255.0f);
		trigger = swLaser.GetComponent<EventTrigger> ();
		entry = new EventTrigger.Entry ();
		entry.callback.AddListener ( this.selectSwLaser );
		entry.eventID = EventTriggerType.PointerDown;
		trigger.triggers.Add(entry);
		//missile
		swMissile = GameObject.Find("swMissilePrefab");
		swMissileText = swMissile.GetComponent<Text> ();
		swMissileText.text = "MISSILE";
		swMissileText.color = new Color (90.0f/255.0f, 200.0f/255.0f, 90.0f/255.0f, 0.0f/255.0f);
		trigger = swMissile.GetComponent<EventTrigger> ();
		entry = new EventTrigger.Entry ();
		entry.callback.AddListener ( this.selectSwMissile );
		entry.eventID = EventTriggerType.PointerDown;
		trigger.triggers.Add(entry);

		//player type
		playerTypeDisp = GameObject.Find("playerTypeDispPrefab");
		playerTypeDispText = playerTypeDisp.GetComponent<Text> ();
		playerTypeDispText.text = "PLAYER TYPE";
		playerTypeDispText.color = new Color (90.0f/255.0f, 200.0f/255.0f, 90.0f/255.0f, 0.0f/255.0f);

		//player typeA frame
		ptADispf = GameObject.Find("ptADispfPrefab");
		ptADispf.GetComponent<titleDispController> ().setInitState ( 160.0f, objImg );
		ptADispfImage = ptADispf.GetComponent<Image> ();
		ptADispfImage.color = new Color (110.0f/255.0f, 120.0f/255.0f, 110.0f/255.0f, 0.0f/255.0f);
		trigger = ptADispf.GetComponent<EventTrigger> ();
		entry = new EventTrigger.Entry ();
		entry.eventID = EventTriggerType.PointerDown;
		entry.callback.AddListener ( this.selectPtTypaA );
		trigger.triggers.Add(entry);
		//player typeA image
		ptADisp = GameObject.Find("ptADispPrefab");
		ptADisp.GetComponent<titleDispController> ().setInitState ( (mc.playerType==mc.playerTypeA?255.0f:130.0f), objImg );
		ptADispImage = ptADisp.GetComponent<Image> ();
		ptADispImage.color = new Color (130.0f/255.0f, 130.0f/255.0f, 130.0f/255.0f, 0.0f/255.0f);
		//player typeA text
		ptANameDisp = GameObject.Find("ptANameDispPrefab");
		ptANameDispText = ptANameDisp.GetComponent<Text> ();
		ptANameDispText.color = new Color (200.0f/255.0f, 200.0f/255.0f, 200.0f/255.0f, 0.0f/255.0f);

		//player typeB frame
		ptBDispf = GameObject.Find("ptBDispfPrefab");
		ptBDispf.GetComponent<titleDispController> ().setInitState ( 160.0f, objImg );
		ptBDispfImage = ptBDispf.GetComponent<Image> ();
		ptBDispfImage.color = new Color (110.0f/255.0f, 120.0f/255.0f, 110.0f/255.0f, 0.0f/255.0f);
		trigger = ptBDispf.GetComponent<EventTrigger> ();
		entry = new EventTrigger.Entry ();
		entry.eventID = EventTriggerType.PointerDown;
		entry.callback.AddListener ( this.selectPtTypaB );
		trigger.triggers.Add(entry);
		//player typeB image
		ptBDisp = GameObject.Find("ptBDispPrefab");
		ptBDisp.GetComponent<titleDispController> ().setInitState ( (mc.playerType==mc.playerTypeB?255.0f:130.0f), objImg );
		ptBDispImage = ptBDisp.GetComponent<Image> ();
		ptBDispImage.color = new Color (130.0f/255.0f, 130.0f/255.0f, 130.0f/255.0f, 0.0f/255.0f);
		//player typeB text
		ptBNameDisp = GameObject.Find("ptBNameDispPrefab");
		ptBNameDispText = ptBNameDisp.GetComponent<Text> ();
		ptBNameDispText.color = new Color (200.0f/255.0f, 200.0f/255.0f, 200.0f/255.0f, 0.0f/255.0f);

		//player typeC frame
		ptCDispf = GameObject.Find("ptCDispfPrefab");
		ptCDispf.GetComponent<titleDispController> ().setInitState ( 160.0f, objImg );
		ptCDispfImage = ptCDispf.GetComponent<Image> ();
		ptCDispfImage.color = new Color (110.0f/255.0f, 120.0f/255.0f, 110.0f/255.0f, 0.0f/255.0f);
		trigger = ptCDispf.GetComponent<EventTrigger> ();
		entry = new EventTrigger.Entry ();
		entry.eventID = EventTriggerType.PointerDown;
		entry.callback.AddListener ( this.selectPtTypaC );
		trigger.triggers.Add(entry);
		//player typeC image
		ptCDisp = GameObject.Find("ptCDispPrefab");
		ptCDisp.GetComponent<titleDispController> ().setInitState ( (mc.playerType==mc.playerTypeC?255.0f:130.0f), objImg );
		ptCDispImage = ptCDisp.GetComponent<Image> ();
		ptCDispImage.color = new Color (130.0f/255.0f, 130.0f/255.0f, 130.0f/255.0f, 0.0f/255.0f);
		//player typeC text
		ptCNameDisp = GameObject.Find("ptCNameDispPrefab");
		ptCNameDispText = ptCNameDisp.GetComponent<Text> ();
		ptCNameDispText.color = new Color (200.0f/255.0f, 200.0f/255.0f, 200.0f/255.0f, 0.0f/255.0f);

		//game start frame
		gamestartDispf = GameObject.Find("gamestartDispfPrefab");
		gamestartDispf.GetComponent<titleDispController> ().setInitState ( 100.0f, objImg );
		gamestartDispfImage = gamestartDispf.GetComponent<Image> ();
		gamestartDispfImage.color = new Color (90.0f/255.0f, 200.0f/255.0f, 90.0f/255.0f, 0.0f/255.0f);
		trigger = gamestartDispf.GetComponent<EventTrigger> ();
		entry = new EventTrigger.Entry ();
		entry.eventID = EventTriggerType.PointerDown;
		entry.callback.AddListener ( this.selectGameStart );
		trigger.triggers.Add(entry);
		//game start text
		gamestartDisp = GameObject.Find("gamestartDispPrefab");
		gamestartDispText = gamestartDisp.GetComponent<Text> ();
		gamestartDispText.text = "GAME START";
		gamestartDispText.color = new Color (200.0f/255.0f, 200.0f/255.0f, 200.0f/255.0f, 0.0f/255.0f);

		//credit frame
		creditDispf = GameObject.Find("creditDispfPrefab");
		creditDispf.GetComponent<titleDispController> ().setInitState ( 100.0f, objImg );
		creditDispfImage = creditDispf.GetComponent<Image> ();
		creditDispfImage.color = new Color (90.0f/255.0f, 200.0f/255.0f, 90.0f/255.0f, 0.0f/255.0f);
		trigger = creditDispf.GetComponent<EventTrigger> ();
		entry = new EventTrigger.Entry ();
		entry.eventID = EventTriggerType.PointerDown;
		entry.callback.AddListener ( this.selectCredit );
		trigger.triggers.Add(entry);
		//credit text
		creditDisp = GameObject.Find("creditDispPrefab");
		creditDispText = creditDisp.GetComponent<Text> ();
		creditDispText.text = "LICENSE";
		creditDispText.color = new Color (200.0f/255.0f, 200.0f/255.0f, 200.0f/255.0f, 0.0f/255.0f);

		//description frame
		descriptionDispf = GameObject.Find("descriptionDispfPrefab");
		descriptionDispf.GetComponent<titleDispController> ().setInitState ( 160.0f, objImg );
		descriptionDispfImage = descriptionDispf.GetComponent<Image> ();
		descriptionDispfImage.color = new Color (110.0f/255.0f, 120.0f/255.0f, 110.0f/255.0f, 0.0f/255.0f);
		//description text
		descriptionDisp = GameObject.Find("descriptionDispPrefab");
		descriptionDispText = descriptionDisp.GetComponent<Text> ();
		descriptionDispText.text = "";
		descriptionDispText.color = new Color (200.0f/255.0f, 200.0f/255.0f, 200.0f/255.0f, 0.0f/255.0f);
		//description arrow left text
		descriptionArrowLeftDisp = GameObject.Find("descriptionArrowLeftDispPrefab");
		descriptionArrowLeftDispText = descriptionArrowLeftDisp.GetComponent<Text> ();
		descriptionArrowLeftDispText.text = "<";
		descriptionArrowLeftDispText.color = new Color (200.0f/255.0f, 200.0f/255.0f, 200.0f/255.0f, 0.0f/255.0f);
		trigger = descriptionArrowLeftDisp.GetComponent<EventTrigger> ();
		entry = new EventTrigger.Entry ();
		entry.eventID = EventTriggerType.PointerDown;
		entry.callback.AddListener ( this.selectDescriptionArrowLeft );
		trigger.triggers.Add(entry);
		//description arrow right text
		descriptionArrowRightDisp = GameObject.Find("descriptionArrowRightDispPrefab");
		descriptionArrowRightDispText = descriptionArrowRightDisp.GetComponent<Text> ();
		descriptionArrowRightDispText.text = ">";
		descriptionArrowRightDispText.color = new Color (200.0f/255.0f, 200.0f/255.0f, 200.0f/255.0f, 0.0f/255.0f);
		trigger = descriptionArrowRightDisp.GetComponent<EventTrigger> ();
		entry = new EventTrigger.Entry ();
		entry.eventID = EventTriggerType.PointerDown;
		entry.callback.AddListener ( this.selectDescriptionArrowRight );
		trigger.triggers.Add(entry);

		//debug menu
		if (mc.getDebugMode() == true) {
			debugMenuDisp = Instantiate (debugMenuDispPrefab) as GameObject;
			debugMenuDisp.transform.SetParent (parent.transform, false);
			debugMenuDispText = debugMenuDisp.GetComponent<Text> ();
			debugMenuDispText.text = "デバッグメニュー";
			debugMenuDispText.color = new Color (200.0f / 255.0f, 200.0f / 255.0f, 200.0f / 255.0f, 0.0f / 255.0f);
			trigger = debugMenuDisp.GetComponent<EventTrigger> ();
			entry = new EventTrigger.Entry ();
			entry.eventID = EventTriggerType.PointerDown;
			entry.callback.AddListener (this.selectDebugMenu);
			trigger.triggers.Add (entry);
		}
		//first title logo rotate
		titlelogoRotate = true;
	}

	//title game level select color set
	public void setGameLevelColor(){
		float esa = glEasyText.color.a;
		float nma = glEasyText.color.a;
		float hda = glEasyText.color.a;
		switch (mc.gameLevel) {
		case 0x00:	//easy
			glEasyText.color = new Color (248.0f / 255.0f, 138.0f / 255.0f, 138.0f / 255.0f, esa);
			glNormalText.color = new Color (200.0f / 255.0f, 200.0f / 255.0f, 200.0f / 255.0f, nma);
			glHardText.color = new Color (200.0f / 255.0f, 200.0f / 255.0f, 200.0f / 255.0f, hda);
			setDescriptionTextIndividual (this.dscStrGlEasy);
			break;
		case 0x01:	//normal
			glEasyText.color = new Color (200.0f / 255.0f, 200.0f / 255.0f, 200.0f / 255.0f, esa);
			glNormalText.color = new Color (248.0f / 255.0f, 138.0f / 255.0f, 138.0f / 255.0f, nma);
			glHardText.color = new Color (200.0f / 255.0f, 200.0f / 255.0f, 200.0f / 255.0f, hda);
			setDescriptionTextIndividual (this.dscStrGlNormal);
			break;
		case 0x02:	//hard
			glEasyText.color = new Color (200.0f / 255.0f, 200.0f / 255.0f, 200.0f / 255.0f, esa);
			glNormalText.color = new Color (200.0f / 255.0f, 200.0f / 255.0f, 200.0f / 255.0f, nma);
			glHardText.color = new Color (248.0f / 255.0f, 138.0f / 255.0f, 138.0f / 255.0f, hda);
			setDescriptionTextIndividual (this.dscStrGlHard);
			break;
		default:
			break;
		}
	}

	//title player speed select color set
	public void setPlayerSpeedColor(){
		float sla = psSlowText.color.a;
		float nma = psSlowText.color.a;
		float fta = psSlowText.color.a;
		switch (mc.playerSpeed) {
		case 0x00:	//slow
			psSlowText.color = new Color (248.0f / 255.0f, 138.0f / 255.0f, 138.0f / 255.0f, sla);
			psNormalText.color = new Color (200.0f / 255.0f, 200.0f / 255.0f, 200.0f / 255.0f, nma);
			psFastText.color = new Color (200.0f / 255.0f, 200.0f / 255.0f, 200.0f / 255.0f, fta);
			setDescriptionTextIndividual (this.dscStrPsSlow);
			break;
		case 0x01:	//normal
			psSlowText.color = new Color (200.0f / 255.0f, 200.0f / 255.0f, 200.0f / 255.0f, sla);
			psNormalText.color = new Color (248.0f / 255.0f, 138.0f / 255.0f, 138.0f / 255.0f, nma);
			psFastText.color = new Color (200.0f / 255.0f, 200.0f / 255.0f, 200.0f / 255.0f, fta);
			setDescriptionTextIndividual (this.dscStrPsNormal);
			break;
		case 0x02:	//fast
			psSlowText.color = new Color (200.0f / 255.0f, 200.0f / 255.0f, 200.0f / 255.0f, sla);
			psNormalText.color = new Color (200.0f / 255.0f, 200.0f / 255.0f, 200.0f / 255.0f, nma);
			psFastText.color = new Color (248.0f / 255.0f, 138.0f / 255.0f, 138.0f / 255.0f, fta);
			setDescriptionTextIndividual (this.dscStrPsFast);
			break;
		default:
			break;
		}
	}

	//title sub weapon select color set
	public void setSubWeaponColor(){
		float lsa = swLaserText.color.a;
		float msa = swMissileText.color.a;
		switch (mc.subWeapon) {
		case 0x00:	//laser
			swLaserText.color = new Color (248.0f / 255.0f, 138.0f / 255.0f, 138.0f / 255.0f, lsa);
			swMissileText.color = new Color (200.0f / 255.0f, 200.0f / 255.0f, 200.0f / 255.0f, msa);
			setDescriptionTextIndividual (this.dscStrSwLaser);
			break;
		case 0x01:	//missile
			swLaserText.color = new Color (200.0f / 255.0f, 200.0f / 255.0f, 200.0f / 255.0f, lsa);
			swMissileText.color = new Color (248.0f / 255.0f, 138.0f / 255.0f, 138.0f / 255.0f, msa);
			setDescriptionTextIndividual (this.dscStrSwMissile);
			break;
		default:
			break;
		}
	}

	//title player type select color set
	public void setPlayerTypeColor( bool alpha_max = false ){
		float ta_a = ptADispImage.color.a;
		float tat_a = ptANameDispText.color.a;
		float tb_a = ptBDispImage.color.a;
		float tbt_a = ptBNameDispText.color.a;
		float tc_a = ptCDispImage.color.a;
		float tct_a = ptCNameDispText.color.a;
		switch (mc.playerType) {
		case 0x00:	//typeA
			if (alpha_max == true) {
				ta_a = 255.0f / 255.0f;
				tb_a = 130.0f / 255.0f;
				tc_a = 130.0f / 255.0f;
			}
			ptADispImage.color = new Color (255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f, ta_a);
			ptANameDispText.color = new Color (248.0f / 255.0f, 138.0f / 255.0f, 138.0f / 255.0f, tat_a);
			ptBDispImage.color = new Color (130.0f / 255.0f, 130.0f / 255.0f, 130.0f / 255.0f, tb_a);
			ptBNameDispText.color = new Color (200.0f / 255.0f, 200.0f / 255.0f, 200.0f / 255.0f, tbt_a);
			ptCDispImage.color = new Color (130.0f / 255.0f, 130.0f / 255.0f, 130.0f / 255.0f, tc_a);
			ptCNameDispText.color = new Color (200.0f / 255.0f, 200.0f / 255.0f, 200.0f / 255.0f, tct_a);
			setDescriptionTextIndividual (this.dscStrPtTypeA);
			break;
		case 0x01:	//typeB
			if (alpha_max == true) {
				ta_a = 130.0f / 255.0f;
				tb_a = 255.0f / 255.0f;
				tc_a = 130.0f / 255.0f;
			}
			ptADispImage.color = new Color (130.0f / 255.0f, 130.0f / 255.0f, 130.0f / 255.0f, ta_a);
			ptANameDispText.color = new Color (200.0f / 255.0f, 200.0f / 255.0f, 200.0f / 255.0f, tat_a);
			ptBDispImage.color = new Color (255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f, tb_a);
			ptBNameDispText.color = new Color (248.0f / 255.0f, 138.0f / 255.0f, 138.0f / 255.0f, tbt_a);
			ptCDispImage.color = new Color (130.0f / 255.0f, 130.0f / 255.0f, 130.0f / 255.0f, tc_a);
			ptCNameDispText.color = new Color (200.0f / 255.0f, 200.0f / 255.0f, 200.0f / 255.0f, tct_a);
			setDescriptionTextIndividual (this.dscStrPtTypeB);
			break;
		case 0x02:	//typeC
			if (alpha_max == true) {
				ta_a = 130.0f / 255.0f;
				tb_a = 130.0f / 255.0f;
				tc_a = 255.0f / 255.0f;
			}
			ptADispImage.color = new Color (130.0f / 255.0f, 130.0f / 255.0f, 130.0f / 255.0f, ta_a);
			ptANameDispText.color = new Color (200.0f / 255.0f, 200.0f / 255.0f, 200.0f / 255.0f, tat_a);
			ptBDispImage.color = new Color (130.0f / 255.0f, 130.0f / 255.0f, 130.0f / 255.0f, tb_a);
			ptBNameDispText.color = new Color (200.0f / 255.0f, 200.0f / 255.0f, 200.0f / 255.0f, tbt_a);
			ptCDispImage.color = new Color (255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f, tc_a);
			ptCNameDispText.color = new Color (248.0f / 255.0f, 138.0f / 255.0f, 138.0f / 255.0f, tct_a);
			setDescriptionTextIndividual (this.dscStrPtTypeC);
			break;
		default:
			break;
		}
	}

	//title game start select color set
	public void setGameStartColor(){
		gamestartDispText.color = new Color (248.0f / 255.0f, 138.0f / 255.0f, 138.0f / 255.0f, 255.0f / 255.0f);
	}

	//title credit select color set
	public void setCreditStartColor(){
		creditDispText.color = new Color (248.0f / 255.0f, 138.0f / 255.0f, 138.0f / 255.0f, 255.0f / 255.0f);
	}

	//title debug menu select color set
	public void setDebugMenuStartColor(){
		debugMenuDispText.color = new Color (248.0f / 255.0f, 138.0f / 255.0f, 138.0f / 255.0f, 255.0f / 255.0f);
	}

	//title version+debugmode display set
	public void setVerDisplay(){
		if (mc.getDebugMode() == true) {
			verDispText.text = "ver" + Application.version + " debugmode:on";
		}
	}

	//title version+debugmode display clear
	public void clearVerDisplay(){
		verDispText.text = "";
	}

	//select title description arrow left process
	public void selDescriptionArrowLeft(){
		dscIndex--;
		if (dscIndex < 0) {
			dscIndex = (descriptionStringTable.Length-1);
		}
		this.setDescriptionText ();
		dscArrowLeftColorCnt = 5;
	}

	//select title description arrow right process
	public void selDescriptionArrowRight(){
		dscIndex++;
		if (dscIndex >= (descriptionStringTable.Length)) {
			dscIndex = 0;
		}
		this.setDescriptionText ();
		dscArrowRightColorCnt = 5;
	}

	//set title description display
	public void setDescriptionText(){
		dscStr = "(" + ((dscIndex+1).ToString()) + "/" + (descriptionStringTable.Length.ToString()) + ")" + descriptionStringTable [dscIndex];
		dscStrDispCnt = 0;
		dscStrDispIntCnt = 0;
		dscDisplay = true;
	}

	//set title description display individual 
	public void setDescriptionTextIndividual( string st){
		dscStr = st;
		dscStrDispCnt = 0;
		dscStrDispIntCnt = 0;
		dscDisplay = true;
	}

	//generate credit screen
	public void generateCreditScreen(){
		GameObject parent = GameObject.Find ("Canvas");
		//credit
		creditListDisp = Instantiate (creditListDispPrefab) as GameObject;
		creditListDisp.transform.SetParent (parent.transform, false);
		creditListDispText = creditListDisp.GetComponent<Text> ();
		creditListDispText.color = new Color (90.0f/255.0f, 200.0f/255.0f, 90.0f/255.0f, 0.0f/255.0f);
		creditListDispText.text = 
			"CREDITS\n\n\nGAME DESIGNER\nPROGRAMMER\nALL SOUNDS COMPOSER\n<color=#FFFFFF>KOICHI 'JON' NISHIYAMA</color>\n\n\nSPECIAL ADVISER\n<color=#FFFFFF>Inochin\nIPC-BOSS\nIPC-UME\nJUNKO\nQ-CHAN\nshirokuma\nYUKO.S\nYutaka Otake</color>\n\n\nGRAPHICS\n<color=#ffffff>M.F.S.RECLUSE\n<size=9>http://mfstg.web.fc2.com/</size></color>\n\n\nFONTS\n<color=#ffffff>Heart To Me\n<size=9>http://www2g.biglobe.ne.jp/~misana/</size>\npatrick h. lauke\n<size=9>https://www.splintered.co.uk/</size></color>\n\n\nSPECIAL THANKS\n<color=#ffffff>HARUMI\nHIDEKI ASAI\nKOKOCHAN\nRIKA\nTSUGUMI</color>";
	}

	//generate debug menu screen
	public void generateDebugMenuListScreen(){
		GameObject parent = GameObject.Find ("Canvas");
		//debugmenu list
		debugMenuListDisp = Instantiate (debugMenuListDispPrefab) as GameObject;
		debugMenuListDisp.transform.SetParent (parent.transform, false);
		debugMenuListDispText = debugMenuListDisp.GetComponent<Text> ();
		debugMenuListDispText.color = new Color (255.0f/255.0f, 255.0f/255.0f, 255.0f/255.0f, 255.0f/255.0f);
		debugMenuListDispText.text = "デバッグメニュー\n\n\n\n\n\n\n\nショットパワー\n\n\nオプション\n\n\nサブウェポンパワー\n\n\nスタート地点\n\n";
		//jikimuteki
		jikimuteki = GameObject.Find("jikimutekiPrefab");
		jikimutekiToggle = jikimuteki.GetComponent<Toggle> ();
		jikimutekiToggle.isOn = mc.debug_jikimuteki;
		jikimutekiToggle.onValueChanged.AddListener( this.select_jikimuteki );
		mc.debug_jikimuteki = jikimutekiToggle.isOn;
		//jikimugen
		jikimugen = GameObject.Find("jikimugenPrefab");
		jikimugenToggle = jikimugen.GetComponent<Toggle> ();
		jikimugenToggle.isOn = mc.debug_jikimugen;
		jikimugenToggle.onValueChanged.AddListener( this.select_jikimugen );
		mc.debug_jikimugen = jikimugenToggle.isOn;
		//jikimugen
		bombmugen = GameObject.Find("bombmugenPrefab");
		bombmugenToggle = bombmugen.GetComponent<Toggle> ();
		bombmugenToggle.isOn = mc.debug_bombmugen;
		bombmugenToggle.onValueChanged.AddListener( this.select_bombmugen );
		mc.debug_bombmugen = bombmugenToggle.isOn;
		//debug status( slider value )
		debugstatus = GameObject.Find("debugstatusPrefab");
		debugStatusText = debugstatus.GetComponent<Text> ();
		debugStatusText.text = "";
		//shot power
		spower = GameObject.Find("shotpowerPrefab");
		spowerSlider = spower.GetComponent<Slider> ();
		spowerSlider.value = mc.debug_spower;
		spowerSlider.onValueChanged.AddListener (this.select_spower);
		mc.sel_spower (spowerSlider.value);
		//option
		option = GameObject.Find("optionnumPrefab");
		optionSlider = option.GetComponent<Slider> ();
		optionSlider.value = mc.debug_option;
		optionSlider.onValueChanged.AddListener (this.select_option);
		mc.sel_option (optionSlider.value);
		//sub weapon
		sweapon = GameObject.Find("subweaponpowerPrefab");
		sweaponSlider = sweapon.GetComponent<Slider> ();
		sweaponSlider.value = mc.debug_swpower;
		sweaponSlider.onValueChanged.AddListener (this.select_sweapon);
		mc.sel_sweapon (sweaponSlider.value);
		//start position
		startpos = GameObject.Find("startpositionPrefab");
		startposDropdown = startpos.GetComponent<Dropdown> ();
		startposDropdown.value = mc.debug_startposition;
		startposDropdown.onValueChanged.AddListener (this.select_startpos);
		mc.sel_startpos (startposDropdown.value);
		//debug start
		debugstart = GameObject.Find("debugstartPrefab");
		debugstartButton = debugstart.GetComponent<Button> ();
		debugstartButton.onClick.AddListener (this.select_debugstart);
		//debug return title
		debugTitle = GameObject.Find("debugTitlePrefab");
		debugTitleButton = debugTitle.GetComponent<Button> ();
		debugTitleButton.onClick.AddListener (this.select_debugTitle);
	}

	//info no display
	public void infoNoDisplay(){
		dpMode = dpModeNoDisp;
		//up info
		scoreTxt.GetComponent<RectTransform> ().localPosition = new Vector3(scoretxt_x, disp_u1ybase+disp_uoffset, 0);
		scoreDisp.GetComponent<RectTransform> ().localPosition = new Vector3(scoredisp_x, disp_u2ybase+disp_uoffset, 0);
		shieldTxt.GetComponent<RectTransform> ().localPosition = new Vector3(shieldtxt_x, disp_u1ybase+disp_uoffset, 0);
		shieldDisp.GetComponent<RectTransform> ().localPosition = new Vector3(shielddisp_x, disp_u2ybase+disp_uoffset, 0);
		playerTxt.GetComponent<RectTransform> ().localPosition = new Vector3(playertxt_x, disp_u1ybase+disp_uoffset, 0);
		playerDisp.GetComponent<RectTransform> ().localPosition = new Vector3(playerdisp_x, disp_u2ybase+disp_uoffset, 0);
		//up3 info
		pauseButton.GetComponent<RectTransform>().localPosition = new Vector3(pauseb_xbase+pauseb_xbase, pauseb_y, 0);
		//bomb info
		bombButton.GetComponent<RectTransform>().localPosition = new Vector3(bombb_xbase+disp_bomboffset, bombb_y, 0);
		bombTxt.GetComponent<RectTransform>().localPosition = new Vector3(bombtxt_xbase+disp_bombtxtoffset, bombtxt_y, 0);
		bombDisp.GetComponent<RectTransform>().localPosition = new Vector3(bombdisp_xbase+disp_bombdispoffset, bombdisp_y, 0);
		//star info
		starTxt.GetComponent<RectTransform>().localPosition = new Vector3(startxt_xbase+disp_startxtoffset, startxt_y, 0);
		starDisp.GetComponent<RectTransform>().localPosition = new Vector3(stardisp_xbase+disp_stardispoffset, stardisp_y, 0);
	}

	//game info display on
	public void infoDisplay(){
		dpMode = dpModeDispOn;
		//up info
		scoreTxt.GetComponent<RectTransform> ().localPosition = new Vector3(scoretxt_x, disp_u1ybase, 0);
		scoreDisp.GetComponent<RectTransform> ().localPosition = new Vector3(scoredisp_x, disp_u2ybase, 0);
		shieldTxt.GetComponent<RectTransform> ().localPosition = new Vector3(shieldtxt_x, disp_u1ybase, 0);
		shieldDisp.GetComponent<RectTransform> ().localPosition = new Vector3(shielddisp_x, disp_u2ybase, 0);
		playerTxt.GetComponent<RectTransform> ().localPosition = new Vector3(playertxt_x, disp_u1ybase, 0);
		playerDisp.GetComponent<RectTransform> ().localPosition = new Vector3(playerdisp_x, disp_u2ybase, 0);
		//up3 info
		pauseButton.GetComponent<RectTransform>().localPosition = new Vector3(pauseb_xbase, pauseb_y, 0);
		//bomb info
		bombButton.GetComponent<RectTransform>().localPosition = new Vector3(bombb_xbase, bombb_y, 0);
		bombTxt.GetComponent<RectTransform>().localPosition = new Vector3(bombtxt_xbase, bombtxt_y, 0);
		bombDisp.GetComponent<RectTransform>().localPosition = new Vector3(bombdisp_xbase, bombdisp_y, 0);
		//star info
		starTxt.GetComponent<RectTransform>().localPosition = new Vector3(startxt_xbase, startxt_y, 0);
		starDisp.GetComponent<RectTransform>().localPosition = new Vector3(stardisp_xbase, stardisp_y, 0);
	}

	//game info display in
	public void infoDisplayIn(){
		dpMode = dpModeDispIn;
		//offset
		u1_ymov = disp_uoffset;
		u3_xmov = pauseb_offset;
		bb_xmov = disp_bomboffset;
		bt_xmov = disp_bombtxtoffset;
		bd_xmov = disp_bombdispoffset;
		st_xmov = disp_startxtoffset;
		sd_xmov = disp_stardispoffset;
		//up info
		scoreTxt.GetComponent<RectTransform> ().localPosition = new Vector3(scoretxt_x, disp_u1ybase+u1_ymov, 0);
		scoreDisp.GetComponent<RectTransform> ().localPosition = new Vector3(scoredisp_x, disp_u2ybase+u1_ymov, 0);
		shieldTxt.GetComponent<RectTransform> ().localPosition = new Vector3(shieldtxt_x, disp_u1ybase+u1_ymov, 0);
		shieldDisp.GetComponent<RectTransform> ().localPosition = new Vector3(shielddisp_x, disp_u2ybase+u1_ymov, 0);
		playerTxt.GetComponent<RectTransform> ().localPosition = new Vector3(playertxt_x, disp_u1ybase+u1_ymov, 0);
		playerDisp.GetComponent<RectTransform> ().localPosition = new Vector3(playerdisp_x, disp_u2ybase+u1_ymov, 0);
		//up3 info
		pauseButton.GetComponent<RectTransform>().localPosition = new Vector3(pauseb_xbase+u3_xmov, pauseb_y, 0);
		//bomb info
		bombButton.GetComponent<RectTransform>().localPosition = new Vector3(bombb_xbase+bb_xmov, bombb_y, 0);
		bombTxt.GetComponent<RectTransform>().localPosition = new Vector3(bombtxt_xbase+bt_xmov, bombtxt_y, 0);
		bombDisp.GetComponent<RectTransform>().localPosition = new Vector3(bombdisp_xbase+bd_xmov, bombdisp_y, 0);
		//star info
		starTxt.GetComponent<RectTransform>().localPosition = new Vector3(startxt_xbase+st_xmov, startxt_y, 0);
		starDisp.GetComponent<RectTransform>().localPosition = new Vector3(stardisp_xbase+sd_xmov, stardisp_y, 0);
	}

	//game info display out
	public void infoDisplayOut(){
		dpMode = dpModeDispOut;
		//offset
		u1_ymov = 0.0f;
		u3_xmov = 0.0f;
		bb_xmov = 0.0f;
		bt_xmov = 0.0f;
		bd_xmov = 0.0f;
		st_xmov = 0.0f;
		sd_xmov = 0.0f;
		//up info
		scoreTxt.GetComponent<RectTransform> ().localPosition = new Vector3(scoretxt_x, disp_u1ybase+u1_ymov, 0);
		scoreDisp.GetComponent<RectTransform> ().localPosition = new Vector3(scoredisp_x, disp_u2ybase+u1_ymov, 0);
		shieldTxt.GetComponent<RectTransform> ().localPosition = new Vector3(shieldtxt_x, disp_u1ybase+u1_ymov, 0);
		shieldDisp.GetComponent<RectTransform> ().localPosition = new Vector3(shielddisp_x, disp_u2ybase+u1_ymov, 0);
		playerTxt.GetComponent<RectTransform> ().localPosition = new Vector3(playertxt_x, disp_u1ybase+u1_ymov, 0);
		playerDisp.GetComponent<RectTransform> ().localPosition = new Vector3(playerdisp_x, disp_u2ybase+u1_ymov, 0);
		//up3 info
		pauseButton.GetComponent<RectTransform>().localPosition = new Vector3(pauseb_xbase+u3_xmov, pauseb_y, 0);
		//bomb info
		bombButton.GetComponent<RectTransform>().localPosition = new Vector3(bombb_xbase+bb_xmov, bombb_y, 0);
		bombTxt.GetComponent<RectTransform>().localPosition = new Vector3(bombtxt_xbase+bt_xmov, bombtxt_y, 0);
		bombDisp.GetComponent<RectTransform>().localPosition = new Vector3(bombdisp_xbase+bd_xmov, bombdisp_y, 0);
		//star info
		starTxt.GetComponent<RectTransform>().localPosition = new Vector3(startxt_xbase+st_xmov, startxt_y, 0);
		starDisp.GetComponent<RectTransform>().localPosition = new Vector3(stardisp_xbase+sd_xmov, stardisp_y, 0);
	}

	//ehp no display
	public void ehpNoDisplay(){
		ehMode = ehModeNoDisp;
		ehCurrent = 0.0f;
		ehTarget = 0.0f;
		ehBattle = false;
		//enemy hp
		ehBasef.GetComponent<RectTransform>().localPosition = new Vector3(eh_xbase+eh_offset, ehbf_y, 0);
		ehBasef.GetComponent<RectTransform> ().localScale = new Vector3 (1.0f, 1.0f, 1.0f);
		ehBase.GetComponent<RectTransform>().localPosition = new Vector3(eh_xbase+eh_offset, ehb_y, 0);
		ehBase.GetComponent<RectTransform> ().localScale = new Vector3 (1.0f, 1.0f, 1.0f);
		cashRectTransform_eh.localPosition = new Vector3(eh_xbase+eh_offset, eh_y, 0);
		cashRectTransform_eh.localScale = new Vector3 (1.0f, 0.0f, 1.0f);
		cashCanvasRenderer_eh.SetColor( new Color (220.0f / 255.0f, 220.0f / 255.0f, 90.0f / 255.0f) );
	}

	//ehp display on
	public void ehpDisplay( int level ){
		ehMode = ehModeDispOn;
		ehLevel = level;
		//yoffset
		float yoffsetb = 0;
		float yoffset = 0;
		if (ehLevel == 0) {
			yoffsetb = eh_yoffset;
			yoffset = eh_yoffset-2;
		} else if (ehLevel == 1) {
			yoffsetb = 0;
			yoffset = 0;
		}
		//enemy hp
		ehBasef.GetComponent<RectTransform>().localPosition = new Vector3(eh_xbase, (ehbf_y+yoffsetb), 0);
		if (level == 0) {
			ehBasef.GetComponent<RectTransform> ().localScale = new Vector3 (1.0f, 0.5f, 1.0f);
		} else if(level == 1){
			ehBasef.GetComponent<RectTransform> ().localScale = new Vector3 (1.0f, 1.0f, 1.0f);
		}
		ehBase.GetComponent<RectTransform>().localPosition = new Vector3(eh_xbase, (ehb_y+yoffset), 0);
		if (level == 0) {
			ehBase.GetComponent<RectTransform> ().localScale = new Vector3 (1.0f, 0.5f, 1.0f);
		} else if(level == 1){
			ehBase.GetComponent<RectTransform> ().localScale = new Vector3 (1.0f, 1.0f, 1.0f);
		}
		cashRectTransform_eh.localPosition = new Vector3(eh_xbase, (eh_y+yoffset), 0);
		if (level == 0) {
			cashRectTransform_eh.localScale = new Vector3 (1.0f, (ehCurrent/2.0f), 1.0f);
		} else if(level == 1){
			cashRectTransform_eh.localScale = new Vector3 (1.0f, (ehCurrent), 1.0f);
		}
//		cashCanvasRenderer_eh.SetColor( new Color (220.0f / 255.0f, 220.0f / 255.0f, 90.0f / 255.0f) );
	}

	//ehp display in
	public void ehpDisplayIn( int level ){
		ehMode = ehModeDispIn;
		eh_xmov = eh_offset;
		ehLevel = level;
		ehCurrent = 0.0f;
		ehTarget = 0.0f;
		ehBattle = false;
		//yoffset
		float yoffsetb = 0;
		float yoffset = 0;
		if (ehLevel == 0) {
			yoffsetb = eh_yoffset;
			yoffset = eh_yoffset-2;
		} else if (ehLevel == 1) {
			yoffsetb = 0;
			yoffset = 0;
		}
		//enemy hp
		ehBasef.GetComponent<RectTransform>().localPosition = new Vector3(eh_xbase+eh_xmov, (ehbf_y+yoffsetb), 0);
		if (level == 0) {
			ehBasef.GetComponent<RectTransform> ().localScale = new Vector3 (1.0f, 0.5f, 1.0f);
		} else if(level == 1){
			ehBasef.GetComponent<RectTransform> ().localScale = new Vector3 (1.0f, 1.0f, 1.0f);
		}
		ehBase.GetComponent<RectTransform>().localPosition = new Vector3(eh_xbase+eh_xmov, (ehb_y+yoffset), 0);
		if (level == 0) {
			ehBase.GetComponent<RectTransform> ().localScale = new Vector3 (1.0f, 0.5f, 1.0f);
		} else if(level == 1){
			ehBase.GetComponent<RectTransform> ().localScale = new Vector3 (1.0f, 1.0f, 1.0f);
		}
		cashRectTransform_eh.localPosition = new Vector3(eh_xbase+eh_xmov, (eh_y+yoffset), 0);
		cashRectTransform_eh.localScale = new Vector3 (1.0f, 0.0f, 1.0f);
		cashCanvasRenderer_eh.SetColor( new Color (220.0f / 255.0f, 220.0f / 255.0f, 90.0f / 255.0f) );
	}

	//ehp display out
	public void ehpDisplayOut(){
		ehMode = ehModeDispOut;
		eh_xmov = 0.0f;
		ehCurrent = 0.0f;
		ehTarget = 0.0f;
		ehBattle = false;
		//yoffset
		float yoffset = 0;
		if (ehLevel == 0) {
			yoffset = eh_yoffset;
		} else if (ehLevel == 1) {
			yoffset = 0;
		}
		//enemy hp
		ehBasef.GetComponent<RectTransform>().localPosition = new Vector3(eh_xbase+eh_xmov, (ehbf_y+yoffset), 0);
		ehBase.GetComponent<RectTransform>().localPosition = new Vector3(eh_xbase+eh_xmov, (ehb_y+yoffset), 0);
		cashRectTransform_eh.localPosition = new Vector3(eh_xbase+eh_xmov, (eh_y+yoffset), 0);
	}

	//submenu no display
	public void subMenuNoDisplay(){
		//continue button
		GameObject.Destroy( continueButton );
		continueButton = null;
		//goto title button
		GameObject.Destroy( toTitleButton );
		toTitleButton = null;
	}

	//submenu display on
	public void subMenuDisplayOn( bool ending = false ){
		GameObject parent = GameObject.Find ("Canvas");
		//continue button
		if (ending == false) {
			if (continueButton == null) {
				continueButton = Instantiate (continueButtonPrefab) as GameObject;
				continueButton.transform.SetParent (parent.transform, false);
			}
		}
		//goto title button
		if (toTitleButton == null) {
			toTitleButton = Instantiate (gotoTitleButtonPrefab) as GameObject;
			toTitleButton.transform.SetParent (parent.transform, false);
			if (ending == true) {
				Vector3 pos = toTitleButton.transform.position;
				pos.y = 150;
				toTitleButton.transform.position = pos;
			}
		}
	}

	//display score
	public void dispScore( int score ){
		scoreDispText.text = score.ToString("D");
	}

	//display shield
	public void dispShield( int sld ){
		string stxt = "";
		if (sld <= 0) {
			shieldDispText.font = font2;
			shieldDispText.fontSize = 15;
			stxt = "NONE";
		} else {
			shieldDispText.font = font1;
			shieldDispText.fontSize = 18;
			for (int i = 0; i < sld; i++) {
				stxt = stxt + "*";
			}
		}
		shieldDispText.text = stxt;
	}

	//display player num
	public void dispPlayer( int ply ){
		string ptxt = "";
		if (ply <= 0) {
			playerDispText.font = font2;
			playerDispText.fontSize = 15;
			ptxt = "NONE";
		} else {
			playerDispText.font = font1;
			playerDispText.fontSize = 18;
			for (int i = 0; i < ply; i++) {
				ptxt = ptxt + "*";
			}
		}
		playerDispText.text = ptxt;
	}

	//display bomb num
	public void dispBomb( int bomb ){
		string btxt = "";
		if (bomb == 0) {
			bombDispText.font = font2;
			bombDispText.fontSize = 15;
			btxt = "NONE";
		} else {
			bombDispText.font = font1;
			bombDispText.fontSize = 18;
			for (int i = 0; i < bomb; i++) {
				btxt = btxt + "*";
			}
		}
		bombDispText.text = btxt;
	}

	//display star num
	public void dispStar( int star ){
		starDispText.text = star.ToString("D");
	}

	//set score color change
	const int colorChgCnt = 60;
	public void setScoreColorChg(){
		scoreColorCnt = colorChgCnt;
		scoreDispText.color = new Color (255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f, 220.0f / 255.0f);
	}

	//set shield color change
	public void setShieldColorChg( int cl = 0 ){
		shieldColorCnt = colorChgCnt;
		if (cl == 0) {
			shieldDispText.color = new Color (255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f, 220.0f / 255.0f);
		} else if (cl == 1) {
			shieldDispText.color = new Color (248.0f / 255.0f, 138.0f / 255.0f, 138.0f / 255.0f, 220.0f / 255.0f);
		}
	}

	//set player color change
	public void setPlayerColorChg( int cl = 0 ){
		playerColorCnt = colorChgCnt;
		if (cl == 0) {
			playerDispText.color = new Color (255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f, 220.0f / 255.0f);
		} else if (cl == 1) {
			playerDispText.color = new Color (248.0f / 255.0f, 138.0f / 255.0f, 138.0f / 255.0f, 220.0f / 255.0f);
		}
	}

	//set bomb num color change
	public void setBombNumColorChg(){
		bombColorCnt = colorChgCnt;
		bombDispText.color = new Color (255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f, 220.0f / 255.0f);
	}

	//set star num color change
	public void setStarNumColorChg(){
		starNumColorCnt = colorChgCnt;
		starDispText.color = new Color (255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f, 220.0f / 255.0f);
	}

	//set main message disp
	public void setMainMessage( string str, int mode = mdModeTimeDisp, int fontsize = 36, int msgcnt = 50, int pauseonoff = 0 ){
		if (pauseonoff == 2) {	//pause off
			if (mainMsgCnt_pauseBackup > 0) {
				mainMsgDispMode = mainMsgDispMode_pauseBackup;
				mainMsgStr = mainMsgStr_pauseBackup;
				mainMsgStrDispCnt = mainMsgStrDispCnt_pauseBackup;
				mainMsgStrDispIntCnt = mainMsgStrDispIntCnt_pauseBackup;
				mainMsgCnt = mainMsgCnt_pauseBackup;
				mainMsgBlinkCnt = mainMsgBlinkCnt_pauseBackup;
				mainMessageDispText.text = "";// mainMsgStr;
				mainMessageDispText.color = new Color (90.0f / 255.0f, 200.0f / 255.0f, 90.0f / 255.0f, 210.0f / 255.0f);
				mainMessageDispText.fontSize = mainMsgStrFontSize_pauseBackup;
				return;
			}
		}
		if (pauseonoff == 1) {	//pause on
			if (mainMsgCnt > 0) {
				mainMsgDispMode_pauseBackup = mainMsgDispMode;
				mainMsgStr_pauseBackup = mainMsgStr;
				mainMsgStrDispCnt_pauseBackup = mainMsgStrDispCnt;
				mainMsgStrDispIntCnt_pauseBackup = mainMsgStrDispIntCnt;
				mainMsgCnt_pauseBackup = mainMsgCnt;
				mainMsgBlinkCnt_pauseBackup = mainMsgBlinkCnt;
				mainMsgStrFontSize_pauseBackup = mainMessageDispText.fontSize;
				mainMessageDispText.text = "";
			}
		}
		mainMessageDispText.color = new Color (90.0f / 255.0f, 200.0f / 255.0f, 90.0f / 255.0f, 210.0f / 255.0f);
		mainMessageDispText.fontSize = fontsize;
		this.mainMsgStr = str;
		if (mode == mdModeTimeDisp) {
			this.mainMsgCnt = msgcnt;
		} else if (mode == mdModeDispStart) {
			this.mainMsgCnt = 1;
		} else if (mode == mdModeDispDelete) {
			this.mainMsgCnt = 0;
			this.mainMsgCnt_pauseBackup = 0;
			mainMessageDispText.text = "";
		}
		this.mainMsgDispMode = mode;
		this.mainMsgBlinkCnt = 0;
		this.mainMsgStrDispCnt = 0;
		this.mainMsgStrDispIntCnt = 0;
	}

	//set sub message disp
	public void setSubMessage( float wx, float wy, float xx, float yy, string str, int cl = 0, bool getstar = false ){
		//parent (canvas)
		GameObject parent = GameObject.Find ("Canvas");
		float srx = parent.GetComponent<CanvasScaler>().referenceResolution.x;
		float sry = parent.GetComponent<CanvasScaler>().referenceResolution.y;
		//pos convert
		Vector3 spos = Camera.main.WorldToScreenPoint( new Vector3(wx, wy, 0) );
		spos.x = spos.x/Camera.main.pixelWidth * srx;
		spos.y = spos.y/Camera.main.pixelHeight * sry;
		spos.x = spos.x - (srx/2);
		spos.y = spos.y - (sry/2);
		//generate sub message
		GameObject subMessageDisp = Instantiate (subMessageControllerPrefab) as GameObject;
		subMessageDisp.transform.SetParent (parent.transform, false);
		subMessageController subMessageControllerDisp = subMessageDisp.GetComponent<subMessageController> ();
		subMessageControllerDisp.setInitStatus (spos.x, spos.y, xx, yy, str, cl, getstar);
	}

	//enemy hp disp
	public void dispEhp( int hp, int hpb ){
		float hhp = (float)hp;
		float hhpb = (float)hpb;
		if (ehMode == ehModeDispOn) {
			ehTarget = hhp / hhpb;
			ehBattle = true;
		}
	}

	//start result display
	public void startResultDisplay(){
		//generate result display
		GameObject parent = GameObject.Find ("Canvas");
		EventTrigger trigger;
		EventTrigger.Entry entry;
		//basef
		resultBasef = Instantiate (resultBasefPrefab) as GameObject;
		resultBasef.transform.SetParent (parent.transform, false);
		//base
		resultBase = GameObject.Find("resultBasePrefab");
		//Text
		resultDisp = GameObject.Find("resultTextPrefab");
		trigger = resultDisp.GetComponent<EventTrigger> ();
		entry = new EventTrigger.Entry ();
		entry.eventID = EventTriggerType.PointerDown;
		entry.callback.AddListener (this.selectResultDisp );
		trigger.triggers.Add(entry);
		resultDispText = resultDisp.GetComponent<Text> ();
		//generate initial text
		lineCal = new string[]{
			"<color=\"#f88a8a\">STAGE ",	//stage
			"\n\nSTAR*?   ×1000 =",	//star
			"\n\nPLAYER*? *10000 =",	//player
			"\n\nSHIELD*? *5000 =",	//shield
			"\n\nBOMB*?  *3000 =",	//bomb
			"\n\nNO CONTINUE =",	//no continue
			"\n\nNO MISS =",	//no miss
			"\n\nNO DAMAGE =",	//no damage
			"\n\nBOMB NO USE =",	//no bomb use
			"\n\n\n<color=\"#f88a8a\">TOTAL BONUS </color>",	//total
		};
		lineRslt = new string[]{
			"0 RESULT</color>",	//stage
			"      <color=\"#f8f88a\">?</color>",	//star
			"     <color=\"#f8f88a\">?</color>",	//player
			"      <color=\"#f8f88a\">?</color>",	//shield
			"       <color=\"#f8f88a\">?</color>",	//bomb
			"         <color=\"#f8f88a\">?</color>",	//no continue
			"             <color=\"#f8f88a\">?</color>",	//no miss
			"           <color=\"#f8f88a\">?</color>",	//no damage
			"         <color=\"#f8f88a\">?</color>",	//no bomb use
			"<color=\"#ffffff\">????????</color>",	//toral
		};
		lineRslt[0] = ((mc.getStage()+1).ToString()) + " RESULT</color>";
		for (int i = 0; i < lineCal.Length; i++) {
			resultStr = resultStr + lineCal [i] + lineRslt [i];
		}
		resultDispText.text = resultStr;
		//display start
		mc.setPauseMaskOn();
//		Time.timeScale = 0.2f;
		resultDispSeq = 0;
		resultDispNextSeq = 0;
		resultDispSeqCnt = 0;
		resultDispFixCnt = 0;
		resultTotal = 0;
		crntTotal = 0;
		colorCnt = 0;
		resultStr = "";
		resultSkip = false;
		resultBasef.transform.transform.localScale = new Vector3 (0.05f, 0.05f, 1.0f);
		this.resultDisplay = true;
	}

	//stop result display
	public void stopResultDisplay(){
		//stop result display
		this.resultDisplay = false;
		GameObject.Destroy (resultBasef);
		GameObject.Destroy (nextStageButton);
		mc.setPauseMaskOff ();
	}

	//generate goto next stage button
	private void generateNextStageButton(){
		//generate goto next stage button
		GameObject parent = GameObject.Find ("Canvas");
		//goto next stage button
		nextStageButton = Instantiate (nextStageButtonPrefab) as GameObject;
		nextStageButton.transform.SetParent (parent.transform, false);
		if( mc.getStageLast() == true ){
			nextStageButton.GetComponentInChildren<Text>().text = "OK";
		}
	}

	//start ending display
	public void startEndingDisplay(){
		this.endingDisplay = true;
	}

	//title screen click trigger process
	public void selectGlEasy( BaseEventData eventData ){
		mc.selGameLevel( mc.gameLevelEasy );
	}
	public void selectGlNormal( BaseEventData eventData ){
		mc.selGameLevel( mc.gameLevelNormal );
	}
	public void selectGlHard( BaseEventData eventData ){
		mc.selGameLevel( mc.gameLevelHard );
	}
	public void selectPsSlow( BaseEventData eventData ){
		mc.selPlayerSpeed( mc.playerSpeedSlow );
	}
	public void selectPsNormal( BaseEventData eventData ){
		mc.selPlayerSpeed( mc.playerSpeedNormal );
	}
	public void selectPsFast( BaseEventData eventData ){
		mc.selPlayerSpeed( mc.playerSpeedFast );
	}
	public void selectSwLaser( BaseEventData eventData ){
		mc.selSubWeapon( mc.subWeaponLaser );
	}
	public void selectSwMissile( BaseEventData eventData ){
		mc.selSubWeapon( mc.subWeaponMissile );
	}
	public void selectPtTypaA( BaseEventData eventData ){
		mc.selPlayerType( mc.playerTypeA );
	}
	public void selectPtTypaB( BaseEventData eventData ){
		mc.selPlayerType( mc.playerTypeB );
	}
	public void selectPtTypaC( BaseEventData eventData ){
		mc.selPlayerType( mc.playerTypeC );
	}
	public void selectGameStart( BaseEventData eventData ){
		dscDisplay = false;	//description display stop
		dscArrowLeftColorCnt = 0;
		dscArrowRightColorCnt = 0;
		mc.selGameStart ();
	}
	public void selectCredit( BaseEventData eventData ){
		dscDisplay = false;	//description display stop
		dscArrowLeftColorCnt = 0;
		dscArrowRightColorCnt = 0;
		mc.selCredit ();
	}
	public void selectDescriptionArrowLeft( BaseEventData eventData ){
		dscDisplay = false;	//description display stop
		dscArrowLeftColorCnt = 0;
		mc.selDescriptionArrowLeft ();
	}
	public void selectDescriptionArrowRight( BaseEventData eventData ){
		dscDisplay = false;	//description display stop
		dscArrowRightColorCnt = 0;
		mc.selDescriptionArrowRight ();
	}
	public void selectDebugMenu( BaseEventData eventData ){
		dscDisplay = false;	//description display stop
		dscArrowLeftColorCnt = 0;
		dscArrowRightColorCnt = 0;
		mc.selDebugMenu ();
	}

	//result display click trigger process
	public void selectResultDisp( BaseEventData eventData ){
		if (mc.getPause () == false) {
			this.resultSkip = true;
		}
	}

	//debug menu click trigger process
	public void select_jikimuteki( bool mtk ){
		mc.sel_jikimuteki (mtk);
	}
	public void select_jikimugen( bool mgn ){
		mc.sel_jikimugen (mgn);
	}
	public void select_bombmugen( bool bmgn ){
		mc.sel_bombmugen (bmgn);
	}
	public void select_spower( float sp ){
		debugStatusText.text = "shot level\n" + (Mathf.Floor (sp * 5.0f)).ToString();
		mc.sel_spower (sp);
	}
	public void select_option( float op ){
		debugStatusText.text = "option\n" + (2 + ((int)(Mathf.Floor (op * 4.0f))*2)).ToString();
		mc.sel_option (op);
	}
	public void select_sweapon( float sw ){
		debugStatusText.text = "s.w. level\n" + (Mathf.Floor (sw * 5.0f)).ToString();
		mc.sel_sweapon (sw);
	}
	public void select_startpos( int sp ){
		mc.sel_startpos (sp);
	}
	public void select_debugstart(){
		GameObject.Destroy (debugMenuListDisp);
		mc.sel_debugstart ();
	}
	public void select_debugTitle(){
		GameObject.Destroy (debugMenuListDisp);
		mc.sel_debugTitle ();
	}

	//get result display 
	public bool getResultDisplay(){
		return resultDisplay;
	}

	//set title logo first rotate reset
	public void setResetFirstRotate(){
		this.titlelogoRotate = false;
	}

}
