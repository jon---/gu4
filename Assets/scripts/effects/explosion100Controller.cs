using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion100Controller : MonoBehaviour {
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

	//mov x,y
	float xx;
	float yy;


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
		animt.speed = 2.8f;
		AnimatorStateInfo infAnim = animt.GetCurrentAnimatorStateInfo (0);
		animLength = infAnim.length;	//animation length
		animTimeCnt = 0;				//animation time counter

		//pos x,y
		cashTransform.position = new Vector3( posx, posy, 0.0f );

		//explosion se play
		mc.playSound(mc.se_enemyexplosion);

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
			cashTransform.Translate (0, speed_y * -1, 0);
			//move2
			cashTransform.Translate (xx, yy, 0);
		}	
	}

	//public

	//set init status
	public void setInitStatus( float px, float py, float xx = 0.0f, float yy = 0.0f ){
		this.posx = px;
		this.posy = py;
		this.xx = xx;
		this.yy = yy;
	}

}
