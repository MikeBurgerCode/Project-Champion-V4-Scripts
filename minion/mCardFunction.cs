using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mCardFunction : Photon.PunBehaviour {

	public Card cData;
	public TurnManager turnManager;
	public bool selected;

	// Use this for initialization

	void Awake(){
		
		turnManager = GameObject.Find ("TurnManager").GetComponent<TurnManager> ();


	}
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDrag(){
		if(this.enabled && cData.ownerChamp.playerName==PhotonNetwork.player.NickName && turnManager.gameStart ){
			if(turnManager.isMyTurn(cData.ownerChamp.playerName)){
		Champion owner = cData.ownerChamp;
	//	if(owner.playerName==PhotonNetwork.player.NickName && turnMang.gameStart){
		//	if (turnMang.isMyTurn(PhotonNetwork.player.NickName )) {
				if (!owner.busy) {

				if (owner.mana >= cData.finalCost && owner.minions.Count < 7 && cData.powerReq()) {
						Debug.Log ("can summon");
						GameObject.Find("cursorStatus").GetComponent<cursorStatus> ().status = "summoning";
						selected = true;
					} else {
						Debug.Log ("can't summon");
						GameObject.Find("cursorStatus").GetComponent<cursorStatus> ().status = "";

					}

			

		//	}

		//}
				}
		
			}
				}
	}

	void OnMouseUp(){
		if (this.enabled && cData.ownerChamp.playerName == PhotonNetwork.player.NickName && turnManager.gameStart) {
			if (turnManager.isMyTurn (cData.ownerChamp.playerName)) {
				
					if (selected) {
		
						Playing ();

					}

					GameObject.Find ("cursorStatus").GetComponent<cursorStatus> ().status = "";
					selected = false;

			}
		}
	}
			

	void Playing(){
		
		Vector3 screenPoint = Input.mousePosition;

		screenPoint.z = -10;

		Vector3 mouseGamePos;
		Vector3 mPos = Input.mousePosition;

		mPos.z = -1 * Camera.main.transform.position.z;

		mouseGamePos = Camera.main.ScreenToWorldPoint (mPos);


		Ray ray = Camera.main.ScreenPointToRay(screenPoint);
		//Debug.DrawRay(
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 100)) {
			if (hit.transform.gameObject.tag == "summonZone") {

				int summonIndex = 0;
				if (mouseGamePos.x < hit.transform.position.x) {
					//minionIndex;
					summonIndex = 0;

				} else {
					summonIndex = -1;
				}

				cData.photonView.RPC ("PlayMinion", PhotonTargets.MasterClient, summonIndex);
				this.photonView.RPC ("RemoveCard", PhotonTargets.MasterClient);

			}


			//if (PhotonNetwork.isMasterClient) {
				if (hit.transform.gameObject.tag == cData.ownerChamp.tag+"Minion") {

					int minionIndex = hit.transform.GetSiblingIndex ();
					Debug.Log ("index:" + minionIndex);

					if (mouseGamePos.x < hit.transform.position.x) {
						//minionIndex;

					}
					if (mouseGamePos.x > hit.transform.position.x) {
						minionIndex += 1;

					}

					//if (minionIndex < 0) {
					//minionIndex = 0;
					//}
					if (minionIndex > 6) {
						minionIndex = 6;
					}

					cData.photonView.RPC ("PlayMinion", PhotonTargets.MasterClient, minionIndex);
					this.photonView.RPC ("RemoveCard", PhotonTargets.MasterClient);
				}

			//} 
				
			
		}

	}


}

