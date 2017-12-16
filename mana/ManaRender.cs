using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ManaRender : MonoBehaviour {

	public Champion ownerChamp;
	public TextMeshProUGUI manaText;
	public TextMeshProUGUI poolText;
	public imageResize poolContainer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		manaText.text = "" + ownerChamp.mana + "/" + ownerChamp.currentMaxMana;

		string poolData = "";
		int poolLength = -70;

		if (ownerChamp.power [0] > 0) {
		
			poolData +="<sprite name=\"r\">:"+ownerChamp.power [0]+" ";
			poolLength += 70;

			if (ownerChamp.power [0] > 9) {
				poolLength += 35/2;
			}
		}
		if (ownerChamp.power [1] > 0) {

			poolData +="<sprite name=\"u\">:"+ownerChamp.power [1]+" ";
			poolLength += 70;

			if (ownerChamp.power [1] > 9) {
				poolLength += 35/2;
			}
		}
		if (ownerChamp.power [2] > 0) {

			poolData +="<sprite name=\"y\">:"+ownerChamp.power [2]+" ";
			poolLength += 70;

			if (ownerChamp.power [2] > 9) {
				poolLength += 35/2;
			}
		}
		if (ownerChamp.power [3] > 0) {

			poolData +="<sprite name=\"p\">:"+ownerChamp.power [3]+" ";
			poolLength += 70;

			if (ownerChamp.power [3] > 9) {
				poolLength += 35/2;
			}
		}
		if (ownerChamp.power [4] > 0) {

			poolData +="<sprite name=\"g\">:"+ownerChamp.power [4]+" ";
			poolLength += 70;

			if (ownerChamp.power [4] > 9) {
				poolLength += 35/2;
			}
		}
		if (ownerChamp.power [5] > 0) {

			poolData +="<sprite name=\"w\">:"+ownerChamp.power [5]+" ";
			poolLength += 70;

			if (ownerChamp.power [4] > 9) {
				poolLength += 35/2;
			}
		}

		poolContainer.ratio = poolLength;
		poolText.text = poolData;
	}
}
