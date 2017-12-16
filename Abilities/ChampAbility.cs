using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


[Serializable()]
public class ChampAbility  {

	public string id,type,champTag,image="";
	public int cost=0;
	public int[] reqPower = new int[6]; //r=0,u=1,y=2,p=3,g=4,w=5
	public bool active=true;
	public cardData cData;

	public virtual void cast(Champion champOwner){champTag = champOwner.tag;}
	public virtual bool castOnTarget(GameObject target){ return false; }
	public virtual bool hasTarget(Champion ownerChamp){return true;}
	public virtual void readyUp(){active = true;}


}
