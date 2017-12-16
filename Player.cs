using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;


public class Player : Photon.PunBehaviour, IPunObservable {


	public GameManager gManager;

	//

	// Use this for initialization
	void Start () {
		var path =File.ReadAllText (Application.persistentDataPath + "/Decks/"+gameStat.currentDeck, Encoding.UTF8);
		gManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();

		if (photonView.isMine) {
			if (PhotonNetwork.isMasterClient) {
				Debug.Log ("you are master");
				gManager.player1.loadDeck (path, PhotonNetwork.playerName);

			} else {
				Debug.Log ("you are client");
				gManager.player2.photonView.RPC ("loadDeck", PhotonTargets.MasterClient, path, PhotonNetwork.playerName);


			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting) {

			//stream.SendNext (mouseX);
			//stream.SendNext (mouseY);
			//stream.SendNext (GameObject.Find("cursorStatus").GetComponent<cursorStatus>().i);

		} else {

			//mouseX = (float)stream.ReceiveNext ();
			//mouseY = (float)stream.ReceiveNext ();
			//cursorStatus=(int)stream.ReceiveNext ();
		}
	}
}
