using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombLaserController : MonoBehaviour {

	//public

	//private
	//local const
	float posy_offset = 5.44f;
	float max_a = 190.0f;

	//system local
	int intervalCnt;	//interval counter

	//system cash

	//component cash
	Transform cashTransform;
	SpriteRenderer sr;
	GameObject mainCtr;
	mainController mc;
	GameObject playerCtr;
	playerController plc;

	//local
	//pos x,y
	float posx;
	float posy;
	float lposx;
	float lposy;

	//exist cnt
	int existCnt;

	//fadein
	bool fdin;
	float fdina;

	//fadeout
	bool fdout;
	float fdouta;

	//objinc
	bool incobj = false;


	// Use this for initialization
	void Start () {
		//system init
		intervalCnt = 0;

		//cash
		//transform cash
		cashTransform = transform;

		//sprite renderer
		sr = GetComponent<SpriteRenderer>();

		//maincontroller
		mainCtr = GameObject.Find ("mainController");
		mc = mainCtr.GetComponent<mainController> ();

		//player controller
		playerCtr = GameObject.Find ("playerController");
		plc = playerCtr.GetComponent<playerController> ();

		//pos x,y	//(set from parent objects)
		cashTransform.position = new Vector3( posx, posy, 0.0f );
		lposx = posx;
		lposy = posy;

		//exist cnt
		if (mc.playerType == mc.playerTypeC) {
			existCnt = 420;
		} else {
			existCnt = 180;
		}

		//fadein
		fdin = true;
		fdina = 0.0f;

		//fadeout
		fdout = false;
		fdouta = max_a;

		//objnum inc
		mc.incObj();
		incobj = true;
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

		//move
		Vector2 ppos = plc.getPlayerPos();
		posx = ppos.x;
		posy = ppos.y + posy_offset;
		float xmov = posx - lposx;
		float ymov = posy - lposy;
		lposx = posx;
		lposy = posy;
		cashTransform.Translate (xmov, ymov, 0.0f);


		//exist cnt
		existCnt--;
		if (existCnt <= 0) {
			existCnt = 0;
			//fade out and term
			fdout = true;
		}

		//color
		//fadein
		if (fdin == true) {
			Color cr = sr.color;
			cr.a = (fdina/255.0f);
			sr.color = cr;
			fdina = fdina + 12.0f;
			if (fdina >= max_a) {
				fdin = false;
				fdina = max_a;
			}
		}

		//fadeout
		if (fdout == true) {
			Color cr = sr.color;
			cr.a = (fdouta/255.0f);
			sr.color = cr;
			fdouta = fdouta - 4.0f;
			if (fdouta <= 0.0f) {
				fdout = false;
				fdouta = 0.0f;
				//delete this object
				if (incobj == true) {
					mc.decObj ();
				} else {
					#if UNITY_EDITOR
					Debug.Log ("no inc dec bomblaser");
					#endif
				}
				//delete this
				Destroy (gameObject);
			}
		}

		////interval process
		//interval count
		intervalCnt++;
		if (intervalCnt >= 3) {
			intervalCnt = 0;

			//play se
			if (existCnt >= 20) {
				mc.playSound (mc.se_bomblaser);
			}
		}	
	}

	//public
	public void setInitStatus( float px, float py ){
		posx = px;
		posy = py + posy_offset;
	}

}
