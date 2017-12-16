using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActiveButton : MonoBehaviour {

	public Active active;
	public minionData mData;
	public TextMeshProUGUI text;


	// Use this for initialization
	void Start () {
		
	}


	// Update is called once per frame
	void Update () {

		text.text = active.text;

	}

	public void click(){
	
		if (active.cost <= mData.ownerChamp.mana) {

			active.cast (mData);
		}
		Destroy (GameObject.FindGameObjectWithTag("ActiveList"));
	
	}
}
