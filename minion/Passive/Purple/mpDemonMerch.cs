using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable()]

public class mpDemonMerch : minionPassive {

	// Use this for initialization
	public override void discardTrigger (cardData cData)
	{
		Champion ownerChamp = GameObject.FindGameObjectWithTag (owner).GetComponent<Champion> ();
		ownerChamp.draw ();
		//ownerChamp.gManager.minions[minionIndex].BuffMinion (1, 0,false);
		//this.minion.ownerChamp.draw ();
	}
}
