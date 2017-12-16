using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


[Serializable()]
public class TestSpell : SpellData {



	int damage=3;
	int mindionIndex=-1;

	public override bool hasTarget (Champion ownerChamp)
	{

		//if(ownerChamp.gManager.minions

		return true;
	}

	public override void cardPick (cardData cData)
	{
		Champion ownerChamp = GameObject.FindGameObjectWithTag(champTag).GetComponent<Champion>();
		//Debug.Log (cData.name);
		minionData targetMinion=GameObject.Find ("SpellManager").GetComponent<SpellManager> ().targetMinion;

		int i = -1;
		foreach (var equip in targetMinion.equipment) {
			i += 1;
			if (equip.cData == cData) {
			

				break;
			}
		
		}
		targetMinion.photonView.RPC ("DestroyEquip", PhotonTargets.MasterClient, i);
		ownerChamp.photonView.RPC ("setBusy", PhotonTargets.MasterClient,false);
	}

	public override void placeCard (bool top)
	{
		


	}

	public override bool isVaildTarget (GameObject target)
	{
		//Champion ownerChamp = GameObject.FindGameObjectWithTag(champTag).GetComponent<Champion>();
		if(target.tag=="Player1Minion"||target.tag=="Player2Minion"){

			return true;
		}


		return false;
	}

	public override void castOnTarget (GameObject target)
	{
		//champTag = spellOwner.tag;
		Champion ownerChamp = GameObject.FindGameObjectWithTag(champTag).GetComponent<Champion>();
		string sp = new Serializer ().SerializeToString (this);


		if(target.tag=="Player1Minion"||target.tag=="Player2Minion"){

			minionData mData = target.GetComponent<minionData> ();

			GameObject instance = GameObject.Instantiate(Resources.Load("SpellCardListGui", typeof(GameObject))) as GameObject;
			instance.transform.parent = GameObject.Find ("Canvas").transform;
			instance.transform.localPosition = new Vector3 (0, 0, 0);


			foreach( var equip in mData.equipment){
				instance.GetComponent<GuiCardList> ().addCard (equip.cData);
			}

			GameObject.Find ("SpellManager").GetComponent<SpellManager> ().spell = this;
			GameObject.Find ("SpellManager").GetComponent<SpellManager> ().targetMinion = mData;
			ownerChamp.photonView.RPC ("setBusy", PhotonTargets.MasterClient,true);
		}


	}



}
