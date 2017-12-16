using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml;
using System.IO;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeckList : MonoBehaviour {

	public string selectedDeck ="";
	public GameObject deckSlot;
	public Transform content;
	// Use this for initialization
	void Start () {

		selectedDeck = Path.GetFileNameWithoutExtension (gameStat.currentDeck);
		var decks =Directory.GetFiles(Application.persistentDataPath  + "/Decks");

		for (int i = 0; i < decks.Count(); i++) {

			deckSlot.GetComponent<DeckSlot>().deckName=Path.GetFileNameWithoutExtension (decks [i]);
			GameObject c=Instantiate (deckSlot);
			c.transform.parent = content;
			c.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void edit(){

		if (selectedDeck != "") {

			gameStat.currentDeck = selectedDeck + ".xml";
			SceneManager.LoadScene ("DeckEditor",LoadSceneMode.Single);

		} else {
			gameStat.currentDeck = "";
		}


	}

	public void newDeck(){
		gameStat.currentDeck = "";
		SceneManager.LoadScene ("DeckEditor",LoadSceneMode.Single);
	}
	public void play(){

		if (selectedDeck != "") {

			gameStat.currentDeck = selectedDeck + ".xml";
			SceneManager.LoadScene ("Lobby",LoadSceneMode.Single);
			//SceneManager.LoadScene ("PunBasics-Launcher",LoadSceneMode.Single);

		} else {
			gameStat.currentDeck = "";
		}
	}
}
