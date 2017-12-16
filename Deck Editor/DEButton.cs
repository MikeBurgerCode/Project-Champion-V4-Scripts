using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEButton : MonoBehaviour {

	public bool foward;
	public DeckEdiotr DE;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseOver(){
		if (Input.GetMouseButtonDown (0)) {
		
			if (foward) {
				DE.page += 6;
				//DE.
			
			} else {
				DE.page -= 6;
			}
		
		}
	}
}
