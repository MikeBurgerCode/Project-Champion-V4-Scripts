using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellFunction : Photon.PunBehaviour {

	public Card card;
	public TurnManager turnManager;
	public bool selected;

	// Use this for initialization
	void Awake(){

		turnManager = GameObject.Find ("TurnManager").GetComponent<TurnManager> ();


	}
	// Update is called once per frame
	void Update () {

		turnManager = GameObject.Find ("TurnManager").GetComponent<TurnManager> ();
		
	}

	void OnMouseDrag(){
		Champion owner = card.ownerChamp;

		if (this.enabled && owner.playerName == PhotonNetwork.player.NickName && turnManager.gameStart) {

			if (turnManager.isMyTurn (PhotonNetwork.player.NickName)) {
				if (!owner.busy) {
					
					if (owner.mana >= card.finalCost && card.spell.hasTarget (owner) && card.powerReq ()) {

						if (card.spell.type == "play") {
						
								GameObject.Find ("cursorStatus").GetComponent<cursorStatus> ().status = "summoning";
							} else {
								GameObject.Find ("cursorStatus").GetComponent<cursorStatus> ().status = "target";
							}

							selected = true;
											
						}
					
				}
			}

		}
	}

	void OnMouseUp(){
		Champion owner = card.ownerChamp;
		if (this.enabled && selected == true && owner.playerName == PhotonNetwork.player.NickName) {

			if (card.spell.type == "play") {
				playingSpell ();
			} else {
				targetSpell ();
			}
		
		}

		GameObject.Find("cursorStatus").GetComponent<cursorStatus> ().status = "";
		selected = false;

	}
	void OnMouseEnter(){
		
	}
	void OnMouseExit(){
	}


	void playingSpell(){

		Vector3 mouseGamePos;
		Vector3 mPos = Input.mousePosition;

		mPos.z = -1 * Camera.main.transform.position.z;

		mouseGamePos = Camera.main.ScreenToWorldPoint (mPos);


		if (mouseGamePos.y > -7) {
			//cData.photonView.RPC ("cast", PhotonTargets.MasterClient);
			card.ownerChamp.mana-=card.finalCost;

			card.photonView.RPC ("RemoveCard", PhotonTargets.MasterClient);

			card.spell.cast(card.ownerChamp);

			card.gManager.PlayCardTrigger (new Serializer().SerializeToString(card.cData), card.ownerChamp.tag);

			card.ownerChamp.photonView.RPC ("addToPool", PhotonTargets.MasterClient, new Serializer ().SerializeToString (card.cData.colour));

			card.ownerChamp.grave.Add (card.cData);
		}

	}
	void targetSpell(){

		//cData.spell.
		Vector3 screenPoint = Input.mousePosition;
		screenPoint.z = -10;

		Ray ray = Camera.main.ScreenPointToRay (screenPoint);
		//Debug.DrawRay(
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit, 100)) {


			if (card.spell.isVaildTarget (hit.transform.gameObject)) {
				
				card.ownerChamp.mana -= card.finalCost;

				card.photonView.RPC ("RemoveCard", PhotonTargets.MasterClient);

				card.spell.cast(card.ownerChamp);
				card.spell.castOnTarget (hit.transform.gameObject);

				card.gManager.PlayCardTrigger (new Serializer().SerializeToString(card.cData), card.ownerChamp.tag);

				card.ownerChamp.photonView.RPC ("addToPool", PhotonTargets.MasterClient, new Serializer ().SerializeToString (card.cData.colour));

				card.ownerChamp.grave.Add (card.cData);

				
			} else {
			
				card.ownerChamp.sendMessage ("Invaild Target");
			
			}

		}
	
	}

	[PunRPC]
	void CastOnMinion(int minionIndex){

		minionData tMinion = card.gManager.minions[minionIndex];

	}

	[PunRPC]
	void CastOnChampion(string owner){

		Champion tChamp = GameObject.FindGameObjectWithTag (owner).GetComponent<Champion> ();
	
	}
}
