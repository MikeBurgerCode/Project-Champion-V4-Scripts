using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class minionFunction : MonoBehaviour {

	public bool selected,over,attacking=false;
	public minionData mData;
	public TurnManager turnManager;

	// Use this for initialization
	void Start () {
		turnManager = GameObject.Find ("TurnManager").GetComponent<TurnManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnMouseDrag(){
	
		if (mData.ownerChamp.playerName == PhotonNetwork.player.NickName && !mData.ownerChamp.busy) {
			if (turnManager.isMyTurn (mData.ownerChamp.playerName)) {
				if (mData.atkTimes > 0 && mData.attack > 0 && !mData.summonSick && !mData.exhaust) {
					GameObject.Find ("cursorStatus").GetComponent<cursorStatus> ().status = "attacking";
					attacking = true;
				}
			}
		}
	}
	void OnMouseUp(){
	
		if (attacking) {
			Attacking();
		}
		GameObject.Find("cursorStatus").GetComponent<cursorStatus> ().status = "";
		attacking = false;
	}
	void OnMouseEnter(){

		over = true;
		Debug.Log ("enter");
		StartCoroutine(highLight (0.25f));


		//this.transform.position += new Vector3 (0.0f, 5.0f, 0.0f);
	}

	void OnMouseOver(){
		if (mData.ownerChamp.playerName == PhotonNetwork.player.NickName && !mData.ownerChamp.busy) {
			if (Input.GetMouseButtonDown (1)) {

				//Debug.Log ("right click");
				if (!mData.exhaust) {
					if (mData.aaList.Count > 0) {
			
			
						GameObject aList = Instantiate (Resources.Load ("Prefab/ActiveList")) as GameObject;
						aList.transform.position = new Vector3 (mData.gameObject.transform.position.x, mData.gameObject.transform.position.y, 4.4f);

						

						foreach (var act in mData.aaList) {
				
							aList.GetComponent<ActiveList> ().addActive (mData, act);
				
						}

						foreach (var equip in mData.equipment) {
							foreach (var act in equip.aaList) {

								aList.GetComponent<ActiveList> ().addActive (mData, act);

							}

						
						}


					}

					bool hasActWepAbi = false;
					foreach (var equip in mData.equipment) {
						if (equip.aaList.Count > 0) {
							hasActWepAbi = true;
							break;
						}
					
					}

					if (hasActWepAbi) {
						
						GameObject aList = Instantiate (Resources.Load ("Prefab/ActiveList")) as GameObject;
						aList.transform.position = new Vector3 (mData.gameObject.transform.position.x, mData.gameObject.transform.position.y, 4.4f);

						foreach (var equip in mData.equipment) {
							foreach (var act in equip.aaList) {

								aList.GetComponent<ActiveList> ().addActive (mData, act);

							}


						}
					}
				}
		
			}
		}
	
	}
	void OnMouseExit(){
		Debug.Log ("exit");
		over = false;
		//GameObject.Find("RenderCard").transform.GetChild(0).gameObject.SetActive (false);
		//GameObject.Find ("RenderCard").GetComponent<cardRender> ().card = null;
		GameObject.Find ("infoCard").transform.GetChild (0).gameObject.SetActive (false);
		GameObject.Find ("minionInfoCard").transform.GetChild (0).gameObject.SetActive (false);
		GameObject.Find ("minionInfoCard").GetComponent<mInfoCardRender> ().mData = null;
		//highlightPos = new Vector3 (0.0f, 0.0f, 0.0f);
		//highlightScale = new Vector3 (1.0f, 1.0f, 1.0f);
		//this.transform.Find ("Card").transform.localPosition = Vector3.zero;
		//	this.transform.Find("Card").transform.localScale= new Vector3(1.0f,1.0f,1.0f);


		//this.transform.position += new Vector3 (0.0f, 5.0f, 0.0f);
	}

	void OnDestroy(){
		Debug.Log ("exit");
		over = false;
		GameObject.Find("infoCard").transform.GetChild(0).gameObject.SetActive (false);
		//GameObject.Find ("RenderCard").GetComponent<cardRender> ().card = null;
		GameObject.Find ("minionInfoCard").transform.GetChild (0).gameObject.SetActive (false);
		GameObject.Find ("minionInfoCard").GetComponent<mInfoCardRender> ().mData = null;
	
	}

	IEnumerator highLight(float time){


		yield return new WaitForSeconds (time);
		//Debug.Log (over);
		if (over == true) {


			Vector3 screenPoint = Input.mousePosition;
			screenPoint.z = -10;

			Ray ray = Camera.main.ScreenPointToRay (screenPoint);
			//Debug.DrawRay(
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit, 100)) {
				if (hit.transform.gameObject == this.gameObject) {
					//Debug.Log ("unit data");

					GameObject.Find ("minionInfoCard").transform.GetChild (0).gameObject.SetActive (true);
					GameObject.Find ("minionInfoCard").GetComponent<mInfoCardRender> ().mData = mData;
					//highlightPos = new Vector3 (0.0f, 12.0f, -1.0f);
					//highlightScale = new Vector3 (2.75f,2.75f, 2.75f);

				}
			}
		}
	}

	public void Attacking(){

		Vector3 screenPoint = Input.mousePosition;
		screenPoint.z = -10;

		Ray ray = Camera.main.ScreenPointToRay (screenPoint);
		//Debug.DrawRay(
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit, 100)) {
			//Debug.Log (hit.transform.gameObject.tag);
			if (hit.transform.gameObject.tag == mData.ownerChamp.opponent.tag + "Minion") {
				Debug.Log ( "attacking");
				minionData target = hit.transform.gameObject.GetComponent<minionData> ();

				if (checkForTaunt (target)) {
					Debug.Log ( "Attack the minion with taunt");
					mData.ownerChamp.sendMessage ("You must attack the enemy with taunt");
				
				} else {
					mData.gManager.photonView.RPC ("MinionAttackM", PhotonTargets.MasterClient, mData.minionIndex, target.minionIndex);
				}



				//mData.gManager.photonView.RPC ("MinionAttackM", PhotonTargets.MasterClient, mData.minionIndex, target.minionIndex);
				//Debug.Log ( mData.ownerChamp.opponent.tag + "Minion");

			}

			if(hit.transform.gameObject.tag == mData.ownerChamp.opponent.tag){

				Champion target = mData.ownerChamp.opponent;

				if (checkForTaunt (target)) {
					Debug.Log ( "Attack the minion with taunt");
					mData.ownerChamp.sendMessage ("You must attack the enemy with taunt");

				} else {
					mData.gManager.photonView.RPC ("MinionAttackC", PhotonTargets.MasterClient, mData.minionIndex, target.tag);
				}

			}
		
		}
	}

	public bool checkForTaunt(minionData target){

		if (target.hasTaunt()) { // if target already has taunt
		
			return false;

		}

		foreach (var minion in target.ownerChamp.minions) {
		
			minionData mData = minion.GetComponent<minionData> ();

			if (mData.hasTaunt()) {
				return true;
			}
		
		}

		return false;
	}

	public bool checkForTaunt(Champion target){

		if (target.taunt) { // if target already has taunt

			return false;

		}

		foreach (var minion in target.minions) {

			minionData mData = minion.GetComponent<minionData> ();

			if (mData.hasTaunt()) {
				return true;
			}

		}

		return false;
	}
}
