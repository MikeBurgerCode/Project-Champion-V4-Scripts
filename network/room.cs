using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class room : MonoBehaviour {

	// Use this for initialization

	public string roomName;
	public int playerCount;
	public int maxPlayer;
	public Text txt;
	public LobbyManager LM;

	public Button roomButton;


	void Awake () {
		LM = GameObject.Find ("lobbyManager").GetComponent<LobbyManager>();

		
	}
	
	// Update is called once per frame
	void Update () {
		if (playerCount < maxPlayer) {

			roomButton.interactable = true;

		} else {
			roomButton.interactable = false;
		
		}

		txt.text = roomName + " " + playerCount + "/" + maxPlayer;
	}

	public void joinRoom(){
		

		LM.joinRoom (roomName);

	
	}
}
