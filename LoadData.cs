using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using UnityEngine;

public class LoadData {

	// Use this for initialization

	public cardData loadMinionCardData(string id){
		cardData cData = new cardData ();

		TextAsset path = Resources.Load ("xml/CardDataBase", typeof(TextAsset)) as TextAsset;
		//			Debug.Log (path);

		//Debug.Log ("not null");
		XElement root = XElement.Parse (path.text);
		IEnumerable<XElement> cd =
			from el in root.Elements ("cards").Elements ("card")
				where el.Attribute ("id").Value == id
			select el;

		XElement cardData = cd.First ();

		cData.id = cardData.Attribute ("id").Value;
		cData.cost = int.Parse (cardData.Element ("cost").Value);
		//cData.oCost = int.Parse (cardData.Element ("cost").Value);
		cData.attack = int.Parse (cardData.Element ("attack").Value);
		//cData.oAttack = int.Parse (cardData.Element ("attack").Value);
		cData.health = int.Parse (cardData.Element ("health").Value);
		//cData.oHealth = int.Parse (cardData.Element ("health").Value);
		//cData.colour = cardData.Element ("colour").Value;

		string colours = cardData.Element ("colour").Value;

		List<string> colourList = colours.Split (' ').ToList<string> ();

		cData.colour = colourList;

		string costPool = cardData.Element ("costPool").Value;
		if (cardData.Element ("costPool").Value != "") {

			List<string> costPoolList = costPool.Split (' ').ToList<string> ();

			cData.pool = costPoolList;
		}
		string race = cardData.Element ("race").Value;

		List<string> raceList = race.Split (' ').ToList<string> ();

		cData.race = raceList;

		cData.name = cardData.Element ("name").Value;
		cData.image = cardData.Element ("image").Value;
		cData.text = cardData.Element ("text").Value;
		//cData.race = cardData.Element ("race").Value;
		cData.rarity=cardData.Element ("rarity").Value;
		cData.type = cardData.Element ("type").Value;

		cData.cp= new CardPassFactory().getCardPassive(cardData.Element ("passive").Value);

		cData.fontSize = float.Parse (cardData.Element ("fontSize").Value);
		path = Resources.Load ("xml/minionData", typeof(TextAsset)) as TextAsset;
		//Debug.Log (path);
		//Debug.Log ("not null");
		root = XElement.Parse (path.text);
		cd =
			from el in root.Elements ("minions").Elements ("minion")
				where el.Attribute ("id").Value == id
			select el;


		if (cd.FirstOrDefault () != null) {
			XElement minionAbilites = cd.First ();

			if (minionAbilites.Element ("skills").Value != "") {
				string abi = minionAbilites.Element ("skills").Value;

				List<string>skills = abi.Split (' ').ToList<string> ();

				foreach (var skill in skills) {

					if (skill == "mm") {

						cData.maxMana += 1;

					}

					if (skill == "taunt") {

						cData.taunt = true;

					}

					if (skill == "charge") {

						cData.charge = true;

					}

					if (skill == "lifeSteal") {

						cData.lifeSteal = true;

					}

					if (skill == "block") {

						cData.block = true;

					}

					if (skill == "twinStrike") {

						cData.twinStrike = true;

					}
				}
			}


			if (minionAbilites.Element ("awaken").Value != "") {
				cData.awaken = true;
			}

			if (minionAbilites.Element ("release").Value != "") {
				cData.release = true;
			}
				
		}

		//cData.originalData=cda
		return cData;

	}

	public void makeMinionCard(cardData cData, Card card){

		//card.powerCost

		card.cData = cData;
		card.cData.cp.postion="hand";

	}

