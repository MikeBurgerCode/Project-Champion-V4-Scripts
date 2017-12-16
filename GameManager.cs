using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Linq;
using System.Linq;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager: Photon.PunBehaviour{
	public static GameManager Instance;
	public Card playCard = null;
	public cardData summonMinion = null;
	public cardData cardDrew;
	public string playerDrew;

	public int summonIndex = -1;
	public bool summoning=false;
	public bool playing=false;
	public GameObject playerPrefab;
	public GameObject TA;

	public Champion player1;
	public Champion player2;

	public ManaRender player1Mana;
	public ManaRender player2Mana;

	public GuiCardData player1Grave;
	public GuiCardData player2Grave;

	public GameObject player1Hand;
	public GameObject player2Hand;

	public GameObject player1Minions;
	public GameObject player2Minions;

	public List<minionData> minions = new List<minionData> ();
	public List<minionData> minionQueue= new List<minionData> ();

	public int minionCount=0;
	public int damageQueue = 1;

	// Use this for initialization
	void Start () {
		Instance = this;

		if (!PhotonNetwork.connected)
		{
			SceneManager.LoadScene("DeckList");

			return;
		}

		if (!PhotonNetwork.isMasterClient) {
		
			player1Mana.ownerChamp = player2;
			player2Mana.ownerChamp = player1;

			player1Grave.ownerChamp = player2;
			player2Grave.ownerChamp = player1;
		
			Vector3 player2Pos = player1.transform.position; 
			Vector3 player1Pos = player2.transform.position; 

			player1.transform.position = player1Pos;
			player2.transform.position = player2Pos;

			Vector3 p1MinionPos = player2Minions.transform.position;
			Vector3 p2MinionPos = player1Minions.transform.position;

			player1Minions.transform.position = p1MinionPos;
			player2Minions.transform.position = p2MinionPos;

			Vector3 p1Hand = player2Hand.transform.position;
			Vector3 p2Hand = player1Hand.transform.position;

			player1Hand.transform.position = p1Hand;
			player2Hand.transform.position = p2Hand;
		}

		var pos = PhotonNetwork.isMasterClient ? new Vector3(0f, 0f, 0f) : new Vector3(0f, 0f, 0f);
		PhotonNetwork.Instantiate(this.playerPrefab.name, pos, Quaternion.identity, 0);

	}
	
	// Update is called once per frame
	void Update () {

		minionCount = minions.Count;

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			//LeaveRoom ();
			//this.photonView.RPC ("LeaveRoom", PhotonTargets.All);
		}
	}

	[PunRPC]
	public void GameUpdate(){
		foreach (var minion in minions) {
			minion.gameUpdate ();
		
		}

		checkMinionsLife ();


		foreach (var card in player1.hand) {

			card.GetComponent<Card> ().GameUpdate ();
		
		}

		foreach (var card in player2.hand) {

			card.GetComponent<Card> ().GameUpdate ();

		}

		player1.GameUpdate ();
		player2.GameUpdate ();
	}

	[PunRPC]
	public void checkMinionsLife(){

		if (minions.Count > 0) {
			while (damageQueue != 0) {
				minionQueue.Clear ();

				foreach (var minion in minions) {
					minionQueue.Add (minion);

				}
				bool notEmpty = true;


				while (notEmpty) {

					minionData mData = minionQueue [0];
					minionQueue.RemoveAt (0);

					mData.lifeCheck ();

					if (minionQueue.Count < 1) {
						notEmpty = false;
						damageQueue -= 1;

					}

				}
			}
			damageQueue = 1;
		}

		bool p1 =false, p2 = false;

		if (player1.health < 1) {
			p1 = true;
		}

		if (player2.health < 1) {
			p2 = true;
		}

		if (p1==true) {
			player1.busy = true;
			player2.busy = true;
			EndGame ("You Lose");
			this.photonView.RPC ("EndGame", PhotonTargets.Others, "You Win");

		}

		if (p2==true) {

			player1.busy = true;
			player2.busy = true;
			EndGame ("You Win");
			this.photonView.RPC ("EndGame", PhotonTargets.Others, "You Lose");
		}

		if (p1&&p2) {
			
			player1.busy = true;
			player2.busy = true;
			EndGame ("Draw");
			this.photonView.RPC ("EndGame", PhotonTargets.Others, "Draw");
		}
	}

	[PunRPC]
	public void MinionAttackM(int m1, int m2){
	
		minionData attacker = minions [m1];
		minionData defender = minions [m2];

		attacker.BattleMinion (defender);

		//attacker.exhaust = true;
	
	}
	[PunRPC]
	public void MinionAttackC(int minion, string champ){

		minionData attacker = minions [minion];
		Champion defener = GameObject.FindGameObjectWithTag (champ).GetComponent<Champion> ();

		attacker.BattleChampion(defener);


	}
	public void LeaveRoom()
	{
		//PhotonNetwork.LeaveRoom();
	}
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting) {
			//stream.SendNext (owner);
		//	stream.SendNext (id);

			//stream.SendNext (new Serializer().SerializeToString(minions));
			stream.SendNext (minionCount);
		} else {

			//minions=new Serializer().DeserializeFromString<List<minionData>> ((string)stream.ReceiveNext ());
		//	id=(string) stream.ReceiveNext();
			minionCount = (int) stream.ReceiveNext();
		}
	}

	[PunRPC]
	public void StartTurn(){
	
	
	}

	[PunRPC]
	public void EndTurn(){

	}

	[PunRPC]
	public void createTargetAwaken(string awakenData){


		Debug.Log (GameObject.Find ("cursorStatus").GetComponent<cursorStatus> ().status);

		GameObject targetAwaken = Instantiate (TA);
		Awaken awaken = new Serializer ().DeserializeFromString<Awaken> (awakenData);
		targetAwaken.GetComponent<TargetAwaken> ().awaken = awaken;


	}

	[PunRPC]

	public void createAwaken(string awakenData){
		Awaken awaken = new Serializer ().DeserializeFromString<Awaken> (awakenData);
		awaken.play ();

		
	}

	[PunRPC]
	public void RemoveMinion(int i){
	
		minions.RemoveAt (i);

	}
	[PunRPC]

	public void EndGame(string text){

		GameObject vicGui = GameObject.Find ("Canvas").transform.Find ("VicGui").gameObject;
		vicGui.SetActive (true);
		vicGui.transform.Find ("text").GetComponent<TextMeshProUGUI> ().text = text;


	}

	[PunRPC]

	public void PlayCardTrigger(string data,string ownerTag){

		cardData cData = new Serializer ().DeserializeFromString<cardData> (data);

		foreach (var minion in minions) {
		
			minion.mPass.playTrigger (cData, ownerTag);

			foreach (var mPass in minion.passList) {
			
				mPass.playTrigger (cData, ownerTag);
			}

		}

		List<CardPassive> hand = new List<CardPassive> ();
		foreach (var card in player1.hand) {
		
			hand.Add (card.GetComponent<Card> ().cData.cp);
		
		}

		foreach (var card in hand) {
		
			card.playTrigger (cData);
		}

		hand = new List<CardPassive> ();

		foreach (var card in player2.hand) {

			hand.Add (card.GetComponent<Card> ().cData.cp);

		}

		foreach (var card in hand) {

			card.playTrigger (cData);
		}


		List<CardPassive> deck = new List<CardPassive> ();

		foreach (var card in player1.deck) {

			deck.Add (card.cp);

		}

		foreach (var card in deck) {

			card.playTrigger (cData);
		}


		deck = new List<CardPassive> ();

		foreach (var card in player2.deck) {

			deck.Add (card.cp);

		}

		foreach (var card in deck) {

			card.playTrigger (cData);
		}

		List<CardPassive> grave = new List<CardPassive> ();

		foreach (var card in player1.grave) {

			grave.Add (card.cp);

		}

		foreach (var card in grave) {

			card.playTrigger (cData);
		}

		grave = new List<CardPassive> ();

		foreach (var card in player2.grave) {

			grave.Add (card.cp);

		}
		foreach (var card in grave) {

			card.playTrigger (cData);
		}
	}

	//[PunRPC]
	public void DamageTrigger(int damage, minionData mData){

		foreach (var minion in minions) {

			minion.mPass.damageTrigger (damage, mData);

			foreach (var mPass in minion.passList) {

				mPass.damageTrigger (damage, mData);
			}

		}

		List<CardPassive> hand = new List<CardPassive> ();
		foreach (var card in player1.hand) {

			hand.Add (card.GetComponent<Card> ().cData.cp);

		}

		foreach (var card in hand) {

			card.damageTrigger (damage, mData);
		}

		hand = new List<CardPassive> ();

		foreach (var card in player2.hand) {

			hand.Add (card.GetComponent<Card> ().cData.cp);

		}

		foreach (var card in hand) {

			card.damageTrigger (damage, mData);
		}


		List<CardPassive> deck = new List<CardPassive> ();

		foreach (var card in player1.deck) {

			deck.Add (card.cp);

		}

		foreach (var card in deck) {

			card.damageTrigger (damage, mData);
		}


		deck = new List<CardPassive> ();

		foreach (var card in player2.deck) {

			deck.Add (card.cp);

		}

		foreach (var card in deck) {

			card.damageTrigger (damage, mData);
		}

		List<CardPassive> grave = new List<CardPassive> ();

		foreach (var card in player1.grave) {

			grave.Add (card.cp);

		}

		foreach (var card in grave) {

			card.damageTrigger (damage, mData);
		}

		grave = new List<CardPassive> ();

		foreach (var card in player2.grave) {

			grave.Add (card.cp);

		}
		foreach (var card in grave) {

			card.damageTrigger (damage, mData);
		}
	
	}

	public void HealMinionTrigger(int heal, minionData mData){

		foreach (var minion in minions) {

			minion.mPass.healTrigger (heal, mData);

			foreach (var mPass in minion.passList) {

				mPass.healTrigger (heal, mData);
			}

		}

		List<CardPassive> hand = new List<CardPassive> ();
		foreach (var card in player1.hand) {

			hand.Add (card.GetComponent<Card> ().cData.cp);

		}

		foreach (var card in hand) {

			card.healTrigger (heal, mData);
		}

		hand = new List<CardPassive> ();

		foreach (var card in player2.hand) {

			hand.Add (card.GetComponent<Card> ().cData.cp);

		}

		foreach (var card in hand) {

			card.healTrigger (heal, mData);
		}


		List<CardPassive> deck = new List<CardPassive> ();

		foreach (var card in player1.deck) {

			deck.Add (card.cp);

		}

		foreach (var card in deck) {

			card.healTrigger (heal, mData);
		}


		deck = new List<CardPassive> ();

		foreach (var card in player2.deck) {

			deck.Add (card.cp);

		}

		foreach (var card in deck) {

			card.healTrigger (heal, mData);
		}

		List<CardPassive> grave = new List<CardPassive> ();

		foreach (var card in player1.grave) {

			grave.Add (card.cp);

		}

		foreach (var card in grave) {

			card.healTrigger (heal, mData);
		}

		grave = new List<CardPassive> ();

		foreach (var card in player2.grave) {

			grave.Add (card.cp);

		}
		foreach (var card in grave) {

			card.healTrigger (heal, mData);
		}
	}

	[PunRPC]
	public void HealChampTrigger(int heal,Champion champ){

		foreach (var minion in minions) {

			minion.mPass.healTrigger (heal, champ);

			foreach (var mPass in minion.passList) {

				mPass.healTrigger (heal, champ);
			}

		}

		List<CardPassive> hand = new List<CardPassive> ();
		foreach (var card in player1.hand) {

			hand.Add (card.GetComponent<Card> ().cData.cp);

		}

		foreach (var card in hand) {

			card.healTrigger (heal, champ);
		}

		hand = new List<CardPassive> ();

		foreach (var card in player2.hand) {

			hand.Add (card.GetComponent<Card> ().cData.cp);

		}

		foreach (var card in hand) {

			card.healTrigger (heal, champ);
		}


		List<CardPassive> deck = new List<CardPassive> ();

		foreach (var card in player1.deck) {

			deck.Add (card.cp);

		}

		foreach (var card in deck) {

			card.healTrigger (heal, champ);
		}


		deck = new List<CardPassive> ();

		foreach (var card in player2.deck) {

			deck.Add (card.cp);

		}

		foreach (var card in deck) {

			card.healTrigger (heal, champ);
		}

		List<CardPassive> grave = new List<CardPassive> ();

		foreach (var card in player1.grave) {

			grave.Add (card.cp);

		}

		foreach (var card in grave) {

			card.healTrigger (heal, champ);
		}

		grave = new List<CardPassive> ();

		foreach (var card in player2.grave) {

			grave.Add (card.cp);

		}
		foreach (var card in grave) {

			card.healTrigger (heal, champ);
		}

	}

	[PunRPC]

	public void DiscardTrigger(string data){
	
		cardData cData = new Serializer ().DeserializeFromString<cardData> (data);

		foreach (var minion in minions) {

			minion.mPass.discardTrigger (cData);

			foreach (var mPass in minion.passList) {

				mPass.discardTrigger (cData);
			}

		}

		List<CardPassive> hand = new List<CardPassive> ();
		foreach (var card in player1.hand) {

			hand.Add (card.GetComponent<Card> ().cData.cp);

		}

		foreach (var card in hand) {

			card.discardTrigger (cData);
		}

		hand = new List<CardPassive> ();

		foreach (var card in player2.hand) {

			hand.Add (card.GetComponent<Card> ().cData.cp);

		}

		foreach (var card in hand) {

			card.discardTrigger (cData);
		}


		List<CardPassive> deck = new List<CardPassive> ();

		foreach (var card in player1.deck) {

			deck.Add (card.cp);

		}

		foreach (var card in deck) {

			card.discardTrigger (cData);
		}


		deck = new List<CardPassive> ();

		foreach (var card in player2.deck) {

			deck.Add (card.cp);

		}

		foreach (var card in deck) {

			card.discardTrigger (cData);
		}

		List<CardPassive> grave = new List<CardPassive> ();

		foreach (var card in player1.grave) {

			grave.Add (card.cp);

		}

		foreach (var card in grave) {

			card.discardTrigger (cData);
		}

		grave = new List<CardPassive> ();

		foreach (var card in player2.grave) {

			grave.Add (card.cp);

		}
		foreach (var card in grave) {

			card.discardTrigger (cData);
		}
	}


	//[PunRPC]

	public void SummonTrigger(minionData mData){


	}


	public void DeathTrigger(minionData mData){


	}

	//[pun


}
