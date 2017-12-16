using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


[Serializable()]
public class abiScry : ChampAbility {

	public override void cast (Champion champOwner)
	{

		SpellData spell = new SpellFactory ().getSpell ("abiScry");
		spell.champTag = champOwner.tag;
		GameObject.Find ("Canvas").transform.Find("ScryCard").gameObject.GetComponent<GuiCardData>().cData=champOwner.deck[0];
		GameObject.Find ("Canvas").transform.Find ("ScryCard").gameObject.GetComponent<GuiCardData> ().spell = true;
		GameObject.Find ("Canvas").transform.Find ("ScryCard").gameObject.SetActive (true);

		GameObject.Find ("SpellManager").GetComponent<SpellManager> ().spell = spell;
		champOwner.photonView.RPC ("setBusy", PhotonTargets.MasterClient,true);
	}
}
