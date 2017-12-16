using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using TMPro;
using UnityEngine;

public class AbilityButton : Photon.PunBehaviour, IPunObservable {

	public MeshRenderer mr;
	public TurnManager turnManager;
	public Champion ownerChamp;
	public int num,a,totalReq = 0;
	public bool ready,selected,over = false;
	//public bool active =true;
	public TextMeshPro text;

	public ChampAbility ability;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (PhotonNetwork.isMasterClient) {
			ability = ownerChamp.abilites [num];
			ability.champTag = ownerChamp.tag;
		
			//render ();
			a=0;
			totalReq=0;
			for (int i = 0; i < 6; i++) {
				if(ability.reqPower [i]>0) {

					totalReq += 1;
				}
			
			}
			for (int i = 0; i < 6; i++) {
		
				if (ownerChamp.power [i] >= ability.reqPower [i] && ability.reqPower [i]!=0) {
			

					a += 1;
				}
		
			}

			if (a != totalReq) {
				ready = false;
				//mr.material.color = new Color (0.5f, 0.5f, 0.5f, 1);
			} else {
		
				ready = true;
				//mr.material.color = new Color (1.0f, 1.0f, 1.0f, 1);
			}
		}

		if (ready) {
			mr.material.color = new Color (1.0f, 1.0f, 1.0f, 1);
		} else {
			mr.material.color = new Color (0.5f, 0.5f, 0.5f, 1);
		}

		if (ability.image != "") {

			if (ready || ownerChamp.playerName == PhotonNetwork.player.NickName) {
				text.text = ""+ability.cost;
				if (ability.active) {
				
					Sprite spArt = Resources.Load ("Sprites/Abilities/" + ability.image, typeof(Sprite)) as Sprite;
					mr.material.mainTexture = spArt.texture;
					//Debug.Log ("aact");
				} else {
					Sprite spArt = Resources.Load ("Sprites/Abilities/blankSymbol", typeof(Sprite)) as Sprite;
					mr.material.mainTexture = spArt.texture;
					//Debug.Log ("blank");
				}
			} else {
				text.text = "?";
				Sprite spArt = Resources.Load ("Sprites/Abilities/blankSymbol", typeof(Sprite)) as Sprite;
				mr.material.mainTexture = spArt.texture;
			}


		
		}
		

	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			stream.SendNext (new Serializer().SerializeToString(ability));
			stream.SendNext (ready);
			//stream.SendNext (active);
		}
		else
		{
			//cost = (int)stream.ReceiveNext ();
			ability = new Serializer().DeserializeFromString<ChampAbility>((string)stream.ReceiveNext ());

			ready = (bool)stream.ReceiveNext ();
		}
	}

	void OnMouseDown(){
		if (ownerChamp.playerName == PhotonNetwork.player.NickName && !ownerChamp.busy && turnManager.gameStart) {
			if (turnManager.isMyTurn (ownerChamp.playerName) && ability.hasTarget (ownerChamp)) {
				if (ability.active && ready) {
					if (ownerChamp.mana >= ability.cost) {
						if (ability.type == "play") {
		
							ability.cast (ownerChamp);
							ownerChamp.photonView.RPC ("useAbility", PhotonTargets.MasterClient, num);
							//ownerChamp.mana -= ability.cost;
							//ability.active = false;

						}
					}
				}
			}
		}
	}

	void OnMouseDrag(){
		if (ability.type == "target") {
			if (ownerChamp.playerName == PhotonNetwork.player.NickName && !ownerChamp.busy && turnManager.gameStart) {
				if (turnManager.isMyTurn (ownerChamp.playerName) && ability.hasTarget (ownerChamp)) {
					if (ability.active && ready) {
						if (ownerChamp.mana >= ability.cost) {
							selected = true;
							GameObject.Find ("cursorStatus").GetComponent<cursorStatus> ().status = "target";
						}
					}

			
				}
		
			}
		}
	
	}

	void OnMouseUp(){
		if (selected) {
		
			Vector3 screenPoint = Input.mousePosition;
			screenPoint.z = -10;

			Ray ray = Camera.main.ScreenPointToRay (screenPoint);
			//Debug.DrawRay(
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit, 100)) {
				if (ability.castOnTarget (hit.transform.gameObject)) {
					ownerChamp.mana -= ability.cost;
					ability.active = false;
				
				}
			}

		
		}
		GameObject.Find ("cursorStatus").GetComponent<cursorStatus> ().status = "";
		selected = false;
	}

	void OnMouseEnter(){

		if (ready||ownerChamp.playerName == PhotonNetwork.player.NickName) {
			over = true;
			Debug.Log ("enter");
			StartCoroutine (highLight (0.25f));
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

					GameObject.Find ("infoCard").transform.GetChild (0).gameObject.SetActive (true);
					GameObject.Find ("infoCard").transform.GetChild (0).gameObject.GetComponent<GuiCardData> ().cData = ability.cData;
					//highlightPos = new Vector3 (0.0f, 12.0f, -1.0f);
					//highlightScale = new Vector3 (2.75f,2.75f, 2.75f);

				}
			}
		}
	}

	void OnMouseExit(){
		
			Debug.Log ("exit");
			over = false;
			//GameObject.Find("RenderCard").transform.GetChild(0).gameObject.SetActive (false);
			//GameObject.Find ("RenderCard").GetComponent<cardRender> ().card = null;

			GameObject.Find("infoCard").transform.GetChild(0).gameObject.SetActive (false);

			


		//this.transform.position += new Vector3 (0.0f, 5.0f, 0.0f);
	}

	void render(){

		text.text = ""+ability.cost;
	}
}
