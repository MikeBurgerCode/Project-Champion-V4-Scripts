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
using UnityEngine.UI;

public class LobbyManager :  Photon.PunBehaviour {

	public GameObject roomPrefab;
	public string gameScene;
	byte maxPlayersPerRoom =2;
	public Button roomButton;
	public TextMeshProUGUI deckText;
	public TextMeshProUGUI roomText;
	public TextMeshProUGUI verText;
	public GameObject rooms;
	#region Private Variables
	/// <summary>
	/// Keep track of the current process. Since connection is asynchronous and is based on several callbacks from Photon, 
	/// we need to keep track of this to properly adjust the behavior when we receive call back by Photon.
	/// Typically this is used for the OnConnectedToMaster() callback.
	/// </summary>
	bool isConnected=false;

	/// <summary>
	/// This client's version number. Users are separated from each other by gameversion (which allows you to make breaking changes).
	/// </summary>
	/// 
	string _gameVersion = "Ver2";

	#endregion


	// Use this for initialization
	void Awake () {
		PhotonNetwork.ConnectUsingSettings(_gameVersion);
		PhotonNetwork.autoJoinLobby = true;
	}
	
	// Update is called once per frame
	void Update () {

		verText.text = _gameVersion;
		deckText.text = "Current Deck: " + Path.GetFileNameWithoutExtension (gameStat.currentDeck);

		if (PhotonNetwork.connected) {
			if (!isConnected) {

				roomButton.interactable = true;
			}
			isConnected = true;
			//reloadLobby ();

		} else {
			isConnected = false;
			Debug.Log ("not connected");
			PhotonNetwork.ConnectUsingSettings(_gameVersion);
		}
	}

	public void makeRoom(){

		roomButton.interactable = false;
		roomText.text="Creating Room...";
		string roomName = PhotonNetwork.playerName + " room";
		PhotonNetwork.CreateRoom(roomName, new RoomOptions() { MaxPlayers = this.maxPlayersPerRoom}, null);
	}

	public void reloadLobby(){
		if(isConnected){
			foreach (Transform child in rooms.transform) {
				GameObject.Destroy(child.gameObject);
			}
			foreach (RoomInfo game in PhotonNetwork.GetRoomList()) {
				GameObject room = roomPrefab;

				Instantiate (room,rooms.transform);
				room.GetComponent<room> ().roomName = game.Name;
				room.GetComponent<room> ().playerCount = game.PlayerCount;
				room.GetComponent<room> ().maxPlayer = game.MaxPlayers;
				//room.transform.SetParent (rooms.transform);
			}

		}
	}

	public void goToDeckList(){
	
		SceneManager.LoadScene ("DeckList",LoadSceneMode.Single);
	}

	public void joinRoom(string roomName){
		PhotonNetwork.JoinRoom (roomName);

	}
	public override void OnJoinedRoom()
	{
		//LogFeedback("<Color=Green>OnJoinedRoom</Color> with "+PhotonNetwork.room.PlayerCount+" Player(s)");
		//Debug.Log("DemoAnimator/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.\nFrom here on, your game would be running. For reference, all callbacks are listed in enum: PhotonNetworkingMessage");

		// #Critical: We only load if we are the first player, else we rely on  PhotonNetwork.automaticallySyncScene to sync our instance scene.

		//	Debug.Log("We load the 'Room for 1' ");

			// #Critical
			// Load the Room Level. 
			PhotonNetwork.LoadLevel(gameScene);

	
	}
}
