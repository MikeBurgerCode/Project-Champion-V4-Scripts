using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


[Serializable()]
public class abiSummonPlant : ChampAbility {

	public override void cast (Champion champOwner)
	{

		cardData token;

		int num = UnityEngine.Random.Range (0, 3);

		token = new LoadData ().loadMinionCardData ("mtG"+num);
		token.owner = champOwner.tag;
		token.originalData = new LoadData ().loadMinionCardData ("mtG"+num);




		string data = new Serializer ().SerializeToString (token);
		champOwner.photonView.RPC ("summonMinion", PhotonTargets.MasterClient,data,-1);
	}
}
