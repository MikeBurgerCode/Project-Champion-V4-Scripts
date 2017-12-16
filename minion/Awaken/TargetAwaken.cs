using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAwaken : MonoBehaviour {

	public Awaken awaken = new Awaken();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		awaken.update (this.gameObject);
	}

	public void cardPick( cardData cData){

		awaken.cardPick (cData);

	}

	void OnDestroy(){
	
		GameObject.Find ("cursorStatus").GetComponent<cursorStatus> ().status = "";

	}
}
