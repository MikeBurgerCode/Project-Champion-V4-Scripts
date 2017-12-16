using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using TMPro;
using UnityEngine;

public class cardRender : MonoBehaviour {

	public Card card;


	public TextMeshPro costText;
	public TextMeshPro nameText;
	public TextMeshPro healthText;
	public TextMeshPro attackText;
	public TextMeshPro cardText;
	public TextMeshPro typeText;
	public TextMeshPro poolText;

	public GameObject cardBody;
	public GameObject costSymbol;
	public GameObject attackSymbol;
	public GameObject healthSymbol;
	public GameObject Art;

	private Color purple = new Color(201.0f/255,82.0f/255,209.0f/255);
	private Color AtkRed = new Color(255.0f/255,59.0f/255,10.0f/255);
	private Color HpRed= new Color(206.0f/255.0f,0.0f/255.0f,0.0f/255.0f);
	private Color green = new Color (0.0f,1.0f,0.0f);
	private Color white = new Color (1.0f,1.0f,1.0f);

	public string cText="";
	// Use this for initialization
	void Start () {
		//poolText.enableCulling=false;
	}
	void Awake(){
	//	poolText.enableCulling=true;
	//	poolText.ForceMeshUpdate ();
	}
	// Update is called once per frame
	void Update () {
		if(card!=null){
		cardColour ();

		switch (card.cData.type) {
		case "minion":
			renderMinion();
			break;

		case "spell":
			renderSpell ();
			break;
		case "equip": 
			renderEquip ();
			break;
		
		}


		Sprite spArt = new Sprite ();
			if (card.cData.image != "") {
				spArt = Resources.Load ("Sprites/Art/" + card.cData.image, typeof(Sprite)) as Sprite;
				Art.GetComponent<MeshRenderer> ().material.mainTexture = spArt.texture;
			} else {
				spArt = Resources.Load ("Sprites/Art/null", typeof(Sprite)) as Sprite;
				Art.GetComponent<MeshRenderer> ().material.mainTexture = spArt.texture;
			}

		
		cardText.fontSize = card.cData.fontSize;
		//cardText.e
		}

	}

	void cardColour(){
		Sprite spBody = new Sprite();

		string colour ="";
		foreach (var c in card.cData.colour) {
		
			colour += c;
		}

		if (card.cData.colour.Count > 1) {

			spBody = Resources.Load ("Sprites/Cards/bCard", typeof(Sprite)) as Sprite;
		} else {

			spBody = Resources.Load ("Sprites/Cards/" + colour + "Card", typeof(Sprite)) as Sprite;
		}

	

		cardBody.GetComponent<MeshRenderer>().material.mainTexture=spBody.texture;
	}

	void renderMinion(){
		costSymbol.SetActive (true);
		attackSymbol.SetActive (true);
		healthSymbol.SetActive (true);

		Sprite spSymb = Resources.Load ("Sprites/Symbols/healthSymbol", typeof(Sprite)) as Sprite;
	
		healthSymbol.GetComponent<MeshRenderer>().material.mainTexture = spSymb.texture;


		StatTextColour();

		cText = "";
		if (!card.cData.silence) {
			abiText ();
			cText += card.cData.text;
		}

		cardText.text = cText;

		costText.text =""+card.finalCost;
		nameText.text = card.cData.name;
		string race ="";
		string pool ="";
		foreach (var r in card.cData.race) {

			race += "<sprite name=\"" + r + "\">";
		}
		if (card.cData.pool.Count > 0) {
			foreach (var p in card.cData.pool) {

				pool += "<sprite name=\"" + p + "\">";
			}
		}
		attackText.text = "" + card.attack;
		healthText.text = "" + card.health;

		poolText.text=pool;
		typeText.text=race;

	}

