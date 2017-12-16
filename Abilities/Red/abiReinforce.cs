using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


[Serializable()]
public class abiReinforce : ChampAbility {


	public override void cast (Champion champOwner)
	{
		cardData token = new LoadData ().loadMinionCardData ("mtR0");
		token.owner = champOwner.tag;
		token.originalData = new LoadData ().loadMinionCardData ("mtR0");
		string data = new Serializer ().SerializeToString (token);
		champOwner.photonView.RPC ("summonMinion", PhotonTargets.MasterClient,data,-1);
	}
}
