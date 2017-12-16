using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


[Serializable()]
public class abiLifeDrain : ChampAbility {

	public override bool castOnTarget (GameObject target)
	{

		SpellData spell= new SpellData();
		spell.lifeSteal = true;
		spell.champTag = champTag;

		string sp = new Serializer ().SerializeToString (spell);

		if(target.tag=="Player1"||target.tag=="Player2"){
			

			target.GetComponent<Champion>().photonView.RPC("TakeDamageFromSpell",PhotonTargets.MasterClient,1,sp);
			target.GetComponent<Champion> ().gManager.photonView.RPC ("GameUpdate", PhotonTargets.MasterClient);
			//target.GetComponent<Champion> ().gManager.photonView.RPC ("GameUpdate", PhotonTargets.MasterClient);

			//Debug.Log ("Deal damage to player");
			return true;
		}

		if(target.tag=="Player1Minion"||target.tag=="Player2Minion"){

			//Debug.Log ("Deal" +damage+" to Minion");
			target.GetComponent<minionData>().photonView.RPC("TakeDamageFromSpell",PhotonTargets.MasterClient,1,sp);
			target.GetComponent<minionData> ().gManager.photonView.RPC ("GameUpdate", PhotonTargets.MasterClient);
			return true;
		}

		return false;
	}
}
