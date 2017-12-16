using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEcardList : MonoBehaviour {

	public Transform content;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		content.Find ("Top").SetAsFirstSibling ();
		content.Find ("Bottom").SetAsLastSibling ();
		
	}
}
