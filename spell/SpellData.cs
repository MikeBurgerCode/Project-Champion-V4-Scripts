using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


[Serializable()]
public class SpellData {

	public string text,champTag,spellName,id,type;
	public bool targeting,lifeSteal = false;

	public cardData cData = new cardData();
	//public Champion ownerChamp;

	// Use this for initialization

	//public virtual string getText(string txt){ return txt;}
	public virtual void cast(Champion spellOwner){champTag = spellOwner.tag;}
	public virtual void castOnTarget(Champion spellOwner, minionData target){ }
	public virtual void castOnTarget(Champion spellOwner, Champion target){ }
	public virtual void castOnTarget(GameObject target){ }
	public virtual bool hasTarget(Champion ownerChamp){return true;}
	public virtual bool isVaildTarget(GameObject target){return true;}
	public virtual void cardPick( cardData cData){}
	public virtual void placeCard(bool top){}
	public virtual void play(){}
	public virtual string getText(){ return "";}
}
