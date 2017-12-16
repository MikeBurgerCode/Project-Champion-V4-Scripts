using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable()]
public class actElfMerch : Active {

	public override void cast (minionData mData)
	{
		//mData.photonView.RPC ("BuffMinion", PhotonTargets.MasterClient, 1, 1, true);
		//mData.photonView.RPC ("minionStat", PhotonTargets.MasterClient, "exhaust", true);

		GameObject instance = GameObject.Instantiate(Resources.Load("SummonCardListGui", typeof(GameObject))) as GameObject;
		instance.transform.parent = GameObject.Find ("Canvas").transform;
		instance.transform.localPosition = new Vector3 (0, 0, 0);

		//instance.GetComponent<GuiCardList> ().addCard (new LoadData().loadSpellCardData("spTest"));

		cardData c =new LoadData().loadSpellCardData("sN0");
		instance.GetComponent<GuiCardList> ().addCard (c);

		c =new LoadData().loadSpellCardData("sN1");

		instance.GetComponent<GuiCardList> ().addCard (c);

		c =new LoadData().loadEquipCardData("eN0");

		instance.GetComponent<GuiCardList> ().addCard (c);

		GameObject.Find ("Awaken/ActiveManager").GetComponent<AaManager> ().actAbility = this;
		GameObject.Find ("Awaken/ActiveManager").GetComponent<AaManager> ().mData = mData;
		GameObject.Find ("Awaken/ActiveManager").GetComponent<AaManager> ().type="active";
		mData.ownerChamp.photonView.RPC ("setBusy", PhotonTargets.MasterClient,true);
	}

	public override void cardPick (cardData cData, minionData mData)
	{
		mData.ownerChamp.photonView.RPC ("addCardtoHand", PhotonTargets.MasterClient, new Serializer ().SerializeToString (cData));
		mData.ownerChamp.photonView.RPC ("setBusy", PhotonTargets.MasterClient,false);
		mData.photonView.RPC ("minionStat", PhotonTargets.MasterClient, "exhaust", true);
	}
}
