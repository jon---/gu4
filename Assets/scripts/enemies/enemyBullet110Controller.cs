using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBullet110Controller : MonoBehaviour {
	//public

	//private
	//local const
	//x,y min/max
	const float xmin = -4.0f;
	const float xmax = 4.0f;
	const float ymin = -6.0f;
	const float ymax = 6.0f;
	//x,y speed base
	//(normal enemy bullet + 0.02)
	const float xspdbase_easy = 0.107f;//0.12f;
	const float yspdbase_easy = 0.107f;//0.12f;
	const float xspdbase_normal = 0.12f;
	const float yspdbase_normal = 0.12f;
	const float xspdbase_hard = 0.15f;
	const float yspdbase_hard = 0.15f;

	//system local
	int intervalCnt;	//interval count

	//component cash
	Transform cashTransform;
	Animator animt;

	//system cash
	GameObject mainCtr;
	mainController mc;

	//x,y speed base
	float xspd_base;
	float yspd_base;

	//x,y move speed (direction)
	private float xx = 0.0f;
	private float yy = 0.0f;

	//scale base
	float xscl_base;
	float yscl_base;

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

		//main controller
		mainCtr = GameObject.Find ("mainController");
		mc = mainCtr.GetComponent<mainController> ();

		//animator
		animt = GetComponent<Animator>();
		animt.speed = 2.5f;

		//x,y move speed (direction)
		//(set from enemy objects)

		//x,y speed base
		xspd_base = xspdbase_normal;	//for safe
		yspd_base = yspdbase_normal;	//for safe
		//x,y speed base
		if (mc.gameLevel == mc.gameLevelEasy) {
			xspd_base = xspdbase_easy;
			yspd_base = yspdbase_easy;
		} else if (mc.gameLevel == mc.gameLevelNormal) {
			xspd_base = xspdbase_normal;
			yspd_base = yspdbase_normal;
		} else if (mc.gameLevel == mc.gameLevelHard) {
			xspd_base = xspdbase_hard;
			yspd_base = yspdbase_hard;
		}

		//base scale save(for laser reduce)
		Vector3 scl = cashTransform.localScale;
		xscl_base = scl.x;
		yscl_base = scl.y;

		//pos x,y
		cashTransform.position = new Vector3( posx, posy, 0.0f );

		//bullet se
		mc.playSound(mc.se_enemybullet);

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
		cashTransform.Translate(((xx*xspd_base)+mc.getMapxMov()), (yy*yspd_base), 0);

		//move result process
		if ( (cashTransform.position.y > ymax) ||
			(cashTransform.position.y < ymin) ||
			(cashTransform.position.x < xmin) ||
			(cashTransform.position.x > xmax) ){
			if (alreadydelete == true) {
				return;
			}
			//objnum dec
			if (incobj == true) {
				mc.decObj ();
				incobj = false;
			} else {
				#if UNITY_EDITOR
				Debug.Log ("no inc dec enemybullet110");
				#endif
			}
			//delete this object
			alreadydelete = true;
			Destroy (gameObject);
		}

		////interval process

		//interval count
		intervalCnt++;
		if (intervalCnt > 0) {
			intervalCnt = 0;
			//nop
		}
	}


	//public
	//collision
	public void OnTriggerEnter2D(Collider2D coll){
		if (alreadydelete == true) {
			return;
		}
		string cotag = coll.gameObject.tag;
		if (cotag == "player") {
			//collision player
			//objnum dec
			if (incobj == true) {
				mc.decObj ();
				incobj = false;
			} else {
				#if UNITY_EDITOR
				Debug.Log ("no inc dec enemybullet110");
				#endif
			}
			//delete this
			alreadydelete = true;
			Destroy (gameObject);
		} else if (cotag == "sideMap") {
			//collision side map
			alreadydelete = true;
			//objnum dec
			if (incobj == true) {
				mc.decObj ();
				incobj = false;
			} else {
				#if UNITY_EDITOR
				Debug.Log ("no inc dec enemybullet110");
				#endif
			}
			//delete this
			alreadydelete = true;
			Destroy (gameObject);
		} else if (cotag == "playerLaser1") {
			//collision player laser 1
			//this reduce
			this.reduce();
		} else if (cotag == "playerLaser2") {
			//collision player laser 2
			//this reduce and vanish
			this.reduceAndVanish();
		} else if ( (cotag == "bomb1") || (cotag == "bomb2") || (cotag == "bombLaser") || (cotag == "wipe2") ) {
			//collision bomb 1/2
			//generate power up item(score)
			mc.generatePowerup100( mc.puType_score, cashTransform.position.x, cashTransform.position.y );
			//generate damage effect
			mc.generateEnemyDamageEffect( cashTransform.position.x, cashTransform.position.y );
			alreadydelete = true;
			//objnum dec
			if (incobj == true) {
				mc.decObj ();
				incobj = false;
			} else {
				#if UNITY_EDITOR
				Debug.Log ("no inc dec enemybullet110");
				#endif
			}
			//delete this
			alreadydelete = true;
			Destroy (gameObject);
		} else if (cotag == "wipe1") {
			//collision wipe 1
			//generate damage effect
			mc.generateEnemyDamageEffect( cashTransform.position.x, cashTransform.position.y );
			//objnum dec
			if (incobj == true) {
				mc.decObj ();
				incobj = false;
			} else {
				#if UNITY_EDITOR
				Debug.Log ("no inc dec enemybullet110");
				#endif
			}
			//delete this
			alreadydelete = true;
			Destroy (gameObject);
		} else {
			//collision other
		}
	}

	//reduce
	private void reduce(){
		Vector3 v = cashTransform.localScale;
		if (v.x >= (xscl_base * 0.6f)) {
			v.x = v.x * 0.92f;
			v.y = v.y * 0.92f;
			cashTransform.localScale = v;
		}
	}

	//reduce and vanish
	private void reduceAndVanish(){
		Vector3 v = cashTransform.localScale;
		v.x = v.x * 0.92f;
		v.y = v.y * 0.92f;
		cashTransform.localScale = v;
		if (v.x <= (xscl_base * 0.6f)) {
			//vanish
			if (alreadydelete == true) {
				return;
			}
			//add score
			mc.addGameScore( 150 );
			//generate damage effect
			mc.generateEnemyDamageEffect( cashTransform.position.x, cashTransform.position.y );
			//objnum dec
			if (incobj == true) {
				mc.decObj ();
				incobj = false;
			} else {
				#if UNITY_EDITOR
				Debug.Log ("no inc dec enemybullet110");
				#endif
			}
			//delete this
			alreadydelete = true;
			Destroy (gameObject);
		}
	}


	//public
	//set init status (x,y move speed (direction), pos x,y )
	public void setInitStatus( float xs, float ys, float px, float py ){
		this.xx = xs;
		this.yy = ys;
		this.posx = px;
		this.posy = py;
	}

}
