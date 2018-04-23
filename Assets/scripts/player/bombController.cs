using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombController : MonoBehaviour {
	//public

	//private
	//local const

	//system local
	int intervalCnt;	//interval counter

	//system cash

	//component cash
	SpriteRenderer sr;
	Animator animt;
	GameObject mainCtr;
	mainController mc;

	//local
	//animation
	float animLength;	//animation length
	float animTimeCnt;	//animation time counter


	// Use this for initialization
	void Start () {
		//system init
		intervalCnt = 0;

		//cash
		//sprite
		sr = GetComponent<SpriteRenderer>();
		//main controller
		mainCtr = GameObject.Find ("mainController");
		mc = mainCtr.GetComponent<mainController> ();

		//animator
		animt = GetComponent<Animator>();
		if (this.tag == "bomb1") {
			animt.speed = 2.0f;
		} else if ( this.tag == "bomb2" ){
			animt.speed = 0.15f;
			sr.color = new Color (255.0f / 255.0f, 140.0f / 255.0f, 0.0f / 255.0f, 150.0f / 255.0f);
		}
		AnimatorStateInfo infAnim = animt.GetCurrentAnimatorStateInfo (0);
		animLength = infAnim.length;	//animation length
		animTimeCnt = 0;				//animation time counter

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

}
