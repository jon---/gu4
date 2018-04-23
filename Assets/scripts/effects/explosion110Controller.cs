using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion110Controller : MonoBehaviour {
	//public
	public float speed_y;	//スクロール速度

	//private
	//local const

	//system local
	int intervalCnt;	//interval counter

	//system cash

	//component cash
	Transform cashTransform;
	GameObject mainCtr;
	mainController mc;
	Animator animt;

	//local
	//animation
	float animLength;	//animation length
	float animTimeCnt;	//animation time counter

	//pos x,y
	float posx;
	float posy;

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
		animt.speed = 1.5f;
		AnimatorStateInfo infAnim = animt.GetCurrentAnimatorStateInfo (0);
		animLength = infAnim.length;	//animation length
		animTimeCnt = 0;				//animation time counter

		//pos x,y
		cashTransform.position = new Vector3( posx, posy, 0.0f );

		//explosion se play
		mc.playSound(mc.se_playerexplosion);

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

		//animation term wait
		animTimeCnt = animTimeCnt + Time.deltaTime;
		if (animTimeCnt > (animLength/(animt.speed)) ) {
			//animation term
			//objnum dec
			if (incobj == true) {
				mc.decObj ();
				incobj = false;
			} else {
				#if UNITY_EDITOR
				Debug.Log ("no inc dec explosion110");
				#endif
			}
			//delete this
			Destroy( gameObject );
		}

		////interval process
		//interval count
		intervalCnt++;
		if (intervalCnt >= 2) {
			intervalCnt = 0;
			//move
			cashTransform.Translate (0, speed_y * -1, 0);
		}	
	}

	//public

	//set init status
	public void setInitStatus( float px, float py ){
		this.posx = px;
		this.posy = py;
	}

}
