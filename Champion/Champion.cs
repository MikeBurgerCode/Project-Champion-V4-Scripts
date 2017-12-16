using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;

public class Champion: Photon.PunBehaviour ,IPunObservable {


	public bool deckLoaded=false;
	public GameManager gManager;
	public string deckName ="";
	public Champion opponent;
	public List<cardData> deck = new List<cardData>();
	public List<cardData> grave = new List<cardData>();
	//public List<cardData> manaDeck = new List<cardData> ();
	public int deckCount;
	public int attack=0;
	public int health =30;
	public int armor = 0;
	public int spellDamage = 0;

	public int mana = 0;
	public int maxMana=0;
	public int currentMaxMana=0;
	public int mMax=10;
	public int[] power = new int[6]; //r=0,u=1,y=2,p=3,g=4,w=5

	public List<GameObject> hand = new List<GameObject> ();
	public List<GameObject> minions = new List<GameObject> ();
	public List<ChampAbility> abilites = new List<ChampAbility> ();

	public bool busy = false;
	public string playerName="";

	//public Card test,test2;
	//public DrawButton db1,db2;
	public bool manaPlay = false;
	public bool taunt,block,lifeSteal=false;
	// Use this for initialization
	void Start () {


		//loadDeck ("","");
		//new LoadData ().makeManaCard (Deck[0], test);
		//new LoadData ().makeMinionCard (Deck[1], test2);
		//new LoadData ().makeMinion (testMinion, test);
		//deck [0];
	}
	
	// Update is called once per frame
	void Update () {

		if (PhotonNetwork.isMasterClient) {


			deckCount = deck.Count ();
		}

		if (Input.GetKeyUp ("space") && playerName==PhotonNetwork.player.NickName) {
			this.photonView.RPC ("draw", PhotonTargets.MasterClient);
		}
	}

