using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPassFactory  {

	CardPassive cp= new CardPassive();

	public CardPassive getCardPassive(string id){
	
		if (id == "test") {
		
			cp = new TestCardPass ();

		
		}
		cp.passName = id;
		return cp;
	}
}
