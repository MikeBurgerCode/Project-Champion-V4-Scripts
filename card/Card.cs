using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : Photon.PunBehaviour, IPunObservable {

	public string id,type,name,text,image,rarity="";

	public GameManager gManager;
	public Champion ownerChamp;
	public string owner;
	public cardData cData = new cardData();
	//public cardData originalData;
	public SpellData spell = new SpellData();



	public int[] powerCost = new int[6];
	public int cost, attack, health, summonIndex, finalCost;
	public float fontSize=2;
	// Use this for initialization

	//public bool taunt,charge,block,lifeSteal,silence,awaken,release=false;
	void Awake(){
		gManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		//originalData = new cardData();
		if (PhotonNetwork.isMasterClient) {
			switch (gManager.cardDrew.type) {
			case "minion":
				new LoadData ().makeMinionCard (gManager.cardDrew, this);
				break;
			case "spell":
				new LoadData ().makeSpellCard (gManager.cardDrew, this);
				break;

			case "equip":
				new LoadData ().makeEquipCard (gManager.cardDrew, this);
				break;
				break;

			case "mana":
				new LoadData ().makeManaCard (gManager.cardDrew, this);
				break;
			}

			getPowerCost ();
			cData.owner = gManager.playerDrew;
			owner = gManager.playerDrew;
			gManager.cardDrew = null;
			gManager.playerDrew = null;
			ownerChamp = GameObject.Find (owner).GetComponent<Champion> ();
			ownerChamp.hand.Add (this.gameObject);
			this.tag=owner+"Card";
			this.gameObject.transform.parent = GameObject.Find (owner + "Hand").transform;
		}
	
	}
	void Start () {
		//this.transform.getchild
	}


	public void GameUpdate(){

		cData.cp.Update (cData);
		finalCost = getCost ();
		attack = getAttack ();
		health = getHealth ();
	}

	// Update is called once per frame
	void Update () {

		if (PhotonNetwork.isMasterClient) {
		
		//	finalCost = getCost ();
			//cp.inHand = true;
			spell.cData = cData;
			spell.champTag = ownerChamp.tag;
		}

		getPowerCost ();
		if (owner!="") {
		this.tag=owner+"Card";
		this.gameObject.transform.parent = GameObject.Find (owner + "Hand").transform;
			ownerChamp = GameObject.Find (owner).GetComponent<Champion> ();
	

			if (ownerChamp != null) {
				if (ownerChamp.playerName == PhotonNetwork.player.NickName) {
					this.gameObject.transform.GetChild (0).transform.rotation = new Quaternion (0, 0, 0, 0);
				} else {
					this.gameObject.transform.GetChild (0).transform.rotation = new Quaternion (0, 180, 0, 0);
				}
			}
		}
	}

	public bool powerReq(){
	
		for (int i = 0; i < 5; i++) {
		
			if (ownerChamp.power [i] < powerCost [i]) {
			
				return false;
			}
		
		}
	
		return true;
	
	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting) {
			//stream.SendNext (owner);
			stream.SendNext (id);
			stream.SendNext (owner);

			stream.SendNext (name);
			stream.SendNext (type);
			stream.SendNext (text);
			stream.SendNext (image);

			stream.SendNext (cost);
			stream.SendNext (finalCost);
			stream.SendNext (attack);
			stream.SendNext (health);


			stream.SendNext (new Serializer().SerializeToString(cData));
			//stream.SendNext (new Serializer().SerializeToString(originalData));
			stream.SendNext (new Serializer().SerializeToString(spell));

			//stream.SendNext (new Serializer().SerializeToString(cp));

		} else {

			id=(string) stream.ReceiveNext();
			owner=(string) stream.ReceiveNext();

			name=(string) stream.ReceiveNext();
			type=(string) stream.ReceiveNext();
			text=(string) stream.ReceiveNext();
			image=(string) stream.ReceiveNext();

			cost=(int) stream.ReceiveNext();
			finalCost=(int) stream.ReceiveNext();
			attack=(int) stream.ReceiveNext();
			health=(int) stream.ReceiveNext();

		
			cData =new Serializer().DeserializeFromString<cardData> ((string)stream.ReceiveNext ());
			//originalData=new Serializer().DeserializeFromString<cardData> ((string)stream.ReceiveNext ());

			spell=new Serializer().DeserializeFromString<SpellData> ((string)stream.ReceiveNext ());

			//cp=new Serializer().DeserializeFromString<CardPassive> ((string)stream.ReceiveNext ());

		}
	}

	public int getAttack(){
		int newAtk = 0;
	

		newAtk += cData.attack;


		if (newAtk < 0) {

			newAtk = 0;
		}

		return newAtk;
	}

	public int getHealth(){
		int newHp = 0;


		newHp += cData.health;


		if (newHp < 0) {

			newHp = 0;
		}

		return newHp;
	}

	public int getCost(){
		int newCost = cData.cost;

		//Debug.Log (cData.cp.costBuff ());
		newCost += cData.cp.costBuff ();

		foreach (var minion in gManager.minions) {
		
			newCost += minion.mPass.costBuff (this);
		
		}


		if (newCost < 0) {
		
			newCost = 0;
		}

		return newCost;
	}

	void getPowerCost(){

		//r=0,u=1,y=2,p=3,g=4,w=5
		int r= 0;
		int u= 0;
		int y= 0;
		int p= 0;
		int g= 0;
		int w= 0;

		foreach (var power in cData.pool) {
			if (power == "r") {
				r += 1;
			}
		
			if (power == "u") {
				u += 1;
			}

			if (power == "w") {
				w += 1;
			}

			if (power == "p") {
				p += 1;
			}
			if (power == "g") {
				g += 1;
			}
			if (power == "w") {
				w += 1;
			}
		}

		powerCost [0] = r;
		powerCost [1] = u;
		powerCost [2] = y;
		powerCost [3] = p;
		powerCost [4] = g;
		powerCost [5] = w;
	
	}

	[PunRPC]
	void PlayMinion(int minionIndex){

		this.ownerChamp.mana -= this.finalCost;
		this.summonIndex = minionIndex;
		this.gManager.playCard = this;
		this.gManager.playing = true;
		PhotonNetwork.Instantiate("Minion", new Vector3(0f,0f,0f), Quaternion.identity, 0);
		//if (!ownerChamp.manaPlay) {

		ownerChamp.addToPool (new Serializer ().SerializeToString (cData.colour));
		
		//}
		//finalCost = getCost();

		//Debug.Log (this.finalCost);
		gManager.PlayCardTrigger (new Serializer().SerializeToString(cData), ownerChamp.tag);
	}

	[PunRPC]
	public void RemoveCard(){
		Debug.Log("remove");
		ownerChamp.hand.Remove(this.gameObject);
		PhotonNetwork.Destroy (this.gameObject);
	}

	[PunRPC]
	public void Discard(){
		ownerChamp.grave.Add(cData);
		RemoveCard ();
		gManager.DiscardTrigger (new Serializer ().SerializeToString (cData));
		cData.cp.discarded ();
	}

	[PunRPC]
	public void SilenceCard(){

		if (type == "minion") {
			cData.silence = true;

			cData.taunt = false;
			cData.charge = false;
			cData.block = false;
			cData.lifeSteal = false;
			cData.awaken = false;
			cData.text = "";

		}
	}


	void OnDestroy(){
	
	
	}
}
