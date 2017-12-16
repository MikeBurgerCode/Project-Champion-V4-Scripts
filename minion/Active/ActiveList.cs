using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveList : MonoBehaviour {

	public GameObject button;
	public Transform content;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void addActive(minionData mData , Active active){
	
		GameObject but=Instantiate (button);
		but.transform.parent = content;
		but.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
		but.GetComponent<ActiveButton> ().active = active;
		but.GetComponent<ActiveButton> ().mData = mData;
	}
}
