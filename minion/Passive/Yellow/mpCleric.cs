using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable()]

public class mpCleric : minionPassive {

	public override void healTrigger (int heal, minionData mData)
	{
		//if (mData.owner == owner) {
		if (mData.injured) {
			Champion ownerChamp = GameObject.FindGameObjectWithTag (owner).GetComponent<Champion> ();
			ownerChamp.draw ();
		}
		
		//}
	}
}
