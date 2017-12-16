using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using TMPro;
using UnityEngine;

public class ChampRender : MonoBehaviour {

	public Champion champ;

	public TextMeshPro healthText;
	public TextMeshPro attackText;
	public TextMeshPro nameText;
	public TextMeshPro armorText;
	public GameObject armor;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		attackText.text = "" + champ.attack;
		healthText.text = "" + champ.health;
		nameText.text = "" + champ.playerName;

		if (champ.armor > 0) {
			armor.SetActive (true);
			armorText.text = "" + champ.armor;
		} else {
			armor.SetActive (false);
		}
	}


}
