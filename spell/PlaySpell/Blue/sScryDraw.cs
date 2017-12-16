using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable()]
public class sScryDraw : SpellData {

	public override void cast (Champion spellOwner)
	{
		//Debug.Log ("p");

		champTag = spellOwner.tag;


		GameObject.Find ("Canvas").transform.Find("ScryCard").gameObject.GetComponent<GuiCardData>().cData=spellOwner.deck[0];
		GameObject.Find ("Canvas").transform.Find ("ScryCard").gameObject.GetComponent<GuiCardData> ().spell = true;
		GameObject.Find ("Canvas").transform.Find ("ScryCard").gameObject.SetActive (true);

		GameObject.Find ("SpellManager").GetComponent<SpellManager> ().spell = this;
		spellOwner.photonView.RPC ("setBusy", PhotonTargets.MasterClient,false);
	}

	public override void placeCard (bool top)
	{
		Champion ownerChamp = GameObject.FindGameObjectWithTag(champTag).GetComponent<Champion>();
		Debug.Log ("draw");
		ownerChamp.photonView.RPC ("draw", PhotonTargets.MasterClient);
		ownerChamp.photonView.RPC ("setBusy", PhotonTargets.MasterClient,false);
		ownerChamp.photonView.RPC ("AddCardToGrave", PhotonTargets.MasterClient, new Serializer ().SerializeToString (cData));

	}
}
