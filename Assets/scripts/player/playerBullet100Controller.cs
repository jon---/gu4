using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBullet100Controller : MonoBehaviour {
	//public

	//private
	//local const
	//x,y min/max
	const float xmin = -4.0f;
	const float xmax = 4.0f;
	const float ymin = -6.0f;
	const float ymax = 5.2f;
	//x,y speed base
	const float xspd_base = 1.0f;//0.8f;//1.3f;
	const float yspd_base = 1.0f;//0.8f;//1.3f;

	//system local
	int intervalCnt;	//interval count

	//component cash
	Transform cashTransform;
	GameObject mainCtr;
	mainController mc;

	//local
	//x,y mov speed
	float xx;
	float yy;

	//scale x,y
	float sclx;
	float scly;

	//direction
	float dir;

	//pos x,y
	float posx;
	float posy;

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

		//x,y mov speed
		xx = 0.0f;
//		yy = 1.0f;	//(set from parent objects)

		//pos x,y
		cashTransform.position = new Vector3 (posx, posy, 0.0f);	//(set from parent objects)

		//scale
		cashTransform.localScale = new Vector3 ( sclx, scly, 0.0f);	//(set from parent objects)

		//rotate
		cashTransform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, dir));	//(set from parent objects)

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

		//bullet move
		cashTransform.Translate((xx*xspd_base), (yy*yspd_base), 0);

		//move result process
		if ( (cashTransform.position.x >= xmax ) || (cashTransform.position.x <= xmin) || 
			 (cashTransform.position.y >= ymax ) || (cashTransform.position.y <= ymin) ){
			if (alreadydelete == true) {
				return;
			}
			//objnum dec
			if (incobj == true) {
				mc.decObj ();
			} else {
				#if UNITY_EDITOR
				Debug.Log ("no inc dec playerbullet100");
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
			mc.generateEnemyDamageEffect (cashTransform.position.x, cashTransform.position.y);
			this.playerBulletHit ();
		}else if (cotag == "enemyLow") {
			//collision enemy low
			mc.generateEnemyDamageEffect( cashTransform.position.x, cashTransform.position.y );
			this.playerBulletHit ();
		}else if (cotag == "groundEnemy") {
			//collision ground enemy
			mc.generateEnemyDamageEffect( cashTransform.position.x, cashTransform.position.y );
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
			Debug.Log ("no inc dec playerbullet100");
			#endif
		}
		//delete this
		alreadydelete = true;
		Destroy (gameObject);
	}


	//public
	//set init status
	public void setInitStatus( float px, float py, float sx, float sy, float dr, float spd ){
		this.posx = px;
		this.posy = py;
		this.sclx = sx;
		this.scly = sy;
		this.dir = dr;
		this.yy = 1.0f * spd;
	}

}
