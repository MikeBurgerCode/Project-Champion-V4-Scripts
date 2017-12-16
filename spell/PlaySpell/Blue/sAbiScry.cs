using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable()]
public class sAbiScry : SpellData {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public override void placeCard (bool top)
	{
		Champion ownerChamp = GameObject.FindGameObjectWithTag(champTag).GetComponent<Champion>();
		//Debug.Log ("draw");

		ownerChamp.photonView.RPC ("Scry", PhotonTargets.MasterClient,top);

		ownerChamp.photonView.RPC ("setBusy", PhotonTargets.MasterClient,false);


	}
}
