using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


[Serializable()]
public class abiHeal : ChampAbility {


	public override bool castOnTarget (GameObject target)
	{
		if(target.tag=="Player1"||target.tag=="Player2"){

			target.GetComponent<Champion>().photonView.RPC("Heal",PhotonTargets.MasterClient,2);
			//target.GetComponent<Champion> ().gManager.photonView.RPC ("GameUpdate", PhotonTargets.MasterClient);

			//Debug.Log ("Deal damage to player");
			return true;
		}

		if(target.tag=="Player1Minion"||target.tag=="Player2Minion"){

			//Debug.Log ("Deal" +damage+" to Minion");
			target.GetComponent<minionData>().photonView.RPC("Heal",PhotonTargets.MasterClient,2);
			return true;
		}

		return false;
	}
}
