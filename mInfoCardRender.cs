using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using TMPro;
using UnityEngine;

public class mInfoCardRender : MonoBehaviour {

	public minionData mData;

	public TextMeshPro costText;
	public TextMeshPro nameText;
	public TextMeshPro healthText;
	public TextMeshPro attackText;
	public TextMeshPro cardText;
	public TextMeshPro typeText;
	public TextMeshPro poolText;
	public TextMeshPro armorText;

	public GameObject cardBody;
	public GameObject costSymbol;
	public GameObject attackSymbol;
	public GameObject healthSymbol;
	public GameObject armorSymbol;
	public GameObject Art;

	public string cText="";

	private Color purple = new Color(201.0f/255,82.0f/255,209.0f/255);
	private Color AtkRed = new Color(255.0f/255,59.0f/255,10.0f/255);
	private Color HpRed= new Color(206.0f/255.0f,0.0f/255.0f,0.0f/255.0f);
	private Color green = new Color (0.0f,1.0f,0.0f);
	private Color white = new Color (1.0f,1.0f,1.0f);
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (mData != null) {
		
			cardText.fontSize = mData.originalData.fontSize;
			attackText.text = "" + mData.attack;
			healthText.text = "" + mData.health;

			if (mData.armor > 0) {

				armorSymbol.SetActive (true);
				armorText.text = "" + mData.armor;

			} else {
				armorSymbol.SetActive (false);
			}

			nameText.text = mData.name;
			string race ="";
			string pool ="";
			foreach (var r in mData.race) {

				race += "<sprite name=\"" + r + "\">";
			}
			foreach (var p in mData.pool) {

				pool += "<sprite name=\"" + p + "\">";
			}

			costText.text = ""+mData.cost;

			Sprite spArt = new Sprite ();
			if (mData.image != "") {
				spArt = Resources.Load ("Sprites/Art/" + mData.image, typeof(Sprite)) as Sprite;
				Art.GetComponent<MeshRenderer> ().material.mainTexture = spArt.texture;
			}

			Sprite spBody = new Sprite();

			string colour ="";
			foreach (var c in mData.colour) {

				colour += c;
			}

			if (mData.colour.Count > 1) {
				spBody = Resources.Load ("Sprites/Cards/bCard", typeof(Sprite)) as Sprite;
			} else {
				spBody = Resources.Load ("Sprites/Cards/"+colour+"Card", typeof(Sprite)) as Sprite;
			}


			cardBody.GetComponent<MeshRenderer>().material.mainTexture=spBody.texture;

			poolText.text=pool;
			typeText.text=race;


			cText = "";
			abiText ();
			cText += mData.text;
			cardText.text = cText;
			StatTextColour ();
		}
	}

	void abiText(){

		string colour = "#25a121";
		int abiCount = 0;


		if (mData.maxMana>0) {


			abiCount += 1;
			if (mData.originalData.maxMana == mData.maxMana) {
				cText += "<b>Max Mana +"+mData.maxMana+"</b>";
			} else {
				cText += "<color="+colour+"><b>Max Mana +"+mData.maxMana+"</b></color>";
			}
			abiCount += 1;
		}

		if (mData.hasTaunt()) {

			if (abiCount > 0) {
				cText+=",";

			}

			if (mData.originalData.taunt) {
				cText += "<b>Taunt</b>";
			} else {
				cText += "<color="+colour+"><b>Taunt</b></color>";
			}
			abiCount += 1;
		}

		if (mData.block) {

			if (abiCount > 0) {
				cText+=",";

			}

			if (mData.originalData.block) {
				cText += "<b>Block</b>";
			} else {
				cText += "<color="+colour+"><b>Block</b></color>";
			}
			abiCount += 1;
		}

		if (mData.hasCharge()) {

			if (abiCount > 0) {
				cText+=",";
			
			}
			if (mData.originalData.charge) {
				cText += "<b>Charge</b>";
			} else {
				cText += "<color="+colour+"><b>Charge</b></color>";
			}
			abiCount += 1;
		}
	
		if (mData.hasLifeSteal()) {

			if (abiCount > 0) {
				cText+=",";

			}
			if (mData.originalData.lifeSteal) {
				cText += "<b>Life Steal</b>";
			} else {
				cText += "<color="+colour+"><b>Life Steal</b></color>";
			}
			abiCount += 1;
		}

		if (mData.hasTwinStrike()) {

			if (abiCount > 0) {
				cText+=",";

			}
			if (mData.originalData.twinStrike) {
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

		if (mData.getAtk() > mData.originalData.attack) {
			attackText.color = green;

		}

		if (mData.getAtk() < mData.originalData.attack) {
			attackText.color = purple;

		} 

		healthText.color = white;

		if (mData.health > mData.originalData.health) {
			healthText.color = green;

		}

		if (mData.health < mData.originalData.health) {
			healthText.color = purple;

		} 

		if (mData.injured) {
			healthText.color = HpRed;
		}

		costText.color = white;

		if (mData.cost > mData.originalData.cost) {
			attackText.color = HpRed;

		}

		if (mData.cost < mData.originalData.cost) {
			attackText.color = green;

		}

	}
}
