using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using TMPro;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;



public class minionData : Photon.PunBehaviour, IPunObservable {

	public GameManager gManager;
	BinaryFormatter bf = new BinaryFormatter();

	public Champion ownerChamp;
	public cardData originalData;
	public string owner,id,type,name,text,image,rarity="";
	public List<string> colour = new List<string>();
	public List<string> race = new List<string>();
	public List<string> pool = new List<string>();
	public int attack,health,maxHealth,spellDamage,healing,currentAtk,currentHp,currentSpellDamage,summonIndex,posIndex,minionIndex,cost,armor,maxMana=0;
	public int atkTimes = 1;
	public float fontSize;
	public int oAttack; //original Attack
	public int oHealth; //original Health
	public int oCost; // original cost
	public bool injured;
	public bool selected;

	public bool isDead,release =false;
	public bool attacked = false; // if the minion already attacked
	public bool stun,exhaust=false;
	public bool taunt,charge,block,lifeSteal,silence,twinStrike = false;
	public bool summonSick = true;
	public Awaken awaken;
	public List<Active> aaList = new List<Active>();
	public List<Release> rlList = new List<Release> ();

	public minionPassive mPass = new minionPassive();
	public List<minionPassive> passList = new List<minionPassive> ();
	public List<EquipData> equipment = new List<EquipData>();
	public void Awake(){
		gManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		gManager.minions.Add (this);
		if (PhotonNetwork.isMasterClient) {
			this.minionIndex = GetMinionIndex ();
			if(gManager.playing){

			new LoadData ().makeMinion (this, gManager.playCard);


			this.gameObject.transform.parent = GameObject.Find (owner + "Minions").transform;
			ownerChamp = GameObject.Find (owner).GetComponent<Champion> ();
			ownerChamp.minions.Add (this.gameObject);
			
			gManager.minionQueue.Add (this);
			GetMinionIndex ();

			this.tag = owner + "Minion";
			if (summonIndex != -1) {
				this.transform.SetSiblingIndex (summonIndex);
			}

			if (charge) {
			
				summonSick = false;
			}

			if (twinStrike) {
				
				atkTimes = 2;
			}


			if (awaken.type == "play") {

				awaken.minionIndex=GetMinionIndex ();
					awaken.champTag = ownerChamp.tag;
				//awaken.play (this);
					ownerChamp.busy = true;
					string tData = new Serializer ().SerializeToString (awaken);

					ownerChamp.busy = true;
					if (owner == "Player1") {


						gManager.createAwaken (tData);


					}

					if (owner == "Player2") {
						gManager.photonView.RPC ("createAwaken", PhotonTargets.Others, tData);
					}
			
			}

				if (awaken.type == "target") {

					if (awaken.hasTarget (ownerChamp)) {
						awaken.minionIndex = GetMinionIndex ();

						awaken.play ();
						string tData = new Serializer ().SerializeToString (awaken);

						ownerChamp.busy = true;
						if (owner == "Player1") {
					
							gManager.createTargetAwaken (tData);

						}

						if (owner == "Player2") {
							gManager.photonView.RPC ("createTargetAwaken", PhotonTargets.Others, tData);
						}



					}
				} else {

					SummonTrigger ();
					gManager.playing = false;
				}
		}


			if (gManager.summoning) {
			
				new LoadData ().makeMinion (this, gManager.summonMinion,gManager.summonIndex);


				this.gameObject.transform.parent = GameObject.Find (owner + "Minions").transform;
				ownerChamp = GameObject.Find (owner).GetComponent<Champion> ();
				ownerChamp.minions.Add (this.gameObject);
				//gManager.minions.Add (this);
				gManager.minionQueue.Add (this);
				GetMinionIndex ();
				this.tag = owner + "Minion";
				if (summonIndex != -1) {
					this.transform.SetSiblingIndex (summonIndex);
				}

				if (charge) {

					summonSick = false;
				}

				SummonTrigger ();
				gManager.summoning = false;
			}

		} 


		gManager.GameUpdate ();

	}

	[PunRPC]

