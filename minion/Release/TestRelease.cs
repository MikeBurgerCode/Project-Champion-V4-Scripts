using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


[Serializable()]
public class TestRelease : Release {


	public override void play (minionData minion)
	{
		minion.ownerChamp.draw ();
	}
}
