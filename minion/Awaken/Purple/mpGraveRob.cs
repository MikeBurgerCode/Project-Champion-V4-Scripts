using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


[Serializable()]
public class mpGraveRob : Awaken {

	public override void play ()
	{

		Champion ownerChamp = GameObject.FindGameObjectWithTag (champTag).GetComponent<Champion> ();

		if (ownerChamp.opponent.grave.Count > 0) {
			GameObject instance = GameObject.Instantiate (Resources.Load ("SummonCardListGui", typeof(GameObject))) as GameObject;
			instance.transform.parent = GameObject.Find ("Canvas").transform;
			instance.transform.localPosition = new Vector3 (0, 0, 0);

			//cardData c;

			foreach (var card in ownerChamp.opponent.grave) {
		
				instance.GetComponent<GuiCardList> ().addCard (card);
			}

			minionData mData = ownerChamp.gManager.minions [this.minionIndex];

			GameObject.Find ("Awaken/ActiveManager").GetComponent<AaManager> ().awaken = this;
			GameObject.Find ("Awaken/ActiveManager").GetComponent<AaManager> ().mData = mData;
			GameObject.Find ("Awaken/ActiveManager").GetComponent<AaManager> ().type = "awaken";
			ownerChamp.photonView.RPC ("setBusy", PhotonTargets.MasterClient, true);
		} else {
			ownerChamp.photonView.RPC ("setBusy", PhotonTargets.MasterClient,false);
		}

	}

	public override void cardPick (cardData cData, minionData mData)
	{
		Debug.Log (mData.ownerChamp.tag);

		string data = new Serializer ().SerializeToString (cData);

		mData.ownerChamp.photonView.RPC ("AddCardToGrave", PhotonTargets.MasterClient,data);
		mData.ownerChamp.opponent.photonView.RPC ("RemoveCardFromGrave", PhotonTargets.MasterClient,data);

		mData.ownerChamp.photonView.RPC ("setBusy", PhotonTargets.MasterClient,false);
	}
}
