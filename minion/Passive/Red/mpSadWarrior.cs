using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable()]

public class mpSadWarrior : minionPassive {


	public override void damageTrigger (int damage, minionData mData)
	{

		Champion ownerChamp = GameObject.FindGameObjectWithTag (owner).GetComponent<Champion> ();
		ownerChamp.gManager.minions[minionIndex].BuffMinion (1, 0,false);
		Debug.Log ("buff");
		//minion.
	}

}