	public void makeMinion(minionData mData,Card card){

		mData.owner = card.owner;
		mData.originalData = card.cData.originalData;
		mData.id = card.cData.id;
		mData.name = card.cData.name;
		mData.colour = card.cData.colour;
		mData.currentAtk = card.attack;
		mData.currentHp = card.health;
		mData.maxHealth = card.health;
		mData.race = card.cData.race;
		mData.image = card.cData.image;
		mData.text = card.cData.text;

		mData.silence = card.cData.silence;

		TextAsset path = Resources.Load ("xml/minionData", typeof(TextAsset)) as TextAsset;
		//			Debug.Log (path);

		//Debug.Log ("not null");
		XElement root = XElement.Parse (path.text);
		IEnumerable<XElement> cd =
			from el in root.Elements ("minions").Elements ("minion")
				where el.Attribute ("id").Value == mData.id
			select el;


		if (cd.FirstOrDefault () != null) {
			XElement minionData = cd.First ();

			if (!mData.silence) {
				if (card.cData.awaken) {
			


					mData.awaken = new AwakenFactory ().getAwaken (minionData.Element ("awaken").Value);

				}

				if (card.cData.release) {
					mData.release = true;
					mData.rlList.Add (new ReleaseFactory ().getRelease (minionData.Element ("release").Value));
			
				}

				mData.mPass = new MinPassFactory ().getPassive (minionData.Element ("passive").Value);

				if (minionData.Element ("active").Value != "") {

					string actives = minionData.Element ("active").Value;
					List<string> activeList = actives.Split (' ').ToList<string> ();


					foreach (var act in activeList) {

						mData.aaList.Add (new ActiveFactory ().GetActiveAbility (act));

					}
					//cData.pool = costPoolList;
				}
			}
		}
		mData.taunt = card.cData.taunt;
		mData.charge = card.cData.charge;
		mData.lifeSteal = card.cData.lifeSteal;
		mData.block = card.cData.block;
		mData.twinStrike = card.cData.twinStrike;
		mData.maxMana = card.cData.maxMana;

		mData.summonIndex = card.summonIndex;

		mData.pool = card.cData.pool;
		mData.fontSize = card.cData.fontSize;
		mData.rarity = card.cData.rarity;
		mData.cost = card.cData.cost;
	
	}

	public void makeMinion(minionData mData,cardData cData,int summonIndex){

		mData.owner = cData.owner;
		mData.originalData = cData.originalData;
		mData.id = cData.id;
		mData.name = cData.name;
		mData.colour = cData.colour;
		mData.currentAtk = cData.attack;
		mData.currentHp = cData.health;
		mData.maxHealth = cData.health;
		mData.race = cData.race;
		mData.image = cData.image;
		mData.text = cData.text;

		mData.silence = cData.silence;

		TextAsset path = Resources.Load ("xml/minionData", typeof(TextAsset)) as TextAsset;
		//			Debug.Log (path);

		//Debug.Log ("not null");
		XElement root = XElement.Parse (path.text);
		IEnumerable<XElement> cd =
			from el in root.Elements ("minions").Elements ("minion")
				where el.Attribute ("id").Value == mData.id
			select el;


		if (cd.FirstOrDefault () != null) {
			XElement minionData = cd.First ();
			//Debug.Log("active: "+(minionData.Element ("active").Value));

			if (!mData.silence) {
				if (cData.awaken) {



					mData.awaken = new AwakenFactory ().getAwaken (minionData.Element ("awaken").Value);

				}

				if (cData.release) {

					mData.rlList.Add (new ReleaseFactory ().getRelease (minionData.Element ("release").Value));

				}

				mData.mPass = new MinPassFactory ().getPassive (minionData.Element ("passive").Value);


				if (minionData.Element ("active").Value != "") {

					string actives = minionData.Element ("active").Value;
					List<string> activeList = actives.Split (' ').ToList<string> ();


					foreach (var act in activeList) {
					
						mData.aaList.Add (new ActiveFactory ().GetActiveAbility (act));

					}
					//cData.pool = costPoolList;
				}
			}
		}
		mData.taunt = cData.taunt;
		mData.charge = cData.charge;
		mData.lifeSteal = cData.lifeSteal;
		mData.block = cData.block;

		mData.summonIndex = summonIndex;

		mData.pool = cData.pool;
		mData.fontSize = cData.fontSize;
		mData.rarity =cData.rarity;
		mData.cost = cData.cost;
		mData.maxMana = cData.maxMana;

	}

