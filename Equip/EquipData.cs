using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


[Serializable()]

public class EquipData {

	// Use this for initialization

	public cardData cData;
	public int attack,armor,spellDamage,healing,minionIndex=0;
	public Awaken awaken;
	public Release release;
	public bool active = false;
	public bool taunt,charge,block,lifeSteal,silence,twinStrike = false;
	public List<Active> aaList = new List<Active> ();

	public void makeEquip(cardData data){

		cData = data;

		new LoadData ().makeEquipment (cData, this);

		attack = cData.attack;
		armor = cData.health;

		if (!cData.silence) {
		
		
		}
	
	}

	public void destroyEquip(minionData mdata){
		
	}

	public void destroyEquip(Champion champ){

	}

}
