using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missileBombController : MonoBehaviour {
	//public

	//private
	//local const

	//system local
	int intervalCnt;	//interval counter

	//system cash

	//component cash
	Transform cashTransfrom;
	SpriteRenderer sr;
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


	// Use this for initialization
	void Start () {
		//system init
		intervalCnt = 0;

		//cash
		//cash transform
		cashTransfrom = transform;
		//sprite
		sr = GetComponent<SpriteRenderer>();
		//main controller
		mainCtr = GameObject.Find ("mainController");
		mc = mainCtr.GetComponent<mainController> ();

		//animator
		animt = GetComponent<Animator>();
		animt.speed = 0.7f;
		AnimatorStateInfo infAnim = animt.GetCurrentAnimatorStateInfo (0);
		animLength = infAnim.length;	//animation length
		animTimeCnt = 0;				//animation time counter

		//pos x,y
		//(set from parent objects)
//		posx = 0.0f;
//		posy = 0.0f;

		//position
		cashTransfrom.position = new Vector3 (posx, posy, 0.0f);

		//se play
		mc.playSound(mc.se_playermissilebomb);

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

		//rotate
		cashTransfrom.Rotate( new Vector3(0.0f, 0.0f, -40.0f) );

		//animation term wait
		animTimeCnt = animTimeCnt + Time.deltaTime;
		if (animTimeCnt > (animLength/(animt.speed)) ) {
			//animation term
			//objnum dec
			mc.decObj();
			//delete this
			Destroy( gameObject );
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
	//set init status
	public void setInitStatus( float px, float py ){
		this.posx = px;
		this.posy = py;
	}

}