	public void SummonTrigger(){
		gManager.SummonTrigger (this);
	
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


		if (owner != "") {
			
			this.tag = owner + "Minion";


			ownerChamp = GameObject.Find (owner).GetComponent<Champion> ();

			if (PhotonNetwork.isMasterClient) {

				if (hasCharge()) {
				
					summonSick = false;

				}
				posIndex = this.transform.GetSiblingIndex ();
				minionIndex = GetMinionIndex ();

				foreach (var equip in equipment) {

					equip.minionIndex = GetMinionIndex ();

				}
				mPass.Update (owner, GetMinionIndex ());
		
			} else {
		
				if (id != "") {
					originalData = new LoadData ().loadMinionCardData (id);
				}

				this.gameObject.transform.parent = GameObject.Find (owner + "Minions").transform;
				this.transform.SetSiblingIndex (posIndex);
			}
		
		}
	}
	public int GetMinionIndex(){

		int index = 0;
/* 		for(int i=0;i<ownerChamp.minions.Count();i++){
			if (ownerChamp.minions [i].gameObject == this.gameObject) {
				return i;
				break;
			}

		}
*/		

		for(int i=0;i<gManager.minions.Count();i++){
			if (gManager.minions [i] == this) {
				return i;
				break;
			}

		}

		return index;
	}
	public int getAtk(){
		int atk;

		atk = currentAtk;


		foreach (var equip in equipment) {
		
			atk += equip.attack;
		
		}

		//if (exhaust) {
		
			//atk
			//atk=(int)Math.Floor((float)atk/2);
	//	}

		return atk;
	}

	public int getHp(){

		int hp;

		hp= currentHp;

		return hp;
	}

