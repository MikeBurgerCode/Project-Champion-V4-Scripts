using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minionArrange : MonoBehaviour {


	public float gap=0.2f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		var c = (GameObject)Resources.Load("Minion");
		float space = (c.GetComponent<BoxCollider> ().size.x/2)+gap;

		if(this.gameObject.transform.childCount==1){
			this.gameObject.transform.GetChild(0).transform.localPosition = new Vector3 (0,0,0);
		}

		if(this.gameObject.transform.childCount==2){
			this.gameObject.transform.GetChild(0).transform.localPosition = new Vector3 (-space,0,0);
			this.gameObject.transform.GetChild(1).transform.localPosition = new Vector3 (space,0,0);
		}

		if(this.gameObject.transform.childCount==3){
			this.gameObject.transform.GetChild(0).transform.localPosition = new Vector3 (-space*2,0,0);
			this.gameObject.transform.GetChild(1).transform.localPosition = new Vector3 (0,0,0);
			this.gameObject.transform.GetChild(2).transform.localPosition = new Vector3 (space*2,0,0);
		}

		if(this.gameObject.transform.childCount==4){
			this.gameObject.transform.GetChild(0).transform.localPosition = new Vector3 (-space*3,0,0);
			this.gameObject.transform.GetChild(1).transform.localPosition = new Vector3 (-space,0,0);
			this.gameObject.transform.GetChild(2).transform.localPosition = new Vector3 (space,0,0);
			this.gameObject.transform.GetChild(3).transform.localPosition = new Vector3 (space*3,0,0);
		}

		if(this.gameObject.transform.childCount==5){
			this.gameObject.transform.GetChild(0).transform.localPosition = new Vector3 (-space*4,0,0);
			this.gameObject.transform.GetChild(1).transform.localPosition = new Vector3 (-space*2,0,0);
			this.gameObject.transform.GetChild(2).transform.localPosition = new Vector3 (0,0,0);
			this.gameObject.transform.GetChild(3).transform.localPosition = new Vector3 (space*2,0,0);
			this.gameObject.transform.GetChild(4).transform.localPosition = new Vector3 (space*4,0,0);
		}

		if(this.gameObject.transform.childCount==6){
			this.gameObject.transform.GetChild(0).transform.localPosition = new Vector3 (-space*5,0,0);
			this.gameObject.transform.GetChild(1).transform.localPosition = new Vector3 (-space*3,0,0);
			this.gameObject.transform.GetChild(2).transform.localPosition = new Vector3 (-space,0,0);
			this.gameObject.transform.GetChild(3).transform.localPosition = new Vector3 (space,0,0);
			this.gameObject.transform.GetChild(4).transform.localPosition = new Vector3 (space*3,0,0);
			this.gameObject.transform.GetChild(5).transform.localPosition = new Vector3 (space*5,0,0);
		}

		if(this.gameObject.transform.childCount==7){
			this.gameObject.transform.GetChild(0).transform.localPosition = new Vector3 (-space*6,0,0);
			this.gameObject.transform.GetChild(1).transform.localPosition = new Vector3 (-space*4,0,0);
			this.gameObject.transform.GetChild(2).transform.localPosition = new Vector3 (-space*2,0,0);
			this.gameObject.transform.GetChild(3).transform.localPosition = new Vector3 (0,0,0);
			this.gameObject.transform.GetChild(4).transform.localPosition = new Vector3 (space*2,0,0);
			this.gameObject.transform.GetChild(5).transform.localPosition = new Vector3 (space*4,0,0);
			this.gameObject.transform.GetChild(6).transform.localPosition = new Vector3 (space*6,0,0);
		}
	
	}
}
