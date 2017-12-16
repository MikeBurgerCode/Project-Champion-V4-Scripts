using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardFunction : MonoBehaviour {

	public bool selected,over;

	public Card card;
	public Vector3 highlightPos= new Vector3 (0.0f, 0.0f, 0.0f);
	public Vector3 highlightScale= new Vector3 (1.0f, 1.0f, 1.0f);
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//string t = card.cData.type;
		if (card!= null) {
			switch (card.cData.type) {
			case "minion":
			//cData = new LoadData ().loadMinionCardData (id);
				this.GetComponent<mCardFunction> ().enabled = true;
				this.GetComponent<manaCardFunction> ().enabled = false;
				this.GetComponent<SpellFunction> ().enabled = false;
				this.GetComponent<equipCardFunction> ().enabled = false;
				break;

			case "spell":
				this.GetComponent<mCardFunction> ().enabled = false;
				this.GetComponent<manaCardFunction> ().enabled = false;
				this.GetComponent<SpellFunction> ().enabled = true;
				this.GetComponent<equipCardFunction> ().enabled = false;
				break;
			case "weapon": 
				this.GetComponent<mCardFunction> ().enabled = false;
				this.GetComponent<manaCardFunction> ().enabled = false;
				this.GetComponent<SpellFunction> ().enabled = false;
				this.GetComponent<equipCardFunction> ().enabled = false;
				break;
			case "equip": 
				this.GetComponent<mCardFunction> ().enabled = false;
				this.GetComponent<manaCardFunction> ().enabled = false;
				this.GetComponent<SpellFunction> ().enabled = false;
				this.GetComponent<equipCardFunction> ().enabled = true;
				break;
			}
		}

	/*	if (cData.ownerChamp.playerName == PhotonNetwork.player.NickName) {
		
			this.gameObject.transform.GetChild (0).transform.rotation = new Quaternion (0, 0, 0, 0);

		} else {

			this.gameObject.transform.GetChild (0).transform.rotation = new Quaternion (0, 180, 0, 0);
		}
	*/
			//this.transform.Find ("card").transform.localScale = highlightScale;
		//	this.transform.Find ("card").transform.localPosition = highlightPos;
		
	}

	void OnMouseEnter(){
		if (this.enabled && card.ownerChamp.playerName==PhotonNetwork.player.NickName) {
		over = true;
		//Debug.Log ("enter");
		StartCoroutine(highLight (0.25f));


		//this.transform.position += new Vector3 (0.0f, 5.0f, 0.0f);
		}
	}

	void OnMouseExit(){
		if (this.enabled && card.ownerChamp.playerName==PhotonNetwork.player.NickName) {
		Debug.Log ("exit");
		over = false;
		//GameObject.Find("RenderCard").transform.GetChild(0).gameObject.SetActive (false);
		//GameObject.Find ("RenderCard").GetComponent<cardRender> ().card = null;

			GameObject.Find("infoCard").transform.GetChild(0).gameObject.SetActive (false);

		//highlightPos = new Vector3 (0.0f, 0.0f, 0.0f);
		//highlightScale = new Vector3 (1.0f, 1.0f, 1.0f);
		//this.transform.Find ("Card").transform.localPosition = Vector3.zero;
	//	this.transform.Find("Card").transform.localScale= new Vector3(1.0f,1.0f,1.0f);
		}


		//this.transform.position += new Vector3 (0.0f, 5.0f, 0.0f);
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

					//GameObject.Find("RenderCard").transform.GetChild(0).gameObject.SetActive (true);
					GameObject.Find("infoCard").transform.GetChild(0).gameObject.SetActive (true);
					GameObject.Find ("infoCard").transform.GetChild (0).gameObject.GetComponent<GuiCardData> ().cData = card.cData;
					//GameObject.Find ("RenderCard").GetComponent<cardRender> ().card = card;
					//highlightPos = new Vector3 (0.0f, 12.0f, -1.0f);
					//highlightScale = new Vector3 (2.75f,2.75f, 2.75f);

				}
			}
		}//callback();

	}

	void OnMouseDrag(){

	}

	void OnMouseUp(){
	
	}


}