	void renderEquip(){
		costSymbol.SetActive (true);
		attackSymbol.SetActive (true);
		healthSymbol.SetActive (true);

		Sprite spSymb = Resources.Load ("Sprites/Symbols/armorSymbol", typeof(Sprite)) as Sprite;

		healthSymbol.GetComponent<MeshRenderer>().material.mainTexture = spSymb.texture;


		StatTextColour();

		costText.text =""+card.finalCost;
		nameText.text = card.cData.name;

	
		cText = "";
		abiText ();
	
		cText += card.cData.text;

		cardText.text = cText;
		cardText.text = cText;
		string pool ="";

		if (card.cData.pool.Count > 0) {
			foreach (var p in card.cData.pool) {

				pool += "<sprite name=\"" + p + "\">";
			}
		}

		attackText.text = "" + card.attack;
		healthText.text = "" + card.health;

		poolText.text=pool;
		typeText.text="Equip";



	}
	void renderSpell(){

		StatTextColour();

		costSymbol.SetActive (true);
		attackSymbol.SetActive (false);
		healthSymbol.SetActive (false);
		costText.text =""+card.finalCost;
		nameText.text = card.cData.name;

		if (card.spell.getText() != "") {

			cardText.text = card.spell.getText ();

		} else {

			cardText.text = card.cData.text;
		}
		string pool ="";

		if (card.cData.pool.Count > 0) {
			foreach (var p in card.cData.pool) {

				pool += "<sprite name=\"" + p + "\">";
			}
		}
		poolText.text=pool;

		attackText.text = "";
		healthText.text = "";

		typeText.text="Spell";

	}



	void abiText(){

		string colour = "#25a121";
		int abiCount = 0;
		if (card.cData.maxMana>0) {


			abiCount += 1;
			if (card.cData.originalData.maxMana == card.cData.maxMana) {
				cText += "<b>Max Mana +"+card.cData.maxMana+"</b>";
			} else {
				cText += "<color="+colour+"><b>Max Mana +"+card.cData.maxMana+"</b></color>";
			}
			abiCount += 1;
		}
		if (card.cData.taunt) {
			if (abiCount > 0) {
				cText+=",";

			}

			if (card.cData.originalData.taunt) {
				cText += "<b>Taunt</b>";
			} else {
				cText += "<color="+colour+"><b>Taunt</b></color>";
			}
			abiCount += 1;
		}

		if (card.cData.block) {
			if (abiCount > 0) {
				cText+=",";

			}

			if (card.cData.originalData.block) {
				cText += "<b>Block</b>";
			} else {
				cText += "<color="+colour+"><b>Block</b></color>";
			}
			abiCount += 1;
		}

		if (card.cData.charge) {

			if (abiCount > 0) {
				cText+=",";

			}
			if (card.cData.originalData.charge) {
				cText += "<b>Charge</b>";
			} else {
				cText += "<color="+colour+"><b>Charge</b></color>";
			}
			abiCount += 1;
		}

		if (card.cData.lifeSteal) {

			if (abiCount > 0) {
				cText+=",";

			}
			if (card.cData.originalData.lifeSteal) {
				cText += "<b>Life Steal</b>";
			} else {
				cText += "<color="+colour+"><b>Life Steal</b></color>";
			}
			abiCount += 1;
		}
			
		if (card.cData.twinStrike) {

			if (abiCount > 0) {
				cText+=",";

			}
			if (card.cData.originalData.twinStrike) {
				cText += "<b>Twin Strike</b>";
			} else {
				cText += "<color="+colour+"><b>Twin Strike</b></color>";
			}
			abiCount += 1;
		}

		if (abiCount > 0) {
			cText += "<br>";
		}
	}


	public void StatTextColour(){

		attackText.color = white;

		if (card.cData.attack > card.cData.originalData.attack) {
			attackText.color = green;
		
		}
	
		if (card.cData.attack < card.cData.originalData.attack) {
			attackText.color = purple;

		} 

		healthText.color = white;

		if (card.cData.health > card.cData.originalData.health) {
			healthText.color = green;

		}

		if (card.cData.health < card.cData.originalData.health) {
			healthText.color = purple;

		} 

		costText.color = white;

		if (card.finalCost < card.cData.originalData.cost) {
			costText.color = green;

		}

		if (card.finalCost > card.cData.originalData.cost) {
			costText.color = HpRed;

		} 
	}


}