	public void GameUpdate(){

		currentMaxMana = getMaxMana ();
	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting) {
			//stream.SendNext (owner);
			stream.SendNext (playerName);

			stream.SendNext (attack);
			stream.SendNext (health);
			stream.SendNext (armor);
			stream.SendNext (mana);
			stream.SendNext (maxMana);
			stream.SendNext (currentMaxMana);

			stream.SendNext (manaPlay);

			stream.SendNext (power[0]);
			stream.SendNext (power[1]);
			stream.SendNext (power[2]);
			stream.SendNext (power[3]);
			stream.SendNext (power[4]);
			stream.SendNext (power[5]);

		

			stream.SendNext (new Serializer().SerializeToString(deck));
			stream.SendNext (new Serializer().SerializeToString(grave));

			stream.SendNext (new Serializer().SerializeToString(abilites));

		} else {
			playerName=(string) stream.ReceiveNext();

			attack=(int) stream.ReceiveNext();
			health=(int) stream.ReceiveNext();
			armor=(int) stream.ReceiveNext();
			mana=(int) stream.ReceiveNext();
			maxMana=(int) stream.ReceiveNext();
			currentMaxMana=(int) stream.ReceiveNext();

			manaPlay=(bool) stream.ReceiveNext();

			power[0]=(int) stream.ReceiveNext();
			power[1]=(int) stream.ReceiveNext();
			power[2]=(int) stream.ReceiveNext();
			power[3]=(int) stream.ReceiveNext();
			power[4]=(int) stream.ReceiveNext();
			power[5]=(int) stream.ReceiveNext();



			deck = new Serializer().DeserializeFromString<List<cardData>> ((string)stream.ReceiveNext ());
			grave = new Serializer().DeserializeFromString<List<cardData>> ((string)stream.ReceiveNext ());
			abilites = new Serializer().DeserializeFromString<List<ChampAbility>> ((string)stream.ReceiveNext ());

		}
	}
	public void shuffleDeck(){

		int n = deck.Count;
		List<cardData> newDeck = new List<cardData> ();
		List<cardData> oldDeck = new List<cardData> ();
		oldDeck = deck;
		//Debug.Log (n);
		while (n > 0) {
			n--;
			int k = Random.Range (0, oldDeck.Count);
			//Debug.Log (k);
			newDeck.Add (oldDeck [k]);
			oldDeck.RemoveAt (k);

		}
		deck = newDeck;
	}
	[PunRPC]
	public void loadDeck(string path,string playerName){
		//TextAsset pathg = Resources.Load ("xml/TestDeck", typeof(TextAsset)) as TextAsset;

		//var path =File.ReadAllText (Application.persistentDataPath + "/Decks/"+gameStat.currentDeck, Encoding.UTF8);
		XElement root = XElement.Parse(path);


	//	Debug.Log ( root.Attribute ("abi1").Value);
		abilites.Add(new LoadData().loadAbility(root.Attribute ("abi1").Value));
		abilites.Add(new LoadData().loadAbility(root.Attribute ("abi2").Value));

		checkForFunctionPlus (abilites[0].id);
		checkForFunctionPlus (abilites[1].id);

		IEnumerable<XElement> cd2 =
			from el in root.Elements ("cards").Elements ("card")
			//		where (int)el.Attribute("id")== id
			select el;

		foreach (var el in cd2) {

		
			cardData cData = new cardData ();



			//Debug.Log (int.Parse(el.Value));
			string id = el.Value;
			string type = el.Attribute ("type").Value;


			switch (type) {
			case "minion":
				cData = new LoadData ().loadMinionCardData (id);
				cData.originalData=new LoadData ().loadMinionCardData (id);
				break;

			case "spell":
				cData = new LoadData ().loadSpellCardData (id);
				cData.originalData=new LoadData ().loadSpellCardData (id);
				break;

			case "equip": 
				cData = new LoadData ().loadEquipCardData (id);
				cData.originalData=new LoadData ().loadEquipCardData (id);
				break;

			case "mana": 
				cData = new LoadData ().loadManaCardData (id);
				break;
			}

			cData.owner = this.tag;
			cData.cp.cData = cData;
			deck.Add (cData);
		}

		shuffleDeck ();
		deckLoaded = true;
	}

	[PunRPC]
	public void mulligian(){
	
		cardData cData = new cardData ();
		foreach (var card in hand) {
			cardData c = card.GetComponent<Card> ().cData;
		
			switch (c.type) {
			case "minion":
				cData = new LoadData ().loadMinionCardData (c.id);
				cData.originalData=new LoadData ().loadMinionCardData (c.id);
				break;

			case "spell":
				cData = new LoadData ().loadSpellCardData (c.id);
				cData.originalData=new LoadData ().loadSpellCardData (c.id);
				break;

			case "equip":
				cData = new LoadData ().loadEquipCardData (c.id);
				cData.originalData=new LoadData ().loadEquipCardData (c.id);
				break;

			case "mana": 
				cData = new LoadData ().loadManaCardData (c.id);
				break;
			}

			cData.owner = this.tag;
			cData.cp.cData = cData;
			deck.Add (cData);
		}
	
		shuffleDeck ();

		foreach (var card in hand) {
			Card c = card.GetComponent<Card> ();
			switch (deck[0].type) {
			case "minion":
				new LoadData ().makeMinionCard (deck[0],c);
				break;
			case "spell":
				new LoadData ().makeSpellCard (deck[0], c);
				break;

			case "equip":
				new LoadData ().makeEquipCard (deck[0], c);
				break;

			case "mana":
				new LoadData ().makeManaCard (deck[0], c);
				break;
			}
		
			deck.RemoveAt (0);

		}
	}


	[PunRPC]
	public void summonMinion(string data,int summonIndex){
		cardData cData=new Serializer().DeserializeFromString<cardData> (data);
		gManager.summonMinion = cData;
		gManager.summonIndex = summonIndex;
		gManager.summoning = true;
		PhotonNetwork.Instantiate("Minion", new Vector3(0f,0f,0f), Quaternion.identity, 0);
	}


	[PunRPC]
	public void addCardtoHand(string data){

		cardData cData=new Serializer().DeserializeFromString<cardData> (data);

		switch (cData.type) {
		case "minion":
			cData.originalData = new LoadData ().loadMinionCardData (cData.id);
			break;
		case "spell":
			cData.originalData = new LoadData ().loadSpellCardData (cData.id);
			break;

		case "equip":
			cData.originalData=new LoadData ().loadEquipCardData (cData.id);
			break;
		}
		cData.cp.postion = "hand";

		gManager.cardDrew = cData;
		gManager.playerDrew = this.gameObject.tag;
		PhotonNetwork.Instantiate ("Card", new Vector3 (14.02f, -0.95f, 0f), Quaternion.identity, 0);
		gManager.GameUpdate ();
	}

	[PunRPC]
	public void drawCardtoHand(string data){

		cardData cData=new Serializer().DeserializeFromString<cardData> (data);
		cData.cp.postion = "hand";
		gManager.cardDrew = cData;
		gManager.playerDrew = this.gameObject.tag;
		PhotonNetwork.Instantiate ("Card", new Vector3 (14.02f, -0.95f, 0f), Quaternion.identity, 0);
		gManager.GameUpdate ();
		//draw trigger
	}
	[PunRPC]
	public void draw(){

		if (deck.Count > 0) {
			
			gManager.cardDrew = deck [0];
			gManager.playerDrew = this.gameObject.tag;
			PhotonNetwork.Instantiate ("Card", new Vector3 (14.02f, -0.95f, 0f), Quaternion.identity, 0);
			deck.RemoveAt (0);
			gManager.GameUpdate ();
		}

	}

	[PunRPC]
	public void DiscardACard(){
		int index = Random.Range (0, hand.Count);
		hand [index].GetComponent<Card> ().Discard ();
	
	
	}

	[PunRPC]
	public void StartTurn(){
	
		//this.photonView.RPC ("draw", PhotonTargets.MasterClient);

		if (PhotonNetwork.isMasterClient) {

			if (maxMana < mMax) {
				maxMana += 1;
			}

			currentMaxMana = getMaxMana ();

			mana = currentMaxMana;
			manaPlay = false;

			foreach (var abi in abilites) {
			
				abi.readyUp ();
			
			}

			foreach (var minion in minions) {
		
				minion.GetComponent<minionData> ().atkTimes = 1;
				if (minion.GetComponent<minionData> ().twinStrike) {
					minion.GetComponent<minionData> ().atkTimes = 2;
				}
				minion.GetComponent<minionData> ().attacked = false;
				minion.GetComponent<minionData> ().summonSick = false;
				minion.GetComponent<minionData> ().exhaust = false;
		
			}

			draw ();

			gManager.GameUpdate ();
		}


	}

	[PunRPC]
	public void EndTurn(){


	}

	[PunRPC]
	public void setBusy(bool b){
	
		busy = b;
	}

	[PunRPC]
	public void sendMessage(string Msg){
		GameObject.Find ("GameText").GetComponent<TextMeshPro> ().text = Msg;
		//Debug.Log ("msg: "+Msg);
		StartCoroutine(wait(4));

	}

	IEnumerator wait(float time){


		yield return new WaitForSeconds (time);
		GameObject.Find ("GameText").GetComponent<TextMeshPro> ().text = "";
	}

	[PunRPC]
	public void Heal(int heal){

		health += heal;

		gManager.HealChampTrigger (heal, this);
		gManager.GameUpdate ();

	}
	[PunRPC]
	public void TakeDamageFromSpell(int damage, string spell){

		Debug.Log ("take from spell");

		SpellData sp =new Serializer ().DeserializeFromString<SpellData> (spell);
		Champion spellOwner = GameObject.FindGameObjectWithTag (sp.champTag).GetComponent<Champion>();

		if (damage > 0) {
			if (!block) {
				if (armor > 0) {
					armor -= damage;
					if (armor < 0) {
						int leftOverDmg = Mathf.Abs (armor);
						health -= leftOverDmg;
						armor = 0;
						if (spellOwner.lifeSteal || sp.lifeSteal) {
							spellOwner.Heal (leftOverDmg);

						}
					}

				} else {
					health -= damage;
					if (spellOwner.lifeSteal || sp.lifeSteal) {
						spellOwner.Heal (damage);

					}
				}
			} else {
				block = false;

			}
		}

	}
	[PunRPC]
	public void TakeDamageFromMinion(int damage,int minionIndex){
		
		if (!block) {
			Debug.Log ("take"+damage+ "damage");
			minionData enemy = gManager.minions [minionIndex];

			if (armor > 0) {
				armor -= damage;
				if (armor < 0) {
					int leftOverDmg = Mathf.Abs (armor);
					health -= leftOverDmg;
					armor = 0;
					if (enemy.hasLifeSteal()) {
						enemy.ownerChamp.Heal (leftOverDmg);

					}
				}

			} else {

				health -= damage;
				if (enemy.lifeSteal) {
					enemy.ownerChamp.Heal (damage);

				}
			}


		} else {
			block = false;

		}
	}


	[PunRPC]
	public void AddCardToGrave(string data){
		cardData cData = new Serializer ().DeserializeFromString<cardData> (data);
		cData.cp.postion="grave";
		grave.Add (cData);
	
	
	}

	[PunRPC]
	public void RemoveCardFromGrave(string data){
		cardData cData = new Serializer ().DeserializeFromString<cardData> (data);
		cData.cp.postion="grave";

		int i = -1;
		foreach (var card in grave) {
			i += 1;
			if (card == cData) {
				break;
			}
		
		}

		grave.RemoveAt (i);

	}

	[PunRPC]
	public void addToPool(string c){


		List<string> colours = new Serializer ().DeserializeFromString<List<string>> (c);

		//if (colours.Count <2) {
			if (colours[0] != "n") {

		if (!manaPlay) {
			foreach (var colour in colours) {
				if (colour == "r") {

					power [0] += 1;

				}

				if (colour == "u") {

					power [1] += 1;

				}

				if (colour == "y") {

					power [2] += 1;

				}

				if (colour == "p") {

					power[3] += 1;

				}

				if (colour == "g") {

					power [4] += 1;

				}

				if (colour == "w") {

					power [5] += 1;

				}

			}
			manaPlay = true;
		}
	}
		//}
	}

	[PunRPC]
	public void Scry(bool top){
	
		if (top==false) {

			cardData c = deck [0];
			deck.RemoveAt(0);
			deck.Add (c);
		}
	
	}

	public int getMaxMana(){
		int mm = 0;

		mm += maxMana;

		foreach (var minion in minions) {
		
			mm +=minion.GetComponent<minionData> ().maxMana;
		
		}

		return mm;

	}

	[PunRPC]

	public void useAbility(int num){
	
		mana -= abilites [num].cost;
		abilites [num].active = false;
	
	}

	[PunRPC]
	public void addArmor(int a){

		armor += a;
	}

	void checkForFunctionPlus(string id){

		Debug.Log ("deck for plus: "+id);
		//r=0,u=1,y=2,p=3,g=4,w=5
		if (id == "aRup") {
			
			power [0] += 1;
		}

		if (id == "aUup") {

			power [1] += 1;
		}
		if (id == "aYup") {

			power [2] += 1;
		}
		if (id == "aPup") {

			power [3] += 1;
		}
		if (id == "aGup") {

			power [4] += 1;
		}
		if (id == "aWup") {

			power [5] += 1;
		}

	}

	public int getSpellDamage(){
		int sd = 0;
		foreach (var minion in minions) {
		
			sd+=minion.GetComponent<minionData> ().spellDamage;
		
		}

		return sd;
	}
}