	public cardData loadManaCardData(string id){
	
		cardData cData = new cardData ();

		TextAsset path = Resources.Load ("xml/CardDataBase", typeof(TextAsset)) as TextAsset;
		//			Debug.Log (path);

		//Debug.Log ("not null");
		XElement root = XElement.Parse (path.text);
		IEnumerable<XElement> cd =
			from el in root.Elements ("cards").Elements ("card")
				where el.Attribute ("id").Value == id
			select el;

		XElement cardData = cd.First ();

		cData.id = cardData.Attribute ("id").Value;


		string colours = cardData.Element ("colour").Value;

		List<string> colourList = colours.Split (' ').ToList<string> ();

		cData.colour = colourList;

		cData.name = cardData.Element ("name").Value;
		cData.image = cardData.Element ("image").Value;
		cData.text = cardData.Element ("text").Value;
		//cData.race = cardData.Element ("race").Value;
		cData.rarity=cardData.Element ("rarity").Value;
		cData.type = cardData.Element ("type").Value;

		return cData;
	
	}
	public void makeManaCard(cardData cData, Card card){




	}

	public cardData loadSpellCardData(string id){

		cardData cData = new cardData ();

		TextAsset path = Resources.Load ("xml/CardDataBase", typeof(TextAsset)) as TextAsset;
		//			Debug.Log (path);

		//Debug.Log ("not null");
		XElement root = XElement.Parse (path.text);
		IEnumerable<XElement> cd =
			from el in root.Elements ("cards").Elements ("card")
				where el.Attribute ("id").Value == id
			select el;

		XElement cardData = cd.First ();

		cData.id = cardData.Attribute ("id").Value;
		cData.cost = int.Parse (cardData.Element ("cost").Value);
		cData.oCost = int.Parse (cardData.Element ("cost").Value);

		string colours = cardData.Element ("colour").Value;

		List<string> colourList = colours.Split (' ').ToList<string> ();

		cData.colour = colourList;

		string costPool = cardData.Element ("costPool").Value;
		if (cardData.Element ("costPool").Value != "") {

			List<string> costPoolList = costPool.Split (' ').ToList<string> ();

			cData.pool = costPoolList;
		}

		cData.name = cardData.Element ("name").Value;
		cData.image = cardData.Element ("image").Value;
		cData.text = cardData.Element ("text").Value;
		//cData.race = cardData.Element ("race").Value;
		cData.rarity=cardData.Element ("rarity").Value;
		cData.type = cardData.Element ("type").Value;

		return cData;
	}

	public void makeSpellCard(cardData cData, Card card){

		card.spell = new SpellFactory ().getSpell (cData.id);

		card.cData = cData;
		card.cData.cp.postion="hand";
	}



	public cardData cardToCardData(minionData mData){
		cardData cData = new cardData ();

		cData.type = "minion";
		cData.id = mData.id;

		cData.pool = mData.pool;
		cData.cost = mData.cost;
		cData.colour = mData.colour;

		cData.name = mData.name;
		cData.image = mData.image;
		cData.text = mData.text;

		cData.rarity = mData.rarity;
		cData.fontSize = mData.fontSize;

		cData.attack = mData.currentAtk;
		cData.health = mData.maxHealth;

		cData.race = mData.race;

		cData.silence = mData.silence;

		cData.taunt = mData.taunt;
		cData.charge = mData.charge;
		cData.lifeSteal = mData.lifeSteal;

		if (mData.awaken.type != "") {
		
			cData.awaken = true;

		}

		cData.release = mData.release;

		return cData;
	}

