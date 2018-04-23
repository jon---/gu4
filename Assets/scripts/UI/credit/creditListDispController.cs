using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class creditListDispController : MonoBehaviour {
	//public

	//private
	//local const
	//color change speed
	const float cc = 15.0f;

	//system local
	int intervalCnt;	//interval counter

	//system cash

	//component cash
	Text cashText;
	GameObject mainCtr;
	mainController mc;

	//local
	//color mode
	const int colorModeNormal = 0x00;
	const int colorModeFadein = 0x80;
	const int colorModeFadeout = 0x81;
	int cm;
	//color
	float r;
	float g;
	float b;
	float a;
	float ta80;
	float ta81;

	// Use this for initialization
	void Start () {
		//system init
		intervalCnt = 0;

		//cash
		cashText = GetComponent<Text>();
		//main controller
		mainCtr = GameObject.Find ("mainController");
		mc = mainCtr.GetComponent<mainController> ();

		//local
		//color mode
		cm = colorModeNormal;

		//color
		Color cr = cashText.color;
		r = cr.r;
		g = cr.g;
		b = cr.b;
		a = 0;
		ta80 = 255.0f / 255.0f;
		ta81 = 0.0f;

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
		Color cr;

		////always process

		float aa=0;
		switch (cm) {
		case colorModeNormal:
			//check color mode change
			if (mc.gameCreditColorMode == mc.gameCreditColorModeFadein) {
				cr = cashText.color;
				r = cr.r;
				g = cr.g;
				b = cr.b;
				cm = colorModeFadein;
				a = 0;
			}
			if( mc.gameCreditColorMode == mc.gameCreditColorModeFadeout ){
				cr = cashText.color;
				r = cr.r;
				g = cr.g;
				b = cr.b;
				cm = colorModeFadeout;
				a = cashText.color.a;
			}
			break;
		case colorModeFadein:
			cr = cashText.color;
			r = cr.r;
			g = cr.g;
			b = cr.b;
			//color change fade in
			aa = ta80 / cc;
			a = a + aa;
			if (a > ta80) {
				a = ta80;
				cm = colorModeNormal;
				mc.termCreditFadein();
			}
			cashText.color = new Color(r, g, b, a);
			break;
		case colorModeFadeout:
			cr = cashText.color;
			r = cr.r;
			g = cr.g;
			b = cr.b;
			//color change fade out
			aa = ta80 / cc;
			a = a - aa;
			if (a < ta81) {
				a = ta81;
				cm = colorModeNormal;
				mc.termCreditFadeout();
				//term
				//objnum dec
				mc.decObj();
				//delete this
				Destroy(gameObject);
			}
			cashText.color = new Color(r, g, b, a);
			break;
		default:
			break;
		}

		////interval process
		//interval count
		intervalCnt++;
		if (intervalCnt >= 2) {
			intervalCnt = 0;
			//nop
		}
	}

}
