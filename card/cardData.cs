using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


[Serializable()]
public class cardData  {

	public cardData originalData;
	public string id,type,name,text,image,rarity,owner="";

	public CardPassive cp = new CardPassive();

	public List<string> colour = new List<string>();
	public List<string> pool = new List<string>();
	public List<string> race = new List<string>();

	public int cost,attack,health,spellDamage,healing;
	public int oAttack; //original Attack
	public int oHealth; //original Health
	public int oCost; // original cost
	public int maxMana=0;
	public float fontSize=2;

	public bool taunt,charge,block,lifeSteal,silence,awaken,release,twinStrike = false;

	public void SilenceCardData(){



		if (type == "minion") {
			silence = true;

			taunt = false;
			charge = false;
			block = false;
			lifeSteal = false;
			awaken = false;
			maxMana = 0;
			text = "";
		
		}

	}
}