	public ChampAbility loadAbility(string id){
		ChampAbility ability = new ChampAbility ();


		ability = new AbiFactory ().getChampAbility (id);


		TextAsset path = Resources.Load ("xml/AbilityData", typeof(TextAsset)) as TextAsset;
		//			Debug.Log (path);

		//Debug.Log ("not null");
		XElement root = XElement.Parse (path.text);
		IEnumerable<XElement> cd =
			from el in root.Elements ("abilites").Elements ("ability")
				where el.Attribute ("id").Value == id 
			select el;

		XElement abiData = cd.First ();

		ability.cost=int.Parse (abiData.Element ("cost").Value);
		ability.type=abiData.Element ("type").Value;
		ability.image=abiData.Element ("image").Value;

		ability.cData = loadAbiCardData (id);
		//ability.reqPower

		string reqPool = abiData.Element ("req").Value;
		if (abiData.Element ("req").Value != "") {

			List<string> reqPoolList = reqPool.Split (' ').ToList<string> ();

			foreach (var power in reqPoolList) {
				if (power == "r") {
					ability.reqPower[0] += 1;
				}

				if (power == "u") {
					ability.reqPower[1] += 1;
				}

				if (power == "y") {
					ability.reqPower[2] += 1;
				}

				if (power == "p") {
					ability.reqPower[3] += 1;
				}
				if (power == "g") {
					ability.reqPower[4] += 1;
				}
				if (power == "w") {
					ability.reqPower[5] += 1;
				}
			
			}
		}

		return ability;
	}

	public cardData loadAbiCardData(string id){

		cardData cData = new cardData ();

		TextAsset path = Resources.Load ("xml/AbilityData", typeof(TextAsset)) as TextAsset;
		//			Debug.Log (path);

		//Debug.Log ("not null");
		XElement root = XElement.Parse (path.text);
		IEnumerable<XElement> cd =
			from el in root.Elements ("abilites").Elements ("ability")
				where el.Attribute ("id").Value == id
			select el;

		XElement cardData = cd.First ();

		cData.id = cardData.Attribute ("id").Value;
		cData.cost = int.Parse (cardData.Element ("cost").Value);
		//cData.oCost = int.Parse (cardData.Element ("cost").Value);


		string costPool = cardData.Element ("req").Value;
		if (cardData.Element ("req").Value != "") {

			List<string> costPoolList = costPool.Split (' ').ToList<string> ();

			cData.pool = costPoolList;
		}

		cData.name = cardData.Element ("name").Value;
		cData.image = cardData.Element ("image").Value;
		cData.text = cardData.Element ("text").Value;
		//cData.race = cardData.Element ("race").Value;
		//cData.rarity=cardData.Element ("rarity").Value;
		cData.type = "ability";

		return cData;
	}

	public string activeText(string id){


		TextAsset path = Resources.Load ("xml/ActiveAbiData", typeof(TextAsset)) as TextAsset;
		//			Debug.Log (path);

		//Debug.Log ("not null");
		XElement root = XElement.Parse (path.text);
		IEnumerable<XElement> cd =
			from el in root.Elements ("Actives").Elements ("Active")
				where el.Attribute ("id").Value == id
			select el;

		XElement abiData = cd.First ();

		return abiData.Element ("text").Value;
	}

	public int activeCost(string id){


		TextAsset path = Resources.Load ("xml/ActiveAbiData", typeof(TextAsset)) as TextAsset;
		//			Debug.Log (path);

		//Debug.Log ("not null");
		XElement root = XElement.Parse (path.text);
		IEnumerable<XElement> cd =
			from el in root.Elements ("Actives").Elements ("Active")
				where el.Attribute ("id").Value == id
			select el;

		XElement abiData = cd.First ();

		return int.Parse (abiData.Element ("cost").Value);
	}

