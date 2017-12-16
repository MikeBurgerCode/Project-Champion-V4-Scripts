using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MullButton : MonoBehaviour {

	public MeshRenderer mr;
	public TurnManager tm;
	public Color c= new Color (1f,1f, 1f, 1);

	public bool mull = false;
	public GameObject otherButton;
	public Champion player;

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

		mr.material.color = c;
	}

	void OnMouseEnter(){
		
				c = new Color (0.9f, 0.9f, 0.9f, 1);
		}

	void OnMouseExit(){
		
				c = new Color (1f, 1f, 1f, 1);
			
	}

	void OnMouseUp(){
	
		Vector3 screenPoint = Input.mousePosition;
		screenPoint.z = -10;

		Ray ray = Camera.main.ScreenPointToRay (screenPoint);
		//Debug.DrawRay(
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit, 100)) {
			//Debug.Log (hit.transform.gameObject.tag);
			if (hit.transform.gameObject == this.gameObject) {
			
				if (mull) {
					player.photonView.RPC ("mulligian", PhotonTargets.MasterClient);
				}

				tm.photonView.RPC("ready",PhotonTargets.MasterClient, PhotonNetwork.player.NickName);
				Destroy (this.gameObject);
				Destroy (otherButton);
			}
		}
	}
}
