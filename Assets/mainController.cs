//#define MAP_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class mainController : MonoBehaviour {

	//debug mode
//	private readonly bool debugMode = false;
	private readonly bool debugMode = true;

	//public
	//public enemy bullet100 Prefab
	public GameObject enemyBullet100ControllerPrefab;
	//public enemy bullet110 Prefab
	public GameObject enemyBullet110ControllerPrefab;
	//public enemy bullet120 Prefab
	public GameObject enemyBullet120ControllerPrefab;
	//public enemy bullet130 Prefab
	public GameObject enemyBullet130ControllerPrefab;
	//public player damage controller Prefab
	public GameObject damagePlayerControllerPrefab;
	//public enemy damage controller Prefab
	public GameObject damageEnemyControllerPrefab;
	//public explosion middle controller Prefab
	public GameObject explosion100ControllerPrefab;
	//public explosion large controller Prefab
	public GameObject explosion110ControllerPrefab;
	//public explosion big controller Prefab
	public GameObject explosion120ControllerPrefab;
	//public ground explosion controller Prefab
	public GameObject explosion130ControllerPrefab;
	//public burner100Controller Prefab
	public GameObject burner100ControllerPrefab;
	//public powerup100Controller Prefab
	public GameObject powerup100ControllerPrefab;
	//public wipe1 prefab
	public GameObject wipe1ControllerPrefab;
	//public wipe2 prefab
	public GameObject wipe2ControllerPrefab;

	//public enemy50 prefab(for game event)
	public GameObject enemy50ControllerPrefab;
	//public enemy100 prefab(for game event)
	public GameObject enemy100ControllerPrefab;
	//public enemy110 prefab(for game event)
	public GameObject enemy110ControllerPrefab;
	//public enemy120 prefab(for game event)
	public GameObject enemy120ControllerPrefab;
	//public enemy130 prefab(for game event)
	public GameObject enemy130ControllerPrefab;
	//public enemy150 prefab(for game event)
	public GameObject enemy150ControllerPrefab;
	//public enemy160 prefab(for game event)
	public GameObject enemy160ControllerPrefab;
	//public enemy170 prefab(for game event)
	public GameObject enemy170ControllerPrefab;
	//public enemy180 prefab(for game event)
	public GameObject enemy180ControllerPrefab;
	//public enemy190 prefab(for game event)
	public GameObject enemy190ControllerPrefab;
	//public enemy200 prefab(for game event)
	public GameObject enemy200ControllerPrefab;
	//public enemy210 prefab(for game event)
	public GameObject enemy210ControllerPrefab;
	//public enemy220 prefab(for game event)
	public GameObject enemy220ControllerPrefab;
	//public enemy230 prefab(for game event)
	public GameObject enemy230ControllerPrefab;
	//public enemy240 prefab(for game event)
	public GameObject enemy240ControllerPrefab;
	//public enemy300 prefab(for game event)
	public GameObject enemy300ControllerPrefab;
	//public enemy500 prefab(for game event)
	public GameObject enemy500ControllerPrefab;
	//public enemy515 prefab(for game event)
	public GameObject enemy515ControllerPrefab;
	//public enemy520 prefab(for game event)
	public GameObject enemy520ControllerPrefab;
	//public enemy530 prefab(for game event)
	public GameObject enemy530ControllerPrefab;

	//public playerbase100 prefab(for game event)
	public GameObject playerBase100ControllerPrefab;


	//system public
	//system const

	//public base x,y min/max
	public float xmin = -4.0f;
	public float xmax = 4.0f;
	public float ymin = -6.0f;
	public float ymax = 6.0f;
	//public bgm
	public int b100 = geBgm100;
	public int b500 = geBgm500;
	public int b501 = geBgm501;
	public int b800 = geBgm800;
	public int b110 = geBgm110;	//result display
	public int b120 = geBgm120;
	public int b101 = geBgm101;
	public int b121 = geBgm121;	//stage1
	public int b502 = geBgm502;	//boss
	public int b801 = geBgm801;	//title screen
	public int b130 = geBgm130;
	public int b131 = geBgm131;
	public int b132 = geBgm132;
	public int b133 = geBgm133;
	public int b140 = geBgm140;
	public int b141 = geBgm141;
	public int b142 = geBgm142;
	public int b143 = geBgm143;	//stage2
	public int b144 = geBgm144;
	public int b152 = geBgm152;	//boss3-1
	public int b153 = geBgm153;
	public int b154 = geBgm154;
	public int b162 = geBgm162;
	public int b163 = geBgm163;
	public int b164 = geBgm164;
	public int b172 = geBgm172;
	public int b174 = geBgm174;
	public int b185 = geBgm185;	//stage3
	//public voice
	public int vo100 = geVoice100;	//title call
	public int vo101 = geVoice101;	//title call echo
	public int vo110 = geVoice110;	//tap description(日本語)
	public int vo120 = geVoice120;	//tap description
	public int vo130 = geVoice130;	//power up description
	public int vo140 = geVoice140;	//power up
	public int vo150 = geVoice150;	//player damage
	public int vo160 = geVoice160;
	public int vo170 = geVoice170;	//warning*2
	public int vo180 = geVoice180;
	public int vo190 = geVoice190;
	public int vo200 = geVoice200;
	public int vo210 = geVoice210;
	public int vo220 = geVoice220;
	public int vo230 = geVoice230;	//title descroption (no distortion)
	public int vo240 = geVoice240;	//please wait moment
	public int vo121 = geVoice121;	//tap description long
	public int vo250 = geVoice250;	//title descroption add s.w. p.type(no distortion)
	public int vo260 = geVoice260;
	public int vo270 = geVoice270;
	public int vo280 = geVoice280;
	//public se
	//(title/menu)
	public int se_t100 = geSe_title100;	//title opening
	public int se_ts100 = geSe_titlesel100;	//menu select
	public int se_ts110 = geSe_titlesel110;	//menu fix
	public int se_t101 = geSe_title101;	//title opening mini
	public int se_ts101 = geSe_titlesel101;	//menu select mini
	public int se_ts111 = geSe_titlesel111;	//menu fix mini
	//(base)
	public int se_bs130 = geSe_base130;	//base acceleration
	//(map)
	public int se_bs131 = geSe_base131;	//map acceleratioin
	//(player)
	public int se_bomb = 0xb8;	//player bomb
	public int se_getitem = 0xb9;	//player get item
	public int se_getstar = 0xba;	//player get star
	public int se_bombempty = 0xbb;	//player bomb empty
	public int se_playershot = 0xbc;	//player shot
	public int se_optionshot = 0xbd;	//player option shot
	public int se_playerlaser = 0xbe;	//player laser shot
	public int se_playermissile = 0xbf;	//player missile shot
	public int se_playerdamage = 0xc0;	//player damage
	public int se_playerexplosion = 0xc1;	//player explosion
	public int se_playermissilebomb = 0xc2;	//player missle bomb
	//(enemy)
	public int se_enemydamage = 0xc3;	//enemy damage
	public int se_enemybullet = 0xc4;	//enemy bullet shot
	public int se_enemyexplosion = 0xc5;	//enemy explosion
	public int se_bossexplosion = 0xc6;	//enemy boss explosion
	public int se_middlebossnoise = 0xc7;	//enemy middle boss flight noise
	public int se_middlebossflightin = 0xc8;	//enemy middle boss flight in
	public int se_middlebossexplosion = 0xc9;	//enemy middle boss explosion
	public int se_bosswakeup = 0xca;	//enemy boss wakeup
	public int se_bossroadnoise = 0xcb;	//enemy boss roadnoise
	public int se_bossrotate = 0xcc;	//enemy boss rotate
	//(base)
	public int se_basenoise = 0xcd;	//base flight noise
	public int se_baseflightin = 0xce;	//base flight noise
	public int se_basereleaseplayer = 0xcf;	//base release player
	//(title/menu)
	public int se_resultcnt = 0xd0;	//result display total count
	//(player)
	public int se_bomblaser = 0xd1;	//player bomb laser

	//public game mode
	//game mode
	public int gamemodeTitleStart = gmTitleStart;
	public int gamemodeTitle = gmTitle;
	public int gamemodeCreditStart = gmCreditStart;
	public int gamemodeCredit = gmCredit;
	public int gamemodeTermCredit = gmTermCredit;
	public int gamemodeDebugMenuStart = gmDebugMenuStart;
	public int gamemodeDebugMenu = gmDebugMenu;
	public int gamemodeGameStart = gmGameStart;
	public int gamemodePlay = gmPlay;
	public int gamemodePlayDebug = gmPlayDebug;
	public int gamemodeGotoTitle = gmGotoTitle;
	#if MAP_EDITOR
	public int gamemodeMapEditor = gmMapEditor;
	#endif
	public int gameMode;

	//public game level
	public int gameLevelEasy = gmlEasy;
	public int gameLevelNormal = gmlNormal;
	public int gameLevelHard = gmlHard;
	public int gameLevel;

	//public player speed
	public int playerSpeedSlow = plsSlow;
	public int playerSpeedNormal = plsNormal;
	public int playerSpeedFast = plsFast;
	public int playerSpeed;

	//public sub weapon
	public int subWeaponLaser = swpLaser;
	public int subWeaponMissile = swpMissile;
	public int subWeapon;

	//public play type
	public int playerTypeA = ptTypeA;
	public int playerTypeB = ptTypeB;
	public int playerTypeC = ptTypeC;
	public int playerType;

	//public title screen color mode
	public int gameTitleColorModeNormal = 0x00;
	public int gameTitleColorModeFadein = 0x01;
	public int gameTitleColorModeFadeout = 0x02;
	public int gameTitleColorMode;

	//public title screen title logo flash
	public bool gameTitleLogoFlash = false;

	//public credit screen color mode
	public int gameCreditColorModeNormal = 0x00;
	public int gameCreditColorModeFadein = 0x01;
	public int gameCreditColorModeFadeout = 0x02;
	public int gameCreditColorMode;

	//public enemy damage point
	public int damageSmall = 2;
	public int damageMiddle = 10;
	public int damageBig = 50;
	public int damagePlayerBullet = 1;
	public int damagePlayerLaser1 = 1;
	public int damagePlayerLaser2 = 2;
	public int damagePlayerMissile1 = 3;
	public int damagePlayerMissileBomb1 = 9;//10;
	public int damagePlayerMissile2 = 4;//5;
	public int damagePlayerMissileBomb2 = 15;//16;
	public int damagePlayer = 15;
	public int damagePlayerBomb1 = 500;
	public int damagePlayerBomb2 = 1400;
	public int damagePlayerBombLaser = 80;
	public int damageWipe1 = 500;
	public int damageWipe2 = 500;

	//public powerup type
	//item type
	public int puType_powerup = 0x00;
	public int puType_laser = 0x01;
	public int puType_missile = 0x02;
	public int puType_option = 0x03;
	public int puType_bomb = 0x04;
	public int puType_shield = 0x05;
	public int puType_score = 0x06;
	public int puType_1up = 0x07;
	public int puType_None = -1;

	//public debug value
	public bool debug_jikimuteki;
	public bool debug_jikimugen;
	public bool debug_bombmugen;
	public float debug_spower;
	public float debug_option;
	public float debug_swpower;
	public int debug_startposition;
	//public debug value start potision
	public int debug_sp1 = debugSpos1;
	public int debug_sp1_nobase = debugSpos2;
	public int debug_sp1_st1 = debugSpos3;
	public int debug_sp1_mboss = debugSpos4;
	public int debug_sp1_st1_2 = debugSpos5;
	public int debug_sp1_boss = debugSpos6;
	public int debug_sp1_st1_c = debugSpos7;
	public int debug_sp2 = debugSpos8;
	public int debug_sp2_boss = debugSpos9;
	public int debug_sp3 = debugSpos10;
	public int debug_sp3_boss = debugSpos11;

	//private
	//local const
	//game mode
	const int gmTitleStart = 0x80;
	const int gmTitle = 0x81;
	const int gmCreditStart = 0x90;
	const int gmCredit = 0x91;
	const int gmTermCredit = 0x92;
	const int gmDebugMenuStart = 0xa0;
	const int gmDebugMenu = 0xa1;
	const int gmGameStart = 0x00;
	const int gmPlay = 0x01;
	const int gmPlayDebug = 0x10;
	const int gmGotoTitle = 0x20;
	#if MAP_EDITOR
	const int gmMapEditor = 0xf0;
	#endif
	//game level
	const int gmlEasy = 0x00;
	const int gmlNormal = 0x01;
	const int gmlHard = 0x02;
	//player speeed
	const int plsSlow = 0x00;
	const int plsNormal = 0x01;
	const int plsFast = 0x02;
	//sub weapon
	const int swpLaser = 0x00;
	const int swpMissile = 0x01;
	//play type
	const int ptTypeA = 0x00;
	const int ptTypeB = 0x01;
	const int ptTypeC = 0x02;

	//const for game event table
	//game event
	//game event mode
	const int geModeNormal = 0x00;
	const int geModeWait = 0x80;
	const int geModeStop = 0xff;

	//event type
	const int geScreenMask = 0x10;
	const int geDisplay = 0x20;
	const int geStatus = 0x30;
	const int geEnemy = 0x80;
	const int gePlayer = 0x90;
	const int geBgm = 0xa0;
	const int geVoice = 0xa1;
	const int geSE = 0xa2;
	const int geMap = 0xb0;
	const int geTitle = 0xc0;
	const int geCredit = 0xc1;
	const int geDebugMenu = 0xc2;
	const int geGameMode = 0xd0;
	const int geTerm = 0xe0;
	const int geLoop = 0xe1;
	const int geNextArea = 0xe2;
	const int geNextStage = 0xe3;
	const int geWait = 0xff;

	//screen mask param1
	const int geNoMask = 0x00;
	const int geMask = 0x01;
	const int geMaskFadeout = 0x02;
	const int geMaskFadein = 0x03;
	//display param1
	const int geInfoNoDisplay = 0x00;
	const int geInfoDisplay = 0x01;
	const int geInfoDisplayIn = 0x02;
	const int geInfoDisplayOut = 0x03;
	const int geEhNoDisplay = 0x04;
	const int geEhDisplay = 0x05;
	const int geEhDisplayIn = 0x06;
	const int geEhDisplayOut = 0x07;
	const int geMainMessageDisplay = 0x08;
	const int geResultDisplayStart = 0x09;
	const int geEndingDisplayStart = 0x0A;
	//display mainmessage param2
	const int geMainMessageWarning = 0x00;
	const int geMainMessageStageStart = 0x01;
	const int geMainMessageStageClear = 0x02;
	const int geMainMessageDelete = 0xff;
	//display mainmessage param3
	const int geMainMessageDisplayTime = 0x00;
	const int geMainMessageDisplayStart = 0x01;
	const int geMainMessageDisplayDelete = 0x02;
	//dispalt eh param2
	const int geEhDisplayLv0 = 0x00;
	const int geEhDisplayLv1 = 0x01;
	//status param1
	const int geDestroyGamePlayObjects = 0x00;
	//enemy param1
	const int geEnemy50 = 50;
	const int geEnemy100 = 100;
	const int geEnemy110 = 110;
	const int geEnemy120 = 120;
	const int geEnemy130 = 130;
	const int geEnemy150 = 150;
	const int geEnemy160 = 160;
	const int geEnemy170 = 170;
	const int geEnemy180 = 180;
	const int geEnemy190 = 190;
	const int geEnemy200 = 200;
	const int geEnemy210 = 210;
	const int geEnemy220 = 220;
	const int geEnemy230 = 230;
	const int geEnemy240 = 240;
	const int geEnemy300 = 300;
	const int geEnemy500 = 500;
	const int geEnemy515 = 515;
	const int geEnemy520 = 520;
	const int geEnemy530 = 530;
	//enemy param enemt130 pattern
	const int geEne130_stop = 0;
	const int geEne130_forward = 1;
	const int geEne130_approaches = 2;
	const int geEne130_escape = 3;
	const int geEne130_totarget = 4;
	//enemy param last
	const int geItemPower = 0;
	const int geItemLaser = 1;
	const int geItemMissile = 2;
	const int geItemOption = 3;
	const int geItemBomb = 4;
	const int geItemShield = 5;
	const int geItemScore = 6;
	const int geItem1up = 7;
	const int geItemNone = -1;
	//player param1
	const int geSetPlayerMode = 0;
	const int gePlayerBase100 = 100;
	//player param2
	const int gePlayerModeNormal = 0x00;	//game play
	const int gePlayerModeInvalid = 0x01;	//after damage
	const int gePlayerModeRebirth = 0x02;	//after death
	const int gePlayerModeOnBase = 0x03;	//at start on playerbase
	const int gePlayerModeNoExist = 0xff;	//no exist player
	//bgm param1
	const int geBgmPlay = 0x00;
	const int geBgmStop = 0x01;
	const int geBgmFadeout = 0x02;
	//bgm param2
	const int geBgm100 = 0x00;
	const int geBgm500 = 0x01;
	const int geBgm501 = 0x02;
	const int geBgm800 = 0x03;
	const int geBgm110 = 0x04;
	const int geBgm120 = 0x05;
	const int geBgm101 = 0x06;
	const int geBgm121 = 0x07;
	const int geBgm502 = 0x08;
	const int geBgm801 = 0x09;
	const int geBgm130 = 0x0a;
	const int geBgm131 = 0x0b;
	const int geBgm132 = 0x0c;
	const int geBgm133 = 0x0d;
	const int geBgm140 = 0x0e;
	const int geBgm141 = 0x0f;
	const int geBgm142 = 0x10;
	const int geBgm143 = 0x11;
	const int geBgm144 = 0x12;
	const int geBgm152 = 0x13;
	const int geBgm153 = 0x14;
	const int geBgm154 = 0x15;
	const int geBgm162 = 0x16;
	const int geBgm163 = 0x17;
	const int geBgm164 = 0x18;
	const int geBgm172 = 0x19;
	const int geBgm174 = 0x1a;
	const int geBgm185 = 0x1b;
	//bgm param3
	const int geBgmNormal = 0x00;
	const int geBgmForce = 0x01;
	//voice param1
	const int geVoicePlay = 0x00;
	const int geVoiceStop = 0x01;
	const int geVoiceFadeout = 0x02;
	//voice param2
	const int geVoice100 = 0x80;
	const int geVoice101 = 0x81;
	const int geVoice110 = 0x82;
	const int geVoice120 = 0x83;
	const int geVoice130 = 0x84;
	const int geVoice140 = 0x85;
	const int geVoice150 = 0x86;
	const int geVoice160 = 0x87;
	const int geVoice170 = 0x88;
	const int geVoice180 = 0x89;
	const int geVoice190 = 0x8a;
	const int geVoice200 = 0x8b;
	const int geVoice210 = 0x8c;
	const int geVoice220 = 0x8d;
	const int geVoice230 = 0x8e;
	const int geVoice240 = 0x8f;
	const int geVoice121 = 0x90;
	const int geVoice250 = 0x91;
	const int geVoice260 = 0x92;
	const int geVoice270 = 0x93;
	const int geVoice280 = 0x94;
	//voice param3
	const int geVoiceNormal = 0x00;
	const int geVoiceForce = 0x01;
	//se param1
	const int geSePlay = 0x00;
	const int geSeStop = 0x01;
	const int geSeFadeout = 0x02;
	//se param2
	const int geSe_title100 = 0xb0;
	const int geSe_titlesel100 = 0xb1;
	const int geSe_titlesel110 = 0xb2;
	const int geSe_title101 = 0xb3;
	const int geSe_titlesel101 = 0xb4;
	const int geSe_titlesel111 = 0xb5;
	const int geSe_base130 = 0xb6;
	const int geSe_base131 = 0xb7;
	//voice param3
	const int geSeNormal = 0x00;
	const int geSeForce = 0x01;
	//map param1
	const int geMapStage = 0x00;
	const int geMapScrollTarget = 0x01;
	const int geMapScrollSet = 0x02;
	const int geMapScrollMovexEnable = 0x03;
	const int geMapScrollMovexDisable = 0x04;
	const int geMapScrollMovexReset = 0x05;
	//map param2
	const int geMapTitle = 0x00;
	const int geMapStage1 = 0x01;
	const int geMapStage2 = 0x02;
	const int geMapStage3 = 0x03;
	const int geMapStage32 = 0x04;
	const int geMapStage33 = 0x05;
	const int geMapDummy = 0x06;
	//title param1
	const int geTitleInit = 0x00;
	const int geTitleFadein = 0x01;
	const int geTitleFadeout = 0x02;
	const int geTitleEnable = 0x03;
	const int geTitleDisable = 0x04;
	//credit param1
	const int geCreditInit = 0x00;
	const int geCreditFadein = 0x01;
	const int geCreditFadeout = 0x02;
	const int geCreditEnable = 0x03;
	const int geCreditDisable = 0x04;
	//debug menu param1
	const int geDebugMenuInit = 0x00;
	//game mode param1
	const int geGameModeTitleStart = 0x00;
	const int geGameModeTitle = 0x01;
	const int geGameModeCreditStart = 0x02;
	const int geGameModeCredit = 0x03;
	const int geGameModeTermCredit = 0x04;
	const int geGameModeDebugMenuStart = 0x05;
	const int geGameModeDebugMenu = 0x06;
	const int geGameModeGameStart = 0x07;
	const int geGameModePlay = 0x08;
	const int geGameModePlayDebug = 0x09;
	const int geGameModeGotoTitle = 0x0a;

	//game stage
	const int gameStage1 = 0x00;
	const int gameStage2 = 0x01;
	const int gameStage3 = 0x02;
	const int gameStageLast = gameStage3;
	//game area
	const int gameArea1 = 0x00;
	const int gameArea2 = 0x01;
	const int gameArea3 = 0x02;
	const int gameArea4 = 0x03;
	const int gameArea5 = 0x04;
	const int gameArea6 = 0x05;
	//debug start position
	const int debugSpos1 = 0x00;	//s1a1 base
	const int debugSpos2 = 0x01;	//s1a2
	const int debugSpos3 = 0x02;	//s1a3 m boss
	const int debugSpos4 = 0x03;	//s1a4
	const int debugSpos5 = 0x04;	//s1a5 boss
	const int debugSpos6 = 0x05;	//s1a6 clear

	const int debugSpos7 = 0x06;	//s2a1
	const int debugSpos8 = 0x07;	//s2a2 m boss
	const int debugSpos9 = 0x08;	//s2a3
	const int debugSpos10 = 0x09;	//s2a4 boss
	const int debugSpos11 = 0x0a;	//s2a5 clear

	const int debugSpos12 = 0x0b;	//s3a1
	const int debugSpos13 = 0x0c;	//s3a2 m boss
	const int debugSpos14 = 0x0d;	//s3a3
	const int debugSpos15 = 0x0e;	//s3a4 boss 1
	const int debugSpos16 = 0x0f;	//s3a5 boss 2
	const int debugSpos17 = 0x10;	//s3a5 clear

	//counter bullet shot on/off table (1:on)
	readonly int[] counterBulletTable = new int[] {
		1, 0, 1, 1, 0,
	};

	//
	//(event table partial mainControllerEventTable.cs)
	//

	//system local
	//interval count
	int intervalCnt;

	//objects num
	int objNum;
	int starObjNum;
	int starGetMessageObjNum;

	//component cash
	GameObject playerCtr;
	playerController plc;
	GameObject mapCtr;
	mapController mpc;
	GameObject screenCtr;
	screenController src;
	GameObject displayCtr;
	displayController dsc;
	GameObject soundCtr;
	soundController sdc;
	//system
	GameObject stsDisp;
	Text stsDispText;

	//local
	//game stage
	int gameStage;
	//game area
	int gameArea;

	//game score
	int gameScore;

	//star num
	int starNum;

	//continue cnt
	int continueCnt;

	//stage result
	int stgGetStar;
	bool stgUseContinue;
	bool stgLostPlayer;
	bool stgHaveDamage;
	bool stgUseBomb;

	//scroll move x enable
	bool scrollMovexEnable;

	//title event process
	bool gameTitleEnable;

	//credit event process
	bool gameCreditEnable;

	//display mode
	int dpMode;

	//pause
	bool gamePause;
	bool pauseMask;
	bool pauseMask_pauseBackup;
	float timescale_pauseBackup;

	//game over
	bool gameOver;

	//ending
	bool ending;

	//game event process
	int gameEventIndex;	//table index
	int gameEventMode;	//process mode
	int gameEventWaitCnt;	//wait cnt
	int gameEventWaitTime;	//wait target

	//counter bullet cnt
	int counterBulletCnt;

	//power up item description voice cnt
	int pupVoiceCnt;

	//debug option num
	int debug_optionnum;

	// Use this for initialization
	void Start () {
		//system init
		intervalCnt = 0;
		objNum = 0;
		starObjNum = 0;
		starGetMessageObjNum = 0;

		//cash
		//sts disp text
		stsDisp = GameObject.Find ("stsDisp");
		stsDispText = stsDisp.GetComponent<Text> ();

		//player controller
		playerCtr = GameObject.Find ("playerController");
		plc = playerCtr.GetComponent<playerController> ();

		//map controller
		mapCtr = GameObject.Find ("mapController");
		mpc = mapCtr.GetComponent<mapController> ();

		//screen controller
		screenCtr = GameObject.Find ("screenController");
		src = screenCtr.GetComponent<screenController> ();

		//display controller
		displayCtr = GameObject.Find ("displayController");
		dsc = displayCtr.GetComponent<displayController> ();

		//sound controller
		soundCtr = GameObject.Find ("soundController");
		sdc = soundCtr.GetComponent<soundController> ();

		//public init
//debug -->
//新キャラ/パターン/イベント実装時のテスト用
//		gameLevel = gameLevelEasy;	//debug
		gameLevel = gameLevelNormal;	//debug
//		gameLevel = gameLevelHard;	//debug
//debug <--
//debug -->
//新キャラ/パターン/イベント実装時のテスト用
//		playerSpeed = playerSpeedSlow;	//debug
		playerSpeed = playerSpeedNormal;	//debug
//		playerSpeed = playerSpeedFast;	//debug
//debug <--
//debug -->
//新キャラ/パターン/イベント実装時のテスト用
		subWeapon = swpLaser;	//debug
//		subWeapon = swpMissile;	//debug
//debug <--
//debug -->
//新キャラ/パターン/イベント実装時のテスト用
		playerType = playerTypeA;	//debug
//		playerType = playerTypeB;	//debug
//		playerType = playerTypeC;	//debug
//debug <--

		gameTitleColorMode = gameTitleColorModeNormal;
		gameCreditColorMode = gameCreditColorModeNormal;

		//local init
		//game stage
//debug -->
//新キャラ/パターン/イベント実装時のテスト用
		gameStage = gameStage1;
//		gameStage = gameStage2;	//debug
//		gameStage = gameStage3;	//debug
//debug <--
		//game area
		gameArea = gameArea1;

		//game score
		gameScore = 0;

		//star num
		starNum = 0;

		//continue cnt
		continueCnt = 0;

		//pause
		gamePause = false;
		pauseMask = false;
		pauseMask_pauseBackup = false;
		timescale_pauseBackup = 1.0f;

		//game over
		gameOver = false;

		//ending
		ending = false;

		//stage result
		stgGetStar = 0;
		stgUseContinue = false;
		stgLostPlayer = false;
		stgHaveDamage = false;
		stgUseBomb = false;

		//object num reset
		starObjNum = 0;
		starGetMessageObjNum = 0;

		//scroll move x enable
		scrollMovexEnable = true;

		//counter bullet cnt
		counterBulletCnt = 0;

		//power up item description voice cnt
		pupVoiceCnt = 3;

		//debug option num
		debug_optionnum = 2;
		//debug public value
		debug_jikimuteki = false;
		debug_jikimugen = true;
		debug_bombmugen = true;
		debug_spower = 0;
		debug_option = 0;
		debug_swpower = 0;
		debug_startposition = debugSpos2;	//no base default

		//fps display on/off
		if (this.getDebugMode() == false) {
			GameObject go = GameObject.Find ("gameController");
			go.GetComponent<gameController> ().sw_fpsDisp = false;
		}

		//game mode init
		#if MAP_EDITOR
		setGameMode( gmMapEditor );
		#else
		setGameMode( gmTitleStart );
		#endif
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

		//game mode process
//		Debug.Log( "gamemode:"+gameMode.ToString()+" idx:"+gameEventIndex.ToString() );	//debug
		switch (gameMode) {
		case gmTitleStart:
			//title start event process
			this.gameEventProcess( ref gameEventTitleStartTable );
			break;
		case gmTitle:
			//title event process
			this.gameEventProcess( ref gameEventTitleTable );
			break;
		case gmCreditStart:
			//credit start event process
			this.gameEventProcess( ref gameEventCreditStartTable );
			break;
		case gmCredit:
			//credit event event process
			this.gameEventProcess( ref gameEventCreditTable );
			break;
		case gmTermCredit:
			//term credit event process
			this.gameEventProcess( ref gameEventTermCreditTable );
			break;
		case gmDebugMenuStart:
			//debug menu start event process
			this.gameEventProcess( ref gameEventDebugMenuStartTable );
			break;
		case gmDebugMenu:
			//debug menu event process
			this.gameEventProcess( ref gameEventDebugMenuTable );
			break;
		case gmGameStart:
			//game start event process
			this.gameEventProcess( ref gameEventGameStartTable );
			break;
		case gmPlay:
			//game play event process
			this.gameEventPlayProcess();	//stage/area table process
			break;
		case gmPlayDebug:
			//game play event process (debug)
			this.gameEventPlayProcess();	//stage/area table process
			break;
		case gmGotoTitle:
			//goto title event process
			this.gameEventProcess( ref gameEventGotoTitleTable );
			break;
		default:
			break;
		}

		//credit screen tap  return to title screen
		if ((this.gameCreditEnable == true) && (this.gameMode == gmCredit)) {
			if (Input.GetMouseButtonDown (0)) {
				this.playSound (se_ts100);
				this.setGameMode (gmTermCredit);
			}
		}

//debug --> 
		#if UNITY_EDITOR
		if( Input.anyKeyDown == true ){
			//space key game start ( for unity editor only )
			if (this.gameTitleEnable == true) {
				if (Input.GetKeyDown (KeyCode.Space)) {
					this.selGameStart ();
				}
			}

			//'f' key debug mode start ( for unity editor only )
			if (this.gameTitleEnable == true) {
				if (Input.GetKeyDown (KeyCode.F)) {
					this.selDebugMenu ();
				}
			}

			//'f' key debug mode start ( for unity editor only )
			if (this.gameMode == gmDebugMenu) {
				if (Input.GetKeyDown (KeyCode.F)) {
					dsc.select_debugstart ();
				}
			}

			//'p' key pause ( for unity editor only )
			if( (this.gameMode == gmPlay) || (this.gameMode == gmPlayDebug) ){
				if (Input.GetKeyDown (KeyCode.P)) {
					this.tapPauseButton ();
				}
			}
		}
		#endif
//debug <--

		////interval process
		//interval count
		intervalCnt++;
		if ((intervalCnt % 2) == 0) {
			//nop
		}
		if (intervalCnt >= 6) {
			intervalCnt = 0;
			//display objects num
			if (this.getDebugMode() == true) {
				stsDispText.text = "objects:" + objNum.ToString ("D");
			} else {
				//stsDispText.text = "";
			}
		}
	}

	//private
	//game event play process
	private void gameEventPlayProcess(){
		if ((gameOver == false) && (gamePause == false)) {	//gameover or pause?
			//stage/area table process
			switch (gameStage) {
			case gameStage1:
				switch (gameArea) {
				case gameArea1:
					this.gameEventProcess (ref gameEventStage1Area1Table);
					break;
				case gameArea2:
					this.gameEventProcess (ref gameEventStage1Area2Table);
					break;
				case gameArea3:
					this.gameEventProcess (ref gameEventStage1Area3Table);
					break;
				case gameArea4:
					this.gameEventProcess (ref gameEventStage1Area4Table);
					break;
				case gameArea5:
					this.gameEventProcess (ref gameEventStage1Area5Table);
					break;
				case gameArea6:
					this.gameEventProcess (ref gameEventStage1Area6Table);
					break;
				default:
					break;
				}
				break;
			case gameStage2:
				switch (gameArea) {
				case gameArea1:
					this.gameEventProcess (ref gameEventStage2Area1Table);
					break;
				case gameArea2:
					this.gameEventProcess (ref gameEventStage2Area2Table);
					break;
				case gameArea3:
					this.gameEventProcess (ref gameEventStage2Area3Table);
					break;
				case gameArea4:
					this.gameEventProcess (ref gameEventStage2Area4Table);
					break;
				case gameArea5:
					this.gameEventProcess (ref gameEventStage2Area5Table);
					break;
				default:
					break;
				}
				break;
			case gameStage3:
				switch (gameArea) {
				case gameArea1:
					this.gameEventProcess (ref gameEventStage3Area1Table);
					break;
				case gameArea2:
					this.gameEventProcess (ref gameEventStage3Area2Table);
					break;
				case gameArea3:
					this.gameEventProcess (ref gameEventStage3Area3Table);
					break;
				case gameArea4:
					this.gameEventProcess (ref gameEventStage3Area4Table);
					break;
				case gameArea5:
					this.gameEventProcess (ref gameEventStage3Area5Table);
					break;
				case gameArea6:
					this.gameEventProcess (ref gameEventStage3Area6Table);
					break;
				default:
					break;
				}
				break;
			default:
				break;
			}
		}
	}

	//game event process
	private void gameEventProcess( ref int[] evtTbl ){
		//game event process
		switch( gameEventMode ){
		case geModeWait:
			//wait mode process
			this.waitProcess( ref evtTbl );
			break;
		case geModeNormal:
			//normal mode process
			int evt = evtTbl [gameEventIndex];
			switch ( evt ) {
			case geScreenMask:
				//screen mask
				this.screenMaskProcess( ref evtTbl );
				break;
			case geDisplay:
				this.gameDisplayProcess( ref evtTbl );
				break;
			case geStatus:
				this.gameStatusProcess( ref evtTbl );
				break;
			case geEnemy:
				//enemy process
				this.enemyProcess( ref evtTbl );
				break;
			case gePlayer:
				//player process
				this.playerProcess (ref evtTbl);
				break;
			case geBgm:
				//bgm process
				this.bgmProcess( ref evtTbl );
				break;
			case geVoice:
				//voice process
				this.voiceProcess( ref evtTbl );
				break;
			case geSE:
				//se process
				this.seProcess( ref evtTbl );
				break;
			case geMap:
				//map process
				this.mapProcess( ref evtTbl );
				break;
			case geTitle:
				//title screen process
				this.titleProcess( ref evtTbl );
				break;
			case geCredit:
				//credit screeen process
				this.creditProcess ( ref evtTbl );
				break;
			case geDebugMenu:
				//debug menu process
				this.debugMenuProcess ( ref evtTbl );
				break;
			case geGameMode:
				//gameMode process
				this.gameModeProcess (ref evtTbl);
				break;
			case geTerm:
				//game event stop
				gameEventMode = geModeStop;
				break;
			case geLoop:
				//game event loop
				gameEventIndex = 0;
				gameEventMode = geModeNormal;
				gameEventWaitCnt = 0;
				gameEventWaitTime = 0;
				break;
			case geNextArea:
				//game event next area
				gameEventIndex = 0;
				gameEventMode = geModeNormal;
				gameEventWaitCnt = 0;
				gameEventWaitTime = 0;
				gameArea = gameArea + 1;
				break;
			case geNextStage:
				//game event next stage
				gameEventIndex = 0;
				gameEventMode = geModeNormal;
				gameEventWaitCnt = 0;
				gameEventWaitTime = 0;
				gameArea = gameArea1;
				gameStage = gameStage + 1;
				//stage result init
				stgGetStar = 0;
				stgUseContinue = false;
				stgLostPlayer = false;
				stgHaveDamage = false;
				stgUseBomb = false;
				break;
			case geWait:
				//game event wait process
				gameEventMode = geModeWait;
				gameEventWaitCnt = 0;
				gameEventWaitTime = evtTbl[gameEventIndex + 1];	//if value is -1 -> no release wait
				gameEventIndex = gameEventIndex + 2;
				break;
			default:
				//nop
				break;
			}
			break;
		case geModeStop:
			//nop
			break;
		default:
			break;
		}
	}

	//wait process
	private void waitProcess( ref int[] evtTbl ){
		if (gameEventWaitTime != -1) {	//no release wait?
			//wait cnt
			gameEventWaitCnt++;
			if (gameEventWaitCnt > gameEventWaitTime) {	//wait term?
				gameEventWaitCnt = 0;
				gameEventWaitTime = 0;
				gameEventMode = geModeNormal;
			}
		}
	}

	//screen mask process
	private void screenMaskProcess( ref int[] evtTbl ){
		int param1 = evtTbl [gameEventIndex + 1];
		switch (param1) {
		case geNoMask:
			src.setNoMask ();
			break;
		case geMask:
			src.setMask ();
			break;
		case geMaskFadeout:
			src.setFadeout ();
			break;
		case geMaskFadein:
			src.setFadein ();
			break;
		default:
			break;
		}
		gameEventIndex = gameEventIndex + 2;
	}

	//game display
	private void gameDisplayProcess( ref int[] evtTbl ){
		int param1 = evtTbl [gameEventIndex + 1];
		int param2 = 0x00;
		switch (param1) {
		case geInfoNoDisplay:
			dsc.infoNoDisplay ();
			gameEventIndex = gameEventIndex + 2;
			break;
		case geInfoDisplay:
			dsc.infoDisplay ();
			gameEventIndex = gameEventIndex + 2;
			break;
		case geInfoDisplayIn:
			dsc.infoDisplayIn ();
			gameEventIndex = gameEventIndex + 2;
			break;
		case geInfoDisplayOut:
			dsc.infoDisplayOut ();
			gameEventIndex = gameEventIndex + 2;
			break;
		case geEhNoDisplay:
			dsc.ehpNoDisplay ();
			gameEventIndex = gameEventIndex + 2;
			break;
		case geEhDisplay:
			param2 = evtTbl [gameEventIndex + 2];
			dsc.ehpDisplay (param2);
			gameEventIndex = gameEventIndex + 3;
			break;
		case geEhDisplayIn:
			param2 = evtTbl [gameEventIndex + 2];
			dsc.ehpDisplayIn (param2);
			gameEventIndex = gameEventIndex + 3;
			break;
		case geEhDisplayOut:
			dsc.ehpDisplayOut ();
			gameEventIndex = gameEventIndex + 2;
			break;
		case geMainMessageDisplay:
			param2 = evtTbl [gameEventIndex + 2];
			int param3 = evtTbl [gameEventIndex + 3];
			int param4 = evtTbl [gameEventIndex + 4];
			int param5 = evtTbl [gameEventIndex + 5];
			string st = "";
			switch (param2) {
			case geMainMessageWarning:
				st = "WARNING!!";
				break;
			case geMainMessageStageStart:
				st = "STAGE " + ((gameStage+1).ToString());
				break;
			case geMainMessageStageClear:
				if (gameStage == gameStageLast) {
					st = "ALL STAGE CLEAR!!";
				} else {
					st = "STAGE " + ((gameStage + 1).ToString ()) + " CLEAR!!";
				}
				break;
			case geMainMessageDelete:
			default:
				st = "";
				break;
			}
			dsc.setMainMessage ( st, param3, param4, param5 );
			gameEventIndex = gameEventIndex + 6;
			break;
		case geResultDisplayStart:
			dsc.startResultDisplay ();
			gameEventIndex = gameEventIndex + 2;
			break;
		case geEndingDisplayStart:
			dsc.startEndingDisplay ();
			gameEventIndex = gameEventIndex + 2;
			break;
		default:
			break;
		}
	}

	//game status
	private void gameStatusProcess( ref int[] evtTbl ){
		int param1 = evtTbl [gameEventIndex + 1];
		switch (param1) {
		case geDestroyGamePlayObjects:
			this.destroyGamePlayObjects();
			break;
		default:
			break;
		}
		gameEventIndex = gameEventIndex + 2;
	}
		
	//enemy process
	private void enemyProcess( ref int[] evtTbl ){
		while( evtTbl[gameEventIndex] == geEnemy ){
			//generate enemy
			this.generateEnemy( ref evtTbl );
		}
	}

	//player process
	private void playerProcess( ref int[] evtTbl ){
		int param1 = evtTbl [gameEventIndex + 1];
		GameObject go;
		switch (param1) {
		case geSetPlayerMode:
			//set player mode
			int param2 = evtTbl [gameEventIndex + 2];
			plc.setPlayerMode (param2);
			gameEventIndex = gameEventIndex + 3;
			break;
		case gePlayerBase100:
			//generate playerbase100
			int type = evtTbl [gameEventIndex + 2];
			go = Instantiate (playerBase100ControllerPrefab) as GameObject;
			go.GetComponent<playerBase100Controller> ().setInitStatus (type);
			gameEventIndex = gameEventIndex + 3;
			break;
		default:
			gameEventIndex = gameEventIndex + 3;
			break;
		}

	}

	//bgm process
	private void bgmProcess( ref int[] evtTbl ){
		int param1 = evtTbl [gameEventIndex + 1];
		int bgm;
		switch(param1){
		case geBgmPlay:
			//play bgm
			bgm = evtTbl [gameEventIndex + 2];
			int param2 = evtTbl [gameEventIndex + 3];
			bool force = (param2 == geBgmForce) ? true : false;
			sdc.playSound (bgm, force);
			gameEventIndex = gameEventIndex + 4;
			break;
		case geBgmStop:
			//stop bgm
			sdc.stopBgm();
			gameEventIndex = gameEventIndex + 2;
			break;
		case geBgmFadeout:
			//fadeout bgm
			sdc.fadeoutBgm();
			gameEventIndex = gameEventIndex + 2;
			break;
		default:
			break;
		}
	}

	//voice process
	private void voiceProcess( ref int[] evtTbl ){
		int param1 = evtTbl [gameEventIndex + 1];
		int vo;
		switch (param1) {
		case geVoicePlay:
			//play voice
			vo = evtTbl [gameEventIndex + 2];
			int param2 = evtTbl [gameEventIndex + 3];
			bool force = (param2 == geVoiceForce) ? true : false;
			sdc.playSound (vo, force);
			gameEventIndex = gameEventIndex + 4;
			break;
		case geVoiceStop:
			//stop voice
			sdc.stopVoice();
			gameEventIndex = gameEventIndex + 2;
			break;
		case geVoiceFadeout:
			//fadeout voice
			sdc.fadeoutVoice();
			gameEventIndex = gameEventIndex + 2;
			break;
		default:
			break;
		}
	}

	//se process
	private void seProcess( ref int[] evtTbl ){
		int param1 = evtTbl [gameEventIndex + 1];
		int se;
		switch (param1) {
		case geSePlay:
			//play se
			se = evtTbl [gameEventIndex + 2];
			int param2 = evtTbl [gameEventIndex + 3];
			bool force = (param2 == geSeForce) ? true : false;
			sdc.playSound (se, force);
			gameEventIndex = gameEventIndex + 4;
			break;
		case geSeStop:
			//stop se
			sdc.stopSe();
			gameEventIndex = gameEventIndex + 2;
			break;
		case geSeFadeout:
			//fadeout se
			sdc.fadeoutSe();
			gameEventIndex = gameEventIndex + 2;
			break;
		default:
			break;
		}
	}

	//map process
	private void mapProcess( ref int[] evtTbl ){
		//request scroll speed (set or target)
		float spd = 0.0f;
		int mode = evtTbl [gameEventIndex + 1];
		switch (mode) {
		case geMapStage:
			//map stage set
			mpc.setMapStage (evtTbl [gameEventIndex + 2]);
			gameEventIndex = gameEventIndex + 3;
			break;
		case geMapScrollTarget:
			//map scroll speed target set
			spd = ((float)(evtTbl [gameEventIndex + 2]))/100.0f;
			mpc.setTargetScrollSpeeed ( spd );
			gameEventIndex = gameEventIndex + 3;
			break;
		case geMapScrollSet:
			//map scroll speed set
			spd = ((float)(evtTbl [gameEventIndex + 2]))/100.0f;
			mpc.setScrollSpeeed (spd);
			gameEventIndex = gameEventIndex + 3;
			break;
		case geMapScrollMovexEnable:
			this.scrollMovexEnable = true;
			gameEventIndex = gameEventIndex + 2;
			break;
		case geMapScrollMovexDisable:
			this.scrollMovexEnable = false;
			gameEventIndex = gameEventIndex + 2;
			break;
		case geMapScrollMovexReset:
			this.setMapxReset ();
			gameEventIndex = gameEventIndex + 2;
			break;
		default:
			break;
		}
	}

	//title screen process
	private void titleProcess( ref int[] evtTbl ){
		int param1 = evtTbl [gameEventIndex + 1];
		switch( param1 ){
		case geTitleInit:
			this.titleInit ();
			dsc.clearVerDisplay ();
			gameEventIndex = gameEventIndex + 2;
			break;
		case geTitleFadein:
			dsc.setVerDisplay ();
			this.gameTitleColorMode = gameTitleColorModeFadein;
			gameEventIndex = gameEventIndex + 2;
			break;
		case geTitleFadeout:
			dsc.clearVerDisplay ();
			this.gameTitleColorMode = gameTitleColorModeFadeout;
			gameEventIndex = gameEventIndex + 2;
			break;
		case geTitleEnable:
			this.gameTitleEnable = true;
			gameEventIndex = gameEventIndex + 2;
			break;
		case geTitleDisable:
			this.gameTitleEnable = false;
			gameEventIndex = gameEventIndex + 2;
			break;
		default:
			break;
		}
	}

	//credit screen process
	private void creditProcess( ref int[] evtTbl ){
		int param1 = evtTbl [gameEventIndex + 1];
		switch( param1 ){
		case geCreditInit:
			this.creditInit ();
			gameEventIndex = gameEventIndex + 2;
			break;
		case geCreditFadein:
			this.gameCreditColorMode = gameCreditColorModeFadein;
			gameEventIndex = gameEventIndex + 2;
			break;
		case geCreditFadeout:
			this.gameCreditColorMode = gameCreditColorModeFadeout;
			gameEventIndex = gameEventIndex + 2;
			break;
		case geCreditEnable:
			this.gameCreditEnable = true;
			gameEventIndex = gameEventIndex + 2;
			break;
		case geCreditDisable:
			this.gameCreditEnable = false;
			gameEventIndex = gameEventIndex + 2;
			break;
		default:
			break;
		}
	}

	//debug menu process
	private void debugMenuProcess( ref int[] evtTbl ){
		int param1 = evtTbl [gameEventIndex + 1];
		switch( param1 ){
		case geDebugMenuInit:
			this.debugMenuInit ();
			gameEventIndex = gameEventIndex + 2;
			break;
		default:
			break;
		}
	}

	//game mode process
	private void gameModeProcess( ref int[] evtTbl ){
		int gm = evtTbl [gameEventIndex + 1];
		switch (gm) {
		case geGameModeTitleStart:
			this.setGameMode (gmTitleStart);
			break;
		case geGameModeTitle:
			this.setGameMode (gmTitle);
			break;
		case geGameModeCreditStart:
			this.setGameMode (gmCreditStart);
			break;
		case geGameModeCredit:
			this.setGameMode (gmCredit);
			break;
		case geGameModeTermCredit:
			this.setGameMode (gmTermCredit);
			break;
		case geGameModeDebugMenuStart:
			this.setGameMode (gmDebugMenuStart);
			break;
		case geGameModeDebugMenu:
			this.setGameMode (gmDebugMenu);
			break;
		case geGameModeGameStart:
			this.setGameMode (gmGameStart);
			break;
		case geGameModePlay:
			this.setGameMode (gmPlay);
			break;
		case geGameModePlayDebug:
			this.setGameMode (gmPlayDebug);
			break;
		case geGameModeGotoTitle:
			this.setGameMode (gmGotoTitle);
			break;
		default:
			break;
		}
	}

	//generate enemy
	private void generateEnemy(  ref int[] evtTbl  ){
		//generate enemy
		GameObject go;
		float x;
		float y;
		float dir;
		float tdir;
		float ddir;
		int ptn;
		float tx;
		float ty;
		int type;
		int item;
		float spd;
		int ene = evtTbl [gameEventIndex + 1];	//enemy type
		switch (ene) {
		case geEnemy50:
			//enemy50
			x = ((float)(evtTbl [gameEventIndex + 2])) / 100.0f;	//x
			y = ((float)(evtTbl [gameEventIndex + 3])) / 100.0f;	//y
			dir = ((float)evtTbl [gameEventIndex + 4]);	//direction
			spd = ((float)evtTbl [gameEventIndex + 5]);	//speed
			item = evtTbl [gameEventIndex + 6];	//item
			if( this.scrollMovexEnable == true ){	//scroll x enable?
				x = x + mpc.getMapxPos();	//map横位置補正
			}
			go = Instantiate (enemy50ControllerPrefab) as GameObject;
			go.GetComponent<enemy50Controller> ().setInitStatus (item, dir, spd, x, y);	//init
			gameEventIndex = gameEventIndex + 7;
			break;
		case geEnemy100:
			//enemy100
			x = ((float)(evtTbl [gameEventIndex + 2])) / 100.0f;	//x
			y = ((float)(evtTbl [gameEventIndex + 3])) / 100.0f;	//y
			dir = ((float)evtTbl [gameEventIndex + 4]);	//direction
			item = evtTbl [gameEventIndex + 5];	//item
			if( this.scrollMovexEnable == true ){	//scroll x enable?
				x = x + mpc.getMapxPos();	//map横位置補正
			}
			go = Instantiate (enemy100ControllerPrefab) as GameObject;
			go.GetComponent<enemy100Controller> ().setInitStatus (dir, item, x, y);	//init
			gameEventIndex = gameEventIndex + 6;
			break;
		case geEnemy110:
			//enemy110
			x = ((float)(evtTbl [gameEventIndex + 2])) / 100.0f;	//x
			y = ((float)(evtTbl [gameEventIndex + 3])) / 100.0f;	//y
			tdir = ((float)evtTbl [gameEventIndex + 4]);	//move direction
			ddir = ((float)evtTbl [gameEventIndex + 5]);	//disp direction
			item = evtTbl [gameEventIndex + 6];	//item
			if( this.scrollMovexEnable == true ){	//scroll x enable?
				x = x + mpc.getMapxPos();	//map横位置補正
			}
			go = Instantiate (enemy110ControllerPrefab) as GameObject;
			go.GetComponent<enemy110Controller> ().setInitStatus (tdir, ddir, item, x, y);	//init
			gameEventIndex = gameEventIndex + 7;
			break;
		case geEnemy120:
			//enemy120
			x = ((float)(evtTbl [gameEventIndex + 2])) / 100.0f;	//x
			y = ((float)(evtTbl [gameEventIndex + 3])) / 100.0f;	//y
			tdir = ((float)evtTbl [gameEventIndex + 4]);	//direction
			item = evtTbl [gameEventIndex + 5];	//item
			if( this.scrollMovexEnable == true ){	//scroll x enable?
				x = x + mpc.getMapxPos();	//map横位置補正
			}
			go = Instantiate (enemy120ControllerPrefab) as GameObject;
			go.GetComponent<enemy120Controller>().setInitStatus (tdir, item, x, y);	//init
			gameEventIndex = gameEventIndex + 6;
			break;
		case geEnemy130:
			//enemy130
			x = ((float)(evtTbl [gameEventIndex + 2])) / 100.0f;	//x
			y = ((float)(evtTbl [gameEventIndex + 3])) / 100.0f;	//y
			tdir = ((float)evtTbl [gameEventIndex + 4]);	//direction
			ptn = (evtTbl [gameEventIndex + 5]);	//movement pattern
			tx = ((float)(evtTbl [gameEventIndex + 6])) / 100.0f;	//target x
			ty = ((float)(evtTbl [gameEventIndex + 7])) / 100.0f;	//target y
			item = evtTbl [gameEventIndex + 8];	//item
			if( this.scrollMovexEnable == true ){	//scroll x enable?
				x = x + mpc.getMapxPos();	//map横位置補正
			}
			go = Instantiate (enemy130ControllerPrefab) as GameObject;
			go.GetComponent<enemy130Controller> ().setInitStatus (tdir, ptn, tx, ty, item, x, y);	//init
			gameEventIndex = gameEventIndex + 9;
			break;
		case geEnemy150:
			//enemy150
 			x = ((float)(evtTbl [gameEventIndex + 2])) / 100.0f;	//x
			y = ((float)(evtTbl [gameEventIndex + 3])) / 100.0f;	//y
			item = evtTbl [gameEventIndex + 4];	//item
			if( this.scrollMovexEnable == true ){	//scroll x enable?
				x = x + mpc.getMapxPos();	//map横位置補正
			}
			go = Instantiate (enemy150ControllerPrefab) as GameObject;
			go.GetComponent<enemy150Controller> ().setInitStatus (item, x, y);	//init
			gameEventIndex = gameEventIndex + 5;
			break;
		case geEnemy160:
			//enemy160
			x = ((float)(evtTbl [gameEventIndex + 2])) / 100.0f;	//x
			y = ((float)(evtTbl [gameEventIndex + 3])) / 100.0f;	//y
			tdir = ((float)evtTbl [gameEventIndex + 4]);	//direction
			type = evtTbl [gameEventIndex + 5];	//type
			item = evtTbl [gameEventIndex + 6];	//item
			if( this.scrollMovexEnable == true ){	//scroll x enable?
				x = x + mpc.getMapxPos();	//map横位置補正
			}
			go = Instantiate (enemy160ControllerPrefab) as GameObject;
			go.GetComponent<enemy160Controller> ().setInitStatus (item, x, y, tdir, type);	//init
			gameEventIndex = gameEventIndex + 7;
			break;
		case geEnemy170:
			//enemy170
			x = ((float)(evtTbl [gameEventIndex + 2])) / 100.0f;	//x
			y = ((float)(evtTbl [gameEventIndex + 3])) / 100.0f;	//y
			dir = ((float)evtTbl [gameEventIndex + 4]);	//direction
			item = evtTbl [gameEventIndex + 5];	//item
			if( this.scrollMovexEnable == true ){	//scroll x enable?
				x = x + mpc.getMapxPos();	//map横位置補正
			}
			go = Instantiate (enemy170ControllerPrefab) as GameObject;
			go.GetComponent<enemy170Controller> ().setInitStatus (dir, item, x, y);	//init
			gameEventIndex = gameEventIndex + 6;
			break;
		case geEnemy180:
			//enemy180
			x = ((float)(evtTbl [gameEventIndex + 2])) / 100.0f;	//x
			y = ((float)(evtTbl [gameEventIndex + 3])) / 100.0f;	//y
			dir = ((float)evtTbl [gameEventIndex + 4]);	//direction
			item = evtTbl [gameEventIndex + 5];	//item
			if( this.scrollMovexEnable == true ){	//scroll x enable?
				x = x + mpc.getMapxPos();	//map横位置補正
			}
			go = Instantiate (enemy180ControllerPrefab) as GameObject;
			go.GetComponent<enemy180Controller> ().setInitStatus (dir, item, x, y);	//init
			gameEventIndex = gameEventIndex + 6;
			break;
		case geEnemy190:
			//enemy190
			x = ((float)(evtTbl [gameEventIndex + 2])) / 100.0f;	//x
			y = ((float)(evtTbl [gameEventIndex + 3])) / 100.0f;	//y
			dir = ((float)evtTbl [gameEventIndex + 4]);	//direction
			item = evtTbl [gameEventIndex + 5];	//item
			if( this.scrollMovexEnable == true ){	//scroll x enable?
				x = x + mpc.getMapxPos();	//map横位置補正
			}
			go = Instantiate (enemy190ControllerPrefab) as GameObject;
			go.GetComponent<enemy190Controller> ().setInitStatus (dir, item, x, y);	//init
			gameEventIndex = gameEventIndex + 6;
			break;
		case geEnemy200:
			//enemy200
			x = ((float)(evtTbl [gameEventIndex + 2])) / 100.0f;	//x
			y = ((float)(evtTbl [gameEventIndex + 3])) / 100.0f;	//y
			dir = ((float)evtTbl [gameEventIndex + 4]);	//direction
			item = evtTbl [gameEventIndex + 5];	//item
			if( this.scrollMovexEnable == true ){	//scroll x enable?
				x = x + mpc.getMapxPos();	//map横位置補正
			}
			go = Instantiate (enemy200ControllerPrefab) as GameObject;
			go.GetComponent<enemy200Controller> ().setInitStatus (dir, item, x, y);	//init
			gameEventIndex = gameEventIndex + 6;
			break;
		case geEnemy210:
			//enemy210
			x = ((float)(evtTbl [gameEventIndex + 2])) / 100.0f;	//x
			y = ((float)(evtTbl [gameEventIndex + 3])) / 100.0f;	//y
			tdir = ((float)evtTbl [gameEventIndex + 4]);	//move direction
			ddir = ((float)evtTbl [gameEventIndex + 5]);	//disp direction
			item = evtTbl [gameEventIndex + 6];	//item
			if( this.scrollMovexEnable == true ){	//scroll x enable?
				x = x + mpc.getMapxPos();	//map横位置補正
			}
			go = Instantiate (enemy210ControllerPrefab) as GameObject;
			go.GetComponent<enemy210Controller> ().setInitStatus (tdir, ddir, item, x, y);	//init
			gameEventIndex = gameEventIndex + 7;
			break;
		case geEnemy220:
			//enemy220
			x = ((float)(evtTbl [gameEventIndex + 2])) / 100.0f;	//x
			y = ((float)(evtTbl [gameEventIndex + 3])) / 100.0f;	//y
			dir = ((float)evtTbl [gameEventIndex + 4]);	//direction
			item = evtTbl [gameEventIndex + 5];	//item
			if( this.scrollMovexEnable == true ){	//scroll x enable?
				x = x + mpc.getMapxPos();	//map横位置補正
			}
			go = Instantiate (enemy220ControllerPrefab) as GameObject;
			go.GetComponent<enemy220Controller> ().setInitStatus (dir, item, x, y);	//init
			gameEventIndex = gameEventIndex + 6;
			break;
		case geEnemy230:
			//enemy230
			x = ((float)(evtTbl [gameEventIndex + 2])) / 100.0f;	//x
			y = ((float)(evtTbl [gameEventIndex + 3])) / 100.0f;	//y
			tdir = ((float)evtTbl [gameEventIndex + 4]);	//move direction
			ddir = ((float)evtTbl [gameEventIndex + 5]);	//disp direction
			item = evtTbl [gameEventIndex + 6];	//item
			if( this.scrollMovexEnable == true ){	//scroll x enable?
				x = x + mpc.getMapxPos();	//map横位置補正
			}
			go = Instantiate (enemy230ControllerPrefab) as GameObject;
			go.GetComponent<enemy230Controller> ().setInitStatus (tdir, ddir, item, x, y);	//init
			gameEventIndex = gameEventIndex + 7;
			break;
		case geEnemy240:
			//enemy240
			x = ((float)(evtTbl [gameEventIndex + 2])) / 100.0f;	//x
			y = ((float)(evtTbl [gameEventIndex + 3])) / 100.0f;	//y
			item = evtTbl [gameEventIndex + 4];	//item
			if( this.scrollMovexEnable == true ){	//scroll x enable?
				x = x + mpc.getMapxPos();	//map横位置補正
			}
			go = Instantiate (enemy240ControllerPrefab) as GameObject;
			go.GetComponent<enemy240Controller> ().setInitStatus (item, x, y);	//init
			gameEventIndex = gameEventIndex + 5;
			break;
		case geEnemy300:
			//enemy300
			type = evtTbl [gameEventIndex + 2];	//type
			item = evtTbl [gameEventIndex + 3];	//item
			go = Instantiate (enemy300ControllerPrefab) as GameObject;
			go.GetComponent<enemy300Controller> ().setInitStatus (type, item);	//init
			gameEventIndex = gameEventIndex + 4;
			break;
		case geEnemy500:
			//enemy500
			item = evtTbl [gameEventIndex + 2];	//item
			go = Instantiate (enemy500ControllerPrefab) as GameObject;
			go.GetComponent<enemy500Controller> ().setInitStatus (item);	//init
			gameEventIndex = gameEventIndex + 3;
			break;
		case geEnemy515:
			//enemy515
			item = evtTbl [gameEventIndex + 2];	//item
			go = Instantiate (enemy515ControllerPrefab) as GameObject;
			go.GetComponent<enemy515Controller> ().setInitStatus (item);	//init
			gameEventIndex = gameEventIndex + 3;
			break;
		case geEnemy520:
			//enemy520
			item = evtTbl [gameEventIndex + 2];	//item
			go = Instantiate (enemy520ControllerPrefab) as GameObject;
			go.GetComponent<enemy520Controller> ().setInitStatus (item);	//init
			gameEventIndex = gameEventIndex + 3;
			break;
		case geEnemy530:
			//enemy530
			item = evtTbl [gameEventIndex + 2];	//item
			go = Instantiate (enemy530ControllerPrefab) as GameObject;
			go.GetComponent<enemy530Controller> ().setInitStatus (item);	//init
			gameEventIndex = gameEventIndex + 3;
			break;
		default:
			gameEventIndex = gameEventIndex + 2;
			break;
		}
	}

	//set game mode
	private void setGameMode( int gm ){
		//set game mode
		gameMode = gm;
		//init for game event process
		gameEventIndex = 0;
		gameEventMode = geModeNormal;
		gameEventWaitCnt = 0;
		gameEventWaitTime = 0;
		//mode init
		switch (gm) {
		case gmTitleStart:
			//game mode title start
			gameTitleEnable = false;
			break;
		case gmTitle:
			//game mode title
			break;
		case gmCreditStart:
			//game mode credit start
			gameCreditEnable = false;
			break;
		case gmCredit:
			//game mode credit
			break;
		case gmTermCredit:
			//game mode term credit
			break;
		case gmDebugMenuStart:
			//game mode debug menu start
			break;
		case gmDebugMenu:
			//game mode debug menu
			break;
		case gmGameStart:
			//game mode game start
			gameStage = gameStage1;
			gameArea = gameArea1;
			break;
		case gmPlay:
			//game mode play
			break;
		case gmPlayDebug:
			//game mode play debug
			//debug mode start position
			switch (debug_startposition) {
			case debugSpos1:	//normal stage1 start
				gameStage = gameStage1;
				gameArea = gameArea1; 
				break;
			case debugSpos2:	//start stage1_1 (no base)
				gameStage = gameStage1;
				gameArea = gameArea2;
				break;
			case debugSpos3:	//start stage1 middle boss
				gameStage = gameStage1;
				gameArea = gameArea3;
				break;
			case debugSpos4:	//start stage1_2
				gameStage = gameStage1;
				gameArea = gameArea4;
				break;
			case debugSpos5:	//start stage1 boss
				gameStage = gameStage1;
				gameArea = gameArea5;
				break;
			case debugSpos6:	//start stage1_c (boss clear)
				gameStage = gameStage1;
				gameArea = gameArea6;
				break;
			case debugSpos7:	//start stage2_1
				gameStage = gameStage2;
				gameArea = gameArea1;
				break;
			case debugSpos8:	//start stage2 middile boss
				gameStage = gameStage2;
				gameArea = gameArea2;
				break;
			case debugSpos9:	//start stage2_2
				gameStage = gameStage2;
				gameArea = gameArea3;
				break;
			case debugSpos10:	//start stage2 boss
				gameStage = gameStage2;
				gameArea = gameArea4;
				break;
			case debugSpos11:	//start stage2_c (boss clear) 
				gameStage = gameStage2;
				gameArea = gameArea5;
				break;
			case debugSpos12:	//start stage3_1
				gameStage = gameStage3;
				gameArea = gameArea1;
				break;
			case debugSpos13:	//start stage3 middile boss
				gameStage = gameStage3;
				gameArea = gameArea2;
				break;
			case debugSpos14:	//start stage3_2
				gameStage = gameStage3;
				gameArea = gameArea3;
				break;
			case debugSpos15:	//start stage3 boss 1
				gameStage = gameStage3;
				gameArea = gameArea4;
				break;
			case debugSpos16:	//start stage3 boss 2
				gameStage = gameStage3;
				gameArea = gameArea5;
				break;
			case debugSpos17:	//start stage3_c (boss clear) 
				gameStage = gameStage3;
				gameArea = gameArea6;
				break;
			default:	//for safe
				gameStage = gameStage1;
				gameArea = gameArea1; 
				break;
			}
			//start position process 
			if (debug_startposition != debugSpos1) {
				//map scroll x reset
				this.setMapxReset();
				//player mode rebirth set
				plc.setPlayerMode ( plc.playerModeRebirth );
				//display info in
				dsc.infoDisplayIn ();
			}
			//stage result init
			stgGetStar = 0;
			stgUseContinue = false;
			stgLostPlayer = false;
			stgHaveDamage = false;
			stgUseBomb = false;
			//object num reset
			starObjNum = 0;
			starGetMessageObjNum = 0;
			break;
		case gmGotoTitle:
			//game mode goto title
			break;
		default:
			break;
		}
	}

	//destroy game play objects
	private void destroyGamePlayObjects(){
		//enemies
		GameObject[] enemmies = GameObject.FindGameObjectsWithTag ("enemy");
		for (int i = 0; i < enemmies.Length; i++) {
			GameObject.Destroy (enemmies [i]);
			this.decObj ();
		}
		//enemies low
		GameObject[] lowenemmies = GameObject.FindGameObjectsWithTag ("enemyLow");
		for (int i = 0; i < lowenemmies.Length; i++) {
			GameObject.Destroy (lowenemmies [i]);
			this.decObj ();
		}
		//ground enemies
		GameObject[] genemmies = GameObject.FindGameObjectsWithTag ("groundEnemy");
		for (int i = 0; i < genemmies.Length; i++) {
			GameObject.Destroy (genemmies [i]);
			this.decObj ();
		}
		//unavailable enemies
		GameObject[] uaenemmies = GameObject.FindGameObjectsWithTag ("unavailableEnemy");
		for (int i = 0; i < uaenemmies.Length; i++) {
			GameObject.Destroy (uaenemmies [i]);
			this.decObj ();
		}
		//enemy bullets
		GameObject[] ebullets = GameObject.FindGameObjectsWithTag ("enemyBullet");
		for (int i = 0; i < ebullets.Length; i++) {
			GameObject.Destroy (ebullets [i]);
			this.decObj ();
		}
		//player base
		GameObject[] pbase = GameObject.FindGameObjectsWithTag ("playerBase");
		for (int i = 0; i < pbase.Length; i++) {
			GameObject.Destroy (pbase [i]);
			this.decObj ();
		}
		//player bullets
		GameObject[] pbullets = GameObject.FindGameObjectsWithTag ("playerBullet");
		for (int i = 0; i < pbullets.Length; i++) {
			GameObject.Destroy (pbullets [i]);
			this.decObj ();
		}
		//player laser 1
		GameObject[] plasers1 = GameObject.FindGameObjectsWithTag ("playerLaser1");
		for (int i = 0; i < plasers1.Length; i++) {
			GameObject.Destroy (plasers1 [i]);
			this.decObj ();
		}
		//player laser 2
		GameObject[] plasers2 = GameObject.FindGameObjectsWithTag ("playerLaser2");
		for (int i = 0; i < plasers2.Length; i++) {
			GameObject.Destroy (plasers2 [i]);
			this.decObj ();
		}
		//player missile 1
		GameObject[] pmissile1 = GameObject.FindGameObjectsWithTag ("playerMissile1");
		for (int i = 0; i < pmissile1.Length; i++) {
			GameObject.Destroy (pmissile1 [i]);
			this.decObj ();
		}
		//player missile 2
		GameObject[] pmissile2 = GameObject.FindGameObjectsWithTag ("playerMissile2");
		for (int i = 0; i < pmissile2.Length; i++) {
			GameObject.Destroy (pmissile2 [i]);
			this.decObj ();
		}
		//missile bomb 1
		GameObject[] mbombs1 = GameObject.FindGameObjectsWithTag ("missileBomb1");
		for (int i = 0; i < mbombs1.Length; i++) {
			GameObject.Destroy (mbombs1 [i]);
			this.decObj ();
		}
		//missile bomb 2
		GameObject[] mbombs2 = GameObject.FindGameObjectsWithTag ("missileBomb2");
		for (int i = 0; i < mbombs2.Length; i++) {
			GameObject.Destroy (mbombs2 [i]);
			this.decObj ();
		}
		//bomb 1
		GameObject[] bombs1 = GameObject.FindGameObjectsWithTag ("bomb1");
		for (int i = 0; i < bombs1.Length; i++) {
			GameObject.Destroy (bombs1 [i]);
			this.decObj ();
		}
		//bomb 2
		GameObject[] bombs2 = GameObject.FindGameObjectsWithTag ("bomb2");
		for (int i = 0; i < bombs2.Length; i++) {
			GameObject.Destroy (bombs2 [i]);
			this.decObj ();
		}
		//bomb laser
		GameObject[] bombl = GameObject.FindGameObjectsWithTag ("bombLaser");
		for (int i = 0; i < bombl.Length; i++) {
			GameObject.Destroy (bombl [i]);
			this.decObj ();
		}
		//wipe 1
		GameObject[] wipe1 = GameObject.FindGameObjectsWithTag ("wipe1");
		for (int i = 0; i < wipe1.Length; i++) {
			GameObject.Destroy (wipe1 [i]);
			this.decObj ();
		}
		//wipe 2
		GameObject[] wipe2 = GameObject.FindGameObjectsWithTag ("wipe2");
		for (int i = 0; i < wipe2.Length; i++) {
			GameObject.Destroy (wipe2 [i]);
			this.decObj ();
		}
		//power items
		GameObject[] pitems = GameObject.FindGameObjectsWithTag ("powerItem");
		for (int i = 0; i < pitems.Length; i++) {
			GameObject.Destroy (pitems [i]);
			this.decObj ();
		}
		//score items
		GameObject[] sitems = GameObject.FindGameObjectsWithTag ("scoreItem");
		for (int i = 0; i < sitems.Length; i++) {
			GameObject.Destroy (sitems [i]);
			this.decObj ();
		}
		//effects
		GameObject[] effects = GameObject.FindGameObjectsWithTag ("effect");
		for (int i = 0; i < effects.Length; i++) {
			GameObject.Destroy (effects [i]);
			this.decObj ();
		}
		//sub message
		GameObject[] submessage = GameObject.FindGameObjectsWithTag ("subMessage");
		for (int i = 0; i < submessage.Length; i++) {
			GameObject.Destroy (submessage [i]);
			this.decObj ();
		}
		//object num reset
		starObjNum = 0;
		starGetMessageObjNum = 0;
	}

	//title init
	private void titleInit(){
		//game mode title
		//fadein/out reset
		gameTitleColorMode = gameTitleColorModeNormal;
		//title operate disable
		gameTitleEnable = false;
		//generate title screen
		dsc.generateTitleScreen ();
		//set select color
		dsc.setGameLevelColor ();
		dsc.setPlayerSpeedColor ();
		dsc.setSubWeaponColor ();
		dsc.setPlayerTypeColor ();
		dsc.setDescriptionText ();
	}

	//credit init
	private void creditInit(){
		//game mode credit
		//fadein/out reset
		gameCreditColorMode = gameCreditColorModeNormal;
		//credit operate disable
		gameCreditEnable = false;
		//generate credit screen
		dsc.generateCreditScreen ();
	}

	//debug menu init
	private void debugMenuInit(){
		//game mode debug menu
		//generate debug menu list screen
		dsc.generateDebugMenuListScreen ();
	}

	//game status init
	private void gameStatusInit(){
		gameScore = 0;
		dsc.dispScore (gameScore);
		starNum = 0;
		dsc.dispStar (starNum);
		continueCnt = 0;
		pupVoiceCnt = 3;
		counterBulletCnt = 0;
	}

	//status result init
	private void stageResultInit(){
		stgGetStar = 0;
		stgUseContinue = false;
		stgLostPlayer = false;
		stgHaveDamage = false;
		stgUseBomb = false;
	}

	//generate enemy bullet num max check
	private bool maxCheckEnemyBullet(){
		if (this.objNum >= 580) {
			#if UNITY_EDITOR
			Debug.Log ("enemy bullet max");
			#endif
			return true;
		}
		return false;
	}

	//enemy damage effect num max check
	private bool maxCheckEnemyDamageEffect(){
		if (this.objNum >= 380) {	//480
			#if UNITY_EDITOR
			Debug.Log ("enemy damage effects max");
			#endif
			return true;
		}
		return false;
	}

	//star obj num max check
	private bool maxCheckStarObj(){
		if ( (this.starObjNum >= 58) || (this.objNum >= 450) ) {
			#if UNITY_EDITOR
			Debug.Log ("star obj num max");
			#endif
			return true;
		}
		return false;
	}

	//star get message obj num max check
	private bool maxCheckStarGetMessageObj(){
		if ( (this.starGetMessageObjNum >= 8) || (this.objNum >= 400) ) {
			#if UNITY_EDITOR
			Debug.Log ("star get message obj num max");
			#endif
			return true;
		}
		return false;
	}

	//public
	//release game event wait
	public void releaseWait(){
		if (gameEventWaitTime == -1) {
			//release wait
			gameEventWaitCnt = 0;
			gameEventWaitTime = 0;
			gameEventMode = geModeNormal;
		}
	}

	//play sound
	public void playSound( int sound, bool force = true ){
		sdc.playSound (sound, force);
	}

	//stop bgm
	public void stopBgm(){
		sdc.stopBgm ();
	}

	//fadeout bgm
	public void fadeoutBgm(){
		sdc.fadeoutBgm ();
	}

	//stop loop se
	public void stopLoopSe(){
		sdc.stopLoopSe ();
	}

	//fadeout loop se
	public void fadeoutLoopSe(){
		sdc.fadeoutLoopSe ();
	}

	//term title fadein
	public void termTitleFadein(){
		gameTitleColorMode = gameTitleColorModeNormal;
		if (this.gameEventWaitTime == -1) {
			this.releaseWait ();
		}
	}

	//term title fadeout
	public void termTitleFadeout(){
		gameTitleColorMode = gameTitleColorModeNormal;
		if (this.gameEventWaitTime == -1) {
			this.releaseWait ();
		}
	}

	//term credit fadein
	public void termCreditFadein(){
		gameCreditColorMode = gameCreditColorModeNormal;
		if (this.gameEventWaitTime == -1) {
			this.releaseWait ();
		}
	}

	//term credit fadeout
	public void termCreditFadeout(){
		gameCreditColorMode = gameCreditColorModeNormal;
		if (this.gameEventWaitTime == -1) {
			this.releaseWait ();
		}
	}

	//generate counter bullet
	public void generateCounterBullet( int bulletType=0, float x=0, float y=0, float xoffset=0, float yoffset=0, float xs=1.0f, float ys=1.0f ){
		float xdistance,ydistance;
		Vector2 ppos = plc.getPlayerPos ();
		xdistance = (ppos.x) - (x + xoffset);	//player,enemy x distance
		ydistance = (ppos.y) - (y + yoffset);	//player,enemy y distance
		if ( ( (Mathf.Abs(xdistance)) + (Mathf.Abs(ydistance)) ) >= 5.5f ) {
			if (counterBulletTable [counterBulletCnt] == 1) {
				this.generateEnemyBullet100 (bulletType, x, y, xoffset, yoffset, 0.82f, 0.82f);
			}
			counterBulletCnt++;
			if (counterBulletCnt >= counterBulletTable.Length) {
				counterBulletCnt = 0;
			}
		}
	}

	//generate enemy bullet100
	public void generateEnemyBullet100( int bulletMode=0, float x=0, float y=0, float xoffset=0, float yoffset=0, float xs=1.0f, float ys=1.0f ){
		//max check
		if (this.maxCheckEnemyBullet() == true) {
			return;
		}
		//bullet
		//direction
		switch( bulletMode){
		case 0:
			//to player x,y
			float xdistance, ydistance;
			float direction;
			Vector2 ppos = plc.getPlayerPos ();
			xdistance = (ppos.x) - (x + xoffset);	//player,enemy x distance
			ydistance = (ppos.y) - (y + yoffset);	//player,enemy y distance
			if ((xdistance == 0) && (ydistance == 0)) {	//for zero exception
				xdistance = 0.0001f;
			}
			direction = Mathf.Atan2 (ydistance, xdistance);	//distance -> direction
			xs = Mathf.Cos(direction) * 1.0f * xs;	//x speed * base speed
			ys = Mathf.Sin(direction) * 1.0f * ys;	//y speed * base speed
			break;
		case 1:
			//set xs,ys
			break;
		default:
			break;
		}

		//genarate
		GameObject go = Instantiate (enemyBullet100ControllerPrefab) as GameObject;
		go.GetComponent<enemyBullet100Controller> ().setInitStatus (xs, ys, (x + xoffset), (y + yoffset) );
	}

	//generate enemy bullet110
	public void generateEnemyBullet110( int bulletMode=0, float x=0, float y=0, float xoffset=0, float yoffset=0, float xs=1.0f, float ys=1.0f ){
		//max check
		if (this.maxCheckEnemyBullet() == true) {
			return;
		}
		//bullet
		//direction
		switch( bulletMode){
		case 0:
			//to player x,y
			float xdistance,ydistance;
			float direction;
			Vector2 ppos = plc.getPlayerPos ();
			xdistance = (ppos.x) - (x + xoffset);	//player,enemy x distance
			ydistance = (ppos.y) - (y + yoffset);	//player,enemy y distance
			if ((xdistance == 0) && (ydistance == 0)) {	//for zero exception
				xdistance = 0.0001f;
			}
			direction = Mathf.Atan2 (ydistance, xdistance);	//distance -> direction
			xs = Mathf.Cos(direction) * 1.0f * xs;	//x speed * base speed
			ys = Mathf.Sin(direction) * 1.0f * ys;	//y speed * base speed
			break;
		case 1:
			//set xs,ys
			break;
		default:
			break;
		}

		//genarate
		GameObject go = Instantiate (enemyBullet110ControllerPrefab) as GameObject;
		go.GetComponent<enemyBullet110Controller> ().setInitStatus (xs, ys, (x + xoffset), (y + yoffset) );
	}

	//generate enemy bullet120
	public void generateEnemyBullet120( float dir, float spd, float x, float y, float xoffset=0, float yoffset=0 ){
		//max check
		if (this.maxCheckEnemyBullet() == true) {
			return;
		}
		//genarate
		GameObject go = Instantiate (enemyBullet120ControllerPrefab) as GameObject;
		go.GetComponent<enemyBullet120Controller> ().setInitStatus (dir, spd, (x + xoffset), (y + yoffset) );
	}

	//generate enemy bullet130
	public void generateEnemyBullet130( int bulletMode=0, float x=0, float y=0, float xoffset=0, float yoffset=0, float xs=1.0f, float ys=1.0f ){
		//max check
		if (this.maxCheckEnemyBullet() == true) {
			return;
		}
		//bullet
		//direction
		switch( bulletMode){
		case 0:
			//to player x,y
			float xdistance, ydistance;
			float direction;
			Vector2 ppos = plc.getPlayerPos ();
			xdistance = (ppos.x) - (x + xoffset);	//player,enemy x distance
			ydistance = (ppos.y) - (y + yoffset);	//player,enemy y distance
			if ((xdistance == 0) && (ydistance == 0)) {	//for zero exception
				xdistance = 0.0001f;
			}
			direction = Mathf.Atan2 (ydistance, xdistance);	//distance -> direction
			xs = Mathf.Cos(direction) * 1.0f * xs;	//x speed * base speed
			ys = Mathf.Sin(direction) * 1.0f * ys;	//y speed * base speed
			break;
		case 1:
			//set xs,ys
			break;
		default:
			break;
		}

		//genarate
		GameObject go = Instantiate (enemyBullet130ControllerPrefab) as GameObject;
		go.GetComponent<enemyBullet130Controller> ().setInitStatus (xs, ys, (x + xoffset), (y + yoffset) );

	}

	//generate enemy50
	public void generateEnemy50( float dir, float spd, float x, float y, int item=-1, float xoffset=0, float yoffset=0 ){
		//max check
		if (this.maxCheckEnemyBullet() == true) {
			return;
		}
		if( this.scrollMovexEnable == true ){	//scroll x enable?
			x = x + mpc.getMapxPos();	//map横位置補正
		}
		GameObject go = Instantiate (enemy50ControllerPrefab) as GameObject;
		go.GetComponent<enemy50Controller> ().setInitStatus (item, dir, spd, (x+xoffset), (y+yoffset));	//init
	}

	//generate enemy150
	public void generateEnemy150( float x, float y ,int item = geItemNone, int type=0 ){
		if( this.scrollMovexEnable == true ){	//scroll x enable?
			x = x + mpc.getMapxPos();	//map横位置補正
		}
		GameObject go = Instantiate (enemy150ControllerPrefab) as GameObject;
		go.GetComponent<enemy150Controller> ().setInitStatus (item, x, y, type);	//init
	}

	//generate enemy damage effect
	public void generateEnemyDamageEffect( float x, float y ){
		//max check
		if (this.maxCheckEnemyDamageEffect() == true) {
			return;
		}
		//genarate
		GameObject go = Instantiate (damageEnemyControllerPrefab) as GameObject;
		go.GetComponent<damageEnemyController> ().setInitStatus (x, y);
	}

	//generate player damage effect
	public void generatePlayerDamageEffect( float x, float y ){
		//genarate
		GameObject go = Instantiate (damagePlayerControllerPrefab) as GameObject;
		go.GetComponent<damagePlayerController> ().setInitStatus (x, y);
	}

	//generate explosion middle effect
	public void generateExplosionMiddleEffect( float x, float y, float xx = 0.0f, float yy = 0.0f ){
		//genarate
		GameObject go = Instantiate (explosion100ControllerPrefab) as GameObject;
		go.GetComponent<explosion100Controller> ().setInitStatus (x, y, xx, yy);
	}

	//generate explosion large effect
	public void generateExplosionLargeEffect( float x, float y ){
		//genarate
		GameObject go = Instantiate (explosion110ControllerPrefab) as GameObject;
		go.GetComponent<explosion110Controller> ().setInitStatus (x, y);
	}

	//generate explosion big effect
	public void generateExplosionBigEffect( float x, float y ){
		//genarate
		GameObject go = Instantiate (explosion120ControllerPrefab) as GameObject;
		go.GetComponent<explosion120Controller> ().setInitStatus (x, y);
	}

	//generate groud explosion effect
	public void generateGroundExplosionEffect( float x, float y ){
		//genarate
		GameObject go = Instantiate (explosion130ControllerPrefab) as GameObject;
		go.GetComponent<explosion130Controller> ().setInitStatus (x, y);
	}

	//generate burner100 effect
	public void generateBurner100Effect( float x, float y, float xspd=0.0f, float yspd=0.0f, float xscl=1.6f, float yscl=1.6f ){
		//genarate
		GameObject go = Instantiate (burner100ControllerPrefab) as GameObject;
		go.GetComponent<burner100Controller> ().setInitStatus (xspd, yspd, x, y, xscl, yscl);
	}

	//generate powerup100
	public void generatePowerup100( int type, float x, float y ){
		//max check
		if ((type == puType_score) && (this.maxCheckStarObj() == true)) {
			return;
		}
		//genarate
		GameObject go = Instantiate (powerup100ControllerPrefab) as GameObject;
		go.GetComponent<powerup100Controller> ().setInitStatus( type, subWeapon, x, y );

		//同フレームでincするためここで行う
		//star obj num inc
		if (type == this.puType_score) {
			this.incStarObjNum ();
		}

		//voice
		if (type != puType_score) {
			if (pupVoiceCnt >= 1) {
				this.playSound (vo130);
			}
			pupVoiceCnt--;
		}
	}

	//generate wipe1
	public void generateWipe1(){
		//genarate
		GameObject go = Instantiate (wipe1ControllerPrefab) as GameObject;
	}

	//generate wipe2
	public void generateWipe2(){
		//genarate
		GameObject go = Instantiate (wipe2ControllerPrefab) as GameObject;
	}

	//generate screen shake effect
	public void generateScreenShakeEffect( int num = 5 ){
		this.src.setScreenShakeEffect (num);
	}

	//generate flash effect
	public void generateScreenFlashEffect(){
		this.src.setFlashEffect ();
	}

	//display enemy hitpoint
	public void dispEnemyHitPoint( int hp, int hpbase ){
		dsc.dispEhp (hp, hpbase);
	}

	//display main message
	public void dispMainMessage( string mes, int mode = 0, int fsize = 26, int time=50, int pauseonoff = 0 ){
		dsc.setMainMessage (mes, mode, fsize, time, pauseonoff);
	}

	//display sub message
	public void dispSubMessage( float wx, float wy, float xx, float yy, string mes, int cl = 0, bool getstar = false ){
		//max check
		if ((getstar == true) && (this.maxCheckStarGetMessageObj() == true)) {
			return;
		}
		dsc.setSubMessage (wx, wy, xx, yy, mes, cl, getstar);
	}

	//display pause mask on
	public void setPauseMaskOn(){
		this.pauseMask = true;
		src.setPauseMaskOn ();
	}

	//display pause mask off
	public void setPauseMaskOff(){
		this.pauseMask = false;
		src.setPauseMaskOff ();
	}

	//tap pause button
	public void tapPauseButton(){
		if (gameOver == false) {
			if (gamePause == false) {
				//pause
				sdc.pauseBgm ();
				sdc.pauseSe ();
				sdc.pauseVoice ();
				this.playSound (se_ts100);
				gamePause = true;
				this.timescale_pauseBackup = Time.timeScale;
				Time.timeScale = 0.0f;
				this.pauseMask_pauseBackup = this.pauseMask;
				src.setPauseMaskOn ();
				this.dispMainMessage ("PAUSE", dsc.mainDispModeDispStart, 26, 1, 1);
				if (dsc.getResultDisplay() == false) {
					dsc.subMenuDisplayOn ();
				}
			} else {
				//un pause
				sdc.resumeBgm ();
				sdc.resumeSe ();
				sdc.resumeVoice ();
				this.playSound (se_ts100);
				gamePause = false;
				if (this.pauseMask_pauseBackup == false) {
					src.setPauseMaskOff ();
				}
				this.dispMainMessage ("", dsc.mainDispModeDispDelete, 26, 1, 2);
				dsc.subMenuNoDisplay ();
				Time.timeScale = this.timescale_pauseBackup;
			}
		}
	}

	//tap continue button
	public void tapContinueButton(){
		if (gameOver == true) {
			//game continue
			int newScore=0;
			this.playSound (se_ts110);
			this.generateWipe1 ();
			plc.generateRebirthItem ();
			plc.setPlayerMode (plc.playerModeRebirth);
			plc.setPlayerStatusInit ();
			this.setPlayerColor (0);	//white
			this.setShieldColor (0);	//white
			this.dispMainMessage ("", dsc.mainDispModeDispDelete);
			dsc.subMenuNoDisplay ();
			continueCnt++;
			stgUseContinue = true;
			newScore = continueCnt;
			if (newScore > 9) {
				newScore = 9;
			}
			gameScore = newScore;
			dsc.dispScore (gameScore);
			starNum = 0;
			dsc.dispStar (starNum);
			stgGetStar = 0;
			src.setPauseMaskOff ();
			Time.timeScale = 1.0f;
			this.gameOver = false;
		} else if (gamePause == true) {
			//unpause
			this.tapPauseButton ();
		}
	}

	//tap goto title button
	public void tapGotoTitleButton(){
		if ( (gameOver == true) || (gamePause == true) || (ending == true) ) {
			//goto title
			sdc.stopBgm ();
			sdc.stopVoice ();
			sdc.stopSe ();
			this.playSound (se_ts100);
			plc.setPlayerMode ( plc.playerModeNoExist );
			dsc.infoNoDisplay ();	//todo: out実装してここなくす
			dsc.ehpNoDisplay ();	//tood: out実装してここなくす
			dsc.subMenuNoDisplay ();
			dsc.setResetFirstRotate ();
			this.dispMainMessage ("", dsc.mainDispModeDispDelete);
			//game status init
			this.gameStatusInit();
			plc.setPlayerStatusInit ();
			//stage result init
			this.stageResultInit();
			//main status init
			src.setPauseMaskOff ();
			this.destroyGamePlayObjects ();
			Time.timeScale = 1.0f;
			this.gameOver = false;
			this.gamePause = false;
			this.ending = false;
			this.setGameMode (gmGotoTitle);
			//object num reset
			starObjNum = 0;
			starGetMessageObjNum = 0;
		}
	}

	//tap next stage button
	public void tapNextStageButton(){
		if (gamePause == false) {
			this.playSound (se_ts110);
			dsc.stopResultDisplay ();
			this.releaseWait ();
		}
	}
		
	//game over
	public void setGameOver(){
		this.gameOver = true;
		Time.timeScale = 0.2f;
		src.setPauseMaskOn ();
		this.dispMainMessage ("GAME OVER", dsc.mainDispModeDispStart, 26);
		dsc.subMenuDisplayOn ();
	}

	//set ending
	public void setEnding(){
		this.ending = true;
	}

	//score add
	public void addGameScore( int score ){
		gameScore = gameScore + score;
		this.dispScore ();
	}

	//display score
	public void dispScore(){
		dsc.dispScore (gameScore);
	}

	//display shield
	public void dispShield( int hitpoint ){	// shilde = hitpoint - 1
		int sld = hitpoint-1;
		dsc.dispShield (sld);
	}

	//display player num
	public void dispPlayer( int ply ){
		dsc.dispPlayer (ply-1);
	}

	//display bomb num
	public void dispBomb( int bomb ){
		dsc.dispBomb (bomb);
	}

	//star num add
	public void addStarNum(){
		starNum++;
		stgGetStar++;	//get star in stage
		this.dispStarNum ();
		//star bonus
		this.starBonusProcess();
	}

	//display star num
	public void dispStarNum(){
		dsc.dispStar (starNum);
	}

	//score color change
	public void setScoreColor(){
		dsc.setScoreColorChg ();
	}

	//shield color change
	public void setShieldColor( int cl = 0 ){
		dsc.setShieldColorChg ( cl );
	}

	//player color change
	public void setPlayerColor( int cl = 0 ){
		dsc.setPlayerColorChg ( cl );
	}

	//bomb num color change
	public void setBombNumColor(){
		dsc.setBombNumColorChg ();
	}

	//star num color change
	public void setStarNumColor(){
		dsc.setStarNumColorChg ();
	}

	//star bonus process
	private void starBonusProcess(){
		if (starNum % 25 == 0) {
			int cl = 1;	//white
			string st = " *"+starNum.ToString ("D") + "!";
			if (starNum % 100 == 0) {
				st = st + "!";
				cl = 2;	//gold
				//star num color change
				this.setStarNumColor();
			}
			this.dispSubMessage (2.2f, 3.75f, -1.0f, -4.0f, st, cl);
		}
	}

	//player lost
	public void lostPlayerStg(){
		stgLostPlayer = true;
	}

	//player have damage
	public void haveDamageStg(){
		stgHaveDamage = true;
	}

	//player use bomb
	public void useBombStg(){
		stgUseBomb = true;
	}

	//get star num in stage
	public int getStarNumStage(){
		return this.stgGetStar;
	}

	//get player num
	public int getPlayerNum(){
		return plc.getPlayerNum ();
	}

	//get player hitpoint num
	public int getPlayerHp(){
		return plc.getPlayerHitPoint ();
	}

	//get player bomb num
	public int getPlayerBombNum(){
		return plc.getBombNum ();
	}

	//get use continue
	public bool getUseContinue(){
		return this.stgUseContinue;
	}

	//get lost player
	public bool getLostPlayer(){
		return this.stgLostPlayer;
	}

	//get have damage
	public bool getHaveDamage(){
		return this.stgHaveDamage;
	}

	//get use bomb
	public bool getUseBomb(){
		return this.stgUseBomb;
	}

	//get result display
	public bool getResultDisplay(){
		return dsc.getResultDisplay ();
	}

	//title trigger
	public void selGameLevel( int level ){
		if (this.gameTitleEnable == true) {
			gameLevel = level;
			this.playSound (se_ts100);
			dsc.setGameLevelColor ();
		}
	}
	public void selPlayerSpeed( int spd ){
		if (this.gameTitleEnable == true) {
			playerSpeed = spd;
			plc.setPlayerSpeed (spd);
			this.playSound (se_ts100);
			dsc.setPlayerSpeedColor ();
		}
	}
	public void selSubWeapon( int wpn ){
		if (this.gameTitleEnable == true) {
			subWeapon = wpn;
			this.playSound (se_ts100);
			dsc.setSubWeaponColor ();
		}
	}
	public void selPlayerType( int typ ){
		if (this.gameTitleEnable == true) {
			playerType = typ;
			plc.setPlayerType (typ);
			this.playSound (se_ts100);
			dsc.setPlayerTypeColor ( true );
		}
	}
	public void selGameStart (){
		if (this.gameTitleEnable == true) {
			this.playSound (se_ts110);
			dsc.setGameStartColor ();
			//game status init
			this.gameStatusInit();
			plc.setPlayerStatusInit ();
			//stage result init
			this.stageResultInit();
			//object num reset
			starObjNum = 0;
			starGetMessageObjNum = 0;
			//set gamemode
			this.setGameMode (gmGameStart);
		}
	}
	public void selCredit (){
		if (this.gameTitleEnable == true) {
			this.playSound (se_ts100);
			dsc.setCreditStartColor ();
			this.setGameMode (gmCreditStart);
		}
	}
	public void selDescriptionArrowLeft (){
		if (this.gameTitleEnable == true) {
			this.playSound (se_ts100);
			dsc.selDescriptionArrowLeft ();
		}
	}
	public void selDescriptionArrowRight (){
		if (this.gameTitleEnable == true) {
			this.playSound (se_ts100);
			dsc.selDescriptionArrowRight ();
		}
	}
	public void selDebugMenu (){
		if (this.gameTitleEnable == true) {
			this.playSound (se_ts100);
			dsc.setDebugMenuStartColor ();
			this.setGameMode (gmDebugMenuStart);
		}
	}

	//debug menu trigger
	public void sel_jikimuteki( bool mtk ){
		this.playSound (se_ts100);
		this.debug_jikimuteki = mtk;
	}
	public void sel_jikimugen( bool mgn ){
		this.playSound (se_ts100);
		this.debug_jikimugen = mgn;
	}
	public void sel_bombmugen( bool bmgn ){
		this.playSound (se_ts100);
		this.debug_bombmugen = bmgn;
	}
	public void sel_spower( float sp ){
		this.playSound (se_resultcnt);
		this.debug_spower = sp;
		plc.pPower = Mathf.FloorToInt(sp*5.0f);
	}
	public void sel_option( float op ){
		this.playSound (se_resultcnt);
		this.debug_option = op;
		this.debug_optionnum = (2 + ((int)(Mathf.Floor (op * 4.0f))*2));
	}
	public void sel_sweapon( float sw ){
		this.playSound (se_resultcnt);
		this.debug_swpower = sw;
		if (subWeapon == subWeaponLaser) {
			plc.pLaser = Mathf.FloorToInt (sw * 5.0f);
		} else {
			plc.pMissile = Mathf.FloorToInt (sw * 5.0f);
		}
	}
	public void sel_startpos( int sp ){
		this.playSound (se_ts100);
		this.debug_startposition = sp;
	}
	public void sel_debugstart(){
		this.playSound (se_ts110);
		//game status init
		this.gameStatusInit();
		plc.setPlayerStatusInitDebugMode ();
		//stage result init
		this.stageResultInit();
		//option rest
		plc.optionReset();
		//option debug num generate
		int gnop = ((this.debug_optionnum)-(plc.oNum))/2;
		for (int i = 0; i < gnop; i++) {
			plc.addOptionDebug ();
		}
		//start( play debug)
		this.setGameMode (gmPlayDebug);
	}
	public void sel_debugTitle(){
		this.playSound (se_ts100);
		//return to title
		this.setGameMode (gmTitleStart);
	}

	//get scroll speed
	public float getScrollSpeed(){
		return mpc.getScrSpd();
	}

	const float mapxk = -0.041f;//(max -0.079)
	//get map x scroll(player x mov) info (for mapcontroller)
	public float getMapxScroll(){
		if ( this.scrollMovexEnable == true ) {
			return (float)(plc.getPlayerxMoveInfo()) * mapxk;
		} else {
			return 0.0f;
		}
	}

	//get map x scroll info (for enemy)
	public float getMapxMov(){
		return mpc.getMapxMov ();
	}

	//set map x reset
	public void setMapxReset(){
		mpc.setMapxReset ();
	}

	//get stage
	public int getStage(){
		return gameStage;
	}

	//get stage last
	public bool getStageLast(){
		if (gameStage == gameStageLast) {
			return true;
		} else {
			return false;
		}
	}

	//get area
	public int getArea(){
		return gameArea;
	}

	//pause?
	public bool getPause(){
		return this.gamePause;
	}

	//game over?
	public bool getGameOver(){
		return this.gameOver;
	}

	//debug mode?
	public bool getDebugMode(){
		return this.debugMode;
	}

	//all objects number inc/dec
	public void incObj(){
		objNum++;
	}
	public void decObj(){
		objNum--;
	}

	//inc star obj num
	public void incStarObjNum(){
		this.starObjNum++;
	}

	//dec star obj num
	public void decStarObjNum(){
		this.starObjNum--;
	}

	//inc star get message obj num
	public void incStarGetMessageObjNum(){
		this.starGetMessageObjNum++;
	}

	//dec star get message obj num
	public void decStarGetMessageObjNum(){
		this.starGetMessageObjNum--;
	}

}
