using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundController : MonoBehaviour {
	//public
	//bgm clip
	//public audio bgm bgm100
	public AudioClip bgm100;
	//public audio bgm bgm500
	public AudioClip bgm500;
	//public audio bgm bgm501
	public AudioClip bgm501;
	//public audio bgm bgm800
	public AudioClip bgm800;
	//public audio bgm bgm110
	public AudioClip bgm110;
	//public audio bgm bgm120
	public AudioClip bgm120;
	//public audio bgm bgm130
	public AudioClip bgm130;
	//public audio bgm bgm131
	public AudioClip bgm131;
	//public audio bgm bgm132
	public AudioClip bgm132;
	//public audio bgm bgm133
	public AudioClip bgm133;
	//public audio bgm bgm140
	public AudioClip bgm140;
	//public audio bgm bgm141
	public AudioClip bgm141;
	//public audio bgm bgm142
	public AudioClip bgm142;
	//public audio bgm bgm143
	public AudioClip bgm143;
	//public audio bgm bgm144
	public AudioClip bgm144;
	//public audio bgm bgm152
	public AudioClip bgm152;
	//public audio bgm bgm153
	public AudioClip bgm153;
	//public audio bgm bgm154
	public AudioClip bgm154;
	//public audio bgm bgm162
	public AudioClip bgm162;
	//public audio bgm bgm163
	public AudioClip bgm163;
	//public audio bgm bgm164
	public AudioClip bgm164;
	//public audio bgm bgm172
	public AudioClip bgm172;
	//public audio bgm bgm174
	public AudioClip bgm174;
	//public audio bgm bgm185
	public AudioClip bgm185;
	//public audio bgm bgm101
	public AudioClip bgm101;
	//public audio bgm bgm121
	public AudioClip bgm121;
	//public audio bgm bgm502
	public AudioClip bgm502;
	//public audio bgm bgm801
	public AudioClip bgm801;
	//voice clip
	//public audio se voice100
	public AudioClip voice100;
	//public audio se voice101
	public AudioClip voice101;
	//public audio se voice110
	public AudioClip voice110;
	//public audio se voice120
	public AudioClip voice120;
	//public audio se voice121
	public AudioClip voice121;
	//public audio se voice130
	public AudioClip voice130;
	//public audio se voice140
	public AudioClip voice140;
	//public audio se voice150
	public AudioClip voice150;
	//public audio se voice160
	public AudioClip voice160;
	//public audio se voice170
	public AudioClip voice170;
	//public audio se voice180
	public AudioClip voice180;
	//public audio se voice190
	public AudioClip voice190;
	//public audio se voice200
	public AudioClip voice200;
	//public audio se voice210
	public AudioClip voice210;
	//public audio se voice220
	public AudioClip voice220;
	//public audio se voice230
	public AudioClip voice230;
	//public audio se voice240
	public AudioClip voice240;
	//public audio se voice250
	public AudioClip voice250;
	//public audio se voice260
	public AudioClip voice260;
	//public audio se voice270
	public AudioClip voice270;
	//public audio se voice280
	public AudioClip voice280;
	//se clip
	//(title/menu)
	//public audio se title 100
	public AudioClip se_title100;
	//public audio se title select 100
	public AudioClip se_titlesel100;
	//public audio se title select 110
	public AudioClip se_titlesel110;
	//public audio se title 101
	public AudioClip se_title101;
	//public audio se title select 101
	public AudioClip se_titlesel101;
	//public audio se title select 111
	public AudioClip se_titlesel111;
	//public audio se shot 100
	public AudioClip se_shot100;
	//(base)
	//public audio se base 100
	public AudioClip se_base100;
	//public audio se base 110
	public AudioClip se_base110;
	//public audio se base 120
	public AudioClip se_base120;
	//public audio se base 130
	public AudioClip se_base130;
	//public audio se base 131
	public AudioClip se_base131;
	//(player)
	//public audio se star 100
	public AudioClip se_star100;
	//public audio se shot 112
	public AudioClip se_shot112;
	//public audio se shot 110
	public AudioClip se_shot110;
	//public audio se shot 111
	public AudioClip se_shot111;
	//public audio se damage 110
	public AudioClip se_damege110;
	//public audio se explosion 100
	public AudioClip se_explosion100;
	//public audio se bomb laser 100
	public AudioClip se_bombLaser100;
	//(enemy)
	//public audio se damage 100
	public AudioClip se_damage100;
	//public audio se enemy bullet 102
	public AudioClip se_enemyBullet102;
	//public audio se explosion 110
	public AudioClip se_explosion110;
	//public audio se explosion 120
	public AudioClip se_explosion120;
	//public audio se tank 100
	public AudioClip se_tank100;
	//public audio se tank 110
	public AudioClip se_tank110;
	//public audio se tank 120
	public AudioClip se_tank120;

	//system public
	//system const
	//public bgm
	public int b100 = 0x00;
	public int b500 = 0x01;
	public int b501 = 0x02;
	public int b800 = 0x03;
	public int b110 = 0x04;	//result display
	public int b120 = 0x05;
	public int b101 = 0x06;
	public int b121 = 0x07;	//stage1
	public int b502 = 0x08;	//boss
	public int b801 = 0x09;	//title screen
	public int b130 = 0x0a;
	public int b131 = 0x0b;
	public int b132 = 0x0c;
	public int b133 = 0x0d;
	public int b140 = 0x0e;
	public int b141 = 0x0f;
	public int b142 = 0x10;
	public int b143 = 0x11;	//stage2
	public int b144 = 0x12;
	public int b152 = 0x13;	//boss3-1
	public int b153 = 0x14;
	public int b154 = 0x15;
	public int b162 = 0x16;
	public int b163 = 0x17;	//all clear
	public int b164 = 0x18;
	public int b172 = 0x19;
	public int b174 = 0x1a;
	public int b185 = 0x1b;	//stage3
	//public voice
	public int vo100 = 0x80;	//title call
	public int vo101 = 0x81;	//title call echo
	public int vo110 = 0x82;	//tap description(日本語)
	public int vo120 = 0x83;	//tap description
	public int vo130 = 0x84;	//power up description
	public int vo140 = 0x85;	//power up
	public int vo150 = 0x86;	//player damage
	public int vo160 = 0x87;
	public int vo170 = 0x88;	//warning*2
	public int vo180 = 0x89;
	public int vo190 = 0x8a;
	public int vo200 = 0x8b;
	public int vo210 = 0x8c;
	public int vo220 = 0x8d;
	public int vo230 = 0x8e;	//title descroption (no distortion)
	public int vo240 = 0x8f;	//please wait moment
	public int vo121 = 0x90;	//tap description long
	public int vo250 = 0x91;	//title descroption add s.w. p.type(no distortion)
	public int vo260 = 0x92;
	public int vo270 = 0x93;
	public int vo280 = 0x94;
	//public se
	//(title/menu)
	public int se_t100 = 0xb0;	//title opening
	public int se_ts100 = 0xb1;	//menu select
	public int se_ts110 = 0xb2;	//menu fix
	public int se_t101 = 0xb3;	//title opening mini
	public int se_ts101 = 0xb4;	//menu select mini
	public int se_ts111 = 0xb5;	//menu fix mini
	//(base)
	public int se_bs130 = 0xb6;	//base acceleration
	//(map)
	public int se_bs131 = 0xb7;	//map acceleratioin
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

	//private
	//local const
	//audio ch
	const int ch0 = 0x00;	//[loop]bgm
	const int ch1 = 0x01;	//voice 0 high priority voice 
	const int ch2 = 0x02;	//voice 1 (damage)
	const int ch3 = 0x03;	//voice 2 (power up)
	const int ch4 = 0x04;	//event se 0 (title/credit/debugmenu/pause/clearevent select/start)
	const int ch5 = 0x05;	//event se 1 title opening se,game opening(base flight in/release/acceleration),game event(map acceleration),middle boss flightt in,boss rotate
	const int ch6 = 0x06;	//[loop]event se 2 loop se (game opening base/middleboss flightnoise / 1boss roadnoise)
	const int ch7 = 0x07;	//player se 0 player shot,option shot
	const int ch8 = 0x08;	//player se 1 player laser/missile shot
	const int ch9 = 0x09;	//player se 2 bomb / bomb empty
	const int ch10 = 0x0a;	//player se 3 player damage / player explosion
	const int ch11 = 0x0b;	//player se 4 get star
	const int ch12 = 0x0c;	//player se 5 get item
	const int ch13 = 0x0d;	//player se 6 missile bomb
	const int ch14 = 0x0e;	//enemy se 0 enemy/enemy bullet damage
	const int ch15 = 0x0f;	//enemy se 1 enemy shot
	const int ch16 = 0x10;	//enemy se 2 enemy explosion
	const int ch17 = 0x11;	//enemy se 3 enemy explosion middle boss/boss
	const int ch18 = 0x12;	//event se 3 1boss wakeup
	const int ch19 = 0x13;	//event se 4 display string(clear bonus cnt/main message/title description)
	const int ch20 = 0x14;	//player se 7 player bomb laser


	//system local
	int intervalCnt;	//interval counter

	//component cash
	GameObject mainCtr;
	mainController mc;
	AudioSource[] aud;

	//local

	//index min/max
	int bgmmin;
	int bgmmax;
	int voicemin;
	int voicemax;
	int semin;
	int semax;

	//bgm fadeout
	bool bgmfadeout;
	int bgmfadeoutcnt;

	//loop se fadeout
	bool loopsefadeout;
	int loopsefadeoutcnt;

	//sound info struct
	struct soundInfo{
		public AudioClip audioClip;
		public float volume;
		public int ch;
		public soundInfo(AudioClip a, float v, int c ){
			audioClip = a;
			volume = v;
			ch = c;
		}
	}
	//sound clip index,volume,ch table
	soundInfo[] soundInfoTbl;

	// Use this for initialization
	void Start () {
		//system init
		intervalCnt = 0;

		//cash
		//maincontroller
		mainCtr = GameObject.Find ("mainController");
		mc = mainCtr.GetComponent<mainController> ();

		//audio
		aud = GetComponents<AudioSource>();

		//local

		//index min/max
		bgmmin = b100;
		bgmmax = b185;
		voicemin = vo100;
		voicemax = vo280;
		semin = se_t100;
		semax = se_bomblaser;

		//bgm fadeout
		bgmfadeout = false;
		bgmfadeoutcnt = 0;

		//loop se fadeout
		loopsefadeout = false;
		loopsefadeoutcnt = 0;

		//sound info table
		soundInfoTbl = new soundInfo[]{	//audio clip, volume, audio ch
			//bgm[loop]
			new soundInfo( bgm100, 1.0f, ch0),
			new soundInfo( bgm500, 1.0f, ch0),
			new soundInfo( bgm501, 1.0f, ch0),
			new soundInfo( bgm800, 1.0f, ch0),
			new soundInfo( bgm110, 1.0f, ch0),	//result display
			new soundInfo( bgm120, 1.0f, ch0),
			new soundInfo( bgm101, 1.0f, ch0),
			new soundInfo( bgm121, 1.0f, ch0),	//stage1
			new soundInfo( bgm502, 0.78f, ch0),	//boss
			new soundInfo( bgm801, 0.93f, ch0),	//title screen
			new soundInfo( bgm130, 1.0f, ch0),
			new soundInfo( bgm131, 1.0f, ch0),
			new soundInfo( bgm132, 1.0f, ch0),
			new soundInfo( bgm133, 1.0f, ch0),
			new soundInfo( bgm140, 1.0f, ch0),
			new soundInfo( bgm141, 1.0f, ch0),
			new soundInfo( bgm142, 0.90f, ch0),
			new soundInfo( bgm143, 0.90f, ch0),	//stage2
			new soundInfo( bgm144, 1.0f, ch0),
			new soundInfo( bgm152, 0.95f, ch0),	//boss3-1
			new soundInfo( bgm153, 1.0f, ch0),
			new soundInfo( bgm154, 1.0f, ch0),
			new soundInfo( bgm162, 1.0f, ch0),
			new soundInfo( bgm163, 0.70f, ch0),	//all clear
			new soundInfo( bgm164, 0.82f, ch0),
			new soundInfo( bgm172, 0.95f, ch0),
			new soundInfo( bgm174, 0.95f, ch0),
			new soundInfo( bgm185, 0.98f, ch0),	//stage3
			//voice
			new soundInfo( voice100, 0.85f, ch1),	//title call
			new soundInfo( voice101, 0.85f, ch1),	//title call echo
			new soundInfo( voice110, 0.90f, ch1),	//tap description(日本語)
			new soundInfo( voice120, 0.80f, ch1),	//tap description
			new soundInfo( voice130, 0.80f, ch1),
			new soundInfo( voice140, 0.70f, ch3),	//power up
			new soundInfo( voice150, 0.70f, ch2),	//damage
			new soundInfo( voice160, 0.80f, ch1),
			new soundInfo( voice170, 0.80f, ch1),	//warning*2
			new soundInfo( voice180, 0.80f, ch1),
			new soundInfo( voice190, 0.80f, ch1),
			new soundInfo( voice200, 0.80f, ch1),
			new soundInfo( voice210, 0.80f, ch1),
			new soundInfo( voice220, 0.80f, ch1),
			new soundInfo( voice230, 0.90f, ch1),	//title descroption (no distortion)
			new soundInfo( voice240, 0.75f, ch1),	//please wait moment
			new soundInfo( voice121, 0.80f, ch1),	//tap description long
			new soundInfo( voice250, 0.90f, ch1),	//title descroption add s.w. p.type(no distortion)
			new soundInfo( voice260, 0.80f, ch1),
			new soundInfo( voice270, 0.80f, ch1),
			new soundInfo( voice280, 0.90f, ch1),
			//se
			//(title/menu)
			new soundInfo( se_title100, 0.58f, ch5),	//menu opening
			new soundInfo( se_titlesel100, 0.68f, ch4),	//menu select
			new soundInfo( se_titlesel110, 0.53f, ch4),	//menu select fix
			new soundInfo( se_title101, 1.0f, ch5),	//menu opening mini
			new soundInfo( se_titlesel101, 1.0f, ch4),	//menu select mini
			new soundInfo( se_titlesel111, 1.0f, ch4),	//menu select fix mini
			//(base)
			new soundInfo( se_base130, 0.41f, ch5),	//base acceleration
			//(map)
			new soundInfo( se_base131, 1.0f, ch5),	//map acceleration
			//(player)
			new soundInfo( se_title100, 0.31f, ch9),	//player bomb
			new soundInfo( se_titlesel110, 0.33f, ch12),	//player get item
			new soundInfo( se_star100, 0.18f, ch11),	//player get star
			new soundInfo( se_base100, 0.27f, ch9),	//player bomb empty
			new soundInfo( se_shot112, 0.18f, ch7),	//player shot
			new soundInfo( se_shot112, 0.18f, ch7),	//player option shot
			new soundInfo( se_shot110, 0.19f, ch8),	//player laser
			new soundInfo( se_shot111, 0.21f, ch8),	//player missile
			new soundInfo( se_damege110, 0.97f, ch10),	//player damage
			new soundInfo( se_explosion100, 0.85f, ch10),	//player explosion
			new soundInfo( se_title101, 0.65f, ch13),	//player missile bomb
			//(enemy)
			new soundInfo( se_damage100, 0.9f, ch14),	//enemy damage
			new soundInfo( se_enemyBullet102, 0.82f, ch15),	//enemy bullet
			new soundInfo( se_explosion110, 0.8f, ch16),	//enemy explosion
			new soundInfo( se_explosion120, 0.96f, ch17),	//enemy boss explosion
			new soundInfo( se_base110, 0.80f, ch6),	//[loop]middle boss flight noise
			new soundInfo( se_base120, 0.30f, ch5),	//middle boss flight in
			new soundInfo( se_explosion120, 0.82f, ch17),	//middle boss explosion
			new soundInfo( se_tank100, 0.66f, ch18),	//boss wakeup
			new soundInfo( se_tank110, 0.63f, ch6),	//[loop]boss roadnoise
			new soundInfo( se_tank120, 0.35f, ch5),	//boss rotate
			//(base)
			new soundInfo( se_base110, 0.94f, ch6),	//[loop]base flight noise
			new soundInfo( se_base120, 0.42f, ch5),	//base flight in
			new soundInfo( se_base100, 0.41f, ch5),	//base player release
			//(title/menu)
			new soundInfo( se_shot100, 1.00f, ch19),	//display string(clear bonus cnt/main message/title description)
			//(player)
			new soundInfo( se_bombLaser100, 0.15f, ch20),	//player bomb laser
		};

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
		if (intervalCnt >= 2) {
			intervalCnt = 0;

			//fadeout bgm
			if (bgmfadeout == true) {
				bgmfadeoutcnt++;
				if (bgmfadeoutcnt % 2 == 0) {
					float vo = aud [ch0].volume;
					vo = vo - 0.028f;
					if (vo <= 0) {
						vo = 0.0f;
						bgmfadeout = false;
						bgmfadeoutcnt = 0;
						aud [ch0].clip = null;
					}
					aud [ch0].volume = vo;
				}
			}

			//fadeout loop se
			if (loopsefadeout == true) {
				loopsefadeoutcnt++;
				if (loopsefadeoutcnt % 2 == 0) {
					float vo = aud [ch6].volume;
					vo = vo - 0.025f;
					if (vo <= 0) {
						vo = 0.0f;
						loopsefadeout = false;
						loopsefadeoutcnt = 0;
						aud [ch6].clip = null;
					}
					aud [ch6].volume = vo;
				}
			}

		}
	}


	//private

	//start bgm fadeout
	private void startBgmFadeout(){
		bgmfadeout = true;
		bgmfadeoutcnt = 0;
	}

	private void stopBgmFadeout(){
		bgmfadeout = false;
		bgmfadeoutcnt = 0;
		aud [ch0].clip = null;
	}

	//start loop se fadeout
	private void startLoopSeFadeout(){
		loopsefadeout = true;
		loopsefadeoutcnt = 0;
	}

	private void stopLoopSeFadeout(){
		loopsefadeout = false;
		loopsefadeoutcnt = 0;
		aud [ch6].clip = null;
	}


	//public

	//play sound
	public void playSound( int sound, bool playForce = true ){
		//fix index
		if (sound >= semin) {	//se?
			sound = bgmmax + (voicemax - voicemin + 1) + (sound - semin + 1);
		} else if (sound >= voicemin) {	//voice?
			sound = bgmmax + (sound - voicemin + 1);
		}
		//play no force (same sound no play)
		if (playForce == false) {
			if ( aud [soundInfoTbl [sound].ch].isPlaying == true ) {
				if (aud [soundInfoTbl [sound].ch].clip == soundInfoTbl [sound].audioClip) {
					return;
				}
			}
		}
		//stop fadeout
		if (soundInfoTbl [sound].ch == ch0) {
			this.stopBgmFadeout ();
		}
		if (soundInfoTbl [sound].ch == ch6) {
			this.stopLoopSeFadeout ();
		}
		//sound play
		aud [soundInfoTbl [sound].ch].clip = soundInfoTbl [sound].audioClip;	//clip
		aud [soundInfoTbl [sound].ch].volume = soundInfoTbl [sound].volume;	//volume
		aud [soundInfoTbl [sound].ch].Play ();
	}

	//stop bgm
	public void stopBgm(){
		aud [ch0].Stop ();
		aud [ch0].clip = null;
	}

	//fadeout bgm
	public void fadeoutBgm(){
		if (aud [ch0].clip != null) {
			this.startBgmFadeout ();
		}
	}

	//pause bgm
	public void pauseBgm(){
		if (aud [ch0].clip != null) {
			if (aud [ch0].isPlaying == true) {
				aud [ch0].Pause ();
			} else {
				aud [ch0].clip = null;
			}
		}
	}

	//resume bgm
	public void resumeBgm(){
		if (aud [ch0].clip != null) {
			aud [ch0].Play ();
		}
	}

	//stop voice
	public void stopVoice(){
		for (int i = ch1; i <= ch3; i++) {
			aud [i].Stop ();
			aud [i].clip = null;
		}
	}

	//fadeout voice
	public void fadeoutVoice(){	//(非対応)
		for (int i = ch1; i <= ch3; i++) {
			aud [i].Stop ();
			aud [i].clip = null;
		}
	}

	//pause voice
	public void pauseVoice(){
		for (int i = ch1; i <= ch3; i++) {
			if (aud [i].clip != null) {
				if (aud [i].isPlaying == true) {
					aud [i].Pause ();
				} else {
					aud [i].clip = null;
				}
			}
		}
	}

	//resume voice
	public void resumeVoice(){
		for (int i = ch1; i <= ch3; i++) {
			if (aud [i].clip != null) {
				aud [i].Play ();
			}
		}
	}

	//stop se
	public void stopSe(){
		for (int i = ch4; i <= ch18; i++) {
			aud [i].Stop ();
			aud [i].clip = null;
		}
	}

	//fadeout se
	public void fadeoutSe(){	//(非対応)
		for (int i = ch4; i <= ch18; i++) {
			aud [i].Stop ();
			aud [i].clip = null;
		}
	}

	//stop loop se
	public void stopLoopSe(){
		aud [ch6].Stop ();
		aud [ch6].clip = null;
	}

	//fadeout loop se
	public void fadeoutLoopSe(){
		if (aud [ch6].clip != null) {
			this.startLoopSeFadeout ();
		}
	}

	//pause se
	public void pauseSe(){
		for (int i = ch5; i <= ch18; i++) {	//ch4は除く
			if (aud [i].clip != null) {
				if (aud [i].isPlaying == true) {
					aud [i].Pause ();
				} else {
					aud [i].clip = null;
				}
			}
		}
	}

	//resume se
	public void resumeSe(){
		for (int i = ch5; i <= ch18; i++) {	//ch4は除く
			if (aud [i].clip != null) {
				aud [i].Play ();
			}
		}
	}

}
