using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable()]
public class Active {
	public bool targeting = false;
	public int cost = 0;
	public string text;
	public virtual void play(minionData mData){
	}

	public virtual void update(GameObject tgo){}

	public virtual void cast(minionData mData){}
	public virtual bool hasTarget(Champion ownerChamp){return true;}
	public virtual bool isVaildTarget(GameObject target){return true;}
	public virtual void castOnTarget(GameObject target){}
	public virtual bool canCast(minionData mData){return true;}
	public virtual void cardPick( cardData cData , minionData mData){}
	public virtual void placeCard(bool top){}
}
