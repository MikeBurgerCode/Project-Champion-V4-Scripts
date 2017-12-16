using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DESlot : MonoBehaviour {

	public Image colourArt;
	public TextMeshProUGUI costText;
	public TextMeshProUGUI nameText;
	public TextMeshProUGUI sumText;
	public int sum = 1;
	public cardData cData;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		nameText.text = cData.name;
		costText.text = ""+cData.cost;
		sumText.text = "x" + sum;

		//Sprite spArt = Resources.Load ("Sprites/DeckSlot/" + cData.image, typeof(Sprite)) as Sprite;
		//colourArt.

		string colour ="";
		foreach (var c in cData.colour) {

			colour += c;
		}

		if (cData.colour.Count > 1) {
			colourArt.sprite = Resources.Load ("Sprites/DeckSlot/bDeckSlot", typeof(Sprite)) as Sprite;
		} else {
			colourArt.sprite = Resources.Load ("Sprites/DeckSlot/" + colour + "DeckSlot", typeof(Sprite)) as Sprite;
		}
	}

	public void removeCard(){
	
		DeckEdiotr DE = GameObject.Find ("DeckEditor").GetComponent<DeckEdiotr> ();
		for (int i = 0; i < DE.deck.Count; i++) {
		
			if (DE.deck [i].id == cData.id) {
				DE.deck.RemoveAt (i);
			
				break;
			}
		
		}

		sum -= 1;

		if (sum <1) {

			Destroy (this.gameObject);
		}

	
	}
}
