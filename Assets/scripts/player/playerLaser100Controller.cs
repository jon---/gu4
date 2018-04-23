using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerLaser100Controller : MonoBehaviour {
	//public

	//private
	//local const
	//x,y min/max
	const float xmin = -4.0f;
	const float xmax = 4.0f;
	const float ymin = -6.0f;
	const float ymax = 6.0f;//10.0f;
	//x,y speed base
	const float xspd_base = 1.0f;
	const float yspd_base = 0.35f;

	//system local
	int intervalCnt;	//interval count

	//component cash
	Transform cashTransform;
	GameObject mainCtr;
	mainController mc;
	GameObject playerCtr;
	playerController plc;

	//local
	//pos x,y
	float posx;
	float posy;

	//scale
	float sclx;
	float scly;

	//x,y mov speed
	float xx;
	float yy;

	//laser x,y
	float lbase;
	float lx;
	float ly;
	float last_lx;
	float last_ly;

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

		//playercontroller
		playerCtr = GameObject.Find ("playerController");
		plc = playerCtr.GetComponent<playerController> ();

		//pos x,y
		cashTransform.position = new Vector3( posx, posy, 0.0f );

		//scale
		cashTransform.localScale = new Vector3 ( sclx, scly, 0.0f);

		//x,y mov speed
		//(set from parent objects)
//		xx = 0.0f * xspd_base;
//		yy = 1.0f * yspd_base;

		last_lx = cashTransform.position.x;
		last_ly = cashTransform.position.y;

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

		//get player info
		Vector2 pposls = plc.getPlayerPosLaser();
		Vector2 pposmov = plc.getPlayerPosMov ();

		//laser base move
		lbase = lbase + (yy * yspd_base);

		//laser direction
		float ld = ( ((pposmov.x) * -1 * 0.53f) * Mathf.Pow(((cashTransform.position.y-pposls.y)+2.5f),2.0f)*0.58f );
		float lxd = Mathf.Cos ((ld+90.0f) * Mathf.Deg2Rad) * 1.0f * lbase;
		float lyd = Mathf.Sin ((ld+90.0f) * Mathf.Deg2Rad) * 1.0f * lbase;

		//laser move
		float nx = (pposls.x) + lxd;
		float ny = (pposls.y) + lyd;
		float xmov = nx - last_lx;
		float ymov = ny - last_ly;
		last_lx = nx;
		last_ly = ny;
		cashTransform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, 0.0f ));
		cashTransform.Translate (xmov, ymov, 0);
		//laser rotation (display only)
		cashTransform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, (ld) ));

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
				Debug.Log ("no inc dec playerLaser100");
				#endif
			}
			//delete this
			alreadydelete = true;
			Destroy (gameObject);
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
			mc.generateEnemyDamageEffect( cashTransform.position.x, cashTransform.position.y );
			this.playerBulletHit ();
		}else if (cotag == "enemyLow") {
			//collision enemy low
			mc.generateEnemyDamageEffect( cashTransform.position.x, cashTransform.position.y );
			this.playerBulletHit ();
		}else if (cotag == "groundEnemy") {
			//collision ground enemy
			mc.generateEnemyDamageEffect( cashTransform.position.x, cashTransform.position.y );
			this.playerBulletHit();
		} else if (cotag == "enemyBullet") {
			//collision enemy bullet
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
			Debug.Log ("no inc dec playerLaser100");
			#endif
		}
		//delete this
		alreadydelete = true;
		Destroy (gameObject);
	}


	//public

	//set init status
	public void setInitStatus( float xs, float ys, float initx, float inity, float sx, float sy ){
		this.xx = xs;
		this.yy = ys;
		this.posx = initx;
		this.posy = inity;
		this.sclx = sx;
		this.scly = sy;
	}

}
