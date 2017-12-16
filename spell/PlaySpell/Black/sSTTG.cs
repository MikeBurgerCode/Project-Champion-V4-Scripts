using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable()]
public class sSTTG : SpellData {

	public override void cast (Champion spellOwner)
	{
		//spellOwner.photonView.RPC ("DiscardACard", PhotonTargets.MasterClient);
		//spellOwner.opponent.photonView.RPC ("DiscardACard", PhotonTargets.MasterClient);
	}
}
