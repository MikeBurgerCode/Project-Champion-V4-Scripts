using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using TMPro;
using UnityEngine;


public class EndButton : MonoBehaviour {

	public MeshRenderer mr;
	public TurnManager tm;
	public TextMeshPro text;
	public Champion player;

	public Color c= new Color (0.5f, 0.5f, 0.5f, 1);
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {

		if (PhotonNetwork.isMasterClient) {
			player = GameObject.Find ("Player1").GetComponent<Champion> ();
		}else{
			player = GameObject.Find ("Player2").GetComponent<Champion> ();

		}

		if (tm.isMyTurn (PhotonNetwork.player.NickName)) {
			text.text="End Turn";

		} else {


			text.text="Enemy Turn";
		}
			mr.material.color = c;

	}

	void OnMouseEnter(){
		if (tm.isMyTurn (PhotonNetwork.player.NickName) && tm.gameStart) {

			c = new Color (0.9f, 0.9f, 0.9f, 1);
		}
	}

	void OnMouseExit(){
		if (tm.isMyTurn (PhotonNetwork.player.NickName) && tm.gameStart) {
			c= new Color (1f, 1f, 1f, 1);
		}
	}

	void OnMouseUp(){
	
		if (tm.isMyTurn (PhotonNetwork.player.NickName) && !player.busy && tm.gameStart) {
		

			Vector3 screenPoint = Input.mousePosition;
			screenPoint.z = -10;

			Ray ray = Camera.main.ScreenPointToRay (screenPoint);
			//Debug.DrawRay(
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit, 100)) {
				
				//Debug.Log (hit.transform.gameObject.tag);
				if (hit.transform.gameObject == this.gameObject) {
					Debug.Log ("button");
					if (!player.busy) {
						//Debug.Log ("end");
						tm.endTurn ();
					}
				}
		
			}
		}
	}
}
