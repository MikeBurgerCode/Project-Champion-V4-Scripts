using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonScripts : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void leaveGame(){

		PhotonNetwork.LeaveRoom();
		SceneManager.LoadScene ("Lobby",LoadSceneMode.Single);

	}
}
