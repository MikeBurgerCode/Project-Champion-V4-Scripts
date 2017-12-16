using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


[Serializable()]
public class Awaken {

	public string type,champTag= "";
	public int minionIndex=-1;

	public virtual void play(){
	}

	public virtual void update(GameObject tgo){}

	public virtual bool hasTarget(Champion ownerChamp){return true;}
	public virtual bool isVaildTarget(GameObject target){return true;}
	public virtual void castOnTarget(GameObject target, GameObject tgo){}
	public virtual void cardPick( cardData cData, minionData mData){}
	public virtual void cardPick( cardData cData){}
	public virtual void placeCard(bool top){}
}
