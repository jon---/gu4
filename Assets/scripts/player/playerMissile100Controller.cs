using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMissile100Controller : MonoBehaviour {
	//public
	//public missile bomb Prefab
	public GameObject missileBombControllerPrefab;

	//private
	//local const
	//x,y min/max
	const float xmin = -4.0f;
	const float xmax = 4.0f;
	const float ymin = -6.0f;
	const float ymax = 5.2f;
	//x,y speed base
	const float xspd_base = 0.15f;
	const float yspd_base = 0.15f;

	//system local
	int intervalCnt;	//interval count

	//component cash
	Transform cashTransform;
	GameObject mainCtr;
	mainController mc;

	//pos x,y
	float posx;
	float posy;

	//local
	//x,y mov speed
	float xx;
	float yy;

	//l,r
	int lr;

	//stseq
	int stseq;
	int stseqcnt;

	//already delete
	bool alreadydelete = false;

	//objinc
	bool incobj = false;


	// Use this for initialization
	void Start () {
		//system init
		intervalCnt = 0;

		//cash
		//transform cash
		cashTransform = transform;

		//maincontroller
		mainCtr = GameObject.Find ("mainController");
		mc = mainCtr.GetComponent<mainController> ();

		//pos x,y
		cashTransform.position = new Vector3( posx, posy, 0.0f );

		//x,y mov speed
		//(set from parent objects)
//		xx = 0.0f * xspd_base;
//		yy = 1.0f * yspd_base;

		//l,r
		//(set from parent objects)
//		lr = 1;

		//stseq
		stseq = 0;
		stseqcnt = 0;

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

		//shot seq
		switch (stseq) {
		case 0:
			//bullet wakeup
			cashTransform.Translate ( (0.042f*((float)lr)), -0.035f, 0);
			stseqcnt++;
			if (stseqcnt >= 10) {
				stseqcnt = 0;
				mc.playSound (mc.se_playermissile);
				stseq++;
			}
			break;
		case 1:
			//bullet move
			cashTransform.Translate ((xx * xspd_base), (yy * yspd_base), 0);
			yy = yy + 0.28f;
			//move result process
			if ( (cashTransform.position.x >= xmax ) || (cashTransform.position.x <= xmin) || 
				(cashTransform.position.y >= ymax ) || (cashTransform.position.y <= ymin) ){
				if (alreadydelete == true) {
					return;
				}
				//objnum dec
				if (incobj == true) {
					mc.decObj ();
					incobj = false;
				} else {
					#if UNITY_EDITOR
					Debug.Log ("no inc dec playerMissile100");
					#endif
				}
				//delete this
				alreadydelete = true;
				Destroy (gameObject);
			}
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


	//collision
	public void OnTriggerEnter2D(Collider2D coll){
		if (alreadydelete == true) {
			return;
		}
		string cotag = coll.gameObject.tag;
		if (cotag == "enemy") {
			//collision enemy
			this.generateMissileBomb();
			this.playerBulletHit ();
		}else if (cotag == "enemyLow") {
			//collision enemy low
			this.generateMissileBomb();
			this.playerBulletHit ();
		}else if (cotag == "groundEnemy") {
			//collision ground enemy
			this.generateMissileBomb();
			this.playerBulletHit();
//		} else if (cotag == "sideMap") {
//			//collision side map
//			this.playerBulletHit();
		} else {
			//collision other
		}
	}

	//player bullet hit process
	private void playerBulletHit(){
		//objnum dec
		if (incobj == true) {
			mc.decObj ();
			incobj = false;
		} else {
			#if UNITY_EDITOR
			Debug.Log ("no inc dec playerMissile100");
			#endif
		}
		//delete this
		alreadydelete = true;
		Destroy (gameObject);
	}

	//generate missile bomb
	private void generateMissileBomb(){
		//genarate
		GameObject go;
		go = Instantiate (missileBombControllerPrefab) as GameObject;
		if ( this.tag == "playerMissile1" ){
			go.tag = "missileBomb1";
		} else if( this.tag == "playerMissile2" ){
			go.tag = "missileBomb2";
		}
		go.GetComponent<missileBombController> ().setInitStatus (cashTransform.position.x, cashTransform.position.y);
	}


	//public
	//set init status
	public void setInitStatus( float xs, float ys, int lr, float px, float py ){
		//x,y speed(direction)
		this.xx = xs;
		this.yy = ys;
		this.lr = lr;
		this.posx = px;
		this.posy = py;
	}

}
