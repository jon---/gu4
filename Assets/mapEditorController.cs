//#define MAP_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class mapController : MonoBehaviour {

	#if MAP_EDITOR

	//public
	public int msEdit = 0x07;	//map for editor
	public int smsEdit = 0x07;	//side map for editor

	//local const
	const float mescale = 0.68f;	//map editor scale

	//local
	//pos
	int mex;
	int mey;
	int merx;
	int mery;
	//blink
	int meblcnt;
	//num input
	int backmd;
	int cnum;
	bool numinput;


	//map editor start
	private void mapEditorStart(){
		//pos
		mex = 0;
		mey = 0;
		merx = 0;
		mery = map_ymax[msEdit]-1;
		//blink
		meblcnt = 0;
		//num input
		backmd = 0;
		cnum = 0;
		numinput = false;

		//set stage
		this.setMapStage( msEdit );
	}


	//map editor update
	private void mapEditorUpdate(){
		//blink
		meblcnt++;
		if( meblcnt >= 10 ){
			meblcnt=0;
			Color cr = mp [((map_ysize-mey-1)*map_xsize) + mex].GetComponent<SpriteRenderer>().color;
			if( cr.a == 255.0f/255.0f){
				cr.a = 200.0f/255.0f;
			}else{
				cr.a = 255.0f/255.0f;
			}
			mp [((map_ysize-mey-1)*map_xsize) + mex].GetComponent<SpriteRenderer>().color = cr;
		}

		//key process

		bool keyon = false;
		bool numfix = false;
		//arrow left
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			keyon = true;
			//reset blink
			mp [((map_ysize-mey-1)*map_xsize) + mex].GetComponent<SpriteRenderer>().color = new Color(1.0f,1.0f,1.0f,1.0f);
			//screen pos
			mex = mex - 1;
			if (mex < 0) {
				mex = 0;
			} else {
				numfix = true;
			}
			//raw data pos
			merx = merx -1;
			if( merx < 0 ){
				merx = 0;
			}
			backmd = map_raw [merx, mery];
		}

		//arrow right
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			keyon = true;
			//reset blink
			mp [((map_ysize-mey-1)*map_xsize) + mex].GetComponent<SpriteRenderer>().color = new Color(1.0f,1.0f,1.0f,1.0f);
			//screen pos
			mex = mex + 1;
			if (mex >= map_xsize) {
				mex = map_xsize-1;
			} else {
				numfix = true;
			}
			//raw data pos
			merx = merx +1;
			if( merx >= map_xsize ){
				merx = map_xsize-1;
			}
			backmd = map_raw [merx, mery];
		}

		//arrow up
		bool updatedraw = false;
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			keyon = true;
			//reset blink
			mp [((map_ysize-mey-1)*map_xsize) + mex].GetComponent<SpriteRenderer>().color = new Color(1.0f,1.0f,1.0f,1.0f);
			//screen pos
			mey = mey + 1;
			if (mey >= map_ysize) {
				mey = map_ysize - 1;
			} else {
				numfix = true;
			}
			//raw data pos
			mery = mery - 1;
			if (mery < 0) {
				mery = 0;
			}else{
				updatedraw = true;
			}
			backmd = map_raw [merx, mery];
		}

		//arrow down
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			keyon = true;
			//reset blink
			mp [((map_ysize-mey-1)*map_xsize) + mex].GetComponent<SpriteRenderer>().color = new Color(1.0f,1.0f,1.0f,1.0f);
			//screen pos
			mey = mey - 1;
			if (mey < 0) {
				mey = 0;
			} else {
				numfix = true;
			}
			//raw data pos
			mery = mery +1;
			if( mery >= map_ymax[mapStage] ){
				mery = map_ymax[mapStage]-1;
			}else{
				updatedraw = true;
			}
			backmd = map_raw [merx, mery];
		}

		//'[' map sprite +1
		int md;
		if (Input.GetKeyDown (KeyCode.LeftBracket)) {
			keyon = true;
			map_raw [merx, mery] = map_raw [merx, mery] + 1;
			if (map_raw [merx, mery] >= mpspr.Length) {
				map_raw [merx, mery] = 0;
			}
			md = map_raw [merx, mery];
			mpctl [((map_ysize-mey-1) * map_xsize) + mex].setSprite ( mpspr[md] );
		}

		//']' map sprite -1
		if (Input.GetKeyDown (KeyCode.RightBracket)) {
			keyon = true;
			if (map_raw [merx, mery] == 0xff) {
				map_raw [merx, mery] = mpspr.Length - 1;
			} else {
				map_raw [merx, mery] = map_raw [merx, mery] - 1;
			}
			if (map_raw [merx, mery] < 0) {
				map_raw [merx, mery] = mpspr.Length -1;
			}
			md = map_raw [merx, mery];
			mpctl [((map_ysize-mey-1) * map_xsize) + mex].setSprite ( mpspr[md] );
		}

		//'del' map sprite null set
		if (Input.GetKeyDown (KeyCode.Delete)) {
			keyon = true;
			map_raw [merx, mery] = map_raw [merx, mery] = 0xff;
			Debug.Log("map value : " + map_raw [merx, mery].ToString() );
			md = map_raw [merx, mery];
			Sprite sp = null;
			if( md != 0xff ){
				sp = mpspr [md];
			}
			mpctl [((map_ysize-mey-1) * map_xsize) + mex].setSprite ( sp );
		}

		// '0'-'9'
		bool numon = false;
		int num = 0;
		if( (Input.GetKeyDown (KeyCode.Keypad0)) || (Input.GetKeyDown (KeyCode.Alpha0))){
			numon = true;
			num = 0;
		}else if( (Input.GetKeyDown (KeyCode.Keypad1)) || (Input.GetKeyDown (KeyCode.Alpha1))){
			numon = true;
			num = 1;
		}else if( (Input.GetKeyDown (KeyCode.Keypad2)) || (Input.GetKeyDown (KeyCode.Alpha2))){
			numon = true;
			num = 2;
		}else if( (Input.GetKeyDown (KeyCode.Keypad3)) || (Input.GetKeyDown (KeyCode.Alpha3))){
			numon = true;
			num = 3;
		}else if( (Input.GetKeyDown (KeyCode.Keypad4)) || (Input.GetKeyDown (KeyCode.Alpha4))){
			numon = true;
			num = 4;
		}else if( (Input.GetKeyDown (KeyCode.Keypad5)) || (Input.GetKeyDown (KeyCode.Alpha5))){
			numon = true;
			num = 5;
		}else if( (Input.GetKeyDown (KeyCode.Keypad6)) || (Input.GetKeyDown (KeyCode.Alpha6))){
			numon = true;
			num = 6;
		}else if( (Input.GetKeyDown (KeyCode.Keypad7)) || (Input.GetKeyDown (KeyCode.Alpha7))){
			numon = true;
			num = 7;
		}else if( (Input.GetKeyDown (KeyCode.Keypad8)) || (Input.GetKeyDown (KeyCode.Alpha8))){
			numon = true;
			num = 8;
		}else if( (Input.GetKeyDown (KeyCode.Keypad9)) || (Input.GetKeyDown (KeyCode.Alpha9))){
			numon = true;
			num = 9;
		}

		//esc
		if (Input.GetKeyDown (KeyCode.Escape)) {
			//num reset
			numinput = false;
			cnum = 0;
			//sprite undo
			Sprite spr = null;
			int spidx = backmd;
			if (spidx == 0xff) {
				spr = null;
			} else {
				spr = mpspr [spidx];
			}
			map_raw [merx, mery] = spidx;
			mpctl [((map_ysize-mey-1) * map_xsize) + mex].setSprite ( spr );
			Debug.Log ("cansel");
		}

		//enter
		if ((Input.GetKeyDown (KeyCode.Return)) || (Input.GetKeyDown (KeyCode.KeypadEnter))) {
			//num fix
			numfix = true;
		}
			
		//num process
		if (numon == true) {
			if (numinput == false) {
				numinput = true;
				cnum = num;
			} else {
				cnum = cnum * 10;
				cnum = cnum + num;
				if (cnum >= 1000) {
					cnum = int.Parse(cnum.ToString("").Substring(1,3));
				}
			}
			int spidx;
			Sprite spr = null;
			if (cnum >= mpspr.Length) {
				spidx = 0xff;
				spr = null;
				Debug.Log ("val: " + cnum.ToString () + " over");
			} else {
				spidx = cnum;
				spr = mpspr [spidx];
				Debug.Log ("val: " + cnum.ToString ());
			}
			map_raw [merx, mery] = spidx;
			mpctl [((map_ysize-mey-1) * map_xsize) + mex].setSprite ( spr );
		}
		if (numfix == true) {
			if (numinput == true) {
				numinput = false;
				cnum = 0;
				Debug.Log ("fix.");
			}
		}

		//'o' raw data log output
		if (Input.GetKeyDown (KeyCode.O)) {
			string rslt = "";
			for (int y = 0; y < map_ymax[mapStage] ; y++) {
				rslt = rslt + "\"";
				for (int x = 0; x < map_xmax; x++) {
					rslt = rslt + string.Format ("{00:X2}", map_raw [x, y]);
					rslt = rslt + " ";
				}
				if (y == (map_ymax [mapStage] - 1)) {
					rslt = rslt + "\"\n";
				} else {
					rslt = rslt + "\" + \n";
				}
			}
			Debug.Log(rslt);
		}

		//map draw all update
		if( updatedraw == true ){
			Sprite sp=null;
			for (int yy = 0; yy < map_ysize ; yy++) {
				for (int xx = 0; xx < map_xsize ; xx++) {
					md = map_raw [xx, mery + (mey - (map_ysize -1)) + yy];
					if (md == mapNull) {
						sp = null;
					} else {
						sp = mpspr [ md ];
					}
					mpctl [(yy * map_xsize) + xx].setSprite (sp);
				}
			}

		}

		//log for key
		if( keyon == true ){
			Debug.Log(" mapstage:"+mapStage.ToString() + 
				"mex:"+mex.ToString()+" mey:"+mey.ToString()+" merx:"+merx.ToString()+" mery:"+mery.ToString() + 
				" map_ymax[mapStage]:"+map_ymax[mapStage].ToString() + " mapraw val:"+map_raw[merx,mery].ToString());
		}
	}


	#endif

}
