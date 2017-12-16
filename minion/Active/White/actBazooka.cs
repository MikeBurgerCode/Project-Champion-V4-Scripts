using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable()]
public class actBazooka : Active {

	public override void cast (minionData mData)
	{
		//mData.photonView.RPC ("BuffMinion", PhotonTargets.MasterClient, 1, 1, true);
		//mData.photonView.RPC ("minionStat", PhotonTargets.MasterClient, "exhaust", true);



		GameObject.Find ("Awaken/ActiveManager").GetComponent<AaManager> ().actAbility = this;
		GameObject.Find ("Awaken/ActiveManager").GetComponent<AaManager> ().mData = mData;
		GameObject.Find ("Awaken/ActiveManager").GetComponent<AaManager> ().type="active";
		GameObject.Find ("Awaken/ActiveManager").GetComponent<AaManager> ().actAbility.targeting=true;
		mData.ownerChamp.photonView.RPC ("setBusy", PhotonTargets.MasterClient,true);
	}


	public override bool isVaildTarget (GameObject target)
	{
		if(target.tag=="Player1"||target.tag=="Player2"||target.tag=="Player1Minion"||target.tag=="Player2Minion"){

			return true;
		}


		return false;
	}
	public override void castOnTarget (GameObject target)
	{


		minionData mData = GameObject.Find ("Awaken/ActiveManager").GetComponent<AaManager> ().mData;

		if(target.tag=="Player1Minion"||target.tag=="Player2Minion"){

			//target.GetComponent<minionData> ().TakeDamageFromMinion(3, mData.minionIndex);
			target.GetComponent<minionData>().photonView.RPC ("TakeDamageFromMinion", PhotonTargets.MasterClient, 3, mData.minionIndex);
		}

		if(target.tag=="Player1"||target.tag=="Player2"){

			//target.GetComponent<Champion> ().TakeDamageFromMinion (3, mData.minionIndex);
			target.GetComponent<Champion> ().photonView.RPC ("TakeDamageFromMinion", PhotonTargets.MasterClient, 3, mData.minionIndex);

		}

		mData.photonView.RPC ("minionStat", PhotonTargets.MasterClient, "exhaust", true);
		mData.gManager.photonView.RPC ("GameUpdate", PhotonTargets.MasterClient);
		GameObject.Find ("cursorStatus").GetComponent<cursorStatus> ().status = "";
		mData.ownerChamp.photonView.RPC ("setBusy", PhotonTargets.MasterClient,false);
		GameObject.Find ("Awaken/ActiveManager").GetComponent<AaManager> ().clearData ();
	}
}
