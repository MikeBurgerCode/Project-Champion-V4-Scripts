using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


[Serializable()]
public class FireBolt : SpellData {

	int damage=3;

	public override bool isVaildTarget (GameObject target)
	{
		//Champion ownerChamp = GameObject.FindGameObjectWithTag(champTag).GetComponent<Champion>();
		if(target.tag=="Player1"||target.tag=="Player2"||target.tag=="Player1Minion"||target.tag=="Player2Minion"){

			return true;
		}


		return false;
	}

	public override void castOnTarget (GameObject target)
	{
		//champTag = spellOwner.tag;
		Champion ownerChamp = GameObject.FindGameObjectWithTag(champTag).GetComponent<Champion>();
		string sp = new Serializer ().SerializeToString (this);

		if(target.tag=="Player1"||target.tag=="Player2"){

			target.GetComponent<Champion>().photonView.RPC("TakeDamageFromSpell",PhotonTargets.MasterClient,damage,sp);
			target.GetComponent<Champion> ().gManager.photonView.RPC ("GameUpdate", PhotonTargets.MasterClient);

			//Debug.Log ("Deal damage to player");
		}

		if(target.tag=="Player1Minion"||target.tag=="Player2Minion"){

			//Debug.Log ("Deal" +damage+" to Minion");
			target.GetComponent<minionData>().photonView.RPC("TakeDamageFromSpell",PhotonTargets.MasterClient,damage+ownerChamp.spellDamage,sp);
			target.GetComponent<minionData> ().gManager.photonView.RPC ("GameUpdate", PhotonTargets.MasterClient);
		}

		ownerChamp.photonView.RPC ("AddCardToGrave", PhotonTargets.MasterClient, new Serializer ().SerializeToString (cData));
	}

	public override string getText ()
	{
		
		string txt = "";
		if (champTag != "") {
			Champion ownerChamp = GameObject.FindGameObjectWithTag (champTag).GetComponent<Champion> ();
			if (ownerChamp.spellDamage <1) {
		
				txt = "Deal " + damage + " to target";
			} else {
		
				int newDamage = damage + ownerChamp.spellDamage;
				txt = "Deal <b>*" +newDamage  + "*</b> to target";
			}

		}
		return txt;

	}
}
