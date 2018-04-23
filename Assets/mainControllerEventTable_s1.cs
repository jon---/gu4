using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class mainController {

	//private
	//const

	//event table

	//game event stage 1 area 1 table (position:*100)
	private int[] gameEventStage1Area1Table = new int[]{
		geScreenMask, geMask,
		geMap, geMapStage, geMapStage1,
		geMap, geMapScrollMovexReset,
		geMap, geMapScrollMovexDisable,
		geMap, geMapScrollSet, 60,
		gePlayer, geSetPlayerMode, gePlayerModeNoExist,
		geBgm, geBgmPlay, geBgm121, geBgmForce,
		geWait,	10,	
		geScreenMask, geMaskFadein,
		geWait, -1,	//screen fadein wait

		gePlayer, gePlayerBase100, 0,
		geWait, 90,

		geVoice, geVoicePlay, geVoice121, geVoiceForce,
		geWait, 90,

		geWait, 80,

		geVoice, geVoicePlay, geVoice110, geVoiceForce,
		geWait, 60,

		geWait, 70,

		geDisplay, geInfoDisplayIn,

		gePlayer, geSetPlayerMode, gePlayerModeInvalid,

		geMap, geMapScrollMovexEnable,
		geMap, geMapScrollTarget, 8,

		geWait, 10,

		geNextArea,

		geTerm
	};

	//game event stage 1 area 2 table (position:*100)
	private int[] gameEventStage1Area2Table = new int[] {
		geMap, geMapStage, geMapStage1,
		geMap, geMapScrollMovexEnable,
		geMap, geMapScrollTarget, 8,
		geBgm, geBgmPlay, geBgm121, geBgmNormal,
		geWait, 20,
		geDisplay, geMainMessageDisplay, geMainMessageStageStart, geMainMessageDisplayTime, 26, 90,
		geWait, 100,

		geWait, 52,

		//#s1-2 #a1
		geEnemy, geEnemy120, 0, 600, 90, geItemNone,
		geWait, 5,
		geEnemy, geEnemy120, -120, 600, 90, geItemNone,
		geEnemy, geEnemy120, 120, 600, 90, geItemNone,
		geWait, 2,
		geEnemy, geEnemy120, -240, 600, 90, geItemPower,
		geEnemy, geEnemy120, 240, 600, 90, geItemLaser,
		geWait, 5,

		geWait, 60,

		//#s1-2 #a2
		geEnemy, geEnemy130, -250, 600, 70, geEne130_stop, 0, 0, geItemNone,
		geEnemy, geEnemy130, -150, 600, 70, geEne130_stop, 0, 0, geItemNone,
		geEnemy, geEnemy130, -50, 600, 70, geEne130_stop, 0, 0, geItemNone,
		geEnemy, geEnemy130, 50, 600, 70, geEne130_stop, 0, 0, geItemNone,
		geEnemy, geEnemy130, 150, 600, 70, geEne130_stop, 0, 0, geItemNone,
		geEnemy, geEnemy130, 250, 600, 70, geEne130_stop, 0, 0, geItemNone,
		geWait, 25,
		geEnemy, geEnemy130, -250, 600, 70, geEne130_escape, 0, 0, geItemNone,
		geEnemy, geEnemy130, -150, 600, 70, geEne130_stop, 0, 0, geItemNone,
		geEnemy, geEnemy130, -50, 600, 70, geEne130_stop, 0, 0, geItemNone,
		geEnemy, geEnemy130, 50, 600, 70, geEne130_stop, 0, 0, geItemNone,
		geEnemy, geEnemy130, 150, 600, 70, geEne130_escape, 0, 0, geItemNone,
		geEnemy, geEnemy130, 250, 600, 70, geEne130_stop, 0, 0, geItemNone,
		geWait, 25,
		geEnemy, geEnemy130, -250, 600, 70, geEne130_totarget, 100, 600, geItemOption,
		geEnemy, geEnemy130, -150, 600, 70, geEne130_totarget, 200, 600, geItemNone,
		geEnemy, geEnemy130, -50, 600, 70, geEne130_totarget, -400, 600, geItemNone,
		geEnemy, geEnemy130, 50, 600, 70, geEne130_escape, 0, 0, geItemNone,
		geEnemy, geEnemy130, 150, 600, 70, geEne130_escape, 0, 0, geItemNone,
		geEnemy, geEnemy130, 250, 600, 70, geEne130_escape, 0, 0, geItemPower,

		geWait, 50,

		//#s1-2 #a3
		geEnemy, geEnemy110, -150, 600, 270, 90, geItemNone,
//		geEnemy, geEnemy110, 150, 600, 270, 90, geItemNone,
		geWait, 24,
//		geEnemy, geEnemy110, -160, 600, 270, 90, geItemNone,
		geEnemy, geEnemy110, 160, 600, 270, 90, geItemNone,
		geWait, 24,
		geEnemy, geEnemy110, -170, 600, 270, 90, geItemNone,
//		geEnemy, geEnemy110, 170, 600, 270, 90, geItemNone,
		geWait, 2,
		geEnemy, geEnemy130, -400, 600, 60, geEne130_forward, 0, 0, geItemNone,
		geWait, 12,
		geEnemy, geEnemy130, -400, 480, 60, geEne130_forward, 0, 0, geItemNone,
		geWait, 12,
		geEnemy, geEnemy130, -400, 360, 60, geEne130_forward, 0, 0, geItemNone,
		geEnemy, geEnemy130, -400, 600, 60, geEne130_forward, 0, 0, geItemNone,
//		geEnemy, geEnemy110, -180, 600, 270, 90, geItemNone,
		geEnemy, geEnemy110, 180, 600, 270, 90, geItemNone,
		geEnemy, geEnemy130, 400, 400, 180, geEne130_approaches, 0, 0, geItemNone,	//add sub enemy
		geWait, 12,
		geEnemy, geEnemy130, -400, 240, 60, geEne130_forward, 0, 0, geItemNone,
		geEnemy, geEnemy130, -400, 480, 60, geEne130_forward, 0, 0, geItemNone,
		geEnemy, geEnemy130, -190, 600, 60, geEne130_forward, 0, 0, geItemNone,
		geWait, 12,
		geEnemy, geEnemy130, -400, 120, 60, geEne130_forward, 0, 0, geItemNone,
		geEnemy, geEnemy130, -400, 360, 60, geEne130_forward, 0, 0, geItemNone,
		geEnemy, geEnemy110, -190, 600, 270, 90, geItemNone,
//		geEnemy, geEnemy110, 190, 600, 270, 90, geItemNone,
//		geEnemy, geEnemy130, 400, 400, 210, geEne130_forward, 0, 0, geItemNone,	//add sub enemy
		geWait, 12,
		geEnemy, geEnemy130, -400, 0, 60, geEne130_forward, 0, 0, geItemNone,
		geEnemy, geEnemy130, -400, 240, 60, geEne130_forward, 0, 0, geItemNone,
		geEnemy, geEnemy130, -190, 600, 60, geEne130_forward, 0, 0, geItemNone,
		geWait, 12,
		geEnemy, geEnemy130, -400, -120, 60, geEne130_forward, 0, 0, geItemNone,
		geEnemy, geEnemy130, -400, 120, 60, geEne130_forward, 0, 0, geItemNone,
		geWait, 12,
		geEnemy, geEnemy130, -400, 0, 60, geEne130_forward, 0, 0, geItemNone,
//		geEnemy, geEnemy130, -400, 300, 340, geEne130_forward, 0, 0, geItemNone,	//add sub enemy
		geWait, 12,
		geEnemy, geEnemy130, -400, -120, 60, geEne130_forward, 0, 0, geItemNone,
		geWait, 12,

		geWait, 30,

		//#s1-2 #a4
		geEnemy, geEnemy100, -400, 350, 45, geItemNone,
		geWait, 4,
		geEnemy, geEnemy100, -400, 360, 45, geItemNone,
		geWait, 4,
		geEnemy, geEnemy100, -400, 370, 45, geItemNone,
		geWait, 4,
		geEnemy, geEnemy100, -400, 380, 45, geItemNone,
		geWait, 4,
		geEnemy, geEnemy100, -400, 390, 45, geItemNone,
		geEnemy, geEnemy100, 300, 600, 310, geItemNone,
		geEnemy, geEnemy130, -150, 600, 270, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
//		geEnemy, geEnemy130, 50, 600, 270, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
//		geEnemy, geEnemy130, 250, 600, 270, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geWait, 4,
		geEnemy, geEnemy100, -400, 400, 45, geItemNone,
		geEnemy, geEnemy100, 180, 600, 310, geItemNone,
		geWait, 4,
		geEnemy, geEnemy100, -400, 410, 45, geItemNone,
		geEnemy, geEnemy100, 100, 600, 310, geItemNone,
		geWait, 4,
		geEnemy, geEnemy100, -20, 600, 310, geItemNone,
		geWait, 4,
		geEnemy, geEnemy100, -100, 600, 310, geItemNone,
//		geEnemy, geEnemy130, -150, 600, 270, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
//		geEnemy, geEnemy130, 50, 600, 270, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 250, 600, 270, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geWait, 4,
		geEnemy, geEnemy100, -180, 600, 310, geItemNone,
		geWait, 4,

		geWait, 35,

		//#s1-2 #a5
		geEnemy, geEnemy110, -400, 280, 0, 270, geItemNone,
		geWait, 6,
		geEnemy, geEnemy110, -400, 280, 0, 270, geItemNone,
		geWait, 6,
		geEnemy, geEnemy110, -400, 280, 0, 270, geItemNone,
		geWait, 6,
		geEnemy, geEnemy110, -400, 280, 0, 270, geItemNone,
//		geEnemy, geEnemy120, -240, 600, 180, geItemNone,	//add sub enemy
//		geEnemy, geEnemy120, 240, 600, 0, geItemNone,	//add sub enemy
		geWait, 6,
		geEnemy, geEnemy110, -400, 280, 0, 270, geItemNone,
		geEnemy, geEnemy110, 400, 400, 180, 270, geItemNone,
		geWait, 6,
		geEnemy, geEnemy110, -400, 280, 0, 270, geItemNone,
		geEnemy, geEnemy110, 400, 400, 180, 270, geItemNone,
//		geEnemy, geEnemy120, -120, 600, 180, geItemNone,	//add sub enemy
//		geEnemy, geEnemy120, 120, 600, 0, geItemNone,	//add sub enemy
		geWait, 6,
		geEnemy, geEnemy110, 400, 400, 180, 270, geItemNone,
		geWait, 6,
		geEnemy, geEnemy110, 400, 400, 180, 270, geItemNone,
//		geEnemy, geEnemy120, -200, 600, 270, geItemNone,	//add sub enemy
		geWait, 6,
		geEnemy, geEnemy110, 400, 400, 180, 270, geItemNone,
		geWait, 6,

		geWait, 80,

		//#s1-2 #a6	#power1
		geEnemy, geEnemy120, -200, 600, 225, geItemNone,
		geEnemy, geEnemy120, 200, 600, 315, geItemNone,
//		geEnemy, geEnemy100, 200, 600, 300, geItemNone,	//add sub enemy
		geWait, 5,
		geEnemy, geEnemy120, -100, 600, 225, geItemNone,
		geEnemy, geEnemy120, 100, 600, 315, geItemNone,
		geWait, 5,
		geEnemy, geEnemy120, 0, 600, 270, geItemNone,
//		geEnemy, geEnemy100, 200, 600, 300, geItemNone,	//add sub enemy
		geWait, 10,
		geEnemy, geEnemy130, -50, 600, 0, geEne130_stop, 0, 0, geItemNone,
		geEnemy, geEnemy130, 50, 600, 245, geEne130_stop, 0, 0, geItemNone,
//		geEnemy, geEnemy100, 200, 600, 300, geItemNone,	//add sub enemy
		geWait, 10,
		geEnemy, geEnemy130, -50, 600, 150, geEne130_approaches, 0, 0, geItemPower,
		geEnemy, geEnemy130, 50, 600, 45, geEne130_stop, 0, 0, geItemNone,
		geWait, 10,
		geEnemy, geEnemy120, 0, 600, 90, geItemNone,
		geWait, 5,
		geEnemy, geEnemy120, -100, 600, 135, geItemNone,
		geEnemy, geEnemy120, 100, 600, 45, geItemNone,
		geWait, 5,
		geEnemy, geEnemy120, -200, 600, 135, geItemNone,
		geEnemy, geEnemy120, 200, 600, 45, geItemNone,
		geWait, 50,
		geEnemy, geEnemy110, -150, 600, 270, 90, geItemNone,
		geEnemy, geEnemy110, 400, -180, 140, 0, geItemNone,
//		geEnemy, geEnemy100, -200, 600, 210, geItemNone,	//add sub enemy
		geWait, 10,
//		geEnemy, geEnemy100, -200, 600, 210, geItemNone,	//add sub enemy
		geWait, 10,
		geEnemy, geEnemy100, -200, 600, 210, geItemNone,	//add sub enemy
		geWait, 5,
		geEnemy, geEnemy110, -160, 600, 270, 90, geItemNone,
		geEnemy, geEnemy110, 400, -170, 140, 0, geItemNone,
		geWait, 25,
		geEnemy, geEnemy110, -170, 600, 270, 90, geItemNone,
		geEnemy, geEnemy110, 400, -160, 140, 0, geItemNone,
		geWait, 25,
		geEnemy, geEnemy110, -180, 600, 270, 90, geItemNone,
		geEnemy, geEnemy110, 400, -150, 140, 0, geItemNone,
		geWait, 25,
		geEnemy, geEnemy110, -190, 600, 270, 90, geItemNone,
		geEnemy, geEnemy110, 400, -140, 140, 0, geItemLaser,
		geWait, 25,

		geWait, 5,

		//#s1-2 #a7	#option*1 #bomb*1 #laser*1
		geEnemy, geEnemy130, 400, 500, 138, geEne130_forward, 0, 0, geItemNone,
		geWait, 16,
		geEnemy, geEnemy130, 400, 500, 138, geEne130_forward, 0, 0, geItemNone,
		geEnemy, geEnemy130, 400, 380, 138, geEne130_forward, 0, 0, geItemNone,
		geEnemy, geEnemy130, 400, 260, 138, geEne130_forward, 0, 0, geItemNone,
		geWait, 16,
		geEnemy, geEnemy130, 400, 380, 138, geEne130_forward, 0, 0, geItemNone,
		geEnemy, geEnemy130, 400, 260, 138, geEne130_forward, 0, 0, geItemOption,
		geEnemy, geEnemy130, 400, 140, 138, geEne130_forward, 0, 0, geItemNone,
		geWait, 16,
		geEnemy, geEnemy130, 400, 260, 138, geEne130_forward, 0, 0, geItemNone,
		geEnemy, geEnemy130, 400, 140, 138, geEne130_forward, 0, 0, geItemNone,
		geEnemy, geEnemy130, 400, 20, 138, geEne130_forward, 0, 0, geItemNone,
		geWait, 16,
		geEnemy, geEnemy130, 400, 140, 138, geEne130_forward, 0, 0, geItemNone,
		geEnemy, geEnemy130, 400, 20, 138, geEne130_forward, 0, 0, geItemNone,
		geEnemy, geEnemy130, 400, -100, 138, geEne130_forward, 0, 0, geItemNone,
		geEnemy, geEnemy130, 400, 520, 132, geEne130_forward, 0, 0, geItemNone,
		geEnemy, geEnemy130, 400, 380, 132, geEne130_forward, 0, 0, geItemNone,
		geWait, 16,
		geEnemy, geEnemy130, 400, 20, 138, geEne130_forward, 0, 0, geItemNone,
		geEnemy, geEnemy130, 400, -100, 138, geEne130_forward, 0, 0, geItemLaser,
		geEnemy, geEnemy130, 400, -220, 138, geEne130_forward, 0, 0, geItemNone,
		geEnemy, geEnemy130, 400, 380, 132, geEne130_forward, 0, 0, geItemNone,
		geEnemy, geEnemy130, 400, 240, 132, geEne130_forward, 0, 0, geItemNone,
		geWait, 16,
		geEnemy, geEnemy130, 400, 380, 132, geEne130_forward, 0, 0, geItemNone,
		geEnemy, geEnemy130, 400, 240, 132, geEne130_forward, 0, 0, geItemBomb,
		geEnemy, geEnemy130, 400, 100, 132, geEne130_forward, 0, 0, geItemNone,
		geWait, 16,
		geEnemy, geEnemy130, 400, 240, 132, geEne130_forward, 0, 0, geItemNone,
		geEnemy, geEnemy130, 400, 100, 132, geEne130_forward, 0, 0, geItemNone,
		geEnemy, geEnemy130, 400, -40, 132, geEne130_forward, 0, 0, geItemNone,
		geWait, 16,
		geEnemy, geEnemy130, 400, 100, 132, geEne130_forward, 0, 0, geItemNone,
		geEnemy, geEnemy130, 400, -40, 132, geEne130_forward, 0, 0, geItemNone,
		geEnemy, geEnemy130, 400, -180, 132, geEne130_forward, 0, 0, geItemShield,
		geEnemy, geEnemy130, 400, 560, 126, geEne130_forward, 0, 0, geItemNone,
		geEnemy, geEnemy130, 400, 400, 126, geEne130_forward, 0, 0, geItemNone,
		geWait, 16,
		geEnemy, geEnemy130, 400, -40, 132, geEne130_forward, 0, 0, geItemNone,
		geEnemy, geEnemy130, 400, -180, 132, geEne130_forward, 0, 0, geItemNone,
		geEnemy, geEnemy130, 400, 400, 126, geEne130_forward, 0, 0, geItemNone,
		geEnemy, geEnemy130, 400, 240, 126, geEne130_forward, 0, 0, geItemNone,
		geWait, 16,
		geEnemy, geEnemy130, 400, -180, 132, geEne130_forward, 0, 0, geItemNone,
		geEnemy, geEnemy130, 400, 400, 126, geEne130_forward, 0, 0, geItemNone,
		geEnemy, geEnemy130, 400, 240, 126, geEne130_forward, 0, 0, geItemLaser,
		geEnemy, geEnemy130, 400, 80, 126, geEne130_forward, 0, 0, geItemNone,
		geWait, 16,
		geEnemy, geEnemy130, 400, 240, 126, geEne130_forward, 0, 0, geItemNone,
		geEnemy, geEnemy130, 400, 80, 126, geEne130_forward, 0, 0, geItemNone,
		geEnemy, geEnemy130, 400, -80, 126, geEne130_forward, 0, 0, geItemNone,
		geWait, 16,
		geEnemy, geEnemy130, 400, 80, 126, geEne130_forward, 0, 0, geItemNone,
		geEnemy, geEnemy130, 400, -80, 126, geEne130_forward, 0, 0, geItemNone,
		geEnemy, geEnemy130, 400, -240, 126, geEne130_forward, 0, 0, geItemNone,
		geWait, 16,
		geEnemy, geEnemy130, 400, -80, 126, geEne130_forward, 0, 0, geItemNone,
		geEnemy, geEnemy130, 400, -240, 126, geEne130_forward, 0, 0, geItemPower,
		geWait, 16,
		geEnemy, geEnemy130, 400, -240, 126, geEne130_forward, 0, 0, geItemNone,
		geWait, 16,

		geWait, 50,

		//#s1-2 #a8	#shield*1
		geEnemy, geEnemy100, -180, 600, 280, geItemNone,
		geEnemy, geEnemy100, 0, 600, 270, geItemNone,
		geEnemy, geEnemy100, 180, 600, 260, geItemNone,
		geWait, 4,
		geEnemy, geEnemy100, -180, 600, 280, geItemNone,
		geEnemy, geEnemy100, 0, 600, 270, geItemNone,
		geEnemy, geEnemy100, 180, 600, 260, geItemNone,
		geWait, 4,
		geEnemy, geEnemy100, -180, 600, 280, geItemNone,
		geEnemy, geEnemy100, 0, 600, 270, geItemNone,
		geEnemy, geEnemy100, 180, 600, 260, geItemNone,
		geWait, 4,
		geEnemy, geEnemy100, -180, 600, 280, geItemNone,
		geEnemy, geEnemy100, 0, 600, 270, geItemNone,
		geEnemy, geEnemy100, 180, 600, 260, geItemNone,
		geWait, 4,
		geEnemy, geEnemy100, -180, 600, 280, geItemNone,
		geEnemy, geEnemy100, 0, 600, 270, geItemNone,
		geEnemy, geEnemy100, 180, 600, 260, geItemNone,
		geWait, 4,
		geEnemy, geEnemy100, -180, 600, 280, geItemNone,
		geEnemy, geEnemy100, 0, 600, 270, geItemNone,
		geEnemy, geEnemy100, 180, 600, 260, geItemNone,
		geWait, 4,
		geEnemy, geEnemy100, 350, 600, 210, geItemShield,

		geWait, 100,

		//#s1-2 #a9
		geEnemy, geEnemy200, -180, 550, 270, geItemNone,
		geWait, 60,
		geEnemy, geEnemy200, 180, 550, 270, geItemNone,
		geWait, 50,
		geEnemy, geEnemy200, 0, 550, 270, geItemNone,
		geWait, 30,
		geEnemy, geEnemy200, 0, 550, 270, geItemNone,
		geWait, 20,

		geWait, 50,

		//#s1-2 #a10
		geEnemy, geEnemy130, -300, 600, 30, geEne130_stop, 0, 0, geItemNone,
		geEnemy, geEnemy130, -200, 600, 30, geEne130_stop, 0, 0, geItemNone,
		geEnemy, geEnemy130, -100, 600, 30, geEne130_stop, 0, 0, geItemNone,
		geEnemy, geEnemy130, 0, 600, 30, geEne130_stop, 0, 0, geItemNone,
		geEnemy, geEnemy130, 100, 600, 330, geEne130_stop, 0, 0, geItemOption,
		geWait, 10,
		geEnemy, geEnemy130, -300, 600, 30, geEne130_stop, 0, 0, geItemNone,
		geEnemy, geEnemy130, -200, 600, 30, geEne130_stop, 0, 0, geItemNone,
		geEnemy, geEnemy130, -100, 600, 30, geEne130_stop, 0, 0, geItemNone,
		geEnemy, geEnemy130, 0, 600, 30, geEne130_stop, 0, 0, geItemNone,
		geEnemy, geEnemy130, 100, 600, 330, geEne130_stop, 0, 0, geItemNone,
		geWait, 10,
		geEnemy, geEnemy130, -300, 600, 30, geEne130_stop, 0, 0, geItemBomb,
		geEnemy, geEnemy130, -200, 600, 30, geEne130_stop, 0, 0, geItemNone,
		geEnemy, geEnemy130, -100, 600, 30, geEne130_stop, 0, 0, geItemNone,
		geEnemy, geEnemy130, 0, 600, 30, geEne130_stop, 0, 0, geItemNone,
		geEnemy, geEnemy130, 100, 600, 330, geEne130_stop, 0, 0, geItemNone,
		geWait, 10,
		geEnemy, geEnemy130, -300, 600, 30, geEne130_stop, 0, 0, geItemNone,
		geEnemy, geEnemy130, -200, 600, 30, geEne130_stop, 0, 0, geItemNone,
		geEnemy, geEnemy130, -100, 600, 30, geEne130_stop, 0, 0, geItemNone,
		geEnemy, geEnemy130, 0, 600, 30, geEne130_stop, 0, 0, geItemNone,
		geEnemy, geEnemy130, 100, 600, 330, geEne130_stop, 0, 0, geItemNone,
		geWait, 10,
		geEnemy, geEnemy130, -300, 600, 30, geEne130_stop, 0, 0, geItemNone,
		geEnemy, geEnemy130, -200, 600, 30, geEne130_stop, 0, 0, geItemNone,
		geEnemy, geEnemy130, -100, 600, 30, geEne130_stop, 0, 0, geItemNone,
		geEnemy, geEnemy130, 0, 600, 30, geEne130_stop, 0, 0, geItemNone,
		geEnemy, geEnemy130, 100, 600, 330, geEne130_stop, 0, 0, geItemLaser,
		geWait, 10,
		geEnemy, geEnemy130, -300, 600, 30, geEne130_stop, 0, 0, geItemNone,
		geEnemy, geEnemy130, -200, 600, 30, geEne130_stop, 0, 0, geItemNone,
		geEnemy, geEnemy130, -100, 600, 30, geEne130_stop, 0, 0, geItemNone,
		geEnemy, geEnemy130, 0, 600, 30, geEne130_stop, 0, 0, geItemNone,
		geEnemy, geEnemy130, 100, 600, 330, geEne130_stop, 0, 0, geItemNone,
		geWait, 10,
		geEnemy, geEnemy130, -300, 600, 30, geEne130_stop, 0, 0, geItemPower,
		geEnemy, geEnemy130, -200, 600, 30, geEne130_stop, 0, 0, geItemNone,
		geEnemy, geEnemy130, -100, 600, 30, geEne130_stop, 0, 0, geItemNone,
		geEnemy, geEnemy130, 0, 600, 30, geEne130_stop, 0, 0, geItemNone,
		geEnemy, geEnemy130, 100, 600, 330, geEne130_stop, 0, 0, geItemNone,
		geWait, 10,
		geEnemy, geEnemy130, -300, 600, 30, geEne130_stop, 0, 0, geItemNone,
		geEnemy, geEnemy130, -200, 600, 30, geEne130_stop, 0, 0, geItemNone,
		geEnemy, geEnemy130, -100, 600, 30, geEne130_stop, 0, 0, geItemNone,
		geEnemy, geEnemy130, 0, 600, 30, geEne130_stop, 0, 0, geItemNone,
		geEnemy, geEnemy130, 100, 600, 330, geEne130_stop, 0, 0, geItemNone,
		geWait, 10,
		geEnemy, geEnemy130, -300, 600, 30, geEne130_stop, 0, 0, geItemNone,
		geEnemy, geEnemy130, -200, 600, 30, geEne130_stop, 0, 0, geItemNone,
		geEnemy, geEnemy130, -100, 600, 30, geEne130_stop, 0, 0, geItemNone,
		geEnemy, geEnemy130, 0, 600, 30, geEne130_stop, 0, 0, geItemNone,
		geEnemy, geEnemy130, 100, 600, 330, geEne130_stop, 0, 0, geItemShield,
		geWait, 40,
		geEnemy, geEnemy130, 0, 600, 0, geEne130_stop, 0, 0, geItemNone,
		geEnemy, geEnemy130, 150, 600, 150, geEne130_forward, 0, 0, geItemNone,
		geEnemy, geEnemy130, 250, 600, 240, geEne130_forward, 0, 0, geItemNone,
		geEnemy, geEnemy130, -180, 600, 280, geEne130_forward, 0, 0, geItemNone,
		geEnemy, geEnemy130, 100, 600, 0, geEne130_stop, 0, 0, geItemNone,
		geWait, 5,
		geEnemy, geEnemy130, 400, 230, 130, geEne130_approaches, 0, 0, geItemNone,
		geEnemy, geEnemy130, 400, 330, 130, geEne130_approaches, 0, 0, geItemNone,
		geEnemy, geEnemy130, 250, 600, 270, geEne130_forward, 0, 0, geItemNone,
		geWait, 5,
		geEnemy, geEnemy130, -400, 120, 50, geEne130_approaches, 0, 0, geItemNone,
		geEnemy, geEnemy130, 400, 120, 130, geEne130_approaches, 0, 0, geItemNone,
		geEnemy, geEnemy130, -50, 600, 270, geEne130_escape, 0, 0, geItemPower,
		geEnemy, geEnemy130, -200, 600, 270, geEne130_escape, 0, 0, geItemLaser,
		geEnemy, geEnemy130, -400, -50, 10, geEne130_totarget, -100, 600, geItemNone,
		geEnemy, geEnemy130, -30, 600, 300, geEne130_forward, 0, 0, geItemNone,
		geWait, 5,
		geEnemy, geEnemy130, -400, 150, 10, geEne130_totarget, 0, 600, geItemNone,
		geEnemy, geEnemy130, 400, 150, 170, geEne130_totarget, 100, 600, geItemNone,
		geEnemy, geEnemy130, 30, 600, 260, geEne130_forward, 0, 0, geItemNone,
		geWait, 5,
		geEnemy, geEnemy130, -400, 120, 50, geEne130_approaches, 0, 0, geItemNone,
		geEnemy, geEnemy130, 400, 120, 130, geEne130_approaches, 0, 0, geItemNone,
		geEnemy, geEnemy130, -50, 600, 270, geEne130_escape, 0, 0, geItemNone,
		geEnemy, geEnemy130, -200, 600, 270, geEne130_escape, 0, 0, geItemNone,
		geEnemy, geEnemy130, -400, -50, 10, geEne130_totarget, -100, 600, geItemOption,
		geEnemy, geEnemy130, -30, 600, 300, geEne130_forward, 0, 0, geItemNone,
		geWait, 5,
		geEnemy, geEnemy130, -400, 150, 10, geEne130_totarget, 0, 600, geItemNone,
		geEnemy, geEnemy130, 400, 150, 170, geEne130_totarget, 100, 600, geItemNone,
		geEnemy, geEnemy130, 30, 600, 260, geEne130_forward, 0, 0, geItemNone,
		geWait, 5,

		geWait, 100,

		//#s1-2 #a11
		geEnemy, geEnemy120, 0, 600, 90, geItemNone,
		geWait, 10,
		geEnemy, geEnemy120, -40, 600, 90, geItemNone,
		geEnemy, geEnemy120, 40, 600, 90, geItemNone,
		geWait, 10,
		geEnemy, geEnemy120, -120, 600, 90, geItemNone,
		geEnemy, geEnemy120, 120, 600, 90, geItemNone,
		geWait, 10,
		geEnemy, geEnemy120, -200, 600, 90, geItemNone,
		geEnemy, geEnemy120, 200, 600, 90, geItemNone,
		geEnemy, geEnemy200, 0, 550, 270, geItemPower,	//add sub enemy
		geWait, 10,
		geEnemy, geEnemy120, -220, 600, 90, geItemNone,
		geEnemy, geEnemy120, 220, 600, 90, geItemNone,
		geWait, 10,
		geEnemy, geEnemy120, -220, 600, 90, geItemNone,
		geEnemy, geEnemy120, 220, 600, 90, geItemNone,
		geEnemy, geEnemy200, 0, 550, 270, geItemLaser,	//add sub enemy
		geWait, 10,
		geEnemy, geEnemy120, -220, 600, 90, geItemNone,
		geEnemy, geEnemy120, 220, 600, 90, geItemNone,
		geWait, 10,
		geEnemy, geEnemy120, -220, 600, 90, geItemNone,
		geEnemy, geEnemy120, 220, 600, 90, geItemNone,
		geEnemy, geEnemy200, 0, 550, 270, geItemBomb,	//add sub enemy
		geWait, 10,
		geEnemy, geEnemy120, -220, 600, 90, geItemNone,
		geEnemy, geEnemy120, 220, 600, 90, geItemNone,
		geWait, 10,
		geEnemy, geEnemy120, -220, 600, 90, geItemNone,
		geEnemy, geEnemy120, 220, 600, 90, geItemNone,
		geWait, 10,
		geEnemy, geEnemy120, -220, 600, 90, geItemNone,
		geEnemy, geEnemy120, 220, 600, 90, geItemNone,
		geWait, 10,
		geEnemy, geEnemy120, -220, 600, 90, geItemNone,
		geEnemy, geEnemy120, 220, 600, 90, geItemNone,
		geEnemy, geEnemy200, -350, 500, 310, geItemOption,	//add sub enemy
		geEnemy, geEnemy200, 350, 500, 230, geItemShield,	//add sub enemy
		geWait, 10,
		geEnemy, geEnemy120, -220, 600, 90, geItemNone,
		geEnemy, geEnemy120, 220, 600, 90, geItemNone,
		geWait, 10,
		geEnemy, geEnemy120, -220, 600, 90, geItemNone,
		geEnemy, geEnemy120, 220, 600, 90, geItemNone,
		geWait, 10,
		geEnemy, geEnemy120, -200, 600, 90, geItemNone,
		geEnemy, geEnemy120, 200, 600, 90, geItemNone,
		geWait, 10,
		geEnemy, geEnemy120, -120, 600, 90, geItemNone,
		geEnemy, geEnemy120, 120, 600, 90, geItemNone,
		geEnemy, geEnemy200, -250, 550, 270, geItemPower,	//add sub enemy
		geEnemy, geEnemy200, 250, 550, 270, geItemLaser,	//add sub enemy
		geWait, 10,
		geEnemy, geEnemy120, -40, 600, 90, geItemNone,
		geEnemy, geEnemy120, 40, 600, 90, geItemNone,
		geWait, 10,
		geEnemy, geEnemy120, 0, 600, 90, geItemShield,
		geWait, 10,

		//#s1-2 #a12	#bomb*1 #power*1
		geWait, 100,
		geEnemy, geEnemy210, 200, 650, 270, 75, geItemBomb,
		geWait, 100,
		geEnemy, geEnemy210, -200, 650, 270, 105, geItemPower,
		geWait, 100,

		geNextArea,
		geTerm
	};

	//game event stage 1 area 3 table (position:*100)
	private int[] gameEventStage1Area3Table = new int[] {
		geMap, geMapStage, geMapStage1,
		geMap, geMapScrollMovexEnable,
		geMap, geMapScrollTarget, 8,
		geBgm, geBgmPlay, geBgm121, geBgmNormal,

		geWait, 80,

		//#s1-3 #middle boss1
		geWait, 20,
		geSE, geSePlay, geSe_base131, geSeForce,
		geMap, geMapScrollMovexDisable,
		geMap, geMapScrollTarget, 60,
		geWait, 50,
		geDisplay, geEhDisplayIn, geEhDisplayLv0,
		geWait, 80,
		geVoice, geVoicePlay, geVoice220, geVoiceForce,
		geWait, 35,
		geEnemy, geEnemy300, 0, -1,
		geWait, -1,	//enemy300 wait
		geMap, geMapScrollMovexEnable,
		geMap, geMapScrollTarget, 8,
		geWait,	60,
		geDisplay, geEhNoDisplay,
		geWait, 40,

		geNextArea,

		geTerm
	};

	//game event stage 1 area 4 table (position:*100)
	private int[] gameEventStage1Area4Table = new int[]{
		geMap, geMapStage, geMapStage1,
		geMap, geMapScrollMovexEnable,
		geMap, geMapScrollTarget, 8,
		geBgm, geBgmPlay, geBgm121, geBgmNormal,

		geWait, 50,

		//#s1-4 #b1
		geEnemy, geEnemy110, 400, 380, 225, 180, geItemNone,
		geWait, 4,
		geEnemy, geEnemy110, 400, 380, 225, 180, geItemNone,
		geWait, 4,
		geEnemy, geEnemy110, 400, 380, 225, 180, geItemNone,
		geWait, 4,
		geEnemy, geEnemy110, 400, 380, 225, 180, geItemNone,
		geEnemy, geEnemy100, -400, 480, 75, geItemNone,
		geWait, 4,
		geEnemy, geEnemy110, 400, 380, 225, 180, geItemNone,
		geEnemy, geEnemy100, -400, 480, 75, geItemNone,
		geWait, 4,
		geEnemy, geEnemy110, 400, 380, 225, 180, geItemNone,
		geEnemy, geEnemy100, -400, 480, 75, geItemNone,
		geEnemy, geEnemy110, -400, -12, 60, 270, geItemNone,
		geEnemy, geEnemy100, 400, 80, 200, geItemNone,
		geWait, 4,
		geEnemy, geEnemy110, 400, 380, 225, 180, geItemNone,
		geEnemy, geEnemy100, -400, 480, 75, geItemNone,
		geEnemy, geEnemy110, -400, -12, 60, 270, geItemNone,
		geEnemy, geEnemy100, 400, 80, 200, geItemNone,
		geWait, 4,
		geEnemy, geEnemy100, -400, 480, 75, geItemNone,
		geEnemy, geEnemy110, -400, -12, 60, 270, geItemNone,
		geEnemy, geEnemy100, 400, 80, 200, geItemNone,
		geWait, 4,
		geEnemy, geEnemy100, -400, 480, 75, geItemNone,
		geEnemy, geEnemy110, -400, -12, 60, 270, geItemNone,
		geEnemy, geEnemy100, 400, 80, 200, geItemNone,
		geWait, 4,
		geEnemy, geEnemy100, -400, 480, 75, geItemBomb,
		geEnemy, geEnemy110, -400, -12, 60, 270, geItemNone,
		geEnemy, geEnemy100, 400, 80, 200, geItemNone,
		geWait, 4,
		geEnemy, geEnemy110, -400, -12, 60, 270, geItemNone,
		geEnemy, geEnemy100, 400, 80, 200, geItemNone,
		geWait, 4,
		geEnemy, geEnemy110, -400, -12, 60, 270, geItemNone,
		geEnemy, geEnemy100, 400, 80, 200, geItemNone,
		geWait, 4,
		geEnemy, geEnemy110, -400, -12, 60, 270, geItemNone,
		geEnemy, geEnemy100, 400, 80, 200, geItemNone,
		geWait, 4,
		geEnemy, geEnemy100, 400, 80, 200, geItemNone,
		geWait, 4,
		geEnemy, geEnemy100, 400, 80, 200, geItemNone,
		geWait, 4,
		geEnemy, geEnemy100, 400, 80, 200, geItemNone,
		geWait, 4,
		geEnemy, geEnemy100, 400, 80, 200, geItemNone,
		geWait, 4,
		geEnemy, geEnemy100, 400, 80, 200, geItemNone,
		geWait, 4,

		geWait, 100,

		//#s1-4 #b2	#option*1 laser*1
		geEnemy, geEnemy210, 200, 650, 270, 75, geItemOption,
		geEnemy, geEnemy120, -40, 600, 270, geItemNone,		//add sub enemy
		geEnemy, geEnemy120, 40, 600, 270, geItemNone,		//add sub enemy
		geWait, 10,
		geEnemy, geEnemy120, 110, 600, 270, geItemNone,		//add sub enemy
		geEnemy, geEnemy120, 190, 600, 270, geItemNone,		//add sub enemy
		geWait, 10,
		geEnemy, geEnemy210, -200, 650, 270, 115, geItemLaser,
		geEnemy, geEnemy120, -190, 600, 270, geItemNone,	//add sub enemy
		geEnemy, geEnemy120, -110, 600, 270, geItemNone,	//add sub enemy
		geWait, 10,
		geEnemy, geEnemy120, 110, 600, 270, geItemNone,		//add sub enemy
		geEnemy, geEnemy120, 190, 600, 270, geItemNone,		//add sub enemy
		geWait, 10,
		geEnemy, geEnemy110, -150, 600, 270, 90, geItemNone,
		geEnemy, geEnemy120, -190, 600, 270, geItemNone,	//add sub enemy
		geEnemy, geEnemy120, -110, 600, 270, geItemNone,	//add sub enemy
		geWait, 10,
		geEnemy, geEnemy100, -180, 600, 270, geItemNone,
		geEnemy, geEnemy120, 110, 600, 270, geItemNone,		//add sub enemy
		geEnemy, geEnemy120, 190, 600, 270, geItemNone,		//add sub enemy
		geWait, 10,
		geEnemy, geEnemy100, -400, 350, 0, geItemNone,
		geEnemy, geEnemy120, -190, 600, 270, geItemNone,	//add sub enemy
		geEnemy, geEnemy120, -110, 600, 270, geItemNone,	//add sub enemy

		geWait, 10,
		geEnemy, geEnemy120, -190, 600, 270, geItemNone,	//add sub enemy
		geEnemy, geEnemy120, -110, 600, 270, geItemNone,	//add sub enemy
		geEnemy, geEnemy120, 110, 600, 270, geItemNone,		//add sub enemy
		geEnemy, geEnemy120, 190, 600, 270, geItemNone,		//add sub enemy
		geWait, 10,
		geEnemy, geEnemy120, -190, 600, 270, geItemNone,	//add sub enemy
		geEnemy, geEnemy120, -110, 600, 270, geItemNone,	//add sub enemy
		geEnemy, geEnemy120, 110, 600, 270, geItemNone,		//add sub enemy
		geEnemy, geEnemy120, 190, 600, 270, geItemNone,		//add sub enemy
		geWait, 10,
		geEnemy, geEnemy120, -40, 600, 270, geItemNone,		//add sub enemy
		geEnemy, geEnemy120, 40, 600, 270, geItemNone,		//add sub enemy
		geWait, 10,
		geEnemy, geEnemy120, -40, 600, 270, geItemNone,		//add sub enemy
		geEnemy, geEnemy120, 40, 600, 270, geItemNone,		//add sub enemy
		geWait, 10,
		geEnemy, geEnemy120, -40, 600, 270, geItemNone,		//add sub enemy
		geEnemy, geEnemy120, 40, 600, 270, geItemNone,		//add sub enemy

		geWait, 100,

		//#s1-4 #b3
		geEnemy, geEnemy210, -450, 280, 0, 270, geItemPower,
		geWait, 14,
		geEnemy, geEnemy110, -400, 280, 0, 270, geItemNone,
		geWait, 6,
		geEnemy, geEnemy110, -400, 280, 0, 270, geItemNone,
		geWait, 6,
		geEnemy, geEnemy110, -400, 280, 0, 270, geItemNone,
		geWait, 6,
		geEnemy, geEnemy110, -400, 280, 0, 270, geItemNone,
		geWait, 6,
		geEnemy, geEnemy110, -400, 280, 0, 270, geItemNone,
		geWait, 5,
		geEnemy, geEnemy210, -450, 280, 0, 270, geItemLaser,

		geWait, 100,

		//#s1-4 #b4	#bomb*1 shield*1 power*1
		geEnemy, geEnemy100, -400, 295, 45, geItemNone,
		geEnemy, geEnemy100, 400, 295, 135, geItemNone,
		geEnemy, geEnemy130, -400, 600, 70, geEne130_totarget, 100, 600, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 400, 600, 110, geEne130_totarget, -100, 600, geItemNone,	//add sub enemy
		geWait, 6,
		geEnemy, geEnemy100, -400, 280, 45, geItemNone,
		geEnemy, geEnemy100, -400, 310, 45, geItemNone,
		geEnemy, geEnemy100, 400, 280, 135, geItemNone,
		geEnemy, geEnemy100, 400, 310, 135, geItemNone,
		geWait, 6,
		geEnemy, geEnemy100, -400, 280, 45, geItemNone,
		geEnemy, geEnemy100, -400, 310, 45, geItemNone,
		geEnemy, geEnemy100, 400, 280, 135, geItemNone,
		geEnemy, geEnemy100, 400, 310, 135, geItemNone,
		geWait, 6,
		geEnemy, geEnemy100, -400, 280, 45, geItemNone,
		geEnemy, geEnemy100, -400, 310, 45, geItemNone,
		geEnemy, geEnemy100, 400, 280, 135, geItemNone,
		geEnemy, geEnemy100, 400, 310, 135, geItemNone,
		geEnemy, geEnemy130, -400, 600, 70, geEne130_totarget, 100, 500, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 400, 600, 110, geEne130_totarget, -100, 500, geItemNone,	//add sub enemy
		geWait, 6,
		geEnemy, geEnemy100, -400, 280, 45, geItemNone,
		geEnemy, geEnemy100, -400, 310, 45, geItemNone,
		geEnemy, geEnemy100, 400, 280, 135, geItemNone,
		geEnemy, geEnemy100, 400, 310, 135, geItemNone,
		geWait, 6,
		geEnemy, geEnemy100, -400, 340, 45, geItemNone,
		geEnemy, geEnemy100, -400, 300, 45, geItemNone,
		geEnemy, geEnemy100, -400, 260, 45, geItemNone,
		geEnemy, geEnemy100, 400, 340, 135, geItemNone,
		geEnemy, geEnemy100, 400, 300, 135, geItemNone,
		geEnemy, geEnemy100, 400, 260, 135, geItemNone,
		geWait, 6,
		geEnemy, geEnemy100, -400, 340, 45, geItemNone,
		geEnemy, geEnemy100, -400, 300, 45, geItemNone,
		geEnemy, geEnemy100, -400, 260, 45, geItemNone,
		geEnemy, geEnemy100, 400, 340, 135, geItemNone,
		geEnemy, geEnemy100, 400, 300, 135, geItemNone,
		geEnemy, geEnemy100, 400, 260, 135, geItemNone,
		geEnemy, geEnemy130, -400, 600, 70, geEne130_totarget, 100, 400, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 400, 600, 110, geEne130_totarget, -100, 400, geItemNone,	//add sub enemy
		geWait, 6,
		geEnemy, geEnemy100, -400, 340, 45, geItemNone,
		geEnemy, geEnemy100, -400, 300, 45, geItemNone,
		geEnemy, geEnemy100, -400, 260, 45, geItemNone,
		geEnemy, geEnemy100, 400, 340, 135, geItemNone,
		geEnemy, geEnemy100, 400, 300, 135, geItemNone,
		geEnemy, geEnemy100, 400, 260, 135, geItemNone,
		geWait, 6,
		geEnemy, geEnemy100, -400, 340, 45, geItemNone,
		geEnemy, geEnemy100, -400, 300, 45, geItemNone,
		geEnemy, geEnemy100, -400, 260, 45, geItemNone,
		geEnemy, geEnemy100, 400, 340, 135, geItemNone,
		geEnemy, geEnemy100, 400, 300, 135, geItemNone,
		geEnemy, geEnemy100, 400, 260, 135, geItemNone,
		geWait, 6,
		geEnemy, geEnemy100, -400, 340, 45, geItemNone,
		geEnemy, geEnemy100, -400, 300, 45, geItemNone,
		geEnemy, geEnemy100, -400, 260, 45, geItemNone,
		geEnemy, geEnemy100, 400, 340, 135, geItemNone,
		geEnemy, geEnemy100, 400, 300, 135, geItemNone,
		geEnemy, geEnemy100, 400, 260, 135, geItemNone,
		geEnemy, geEnemy130, -400, 600, 70, geEne130_totarget, 100, 300, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 400, 600, 110, geEne130_totarget, -100, 300, geItemNone,	//add sub enemy
		geWait, 6,
		geEnemy, geEnemy100, -400, 340, 45, geItemNone,
		geEnemy, geEnemy100, -400, 300, 45, geItemNone,
		geEnemy, geEnemy100, -400, 260, 45, geItemNone,
		geEnemy, geEnemy100, 400, 340, 135, geItemNone,
		geEnemy, geEnemy100, 400, 300, 135, geItemNone,
		geEnemy, geEnemy100, 400, 260, 135, geItemNone,
		geEnemy, geEnemy130, -230, 600, 270, geEne130_forward, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 230, 600, 270, geEne130_forward, 0, 0, geItemPower,	//add sub enemy
		geWait, 6,
		geEnemy, geEnemy100, -400, 340, 45, geItemNone,
		geEnemy, geEnemy100, -400, 300, 45, geItemNone,
		geEnemy, geEnemy100, -400, 260, 45, geItemNone,
		geEnemy, geEnemy100, 400, 340, 135, geItemNone,
		geEnemy, geEnemy100, 400, 300, 135, geItemNone,
		geEnemy, geEnemy100, 400, 260, 135, geItemNone,
		geWait, 6,
		geEnemy, geEnemy100, -400, 340, 45, geItemNone,
		geEnemy, geEnemy100, -400, 300, 45, geItemNone,
		geEnemy, geEnemy100, -400, 260, 45, geItemNone,
		geEnemy, geEnemy100, 400, 340, 135, geItemNone,
		geEnemy, geEnemy100, 400, 300, 135, geItemNone,
		geEnemy, geEnemy100, 400, 260, 135, geItemNone,
		geEnemy, geEnemy130, -400, 600, 70, geEne130_totarget, 100, 200, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 400, 600, 110, geEne130_totarget, -100, 200, geItemNone,	//add sub enemy
		geWait, 8,
		geEnemy, geEnemy100, -380, 600, 270, geItemBomb,
		geEnemy, geEnemy100, 380, 600, 270, geItemShield,
		geWait, 6,

		geWait, 110,
		geVoice, geVoicePlay, geVoice220, geVoiceForce,	//stage1 peak
		geWait, 30,

		//#s1-4 #b5	#optione*1 laser*1 (stage1 peak)
		geEnemy, geEnemy210, -450, -640, 70, 135, geItemPower,
		geWait, 4,
		geEnemy, geEnemy210, 450, -640, 110, 45, geItemOption,
		geEnemy, geEnemy200, 0, 650, 270, geItemLaser,
		geWait, 14,
//		geEnemy, geEnemy210, 200, 650, 270, 270, geItemNone,
		geEnemy, geEnemy200, -230, 650, 270, geItemBomb,
		geWait, 8,
//		geEnemy, geEnemy210, -200, 650, 270, 270, geItemNone,
		geEnemy, geEnemy200, 230, 650, 270, geItemShield,
		geWait, 10,
		geEnemy, geEnemy210, -450, 640, 315, 270, geItemOption,
		geEnemy, geEnemy210, 450, 640, 225, 270, geItemLaser,
		geEnemy, geEnemy200, 0, 650, 270, geItemPower,
		geWait, 4,

		geWait, 110,

		//#s1-4 #b6	#shield*1 bomb*1
		geEnemy, geEnemy130, -120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, -40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geWait, 12,
		geEnemy, geEnemy130, -120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, -40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geWait, 12,
		geEnemy, geEnemy110, -350, 600, 280, 270, geItemNone,
		geEnemy, geEnemy130, -120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, -40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geWait, 12,
		geEnemy, geEnemy110, -350, 600, 280, 270, geItemNone,
		geEnemy, geEnemy110, -300, 600, 280, 270, geItemNone,
		geEnemy, geEnemy130, -120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, -40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 120, 600, 45, geEne130_stop, 0, 0, geItemShield,	//add sub enemy
		geWait, 12,
		geEnemy, geEnemy110, -300, 600, 280, 270, geItemNone,
		geEnemy, geEnemy110, -250, 600, 280, 270, geItemNone,
		geEnemy, geEnemy130, -120, 600, 45, geEne130_stop, 0, 0, geItemPower,	//add sub enemy
		geEnemy, geEnemy130, -40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geWait, 12,
		geEnemy, geEnemy110, -250, 600, 280, 270, geItemNone,
		geEnemy, geEnemy110, -200, 600, 280, 270, geItemNone,
		geEnemy, geEnemy130, -120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, -40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geWait, 12,
		geEnemy, geEnemy110, -200, 600, 280, 270, geItemNone,
		geEnemy, geEnemy110, -150, 600, 280, 270, geItemNone,
		geEnemy, geEnemy130, -120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, -40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geWait, 12,
		geEnemy, geEnemy110, -150, 600, 280, 270, geItemNone,
		geEnemy, geEnemy110, -100, 600, 280, 270, geItemNone,
		geEnemy, geEnemy130, -120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, -40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 120, 600, 45, geEne130_stop, 0, 0, geItemBomb,	//add sub enemy
		geWait, 12,
		geEnemy, geEnemy110, -100, 600, 280, 270, geItemNone,
		geEnemy, geEnemy110, -50, 600, 280, 180, geItemNone,
		geEnemy, geEnemy130, -120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, -40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geWait, 12,
		geEnemy, geEnemy110, -50, 600, 280, 180, geItemNone,
		geEnemy, geEnemy110, 0, 600, 270, 270, geItemNone,
		geEnemy, geEnemy130, -120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, -40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geWait, 12,
		geEnemy, geEnemy110, 0, 600, 270, 270, geItemNone,
		geEnemy, geEnemy110, 50, 600, 260, 270, geItemNone,
		geEnemy, geEnemy130, -120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, -40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geWait, 12,
		geEnemy, geEnemy110, 50, 600, 260, 270, geItemNone,
		geEnemy, geEnemy110, 100, 600, 260, 270, geItemNone,
		geEnemy, geEnemy130, -120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, -40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geWait, 12,
		geEnemy, geEnemy110, 100, 600, 260, 270, geItemNone,
		geEnemy, geEnemy110, 150, 600, 260, 270, geItemNone,
		geEnemy, geEnemy130, -120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, -40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geWait, 12,
		geEnemy, geEnemy110, 150, 600, 260, 270, geItemNone,
		geEnemy, geEnemy110, 200, 600, 260, 270, geItemNone,
		geEnemy, geEnemy130, -120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, -40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geWait, 12,
		geEnemy, geEnemy110, 200, 600, 260, 270, geItemNone,
		geEnemy, geEnemy110, 250, 600, 260, 270, geItemNone,
		geEnemy, geEnemy130, -120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, -40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geWait, 6,
		geEnemy, geEnemy100, -400, -280, 80, geItemNone,
		geWait, 6,
		geEnemy, geEnemy110, 250, 600, 260, 270, geItemNone,
		geEnemy, geEnemy110, 300, 600, 260, 270, geItemNone,
		geEnemy, geEnemy100, -400, -260, 80, geItemNone,
		geEnemy, geEnemy130, -120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, -40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 120, 600, 45, geEne130_stop, 0, 0, geItemLaser,	//add sub enemy
		geWait, 6,
		geEnemy, geEnemy100, -400, -240, 80, geItemNone,
		geWait, 6,
		geEnemy, geEnemy110, 300, 600, 260, 270, geItemNone,
		geEnemy, geEnemy110, 350, 600, 260, 270, geItemShield,
		geEnemy, geEnemy100, -400, -220, 80, geItemNone,
		geEnemy, geEnemy130, -120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, -40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geWait, 6,
		geEnemy, geEnemy100, -400, -200, 80, geItemNone,
		geWait, 6,
		geEnemy, geEnemy100, -400, -180, 80, geItemNone,
		geEnemy, geEnemy130, -120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, -40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geWait, 6,
		geEnemy, geEnemy100, -400, -160, 80, geItemNone,
		geWait, 6,
		geEnemy, geEnemy100, -400, -140, 80, geItemNone,
		geEnemy, geEnemy130, -120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, -40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 120, 600, 45, geEne130_stop, 0, 0, geItemShield,	//add sub enemy
		geWait, 6,
		geEnemy, geEnemy100, -400, -120, 80, geItemNone,
		geWait, 6,
		geEnemy, geEnemy100, -400, -100, 80, geItemNone,
		geEnemy, geEnemy130, -120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, -40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geWait, 6,
		geEnemy, geEnemy100, -400, -80, 80, geItemNone,
		geWait, 6,
		geEnemy, geEnemy100, -400, -60, 80, geItemNone,
		geEnemy, geEnemy130, -120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, -40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geWait, 6,
		geEnemy, geEnemy100, -400, -40, 80, geItemNone,
		geWait, 6,
		geEnemy, geEnemy100, -400, -20, 80, geItemNone,
		geEnemy, geEnemy130, -120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, -40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geWait, 6,
		geEnemy, geEnemy100, -400, 0, 80, geItemNone,
		geWait, 6,
		geEnemy, geEnemy100, -400, 20, 80, geItemNone,
		geEnemy, geEnemy130, -120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, -40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geWait, 6,
		geEnemy, geEnemy100, -400, 40, 80, geItemNone,
		geWait, 6,
		geEnemy, geEnemy100, -400, 60, 80, geItemNone,
		geEnemy, geEnemy130, -120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, -40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geWait, 6,
		geEnemy, geEnemy100, -400, 80, 80, geItemNone,
		geWait, 6,
		geEnemy, geEnemy100, -400, 100, 80, geItemNone,
		geEnemy, geEnemy130, -120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, -40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geWait, 6,
		geEnemy, geEnemy100, -400, 120, 80, geItemNone,
		geWait, 6,
		geEnemy, geEnemy100, -400, 140, 80, geItemNone,
		geEnemy, geEnemy130, -120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, -40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geWait, 6,
		geEnemy, geEnemy100, -400, 160, 80, geItemNone,
		geWait, 6,
		geEnemy, geEnemy100, -400, 180, 80, geItemNone,
		geEnemy, geEnemy130, -120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, -40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geWait, 6,
		geEnemy, geEnemy100, -400, 200, 80, geItemNone,
		geWait, 6,
		geEnemy, geEnemy100, -400, 220, 80, geItemNone,
		geEnemy, geEnemy130, -120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, -40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 40, 600, 45, geEne130_stop, 0, 0, geItemBomb,	//add sub enemy
		geEnemy, geEnemy130, 120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geWait, 6,
		geEnemy, geEnemy100, -400, 240, 80, geItemNone,
		geWait, 6,
		geEnemy, geEnemy100, -400, 260, 80, geItemNone,
		geEnemy, geEnemy130, -120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, -40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geWait, 6,
		geEnemy, geEnemy100, -400, 280, 80, geItemNone,
		geWait, 6,
		geEnemy, geEnemy130, -120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, -40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geWait, 12,
		geEnemy, geEnemy130, -120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, -40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geWait, 12,
		geEnemy, geEnemy130, -120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, -40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geWait, 12,
		geEnemy, geEnemy130, -120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, -40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 40, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geEnemy, geEnemy130, 120, 600, 45, geEne130_stop, 0, 0, geItemNone,	//add sub enemy
		geWait, 12,

		geWait, 100,

		//#s1-4 #b7
		geEnemy, geEnemy200, -250, 650, 270, geItemOption,
		geEnemy, geEnemy200, 0, 650, 270, geItemShield,
		geEnemy, geEnemy200, 250, 650, 270, geItemPower,
		geWait, 40,
		geEnemy, geEnemy200, -250, 640, 270, geItemOption,
		geEnemy, geEnemy200, 0, 640, 270, geItemPower,
		geEnemy, geEnemy200, 250, 640, 270, geItemLaser,
		geWait, 40,
		geEnemy, geEnemy200, -250, 640, 270, geItemShield,
		geEnemy, geEnemy200, 0, 640, 270, geItemPower,
		geEnemy, geEnemy200, 250, 640, 270, geItemLaser,
		geWait, 20,

		geWait, 80,

		//#s1-4 #b8
		geEnemy, geEnemy210, 200, 650, 270, 75, geItemPower,
		geEnemy, geEnemy210, -200, 650, 270, 115, geItemShield,
		geWait, 18,
		geEnemy, geEnemy110, -150, 600, 270, 90, geItemNone,
		geWait, 8,
		geEnemy, geEnemy100, -180, 600, 270, geItemNone,
		geWait, 8,
		geEnemy, geEnemy100, -400, 350, 0, geItemNone,
		geWait, 50,

		geWait, 80,

		//#s1-4 #b9	#bomb*2
		geEnemy, geEnemy130, 200, 600, 225, geEne130_escape, 0, 0, geItemPower,	//add sub enemy
		geWait, 12,
		geEnemy, geEnemy130, 200, 600, 225, geEne130_escape, 0, 0, geItemBomb,	//add sub enemy
		geWait, 12,
		geEnemy, geEnemy110, -400, 350, 0, 270, geItemNone,
		geEnemy, geEnemy110, -400, 300, 0, 270, geItemNone,
		geEnemy, geEnemy110, -400, 250, 0, 270, geItemNone,
		geEnemy, geEnemy110, -400, 200, 0, 270, geItemNone,
		geEnemy, geEnemy130, 200, 600, 225, geEne130_escape, 0, 0, geItemNone,	//add sub enemy
		geWait, 6,
		geEnemy, geEnemy110, -400, 350, 0, 270, geItemNone,
		geEnemy, geEnemy110, -400, 300, 0, 270, geItemNone,
		geEnemy, geEnemy110, -400, 250, 0, 270, geItemNone,
		geEnemy, geEnemy110, -400, 200, 0, 270, geItemNone,
		geWait, 6,
		geEnemy, geEnemy110, -400, 350, 0, 270, geItemNone,
		geEnemy, geEnemy110, -400, 300, 0, 270, geItemNone,
		geEnemy, geEnemy110, -400, 250, 0, 270, geItemNone,
		geEnemy, geEnemy110, -400, 200, 0, 270, geItemNone,
		geEnemy, geEnemy130, 200, 600, 225, geEne130_escape, 0, 0, geItemShield,	//add sub enemy
		geWait, 6,
		geEnemy, geEnemy110, -400, 350, 0, 270, geItemNone,
		geEnemy, geEnemy110, -400, 300, 0, 270, geItemNone,
		geEnemy, geEnemy110, -400, 250, 0, 270, geItemNone,
		geEnemy, geEnemy110, -400, 200, 0, 270, geItemNone,
		geWait, 6,
		geEnemy, geEnemy110, -400, 350, 0, 270, geItemNone,
		geEnemy, geEnemy110, -400, 300, 0, 270, geItemNone,
		geEnemy, geEnemy110, -400, 250, 0, 270, geItemNone,
		geEnemy, geEnemy110, -400, 200, 0, 270, geItemNone,
		geEnemy, geEnemy130, 200, 600, 225, geEne130_escape, 0, 0, geItemNone,	//add sub enemy
		geWait, 6,
		geEnemy, geEnemy110, -400, 350, 0, 270, geItemNone,
		geEnemy, geEnemy110, -400, 300, 0, 270, geItemNone,
		geEnemy, geEnemy110, -400, 250, 0, 270, geItemNone,
		geEnemy, geEnemy110, -400, 200, 0, 270, geItemNone,
		geWait, 6,
		geEnemy, geEnemy110, -400, 350, 0, 270, geItemNone,
		geEnemy, geEnemy110, -400, 300, 0, 270, geItemNone,
		geEnemy, geEnemy110, -400, 250, 0, 270, geItemNone,
		geEnemy, geEnemy110, -400, 200, 0, 270, geItemNone,
		geEnemy, geEnemy130, 200, 600, 225, geEne130_escape, 0, 0, geItemPower,	//
		geWait, 6,
		geEnemy, geEnemy110, -400, 350, 0, 270, geItemNone,
		geEnemy, geEnemy110, -400, 300, 0, 270, geItemNone,
		geEnemy, geEnemy110, -400, 250, 0, 270, geItemNone,
		geEnemy, geEnemy110, -400, 200, 0, 270, geItemNone,
		geWait, 6,
		geEnemy, geEnemy110, -400, 350, 0, 270, geItemNone,
		geEnemy, geEnemy110, -400, 300, 0, 270, geItemNone,
		geEnemy, geEnemy110, -400, 250, 0, 270, geItemNone,
		geEnemy, geEnemy110, -400, 200, 0, 270, geItemNone,
		geEnemy, geEnemy130, 200, 600, 225, geEne130_escape, 0, 0, geItemNone,	//
		geWait, 6,
		geEnemy, geEnemy110, -400, 350, 0, 270, geItemNone,
		geEnemy, geEnemy110, -400, 300, 0, 270, geItemNone,
		geEnemy, geEnemy110, -400, 250, 0, 270, geItemNone,
		geEnemy, geEnemy110, -400, 200, 0, 270, geItemNone,
		geWait, 6,
		geEnemy, geEnemy110, -400, 350, 0, 270, geItemNone,
		geEnemy, geEnemy110, -400, 300, 0, 270, geItemNone,
		geEnemy, geEnemy110, -400, 250, 0, 270, geItemNone,
		geEnemy, geEnemy110, -400, 200, 0, 270, geItemNone,
		geEnemy, geEnemy130, 200, 600, 225, geEne130_escape, 0, 0, geItemNone,	//
		geWait, 6,
		geEnemy, geEnemy110, -400, 350, 0, 270, geItemNone,
		geEnemy, geEnemy110, -400, 300, 0, 270, geItemNone,
		geEnemy, geEnemy110, -400, 250, 0, 270, geItemNone,
		geEnemy, geEnemy110, -400, 200, 0, 270, geItemNone,
		geWait, 6,
		geEnemy, geEnemy110, -400, 350, 0, 270, geItemNone,
		geEnemy, geEnemy110, -400, 300, 0, 270, geItemNone,
		geEnemy, geEnemy110, -400, 250, 0, 270, geItemNone,
		geEnemy, geEnemy110, -400, 200, 0, 270, geItemNone,
		geEnemy, geEnemy130, 200, 600, 225, geEne130_escape, 0, 0, geItemLaser,	//
		geWait, 6,
		geEnemy, geEnemy110, -400, 350, 0, 270, geItemNone,
		geEnemy, geEnemy110, -400, 300, 0, 270, geItemNone,
		geEnemy, geEnemy110, -400, 250, 0, 270, geItemNone,
		geEnemy, geEnemy110, -400, 200, 0, 270, geItemNone,
		geWait, 6,
		geEnemy, geEnemy110, -400, 350, 0, 270, geItemNone,
		geEnemy, geEnemy110, -400, 300, 0, 270, geItemNone,
		geEnemy, geEnemy110, -400, 250, 0, 270, geItemNone,
		geEnemy, geEnemy110, -400, 200, 0, 270, geItemNone,
		geEnemy, geEnemy130, 200, 600, 225, geEne130_escape, 0, 0, geItemNone,	//
		geWait, 6,
		geEnemy, geEnemy110, -400, 350, 0, 270, geItemNone,
		geEnemy, geEnemy110, -400, 300, 0, 270, geItemNone,
		geEnemy, geEnemy110, -400, 250, 0, 270, geItemNone,
		geEnemy, geEnemy110, -400, 200, 0, 270, geItemNone,
		geWait, 6,
		geEnemy, geEnemy110, -400, 350, 0, 270, geItemNone,
		geEnemy, geEnemy110, -400, 300, 0, 270, geItemNone,
		geEnemy, geEnemy110, -400, 250, 0, 270, geItemNone,
		geEnemy, geEnemy110, -400, 200, 0, 270, geItemNone,
		geEnemy, geEnemy130, 200, 600, 225, geEne130_escape, 0, 0, geItemOption,	//
		geWait, 6,
		geEnemy, geEnemy110, -400, 350, 0, 270, geItemNone,
		geEnemy, geEnemy110, -400, 300, 0, 270, geItemNone,
		geEnemy, geEnemy110, -400, 250, 0, 270, geItemNone,
		geEnemy, geEnemy110, -400, 200, 0, 270, geItemNone,
		geWait, 6,
		geEnemy, geEnemy130, 200, 600, 225, geEne130_escape, 0, 0, geItemNone,	//
		geWait, 12,
		geEnemy, geEnemy130, 200, 600, 225, geEne130_escape, 0, 0, geItemNone,	//
		geWait, 12,
		geEnemy, geEnemy130, 200, 600, 225, geEne130_escape, 0, 0, geItemNone,	//
		geWait, 12,
		geEnemy, geEnemy130, 200, 600, 225, geEne130_escape, 0, 0, geItemNone,	//

		geWait, 60,

		geNextArea,

		geTerm
	};

	//game event stage 1 area 5 table (position:*100)
	private int[] gameEventStage1Area5Table = new int[]{
		geMap, geMapStage, geMapStage1,
		geMap, geMapScrollMovexEnable,
		geMap, geMapScrollTarget, 8,
		geBgm, geBgmPlay, geBgm121, geBgmNormal,

		geWait, 50,

		geSE, geSePlay, geSe_base131, geSeForce,
		geMap, geMapScrollMovexDisable,
		geMap, geMapScrollTarget, 60,
		geWait, 40,

		geBgm, geBgmFadeout,
		geWait, 110,
		geDisplay, geMainMessageDisplay, geMainMessageWarning, geMainMessageDisplayTime, 26, 130,
		geVoice, geVoicePlay, geVoice170, geVoiceForce,
		geWait,	25,
		geDisplay, geEhDisplayIn, geEhDisplayLv1,
		geBgm, geBgmPlay, geBgm502, geBgmForce,
		geWait,	50,
		geVoice, geVoicePlay, geVoice190, geVoiceForce,
		geWait,	20,

		geMap, geMapScrollTarget, 5,
		geWait, 105,

		//#s1-5 #c1
		geWait, 20,
		geEnemy, geEnemy130, -260, 600, 80, geEne130_stop, 0, 0, geItemNone,
		geEnemy, geEnemy130, -80, 600, 45, geEne130_stop, 0, 0, geItemNone,
		geEnemy, geEnemy130, 80, 600, 135, geEne130_stop, 0, 0, geItemNone,
		geEnemy, geEnemy130, 260, 600, 190, geEne130_stop, 0, 0, geItemNone,
		geWait, 20,
		geEnemy, geEnemy130, -240, 600, 80, geEne130_stop, 0, 0, geItemNone,
		geEnemy, geEnemy130, -70, 600, 45, geEne130_stop, 0, 0, geItemNone,
		geEnemy, geEnemy130, 70, 600, 135, geEne130_stop, 0, 0, geItemNone,
		geEnemy, geEnemy130, 240, 600, 190, geEne130_stop, 0, 0, geItemNone,
		geWait, 20,
		geEnemy, geEnemy130, -220, 600, 80, geEne130_stop, 0, 0, geItemNone,
		geEnemy, geEnemy130, -60, 600, 45, geEne130_stop, 0, 0, geItemNone,
		geEnemy, geEnemy130, 60, 600, 135, geEne130_stop, 0, 0, geItemNone,
		geEnemy, geEnemy130, 220, 600, 190, geEne130_stop, 0, 0, geItemNone,
		geWait, 30,
		geEnemy, geEnemy120, -240, 600, 270, geItemNone,
		geEnemy, geEnemy120, -160, 600, 270, geItemNone,
		geEnemy, geEnemy120, -80, 600, 270, geItemNone,
		geEnemy, geEnemy120, -0, 600, 270, geItemNone,
		geEnemy, geEnemy120, 80, 600, 270, geItemNone,
		geEnemy, geEnemy120, 160, 600, 270, geItemNone,
		geEnemy, geEnemy120, 240, 600, 270, geItemNone,
		geWait, 15,
		geEnemy, geEnemy120, -280, 600, 270, geItemNone,
		geEnemy, geEnemy120, -200, 600, 270, geItemNone,
		geEnemy, geEnemy120, -120, 600, 270, geItemNone,
		geEnemy, geEnemy120, -40, 600, 270, geItemNone,
		geEnemy, geEnemy120, 40, 600, 270, geItemNone,
		geEnemy, geEnemy120, 120, 600, 270, geItemNone,
		geEnemy, geEnemy120, 200, 600, 270, geItemNone,
		geEnemy, geEnemy120, 280, 600, 270, geItemNone,
		geWait, 15,
		geEnemy, geEnemy120, -240, 600, 270, geItemNone,
		geEnemy, geEnemy120, -160, 600, 270, geItemNone,
		geEnemy, geEnemy120, -80, 600, 270, geItemNone,
		geEnemy, geEnemy120, -0, 600, 270, geItemNone,
		geEnemy, geEnemy120, 80, 600, 270, geItemNone,
		geEnemy, geEnemy120, 160, 600, 270, geItemNone,
		geEnemy, geEnemy120, 240, 600, 270, geItemNone,
		geWait, 15,
		geEnemy, geEnemy120, -280, 600, 270, geItemNone,
		geEnemy, geEnemy120, -200, 600, 270, geItemNone,
		geEnemy, geEnemy120, -120, 600, 270, geItemNone,
		geEnemy, geEnemy120, -40, 600, 270, geItemNone,
		geEnemy, geEnemy120, 40, 600, 270, geItemNone,
		geEnemy, geEnemy120, 120, 600, 270, geItemNone,
		geEnemy, geEnemy120, 200, 600, 270, geItemNone,
		geEnemy, geEnemy120, 280, 600, 270, geItemNone,
		geWait, 15,
		geEnemy, geEnemy120, -240, 600, 270, geItemNone,
		geEnemy, geEnemy120, -160, 600, 270, geItemNone,
		geEnemy, geEnemy120, -80, 600, 270, geItemNone,
		geEnemy, geEnemy120, -0, 600, 270, geItemNone,
		geEnemy, geEnemy120, 80, 600, 270, geItemNone,
		geEnemy, geEnemy120, 160, 600, 270, geItemNone,
		geEnemy, geEnemy120, 240, 600, 270, geItemNone,

		geWait, 30,

		//#s1-5 #boss1
		geEnemy, geEnemy500, geItemNone,

		geWait,	-1,	//enemy500 wait

		geBgm, geBgmFadeout,

		geWait,	30,
		geDisplay, geEhNoDisplay,
		geMap, geMapScrollTarget, 80,

		geWait,	10,

		geNextArea,
		geTerm
	};

	//game event stage 1 area 6 table (position:*100)
	private int[] gameEventStage1Area6Table = new int[]{
		geMap, geMapStage, geMapStage1,
		geMap, geMapScrollMovexDisable,
		geMap, geMapScrollTarget, 80,
		geWait,	10,
		geDisplay, geMainMessageDisplay, geMainMessageStageClear, geMainMessageDisplayStart, 18, 1,

		geWait,	90,

		geBgm, geBgmPlay, geBgm110, geBgmForce,
		geWait,	70,

		geDisplay, geResultDisplayStart,
		geWait,	-1,	//result display wait

		geBgm, geBgmFadeout,
		geDisplay, geMainMessageDisplay, geMainMessageDelete, geMainMessageDisplayDelete, 1, 1,

		geWait,	20,

		gePlayer, geSetPlayerMode, gePlayerModeNoExist,

		geScreenMask, geMaskFadeout,
		geWait, -1,	//screen fadeout wait
		geScreenMask, geMask,

		geNextStage,

		geTerm
	};

}
