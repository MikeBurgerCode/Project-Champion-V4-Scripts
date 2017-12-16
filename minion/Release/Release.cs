using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


[Serializable()]
public class Release {

	public string releaseName,releaseOwner;
	public virtual void play(minionData minion){
		Debug.Log ("I die");
	}

}
