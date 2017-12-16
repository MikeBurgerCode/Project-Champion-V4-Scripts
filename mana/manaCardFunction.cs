using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manaCardFunction : Photon.PunBehaviour {

	public Card cData;
	public TurnManager turnManager;
	public bool selected;

	void Awake(){

		turnManager = GameObject.Find ("TurnManager").GetComponent<TurnManager> ();

	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDrag(){
		if (this.enabled && cData.ownerChamp.playerName==PhotonNetwork.player.NickName && turnManager.gameStart) {
			if (turnManager.isMyTurn (cData.ownerChamp.playerName)) {
				if (!cData.ownerChamp.busy) {
					
						if (!cData.ownerChamp.manaPlay) {
							selected = true;
							GameObject.Find ("cursorStatus").GetComponent<cursorStatus> ().status = "summoning";
						}

				}
			}
		}
	}

	void OnMouseUp(){
		if (this.enabled && cData.ownerChamp.playerName == PhotonNetwork.player.NickName && turnManager.gameStart) {
			if (turnManager.isMyTurn (cData.ownerChamp.playerName)) {
				if (selected) {
					Vector3 mouseGamePos;
					Vector3 mPos = Input.mousePosition;

					mPos.z = -1 * Camera.main.transform.position.z;

					mouseGamePos = Camera.main.ScreenToWorldPoint (mPos);


					if (mouseGamePos.y > -7) {
		
						//addPower ();
						this.photonView.RPC ("addPower", PhotonTargets.MasterClient);

						this.photonView.RPC ("RemoveCard", PhotonTargets.MasterClient);
					}
				}
				GameObject.Find ("cursorStatus").GetComponent<cursorStatus> ().status = "";
				selected = false;
			}
		}
	}

	[PunRPC]
	void addPower(){

		/*cData.ownerChamp.manaPlay = true;
		foreach (var colour in cData.colour) {
			if (colour == "r") {
			
				cData.ownerChamp.power [0] += 1;
			
			}

			if (colour == "u") {

				cData.ownerChamp.power [1] += 1;

			}

			if (colour == "y") {

				cData.ownerChamp.power [2] += 1;

			}

			if (colour == "p") {

				cData.ownerChamp.power [3] += 1;

			}

			if (colour == "g") {

				cData.ownerChamp.power [4] += 1;

			}

			if (colour == "w") {

				cData.ownerChamp.power [5] += 1;

			}
		
		}

		cData.ownerChamp.mana += 1;
		cData.ownerChamp.maxMana += 1;
	*/
	}


}
