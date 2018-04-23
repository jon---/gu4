using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class titleDispController : MonoBehaviour {
	//public

	//private
	//local const
	//color change speed
	const float cc = 15.0f;	//fadein/out alpha step

	//system local
	int intervalCnt;	//interval counter

	//system cash

	//component cash
	Transform cashTransform = null;
	GameObject mainCtr;
	mainController mc;
	Text cashText;
	Image cashImage;

	//local
	string thisname;
	//color mode
	int cm;	//mode
	const int colorModeNormal = 0x00;
	const int colorModeFadein = 0x80;
	const int colorModeFadeout = 0x81;
	//obj type
	const int objTxt = 0x00;
	const int objImg = 0x01;
	int objType = objTxt;
	//color
	float r;
	float g;
	float b;
	float a;
	float fadeinTarget_a;
	float fadeoutTarget_a;
	float max_a = 255.0f;	//fadein tagert default = 255.0f
	//title logo
	bool titlelogo = false;
	float taDir;
	float tadd;
	float taScl;
	//title logo flash
	int logoflashcnt;


	// Use this for initialization
	void Start () {
		//system init
		intervalCnt = 0;

		//cash
		cashTransform = transform;
		//main controller
		mainCtr = GameObject.Find ("mainController");
		mc = mainCtr.GetComponent<mainController> ();

		//object name
		thisname = cashTransform.name;

		//local
		//color mode
		cm = colorModeNormal;

		//obj type (set from parent objects)
//		objType = objTxt;
		if (objType == objTxt) {
			cashText = GetComponent<Text> ();
		} else {
			cashImage = GetComponent<Image> ();
		}

		//color
//		max_a = 255.0f; (set from parent objects)
		Color cr = new Color(0.0f, 0.0f, 0.0f, 0.0f);
		Text txt = GetComponent<Text>();
		if (txt != null) {
			cr = cashText.color;
		} else {
			cr = cashImage.color;
		}
		r = cr.r;
		g = cr.g;
		b = cr.b;
		a = 0;
		fadeinTarget_a = max_a / 255.0f;	//fadein target
		fadeoutTarget_a = 0.0f;	//fadeout target

		//title logo
//		titlelogo = false;	(set from parent objects)
		taDir = 0.0f;
		tadd = 80.0f;
		taScl = 0.0f;

		//title logo flash
		logoflashcnt = 20;

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

		Color cr = new Color(0.0f, 0.0f, 0.0f, 0.0f);
		float aa=0;	//fadein/out speed
		switch (cm) {
		case colorModeNormal:
			//normal
			//check color mode change
			if (mc.gameTitleColorMode == mc.gameTitleColorModeFadein) {
				if (objType == objTxt) {
					cr = cashText.color;
				} else {
					cr = cashImage.color;
				}
				r = cr.r;
				g = cr.g;
				b = cr.b;
				cm = colorModeFadein;
				a = 0;
			}
			if( mc.gameTitleColorMode == mc.gameTitleColorModeFadeout ){
				if (objType == objTxt) {
					cr = cashText.color;
				} else {
					cr = cashImage.color;
				}
				r = cr.r;
				g = cr.g;
				b = cr.b;
				cm = colorModeFadeout;
				if (objType == objTxt) {
					a = cashText.color.a;
				} else {
					a = cashImage.color.a;
				}
			}
			break;
		case colorModeFadein:
			//fadein
			if (objType == objTxt) {
				cr = cashText.color;
			} else {
				cr = cashImage.color;
			}
			r = cr.r;
			g = cr.g;
			b = cr.b;
			//color change fade in
			aa = fadeinTarget_a / cc;	//fadein speed
			a = a + aa;
			if (a > fadeinTarget_a) {
				a = fadeinTarget_a;
				cm = colorModeNormal;
				mc.termTitleFadein();
			}
			if (objType == objTxt) {
				cashText.color = new Color(r, g, b, a);
			} else {
				cashImage.color = new Color(r, g, b, a);
			}
			break;
		case colorModeFadeout:
			//fadeout
			if (objType == objTxt) {
				cr = cashText.color;
			} else {
				cr = cashImage.color;
			}
			r = cr.r;
			g = cr.g;
			b = cr.b;
			//color change fade out
			aa = fadeinTarget_a / cc;
			a = a - aa;
			if (a < fadeoutTarget_a) {
				a = fadeoutTarget_a;
				cm = colorModeNormal;
				mc.termTitleFadeout ();
				//term
				titlelogo = false;
				mc.gameTitleLogoFlash = false;
				//objnum dec
				mc.decObj ();
				//delete this
				Destroy (gameObject);
			}
			if (objType == objTxt) {
				cashText.color = new Color(r, g, b, a);
			} else {
				cashImage.color = new Color(r, g, b, a);
			}
			break;
		default:
			break;
		}

		//title logo animation
		if (titlelogo == true) {
			const float rotatestopdd = 8.8f;
			const float rotatebrake = 1.03f;
			//rotate
			taDir = taDir + tadd;
			if (tadd > rotatestopdd) {
				//rotate speed
				tadd = tadd - rotatebrake;
			}
			//rotate stop
			if (taDir >= 360.0f) {
				taDir = taDir - 360.0f;
				if (tadd <= rotatestopdd) {
					tadd = 0.0f;
					taDir = 0.0f;
					mc.playSound (mc.se_t101);
					mc.generateScreenFlashEffect ();
					mc.generateScreenShakeEffect (15);
					mc.gameTitleLogoFlash = true;
				}
			}
			//scale
			if (taScl < 1.0f) {
				taScl = taScl + 0.012f;
			}
			//rotate/scale change
			cashTransform.localRotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, taDir));
			cashTransform.localScale = new Vector3 (taScl, taScl, 1.0f );
		}

		//title logo flash
		if ((thisname == "titleDispPrefab") || (thisname == "titleDispBPrefab")) {
			if (mc.gameTitleLogoFlash == true) {
				if (logoflashcnt > 0) {
					if ((logoflashcnt % 3) == 0) {
						cashText.color = new Color (255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f, max_a / 255.0f);
					} else if ((logoflashcnt % 3) == 1) {
						cashText.color = new Color (255.0f / 255.0f, 255.0f / 255.0f, 0.0f / 255.0f, max_a / 255.0f);
					} else if ((logoflashcnt % 3) == 2) {
						cashText.color = new Color (255.0f / 255.0f, 0.0f / 255.0f, 0.0f / 255.0f, max_a / 255.0f);
					}
					logoflashcnt--;
					if (logoflashcnt <= 0) {
						cashText.color = new Color (90.0f / 255.0f, 200.0f / 255.0f, 90.0f / 255.0f, max_a / 255.0f);
						logoflashcnt = 0;
					}
				}

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

	//public set initial status
	public void setInitState( float ma, int ot, bool titlelogo=false ){
		this.max_a = ma;	//max alpha val
		this.objType = ot;	//txt or image
		this.titlelogo = titlelogo;	//title logo (for animation)
	}

}
