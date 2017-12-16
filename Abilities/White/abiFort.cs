using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable()]
public class abiFort : ChampAbility {

	public override void cast (Champion champOwner)
	{
		
		champOwner.photonView.RPC ("addArmor", PhotonTargets.MasterClient,2);
	}
}
