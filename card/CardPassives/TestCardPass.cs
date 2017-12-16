using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable()]
public class TestCardPass : CardPassive {



	public override int costBuff ()
	{
		if (cData.owner != "") {
			Champion ownerChamp = GameObject.FindGameObjectWithTag (cData.owner).GetComponent<Champion> ();

			//int buff = -1 * (ownerChamp.hand.Count - 1);

//		Debug.Log (buff);

			return 0;
		}

		return 0;
	}

	public override void playTrigger (cardData cData)
	{
		if (postion == "deck") {

			if (this.cData.owner == cData.owner) {
				if (cData.race.Contains ("human")) {
			
					Champion ownerChamp = GameObject.FindGameObjectWithTag (this.cData.owner).GetComponent<Champion> ();
			
					ownerChamp.summonMinion (new Serializer ().SerializeToString (this.cData),-1);
					int pos = 0;
					for (int i = 0; i < ownerChamp.deck.Count; i++) {
						if (ownerChamp.deck [i] == this.cData) {
							pos = i;
							break;
						}
				
					}
				
					ownerChamp.deck.RemoveAt (pos);
				}
			}
		
		}
	}
}