	[PunRPC]
	public void addEquip(string data){

		cardData cData = new Serializer ().DeserializeFromString<cardData> (data); 
		EquipData eData = new EquipData ();
		eData.makeEquip (cData);

		armor += eData.armor;

		if (eData.twinStrike) {
			if (!hasTwinStrike() && atkTimes<2) {
				atkTimes += 1;	
		
			}
		}

		equipment.Add (eData);

		gameUpdate ();

	}
	public void gameUpdate(){

		attack = getAtk ();
		health = getHp ();

		if (health < maxHealth) {
		
			injured = true;
		
		} else {
		
			injured = false;
		}
	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting) {
			//stream.SendNext (owner);


			stream.SendNext (owner);

			stream.SendNext (posIndex);
			stream.SendNext (minionIndex);

			stream.SendNext (summonSick);
			stream.SendNext (attacked);

			stream.SendNext (name);
			stream.SendNext (id);
			//stream.SendNext (race);

			stream.SendNext (health);
			stream.SendNext (maxHealth);
			stream.SendNext (oHealth);
			stream.SendNext (currentHp);

			stream.SendNext (armor);

			stream.SendNext (injured);

			stream.SendNext (attack);
			stream.SendNext (oAttack);
			stream.SendNext (currentAtk);

			stream.SendNext (text);

			stream.SendNext (image);


			stream.SendNext (exhaust);
			stream.SendNext (taunt);
			stream.SendNext (charge);
			stream.SendNext (block);
			stream.SendNext (lifeSteal);
			stream.SendNext (twinStrike);

			stream.SendNext (maxMana);
			stream.SendNext (atkTimes);

		//	Stream s = File.Open("data.bin");
			//var clr = bf.Serialize (s, colour);

			stream.SendNext (new Serializer().SerializeToString(colour));
			stream.SendNext (new Serializer().SerializeToString(race));
			stream.SendNext (new Serializer().SerializeToString(pool));
			stream.SendNext (new Serializer().SerializeToString(originalData));

			stream.SendNext (cost);
			stream.SendNext (fontSize);

			stream.SendNext (new Serializer().SerializeToString(aaList));
			stream.SendNext (new Serializer().SerializeToString(equipment));

		} else {
			
			owner = (string)stream.ReceiveNext ();

			posIndex = (int)stream.ReceiveNext ();
			minionIndex = (int)stream.ReceiveNext ();

			summonSick=(bool)stream.ReceiveNext ();
			attacked=(bool)stream.ReceiveNext ();

			name = (string)stream.ReceiveNext ();
			id = (string)stream.ReceiveNext ();
			//race = (List<string>)stream.ReceiveNext ();

			health = (int)stream.ReceiveNext ();
			maxHealth = (int)stream.ReceiveNext ();
			oHealth = (int)stream.ReceiveNext ();
			currentHp = (int)stream.ReceiveNext ();

			armor = (int)stream.ReceiveNext ();

			injured=(bool)stream.ReceiveNext ();

			attack = (int)stream.ReceiveNext ();
			oAttack = (int)stream.ReceiveNext ();
			currentAtk = (int)stream.ReceiveNext ();

			text = (string)stream.ReceiveNext ();

			image = (string)stream.ReceiveNext ();

			exhaust=(bool) stream.ReceiveNext();
			taunt=(bool) stream.ReceiveNext();
			charge=(bool) stream.ReceiveNext();
			block=(bool) stream.ReceiveNext();
			lifeSteal=(bool) stream.ReceiveNext();
			twinStrike=(bool) stream.ReceiveNext();

			maxMana = (int)stream.ReceiveNext ();
			atkTimes = (int)stream.ReceiveNext ();

			colour=new Serializer().DeserializeFromString<List<string>> ((string)stream.ReceiveNext ());
			race=new Serializer().DeserializeFromString<List<string>> ((string)stream.ReceiveNext ());
			pool=new Serializer().DeserializeFromString<List<string>> ((string)stream.ReceiveNext ());
			originalData=new Serializer().DeserializeFromString<cardData> ((string)stream.ReceiveNext ());

			cost=(int) stream.ReceiveNext();
			fontSize=(float) stream.ReceiveNext();

			aaList=new Serializer().DeserializeFromString<List<Active>> ((string)stream.ReceiveNext ());
			equipment=new Serializer().DeserializeFromString<List<EquipData>> ((string)stream.ReceiveNext ());
		}
	}

	public void BattleMinion(minionData enemy){


		int damage = enemy.attack;
		enemy.TakeDamageFromMinion (attack, minionIndex);
		this.TakeDamageFromMinion (damage, enemy.minionIndex);
	
		atkTimes -= 1;
		if (atkTimes < 1) {
			exhaust = true;
		}
		//exhaust = true;
		attacked = true;
		gManager.GameUpdate ();
	}

	public void BattleChampion(Champion enemy){

		enemy.TakeDamageFromMinion (attack, minionIndex);
		this.TakeDamageFromChampion (enemy.attack, enemy.tag);

		atkTimes -= 1;
		if (atkTimes < 1) {
			exhaust = true;
		}
		attacked = true;
		gManager.GameUpdate ();
	}

	[PunRPC]
	public void Heal(int heal){
		currentHp += heal;
		if (currentHp > maxHealth) {
		
			currentHp = maxHealth;
		}
	
		gManager.HealMinionTrigger (heal, this);
		gManager.GameUpdate ();
	}

	[PunRPC]
	public void TakeDamageFromMinion(int damage,int minionIndex){

		minionData enemy = gManager.minions [minionIndex];
		if (damage > 0) {
			if (!block) {
				if (armor > 0) {
					armor -= damage;
					if (armor < 0) {
						int leftOverDmg = Mathf.Abs (armor);
						this.currentHp -= leftOverDmg;
						armor = 0;
						if (enemy.hasLifeSteal()) {
							enemy.ownerChamp.Heal (leftOverDmg);

						}


					}

				} else {
					
					this.currentHp -= damage;
					gManager.DamageTrigger (damage, this);
					if (enemy.hasLifeSteal()) {
						enemy.ownerChamp.Heal (damage);
			
					}
				}

			} else {
				block = false;
		
			}
		}
	}

	[PunRPC]
	public void TakeDamageFromChampion(int damage, string player){
		Champion enemy = GameObject.FindGameObjectWithTag (player).GetComponent<Champion>();
		if (damage > 0) {
			if (!block) {

				if (armor > 0) {
					armor -= damage;
					if (armor < 0) {
						int leftOverDmg = Mathf.Abs (armor);
						this.health -= leftOverDmg;
						armor = 0;
						if (enemy.lifeSteal) {
							enemy.Heal (leftOverDmg);

						}

					}

				} else {
				this.currentHp -= damage;
				gManager.DamageTrigger (damage, this);
					if (enemy.lifeSteal) {
						enemy.Heal (damage);
					}
				}
			} else {
				block = false;

			}
		}
	}

	[PunRPC]
	public void TakeDamageFromSpell(int damage, string spell){

		SpellData sp =new Serializer ().DeserializeFromString<SpellData> (spell);
		Champion spellOwner = GameObject.FindGameObjectWithTag (sp.champTag).GetComponent<Champion>();

		if (damage > 0) {
			if (!block) {
				if (armor > 0) {
					armor -= damage;
					if (armor < 0) {
						int leftOverDmg = Mathf.Abs (armor);
						this.currentHp -= leftOverDmg;
						armor = 0;
						if (spellOwner.lifeSteal||sp.lifeSteal) {
							spellOwner.Heal (leftOverDmg);

						}


					}

				} else {

					this.currentHp -= damage;
					gManager.DamageTrigger (damage, this);
					if (spellOwner.lifeSteal||sp.lifeSteal) {
						spellOwner.Heal (damage);

					}
				}

			} else {
				block = false;

			}
		}
	}
		
	[PunRPC]
	public void BuffMinion(int atk, int hp,bool update){
		currentAtk += atk;
		currentHp += hp;
		maxHealth += hp;

		if (update) {
			gManager.GameUpdate ();
		}
	}

	[PunRPC]
	public void lifeCheck(){
		//Debug.Log ("lifeCheck ");

		if (getHp() < 1) {

			//die ();
			this.photonView.RPC ("die", PhotonTargets.MasterClient);
			//	Debug.Log ("minion Die");


		}

	}
	[PunRPC]
	public void die(){


		gManager.photonView.RPC ("RemoveMinion", PhotonTargets.All, GetMinionIndex ());
		ownerChamp.minions.Remove (this.gameObject);

		ownerChamp.grave.Add( new LoadData().cardToCardData(this));

		for (int i = 0; i < rlList.Count; i++) {

			rlList [i].play (this);

		}

		foreach (var equip in equipment) {
		
		
			ownerChamp.grave.Add(equip.cData);

		}
		PhotonNetwork.Destroy (this.gameObject);
		gManager.GameUpdate ();
	}

	[PunRPC]
	public void minionStat(string stat, bool s){

		if (stat == "exhaust") {
			exhaust = s;
		}



	}

	public bool hasLifeSteal(){
	
		foreach (var equip in equipment) {
		
			if (equip.lifeSteal) {
				return true;
			
			}
		
		}
	
	
		return lifeSteal;
	}

	public bool hasTaunt(){

		foreach (var equip in equipment) {

			if (equip.taunt) {
				return true;

			}

		}

		return taunt;
	}

	public bool hasCharge(){

		foreach (var equip in equipment) {

			if (equip.charge) {
				return true;

			}

		}

		return charge;
	}

	public bool hasTwinStrike(){

		foreach (var equip in equipment) {

			if (equip.twinStrike) {
				return true;

			}

		}

		return twinStrike;
	}

	[PunRPC]
	public void SilenceMinion(){

		silence = true;
		text = "";
		awaken = new Awaken ();
		lifeSteal = false;
		block = false;
		charge = false;
		taunt = false;
		mPass = new minionPassive ();

	}

	[PunRPC]
	public void addArmor(int a){

		armor += a;
	}

	[PunRPC]
	public void DestroyEquip(int index){
		ownerChamp.grave.Add(equipment[index].cData);
		equipment.RemoveAt (index);

		gManager.GameUpdate ();

	}

	public int GetSpellDamage(){
		int sd = 0;

		sd += currentSpellDamage;

		foreach (var equip in equipment) {
		
			sd += equip.spellDamage;
		}

		return sd;
	
	}
}