	public cardData loadEquipCardData(string id){
		cardData cData = new cardData ();

		TextAsset path = Resources.Load ("xml/CardDataBase", typeof(TextAsset)) as TextAsset;
		//			Debug.Log (path);

		//Debug.Log ("not null");
		XElement root = XElement.Parse (path.text);
		IEnumerable<XElement> cd =
			from el in root.Elements ("cards").Elements ("card")
				where el.Attribute ("id").Value == id
			select el;

		XElement cardData = cd.First ();

		cData.id = cardData.Attribute ("id").Value;
		cData.cost = int.Parse (cardData.Element ("cost").Value);
		//cData.oCost = int.Parse (cardData.Element ("cost").Value);
		cData.attack = int.Parse (cardData.Element ("attack").Value);
		//cData.oAttack = int.Parse (cardData.Element ("attack").Value);
		cData.health = int.Parse (cardData.Element ("health").Value);
		//cData.oHealth = int.Parse (cardData.Element ("health").Value);
		//cData.colour = cardData.Element ("colour").Value;

		string colours = cardData.Element ("colour").Value;

		List<string> colourList = colours.Split (',').ToList<string> ();

		cData.colour = colourList;

		string costPool = cardData.Element ("costPool").Value;
		if (cardData.Element ("costPool").Value != "") {

			List<string> costPoolList = costPool.Split (' ').ToList<string> ();

			cData.pool = costPoolList;
		}


		cData.name = cardData.Element ("name").Value;
		cData.image = cardData.Element ("image").Value;
		cData.text = cardData.Element ("text").Value;
		//cData.race = cardData.Element ("race").Value;
		cData.rarity=cardData.Element ("rarity").Value;
		cData.type = cardData.Element ("type").Value;

		cData.cp= new CardPassFactory().getCardPassive(cardData.Element ("passive").Value);

		cData.fontSize = float.Parse (cardData.Element ("fontSize").Value);
		path = Resources.Load ("xml/equipData", typeof(TextAsset)) as TextAsset;
		//Debug.Log (path);
		//Debug.Log ("not null");
		root = XElement.Parse (path.text);
		cd =
			from el in root.Elements ("equipments").Elements ("equip")
				where el.Attribute ("id").Value == id
			select el;


		if (cd.FirstOrDefault () != null) {
			XElement minionAbilites = cd.First ();

			if (minionAbilites.Element ("skills").Value != "") {
				string abi = minionAbilites.Element ("skills").Value;

				List<string>skills = abi.Split (' ').ToList<string> ();

				foreach (var skill in skills) {

					if (skill == "taunt") {

						cData.taunt = true;

					}

					if (skill == "charge") {

						cData.charge = true;

					}

					if (skill == "lifeSteal") {

						cData.lifeSteal = true;

					}

					if (skill == "block") {

						cData.block = true;

					}
				}
			}


			if (minionAbilites.Element ("awaken").Value != "") {
				cData.awaken = true;
			}

			if (minionAbilites.Element ("release").Value != "") {
				cData.release = true;
			}

		}

		//cData.originalData=cda
		return cData;

	}

	public void makeEquipCard(cardData cData, Card card){



		card.cData = cData;
		card.cData.cp.postion="hand";

	}

	public void makeEquipment(cardData cData, EquipData eData){

		eData.attack = cData.attack;
		eData.armor = cData.health;

		if (!cData.silence) {
		
			TextAsset	path = Resources.Load ("xml/equipData", typeof(TextAsset)) as TextAsset;
			//Debug.Log (path);
			//Debug.Log ("not null");
			XElement	root = XElement.Parse (path.text);
			IEnumerable<XElement>	cd =
				from el in root.Elements ("equipments").Elements ("equip")
					where el.Attribute ("id").Value == cData.id
				select el;

			if (cd.FirstOrDefault () != null) {
				XElement equipData = cd.First ();

				if (equipData.Element ("active").Value != "") {
					string actString = equipData.Element ("active").Value;

					List<string> actives = actString.Split (' ').ToList<string> ();

					foreach (var act in actives) {
				
						eData.aaList.Add (new ActiveFactory ().GetActiveAbility (act));
				
					}
				}
				string abi = equipData.Element ("skills").Value;

				List<string>skills = abi.Split (' ').ToList<string> ();

				foreach (var skill in skills) {

					if (skill == "taunt") {

						eData.taunt = true;

					}

					if (skill == "charge") {

						eData.charge = true;

					}

					if (skill == "lifeSteal") {

						eData.lifeSteal = true;

					}

					if (skill == "twinStrike") {

						eData.twinStrike = true;

					}
				}
			}

		}

	}
}

