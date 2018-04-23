using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class endingTextDispController : MonoBehaviour {
	//public

	//private
	//local const
	//color change speed
	const float aa = 2.0f;

	//system local
	int intervalCnt;	//interval counter

	//system cash
	//rect transform cash
	RectTransform cashRectTransform;
	Text cashText;
	//component cash
	GameObject mainCtr;
	mainController mc;

	//local
	//color
	float r;
	float g;
	float b;
	float a;

	//txt color
	int col;

	//color cnt (txt color 1/2)
	int colCnt;

	//mov
	float xx;
	float yy;
	float posx;
	float posy;
	float endy;

	//message
	string mes;

	//fade in/out
	bool fdin;
	bool fdout;

	//last message
	bool lastmsg;

	// Use this for initialization
	void Start () {
		//system init
		intervalCnt = 0;

		//cash
		//cash rect transform
		cashRectTransform = GetComponent<RectTransform> ();
		cashText = GetComponent<Text> ();

		//main controller
		mainCtr = GameObject.Find ("mainController");
		mc = mainCtr.GetComponent<mainController> ();

		//local

		//color init
		if ((col == 1) || (col == 2)) {	//white / gold
			cashText.color = new Color (255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f, 0.0f / 255.0f);
		} else if (col == 3) {	//red
			cashText.color = new Color (248.0f / 255.0f, 138.0f / 255.0f, 138.0f / 255.0f, 0.0f / 255.0f);
		}

		//color cnt
		colCnt = 0;

		//color 
		Color cr = cashText.color;
		r = cr.r;
		g = cr.g;
		b = cr.b;
		a = 0.0f;//220.0f;

		//position
		cashRectTransform.localPosition = new Vector3( posx, posy, 0.0f);

		//mov
		//(set form parent objects)
//		xx = 0.0f;
//		yy = 0.0f;

		//message
		//(set form parent objects)
//		mes="";
		this.cashText.text = mes;

		//disp time
		fdin = true;
		fdout = false;

		//last message
		//(set form parent objects)
//		lastmsg = false;

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

		//mov
//		cashRectTransform.Translate( xx, yy, 0.0f);
		Vector2 lpos = cashRectTransform.localPosition;
		lpos.y = lpos.y + yy;
		cashRectTransform.localPosition = lpos;
		if (lastmsg == true) {
			if (cashRectTransform.localPosition.y >= 150) {
				yy = 0;
			}
		} else {
			if (cashRectTransform.localPosition.y >= endy) {
				if (fdout == false) {
					fdout = true;
				}
			}
		}

		//color
		if (col == 2) {	//gold?
			if (colCnt == 0) {
				r = 255.0f / 255.0f;
				g = 255.0f / 255.0f;
				b = 255.0f / 255.0f;
			} else if (colCnt == 1) {
				r = 255.0f / 255.0f;
				g = 255.0f / 255.0f;
				b = 40.0f / 255.0f;
			} else if (colCnt == 2) {
				r = 255.0f / 255.0f;
				g = 40.0f / 255.0f;
				b = 40.0f / 255.0f;
			}
			colCnt++;
			if (colCnt >= 3) {
				colCnt = 0;
			}
		}
		cashText.color = new Color(r, g, b, (a/255.0f));
		//fade in
		if ( fdin == true) {
			a = a + aa;
			if (a >= 255.0f) {
				a = 255.0f;
				fdin = false;
			}
		}
		//fade out
		if ( fdout == true ) {
			a = a - aa;
			if (a <= 0.0f) {
				fdout = false;
				a = 0.0f;
				//term
				//objnum dec
				mc.decObj ();
				//delete this
				Destroy (gameObject);
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
	public void setInitStatus( float x, float y, float ey, float xs, float ys, string msg, int cl=0, bool last=false ){
		this.posx = x;
		this.posy = y;
		this.endy = ey;
		this.xx = xs;
		this.yy = ys;
		this.mes = msg;
		this.col = cl;
		this.lastmsg = last;
	}

}
