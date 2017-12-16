using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


[Serializable()]
public class sDisDraw : SpellData {

	public override void cast (Champion spellOwner)
	{
		//Debug.Log ("p");

		champTag = spellOwner.tag;


		spellOwner.photonView.RPC ("draw", PhotonTargets.MasterClient);
		GameObject.Find ("SpellManager").GetComponent<SpellManager> ().spell = this;
		spellOwner.busy = true;
		targeting = true;
		spellOwner.sendMessage ("Discard a card");
	}

	public override bool isVaildTarget (GameObject target)
	{
		//Champion ownerChamp = GameObject.FindGameObjectWithTag(champTag).GetComponent<Champion>();
		if(target.tag==champTag+"Card"){

			return true;
		}


		return false;
	}

	public override void castOnTarget (GameObject target)
	{
		Champion ownerChamp = GameObject.FindGameObjectWithTag(champTag).GetComponent<Champion>();
		target.GetComponent<Card> ().photonView.RPC ("Discard", PhotonTargets.MasterClient);
		GameObject.Find ("cursorStatus").GetComponent<cursorStatus> ().status = "";
		targeting = false;
		ownerChamp.photonView.RPC ("setBusy", PhotonTargets.MasterClient,false);
		ownerChamp.photonView.RPC ("AddCardToGrave", PhotonTargets.MasterClient, new Serializer ().SerializeToString (cData));
	}
}
