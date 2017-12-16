using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml;
using System.IO;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine;

public class PlayerNameInput : MonoBehaviour {

	// Use this for initialization
	static string playerNamePrefKey = "PlayerName";

	void Start () {
		string defaultName = "";
		TMP_InputField _inputField = this.GetComponent<TMP_InputField>();

		if (_inputField!=null)
		{
			if (PlayerPrefs.HasKey(playerNamePrefKey))
			{
				defaultName = PlayerPrefs.GetString(playerNamePrefKey);
				_inputField.text = defaultName;
			}
		}

		PhotonNetwork.playerName =	defaultName;
	}
	
	public void SetPlayerName(string value)
	{
		// #Important
		TMP_InputField _inputField = this.GetComponent<TMP_InputField>();
		value = _inputField.text;
		PhotonNetwork.playerName = value + " "; // force a trailing space string in case value is an empty string, else playerName would not be updated.

		PlayerPrefs.SetString(playerNamePrefKey,value);
	}
}
