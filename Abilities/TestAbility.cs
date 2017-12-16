using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


[Serializable()]

public class TestAbility : ChampAbility {


	public override void cast (Champion champOwner)
	{
		champOwner.photonView.RPC ("draw", PhotonTargets.MasterClient);
	}

	public override bool castOnTarget (GameObject target)
	{
		Champion ownerChamp = GameObject.FindGameObjectWithTag (champTag).GetComponent<Champion> ();

		Debug.Log (target.tag);
		if (target.tag == ownerChamp.tag+"Minion") {
		
		
			minionData tMinion = target.GetComponent<minionData> ();

			tMinion.photonView.RPC ("BuffMinion", PhotonTargets.MasterClient, 1, 1,true);

			return true;
		}

		return false;
	}
}
