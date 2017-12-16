using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable()]
public class sRevenge : SpellData {

	public override void cast (Champion spellOwner)
	{
		//Debug.Log ("p");

		champTag = spellOwner.tag;

		GameObject instance = GameObject.Instantiate(Resources.Load("SpellCardListGui", typeof(GameObject))) as GameObject;
		instance.transform.parent = GameObject.Find ("Canvas").transform;
		instance.transform.localPosition = new Vector3 (0, 0, 0);


		foreach( var card in spellOwner.grave){
			instance.GetComponent<GuiCardList> ().addCard (card);
		}

		GameObject.Find ("SpellManager").GetComponent<SpellManager> ().spell = this;
		spellOwner.photonView.RPC ("setBusy", PhotonTargets.MasterClient,false);
	}

	public override bool hasTarget (Champion ownerChamp)
	{

		if (ownerChamp.grave.Count > 0) {

			return true;
		} else {
	
			return false;

		}


	}

	public override void cardPick (cardData cData)
	{
		cData.attack += 1;
		cData.health += 1;
		string data = new Serializer ().SerializeToString (cData);
		//ownerChamp.drawCardtoHand
		Champion ownerChamp = GameObject.FindGameObjectWithTag(champTag).GetComponent<Champion>();
		ownerChamp.photonView.RPC ("drawCardtoHand", PhotonTargets.MasterClient, data);
		ownerChamp.photonView.RPC ("RemoveCardFromGrave", PhotonTargets.MasterClient,data);
		ownerChamp.photonView.RPC ("setBusy", PhotonTargets.MasterClient,false);
		//ownerChamp.photonView.RPC ("AddCardToGrave", PhotonTargets.MasterClient, new Serializer ().SerializeToString (cData));
	}
}
