using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;

public class DEAbi : MonoBehaviour {

	public Image art;
	public int num=0;
	public DeckEdiotr DE;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (DE.Abilites [num].id != "") {
			
			art.sprite= Resources.Load ("Sprites/Abilities/"+DE.Abilites[num].image, typeof(Sprite)) as Sprite;

		} else {
		
			art.sprite= Resources.Load ("Sprites/Buttons/addAbi", typeof(Sprite)) as Sprite;

		}
	}
}
