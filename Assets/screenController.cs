using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screenController : MonoBehaviour {

	//public
	public Sprite screenMask100;

	//private
	//const

	//local
	//component cash
	Transform cashTransform;
	GameObject mainCtr;
	GameObject cameraCtr;
	mainController mc;
	SpriteRenderer sr;

	//system local
	int intervalCnt;	//interval count

	//local
	//fadein/out maskmode
	const int mskMdNoMask = 0x00;
	const int mskMdMask = 0x01;
	const int mskMdFadeout = 0x02;
	const int mskMdFadein = 0x03;
	int maskMode;

	//fade in/out alpha
	float fadea;

	//fade in/out alpha set speed
	float aspeed;

	//pause mask mode
	bool pauseMaskMode;

	//flash effect mode
	bool flashMode;

	//flash count
	int flashCnt;

	//screen shake effect mode
	bool screenShakeMode;

	//screen shake effect cnt
	int screenShakecnt;


	// Use this for initialization
	void Start () {
		//system init
		intervalCnt = 0;

		//cash
		//transform cash
		cashTransform = transform;

		//camera
		cameraCtr = GameObject.Find ("Main Camera");

		//maincontroller
		mainCtr = GameObject.Find ("mainController");
		mc = mainCtr.GetComponent<mainController> ();

		//sprite renderer
		sr = GetComponent<SpriteRenderer>();

		//mask mode
		maskMode = mskMdNoMask;

		//fade in/out alpha
		fadea = 0.0f;

		//fade in/out alpha set speed
		aspeed = 10.0f;

		//pause mask mode
		pauseMaskMode = false;

		//flash effect mode
		flashMode = false;

		//flash count
		flashCnt = 0;

		//screen shake effect mode
		screenShakeMode = false;

		//screen shake effect cnt
		screenShakecnt = 0;

		//mask sprite
		sr.sprite = null;

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

		//fadein/out process
		if ( flashMode == false) {
			switch (maskMode) {
			case mskMdNoMask:
				break;
			case mskMdMask:
				break;
			case mskMdFadeout:
				fadeoutProcess ();
				break;
			case mskMdFadein:
				fadeinProcess ();
				break;
			default:
				break;
			}
		}

		//flash process
		if (flashMode == true) {
			//color change
			if (flashCnt == 0) {
				sr.color = new Color (255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f, 128.0f / 255.0f);
			} else if (flashCnt == 1) {
				sr.color = new Color (255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f);
			} else if (flashCnt == 2) {
				sr.color = new Color (255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f, 230.0f / 255.0f);
			} else if (flashCnt == 3) {
				sr.color = new Color (255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f, 128.0f / 255.0f);
			} else if (flashCnt == 4) {
				sr.color = new Color (0.0f / 255.0f, 0.0f / 255.0f, 0.0f / 255.0f, 0.0f / 255.0f);
			}
			flashCnt++;
			if (flashCnt >= 5) {
				//mask sprite
				sr.sprite = null;
				flashCnt = 0;
				flashMode = false;
			}
		}

		//screen shake effect
		if (screenShakeMode == true) {
			if (screenShakecnt >= 0) {
				float x = 0;
				float y = 0;
				if (screenShakecnt % 4 == 3) {
					x = 0.0f;
					y = 0.1f;
				}else if (screenShakecnt % 4 == 2) {
					x = 0.0f;
					y = -0.1f;
				}else if (screenShakecnt % 4 == 1) {
					x = 0.1f;
					y = 0.0f;
				}else if (screenShakecnt % 4 == 0) {
					x = -0.1f;
					y = 0.0f;
				}
				cameraCtr.transform.Translate (x, y, 0.0f);
				screenShakecnt--;
				if (screenShakecnt < 0) {
					screenShakeMode = false;
					screenShakecnt = 0;
					cameraCtr.transform.position = new Vector3 (0.0f, 0.0f, -10.0f);
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

	//fade out process
	private void fadeoutProcess(){
		if (sr.sprite == null) {
			//mask sprite
			sr.sprite = screenMask100;
		}
		fadea = fadea + aspeed;
		if (fadea >= 255.0f) {
			fadea = 255.0f;
			//color
			sr.color = new Color (0.0f / 255.0f, 0.0f / 255.0f, 0.0f / 255.0f, 255.0f/255.0f);
			this.maskMode = mskMdMask;
			mc.releaseWait ();
			return;
		}
		//color
		sr.color = new Color (0.0f / 255.0f, 0.0f / 255.0f, 0.0f / 255.0f, fadea/255.0f);
	}

	//fade in process
	private void fadeinProcess(){
		if (sr.sprite == null) {
			//mask sprite
			sr.sprite = screenMask100;
		}
		fadea = fadea - aspeed;
		if (fadea <= 0.0f) {
			fadea = 0.0f;
			//color
			sr.color = new Color (0.0f / 255.0f, 0.0f / 255.0f, 0.0f / 255.0f, 0.0f/255.0f);
			//mask sprite
			sr.sprite = null;
			this.maskMode = mskMdNoMask;
			mc.releaseWait ();
			return;
		}
		//color
		sr.color = new Color (0.0f / 255.0f, 0.0f / 255.0f, 0.0f / 255.0f, fadea/255.0f);
	}


	//set screen mask mode
	public void setMask(){
		this.maskMode = mskMdMask;
		//mask sprite
		sr.sprite = screenMask100;
		//color
		sr.color = new Color (0.0f / 255.0f, 0.0f / 255.0f, 0.0f / 255.0f, 255.0f / 255.0f);
	}

	//set screen no mask mode
	public void setNoMask(){
		this.maskMode = mskMdNoMask;
		//color
		sr.color = new Color (0.0f / 255.0f, 0.0f / 255.0f, 0.0f / 255.0f, 0.0f / 255.0f);
		//mask sprite
		sr.sprite = null;
	}

	//set pause screen mask mode on
	public void setPauseMaskOn(){
		if (maskMode != mskMdNoMask) {
			return;
		}
		pauseMaskMode = true;
		//mask sprite
		sr.sprite = screenMask100;
		//color
		sr.color = new Color (0.0f / 255.0f, 0.0f / 255.0f, 0.0f / 255.0f, 70.0f / 255.0f);
	}

	//set pause screen mask mode off
	public void setPauseMaskOff(){
		if (maskMode != mskMdNoMask) {
			return;
		}
		pauseMaskMode = false;
		//color
		sr.color = new Color (0.0f / 255.0f, 0.0f / 255.0f, 0.0f / 255.0f, 0.0f / 255.0f);
		//mask sprite
		sr.sprite = null;
	}

	//set screen fade out
	public void setFadeout(){
		fadea = 0.0f;
		this.maskMode = mskMdFadeout;
		//mask sprite
		sr.sprite = screenMask100;
	}

	//set screen fade in
	public void setFadein(){
		fadea = 255.0f;
		this.maskMode = mskMdFadein;
		//mask sprite
		sr.sprite = screenMask100;
	}

	//set flash effect
	public void setFlashEffect(){
		flashMode = true;
		this.flashCnt = 0;
		//mask sprite
		sr.sprite = screenMask100;
	}

	//set screen shake effect
	public void setScreenShakeEffect( int num = 5 ){
		cameraCtr.transform.position = new Vector3 (0.0f, 0.0f, -10.0f);
		screenShakeMode = true;
		screenShakecnt = num;
	}

}
