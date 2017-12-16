using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class equipCardFunction : MonoBehaviour {

	public Card card;
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
		if(this.enabled && card.ownerChamp.playerName==PhotonNetwork.player.NickName && turnManager.gameStart ){
			if(turnManager.isMyTurn(card.ownerChamp.playerName)){
				Champion owner = card.ownerChamp;
				//	if(owner.playerName==PhotonNetwork.player.NickName && turnMang.gameStart){
				//	if (turnMang.isMyTurn(PhotonNetwork.player.NickName )) {
				if (!owner.busy) {
					if (owner.mana >= card.finalCost && card.spell.hasTarget (owner) && card.powerReq ()) {
						GameObject.Find ("cursorStatus").GetComponent<cursorStatus> ().status = "target";
						selected = true;
					}
				}
			
			}
		}
	}

	void OnMouseUp(){
		if (this.enabled && card.ownerChamp.playerName == PhotonNetwork.player.NickName && turnManager.gameStart) {
			if (turnManager.isMyTurn (card.ownerChamp.playerName)) {

				if (selected) {
					rayCast ();
				}

				GameObject.Find ("cursorStatus").GetComponent<cursorStatus> ().status = "";
				selected = false;
			}
		}
	}

	public void rayCast(){
	
		Vector3 screenPoint = Input.mousePosition;
		screenPoint.z = -10;

		Ray ray = Camera.main.ScreenPointToRay (screenPoint);
		//Debug.DrawRay(
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit, 100)) {

			if (hit.transform.gameObject.tag == card.owner) {
			
			}
		
			if (hit.transform.gameObject.tag == card.owner+"Minion") {

				card.ownerChamp.mana -= card.finalCost;

				card.photonView.RPC ("RemoveCard", PhotonTargets.MasterClient);

				string data = new Serializer ().SerializeToString (card.cData);
				hit.transform.gameObject.GetComponent<minionData> ().photonView.RPC ("addEquip", PhotonTargets.MasterClient, data);

				card.gManager.PlayCardTrigger (new Serializer().SerializeToString(card.cData), card.ownerChamp.tag);

				card.ownerChamp.photonView.RPC ("addToPool", PhotonTargets.MasterClient, new Serializer ().SerializeToString (card.cData.colour));
			}
		
		}
	}
}
