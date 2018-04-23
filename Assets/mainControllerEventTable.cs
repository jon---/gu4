using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class mainController {

	//private
	//const

	//event table

	//game event title screen start table
	private int[] gameEventTitleStartTable = new int[]{
//debug -->
//新キャラ/パターン/イベント実装時のテスト用
/*
		geDisplay, geInfoDisplay,
		geScreenMask, geNoMask,
		geMap, geMapStage, geMapStage33,	//33, //32,	//3,	//2,	//1,
//		geMap, geMapScrollMovexEnable,	//d,	//d,	//e,	//e,	//e
		geMap, geMapScrollMovexDisable,	//d,	//d,	//e,	//e,	//e
		geMap, geMapScrollSet, 80,	//80,	//3,	//30,	//12,	//8,
		geBgm, geBgmPlay, geBgm163, geBgmNormal,	//163,	//502,	//152,	//185,	//143,	//121,

//新キャラ/パターン/イベント実装時のテスト場所
//event -->
//		geMap, geMapScrollSet, 80,
//		geBgm, geBgmPlay, geBgm110, geBgmForce,
//		geWait,	20,
//		geDisplay, geMainMessageDisplay, geMainMessageStageClear, geMainMessageDisplayStart, 18, 1,
//		geWait,	150,
//		geDisplay, geResultDisplayStart,
//		geDisplay, geMainMessageDisplay, geMainMessageDelete, geMainMessageDisplayDelete, 1, 1,
//		geDisplay, geInfoNoDisplay,
//		geWait,	60,
//		gePlayer, geSetPlayerMode, gePlayerModeOnBase,
//		geWait,	20,
//		gePlayer, gePlayerBase100, 1,
//		geWait,	-1,	//base wait
//		geDisplay, geEndingDisplayStart,
//event <--

//enemy -->
//		geEnemy, geEnemy110, 400, -170, 140, 0, geItemNone,
//		geEnemy, geEnemy210, 200, 650, 270, 75, geItemOption,
//		geEnemy, geEnemy200, -180, 550, 270, geItemNone,
//		geEnemy, geEnemy100, -400, 350, 45, geItemNone,
//		geEnemy, geEnemy120, 0, 600, 90, geItemNone,
//		geEnemy, geEnemy130, -250, 600, 70, geEne130_stop, 0, 0, geItemNone,
//		geEnemy, geEnemy240, -100,	600, geItemNone,
//		geEnemy, geEnemy190, -150, 600, 270, geItemNone,
//		geEnemy, geEnemy160, 0,	600, 290, 0, geItemNone,
//		geWait, 50,
//		geEnemy, geEnemy160, 0,	600, 270, 1, geItemNone,
//		geWait, 50,
//		geWait, 10,
//		geEnemy, geEnemy170, 200, 600, 250, geItemNone,
//		geEnemy, geEnemy170, -200, 600, 290, geItemNone,
//		geWait, 5,
//		geEnemy, geEnemy180, -200, 600, 290, geItemNone,
//		geEnemy, geEnemy220, 130, 650, 270,geItemNone,
//		geEnemy, geEnemy230, 200, 650, 270, 75, geItemNone,
//		geWait, 100,
//		geLoop,
//		geTerm,
//enemy <--

//middile boss -->
//		geMap, geMapScrollMovexDisable,
//		geMap, geMapScrollSet, 60,
//		geWait, 50,
//		geDisplay, geEhDisplayIn, geEhDisplayLv0,
//		geWait, 80,
//		geVoice, geVoicePlay, geVoice220, geVoiceForce,
//		geWait, 35,
//		geEnemy, geEnemy300, 2, -1,	//2,	//1,	//0,
//		geTerm,
//middile boss <--


//boss -->
//		geMap, geMapScrollMovexDisable,
//		geMap, geMapScrollSet, 3,	//3,	//30,	//16,	//5,
//		geBgm, geBgmPlay, geBgm502, geBgmForce,	//502,	//152,	//502,	//502,
//		geDisplay, geEhDisplayIn, geEhDisplayLv1,
////		geWait, 20,
//		geEnemy, geEnemy530, geItemNone,	//530,	//520,	//515,	//500,
//		geWait, -1,
//530only -->
//		geMap, geMapScrollTarget, -60,
//		geDisplay, geMainMessageDisplay, geMainMessageWarning, geMainMessageDisplayTime, 26, 130,
//		geVoice, geVoicePlay, geVoice170, geVoiceForce,
//		geWait, -1,
//		geMap, geMapStage, geMapStage33,	//33, //32,	//3,	//2,	//1,
//		geMap, geMapScrollTarget, -4,
//		geWait, 150,
//		geBgm, geBgmPlay, geBgm163, geBgmForce,	//163,	//502,	//152,	//185,	//143,	//121,
//		geWait, 100,
//		geMap, geMapScrollTarget, 60,
//		geSE, geSePlay, geSe_base131, geSeForce,
//530only <--
//		geTerm,
//boss <--

//pattern -->
//		geWait, 52,
//
//		geTerm,
//		geLoop,
//
//		geWait, -1,
//		geTerm,
//pattern <--
		geTerm,

*/
//debug <--
		geDisplay, geInfoNoDisplay,
		geScreenMask, geMask,
		geMap, geMapStage, geMapTitle,
		geMap, geMapScrollMovexReset,
		geMap, geMapScrollMovexDisable,
		geMap, geMapScrollSet, 7,
		geScreenMask, geMaskFadein,
		geWait, -1,	//screen fadein wait
		geTitle, geTitleInit,
		geTitle, geTitleFadein,
		geWait, -1,	//ui fade in wait
//		geSE, geSePlay, geSe_title101, geSeForce,
		geTitle, geTitleEnable,

		geWait, 20,
		geVoice, geVoicePlay, geVoice101, geVoiceForce,
		geWait, 80,//60,
		geBgm, geBgmPlay, geBgm801, geBgmForce,
		geWait, 50,
//		geVoice, geVoicePlay, geVoice250, geVoiceForce,
		geGameMode, geGameModeTitle,
		geTerm
	};

	//game event title screen table
	private int[] gameEventTitleTable = new int[]{
		geVoice, geVoicePlay, geVoice250, geVoiceNormal,
		geWait, -1,	//game start or credit wait
		geTerm
	};

	//game event creditlist screen start table
	private int[] gameEventCreditStartTable = new int[]{
		geBgm, geBgmPlay, geBgm801, geBgmNormal,
		geTitle, geTitleDisable,
		geTitle, geTitleFadeout,
		geWait, -1,	//ui fade out wait
		geGameMode, geGameModeCredit,
		geTerm
	};

	//game event creditlist screen table
	private int[] gameEventCreditTable = new int[]{
		geCredit, geCreditInit,
		geCredit, geCreditFadein,
		geWait, -1,	//ui fade in wait
		geCredit, geCreditEnable,
		geTerm
	};

	//game event term creditlist screen table
	private int[] gameEventTermCreditTable = new int[]{
		geCredit, geCreditDisable,
		geCredit, geCreditFadeout,
		geWait, -1,	//ui fade out wait
		geTitle, geTitleInit,
		geTitle, geTitleFadein,
		geWait, -1,	//ui fade in wait
		geTitle, geTitleEnable,
		geGameMode, geGameModeTitle,
		geTerm
	};

	//game event debug menu start table
	private int[] gameEventDebugMenuStartTable = new int[]{
		geBgm, geBgmStop,
		geVoice, geVoiceStop,
		geTitle, geTitleDisable,
		geTitle, geTitleFadeout,
		geWait, -1,	//ui fade out wait
		geGameMode, geGameModeDebugMenu,
		geTerm
	};

	//game event debug menu table
	private int[] gameEventDebugMenuTable = new int[]{
		geDebugMenu, geDebugMenuInit,
		geWait, -1,	//game start wait
		geTerm
	};

	//game event play start table
	private int[] gameEventGameStartTable = new int[]{
		geBgm, geBgmStop,
		geVoice, geVoiceStop,
		geTitle, geTitleDisable,
		geTitle, geTitleFadeout,
		geWait, -1,	//game start wait
		geScreenMask, geMaskFadeout,
		geWait, -1,	//screen fadeout wait
		geGameMode, geGameModePlay,
		geTerm
	};

	//game event goto title screen table (for play game mode)
	private int[] gameEventGotoTitleTable = new int[]{
		geDisplay, geInfoNoDisplay,	//geInfoDisplayOut,	//out未実装
		geDisplay, geEhNoDisplay, //geEhDisplayOut,	//out未実装
		geScreenMask, geMaskFadeout,
		geWait, -1,	//screen fadeout wait
		geStatus, geDestroyGamePlayObjects,
		geWait, 20,
		geGameMode, geGameModeTitleStart,
		geTerm
	};

}
