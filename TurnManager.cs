using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : Photon.PunBehaviour, IPunObservable  {

	public GameManager gManager;
	Champion firstPlayer;
	public bool player1Ready, player2Ready= false;

	public string player1Name, player2Name="";
	public bool setUpReady = false;
	public bool gameStart = false;
	public string playerTurn;
	public EndButton endButton;
	public GameObject mButton1,mButton2;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (PhotonNetwork.isMasterClient && !setUpReady) {

			if (PhotonNetwork.playerList.Length == 2) {
				//if (gManager.player2.champId != "") {
				if (gManager.player2.deckLoaded) {
					determineWhoFirst ();
				}

			//	}

			}

			if (gameStart) {
			
			}

		}
	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			
			stream.SendNext(gameStart);
			stream.SendNext (playerTurn);
			stream.SendNext(player1Name);
			stream.SendNext(player2Name);
			stream.SendNext(player1Ready);
			stream.SendNext(player2Ready);


		}
		else
		{
			
			gameStart = (bool) stream.ReceiveNext();
			playerTurn= (string) stream.ReceiveNext();
			player1Name = (string) stream.ReceiveNext();
			player2Name = (string) stream.ReceiveNext();
			player1Ready = (bool) stream.ReceiveNext();
			player2Ready = (bool) stream.ReceiveNext();

		}
	}

	public void determineWhoFirst(){

		if (PhotonNetwork.playerList [0].NickName == PhotonNetwork.playerList [1].NickName) {
			Debug.Log ("same name");
		
			foreach (var player in PhotonNetwork.playerList) {
				if (player.IsMasterClient) {
					player.NickName = player.NickName + "X";
					player1Name = player.NickName;
					gManager.player1.playerName = player.NickName;
				} else {
					player2Name = player.NickName;
					gManager.player2.playerName = player.NickName;

				}
			}
		} else {
		
			foreach (var player in PhotonNetwork.playerList) {
				if (player.IsMasterClient) {
					player1Name = player.NickName;
					gManager.player1.playerName = player.NickName;
				} else {
					player2Name = player.NickName;
					gManager.player2.playerName = player.NickName;

				}
			}
		}

		int first = Random.Range (0, 2);


		if (first == 0) {
			playerTurn = player1Name;
			firstPlayer = gManager.player1;
			
		} else {
			playerTurn = player2Name;
			firstPlayer = gManager.player2;


		}


		gManager.player1.draw ();
		gManager.player1.draw ();
		gManager.player1.draw ();

		gManager.player2.draw ();
		gManager.player2.draw ();
		gManager.player2.draw ();

		setUpReady=true;

		this.photonView.RPC ("mullButtonsActive", PhotonTargets.All);
	}
	[PunRPC]
	public void mullButtonsActive(){
	
		mButton1.SetActive (true);
		mButton2.SetActive (true);
	}

	[PunRPC]
	public void ready(string name){
		if(name == player1Name){
			player1Ready = true;

			if (name != playerTurn) {
				//gManager.player1.addManaPotion ();
			}
			//gManager.player1.ready = true;
		}

		if(name == player2Name){
			player2Ready = true;

			if (name != playerTurn) {
				//gManager.player2.addManaPotion ();
			}
			//gManager.player2.ready = true;
		}

		if(player1Ready && player2Ready){
			gameStart = true;
			firstPlayer.photonView.RPC ("StartTurn", PhotonTargets.All);
			//this.photonView.RPC ("StartGame", PhotonTargets.All);
		}

	}

	[PunRPC]
	public void startTurn(){
		endButton.c = new Color (1f, 1f, 1f);
	
	}

	[PunRPC]
	public void endTurn(){
		endButton.c = new Color (0.5f, 0.5f, 0.5f);
		gManager.photonView.RPC ("EndTurn", PhotonTargets.MasterClient);
		if (PhotonNetwork.isMasterClient) {
			SetTurn (gManager.player2.playerName);
			gManager.player1.photonView.RPC ("EndTurn", PhotonTargets.All);
			this.photonView.RPC ("startTurn", PhotonTargets.Others);
			gManager.player2.photonView.RPC ("StartTurn", PhotonTargets.All);
			
		} else {
			SetTurn (gManager.player1.playerName);
			gManager.player2.photonView.RPC ("EndTurn", PhotonTargets.All);
			this.photonView.RPC ("SetTurn", PhotonTargets.MasterClient, gManager.player1.playerName);
			this.photonView.RPC ("startTurn", PhotonTargets.MasterClient);
			gManager.player1.photonView.RPC ("StartTurn", PhotonTargets.All);

			//startTurn
		}
		//gManager.photonView.RPC ("StartTurn", PhotonTargets.MasterClient);
	}
	[PunRPC]
	void SetTurn(string name){
		playerTurn = name;
	}
	public bool isMyTurn(string playerName){

		//Debug.Log (playerName+""+playerTurn);

		if (playerName == playerTurn) {
			//Debug.Log ("your turn");
			return true;
		
		} else {
			//Debug.Log ("not uour turn");
			return false;
		}
	}

}
