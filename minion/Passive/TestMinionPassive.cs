using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable()]
public class TestMinionPassive : minionPassive {

	public override void playTrigger (cardData cData, string ownerTag)
	{
		//if (ownerTag == minion.owner && cData.type=="spell") {
		
		//	minion.BuffMinion (1, 1,true);
		
		//}
	}

	public override int costBuff (Card card)
	{
		if (card.cData.type=="minion"&&card.cData.awaken) {
		
			return 1;

		}

		return 0;
	}

}
