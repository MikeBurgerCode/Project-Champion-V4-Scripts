using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


[Serializable()]
public class TestAwaken : Awaken {

	public override void play ()
	{
		
	}

	public override bool hasTarget (Champion ownerChamp)
	{
		champTag = ownerChamp.tag;
		//Debug.Log ("ownerChamp.minions.Count");
		if (ownerChamp.minions.Count > 1) {

			return true;
		
		}

		return false;
	}


	public override bool isVaildTarget (GameObject target)
	{

		if (target.tag == champTag+"Minion") {
		
			return true;
		}

		return false;
	}

	public override void castOnTarget (GameObject target, GameObject tgo)
	{

		Champion ownerChamp = GameObject.FindGameObjectWithTag (champTag).GetComponent<Champion> ();
		target.GetComponent<minionData> ().photonView.RPC ("BuffMinion",PhotonTargets.MasterClient,1,1,true);
		ownerChamp.photonView.RPC ("setBusy", PhotonTargets.MasterClient,false);
		target.GetComponent<minionData> ().photonView.RPC ("SummonTrigger",PhotonTargets.MasterClient);
		GameObject.Destroy (tgo);
	}

	public override void update (GameObject tgo)
	{
		GameObject.Find ("cursorStatus").GetComponent<cursorStatus> ().status = "target";
		if(Input.GetMouseButtonDown(0)){
			Vector3 screenPoint = Input.mousePosition;
			screenPoint.z = -10;

			Ray ray = Camera.main.ScreenPointToRay(screenPoint);

			RaycastHit hit;

			if (Physics.Raycast (ray, out hit, 100)) {

				if (isVaildTarget (hit.transform.gameObject)) {

					castOnTarget (hit.transform.gameObject,tgo);
				
				}


			}
	}
}
}
