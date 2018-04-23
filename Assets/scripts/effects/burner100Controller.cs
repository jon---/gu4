using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class burner100Controller : MonoBehaviour {
	//public

	//private
	//local const
	//x,y min/max
	const float xmin = -6.0f;	//todo: maincontroller public
	const float xmax = 6.0f;
	const float ymin = -12.0f;
	const float ymax = 12.0f;

	//system local
	int intervalCnt;	//interval counter

	//system cash
	Transform cashTransform;
	//component cash
	GameObject mainCtr;
	mainController mc;
	Animator animt;

	//local
	//animation
	float animLength;	//animation length
	float animTimeCnt;	//animation time counter

	//move x,y
	float xx=0.0f;
	float yy=0.0f;

	//pos x,y
	float posx;
	float posy;

	//scale
	float sclx;
	float scly;


	// Use this for initialization
	void Start () {
		//system init
		intervalCnt = 0;

		//cash
		//cash transform
		cashTransform = transform;
		//main controller
		mainCtr = GameObject.Find ("mainController");
		mc = mainCtr.GetComponent<mainController> ();

		//animator
		animt = GetComponent<Animator>();
		animt.speed = 2.0f;
		AnimatorStateInfo infAnim = animt.GetCurrentAnimatorStateInfo (0);
		animLength = infAnim.length;	//animation length
		animTimeCnt = 0;				//animation time counter

		//pos x,y
		cashTransform.position = new Vector3( posx, posy, 0.0f );

		//scale
		cashTransform.localScale = new Vector3 (sclx, scly, 1.0f);

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
			//move
			cashTransform.Translate( xx, yy, 0);
			Vector3 ppos = cashTransform.position;
			if( (ppos.x > xmax) || (ppos.x < xmin) ||(ppos.y > ymax) || (ppos.y < ymin) ){
				//objnum dec
				mc.decObj();
				//delete this
				Destroy( gameObject );
			}
		}	
	}

	//public

	//set init status
	public void setInitStatus(  float xs, float ys, float px, float py, float sx, float sy ){
		this.xx = xs;
		this.yy = ys;
		this.posx = px;
		this.posy = py;
		this.sclx = sx;
		this.scly = sy;
	}

}
