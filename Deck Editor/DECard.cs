using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using TMPro;
using UnityEngine;

public class DECard : MonoBehaviour {


	public cardData cData;

	public TextMeshPro costText;
	public TextMeshPro nameText;
	public TextMeshPro healthText;
	public TextMeshPro attackText;
	public TextMeshPro cardText;
	public TextMeshPro poolText;
	public TextMeshPro typeText;

	public GameObject Art;
	public GameObject Back;
	public GameObject costArt;
	public GameObject cardBody;
	public GameObject costSymbol;
	public GameObject attackSymbol;
	public GameObject healthSymbol;

	private Color purple = new Color(201.0f/255,82.0f/255,209.0f/255);
	private Color AtkRed = new Color(255.0f/255,59.0f/255,10.0f/255);
	private Color HpRed= new Color(206.0f/255.0f,0.0f/255.0f,0.0f/255.0f);
	private Color green = new Color (0.0f,1.0f,0.0f);
	private Color white = new Color (1.0f,1.0f,1.0f);

	public string cText="";
	// Use this for initialization
	void Start () {
		//poolText.enableCulling=false;
		//cData= new LoadData().loadMinionCardData("mTest");
		//cData= new LoadData().loadSpellCardData("spTest");
	}
	void Awake(){
		//	poolText.enableCulling=true;
		//	poolText.ForceMeshUpdate ();
	}
	// Update is called once per frame
	void Update () {
		//if(card!=null){
		//cardColour();

			switch (cData.type) {
			case "minion":
				renderMinion();
				cardColour ();
				break;

			case "spell":
				renderSpell ();
				cardColour ();
				break;
			case "ability": 
				renderAbility ();
				break;
		case "equip": 
			renderEquip ();
			cardColour ();
			break;

			}


			Sprite spArt = new Sprite ();
			if (cData.image != "") {
				//Debug.Log ("load art");
				if (cData.type == "ability") {
					spArt = Resources.Load ("Sprites/Abilities/" + cData.image, typeof(Sprite)) as Sprite;
					Art.GetComponent<MeshRenderer> ().material.mainTexture = spArt.texture;
				}else{
					spArt = Resources.Load ("Sprites/Art/" + cData.image, typeof(Sprite)) as Sprite;
					Art.GetComponent<MeshRenderer> ().material.mainTexture = spArt.texture;
				}
			} else {
				spArt = Resources.Load ("Sprites/Art/null", typeof(Sprite)) as Sprite;
				Art.GetComponent<MeshRenderer> ().material.mainTexture = spArt.texture;
			}


			cardText.fontSize = cData.fontSize;
			//cardText.e


	}

	void cardColour(){
		Sprite spBody = new Sprite();

		string colour ="";
		foreach (var c in cData.colour) {

			colour += c;
		}

		if (cData.colour.Count > 1) {

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

		cText = "";
	//	if (!card.cData.silence) {
			abiText ();
			cText += cData.text;
		//}

		cardText.text = cText;

		costText.text = "" + cData.cost;
		nameText.text = cData.name;
		string race ="";
		string pool ="";
		foreach (var r in cData.race) {

			race += "<sprite name=\"" + r + "\">";
		}
		if (cData.pool.Count > 0) {
			foreach (var p in cData.pool) {

				pool += "<sprite name=\"" + p + "\">";
			}
		}
		attackText.text = "" + cData.attack;
		healthText.text = "" + cData.health;

		poolText.text=pool;
		typeText.text=race;

	}
	void renderSpell(){



		costSymbol.SetActive (true);
		attackSymbol.SetActive (false);
		healthSymbol.SetActive (false);
		costText.text =""+cData.cost;
		nameText.text = cData.name;



			cardText.text = cData.text;
	
		string pool ="";

		if (cData.pool.Count > 0) {
			foreach (var p in cData.pool) {

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

		if (cData.maxMana > 0) {
			cText += "<b>Max Mana +"+cData.maxMana+"</b>";
			abiCount += 1;
		}


		if (cData.taunt) {
			if (abiCount > 0) {
				cText+=",";

			}


			cText += "<b>Taunt</b>";
		
			abiCount += 1;
		}

		if (cData.block) {
			if (abiCount > 0) {
				cText+=",";

			}


			cText += "<b>Block</b>";

			abiCount += 1;
		}

		if (cData.charge) {

			if (abiCount > 0) {
				cText+=",";

			}

			cText += "<b>Charge</b>";

			abiCount += 1;
		}

		if (cData.lifeSteal) {

			if (abiCount > 0) {
				cText+=",";

			}

			cText += "<b>Life Steal</b>";

			abiCount += 1;
		}

		if (cData.twinStrike) {

			if (abiCount > 0) {
				cText+=",";

			}

			cText += "<b>Twin Strike</b>";

			abiCount += 1;
		}

		if (abiCount > 0) {
			cText += "<br>";
		}
	}

	void renderAbility(){


		Sprite spBody = Resources.Load ("Sprites/Cards/aCard", typeof(Sprite)) as Sprite;



		cardBody.GetComponent<MeshRenderer>().material.mainTexture=spBody.texture;

		costSymbol.SetActive (true);
		attackSymbol.SetActive (false);
		healthSymbol.SetActive (false);
		costText.text =""+cData.cost;
		nameText.text = cData.name;



		cardText.text = cData.text;

		string pool ="";

		if (cData.pool.Count > 0) {
			foreach (var p in cData.pool) {

				pool += "<sprite name=\"" + p + "\">";
			}
		}
		poolText.text=pool;

		attackText.text = "";
		healthText.text = "";

		typeText.text="Ability";

	}

	void renderEquip(){
		costSymbol.SetActive (true);
		attackSymbol.SetActive (true);
		healthSymbol.SetActive (true);

		Sprite spSymb = Resources.Load ("Sprites/Symbols/armorSymbol", typeof(Sprite)) as Sprite;

		healthSymbol.GetComponent<MeshRenderer>().material.mainTexture = spSymb.texture;



		costText.text =""+cData.cost;
		nameText.text = cData.name;

		cardText.text = cData.text;

		string pool ="";

		if (cData.pool.Count > 0) {
			foreach (var p in cData.pool) {

				pool += "<sprite name=\"" + p + "\">";
			}
		}



		cText = "";
		//	if (!card.cData.silence) {
		abiText ();
		cText += cData.text;
		//}

		cardText.text = cText;

		attackText.text = "" + cData.attack;
		healthText.text = "" + cData.health;

		poolText.text=pool;
		typeText.text="Equip";



	}

	void OnMouseDown(){

		DeckEdiotr DE = GameObject.Find ("DeckEditor").GetComponent<DeckEdiotr> ();


		if (cData.type == "ability") {
		
			DE.addAbi (cData);
		} else {
			DE.addCard (cData);
		}

	}

}
