using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using TMPro;
using UnityEngine;

public class minionRender : MonoBehaviour {

	public minionData mData;
	public TextMeshPro healthText;
	public TextMeshPro attackText;
	public TextMeshPro armorText;

	public GameObject Art;
	public GameObject Border;
	public GameObject Stat;
	public GameObject armorStat;


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

		if (mData.id != "") {
			attackText.text = "" + mData.attack;
			healthText.text = "" + mData.health;
			StatTextColour ();
			Sprite spArt = new Sprite ();
			if (mData.image != "") {
				spArt = Resources.Load ("Sprites/Art/" + mData.image, typeof(Sprite)) as Sprite;
			} else {
				spArt = Resources.Load ("Sprites/Art/null", typeof(Sprite)) as Sprite;
			}
			Art.GetComponent<MeshRenderer> ().material.mainTexture = spArt.texture;

			if (mData.colour.Count > 0) {
				renderColour ();
			} else {
				Sprite spBorder = new Sprite ();
				spBorder = Resources.Load ("Sprites/Border/nBorder", typeof(Sprite)) as Sprite;
				Border.GetComponent<MeshRenderer> ().material.mainTexture = spBorder.texture;
			}

			status ();


			if (mData.armor > 0) {
				armorStat.SetActive (true);
				armorText.text = "" + mData.armor;
			
			} else {
				armorStat.SetActive (false);
			
			}

			if (mData.exhaust) {
			
				Art.GetComponent<MeshRenderer> ().material.color = new Color (0.5f, 0.5f, 0.5f);
				Border.GetComponent<MeshRenderer> ().material.color = new Color (0.5f, 0.5f, 0.5f);
			} else {
				Art.GetComponent<MeshRenderer> ().material.color = new Color (1.0f, 1.0f, 1.0f);
				Border.GetComponent<MeshRenderer> ().material.color = new Color (1.0f, 1.0f, 1.0f);
			
			}
		}
	}

	public void  renderColour(){

		Sprite spBorder = new Sprite();

		string colour ="";
		foreach (var c in mData.colour) {

			colour += c;
		}

		if (mData.colour.Count > 1) {
			spBorder = Resources.Load ("Sprites/Border/bBorder", typeof(Sprite)) as Sprite;
		} else {
			spBorder = Resources.Load ("Sprites/Border/" + colour + "Border", typeof(Sprite)) as Sprite;
		}



		Border.GetComponent<MeshRenderer>().material.mainTexture=spBorder.texture;

	}

	public void StatTextColour(){

		attackText.color = white;

		if (mData.attack > mData.originalData.attack) {
			attackText.color = green;

		}

		if (mData.attack < mData.originalData.attack) {
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




	}

	void status(){
		if (mData.summonSick) {
			Stat.transform.Find ("sleep").gameObject.SetActive (true);
		} else {
			Stat.transform.Find ("sleep").gameObject.SetActive (false);
		}

		if (mData.summonSick) {
			Stat.transform.Find ("sleep").gameObject.SetActive (true);
		} else {
			Stat.transform.Find ("sleep").gameObject.SetActive (false);
		}

		if (mData.taunt) {
			Stat.transform.Find ("taunt").gameObject.SetActive (true);
		} else {
			Stat.transform.Find ("taunt").gameObject.SetActive (false);
		}

		if (mData.hasLifeSteal()) {
			Stat.transform.Find ("lifesteal").gameObject.SetActive (true);
		} else {
			Stat.transform.Find ("lifesteal").gameObject.SetActive (false);
		}

		if (mData.equipment.Count > 0) {
			Stat.transform.Find ("equip").gameObject.SetActive (true);
		
		} else {
			Stat.transform.Find ("equip").gameObject.SetActive (false);
		}

		if (mData.block) {
			Stat.transform.Find ("block").gameObject.SetActive (true);

		} else {
			Stat.transform.Find ("block").gameObject.SetActive (false);
		}
	
	}
}


