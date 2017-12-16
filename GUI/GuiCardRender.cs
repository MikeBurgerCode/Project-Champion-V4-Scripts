using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GuiCardRender : MonoBehaviour {

	public GuiCardData guiData;
	public cardData cData;
	public cardData originalData;

	public TextMeshProUGUI costText;
	public TextMeshProUGUI nameText;
	public TextMeshProUGUI healthText;
	public TextMeshProUGUI attackText;
	public TextMeshProUGUI cardText;
	public TextMeshProUGUI typeText;
	public TextMeshProUGUI poolText;

	public GameObject cardBody;
	public GameObject costSymbol;
	public GameObject attackSymbol;
	public GameObject healthSymbol;
	public GameObject Art;
	// Use this for initialization

	private Color purple = new Color(201.0f/255,82.0f/255,209.0f/255);
	private Color AtkRed = new Color(255.0f/255,59.0f/255,10.0f/255);
	private Color HpRed= new Color(206.0f/255.0f,0.0f/255.0f,0.0f/255.0f);
	private Color green = new Color (0.0f,1.0f,0.0f);
	private Color white = new Color (1.0f,1.0f,1.0f);

	public string cText="";
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		cData = guiData.cData;
		originalData = guiData.originalData;

			if(cData!=null){
				

				switch (cData.type) {
				case "minion":
					cardColour ();
					renderMinion();
					break;

				case "spell":
					cardColour ();
					renderSpell ();
					break;
				case "equip": 
					cardColour ();
					renderEquip ();
					break;
				case "ability":
				renderAbility ();
					break;
				}


				Sprite spArt = new Sprite ();
			if (cData.image != "") {
				if (cData.type == "ability") {

					spArt = Resources.Load ("Sprites/Abilities/" + cData.image, typeof(Sprite)) as Sprite;
				} else {
					spArt = Resources.Load ("Sprites/Art/" + cData.image, typeof(Sprite)) as Sprite;
				}
				Art.GetComponent<Image> ().sprite = spArt;
			} else {
				spArt = Resources.Load ("Sprites/Art/null", typeof(Sprite)) as Sprite;
				Art.GetComponent<Image> ().sprite = spArt;
			}


				cardText.fontSize = cData.fontSize*10;
				//cardText.e
			}

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



		cardBody.GetComponent<Image>().sprite=spBody;
	}

	public void StatTextColour(){

		attackText.color = white;

		if (cData.attack > originalData.attack) {
			attackText.color = green;

		}

		if (cData.attack < originalData.attack) {
			attackText.color = purple;

		} 

		healthText.color = white;

		if (cData.health > originalData.health) {
			healthText.color = green;

		}

		if (cData.health < originalData.health) {
			healthText.color = purple;

		} 

		costText.color = white;

		if (cData.cost < originalData.cost) {
			costText.color = green;

		}

		if (cData.cost > originalData.cost) {
			costText.color = HpRed;

		} 
	}

	void abiText(){

		string colour = "#25a121";
		int abiCount = 0;


		if (cData.maxMana>0) {


			abiCount += 1;
			if (cData.originalData.maxMana == cData.maxMana) {
				cText += "<b>Max Mana +"+cData.maxMana+"</b>";
			} else {
				cText += "<color="+colour+"><b>Max Mana +"+cData.maxMana+"</b></color>";
			}
			abiCount += 1;
		}

		if (cData.taunt) {

			if (abiCount > 0) {
				cText+=",";

			}

			if (cData.originalData.taunt) {
				cText += "<b>Taunt</b>";
			} else {
				cText += "<color="+colour+"><b>Taunt</b></color>";
			}
			abiCount += 1;
		}

		if (cData.block) {

			if (abiCount > 0) {
				cText+=",";

			}

			if (cData.originalData.block) {
				cText += "<b>Block</b>";
			} else {
				cText += "<color="+colour+"><b>Block</b></color>";
			}
			abiCount += 1;
		}

		if (cData.charge) {

			if (abiCount > 0) {
				cText+=",";

			}
			if (originalData.charge) {
				cText += "<b>Charge</b>";
			} else {
				cText += "<color="+colour+"><b>Charge</b></color>";
			}
			abiCount += 1;
		}

		if (cData.lifeSteal) {

			if (abiCount > 0) {
				cText+=",";

			}
			if (originalData.lifeSteal) {
				cText += "<b>Life Steal</b>";
			} else {
				cText += "<color="+colour+"><b>Life Steal</b></color>";
			}
			abiCount += 1;
		}

		if (cData.lifeSteal) {

			if (abiCount > 0) {
				cText+=",";

			}
			if (originalData.twinStrike) {
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

	void renderMinion(){
		costSymbol.SetActive (true);
		attackSymbol.SetActive (true);
		healthSymbol.SetActive (true);

		Sprite spSymb = Resources.Load ("Sprites/Symbols/healthSymbol", typeof(Sprite)) as Sprite;

		healthSymbol.GetComponent<Image>().sprite = spSymb;

		StatTextColour();

		if (!cData.silence) {

		cText = "";
		abiText ();
		}
		cText += cData.text;
		cardText.text = cText;

		costText.text =""+cData.cost;
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

		StatTextColour();

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

	void renderMana(){

		StatTextColour();

		attackSymbol.SetActive (false);
		healthSymbol.SetActive (false);
		costSymbol.SetActive(false);

		nameText.text = cData.name;
		cardText.text = cData.text;

		attackText.text = "";
		healthText.text = "";

		costText.text = "";

		poolText.text = "";
		typeText.text="Shrine";
	}

	void renderEquip(){
		costSymbol.SetActive (true);
		attackSymbol.SetActive (true);
		healthSymbol.SetActive (true);

		Sprite spSymb = Resources.Load ("Sprites/Symbols/armorSymbol", typeof(Sprite)) as Sprite;

		healthSymbol.GetComponent<Image>().sprite = spSymb;


		StatTextColour();

		costText.text =""+cData.cost;
		nameText.text = cData.name;

		cText = "";
		abiText ();

		cText += cData.text;
		cardText.text = cText;

		string pool ="";

		if (cData.pool.Count > 0) {
			foreach (var p in cData.pool) {

				pool += "<sprite name=\"" + p + "\">";
			}
		}

		attackText.text = "" + cData.attack;
		healthText.text = "" + cData.health;

		poolText.text=pool;
		typeText.text="Equip";



	}

	void renderAbility(){


		Sprite spBody = Resources.Load ("Sprites/Cards/aCard", typeof(Sprite)) as Sprite;



		cardBody.GetComponent<Image>().sprite=spBody;

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

}
