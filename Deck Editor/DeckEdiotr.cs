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


public class DeckEdiotr : MonoBehaviour {

	public TMP_InputField nameDeckText;
	public int cardLimit =2;
	public GameObject deSlot;
	public Transform content;
	public int cardIndexSize;
	public string currentColour = "n";
	public string deckName = "";
	public int page = 0;
	public int num = 0;
	public cardData[] Abilites = new cardData[2];
	public GameObject forwButton;
	public GameObject backButton;

	public List<cardData> deck = new List<cardData> ();
	public List<GameObject> cards = new List <GameObject> ();
	public List<cardData> cardIndex = new List<cardData> ();
	// Use this for initialization
	void Start () {

		if (!Directory.Exists (Application.persistentDataPath + "/Decks")) {
			var folder = Directory.CreateDirectory (Application.persistentDataPath + "/Decks");
		}


		if (gameStat.currentDeck != "") {

			loadDeck ();
		
		}

		getIndex ();
		
	}

	void loadDeck(){
		nameDeckText.text =  Path.GetFileNameWithoutExtension(Application.persistentDataPath + "/Decks/"+gameStat.currentDeck);
	
		var path =File.ReadAllText (Application.persistentDataPath + "/Decks/"+gameStat.currentDeck, Encoding.UTF8);

		XElement root = XElement.Parse (path);
		//champId=root.Attribute ("champion").Value;
		Abilites[0]=new LoadData().loadAbiCardData(root.Attribute ("abi1").Value);
		Abilites[1]=new LoadData().loadAbiCardData(root.Attribute ("abi2").Value);
		IEnumerable<XElement> cd =
			from el in root.Elements ("cards").Elements ("card")
			//		where (int)el.Attribute("id")== id
			select el;


		foreach (var el in cd) {

			//Debug.Log (el.Value);
			//deck.Add(el.Value);

			//Debug.Log (el.Attribute ("type").Value);
			cardData c = new cardData ();



			//Debug.Log (int.Parse(el.Value));
			string id = el.Value;
			string type = el.Attribute ("type").Value;


			switch (type) {
			case "minion":
				c = new LoadData ().loadMinionCardData (id);
				break;

			case "spell":
				c = new LoadData ().loadSpellCardData (id);
				break;
			case "equip": 
				c = new LoadData ().loadEquipCardData (id);
				break;
			}

			addCard (c);
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (page == 0) {
			backButton.SetActive (false);
		
		} else {
			backButton.SetActive (true);
		}

		content.Find ("Top").SetAsFirstSibling ();
		content.Find ("Bottom").SetAsLastSibling ();

		for (int i = 0; i < 6; i++){
			if (i+page < cardIndex.Count) {
				cards [i].SetActive (true);
				cards [i].GetComponent<DECard> ().cData = cardIndex [i+page];
				forwButton.SetActive (true);
				if ((i+page) == cardIndex.Count-1) {
					forwButton.SetActive (false);
				}

			} else {
				cards [i].SetActive (false);
				forwButton.SetActive (false);

				//cards [i].GetComponent<DECard> ().cardData = null;
			}
		}
	}


	void getIndex(){
		
		TextAsset path = Resources.Load ("xml/CardDataBase", typeof(TextAsset)) as TextAsset;

		XElement root = XElement.Parse (path.text);
		IEnumerable<XElement> cd =
			from el in root.Elements ("cards").Elements ("card")
				where el.Element ("colour").Value.Contains(currentColour) && el.Element ("deckListed").Value == "true"
			orderby  el.Element ("cost").Value ascending,el.Element ("name").Value ascending
		select el;

		//cd.OrderBy()
		foreach (var el in cd) {

			//Debug.Log (el.Value);
			//deck.Add(el.Value);

			//Debug.Log (el.Attribute ("type").Value);
			cardData c = new cardData ();



			//Debug.Log (int.Parse(el.Value));
			string id = el.Attribute ("id").Value;
			string type = el.Element ("type").Value;

			switch (type) {
			case "minion":
				c = new LoadData().loadMinionCardData(id);
				//Debug.Log ("minion");
				break;

			case "spell":
				c =  new LoadData().loadSpellCardData (id);
				break;
			case "equip": 
				c = new LoadData ().loadEquipCardData (id);
				break;
			case "Champion":
				//c = loader.loadChampData (id);
				break;
			}

			cardIndex.Add (c);

		}
	}

	public void getAbiIndex(int i){

		num = i;
		cardIndex.Clear ();
		page = 0;

		TextAsset path = Resources.Load ("xml/AbilityData", typeof(TextAsset)) as TextAsset;

		XElement root = XElement.Parse (path.text);
		IEnumerable<XElement> cd =
			from el in root.Elements ("abilites").Elements ("ability")
			orderby  el.Element ("req").Value ascending,el.Element ("name").Value ascending
		select el;

		//cd.OrderBy()
		foreach (var el in cd) {

			//Debug.Log (el.Value);
			//deck.Add(el.Value);

			//Debug.Log (el.Attribute ("type").Value);
			cardData c = new cardData ();



			//Debug.Log (int.Parse(el.Value));
			string id = el.Attribute ("id").Value;

			c = new LoadData ().loadAbiCardData (id);

			cardIndex.Add (c);

		}
	}

	public void addAbi(cardData abi){

		if (num == 0) {

			if (abi.id != Abilites [1].id) {
				Abilites [num] = abi;
			}
		}

		if (num == 1) {

			if (abi.id != Abilites [0].id) {
				Abilites [num] = abi;
			}
		}
	}
	public void setColour(string c){

		cardIndex.Clear ();
		page = 0;
		currentColour = c;
		getIndex ();
	}



	public void addCard(cardData cData){
	
		int i = 0;
		foreach (var card in deck) {

			if (card.id == cData.id) {
			
				i += 1;
			}
		
		}

		Debug.Log (i);
		if (i < cardLimit) {
		
			if (i > 0) {
				deck.Add (cData);

				GameObject[] c = GameObject.FindGameObjectsWithTag ("DESlot");

				foreach (var card in c) {
				
					if (card.GetComponent<DESlot> ().cData.id == cData.id) {
						card.GetComponent<DESlot> ().sum += 1;
					}

				}
			} else {
			
				deck.Add (cData);
				deSlot.GetComponent<DESlot> ().cData = cData;
				GameObject c=Instantiate (deSlot);
				c.transform.parent = content;
				c.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
			
			}
		

		}
	
	}

	public void SaveDeck(){
		XmlWriterSettings settings = new XmlWriterSettings ();
		settings.Indent = true;
		settings.IndentChars = "\t";
		settings.Encoding = Encoding.UTF8;

		var sww = new StringWriter ();
		XmlWriter writer;
		using ( writer = XmlWriter.Create (sww,settings)) {


			writer.WriteStartDocument ();

			writer.WriteStartElement("Deck");
			writer.WriteAttributeString ("abi1", ""+Abilites[0].id);
			writer.WriteAttributeString ("abi2", ""+Abilites[1].id);
			writer.WriteStartElement("cards","");

			for (int i = 0; i < deck.Count (); i++) {
				writer.WriteStartElement ("card");
				writer.WriteAttributeString ("type", ""+deck[i].type);
				writer.WriteString (""+deck[i].id);
				writer.WriteEndElement ();
			}

			writer.WriteEndElement ();
			writer.WriteEndElement ();
			//writer.WriteEndElement ();
			writer.WriteEndDocument();
			//		writer.Close ();

		}

		Debug.Log (sww.ToString());
		string xml = sww.ToString ();
		File.WriteAllText (Application.persistentDataPath  + "/Decks/" + nameDeckText.text + ".xml", ""+xml,Encoding.UTF8);
		Debug.Log (Application.persistentDataPath );

		back ();
		//	sww.Close ();


	}

	public void back(){
		SceneManager.LoadScene ("DeckList",LoadSceneMode.Single);

	}
}
