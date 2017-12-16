using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiCardData : MonoBehaviour {


	public cardData cData;
	public cardData originalData;
	public Champion ownerChamp = null;
	public bool spell = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (ownerChamp != null) {
		
			if (ownerChamp.grave.Count > 0) {
				cData = ownerChamp.grave [ownerChamp.grave.Count - 1];
				//this.gameObject.SetActive (true);
				this.transform.GetChild(0).gameObject.SetActive(true);
			} else {
			
				//this.gameObject.SetActive (false);
				this.transform.GetChild(0).gameObject.SetActive(false);

			}
		
		}


		switch (cData.type) {
		case "minion":
			originalData = new LoadData ().loadMinionCardData (cData.id);
			break;

		case "spell":
			originalData = new LoadData ().loadSpellCardData (cData.id);
			break;
		case "equip": 
			originalData = new LoadData ().loadEquipCardData (cData.id);
			break;

		case "ability": 
			//originalData = new LoadData ().loadManaCardData (cData.id);
			break;
		}


	}

	public void ButtonPress(){

		if (spell) {

			GameObject.Find ("SpellManager").GetComponent<SpellManager> ().spell.cardPick (cData);
			Destroy (GameObject.FindGameObjectWithTag ("CardListGui"));
		}else{

			GameObject.Find ("Awaken/ActiveManager").GetComponent<AaManager> ().cardPick (cData);
			Destroy (GameObject.FindGameObjectWithTag ("CardListGui"));
		}
	
	}

	public void OpenGrave(){

		GameObject instance = GameObject.Instantiate(Resources.Load("GraveListGui", typeof(GameObject))) as GameObject;
		instance.transform.parent = GameObject.Find ("Canvas").transform;
		instance.transform.localPosition = new Vector3 (0, 0, 0);

		foreach (var card in ownerChamp.grave) {
			instance.GetComponent<GuiCardList> ().addCard (card);
		}
	}

	public void TopButton(){

		Champion champ;
		if (PhotonNetwork.isMasterClient) {

			champ = GameObject.Find ("Player1").GetComponent<Champion> ();

		} else {
			champ = GameObject.Find ("Player2").GetComponent<Champion> ();

		}

		if (spell) {
			GameObject.Find ("SpellManager").GetComponent<SpellManager> ().spell.placeCard (true);
		} else {
		
		}
		champ.busy = false;
		this.gameObject.SetActive (false);
	}

	public void BottomButton(){

		Champion champ;
		if (PhotonNetwork.isMasterClient) {

			champ = GameObject.Find ("Player1").GetComponent<Champion> ();

		} else {
			champ = GameObject.Find ("Player2").GetComponent<Champion> ();

		}

		//champ.deck.Add (cData);
		//champ.deck.RemoveAt (0);

		if (spell) {
			GameObject.Find ("SpellManager").GetComponent<SpellManager> ().spell.placeCard (false);
		} else {
		
		}
		champ.busy = false;
		this.gameObject.SetActive (false);
	}
}
