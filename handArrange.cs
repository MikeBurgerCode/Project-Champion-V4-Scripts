using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handArrange : MonoBehaviour {

	// Use this for initialization

	public float gap=0.2f;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		var c = (GameObject)Resources.Load("Card");
		float space = Mathf.Abs((c.GetComponent<BoxCollider> ().size.x/2)+(gap-0.1f*this.transform.childCount));

		if(this.gameObject.transform.childCount==1){
			this.gameObject.transform.GetChild(0).transform.localPosition = new Vector3 (0,0,0);
		}

		if(this.gameObject.transform.childCount==2){
			this.gameObject.transform.GetChild(0).transform.localPosition = new Vector3 (-space,0,0);
			this.gameObject.transform.GetChild(1).transform.localPosition = new Vector3 (space,0,-0.1f);
		}

		if(this.gameObject.transform.childCount==3){
			this.gameObject.transform.GetChild(0).transform.localPosition = new Vector3 (-space*2,0,0);
			this.gameObject.transform.GetChild(1).transform.localPosition = new Vector3 (0,0,-0.1f);
			this.gameObject.transform.GetChild(2).transform.localPosition = new Vector3 (space*2,0,-0.2f);
		}

		if(this.gameObject.transform.childCount==4){
			this.gameObject.transform.GetChild(0).transform.localPosition = new Vector3 (-space*3,0,0);
			this.gameObject.transform.GetChild(1).transform.localPosition = new Vector3 (-space,0,-0.1f);
			this.gameObject.transform.GetChild(2).transform.localPosition = new Vector3 (space,0,-0.2f);
			this.gameObject.transform.GetChild(3).transform.localPosition = new Vector3 (space*3,0,-0.3f);
		}

		if(this.gameObject.transform.childCount==5){
			this.gameObject.transform.GetChild(0).transform.localPosition = new Vector3 (-space*4,0,0);
			this.gameObject.transform.GetChild(1).transform.localPosition = new Vector3 (-space*2,0,-0.1f);
			this.gameObject.transform.GetChild(2).transform.localPosition = new Vector3 (0,0,-0.2f);
			this.gameObject.transform.GetChild(3).transform.localPosition = new Vector3 (space*2,0,-0.3f);
			this.gameObject.transform.GetChild(4).transform.localPosition = new Vector3 (space*4,0,-0.4f);
		}

		if(this.gameObject.transform.childCount==6){
			this.gameObject.transform.GetChild(0).transform.localPosition = new Vector3 (-space*5,0,0);
			this.gameObject.transform.GetChild(1).transform.localPosition = new Vector3 (-space*3,0,-0.1f);
			this.gameObject.transform.GetChild(2).transform.localPosition = new Vector3 (-space,0,-0.2f);
			this.gameObject.transform.GetChild(3).transform.localPosition = new Vector3 (space,0,-0.3f);
			this.gameObject.transform.GetChild(4).transform.localPosition = new Vector3 (space*3,0,-0.4f);
			this.gameObject.transform.GetChild(5).transform.localPosition = new Vector3 (space*5,0,-0.5f);
		}

		if(this.gameObject.transform.childCount==7){
			this.gameObject.transform.GetChild(0).transform.localPosition = new Vector3 (-space*6,0,0);
			this.gameObject.transform.GetChild(1).transform.localPosition = new Vector3 (-space*4,0,-0.1f);
			this.gameObject.transform.GetChild(2).transform.localPosition = new Vector3 (-space*2,0,-0.2f);
			this.gameObject.transform.GetChild(3).transform.localPosition = new Vector3 (0,0,-0.3f);
			this.gameObject.transform.GetChild(4).transform.localPosition = new Vector3 (space*2,0,-0.4f);
			this.gameObject.transform.GetChild(5).transform.localPosition = new Vector3 (space*4,0,-0.5f);
			this.gameObject.transform.GetChild(6).transform.localPosition = new Vector3 (space*6,0,-0.6f);
		}

		if(this.gameObject.transform.childCount==8){
			this.gameObject.transform.GetChild(0).transform.localPosition = new Vector3 (-space*7,0,0);
			this.gameObject.transform.GetChild(1).transform.localPosition = new Vector3 (-space*5,0,-0.1f);
			this.gameObject.transform.GetChild(2).transform.localPosition = new Vector3 (-space*3,0,-0.2f);
			this.gameObject.transform.GetChild(3).transform.localPosition = new Vector3 (-space,0,-0.3f);
			this.gameObject.transform.GetChild(4).transform.localPosition = new Vector3 (space,0,-0.4f);
			this.gameObject.transform.GetChild(5).transform.localPosition = new Vector3 (space*3,0,-0.5f);
			this.gameObject.transform.GetChild(6).transform.localPosition = new Vector3 (space*5,0,-0.6f);
			this.gameObject.transform.GetChild(7).transform.localPosition = new Vector3 (space*7,0,-0.7f);
		}

		if(this.gameObject.transform.childCount==9){
			this.gameObject.transform.GetChild(0).transform.localPosition = new Vector3 (-space*8,0,0);
			this.gameObject.transform.GetChild(1).transform.localPosition = new Vector3 (-space*6,0,-0.1f);
			this.gameObject.transform.GetChild(2).transform.localPosition = new Vector3 (-space*4,0,-0.2f);
			this.gameObject.transform.GetChild(3).transform.localPosition = new Vector3 (-space*2,0,-0.3f);
			this.gameObject.transform.GetChild(4).transform.localPosition = new Vector3 (0,0,-0.4f);
			this.gameObject.transform.GetChild(5).transform.localPosition = new Vector3 (space*2,0,-0.5f);
			this.gameObject.transform.GetChild(6).transform.localPosition = new Vector3 (space*4,0,-0.6f);
			this.gameObject.transform.GetChild(7).transform.localPosition = new Vector3 (space*6,0,-0.7f);
			this.gameObject.transform.GetChild(8).transform.localPosition = new Vector3 (space*8,0,-0.8f);
		}

		if(this.gameObject.transform.childCount==10){
			this.gameObject.transform.GetChild(0).transform.localPosition = new Vector3 (-space*9,0,0);
			this.gameObject.transform.GetChild(1).transform.localPosition = new Vector3 (-space*7,0,-0.1f);
			this.gameObject.transform.GetChild(2).transform.localPosition = new Vector3 (-space*5,0,-0.2f);
			this.gameObject.transform.GetChild(3).transform.localPosition = new Vector3 (-space*3,0,-0.3f);
			this.gameObject.transform.GetChild(4).transform.localPosition = new Vector3 (-space,0,-0.4f);
			this.gameObject.transform.GetChild(5).transform.localPosition = new Vector3 (space,0,-0.5f);
			this.gameObject.transform.GetChild(6).transform.localPosition = new Vector3 (space*3,0,-0.6f);
			this.gameObject.transform.GetChild(7).transform.localPosition = new Vector3 (space*5,0,-0.7f);
			this.gameObject.transform.GetChild(8).transform.localPosition = new Vector3 (space*7,0,-0.8f);
			this.gameObject.transform.GetChild(9).transform.localPosition = new Vector3 (space*9,0,-0.9f);
		}
	}

}
