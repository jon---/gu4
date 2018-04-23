using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getStar100Controller : MonoBehaviour {
	//public

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

	//move
	float xx;
	float yy;

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
		//tarnsform cash
		cashTransform = transform;

		//main controller
		mainCtr = GameObject.Find ("mainController");
		mc = mainCtr.GetComponent<mainController> ();

		//animator
		animt = GetComponent<Animator>();
		animt.speed = 0.8f;
		AnimatorStateInfo infAnim = animt.GetCurrentAnimatorStateInfo (0);
		animLength = infAnim.length;	//animation length
		animTimeCnt = 0;				//animation time counter

		//x,y move
		xx = Random.Range (-0.09f, +0.09f);
		yy = Random.Range (-0.09f, +0.09f);

		//pos x,y
		cashTransform.position = new Vector3( posx, posy, 0.0f);

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
		cashTransform.Translate( xx, yy, 0);

		//move speed
		yy = yy - 0.005f;

		//animation term wait
		animTimeCnt = animTimeCnt + Time.deltaTime;
		if (animTimeCnt > (animLength/(animt.speed)) ) {
			//animation term
			//objnum dec
			if (incobj == true) {
				mc.decObj ();
			} else {
				#if UNITY_EDITOR
				Debug.Log ("no inc dec getstar");
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
