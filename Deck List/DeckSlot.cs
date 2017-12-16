using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Xml.Linq;
using System.Xml;
using System.IO;
using System.Linq;
using System.Text;

public class DeckSlot : MonoBehaviour {

	public TextMeshProUGUI nameGui;
	public Image abiImage1;
	public Image abiImage2;
	public string deckName,abi1,abi2="";
	public bool selected;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (selected) {
		
			deckName = GameObject.Find ("DeckList").GetComponent<DeckList> ().selectedDeck;
		}

		if (deckName != "") {

			nameGui.text = deckName;

			var path =File.ReadAllText (Application.persistentDataPath + "/Decks/"+deckName+".xml", Encoding.UTF8);

			XElement root = XElement.Parse (path);
			abi1=root.Attribute ("abi1").Value;
			abi2=root.Attribute ("abi2").Value;

			if (abi1 != "") {
				TextAsset path2 = Resources.Load ("xml/AbilityData", typeof(TextAsset)) as TextAsset;
				//			Debug.Log (path);

				//Debug.Log ("not null");
				XElement root2 = XElement.Parse (path2.text);
				IEnumerable<XElement> cd =
					from el in root2.Elements ("abilites").Elements ("ability")
					where el.Attribute ("id").Value == abi1
					select el;

				XElement abiData = cd.First ();

				abiImage1.sprite = Resources.Load ("Sprites/Abilities/" + abiData.Element ("image").Value, typeof(Sprite)) as Sprite;
			}

			if (abi2 != "") {
				TextAsset path2 = Resources.Load ("xml/AbilityData", typeof(TextAsset)) as TextAsset;
				//			Debug.Log (path);

				//Debug.Log ("not null");
				XElement root2 = XElement.Parse (path2.text);
				IEnumerable<XElement> cd =
					from el in root2.Elements ("abilites").Elements ("ability")
					where el.Attribute ("id").Value == abi2
					select el;

				XElement abiData = cd.First ();

				abiImage2.sprite = Resources.Load ("Sprites/Abilities/" + abiData.Element ("image").Value, typeof(Sprite)) as Sprite;
			} 

		}else {

			if (selected) {

				nameGui.text = "Empty";
				abiImage1.sprite = Resources.Load ("Sprites/Abilities/blankSymbol", typeof(Sprite)) as Sprite;
				abiImage2.sprite = Resources.Load ("Sprites/Abilities/blankSymbol", typeof(Sprite)) as Sprite;
			}
		}
	}

	public void click(){
		GameObject.Find ("DeckList").GetComponent<DeckList> ().selectedDeck = deckName;
	}

	public void deleteDeck(){

		if(GameObject.Find ("DeckList").GetComponent<DeckList> ().selectedDeck == deckName){

			GameObject.Find ("DeckList").GetComponent<DeckList> ().selectedDeck = "";
		}

		File.Delete (Application.persistentDataPath + "/Decks/"+deckName+".xml");
		Destroy (this.gameObject);

	}
}
